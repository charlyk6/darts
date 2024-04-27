using darts.db.Enums;

namespace darts.db.Entities
{
    public class UsersGameEntity : BaseEntity
    {
        /// <summary>
        /// Уровень сложности
        /// </summary>
        public Level Level { get; set; }

        /// <summary>
        /// Кол-во очков, которые НАДО набрать игроку (цель игры)
        /// </summary>
        public int? Total { get; set; }

        /// <summary>
        /// Кол-во очков, которые ОСТАЛОСЬ набрать
        /// </summary>
        public int? Scores { get; set; }
        
        /// <summary>
        /// Номер броска
        /// </summary>
        public int NumberThrow { get; set; }
        
        public int? GameId { get; set; }
        public virtual GameEntity? Game { get; set; }
        public int? UserId { get; set; }
        public virtual UserEntity? User { get; set; }
        
    }
}
