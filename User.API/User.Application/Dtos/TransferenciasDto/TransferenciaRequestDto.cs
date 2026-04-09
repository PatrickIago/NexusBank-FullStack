namespace User.Application.Dtos.TransferenciasDto;

public class TransferenciaRequestDto
{
    public int UsuarioRemetenteId { get; set; }
    public int UsuarioDestinatarioId { get; set; }
    public decimal Valor { get; set; }
    public Guid? ChaveIdempotencia { get; set; }
}
