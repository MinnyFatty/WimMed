using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        [Route("GetPatientById/{id:guid}")]
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

            // Validate required fields
            if (string.IsNullOrEmpty(patient.Name) || string.IsNullOrEmpty(patient.Surname) ||
                string.IsNullOrEmpty(patient.IdNumber) || string.IsNullOrEmpty(patient.Phone))
            {
                return BadRequest("Name, Surname, ID Number, and Phone are required fields.");
            }
            // Validate South African ID number format

            ValidateSouthAfricanIdNumberUsingDOB(patient.IdNumber, patient.DateOfBirth.ToString("yyyy-MM-dd"));

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
        [Route("EditPatient/{id:guid}")]
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
            // Validate required fields
            if (string.IsNullOrEmpty(updatedPatient.Name) || string.IsNullOrEmpty(updatedPatient.Surname) ||
                string.IsNullOrEmpty(updatedPatient.IdNumber) || string.IsNullOrEmpty(updatedPatient.Phone))
            {
                return BadRequest("Name, Surname, ID Number, and Phone are required fields.");
            }
            // Validate South African ID number format
            //Handle the case where the IDNumber/DOB is being updated and needs to be validated again
            if (existingPatient.IdNumber != updatedPatient.IdNumber || existingPatient.DateOfBirth != updatedPatient.DateOfBirth)
                try
                {
                    //store response of call ValidateSouthAfricanIdNumberUsingDOB in a variable for future use if needed

                    var response = ValidateSouthAfricanIdNumberUsingDOB(updatedPatient.IdNumber, updatedPatient.DateOfBirth.ToString("yyyy-MM-dd"));
                    // Replace this block in EditPatient method:
                    // response.ExecuteResultAsync(HttpContext).Wait(); // Execute the validation synchronously for simplicity

                    // With this:
                    if (response is BadRequestObjectResult badRequest)
                    {
                        return BadRequest(badRequest.Value);
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            // Update the existing patient record
            existingPatient.Name = updatedPatient.Name;
            existingPatient.Surname = updatedPatient.Surname;
            existingPatient.IdNumber = updatedPatient.IdNumber;
            existingPatient.Phone = updatedPatient.Phone;
            existingPatient.Email = updatedPatient.Email;
            existingPatient.DateOfBirth = updatedPatient.DateOfBirth;
            try
            {
                dbContext.Patients.Update(existingPatient);
                dbContext.SaveChanges();
                return Ok(existingPatient);
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
     



        }

        /// <summary>
        /// Delete existing patient record from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the patient.</param>
        [HttpDelete]
        [Route("DeletePatient/{id:guid}")]
        public IActionResult DeletePatient(Guid id)
        {
            var patient = dbContext.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            //remove PatientInfos using Patient Id
            var patientInfos = dbContext.PatientInfos.Where(pi => pi.PatientId == id).ToList();
            if (patientInfos.Any())
            {
                dbContext.PatientInfos.RemoveRange(patientInfos);
                dbContext.SaveChanges();
            }
            dbContext.Patients.Remove(patient);
            try 
            { 
                dbContext.SaveChanges();
                return Ok("Patient record deleted successfully.");
            }
            catch (Exception dbEx)
            {
                //handle foreign key constraint errors or other database errors
                if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("foreign key constraint"))
                {
                    return Conflict("Cannot delete patient with existing related records.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {dbEx.Message}");
                }
            }

        }

        /// <summary>
        /// Validate South African ID number using date of birth.
        /// </summary>

        /// <param name="idNumber">The South African ID number to validate.</param>
        /// <param name="DateOfBirth">The date of birth in "yyyy-MM-dd" format.</param>
        
        [HttpGet]
        [Route("validate-id-number")]
        public IActionResult ValidateSouthAfricanIdNumberUsingDOB(string idNumber, string DateOfBirth)
        {
            // Validate the South African ID number format using date of birth
            // Assuming the date of birth is in the format "yyyy-MM-dd" and id number is a 13-digit string and in the format "YYMMDDSSSSCAZ"
           
            if (string.IsNullOrEmpty(DateOfBirth) || !DateOnly.TryParse(DateOfBirth, out DateOnly dob))
            {
                return BadRequest("Invalid date of birth format.");
            }

            if (string.IsNullOrEmpty(idNumber) || idNumber.Length != 13)
            {
                return BadRequest("Invalid South African ID number format.");
            }

            // Extract the date of birth from the ID number
            string idNumberDateOfBirth = idNumber.Substring(0, 6);
            // Convert the ID number date of birth to DateOnly
            if(idNumberDateOfBirth != dob.ToString("yyMMdd"))
            {
                return BadRequest("The date of birth does not match the ID number.");
            }
            return Ok(true);
        }

        //Search for patients by name or surname or ID number
        [HttpGet]
        [Route("search")]
        public IActionResult SearchPatients(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Search query cannot be empty.");
            }
            var loweredQuery = query.ToLower();
            var patients = dbContext.Patients
                .Where(p => p.Name.ToLower().Contains(loweredQuery) ||
                            p.Surname.ToLower().Contains(loweredQuery) ||
                            p.IdNumber.ToLower().Contains(loweredQuery))
                .ToList();
            if (!patients.Any())
            {
                return NotFound("No patients found matching the search criteria.");
            }
            return Ok(patients);
        }
    }
}