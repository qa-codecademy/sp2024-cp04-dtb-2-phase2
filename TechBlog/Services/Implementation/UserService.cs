using Data_Access.Implementations;
using Data_Access.Interfaces;
using Domain_Models;
using DTOs.User;
using Mappers;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserReposiotry _userRepository;
        
        public UserService(IUserReposiotry userReposiotry)
        {
            this._userRepository = userReposiotry;
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                throw new DataException("User cannot be null");
            }
            
            if (!string.IsNullOrEmpty(registerUserDto.FirstName))
            {
                ValidationHelper.ValidateStringColumnLength(registerUserDto.FirstName, "Firstname", 50);
            }

            if (registerUserDto.LastName != null)
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



        public string Login(LoginUserDto loginUserDto)
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

            User userDb = _userRepository.GetUserByEmailAndPassword(loginUserDto.Email, hash);
            if (userDb != null)
            {
                throw new Exception($"Invalid login for the user with email: {loginUserDto.Email}");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("SecretKey");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(5),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                                        SecurityAlgorithms.HmacSha256Signature),

                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim ("userFullName", userDb.FirstName + ' ' + userDb.LastName),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Email),
                        new Claim("userRole", "")
                    })
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            string resultToken = jwtSecurityTokenHandler.WriteToken(token);

            return resultToken;
        }


        private static string GenerateHash(string password)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string hash = Encoding.ASCII.GetString(hashedBytes);

            return hash;
        }

    }
}
