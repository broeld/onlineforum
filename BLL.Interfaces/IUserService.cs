using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<SignedInUserModel> SignInAsync(LoginModel loginModel, string tokenKey, int tokenExpTime, string tokenAud, string tokenIssuer);
        Task SignOutAsync();
        Task<SignedInUserModel> SignUpAsync(RegistrationModel registrationModel, string tokenKey, int tokenExpTime, string tokenAud, string tokenIssuer);
        Task<UserModel> GetUserDetail(int userId);
        Task<bool> Deactivate(int userId);
        Task<bool> IsInRoleAsync(int id, string role);
        Task<ICollection<string>> GetRolesAsync(int userId);
    }
}
