using AutoMapper;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Application.Admin.UserService;
public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly IMapper mapper = mapper;

    public async Task<List<User>> GetUsersAsync()
    {
        return await userRepository.GetAllAsync();
    }
}
