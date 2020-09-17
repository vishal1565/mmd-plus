using System.Collections.Generic;
using System;
namespace DataAccess.Model
{
    public class Game : IEntityBase
    {
        public Game()
        {
            Rounds = new HashSet<Round>();
            Phases = new HashSet<Phase>();
        }
        public long Id {get; set;}
        public Guid GameId {get; set;}
        public DateTime TimeStamp {get; set;}
        public ICollection<Round> Rounds {get; set;}
        public ICollection<Phase> Phases {get; set;}
    }
}