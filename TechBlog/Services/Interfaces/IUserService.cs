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
        public RegisterUserDto RegisterAdmin(RegisterUserDto dto);
        void RegisterUser(RegisterUserDto registerUserDto);
        LoginResponseDto Login(LoginUserDto loginUserDto);
        public UserDto GetUserById(int id);
        public ICollection<UserDto> GetAllUsers();
        DetailedUserDto? GetDetailedUserById(int id);
        UpdateUserDto? UpdateUser(UpdateUserDto dto, int id);
        void DeleteUser(int id);
    }
}
