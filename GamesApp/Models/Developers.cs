using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamesApp.Models
{
    public partial class Developers
    {
        public Developers()
        {
            GameDev = new HashSet<GameDev>();
        }
        
        public int DeveloperId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        public ICollection<GameDev> GameDev { get; set; }
    }
}
