using darts.db.Enums;

namespace darts.db.Entities
{
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// фамилия
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// имя
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// ник
        /// </summary>
        public string? NickName { get; set;}
        /// <summary>
        /// Кол-во очков, которые надо набрать игроку
        /// </summary>
        public int? Score { get; set; }
        /// <summary>
        /// Уровень сложности
        /// </summary>
        public Level UserLevel { get; set; }
        /// <summary>
        /// Играет ли пользователь
        /// </summary>
        public bool IsPlaying { get; set; } = false;
        public List<UsersGameEntity> UsersGames { get; set; }
        public UserEntity()
        {
            UsersGames = new List<UsersGameEntity>();
        }
    }
}
