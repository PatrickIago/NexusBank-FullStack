using User.Application.Dtos.UserDtos;
using User.Application.Shared;

namespace User.Application.Interfaces;

public interface IUserRepository
{
    Task<List<UserDto>> Get();
    Task<PagedResult<UserDto>> GetPaged(PaginationParams pagination);
    Task<UserDto> Get(int id);
    Task<CreateUserDto> Create(CreateUserDto userCreate);
    Task<UpdateUserDto> Update(UpdateUserDto userUpdate);
    Task<bool> Delete(int id);
}
