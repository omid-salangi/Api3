using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IPostRepository
    {
        public Task<bool> AddPost(string title, string description,DateTime time,string author,CancellationToken cancellationToken);
        public Task<bool> DeletePost(int postid);
        public Task<bool> UpdatePost(int postid, string title, string description, int[] category, DateTime time);
    }
}