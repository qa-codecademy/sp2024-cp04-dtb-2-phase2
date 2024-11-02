using DTOs.CommentDto;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Implementation;
using Services.Interfaces;
using System.Data;
using System.Security.Claims;

namespace TechBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private ITokenHelper _tokenHelper { get; set; }

        public CommentController(ICommentService commentService, ITokenHelper tokenHelper, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
            _tokenHelper = tokenHelper;
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
        [Authorize]
        [HttpPost]
        public ActionResult Add([FromBody] AddCommentDto addCommentDto)
        {
            try
            {
                var userId = _tokenHelper.GetUserId();
                var found = _userService.GetUserById(userId);
                if (found != null)
                {
                    var result = _commentService.Add(addCommentDto, userId);

                    return CreatedAtAction("Add", result);
                }
                return Unauthorized("Invalid user!");
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
