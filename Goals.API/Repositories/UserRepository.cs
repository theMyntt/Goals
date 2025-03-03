using System;
using Goals.API.Abstractions.Repositories;
using Goals.API.Context;
using Goals.API.DTOs.Request;
using Goals.API.Helpers;
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

        public async Task<UserModel> FindOneAsync(string email, string password)
        {
            var user = await _table.FirstOrDefaultAsync(u => u.Email == email.Trim());

            if (user == null)
                // TODO: Throw http exception
                return new UserModel();

            var isEqualPassword = PasswordHelper.Compare(password.Trim(), new PasswordHelperInputDTO(
                Salt: user.PasswordSalt,
                Hash: user.PasswordHash));

            user.PasswordHash = string.Empty;
            user.PasswordSalt = string.Empty;

            if (!isEqualPassword)
                // TODO: Throw http exception
                return new UserModel();

            if (user.LoginAttempt > 5)
                // TODO: Throw http exception
                return new UserModel();

            return user;
        }
    }
}
