using Goals.API.Models;

namespace Goals.API.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> FindOneAsync(string email, string password);
        Task<UserModel> CreateAsync(UserModel model);
    }
}
