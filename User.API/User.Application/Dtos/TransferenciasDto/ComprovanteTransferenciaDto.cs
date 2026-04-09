namespace User.Application.Dtos.TransferenciasDto;

public class ComprovanteTransferenciaDto
{
    public string Mensagem { get; set; }
    public Guid TransacaoId { get; set; }
    public DateTime DataHora { get; set; }
    public string Remetente { get; set; }
    public string DocumentoRemetente { get; set; }
    public string Destinatario { get; set; }
    public decimal Valor { get; set; }
}
