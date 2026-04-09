using Microsoft.AspNetCore.Mvc;
using User.Application.Dtos.UserDtos;
using User.Application.Interfaces;
using User.Application.Shared;

namespace User.API.Controllers;

/// <summary>
/// Gerenciamento de usuários
/// </summary>
[ApiController]
[Route("api/users")]
public class UsuarioController : ControllerBase
{
    private readonly IUserRepository _userService;

    public UsuarioController(IUserRepository userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <remarks>
    /// Exemplo de request:
    ///
    ///     POST /api/users
    ///     {
    ///        "name": "teste",
    ///        "email": "teste@email.com"
    ///        "idade": 18
    ///        "celular": "51980578892"
    ///        "tipoPessoa": "pessoafisica"
    ///     }
    /// </remarks>
    /// <response code="201">Usuário criado com sucesso</response>
    /// <response code="400">Erro de validação</response>
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var result = await _userService.Create(dto);
        return Ok(result);
    }

    /// <summary>
    /// Retorna a lista de usuários
    /// </summary>
    /// <response code="200">Lista retornada com sucesso</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.Get();
        return Ok(users);
    }

    /// <summary>
    /// Retorna um usuário pelo ID
    /// </summary>
    /// 
    /// <param name="id">ID do usuário</param>
    /// <response code="200">Usuário encontrado</response>
    /// <response code="404">Usuário não encontrado</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.Get(id);

        if (user == null)
            return NotFound("Usuário não encontrado");

        return Ok(user);
    }

    /// <summary>
    /// Atualiza os dados de um usuário
    /// </summary>
    /// <remarks>
    /// Exemplo de request:
    ///
    ///     PUT /api/users
    ///     {
    ///        "id": 1,
    ///        "name": "teste atualizado",
    ///        "email": "teste@email.com",
    ///        "idade": 19,
    ///        "celular": "51980578892",
    ///        "tipoPessoa": "pessoafisica"
    ///     }
    /// </remarks>
    /// <response code="200">Usuário atualizado com sucesso</response>
    /// <response code="404">Usuário não encontrado</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        dto.Id = id;
        var updated = await _userService.Update(dto);

        if (updated == null)
            return NotFound("Usuário não encontrado");

        return Ok(updated);
    }

    /// <summary>
    /// Remove um usuário pelo ID
    /// </summary>
    /// <remarks>
    /// Para remover um usuário, é obrigatório informar o ID na rota.
    ///
    /// Exemplo de request:
    ///
    ///     DELETE /api/users/1
    ///
    /// </remarks>
    /// <param name="id">ID do usuário que será removido</param>
    /// <response code="204">Usuário removido com sucesso</response>
    /// <response code="404">Usuário não encontrado</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _userService.Delete(id);

        if (!deleted)
            return NotFound("Usuário não encontrado");

        return NoContent();
    }

    /// <summary>
    /// Retorna a lista de usuários por paginação
    /// </summary>
    /// <response code="200">Lista retornada com sucesso</response>
    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        var result = await _userService.GetPaged(
            new PaginationParams { PageNumber = pageNumber, PageSize = pageSize }
        );

        return Ok(result);
    }
}