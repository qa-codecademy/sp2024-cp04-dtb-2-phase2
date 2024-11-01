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
        public StarsController(IStarService starService)
        {
            _starService = starService;
        }
        [HttpPost("AddRating")]
        public IActionResult AddStar(CreateStarDto dto)
        {
            try
            {
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
