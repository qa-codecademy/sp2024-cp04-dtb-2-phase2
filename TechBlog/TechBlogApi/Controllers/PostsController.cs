using Domain_Models;
using DTOs.FilterDto;
using DTOs.Post;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace TechBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private ITokenHelper _tokenHelper { get; set; }
        public PostsController(IPostService service, ITokenHelper helper)
        {
            _postService = service;
            _tokenHelper = helper;
        }
        [HttpPost]
        public async Task<IActionResult> GetPaginatedPosts([FromBody] PostFilter filters)
        {
            var result = await _postService.GetPaginatedPosts(filters.PageIndex, filters);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            if (!_postService.Any(id))
                return NotFound($"No post was found with specified id - {id}");

            var result = _postService.GetById(id);
            return Ok(result);

        }
        [Authorize]
        [HttpPost("create")]
        public IActionResult Create([FromForm] PostCreateDto dto)
        {
            var userId = _tokenHelper.GetUserId();
            var found = _userService.GetUserById(userId);
            if (found != null)
            {
                dto.UserId = userId;
                if (!_postService.Add(dto))
                    return BadRequest("The post wasn't created successfully!");

                return CreatedAtAction("Create", dto);
            }
            return Unauthorized("Invalid user!");
        }
        [Authorize]
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            if (id < 1)
                return BadRequest("Please ensure the value is greater than 0!");

            if (_postService.DeleteById(id))
                return Ok("The post was deleted successfuly!");

            return StatusCode(StatusCodes.Status500InternalServerError, "Post wasn't deleted successfully!");
        }
        [Authorize]
        [HttpPut("update")]
        public IActionResult Update(PostCreateDto dto, int id)
        {
            var userId = _tokenHelper.GetUserId();
            var found = _userService.GetUserById(userId);
            if (found != null)
            {
                dto.UserId = userId;
                if (!_postService.Any(id))
                    return BadRequest("No Post was found with the specified id!");

                if (_postService.Update(dto, id))
                    return Ok("Successfuly updated the post!");


                return StatusCode(StatusCodes.Status500InternalServerError, "The post wasn't updated successfully!");

            }
            return BadRequest("Invalid User");
        }

    }
}