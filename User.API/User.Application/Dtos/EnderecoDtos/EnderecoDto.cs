namespace User.Application.Dtos.EnderecoDtos;

public class EnderecoDto
{
    public int Id { get; set; }
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public int UsuarioId { get; set; }
}
