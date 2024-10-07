﻿using DTOs.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IImageService
    {
        void Upload(UploadImageDto uploadImageDto);
        ImageDto GetByPostId(int postId);
        
    }
}