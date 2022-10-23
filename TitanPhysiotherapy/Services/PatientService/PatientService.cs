using AutoMapper;
using TitanPhysiotherapy.Models.PatientModels;
using TitanPhysiotherapy.Models.PatientModels.DTOS;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Database;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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
        private const String API_KEY = "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";
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
            patientToAdd.email = patient.email;

            CreateHashPassword(patient.password, out byte[] passwordHash, out byte[] passwordSalt);

            patientToAdd.passwordHash = passwordHash;
            patientToAdd.passwordSalt = passwordSalt;
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

        public async Task<ServiceResponse<bool>> ContactUs (ContactUsDto contactUsDto)
        {
            var subject = "Query " + contactUsDto.firstName + " " + contactUsDto.lastName;
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("dhruvmat1998@gmail.com", "Titan Physio");
            var to = new EmailAddress(contactUsDto.email, "");
            var plainTextContent = "Thank You For COntacting Us. We Will get back to you shortly" + "\n" + "Your Query: " + contactUsDto.description;
            var htmlContent = "<p>" + contactUsDto.description + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
            var serviceResponse = new ServiceResponse<bool>();
            serviceResponse.Data = true;
            serviceResponse.success = true;
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> BulkEmail([FromForm] IFormFile emailFile)
        {
            var subject = "TITAN PHYSIO NEWS LETTER";
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("dhruvmat1998@gmail.com", "Titan Physio");
            var plainTextContent = "Here is our weekly news letter" + "\n" + "Your Query: " ;
            var htmlContent = "<p>" + "News Letter" + "</p>";
            
            var patients = await _context.Patients.ToListAsync();
            var emails = new List<string>();

            foreach(var patient in patients)
            {
                emails.Add(patient.email);
            }

            var serviceResponse = new ServiceResponse<bool>();
            
            foreach (var email in emails)
            {
                var to = new EmailAddress(email, "");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                await msg.AddAttachmentAsync(
                    emailFile.FileName,
                    emailFile.OpenReadStream(),
                    emailFile.ContentType,
                    "attachment");
                var res = client.SendEmailAsync(msg);
                serviceResponse.Data = true;
            }

            return serviceResponse;
        }

        private void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
