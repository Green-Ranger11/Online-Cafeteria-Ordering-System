using System;
using System.IO;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        public async Task<Photo> SaveToDiskAsync(IFormFile file)
        {
            var photo = new Photo();
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("wwwroot/images/meals", fileName);
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                photo.FileName = fileName;
                photo.PictureUrl = "images/meals/" + fileName;

                return photo;
            }

            return null;
        }

        public void DeleteFromDisk(Photo photo)
        {
            if (File.Exists(Path.Combine("wwwroot/images/meals", photo.FileName)))
            {
                File.Delete("wwwroot/images/meals/" + photo.FileName);
            }
        }
    }
}