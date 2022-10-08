using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.StaffModel;

namespace TitanPhysiotherapy.Services.StaffService
{
    public interface IStaffInterface
    {
        Task<ServiceResponse<List<Staff>>> GetAllStaff();
        Task<ServiceResponse<Staff>> GetStaffById(int id);
        Task<ServiceResponse<List<Staff>>> AddStaff(StaffDto staff);
        Task<ServiceResponse<List<Staff>>> DeleteStaffById(int id);
        Task<ServiceResponse<List<Staff>>> UpdateStaffById(StaffDto staff);
    }
}
