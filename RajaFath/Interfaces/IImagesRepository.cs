using RajaFath.Models;

namespace RajaFath.Interfaces
{
    public interface IImagesRepository
    {
        Task<Image> GetImageAsync(int imageId);
        Task<ICollection<Image>> GetImagesAsync();
        Task<bool> SaveImageAsync(Image image);
        Task<bool> DeleteImagePathAsync(int imageId);
        Task<bool> SaveAsync();
    }
}
