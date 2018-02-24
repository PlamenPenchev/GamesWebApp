using System;
using System.Collections.Generic;

namespace GamesApp.Models
{
    public partial class GameDev
    {
        public int GameDevId { get; set; }
        public int GameId { get; set; }
        public int DeveloperId { get; set; }
        public DateTime ReleaseDate { get; set; }

        
        public Developers Developer { get; set; }
        public Games Game { get; set; }
    }
}
