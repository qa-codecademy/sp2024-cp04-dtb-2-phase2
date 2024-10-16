using DTOs.Image;
using DTOs.NewsLetter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implementation;
using Services.Interfaces;

namespace TechBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsLettersController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public NewsLettersController(IEmailService emailService)
        {
            _emailService = emailService; 
        }
        [HttpPost]
        public IActionResult Upload(string email)
        {
            try
            {
                if (email == null)
                {
                    return BadRequest("No Email given");
                }
                _emailService.Subscribe(email);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Update(NewsLetterUpdateDto newsLetterUpdateDto)
        {
            try
            {
                _emailService.UpdateSubscriber(newsLetterUpdateDto);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete(string email)
        {
            try 
            {
                if (email == null) 
                {
                    return BadRequest("No Email given");
                }
                _emailService.Unsubscribe(email);
                return Ok();
            } 
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
