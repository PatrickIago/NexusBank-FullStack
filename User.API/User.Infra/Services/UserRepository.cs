using AutoMapper;
using Microsoft.EntityFrameworkCore;
using User.Application.Dtos.UserDtos;
using User.Application.Interfaces;
using User.Application.Shared;
using User.Domain.Entities;
using User.Infra.Data;
namespace User.Infra.Services;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;
    public UserRepository(UserDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CreateUserDto> Create(CreateUserDto userCreate)
    {
        var usuario = _mapper.Map<Usuario>(userCreate);

        usuario.Carteira = new Carteira();

        _context.Add(usuario);
        await _context.SaveChangesAsync();

        return _mapper.Map<CreateUserDto>(usuario);
    }

    public async Task<bool> Delete(int id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null) return false;

        _context.Remove(usuario);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<UserDto>> Get()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Endereco)
            .Include(c => c.Carteira)
            .ToListAsync();

        return _mapper.Map<List<UserDto>>(usuarios);
    }

    public async Task<PagedResult<UserDto>> GetPaged(PaginationParams pagination)
    {
        var query = _context.Usuarios
            .Include(u => u.Endereco)
            .AsNoTracking();

        var totalItems = await query.CountAsync();

        var usuarios = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new PagedResult<UserDto>
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalItems = totalItems,
            Data = _mapper.Map<List<UserDto>>(usuarios)
        };
    }

    public async Task<UserDto> Get(int id)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Endereco)
            .Include(c => c.Carteira)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null) return null;

        return _mapper.Map<UserDto>(usuario);

    }

    public async Task<UpdateUserDto> Update(UpdateUserDto userUpdate)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Id == userUpdate.Id);

        if (usuario == null) return null;

        usuario.Nome = userUpdate.Nome;
        usuario.Email = userUpdate.Email;
        usuario.Celular = userUpdate.Celular;
        usuario.Idade = userUpdate.Idade;

        usuario.AtualizarDocumento(userUpdate.TipoPessoa, userUpdate.Documento);

        _context.Usuarios.Update(usuario);

        await _context.SaveChangesAsync();

        return userUpdate;
    }
}
