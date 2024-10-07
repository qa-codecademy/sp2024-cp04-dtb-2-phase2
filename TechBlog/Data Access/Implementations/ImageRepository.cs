﻿using Data_Access.Interfaces;
using Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Implementations
{
    public class ImageRepository : Repository <Image> , IImageRepository
    {
        private TechBlogDbContext _imageContext;
        public ImageRepository(TechBlogDbContext imageContext) : base(imageContext)
        {
            _imageContext = imageContext;
        }

        public Image GetByPostId(int postId)
        {
            return _imageContext.Images.FirstOrDefault(x => x.PostId == postId);
        }
    }
}