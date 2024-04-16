namespace darts.db.Entities
{
    public class ThrowEntity : BaseEntity
    {
        public int UsersGameId { get; set; }
        public UsersGameEntity? UsersGame { get; set; }        
        public int Sector { get; set; } = 0;
        public int Mult { get; set; } = 1;

        private int total = 0;
        public int Total
        {
            get { return total; }
            set { total = Sector * Mult; }
        }
    }
}
