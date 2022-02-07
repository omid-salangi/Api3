using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Roles
    {
        [Required]  
        [StringLength(50)]
        public string name { get; set; }    

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
