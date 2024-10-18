using AutoMapper;
using Data_Access.Interfaces;
using Domain_Models;
using DTOs.Image;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;


        public ImageService (IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;

        }
        public void Upload(UploadImageDto uploadImageDto)
        {
            using (var memoryStream  = new MemoryStream())
            {
                uploadImageDto.ImageFile.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);

                var newImage = new Image
                {
                    UserId = uploadImageDto.UserId.Value,
                    Data = base64String,
                    //ContentType = uploadImageDto.ImageFile.ContentType,
                    //Name = uploadImageDto.ImageFile.Name

                };
                _imageRepository.Add(newImage);
            }
        }

        public ImageDto GetById (int? id)
        {
            Image image = _imageRepository.GetById(id);

            if(image == null)
            {
                throw new NotFoundException($"The image for the post with id: {id} was not found");
            }

            ImageDto imageDto = _mapper.Map<ImageDto>(image);
            return imageDto;
        }

        public List<ImageDto> GetAll()
        {
            return _mapper.Map<List<ImageDto>>(_imageRepository.GetAll());
        }

        public List<ImageDto> GetUserImages(int id)
        {
            return _mapper.Map<List<ImageDto>>(_imageRepository.GetUserImages(id));
        
        }
    }
}
