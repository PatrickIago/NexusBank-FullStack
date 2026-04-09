using AutoMapper;
using Microsoft.EntityFrameworkCore;
using User.Application.Dtos.CarteiraDto;
using User.Application.Dtos.TransferenciasDto;
using User.Application.Dtos.UserDtos;
using User.Application.Interfaces;
using User.Infra.Data;
namespace User.Infra.Services;

public class SuporteRepository : ISuporteRepository
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPdfGenerator _generator;

    public SuporteRepository(UserDbContext context, IMapper mapper, IPdfGenerator generator)
    {
        _context = context;
        _mapper = mapper;
        _generator = generator;

    }

    public async Task<List<UserDto>> ObterUsuariosPorEndereco(string endereco)
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Endereco)
            .Where(u =>
                u.Endereco != null &&
               (
                 u.Endereco.Cidade.ToLower() == endereco.ToLower()
                || u.Endereco.Estado.ToLower() == endereco.ToLower()
               )
            )
            .ToListAsync();

        return _mapper.Map<List<UserDto>>(usuarios);
    }


    public async Task<List<TransferenciaResponseDto>>
        ObterTransacoesPorDocumento(string documento)
    {
        documento = new string(documento.Where(char.IsDigit).ToArray());

        var usuario = await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Documento == documento);

        if (usuario == null)
            throw new InvalidOperationException("Usuário não encontrado.");

        var transacoes = await _context.Transacoes
            .Include(t => t.CarteiraRemetente)
                .ThenInclude(c => c.Usuario)
            .Include(t => t.CarteiraDestinatario)
                .ThenInclude(c => c.Usuario)
            .Where(t => t.CarteiraRemetente.UsuarioId == usuario.Id
                     || t.CarteiraDestinatario.UsuarioId == usuario.Id)
            .OrderByDescending(t => t.DataHora)
            .ToListAsync();

        return transacoes.Select(t => new TransferenciaResponseDto
        {
            TransacaoId = t.Id,
            Valor = t.Valor,
            Destinatario = t.CarteiraDestinatario.Usuario.Nome,
            Status = t.Status.ToString(),
            DataHora = t.DataHora
        }).ToList();
    }

    public async Task<SaldoDto> ObterSaldoPorDocumento(string documento)
    {
        documento = new string(documento.Where(char.IsDigit).ToArray());

        var usuario = await _context.Usuarios
            .AsNoTracking()
            .Include(c => c.Carteira)
            .FirstOrDefaultAsync(u => u.Documento == documento);

        if (usuario == null)
            throw new InvalidOperationException("Usuario não encontrado");

        var carteiras = await _context.Carteiras
            .FirstOrDefaultAsync(c => c.UsuarioId == usuario.Id);

        if (carteiras == null)
            throw new InvalidOperationException("Carteira não encontrada.");

        return new SaldoDto
        {
            Mensagem = $"Saldo disponivel para o cliente {usuario.Nome} e de documento {usuario.Documento}",
            Saldo = carteiras.Saldo,
        };
    }

    public async Task<ComprovanteTransferenciaDto> ObterComprovante(Guid transacaoId)
    {
        var transacao = await _context.Transacoes
            .Include(t => t.CarteiraRemetente)
               .ThenInclude(c => c.Usuario)
            .Include(t => t.CarteiraDestinatario)
               .ThenInclude(c => c.Usuario)
            .FirstOrDefaultAsync(t => t.Id == transacaoId);

        if (transacao == null)
            throw new InvalidOperationException("Transação não encontrada.");

        return new ComprovanteTransferenciaDto
        {
            Mensagem = "Comprovante de transferência",
            TransacaoId = transacao.Id,
            DataHora = transacao.DataHora,
            Destinatario = transacao.CarteiraDestinatario.Usuario.Nome,
            Remetente = transacao.CarteiraRemetente.Usuario.Nome,
            DocumentoRemetente = transacao.CarteiraRemetente.Usuario.Documento,
            Valor = transacao.Valor,
        };

    }
    public async Task<byte[]> ObterComprovantePdf(Guid transacaoId)
    {
        var comprovante = await ObterComprovante(transacaoId);

        return _generator.GerarPdf(comprovante);
    }
}
