﻿using TitanPhysiotherapy.Models;

namespace TitanPhysiotherapy.Services.TreatmentService
{
    public interface ITreatmentInterface
    {
        Task<ServiceResponse<List<Treatment>>> GetTreatmentByPatientId(int id);
        Task<ServiceResponse<List<Treatment>>> GetTreatmentById(int id);
        Task<ServiceResponse<List<Treatment>>> GetTreatmentByStaffId(int id);
        Task<ServiceResponse<List<Treatment>>> AddTreatment(TreatmentDto treatment);
        Task<ServiceResponse<List<Treatment>>> RemoveTreatment(int id);
        Task<ServiceResponse<List<Treatment>>> UpdateTreatment(Treatment treatment);
        Task<ServiceResponse<List<Treatment>>> GetTreatmentsByStaffIdAndDate(int id, string date);
    }
}
