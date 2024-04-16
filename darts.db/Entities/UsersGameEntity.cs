using darts.db.Enums;

namespace darts.db.Entities
{
    public class UsersGameEntity : BaseEntity
    {
        /// <summary>
        /// Кол-во очков, которые надо набрать игроку
        /// </summary>
        public int? Total { get; set; }
        /// <summary>
        /// Уровень сложности
        /// </summary>
        public Level Level { get; set; }
        /// <summary>
        /// Броски
        /// </summary>
        public List<ThrowEntity> Throws { get; set; }
        public int? GameId { get; set; }
        public virtual GameEntity? Game { get; set; }
        public int? UserId { get; set; }
        public virtual UserEntity? User { get; set; }

        public UsersGameEntity()
        {
            Throws = new List<ThrowEntity>();
        }
    }
}
