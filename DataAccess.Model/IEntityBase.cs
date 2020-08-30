using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Model
{
    public interface IEntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        long Id { get; set; }
    }
}
