using Goals.API.Models;

namespace Goals.API.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> CreateAsync(UserModel model);
    }
}
