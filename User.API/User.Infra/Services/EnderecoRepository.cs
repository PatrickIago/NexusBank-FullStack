using AutoMapper;
using Microsoft.EntityFrameworkCore;
using User.Application.Dtos.EnderecoDtos;
using User.Application.Interfaces;
using User.Domain.Entities;
using User.Infra.Data;

namespace User.Infra.Services;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly UserDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public EnderecoRepository(UserDbContext context, IMapper mapper, IUserRepository userRepository)
    {
        _context = context;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<CreateEnderecoDto> Create(CreateEnderecoDto enderecoCreate)
    {
        var usuarioExiste = await _userRepository
            .Get(enderecoCreate.UsuarioId);

        if (usuarioExiste == null)
            throw new Exception("Usuário não encontrado");

        var endereco = _mapper.Map<Endereco>(enderecoCreate);

        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();

        return _mapper.Map<CreateEnderecoDto>(endereco);
    }

    public async Task<bool> Delete(int id)
    {
        var endereco = await _context.Enderecos
            .FirstOrDefaultAsync(e => e.Id == id);

        if (endereco == null)
            return false;

        _context.Enderecos.Remove(endereco);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<EnderecoDto> Get(int id)
    {
        var endereco = await _context.Enderecos
            .Include(u => u.Usuario)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (endereco == null)
            return null;

        return _mapper.Map<EnderecoDto>(endereco);
    }

    public async Task<UpdateEnderecoDto> Update(UpdateEnderecoDto enderecoUpdate)
    {
        var endereco = await _context.Enderecos
            .Include(u => u.Usuario)
            .FirstOrDefaultAsync(e => e.Id == enderecoUpdate.Id);

        if (endereco == null)
            return null;

        endereco.UsuarioId = enderecoUpdate.UsuarioId;
        endereco.Rua = enderecoUpdate.Rua;
        endereco.Numero = enderecoUpdate.Numero;
        endereco.Cidade = enderecoUpdate.Cidade;
        endereco.Estado = enderecoUpdate.Estado;

        await _context.SaveChangesAsync();

        return enderecoUpdate;
    }
}