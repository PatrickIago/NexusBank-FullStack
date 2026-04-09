using Microsoft.AspNetCore.Mvc;
using User.Application.Dtos.TransferenciasDto;
using User.Application.Interfaces;

namespace User.API.Controllers;

/// <summary>
/// Gerenciamento de transferências entre usuários
/// </summary>
[ApiController]
[Route("api/transferencias")]
public class TransferenciasController : ControllerBase
{
    private readonly ITransferenciaService _transferenciaService;

    public TransferenciasController(ITransferenciaService transferenciaService)
    {
        _transferenciaService = transferenciaService;
    }

    /// <summary>
    /// Realiza uma transferência entre usuários
    /// </summary>
    /// <remarks>
    /// <b>Observações:</b>
    /// <br/>
    /// - O valor deve ser maior que zero
    /// - Não é permitido transferir para si mesmo
    /// - A chave de idempotência evita duplicidade
    ///
    /// <br/><br/>
    /// <b>Exemplo de request:</b>
    ///
    ///     POST /api/transferencias
    ///     {
    ///        "usuarioRemetenteId": 1,
    ///        "usuarioDestinatarioId": 2,
    ///        "valor": 150.00,
    ///        "chaveIdempotencia": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    ///     }
    /// </remarks>
    /// <response code="200">Transferência realizada com sucesso</response>
    /// <response code="400">Erro de validação</response>
    [HttpPost]
    [ProducesResponseType(typeof(TransferenciaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Transferir([FromBody] TransferenciaRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _transferenciaService.TransferirAsync(request);

        return Ok(response);
    }
}