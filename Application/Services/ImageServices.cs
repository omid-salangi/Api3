using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Common.Dependency;

namespace Application.Services
{
    public class ImageServices : IImageServices , IScopedDependency
    {
        public async Task<string> SaveImage(byte[] image, string wwwrootpath,string fullname , CancellationToken cancellationToken)
        {
            string fileName = Path.GetFileNameWithoutExtension(fullname);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(fullname);
            string path = Path.Combine(wwwrootpath, "img", fileName);
            using (var fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await fileStream.WriteAsync(image, cancellationToken);
            }

            string ImageUrl = Path.Combine("/" + "img", fullname).Replace('\\', '/');
            return ImageUrl;
        }

        public async Task Delete(string imgname, string wwwrootpath)
        {
            string path = Path.Combine(wwwrootpath, "img", imgname);
            if (File.Exists(path) && imgname != "nopicture.jpg")
            {
                File.Delete(path);
            }
        }
    }
}
