using AutoMapper;
using POS.Core.Dtos.UserDTOs;
using POS.Core.Dtos;
using POS.Core.Repository;
using POS.Core.Service;
using POS.Service;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> Authenticate(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
            return null; // Authentication failed

        return _mapper.Map<UserDto>(user);
    }

    public async Task Create(UserCreateDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.PasswordHash = PasswordHasher.HashPassword(userDto.Password); // Secure hashing
        await _userRepository.AddUserAsync(user);
    }

    public async Task Delete(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetById(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<UserDto?> GetByUsername(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task Update(UserUpdateDto userDto)
    {
        var user = await _userRepository.GetUserByIdAsync(userDto.UserId);
        if (user != null)
        {
            _mapper.Map(userDto, user);
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
