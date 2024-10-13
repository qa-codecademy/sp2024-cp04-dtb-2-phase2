using Domain_Models;
using DTOs.FilterDto;
using DTOs.Post;
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
        public PostsController(IPostService service)
        {
            _postService = service;
        }
        [HttpGet]
        public IActionResult GetAll(PaginatedList pagination, PostFilter filters)
        {
            return Ok(_postService.GetPaginatedPosts(pagination.PageIndex, filters));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute]int id) 
        {
            if(!_postService.Any(id))
                return NotFound($"No post was found with specified id - {id}");

            return Ok(_postService.GetById(id));
        }
        [HttpPost("create")]
        public IActionResult Create([FromForm] PostCreateDto dto) 
        {
            if (!_postService.Add(dto))
                return BadRequest("The post wasn't created successfully!");

            return CreatedAtAction("Create", dto);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id) 
        {
            if (id < 1)
                return BadRequest("Please ensure the value is greater than 0!");

            if (_postService.DeleteById(id))
                return Ok("The post was deleted successfuly!");

            return StatusCode(StatusCodes.Status500InternalServerError, "Post wasn't deleted successfully!");
        }

        [HttpPut("update")]
        public IActionResult Update(PostCreateDto dto, int id)
        {
            if (!_postService.Any(id))
                return BadRequest("No Post was found with the specified id!");

            if (_postService.Update(dto, id))
                return Ok("Successfuly updated the post!");

            return StatusCode(StatusCodes.Status500InternalServerError, "The post wasn't updated successfully!");
        }

    }
}