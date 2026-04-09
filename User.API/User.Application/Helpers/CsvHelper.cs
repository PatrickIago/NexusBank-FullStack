using System.Text;
using User.Application.Dtos.UserDtos;
namespace User.Application.Helpers;

public static class CsvHelper
{
    public static byte[] GerarUsuariosCsv(List<UserDto> usuarios)
    {
        var sb = new StringBuilder();

        // Cabeçalho
        sb.AppendLine("Id;Nome;Email;Idade;Celular;Cidade;Estado");

        foreach (var u in usuarios)
        {
            sb.AppendLine(
                $"{u.Id};{u.Nome};{u.Email};{u.Idade};{u.Celular};" +
                $"{u.Endereco?.Cidade};{u.Endereco?.Estado}"
            );
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }
}
