namespace User.Application.Dtos.EnderecoDtos;

public class CreateEnderecoDto
{
    public int UsuarioId { get; set; }
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
}
