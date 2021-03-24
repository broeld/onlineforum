using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task SignInAsync();
        Task SignOutAsync();
        Task SignUpAsync();
        Task<UserDto> GetUserDetail(int userId);
        Task<bool> Deactivate(int userId);
        Task UpdateImage(int userId, string profileImageName, string path, byte[] image);
    }
}
