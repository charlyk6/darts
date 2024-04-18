namespace darts.db.Entities
{
    public class ThrowEntity : BaseEntity
    {
        public int UsersGameId { get; set; }
        public UsersGameEntity? UsersGame { get; set; }        
        /// <summary>
        /// Очки попадания, в зависимости от сектора, 0 - мимо мишени
        /// </summary>
        public int Sector { get; set; } = 0;
        /// <summary>
        /// умножение, 1, 2, 3
        /// </summary>
        public int Mult { get; set; } = 1;
        private int total = 0;
        /// <summary>
        /// Рассчитанное количество очков за бросок
        /// </summary>
        public int Total
        {
            get { return total; }
            set { total = Sector * Mult; }
        }
    }
}
