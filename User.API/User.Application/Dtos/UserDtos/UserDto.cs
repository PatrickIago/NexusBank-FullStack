using User.Application.Dtos.EnderecoDtos;
using User.Domain.Enuns;

namespace User.Application.Dtos.UserDtos;

public class UserDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Email { get; set; }
    public string Celular { get; set; }
    public string Documento { get; set; }
    public TipoPessoa TipoPessoa { get; set; }
    public EnderecoDto Endereco { get; set; }
    public decimal Saldo { get; set; }
}
