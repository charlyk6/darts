namespace darts.db.Entities
{
    public class GameEntity : BaseEntity
    {
        public string? Title { get; set; }
        public UserEntity? Winner { get; set; }
        public DateTime? Date { get; set; }
        public List<UsersGameEntity> UsersGames { get; set; }
        public GameEntity()
        {
            UsersGames = new List<UsersGameEntity>();
        }
    }
}
