using Goals.API.Abstractions.Helpers;
using Goals.API.Abstractions.Repositories;
using Goals.API.Abstractions.Services;
using Goals.API.DTOs.Request;
using Goals.API.DTOs.Response;
using Goals.API.Helpers;
using Goals.API.Models;

namespace Goals.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtHelper _jwtHelper;

        public UserService(IUserRepository repository, IJwtHelper jwtHelper)
        {
            _repository = repository;
            _jwtHelper = jwtHelper;
        }

        public async Task<RegisterUserOutputDTO> CreateAsync(RegisterUserInputDTO dto)
        {
            var password = PasswordHelper.Encrypt(dto.Password.Trim());
            
            var model = new UserModel
            {
                Email = dto.Email.ToLower().Trim(),
                Name = dto.Name.Trim(),
                LoginAttempt = 0,
                PasswordHash = password.Hash,
                PasswordSalt = password.Salt
            };

            var user = await _repository.CreateAsync(model);
            var token = _jwtHelper.GenerateToken(user.Name, user.Email);

            return new RegisterUserOutputDTO("Created.", token, 201);
        }

        public async Task<LoginOutputDTO> Login(LoginInputDTO dto)
        {
            var user = await _repository.FindOneAsync(dto.Email, dto.Password);
            var token = _jwtHelper.GenerateToken(user.Name, user.Email);

            return new LoginOutputDTO("User found.", token, 200);
        }
    }
}
