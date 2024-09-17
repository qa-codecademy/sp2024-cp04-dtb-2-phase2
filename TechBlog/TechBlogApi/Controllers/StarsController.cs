using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace TechBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarsController : ControllerBase
    {
        private readonly IStarService _starService;
        public StarsController(IStarService starService)
        {
            _starService = starService;
        }
        [HttpPost("AddRating")]
        public IActionResult AddStar(int userId, int postId, int rating)
        {
            try
            {
                _starService.AddRating(userId, postId, rating);
                return Ok("Successfully added star!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("RemoveRating")]
        public IActionResult DeleteStar(int userId, int postId)
        {
            try
            {
                _starService.RemoveRating(userId, postId);
                return Ok("Successfully removed star!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public IActionResult UpdateStar(int userId, int postId, int rating)
        {
            try
            {
                _starService.UpdateRating(userId, postId, rating);
                return Ok("Successfully updated star!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
