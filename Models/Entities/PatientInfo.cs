
using System.ComponentModel.DataAnnotations;

namespace WimMed.Models.Entities
{
    public class PatientInfo
    {
        public int Id { get; set; } //Primary key
        /// <summary>
        /// Weight in kg
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// Height in cm
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Age in years
        /// </summary>  
        public int? Age { get; set; }

        /// <summary>
        /// Body Mass Index
        /// </summary>
        public double? BMI { get; set; }

        /// <summary>
        /// BMI Status (e.g., Underweight, Normal, Overweight, Obese)
        /// </summary>
        [StringLength(256)]
        public string? BMIStatus { get; set; }

        public Guid PatientId { get; set; } //Foreign key to Patient entity


    }

    // Note: The PatientInfo class is designed to store additional information about a patient,
  
}
