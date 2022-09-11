using AutoMapper;
using Microsoft.Exchange.WebServices.Data;
using TitanPhysiotherapy.Models.PatientModels;
using TitanPhysiotherapy.Models.PatientModels.DTOS;
using TitanPhysiotherapy.Models;

namespace TitanPhysiotherapy.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private static List<Patient> patients = new List<Patient>()
        {
            new Patient {id = 1, firstName = "Dhruv", lastName = "Mathur"}
        };

        private readonly IMapper _mapper;

        public PatientService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<Patient>>> AddPatient(PatientDto patient)
        {
            var ServiceResponse = new ServiceResponse<List<Patient>>();
            Patient patientToAdd = new Patient();

            patientToAdd.age = patient.age;
            patientToAdd.firstName = patient.firstName;
            patientToAdd.lastName = patient.lastName;
            patientToAdd.contactNum = patient.contactNum;
            patientToAdd.id = patient.id;

            patients.Add(patientToAdd);
            ServiceResponse.Data = patients;
            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<Patient>>> DeletePatientById(int id)
        {
            var serviceResponse = new ServiceResponse<List<Patient>>();
            try
            {
                Patient patient = patients.First(p => p.id == id);
                patients.Remove(patient);
                serviceResponse.Data = patients;
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = "Patient Not Found";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Patient>>> GetAllPatients()
        {
            var serviceResponse = new ServiceResponse<List<Patient>>();
            serviceResponse.Data = patients;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Patient>> GetPatientById(int id)
        {
            Patient patient = patients.Find(p => p.id == id);
            var serviceResponse = new ServiceResponse<Patient>();
            serviceResponse.Data = patient;
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<Patient>>> UpdatePatientById(PatientDto newPatient)
        {
            Patient patientToUpdate = patients.Find(p => p.id == newPatient.id);
            patientToUpdate.firstName = newPatient.firstName;
            patientToUpdate.lastName = newPatient.lastName;
            patientToUpdate.contactNum = newPatient.contactNum;
            patientToUpdate.id = newPatient.id;
            var serviceResponse = new ServiceResponse<List<Patient>>();
            serviceResponse.Data = patients;
            serviceResponse.message = "Patient details updated";
            serviceResponse.success = true;
            return serviceResponse;
        }
    }
}
