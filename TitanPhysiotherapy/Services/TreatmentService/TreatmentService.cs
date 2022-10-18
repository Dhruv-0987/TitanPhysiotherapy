using Microsoft.EntityFrameworkCore;
using TitanPhysiotherapy.Database;
using TitanPhysiotherapy.Models;

namespace TitanPhysiotherapy.Services.TreatmentService
{
    public class TreatmentService : ITreatmentInterface
    {
        private readonly DataContext _context;
        public TreatmentService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<Treatment>>> AddTreatment(TreatmentDto treatmentDto)
        {
            var serviceResponse = new ServiceResponse<List<Treatment>>();
            Treatment treatment = new Treatment();

            treatment.description = treatmentDto.description;
            treatment.patientId = treatmentDto.patientId;
            treatment.staffId = treatmentDto.staffId;
            treatment.treatmentId = treatmentDto.treatmentId;
            treatment.DateTime = DateTime.Parse(treatmentDto.DateTime);
            treatment.clinicLocation = treatmentDto.clinicLocation;
            treatment.staffName = treatmentDto.staffName;

            _context.Treatment.Add(treatment);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Treatment.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Treatment>>> GetTreatmentById(int id)
        {
            var treatments = _context.Treatment.Where(t => t.treatmentId == id);
            var serviceResponse = new ServiceResponse<List<Treatment>>();
            serviceResponse.Data = treatments.ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Treatment>>> GetTreatmentByPatientId(int id)
        {
            var treatments = _context.Treatment.Where(t => t.patientId == id);
            var serviceResponse = new ServiceResponse<List<Treatment>>();
            serviceResponse.Data = treatments.ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Treatment>>> GetTreatmentByStaffId(int id)
        {
            var treatments = _context.Treatment.Where(t => t.staffId == id);
            var serviceResponse = new ServiceResponse<List<Treatment>>();
            serviceResponse.Data = treatments.ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Treatment>>> RemoveTreatment(int id)
        {
            var serviceResponse = new ServiceResponse<List<Treatment>>();
            try
            {
                var treatment = await _context.Treatment.FindAsync(keyValues: id);
                _context.Treatment.Remove(treatment);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Treatment.ToListAsync();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = "Patient Not Found";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Treatment>>> UpdateTreatment(Treatment newTreatment)
        {
            Treatment treatment = await _context.Treatment.FirstOrDefaultAsync(t => t.treatmentId == newTreatment.treatmentId);
            treatment.description = newTreatment.description;
            treatment.staffId = newTreatment.staffId;
            treatment.patientId = newTreatment.patientId;
            treatment.treatmentId = newTreatment.treatmentId;
            treatment.DateTime = newTreatment.DateTime;

            await _context.SaveChangesAsync();
            var serviceResponse = new ServiceResponse<List<Treatment>>();
            serviceResponse.Data = await _context.Treatment.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Treatment>>> GetTreatmentsByStaffIdAndDate(int id, string date)
        {
            var dateTime = DateTime.Parse(date);
            var treatments = _context.Treatment.Where(t => t.staffId == id).Where(t => t.DateTime.Date == dateTime.Date).ToList();
            var serviceResponse = new ServiceResponse<List<Treatment>>();
            serviceResponse.Data = treatments;
            return serviceResponse;
        }
    }
}
