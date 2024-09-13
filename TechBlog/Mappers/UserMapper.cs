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
    }
}
