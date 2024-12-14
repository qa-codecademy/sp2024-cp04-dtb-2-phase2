using DTOs.Image;
using Helpers;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        public ImageController(IImageService imageService, ITokenHelper tokenHelper, IUserService userService)
        {
            _imageService = imageService;
            _tokenHelper = tokenHelper;
            _userService = userService;
        }


        [Authorize]
        [HttpPost]
        public IActionResult Upload([FromBody] UploadImageDto uploadImageDto)
        {
            try
            {
                if (uploadImageDto == null)
                {
                    return BadRequest("No image given");
                }
                var loggedInUserId = _tokenHelper.GetUserId();
                var foundUser = _userService.GetUserById(loggedInUserId);
                if (foundUser == null)
                {
                    return NotFound();
                }
                uploadImageDto.UserId = loggedInUserId;
                var result = _imageService.Upload(uploadImageDto);
                return CreatedAtAction("Upload", result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        //[HttpGet]
        //public IActionResult GetById(int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            return BadRequest($"You need to enter the id value");
        //        }
        //        return Ok(_imageService.GetById(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex);
        //    }
        //}

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_imageService.GetAll());
        }
        [HttpGet("userimages/{id:int}")]
        public IActionResult GetUserImages(int id)
        {
            var foundUser = _userService.GetUserById(id);
            if (foundUser == null)
            {
                return NotFound("User not found");
            }
            return Ok(_imageService.GetUserImages(id));
            
        }
        [HttpGet("defaultimages")]
        public IActionResult GetDefaultImages()
        {
            return Ok(_imageService.GetDefaultImages());
        }
        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id) 
        {
            var found = _imageService.GetById(id);

            if (found.Data == null)
                return NotFound("Image was not found!");

            var userId = _tokenHelper.GetUserId();

            if (found.UserId != userId)
                return Unauthorized("You're not allowed to delete this image!");

            if (_imageService.Delete(id))
                return Ok();

            return BadRequest("The image was't deleted successfully!");
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_imageService.GetById(id));
        }

        [HttpGet("randomimage/{id:int}")]
        public IActionResult GetRandomImage(int id)
        {
            var result = _imageService.GetRandomImage(id);
            if (result != null)
                return Ok(result);
            return BadRequest("Something went wrong with fetching the image");
        }
    }
}
