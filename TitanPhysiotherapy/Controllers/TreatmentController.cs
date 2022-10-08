using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Services.TreatmentService;

namespace TitanPhysiotherapy.Controllers
{
    [Route("api/Treatment")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentInterface _treatmentService;

        public TreatmentController(ITreatmentInterface treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult<ServiceResponse<List<Treatment>>>> getTreatmentByPatientId(int id)
        {
            return Ok(_treatmentService.GetTreatmentByPatientId(id));
        }

        [HttpGet("{staffId}")]
        public async Task<ActionResult<ServiceResponse<Treatment>>> getTreatmentByStaffId(int id)
        {
            return Ok(_treatmentService.GetTreatmentByStaffId(id));
        }

        [HttpGet("{treatmentId}")]
        public async Task<ActionResult<ServiceResponse<List<Treatment>>>> getTreatmentById(int id)
        {
            return Ok(_treatmentService.GetTreatmentById(id));
        }
        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<Treatment>>> addTreatment(Treatment treatment)
        {
            return Ok(_treatmentService.AddTreatment(treatment));
        }
        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Treatment>>> updateTreatment(Treatment treatment)
        {
            return Ok(_treatmentService.UpdateTreatment(treatment));    
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ServiceResponse<Treatment>>> deleteTreatment(int id)
        {
            return Ok(_treatmentService.RemoveTreatment(id));
        } 
    }
}
