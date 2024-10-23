using Domain_Models;
using DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDto registerUserDto, string hash)
        {
            return new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Email = registerUserDto.Email,
                Password = hash
            };
        }
        public static LoginResponseDto ToLoginUserDto(this User user, string token)
        {
            return new LoginResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };
        }
        public static UserDto ToUserDto(this User model)
        {
            var dto = new UserDto();
            if (model != null)
            {
                dto.Fullname = model.FullName;
                dto.Id = model.Id;
            }
            return dto;
        }
    }
}
