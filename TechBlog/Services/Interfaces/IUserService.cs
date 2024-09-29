using DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto registerUserDto);
        LoginResponseDto Login(LoginUserDto loginUserDto);
        public UserDto GetUserById(int id);
        public ICollection<UserDto> GetAllUsers();
        void DeleteUser(int id);
    }
}
