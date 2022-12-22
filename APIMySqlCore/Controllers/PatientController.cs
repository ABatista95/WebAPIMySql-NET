using Microsoft.AspNetCore.Mvc;
using WebApiCore.Data.Repositories.Interfaces;
using WebApiCore.Model.Entities;

namespace APIMySqlCore.Controllers
{
    [ApiController]
    [Route("api/v1/patient")]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            try
            {
                return Ok(await _patientRepository.GetAllPatients());
            } catch(Exception ex)
            {
                return BadRequest("Error. " + ex.Message);
            }
        }

        
        private async Task<IActionResult> GetPatientId(int Id)
        {
            try
            {
                return Ok(await _patientRepository.GetPatientsId(Id));
            }
            catch (Exception ex)
            {
                return BadRequest("Error. " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            try
            {
                if (patient == null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var isPatient = await _patientRepository.GetPatientsId((Int32)patient.Id);

                if (isPatient == null)
                {
                    var created = await _patientRepository.createPatient(patient);
                    return Created("created", created);
                }

                return BadRequest();
            } catch (Exception ex)
            {
                return BadRequest("Error. " + ex.Message);
            }

            
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatient([FromBody] Patient patient)
        {
            try
            {
                if (patient == null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _patientRepository.updatePatient(patient);

                return NoContent();
            } catch (Exception ex)
            {
                return BadRequest("Error Update. " + ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                await _patientRepository.deletePatient(new Patient { Id = id });

                return NoContent();
            } catch (Exception ex)
            {
                return BadRequest("Error Delete. " + ex.Message);
            }
        }

    }
}
