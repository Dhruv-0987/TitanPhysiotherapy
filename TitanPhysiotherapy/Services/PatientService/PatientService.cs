using AutoMapper;
using Microsoft.Exchange.WebServices.Data;
using TitanPhysiotherapy.Models.PatientModels;
using TitanPhysiotherapy.Models.PatientModels.DTOS;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Database;
using Microsoft.EntityFrameworkCore;

namespace TitanPhysiotherapy.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private static List<Patient> patients = new List<Patient>()
        {
            new Patient {id = 1, firstName = "Dhruv", lastName = "Mathur"}
        };

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public PatientService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
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
            _context.Patients.Add(patientToAdd);
            await _context.SaveChangesAsync();
            ServiceResponse.Data = await _context.Patients.ToListAsync();
            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<Patient>>> DeletePatientById(int id)
        {
            var serviceResponse = new ServiceResponse<List<Patient>>();
            try
            {
                Patient patient = await _context.Patients.FindAsync(keyValues: id);
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
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
            var dbPatients = await _context.Patients.ToListAsync();
            serviceResponse.Data = dbPatients;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Patient>> GetPatientById(int id)
        {
            Patient patient = await _context.Patients.FindAsync(keyValues: id);
            var serviceResponse = new ServiceResponse<Patient>();
            serviceResponse.Data = patient;
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<Patient>>> UpdatePatientById(PatientDto newPatient)
        {
            Patient patientToUpdate = await _context.Patients.FindAsync(keyValues: newPatient.id);
            patientToUpdate.firstName = newPatient.firstName;
            patientToUpdate.lastName = newPatient.lastName;
            patientToUpdate.contactNum = newPatient.contactNum;
            patientToUpdate.id = newPatient.id;
            await _context.SaveChangesAsync();
            var serviceResponse = new ServiceResponse<List<Patient>>();
            serviceResponse.Data = patients;
            serviceResponse.message = "Patient details updated";
            serviceResponse.success = true;
            return serviceResponse;
        }
    }
}
