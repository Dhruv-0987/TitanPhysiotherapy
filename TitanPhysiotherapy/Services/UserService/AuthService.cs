using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TitanPhysiotherapy.Database;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.PatientModels;
using TitanPhysiotherapy.Models.PatientModels.DTOS;
using TitanPhysiotherapy.Models.StaffModel;
using TitanPhysiotherapy.Models.UserModels;

namespace TitanPhysiotherapy.Services.UserService
{
    public class AuthService : IAuthInterface
    {
        private readonly DataContext _context;
        public AuthService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<User>> Login(string username, string password)
        {
            var ServiceResponse = new ServiceResponse<User>();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));

            if (user == null)
            {
                ServiceResponse.Data = user;
                ServiceResponse.message = "User Not Found.";
                ServiceResponse.success = false;
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                ServiceResponse.Data = user;
                ServiceResponse.message = "User Not Found password wrong";
                ServiceResponse.success = false;
            }
            else
            {
                ServiceResponse.Data = user;
                ServiceResponse.message = "User Found";
                ServiceResponse.success = true;
            }


            return ServiceResponse;
        }

        public async Task<ServiceResponse<Patient>> LoginPatient(string username, string password)
        {
            var ServiceResponse = new ServiceResponse<Patient>();
            var patient = await _context.Patients.FirstOrDefaultAsync(u => u.email.ToLower().Equals(username.ToLower()));

            if (patient == null)
            {
                ServiceResponse.Data = patient;
                ServiceResponse.message = "User Not Found.";
                ServiceResponse.success = false;
            }
            else if (!VerifyPasswordHash(password, patient.passwordHash, patient.passwordSalt))
            {
                ServiceResponse.Data = new Patient();
                ServiceResponse.message = "User Not Found password wrong";
                ServiceResponse.success = false;
            }
            else
            {
                ServiceResponse.Data = patient;
                ServiceResponse.message = "User Found";
                ServiceResponse.success = true;
            }


            return ServiceResponse;
        }

        public async Task<ServiceResponse<Staff>> LoginStaff(string username, string password)
        {
            var ServiceResponse = new ServiceResponse<Staff>();
            var staff = await _context.Staff.FirstOrDefaultAsync(u => u.email.ToLower().Equals(username.ToLower()));

            if (staff == null)
            {
                ServiceResponse.Data = staff;
                ServiceResponse.message = "User Not Found.";
                ServiceResponse.success = false;
            }
            else if (!VerifyPasswordHash(password, staff.passwordHash, staff.passwordSalt))
            {
                ServiceResponse.Data = new Staff();
                ServiceResponse.message = "User Not Found password wrong";
                ServiceResponse.success = false;
            }
            else
            {
                ServiceResponse.Data = staff;
                ServiceResponse.message = "User Found";
                ServiceResponse.success = true;
            }


            return ServiceResponse;
        }

        public async Task<ServiceResponse<User>> Register(UserRegisterDto user)
        {
            User newUser = new User();
            //newUser.id = id;
            newUser.Username = user.Username.ToString();
            newUser.Firstname = user.firstName.ToString();
            newUser.Lastname = user.lastName.ToString();
            newUser.email = user.email.ToString();
            CreateHashPassword(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _context.Users.Add(newUser);
            _context.SaveChanges();

            if (user.isPatient)
            {
                Patient newPatient = new Patient();
                newPatient.id = user.id;
                newPatient.firstName = user.firstName;
                newPatient.lastName = user.lastName;
                newPatient.email = user.email;
                newPatient.age = user.age;
                newPatient.contactNum = user.contactNum;

                _context.Patients.Add(newPatient);
                _context.SaveChanges();
            }
            User lastAddedUser = _context.Users.OrderBy(u => u.id).LastOrDefault();
            var serviceResponse = new ServiceResponse<User>();
            serviceResponse.Data = lastAddedUser;
            serviceResponse.message = "user added";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Patient>> RegisterPatient(PatientDto request)
        {
            Patient patient = new Patient();
            patient.id = request.id;
            patient.firstName = request.firstName;
            patient.lastName = request.lastName;
            patient.email = request.email;
            patient.age = request.age;
            patient.contactNum = request.contactNum;
            CreateHashPassword(request.password, out byte[] passwordHash, out byte[] passwordSalt);

            patient.passwordHash = passwordHash;
            patient.passwordSalt = passwordSalt;

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            var serviceResponse = new ServiceResponse<Patient>();
            serviceResponse.Data = _context.Patients.OrderBy(p => p.id).LastOrDefault();
            if (serviceResponse.Data != null)
            {
                serviceResponse.success = true;
            }

            return serviceResponse;
        }

        public async Task<bool> UserExists(string username)
        {
            if (_context.Users.Any(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<string>> LoginAdmin(string username, string password)
        {
            var adminEmail = "admin123@titan.com";
            var adminPassword = "Admin@123";
            var serviceResponse = new ServiceResponse<string>();
            if (username.ToLower().Equals(adminEmail.ToLower()) && password.ToLower().Equals(adminPassword.ToLower()))
            {
               serviceResponse.Data = adminEmail;
               serviceResponse.success = true;
            }
            else
            {
                serviceResponse.Data = "Admin not found";
                serviceResponse.success = false;
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
