using System.Collections.Generic;
using System.Threading.Tasks;
using Graduation.Models.Auth;

namespace Graduation.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<List<UserModel>> GetAllUser();
        Task<UserModel> GetUserByUsername(string username);
    }
}