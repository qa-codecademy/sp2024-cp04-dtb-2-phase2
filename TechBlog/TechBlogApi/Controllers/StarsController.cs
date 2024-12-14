using DTOs.StarsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace TechBlogApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StarsController : ControllerBase
    {
        private readonly IStarService _starService;
        private readonly IPostService _postService;
        public StarsController(IStarService starService, IPostService postService)
        {
            _starService = starService;
            _postService = postService;
        }
        [HttpPost("AddRating")]
        public IActionResult AddStar(CreateStarDto dto)
        {
            try
            {
                var post = _postService.GetById(dto.PostId);
                if (post != null && post.User.Id.Equals(dto.UserId))
                    return BadRequest("You're not allowed to rate your own post!");
                _starService.AddRating(dto);
                return Ok("Successfully added star!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("RemoveRating")]
        public IActionResult DeleteStar(RemoveStarDto dto)
        {
            try
            {
                _starService.RemoveRating(dto);
                return Ok("Successfully removed star!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public IActionResult UpdateStar(CreateStarDto dto)
        {
            try
            {
                _starService.UpdateRating(dto);
                return Ok("Successfully updated star!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [AllowAnonymous]

        [HttpPost]
        public IActionResult GetPostStar (RemoveStarDto dto)
        {
            try
            {
                var result = _starService.GetStarByUserAndPostId(dto);
                return Ok(result);
            }catch (Exception ex)
            {

                return BadRequest($"something went wrong\n{ex.Message}");
            }
        }
    }
}
