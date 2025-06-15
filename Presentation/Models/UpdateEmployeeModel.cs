using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class UpdateEmployeeModel
    {
        public  Guid EmployeeId { get; set; }
        public string FirstName { get; set; } = default!;
        [Required(ErrorMessage = "Field is compulsory")]
        public string LastName { get; set; } = default!;
        [Required(ErrorMessage = "Field is compulsory")]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Field is compulsory")]
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "Field is compulsory")]
        [DataType(DataType.Date)]
        public string Salary { get; set; } = default!;
        public Guid DepartmentId { get; set; } = default!;
    }
}
