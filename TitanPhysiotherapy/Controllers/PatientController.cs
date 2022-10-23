using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.PatientModels;
using TitanPhysiotherapy.Models.PatientModels.DTOS;
using TitanPhysiotherapy.Services.PatientService;

namespace TitanPhysiotherapy.Controllers
{
    [Route("api/Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<ServiceResponse<List<Patient>>>> GetAllPatients()
        {
            return Ok(await _patientService.GetAllPatients());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Patient>>> GetPatientById(int id)
        {
            return Ok(await _patientService.GetPatientById(id));
        }

        [HttpPost("AddPatient")]
        public async Task<ActionResult<ServiceResponse<Patient>>> AddPatient(PatientDto newPatient)
        {
            return Ok(await _patientService.AddPatient(newPatient));
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ServiceResponse<List<Patient>>>> DeletePatientById(int id)
        {
            return Ok(await _patientService.DeletePatientById(id));
        }

        [HttpPut("UpdatePatient")]
        public async Task<ActionResult<ServiceResponse<List<Patient>>>> UpdatePatient(PatientDto patient)
        {
            return Ok(await _patientService.UpdatePatientById(patient));
        }

        [HttpPost("ContactUs")]
        public async Task<ActionResult<ServiceResponse<bool>>> ContactUs(ContactUsDto contactUsDto)
        {
            return Ok(await _patientService.ContactUs(contactUsDto));
        }

        [HttpPost("BulkEmail")]
        public async Task<ActionResult<ServiceResponse<bool>>> BulkEmail(IFormFile emailFile)
        {
            return Ok(await _patientService.BulkEmail(emailFile));
        }
    }
}
