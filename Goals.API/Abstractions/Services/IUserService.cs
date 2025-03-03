using Goals.API.DTOs.Request;
using Goals.API.DTOs.Response;

namespace Goals.API.Abstractions.Services
{
    public interface IUserService
    {
        Task<LoginOutputDTO> Login(LoginInputDTO dto);
    }
}
