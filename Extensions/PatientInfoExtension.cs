using WimMed.Models.Entities;

namespace WimMed.Extensions
{
    public class PatientInfoExtension
    {
        public static void CalculateBMI(PatientInfo patientInfo)
        {
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
        }
    }
}
