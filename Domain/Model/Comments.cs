using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset   SubmitTime { get; set; }
        public bool IsConfirmed { get; set; }

        //relations
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
