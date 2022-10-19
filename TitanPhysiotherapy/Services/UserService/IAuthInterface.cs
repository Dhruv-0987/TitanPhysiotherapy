using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.PatientModels;
using TitanPhysiotherapy.Models.PatientModels.DTOS;
using TitanPhysiotherapy.Models.StaffModel;
using TitanPhysiotherapy.Models.UserModels;

namespace TitanPhysiotherapy.Services.UserService
{
    public interface IAuthInterface
    {
        Task<ServiceResponse<Patient>> RegisterPatient(PatientDto request);
        Task<ServiceResponse<Patient>> LoginPatient(string username, string password);
        Task<ServiceResponse<Staff>> LoginStaff(string username, string password);
        Task<ServiceResponse<string>> LoginAdmin(string username, string password);
        Task<bool> UserExists(string username);
        
    }
}
