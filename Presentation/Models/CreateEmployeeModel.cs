using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class CreateEmployeeModel
    {
        public Guid EmployeeId { get; set; }
        [Required(ErrorMessage = "Field is compulsory")]
        public string FirstName { get; set; } = default!;
        [Required(ErrorMessage = "Field is compulsory")]
        public string LastName { get; set; } = default!;
        [Required(ErrorMessage = "Field is compulsory")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Field is compulsory")]
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "Field is compulsory")]
        public decimal Salary { get; set; }
        public Guid DepartmentId { get; set; } = default!;
    }
}
