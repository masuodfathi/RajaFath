using RajaFath.Interfaces.Repositories;
using RajaFath.Interfaces.Services;
using RajaFath.Models;
using System.IO;

namespace RajaFath.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await _imageRepository.GetImagePathAsync(imageId);

            //if file does not exist
            if (image == null)
            {
                throw new KeyNotFoundException($"Image {imageId} not exist");
            }

            //if file is found
            if(File.Exists(image.ImageUrl))
            {

                //delete the file from server
                File.Delete(image.ImageUrl);

                //delete the image path from database
                return await _imageRepository.DeleteImagePathAsync(imageId);
            }
            throw new KeyNotFoundException($"Image {image.ImageUrl} not found");

        }

        public Task<Image> GetImagePathAsync(int imageId)
        {
            return _imageRepository.GetImagePathAsync(imageId);
        }

        public async Task<ICollection<Image>> GetImagesAsync()
        {
            return await _imageRepository.GetImagesPathAsync();
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    // Save the file to the server
                    string filePath = await SaveFileToServerAsync(file);

                    // Save the file path to the database using the repository
                    await _imageRepository.SaveImagePathAsync(new Image { ImageUrl = filePath });

                    return filePath;
                }

                throw new Exception(); // Or throw an exception or return an appropriate result based on your requirements
            }
            catch (Exception)
            {
                // Handle exceptions appropriately
                throw new Exception();
            }
        }
        private async Task<string> SaveFileToServerAsync(IFormFile file)
        {
            // Define the folder where you want to save the images
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            // Ensure the folder exists, if not, create it
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate a unique filename for the uploaded image
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            // Combine the folder and filename to get the full path
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save the file to the server
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}
