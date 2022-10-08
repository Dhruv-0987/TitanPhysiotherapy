using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.UserModels;

namespace TitanPhysiotherapy.Services.UserService
{
    public interface IAuthInterface
    {
        Task<ServiceResponse<User>> Register(UserRegisterDto request);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
        
    }
}
