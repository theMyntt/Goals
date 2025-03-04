using System;
using Goals.API.Abstractions.Repositories;
using Goals.API.Context;
using Goals.API.DTOs.Request;
using Goals.API.Exceptions;
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
                throw new NotFoundException("Email/Password are incorrect.");

            model.Id = Guid.NewGuid();

            await _table.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<UserModel> FindOneAsync(string email, string password)
        {
            var user = await _table.FirstOrDefaultAsync(u => u.Email == email.Trim());

            if (user == null)
                throw new NotFoundException("User not exists.");

            var isEqualPassword = PasswordHelper.Compare(password.Trim(), new PasswordHelperInputDTO(
                Salt: user.PasswordSalt,
                Hash: user.PasswordHash));

            if (user.LoginAttempt > 5)
                throw new ForbiddenException("This user are blocked.");

            if (!isEqualPassword)
            {
                user.LoginAttempt += 1;

                await _context.SaveChangesAsync();

                throw new NotFoundException("Password are incorrect.");
            }

            user.PasswordHash = string.Empty;
            user.PasswordSalt = string.Empty;


            return user;
        }
    }
}
