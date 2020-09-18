using System;
using System.Collections.Generic;

namespace DataAccess.Model
{
    public class Round : IEntityBase
    {
        public Round()
        {
            RoundPhases = new HashSet<Phase>();
            Kills = new HashSet<Kill>();
        }
        public long Id {get; set;}
        public Guid GameId {get; set;}
        public Guid RoundId {get; set;}
        public int RoundNumber {get; set;}
        public DateTime TimeStamp {get; set;}
        public Game Game { get; set; }
        public ICollection<Phase> RoundPhases {get; set;}
        public ICollection<Kill> Kills { get; set; }
    }
}