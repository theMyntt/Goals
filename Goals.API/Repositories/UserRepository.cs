using Goals.API.Abstractions.Repositories;
using Goals.API.Context;
using Goals.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Goals.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<UserModel> _table;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
            _table = context.Set<UserModel>();
        }

        public async Task<UserModel> CreateAsync(UserModel model)
        {
            var isCreated = (await _table.FirstOrDefaultAsync(u => u.Email == model.Email)) != null;

            if (isCreated)
                // TODO: Throw http exception
                return new UserModel();

            model.Id = Guid.NewGuid();

            await _table.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
