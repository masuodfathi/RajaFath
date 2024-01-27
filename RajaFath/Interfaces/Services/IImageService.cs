using RajaFath.Models;

namespace RajaFath.Interfaces.Services
{
    public interface IImageService
    {
        Task<Image> GetImagePathAsync(int imageId);
        Task<ICollection<Image>> GetImagesAsync();
        Task<string> SaveImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(int imageId);
    }
}
