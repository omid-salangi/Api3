namespace Application.Interface
{
    public interface IImageServices
    {
        Task<string> SaveImage(byte[] image, string wwwrootpath, string fullname, CancellationToken cancellationToken);
        Task Delete(string imgname, string wwwrootpath);
    }
}