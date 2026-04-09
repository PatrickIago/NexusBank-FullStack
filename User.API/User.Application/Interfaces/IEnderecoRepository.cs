using User.Application.Dtos.EnderecoDtos;
namespace User.Application.Interfaces;

public interface IEnderecoRepository
{
    Task<EnderecoDto> Get(int id);
    Task<CreateEnderecoDto> Create(CreateEnderecoDto enderecoCreate);
    Task<UpdateEnderecoDto> Update(UpdateEnderecoDto userUpdate);
    Task<bool> Delete(int id);
}
