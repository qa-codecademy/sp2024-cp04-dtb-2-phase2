using Data_Access.Interfaces;
using Domain_Models;
using DTOs.User;
using Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Shared;
using Shared.CustomExceptions;
//using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserReposiotry _userRepository;
        private readonly IConfiguration _config;

        public UserService(IUserReposiotry userReposiotry, IConfiguration config)
        {
            this._userRepository = userReposiotry;
            _config = config;

    }

    public void RegisterUser(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                throw new DataException("User cannot be null");
            }

            if (string.IsNullOrEmpty(registerUserDto.FirstName))
            {
                ValidationHelper.ValidateStringColumnLength(registerUserDto.FirstName, "Firstname", 50);
            }

            if (registerUserDto.LastName == null)
            {
                ValidationHelper.ValidateStringColumnLength(registerUserDto.LastName, "Lastname", 50);
            }

            if (string.IsNullOrEmpty(registerUserDto.Email))
            {
                ValidationHelper.ValidateStringColumnLength(registerUserDto.Email, "Email", 100);
            }

            if (string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmPassword))
            {
                throw new DataException("Password fields are required");
            }

            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new DataException("Passwords must match");
            }

            User userDb = _userRepository.GetUserByEmail(registerUserDto.Email);
            if (userDb != null)
            {
                //this means that we have a user with registerUserDto.Username in the db
                throw new DataException($"Username {registerUserDto.Email} is already in use");
            }

            string hash = GenerateHash(registerUserDto.Password);

            User newUser = registerUserDto.ToUser(hash);

            _userRepository.Add(newUser);

        }



        public LoginResponseDto Login(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
            {
                throw new DataException("User cannot be null");
            }

            if (string.IsNullOrEmpty(loginUserDto.Email) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new DataException("Username and password are required");
            }

            string hash = GenerateHash(loginUserDto.Password);

            User? userDb = _userRepository.GetUserByEmailAndPassword(loginUserDto.Email, hash);
            if (userDb == null)
            {
                throw new Exception($"Invalid login for the user with email: {loginUserDto.Email}");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_config["Token"]);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(5),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                                        SecurityAlgorithms.HmacSha256Signature),

                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim("UserId", userDb.Id.ToString()),
                        new Claim ("userFullName", userDb.FirstName + ' ' + userDb.LastName),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Email),
                        new Claim("userRole", "")
                    })
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            string resultToken = jwtSecurityTokenHandler.WriteToken(token);

            return userDb.ToLoginUserDto(resultToken);
        }


        private string GenerateHash(string password)
        {
            MD5 md5CryptoService = MD5.Create();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            byte[] hashBytes = md5CryptoService.ComputeHash(passwordBytes);

            return Encoding.ASCII.GetString(hashBytes);
        }

        public UserDto GetUserById(int id)
        {
            User? user = _userRepository.GetById(id);
            if (user == null)
            {
                throw new NotFoundException($"User with {id} not found");
            }
            UserDto userDto = user.ToUserDto();
            return userDto;
        }

        public ICollection<UserDto> GetAllUsers()
        {
            return _userRepository.GetAll().Select(x => x.ToUserDto()).ToList(); ;
        }

        public void DeleteUser(int id)
        {
            User? userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new NotFoundException("Comment not found");
            }
            _userRepository.DeleteById(id);
        }
    }
}
