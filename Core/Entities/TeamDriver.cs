using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TeamDriver
    {
        public int IdDriver { get; set; }
        public Driver Driver { get; set; }
        public int IdTeam { get; set; }
        public Team Team { get; set; }
    }
}