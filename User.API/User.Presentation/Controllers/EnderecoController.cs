using Microsoft.AspNetCore.Mvc;
using User.Application.Dtos.EnderecoDtos;
using User.Application.Interfaces;

namespace User.API.Controllers;

/// <summary>
/// Gerenciamento de endereços
/// </summary>
[ApiController]
[Route("api/enderecos")]
public class EnderecoController : ControllerBase
{
    private readonly IEnderecoRepository _enderecoService;

    public EnderecoController(IEnderecoRepository enderecoService)
    {
        _enderecoService = enderecoService;
    }

    /// <summary>
    /// Cria um novo endereço
    /// </summary>
    /// <remarks>
    /// <b>Observações:</b>
    /// <br/>
    /// - O endereço está sempre vinculado a um usuário
    /// - O sistema é exclusivo para o Brasil
    ///
    /// <br/><br/>
    /// <b>Exemplo de request:</b>
    ///
    ///     POST /api/enderecos
    ///     {
    ///        "userId": 0,
    ///        "rua": "Av Brasil",
    ///        "numero": "100",
    ///        "cidade": "Porto Alegre",
    ///        "estado": "RS"
    ///     }
    /// </remarks>
    /// <response code="201">Endereço criado com sucesso</response>
    /// <response code="400">Erro de validação</response>
    [HttpPost]
    [ProducesResponseType(typeof(EnderecoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEnderecoDto dto)
    {
        var result = await _enderecoService.Create(dto);
        return Ok(result);
    }


    /// <summary>
    /// Retorna um endereço pelo ID
    /// </summary>
    /// <param name="id">ID do endereço</param>
    /// <response code="200">Endereço encontrado</response>
    /// <response code="404">Endereço não encontrado</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EnderecoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var endereco = await _enderecoService.Get(id);

        if (endereco == null)
            return NotFound("Endereço não encontrado");

        return Ok(endereco);
    }

    /// <summary>
    /// Atualiza um endereço existente
    /// </summary>
    /// <remarks>
    /// <b>Exemplo de request:</b>
    ///
    ///     PUT /api/enderecos
    ///     {
    ///        "id": 1,
    ///        "rua": "Av Brasil",
    ///        "numero": "200",
    ///        "cidade": "Porto Alegre",
    ///        "estado": "RS"
    ///     }
    /// </remarks>
    /// <response code="200">Endereço atualizado com sucesso</response>
    /// <response code="404">Endereço não encontrado</response>
    [HttpPut]
    [ProducesResponseType(typeof(EnderecoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateEnderecoDto dto)
    {
        var updated = await _enderecoService.Update(dto);

        if (updated == null)
            return NotFound("Endereço não encontrado");

        return Ok(updated);
    }

    /// <summary>
    /// Remove um endereço pelo ID
    /// </summary>
    /// <remarks>
    /// Exemplo:
    ///
    ///     DELETE /api/enderecos/1
    /// </remarks>
    /// <param name="id">ID do endereço</param>
    /// <response code="204">Endereço removido com sucesso</response>
    /// <response code="404">Endereço não encontrado</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _enderecoService.Delete(id);

        if (!deleted)
            return NotFound("Endereço não encontrado");

        return NoContent();
    }
}