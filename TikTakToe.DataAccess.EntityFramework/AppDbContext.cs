using Microsoft.EntityFrameworkCore;
using TikTakToe.Domain.Entities;

namespace TikTakToe.DataAccess.EntityFramework
{
    public class AppDbContext: DbContext
    {
        public DbSet<Step> Steps => Set<Step>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Game> Games => Set<Game>();
        public AppDbContext(DbContextOptions options): base (options) { }
    }
}