namespace User.Domain.Entities;

public class Carteira
{
    public int Id { get; set; }
    public decimal Saldo { get; private set; }

    public byte[] RowVersion { get; set; }

    // Relacionamento usuario
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public Carteira()
    {
        Saldo = 0;
    }

    public void Creditar(decimal valor)
    {
        if (valor <= 0) throw new ArgumentException("Valor deve ser maior que zero.");
        Saldo += valor;
    }

    public void Debitar(decimal valor)
    {
        if (valor <= 0) throw new ArgumentException("Valor deve ser maior que zero.");
        if (Saldo < valor) throw new InvalidOperationException("Saldo insuficiente.");

        Saldo -= valor;
    }

}
