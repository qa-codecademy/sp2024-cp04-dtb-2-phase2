using Domain_Models;
using DTOs.CommentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Interfaces;
using System.Collections.Immutable;
using System.Data;
using System.Linq.Expressions;
using System.Security.Claims;

namespace TechBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public ActionResult<ICollection<CommentDto>> GetAll()
        {
            try
            {

                return Ok(_commentService.GetAll());

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("byid")]
        public ActionResult<CommentDto> Get(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest($"A post with the ID: {id} does not exist");
                }

                return _commentService.GetById(id);
            }
            catch (DataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Add([FromBody] AddCommentDto addCommentDto)
        {
            try
            {
                _commentService.Add(addCommentDto);
                return Ok();
            }
            catch (DataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] UpdateCommentDto updateCommentDto)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userIdClaim = identity.FindFirst("UserId")?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int loggedInUserId))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User ID is missing or invalid.");

                }
                if (loggedInUserId != updateCommentDto.UserId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                _commentService.Update(updateCommentDto);
                return NoContent();
            }
            catch (DataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int id, DeleteCommentDto deleteCommentDto)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var identityUserId = identity.FindFirst("UserId")?.Value;

                if (identityUserId == null || !int.TryParse(identityUserId, out int loggedInUserId))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User ID is missing or invalid.");
                }

                if (loggedInUserId != deleteCommentDto.UserId && identity.FindFirst("userRole").Value != "Admin")
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }
                _commentService.Delete(id);
                return Ok("The comment is deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
