using darts.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace darts.db
{
    public class ContextDB : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<GameEntity> Games { get; set; } = null!;
        public DbSet<UsersGameEntity> UsersGames { get; set; } = null!;
        public DbSet<ThrowEntity> Throws { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=d:\\Projects\\darts\\DataBase\\Darts.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
