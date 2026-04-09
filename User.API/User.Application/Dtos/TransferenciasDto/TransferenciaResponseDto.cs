namespace User.Application.Dtos.TransferenciasDto;

public class TransferenciaResponseDto
{
    public Guid TransacaoId { get; set; }
    public decimal Valor { get; set; }
    public string Destinatario { get; set; }
    public string Status { get; set; }
    public DateTime DataHora { get; set; }
}
