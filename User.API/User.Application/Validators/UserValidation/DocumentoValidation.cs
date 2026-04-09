namespace User.Application.Validators.UserValidation;

public static class DocumentoValidation
{
    public static bool DocumentoValidoCpf(string cpf)
    {
        cpf = SomenteNumeros(cpf);
        return cpf.Length == 11;
    }

    public static bool DocumentoValidoCnpj(string cnpj)
    {
        cnpj = SomenteNumeros(cnpj);
        return cnpj.Length == 14;
    }

    public static string SomenteNumeros(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            return string.Empty;

        return new string(valor.Where(char.IsDigit).ToArray());
    }
}
