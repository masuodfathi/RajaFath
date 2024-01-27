using RajaFath.Models;

namespace RajaFath.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<Image> GetImagePathAsync(int imageId);
        Task<ICollection<Image>> GetImagesPathAsync();
        Task<bool> SaveImagePathAsync(Image image);
        Task<bool> DeleteImagePathAsync(int imageId);
        Task<bool> SaveAsync();
    }
}
