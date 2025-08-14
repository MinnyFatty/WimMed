using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WimMed.Data;
using WimMed.Models.Entities;

namespace WimMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientInfosController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        public PatientInfosController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// This method retrieves a list of patient information records from the database.
        /// </summary>
        [HttpGet]
        public IActionResult GetPatientInfos()
        {
            var allPatientInfos = dbContext.PatientInfos.ToList();
            return Ok(allPatientInfos);
        }
        /// <summary>
        /// This method returns a single patient information record using id
        /// </summary>
        /// <param name="id">The unique identifier of the patient information record.</param>
        [HttpGet]
        [Route("GetPatientInfoById/{id:int}")]
        public IActionResult GetPatientInfoById(int id)
        {
            var patientInfo = dbContext.PatientInfos.FirstOrDefault(pi => pi.Id == id);
            if (patientInfo == null)
            {
                return NotFound();
            }
            return Ok(patientInfo);
        }

        /// <summary>
        /// This method retrieves patient information records for a specific patient using their unique identifier.
        /// </summary>  
        /// <param name="PatientId">The unique identifier of the patient record</param>
        [HttpGet]
        [Route("GetPatientInfoByPatientId/{PatientId:guid}")]
        public IActionResult GetPatientInfoByPatientId(Guid PatientId)
        {
            var patientInfos = dbContext.PatientInfos.Where(pi => pi.PatientId == PatientId);
            if (patientInfos == null || !patientInfos.Any())
            {
                
                return NotFound();
            }
         
            return Ok(patientInfos);
        }

        /// <summary>
        /// Calculates the Body Mass Index (BMI) for a patient based on their weight and height.
        /// </summary>
        /// <param name="patientId">The patient id (guid).</param>"
        
        [HttpGet]
        [Route("CalculateBMI/{patientId:guid}")]
        public IActionResult CalculateBMI(Guid patientId)
        {
            var patientInfo = dbContext.PatientInfos.FirstOrDefault(pi => pi.PatientId == patientId);
            if (patientInfo == null)
            {
                return NotFound("Patient information not found.");
            }
            // Ensure that weight and height are provided before calculating BMI
            if (patientInfo.Weight.HasValue && patientInfo.Height.HasValue && patientInfo.Height.Value > 0)
            {
                double heightInMeters = patientInfo.Height.Value / 100.0; // Convert cm to meters
                patientInfo.BMI = (int)(patientInfo.Weight.Value / (heightInMeters * heightInMeters));
                // Determine BMI status
                if (patientInfo.BMI < 18.5)
                {
                    patientInfo.BMIStatus = "Underweight";
                }
                else if (patientInfo.BMI < 24.9)
                {
                    patientInfo.BMIStatus = "Normal";
                }
                else if (patientInfo.BMI < 29.9)
                {
                    patientInfo.BMIStatus = "Overweight";
                }
                else
                {
                    patientInfo.BMIStatus = "Obese";
                }
            }
            else
            {
                patientInfo.BMI = null;
                patientInfo.BMIStatus = null; // Reset BMI status if calculation is not possible
            }
            dbContext.SaveChanges();
            return Ok(patientInfo);
        }

        ///<summary>
        ///Edit existing patient information record.
        /// </summary>
        
        [HttpPut]
        [Route("EditPatientInfo/{id:int}")]
        public IActionResult EditPatientInfo(int id, [FromBody] PatientInfo patientInfo)
        {
            if (patientInfo == null || patientInfo.Id != id)
            {
                return BadRequest("Patient information data is null or ID mismatch.");
            }
            var existingPatientInfo = dbContext.PatientInfos.FirstOrDefault(pi => pi.Id == id);
            if (existingPatientInfo == null)
            {
                return NotFound("Patient information not found.");
            }
            existingPatientInfo.Weight = patientInfo.Weight;
            existingPatientInfo.Height = patientInfo.Height;
            existingPatientInfo.Age = patientInfo.Age;

            dbContext.SaveChanges();
            CalculateBMI(existingPatientInfo.PatientId); // Recalculate BMI after updating weight or height
            dbContext.SaveChanges();
            return Ok(existingPatientInfo);
        }

        /// <summary>
        /// Delete a patient information record by its unique identifier.
        /// </summary>
        
        [HttpDelete]
        [Route("DeletePatientInfo/{id:int}")]
        public IActionResult DeletePatientInfo(int id)
        {
            var patientInfo = dbContext.PatientInfos.FirstOrDefault(pi => pi.Id == id);
            if (patientInfo == null)
            {
                return NotFound("Patient information not found.");
            }
            dbContext.PatientInfos.Remove(patientInfo);
            dbContext.SaveChanges();
            return Ok("Patient information deleted successfully.");
        }

        /// <summary>
        /// Add a new patient information record to the database.
        /// </summary>
        
        [HttpPost]
        [Route("AddPatientInfo")]
        public IActionResult AddPatientInfo([FromBody] PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                return BadRequest("Patient information data is null.");
            }
            // Validate that the patient exists before adding patient info
            var patient = dbContext.Patients.FirstOrDefault(p => p.Id == patientInfo.PatientId);
            if (patient == null)
            {
                return NotFound("Patient not found.");
            }
            // Validate required field patientInfo.PatientId
            if (patientInfo.PatientId == Guid.Empty)
            {
                return BadRequest("PatientId is required.");
            }

            dbContext.PatientInfos.Add(patientInfo);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetPatientInfoById), new { id = patientInfo.Id }, patientInfo);
        }

    }
}
