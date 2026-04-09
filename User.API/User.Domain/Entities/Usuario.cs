using User.Domain.Enuns;
namespace User.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Email { get; set; }
    public string Celular { get; set; }
    public TipoPessoa TipoPessoa { get; private set; }
    public string Documento { get; private set; }

    // Relacionamentos
    public Carteira Carteira { get; set; }
    public Endereco Endereco { get; set; }


    // Construtores
    public Usuario() { }

    public Usuario(int id, string nome, int idade, string email, string celular,
                   TipoPessoa tipoPessoa, string documento)
    {
        Id = id;
        Nome = nome;
        Idade = idade;
        Email = email;
        Celular = celular;
        TipoPessoa = tipoPessoa;
        Documento = documento;
    }
    public void AtualizarDocumento(TipoPessoa tipoPessoa, string documento)
    {
        TipoPessoa = tipoPessoa;
        Documento = documento;
    }
}
