using DTOs.NewsLetter;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        [HttpGet("{email}")]
        public IActionResult Get([EmailAddress]string email) 
        {
            try
            {
                var found = _emailService.GetSubscriberByEmail(email);
                return Ok(found);
            }
            catch(Exception ex) 
            {
                return BadRequest($"Something went wrong, please try again later!\n\n{ex.Message}");
            }
        }

        [HttpPost("{email}")]
        public IActionResult Upload([EmailAddress]string email)
        {
            try
            {
                if(_emailService.Subscribe(email))
                    return CreatedAtAction("Upload", "Successfully subscribed!");

                return BadRequest($"Attempt to subscribe with email: [ {email} ] wasn't successful!");

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
                return Ok(newsLetterUpdateDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        } 
        [HttpDelete("{email}")]
        public IActionResult Delete([EmailAddress]string email)
        {
            try 
            {
                if (email == null) 
                {
                    return BadRequest("No Email given");
                }
                if(_emailService.Unsubscribe(email))
                    return Ok(email);

                return BadRequest($"Attempt to unsubscribe with email: [ {email} ] wasn't successful!");
            } 
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
