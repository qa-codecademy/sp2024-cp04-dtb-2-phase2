using DTOs.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IImageService
    {
        ImageDto Upload(UploadImageDto uploadImageDto);
        ImageDto GetRandomImage(int userId);
        bool Delete(int id);
        ImageDto GetById(int? id);
        List<ImageDto> GetAll();
        List<ImageDto> GetUserImages (int id);
        List<ImageDto> GetDefaultImages();

        
    }
}
