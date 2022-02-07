using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class User 
    {
        public User()
        {
            IsActive = true;
        }
        [Key]
        public int  Id { get; set; }
        [Required]
        public string USerName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public int age { get; set; }
        public bool IsActive { get; set; }

        //relations

        public ICollection<Post> Posts { get; set; }

    }
}
