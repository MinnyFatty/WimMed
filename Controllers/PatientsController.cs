using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WimMed.Data;

namespace WimMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public PatientsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// This method would retrieve a list of patients from the database.
        /// </summary>
        [HttpGet]
        [Route("all")]
        public IActionResult GetPatients()
        {
            var allPatients = dbContext.Patients.ToList();

            return Ok(allPatients);
        }

        /// <summary>
        /// This method returns a single patient record using id
        /// </summary>
        /// <param name="id">The unique identifier of the patient.</param>"
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetPatientById(Guid id)
        {
            var patient = dbContext.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        /// <summary>
        /// Add new patient record to the database.
        /// </summary>
        /// <param>
        /// <param name="Name">Name of patient</param>
        /// <param name="Surname">Surname of patient</param>    
        /// <param name="IdNumber">South African ID number of the patient</param>
        /// <param name="Phone">Phone number of the patient</param>
        /// <param name="Email">Email address of the patient</param>
        /// <param name="DateOfBirth">Date of birth of the patient</param>
        /// </param>

        [HttpPost]
        [Route("add")]
        public IActionResult AddPatient([FromBody] Models.Entities.Patient patient)
        {
            if (patient == null)
            {
                return BadRequest("Patient data is null.");
            }
            dbContext.Patients.Add(patient);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception dbEx)
            {
                //handle duplicate foreign key or other database errors
                if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("duplicate key"))
                {
                    return Conflict("A patient with the same ID number already exists.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {dbEx.Message}");
                }
            }
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
        }

        /// <summary>
        /// Edit existing patient record in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the patient.</param>

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult EditPatient(Guid id, [FromBody] Models.Entities.Patient updatedPatient)
        {
            if (updatedPatient == null)
            {
                return BadRequest("Patient data is null.");
            }
            var existingPatient = dbContext.Patients.FirstOrDefault(p => p.Id == id);
            if (existingPatient == null)
            {
                return NotFound();
            }
            existingPatient.Name = updatedPatient.Name;
            existingPatient.Surname = updatedPatient.Surname;
            existingPatient.IdNumber = updatedPatient.IdNumber;
            existingPatient.Phone = updatedPatient.Phone;
            existingPatient.Email = updatedPatient.Email;
            existingPatient.DateOfBirth = updatedPatient.DateOfBirth;
            dbContext.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Delete existing patient record from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the patient.</param>
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeletePatient(Guid id)
        {
            var patient = dbContext.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            dbContext.Patients.Remove(patient);
            dbContext.SaveChanges();
            return NoContent();
        }

    }
}