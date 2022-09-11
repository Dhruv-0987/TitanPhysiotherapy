using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.PatientModels;
using TitanPhysiotherapy.Models.PatientModels.DTOS;

namespace TitanPhysiotherapy.Services.PatientService
{
    public interface IPatientService
    {
        Task<ServiceResponse<List<Patient>>> GetAllPatients();
        Task<ServiceResponse<Patient>> GetPatientById(int id);
        Task<ServiceResponse<List<Patient>>> AddPatient(PatientDto patient);
        Task<ServiceResponse<List<Patient>>> DeletePatientById(int id);
        Task<ServiceResponse<List<Patient>>> UpdatePatientById(PatientDto patient);
    }
}
