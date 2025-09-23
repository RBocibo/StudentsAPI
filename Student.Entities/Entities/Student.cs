using Student.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace Students.Domain.Entities
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
