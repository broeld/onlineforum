using BLL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task SignInAsync();
        Task SignOutAsync();
        Task SignUpAsync();
        Task<UserModel> GetUserDetail(int userId);
        Task<bool> Deactivate(int userId);
        Task UpdateImage(int userId, string profileImageName, string path, byte[] image);
    }
}
