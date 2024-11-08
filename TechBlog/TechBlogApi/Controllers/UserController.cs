using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DTOs.User;
using Services.Interfaces;
using System.Security.Claims;
using Helpers;

namespace TechBlogApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenService;

        public UserController(IUserService userService, ITokenHelper tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                //_userService
                _userService.RegisterUser(registerUserDto);
                return Ok();
            }
            catch (Shared.CustomExceptions.DataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var token = _userService.Login(loginUserDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ICollection<UserDto>> GetAll()
        {
            try
            {
                return _userService.GetAllUsers().ToList();
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("userdetails")]
        public IActionResult GetUserDetails()
        {
            try
            {
                var found = _userService.GetDetailedUserById(_tokenService.GetUserId());
                if(found != null)
                    return Ok(found);

                return NotFound("User wasn't found!");
            }catch(Exception ex)
            {
                return BadRequest($"There was an error while attempting to fetch the user!\n\n{ex.Message}");
            }
        }
        [HttpPut]
        public IActionResult UpdateUserDetails([FromBody] UpdateUserDto dto)
        {
            try
            {
                var id = _tokenService.GetUserId();
                var result = _userService.UpdateUser(dto, id);
                if (result != null) return Ok(result);
                return BadRequest("The user wasn't updated successfully!");

            } catch(Exception ex) 
            {
                return BadRequest($"There was an error while attempting to update the user!\n\n{ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var loggedInUserId = _tokenService.GetUserId();
                
                
                if (loggedInUserId == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User ID is missing or invalid.");
                }
                if (loggedInUserId != id && !_tokenService.GetUserRole())
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

                _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
