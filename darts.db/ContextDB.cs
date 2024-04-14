using darts.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace darts.db
{
    public class ContextDB : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<GameEntity> Games { get; set; } = null!;
        public DbSet<UsersGameEntity> UsersGames { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.UseSqlite("Data Source=D:\\Projects\\darts\\DataBase\\Darts.db");
#else
            optionsBuilder.UseSqlite("Data Source=Darts.db");
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
