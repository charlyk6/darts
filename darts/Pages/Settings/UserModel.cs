using darts.db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace darts.Pages.Settings
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? NickName { get; set; }
        public List<UsersGameEntity> UsersGames { get; set; }

        public bool IsPlaying { get; set; } = true;
        public int CountWins { get; set; }
        public UserEntity UserE { get; set; }
        public UserModel(UserEntity u) {
            Id = u.Id;
            LastName = u.LastName;
            FirstName = u.FirstName;
            NickName = u.NickName;
            UsersGames = u.UsersGames;
            UserE = u;
        }
        public UserEntity getEntity()
        {
            return UserE;
        }
    }
}
