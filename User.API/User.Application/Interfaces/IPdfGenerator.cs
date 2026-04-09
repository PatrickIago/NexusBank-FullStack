using User.Application.Dtos.TransferenciasDto;
namespace User.Application.Interfaces;

public interface IPdfGenerator
{
    byte[] GerarPdf(ComprovanteTransferenciaDto comprovante);
}
