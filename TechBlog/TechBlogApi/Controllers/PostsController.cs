using Domain_Models;
using DTOs.CommentDto;
using DTOs.FilterDto;
using DTOs.Post;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;

namespace TechBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private  ITokenHelper _tokenHelper { get; set; }
        public PostsController(IPostService service, ITokenHelper helper, IUserService userService)
        {
            _postService = service;
            _tokenHelper = helper;
            _userService = userService;
        }
        [HttpGet("authorposts/{authorId}")]
        public IActionResult GetUserPosts([FromRoute] int authorId) 
        {
            try
            {
                if (authorId < 1) 
                {
                    return BadRequest("Please enter positive values!");
                }

                var posts = _postService.GetUserPosts(authorId);
                return Ok(posts);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetPaginatedPosts([FromBody]PostFilter filters)
        {
            var result = await _postService.GetPaginatedPosts(filters.PageIndex, filters);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id) 
        {
            if (id < 1) return BadRequest("Values below 1 are invalid!");
            //if(!_postService.Any(id))
            //    return NotFound($"No post was found with specified id - {id}");

            var result = _postService.GetById(id);
            if (result != null)
                return Ok(result);

            return NotFound("No post found!");

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_postService.GetAll());
        }
        [Authorize]
        [HttpPost("create")]
        public IActionResult Create([FromBody] PostCreateDto dto) 
        {
            var userId = _tokenHelper.GetUserId();
            var found = _userService.GetUserById(userId);
            if(found != null)
            {
                dto.UserId = userId;
                if (!_postService.Add(dto))
                    return BadRequest("The post wasn't created successfully!");

                return CreatedAtAction("Create", dto);
            }
            return Unauthorized("Invalid user!");
        }
        [Authorize]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id) 
        {
            if (id < 1)
                return BadRequest("Please ensure the value is greater than 0!");
             var foundPost = _postService.GetById(id);

             var identity = HttpContext.User.Identity as ClaimsIdentity;
             var identityUserId = identity.FindFirst("UserId")?.Value;

             if (identityUserId == null || !int.TryParse(identityUserId, out int loggedInUserId))
             {
                 return StatusCode(StatusCodes.Status401Unauthorized, "User ID is missing or invalid.");
             }

             if (loggedInUserId != foundPost.User.Id && identity.FindFirst("isAdmin").Value != "Admin")
             {
                 return StatusCode(StatusCodes.Status403Forbidden);
             }

             if (_postService.DeleteById(id))
                 return Ok("The post was deleted successfuly!");

             return StatusCode(StatusCodes.Status500InternalServerError, "Post wasn't deleted successfully!");
        }
        [Authorize]
        [HttpPut("update")]
        public IActionResult Update(PostUpdateDto dto)
        {
            try
            {
                var userId = _tokenHelper.GetUserId();
                var found = _userService.GetUserById(userId);

                var postToBeEdited = _postService.GetById(dto.Id);

                if (postToBeEdited == null) return BadRequest("The requested post to be updated wasn't found!");

                if (found != null && found.Id == postToBeEdited.User.Id)
                {
                    if (_postService.Update(dto))
                        return Ok("Successfuly updated the post!");
                } else {
                    return NotFound("User wasn't valid for this request!");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "The post wasn't updated successfully!");

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something went wrong when attempting to update the post!\n\n{ex.Message}");
            }

        }

        [HttpGet("search")]
        public IActionResult SearchPosts([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Search query cannot be empty.");
            }

            var posts = _postService.SearchPosts(query);
            return Ok(posts);
        }

    }
}