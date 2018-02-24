using System;
using System.Collections.Generic;

namespace GamesApp.Models
{
    public partial class Games
    {
        public Games()
        {
            GameDev = new HashSet<GameDev>();
        }

        public int GameId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        

        public ICollection<GameDev> GameDev { get; set; }
    }
}
