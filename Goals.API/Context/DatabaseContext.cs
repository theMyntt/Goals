using Goals.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Goals.API.Context
{
    public class DatabaseContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserModel> Users { get; protected set; }
    }
}
