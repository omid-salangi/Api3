using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            IsActive = true;
            
        }
        
        public string? FullName { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public string? ProfileImg { get; set; }
        public string? SelfDescription { get; set; }

        //relations

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comments> Comments { get; set; }

    }
}
