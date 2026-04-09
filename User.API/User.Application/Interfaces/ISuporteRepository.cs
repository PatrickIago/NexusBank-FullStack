using User.Application.Dtos.CarteiraDto;
using User.Application.Dtos.TransferenciasDto;
using User.Application.Dtos.UserDtos;

namespace User.Application.Interfaces;

public interface ISuporteRepository
{
    Task<List<UserDto>> ObterUsuariosPorEndereco(string endereço);
    Task<List<TransferenciaResponseDto>> ObterTransacoesPorDocumento(string documento);
    Task<SaldoDto> ObterSaldoPorDocumento(string documento);
    Task<ComprovanteTransferenciaDto> ObterComprovante(Guid transacaoId);
    Task<byte[]> ObterComprovantePdf(Guid transacaoId);
}

