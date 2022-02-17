using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class User : IdentityUser
    {
        public User()
        {
            IsActive = true;
            
        }
        
        [Required]
        public string FullName { get; set; }
        public int age { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? lastLoginDate { get; set; }

        //relations

        public ICollection<Post> Posts { get; set; }

    }
}
