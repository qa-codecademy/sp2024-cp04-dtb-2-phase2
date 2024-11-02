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
        public IActionResult Upload([FromForm] UploadImageDto uploadImageDto)
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
                _imageService.Upload(uploadImageDto);
                return Ok();

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
        [Authorize]
        [HttpGet("userimages")]
        public IActionResult GetUserImages()
        {
            var loggedInUserId = _tokenHelper.GetUserId();
            var foundUser = _userService.GetUserById(loggedInUserId);
            if (foundUser == null)
            {
                return NotFound();
            }
            return Ok(_imageService.GetUserImages(loggedInUserId));
            
        }
        [HttpGet("defaultimages")]
        public IActionResult GetDefaultImages()
        {
            return Ok(_imageService.GetDefaultImages());
        }
    }
}
