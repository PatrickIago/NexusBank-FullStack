using AutoMapper;
using User.Application.Dtos.EnderecoDtos;
using User.Application.Dtos.UserDtos;
using User.Domain.Entities;
namespace User.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, Usuario>().ReverseMap();
        CreateMap<UpdateUserDto, Usuario>().ReverseMap();

        CreateMap<Usuario, UserDto>()
            .ForMember(dest => dest.Saldo, opt => opt.MapFrom(src => src.Carteira.Saldo));

        CreateMap<EnderecoDto, Endereco>().ReverseMap();
        CreateMap<CreateEnderecoDto, Endereco>().ReverseMap();
        CreateMap<UpdateEnderecoDto, Endereco>().ReverseMap();
    }
}
