using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Post 
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public string img { get; set; }
        // relations
        
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<CategoryToPost> CategoryToPosts { get; set; }
        public ICollection<Comments> Comments { get; set; }
        
    }
}