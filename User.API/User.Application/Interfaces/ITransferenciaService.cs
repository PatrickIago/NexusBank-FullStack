using User.Application.Dtos.TransferenciasDto;
namespace User.Application.Interfaces;

public interface ITransferenciaService
{
    Task<TransferenciaResponseDto> TransferirAsync(TransferenciaRequestDto request);
}