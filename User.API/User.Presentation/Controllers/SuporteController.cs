using Microsoft.AspNetCore.Mvc;
using User.Application.Helpers;
using User.Application.Interfaces;

namespace User.API.Controllers;

[ApiController]
[Route("api/suporte")]
public class SuporteController : ControllerBase
{
    private readonly ISuporteRepository _suporteService;

    public SuporteController(ISuporteRepository suporteService)
    {
        _suporteService = suporteService;
    }

    /// <summary>
    /// Retorna todos os usuários que moram em uma determinada cidade ou estado
    /// </summary>
    /// <param name="endereco">Nome do endereço (Estado/Cidade) </param>
    /// <response code="200">Lista de usuários encontrados</response>
    /// <response code="404">Nenhum usuário encontrado</response>
    [HttpGet("exportar-usuarios-por-cidade")]
    public async Task<IActionResult> ExportarUsuariosPorEndereco([FromQuery] string endereco)
    {
        var usuarios = await _suporteService.ObterUsuariosPorEndereco(endereco);

        if (!usuarios.Any())
            return NotFound("Nenhum usuário encontrado para esse endereço");

        var csvBytes = CsvHelper.GerarUsuariosCsv(usuarios);

        return File(
            csvBytes,
            "text/csv",
            $"usuarios_{endereco}.csv"
        );
    }

    /// <summary>
    /// Retorna as transações de um usuario pelo documento
    /// </summary>
    /// <param name="documento">Documento do usuario (CPF/CNPJ) </param>
    /// <response code="200">Transações</response>
    [HttpGet("transacoes")]
    public async Task<IActionResult>
    ObterTransacoesPorDocumento([FromQuery] string documento)
    {
        var result = await _suporteService.ObterTransacoesPorDocumento(documento);
        return Ok(result);
    }

    /// <summary>
    /// Retorna o saldo de um usuario pelo documento
    /// </summary>
    /// <param name="documento">Documento do usuario (CPF/CNPJ) </param>
    /// <response code="200">Saldo disponivel</response>
    [HttpGet("Obter-saldo-por-documento")]
    public async Task<IActionResult>
    ObterSaldoPorDocumento([FromQuery] string documento)
    {
        var result = await _suporteService.ObterSaldoPorDocumento(documento);
        return Ok(result);
    }

    /// <summary>
    /// Retorna o extrato de uma transação
    /// </summary>
    /// <param name="transacaoId">Id da transação</param>
    /// <response code="200">Transação</response>
    [HttpGet("Obter-extrato")]
    public async Task<IActionResult>
    ObterExtrato([FromQuery] Guid transacaoId)
    {
        var result = await _suporteService.ObterComprovante(transacaoId);
        return Ok(result);
    }

    /// <summary>
    /// Exporta o extrato de uma transação em PDF
    /// </summary>
    /// <param name="transacaoId">Id da transação</param>
    /// <response code="200">Transação</response>
    [HttpGet("comprovante-pdf")]
    public async Task<IActionResult> BaixarComprovante(Guid transacaoId)
    {
        var pdf = await _suporteService.ObterComprovantePdf(transacaoId);

        return File(pdf, "application/pdf", "comprovante.pdf");
    }
}
