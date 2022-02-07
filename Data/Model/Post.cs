using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Post 
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public string author { get; set; }
        public DateTime Time { get; set; }  

        // relations
        
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<CategoryToPost> CategoryToPosts { get; set; }
    }
}