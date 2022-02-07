using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class CategoryToPost
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public Post Post { get; set; }
    }
}
