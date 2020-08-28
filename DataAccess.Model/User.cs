using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class User : IEntityBase
    {
        public long Id { get; set; }
        public string TeamId { get; set; }
        public string UserId { get; set; }
        public DateTime AddedAt { get; set; }
        public Team Team { get; set; }
    }
}
