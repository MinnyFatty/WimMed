using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WimMed.Models.Entities
{
  public class Patient
  {
    public Guid Id { get; set; } //Primary key

    /// <summary>
    /// Patient's first name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Patient's surname.
    /// </summary>
    public required string Surname { get; set; }

        /// <summary>
        /// South African ID number of the patient.
        /// </summary>
        [DisplayName("South African ID Number")]
        [StringLength(13, ErrorMessage = "ID number cannot exceed 13 characters.")]
        public required string IdNumber { get; set; } //Validate SA Id Number format

    /// <summary>
    /// Phone number of the patient.
    /// </summary>
    [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    [DisplayName("Phone Number")]
    public required string Phone { get; set; }

    /// <summary>
    /// Email address of the patient.
    /// </summary>
    public string? Email { get; set; } //Validate email format

    /// <summary>
    /// Date of birth of the patient.
    /// </summary>
    [DisplayName("Date of Birth")]
    public DateOnly DateOfBirth { get; set; }


  }
}
