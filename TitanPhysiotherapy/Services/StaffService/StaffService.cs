using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TitanPhysiotherapy.Database;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.StaffModel;

namespace TitanPhysiotherapy.Services.StaffService
{
    public class StaffService : IStaffInterface
    {
        private readonly DataContext _context;
        public StaffService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<Staff>>> AddStaff(StaffDto staff)
        {
            Staff newStaff = new Staff();
            newStaff.staffId = staff.id;
            newStaff.lastName = staff.lastName;
            newStaff.firstName = staff.firstName;
            newStaff.contactNum = staff.contactNum; 
            newStaff.clinicLocation = staff.clinicLocation;
            newStaff.email = staff.email;
            newStaff.age = staff.id;

            CreateHashPassword(staff.password, out byte[] passwordHash, out byte[] passwordSalt);

            newStaff.passwordHash = passwordHash;
            newStaff.passwordSalt = passwordSalt;

            _context.Staff.Add(newStaff);
            await _context.SaveChangesAsync();

            var serviceResponse = new ServiceResponse<List<Staff>>();
            serviceResponse.Data = await _context.Staff.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Staff>>> DeleteStaffById(int id)
        {
            var serviceResponse = new ServiceResponse<List<Staff>>();
            try
            {
                Staff staff = await _context.Staff.FindAsync(keyValues: id);
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Staff.ToListAsync();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = "Patient Not Found";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Staff>>> GetAllStaff()
        {
            var serviceResponse = new ServiceResponse<List<Staff>>();
            serviceResponse.Data = await _context.Staff.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Staff>> GetStaffById(int id)
        {
            Staff staff = await _context.Staff.FindAsync(keyValues: id);
            var serviceResponse = new ServiceResponse<Staff>();
            serviceResponse.Data = staff;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Staff>>> UpdateStaffById(StaffDto staff)
        {
            Staff staffToUpdate = await _context.Staff.FindAsync(keyValues: staff.id);
            staffToUpdate.firstName = staff.firstName;
            staffToUpdate.lastName = staff.lastName;
            staffToUpdate.contactNum = staff.contactNum;
            staffToUpdate.staffId = staff.id;
            await _context.SaveChangesAsync();
            var serviceResponse = new ServiceResponse<List<Staff>>();
            serviceResponse.Data = await _context.Staff.ToListAsync();
            serviceResponse.message = "Patient details updated";
            serviceResponse.success = true;
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
