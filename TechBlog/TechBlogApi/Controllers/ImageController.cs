using DTOs.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace TechBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }



        [HttpPost]
        public IActionResult Upload([FromForm] UploadImageDto uploadImageDto)
        {
            try
            {
                if (uploadImageDto == null)
                {
                    return BadRequest("No image given");
                }
                _imageService.Upload(uploadImageDto);
                return Ok();

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpGet]
        public IActionResult GetByPostId(int postId)
        {
            try
            {
                if(postId == 0)
                {
                    return BadRequest($"You need to enter the id value");
                }
                return Ok(_imageService.GetByPostId(postId));
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


    }
}
