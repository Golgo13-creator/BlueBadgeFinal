using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.Models
{
    public class AssignmentDetail
    {
        [Required]
        public int SportId { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int PlayerId { get; set; }
    }
}
