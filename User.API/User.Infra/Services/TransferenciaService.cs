using Microsoft.EntityFrameworkCore;
using User.Application.Dtos.TransferenciasDto;
using User.Application.Interfaces;
using User.Domain.Entities;
using User.Infra.Data;

namespace User.Infra.Services;

public class TransferenciaService : ITransferenciaService
{
    private readonly UserDbContext _context;

    public TransferenciaService(UserDbContext context)
    {
        _context = context;
    }

    public async Task<TransferenciaResponseDto> TransferirAsync(TransferenciaRequestDto request)
    {
        var usuarioRemetenteId = request.UsuarioRemetenteId;
        var usuarioDestinatarioId = request.UsuarioDestinatarioId;
        var valor = request.Valor;
        var chaveIdempotencia = request.ChaveIdempotencia ?? Guid.NewGuid();

        // Verifica idempotência
        var existente = await _context.Set<Transacao>()
            .Include(t => t.CarteiraDestinatario)
            .ThenInclude(c => c.Usuario)
            .FirstOrDefaultAsync(t => t.ChaveIdempotencia == chaveIdempotencia);

        if (existente != null)
        {
            return new TransferenciaResponseDto
            {
                TransacaoId = existente.Id,
                Valor = existente.Valor,
                Destinatario = existente.CarteiraDestinatario.Usuario.Nome,
                Status = existente.Status.ToString(),
                DataHora = existente.DataHora
            };
        }

        if (valor <= 0)
            throw new ArgumentException("Valor deve ser maior que zero.");

        if (usuarioRemetenteId == usuarioDestinatarioId)
            throw new InvalidOperationException("Transferência inválida.");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var remetente = await _context.Carteiras
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioRemetenteId);

            var destinatario = await _context.Carteiras
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioDestinatarioId);

            if (remetente == null || destinatario == null)
                throw new InvalidOperationException("Usuário ou carteira não encontrada.");

            remetente.Debitar(valor);
            destinatario.Creditar(valor);

            var transacaoNova = new Transacao(
                remetente.Id,
                destinatario.Id,
                valor,
                chaveIdempotencia
            );

            transacaoNova.MarcarComoConcluida();

            _context.Set<Transacao>().Add(transacaoNova);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new TransferenciaResponseDto
            {
                TransacaoId = transacaoNova.Id,
                Valor = valor,
                Destinatario = destinatario.Usuario.Nome,
                Status = "Concluida",
                DataHora = transacaoNova.DataHora
            };
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException("Erro de concorrência. Tente novamente.");
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}