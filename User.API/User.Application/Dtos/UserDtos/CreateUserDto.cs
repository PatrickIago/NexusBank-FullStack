using User.Application.Dtos.EnderecoDtos;
using User.Domain.Enuns;

namespace User.Application.Dtos.UserDtos;

public class CreateUserDto
{
    /// <summary>
    /// Nome completo do usuário
    /// </summary>
    public string Nome { get; set; }
    public int? Idade { get; set; }
    public string Email { get; set; }
    public string Celular { get; set; }


    /// <summary>
    /// Documento conforme o TipoPessoa:
    /// CPF → 11 dígitos
    /// CNPJ → 14 dígitos
    /// </summary>
    public string Documento { get; set; }

    /// <summary>
    /// Tipo da pessoa:
    /// PessoaFisica = CPF
    /// PessoaJuridica = CNPJ
    /// </summary>
    public TipoPessoa TipoPessoa { get; set; }

    /// <summary>
    /// Endereço do usuário
    /// </summary>
    public CreateEnderecoDto Endereco { get; set; }

}
