using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Interfaces.Repositories;
using RajaFath.Models;

namespace RajaFath.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteImagePathAsync(int imageId)
        {
            var delitingImage = await _context.Images.FindAsync(imageId);
            if (delitingImage != null)
            {
                _context.Images.Remove(delitingImage);
                return await SaveAsync();
            }
            throw new KeyNotFoundException($"The image with id {imageId} not found");
            
        }

        public async Task<Image> GetImagePathAsync(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image != null)
                return image;

            throw new KeyNotFoundException($"The image with the id {imageId} not found");
        }

        public async Task<ICollection<Image>> GetImagesPathAsync()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                var save = await _context.SaveChangesAsync();
                if(save > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }

        public Task<bool> SaveImagePathAsync(Image image)
        {
            if (image != null)
            {
                _context.Images.Add(image);
                return SaveAsync();
            }
                throw new Exception("Server Error");
                
        }
    }
}
