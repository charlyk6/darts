using darts.db.Enums;

namespace darts.Models
{
    public class PlayerScoreModel
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Ник
        /// </summary>
        public string? NickName { get; set; }
        /// <summary>
        /// Цель игры
        /// </summary>
        public int? Score { get; set; }
        /// <summary>
        /// Уровень сложности
        /// </summary>
        public Level Level { get; set; }
        /// <summary>
        /// Остаток очков
        /// </summary>
        public int? Points { get; set; }
        /// <summary>
        /// Номер броска
        /// </summary>
        public int NumberThrow { get; set; }        
    }
}
