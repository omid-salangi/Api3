using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class Roles : IdentityRole
    {
        
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
