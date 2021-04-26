using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.Data
{
    public class Assignment
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Sport))]
        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        [Key, Column(Order = 2)]
        [ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
