using WimMed.Data;
using WimMed.Models.Entities;

public static class DatabaseSeeder
{
  public static void Seed(ApplicationDbContext dbContext)
  {
    if (!dbContext.Patients.Any())
    {
      var random = new Random();
      var patients = new List<Patient>();
      var patientInfos = new List<PatientInfo>();

      for (int i = 1; i <= 100; i++)
      {
        // Create a new patient with random data
        var patient = new Patient
        {
          Id = Guid.NewGuid(),
          Name = $"Name{i}",
          Surname = $"Surname{i}",
          IdNumber = $"{random.Next(60, 99):D2}{random.Next(1, 12):D2}{random.Next(1, 28):D2}{random.Next(1000, 9999):D4}000{i % 10}",
          Phone = $"082{random.Next(1000000, 9999999)}",
          Email = $"patient{i}@wimmail.co.za",
          DateOfBirth = new DateOnly(random.Next(1960, 2005), random.Next(1, 12), random.Next(1, 28))
        };
        patients.Add(patient);

        var info = new PatientInfo
        {
          Weight = random.Next(50, 100),
          Height = random.Next(150, 200),
          Age = DateTime.Now.Year - patient.DateOfBirth.Year,
          BMI = 0,
          BMIStatus = "Normal",
          PatientId = patient.Id
        };
        patientInfos.Add(info);
      }

      dbContext.Patients.AddRange(patients);
      dbContext.PatientInfos.AddRange(patientInfos);
      dbContext.SaveChanges();
    }
  }
}
