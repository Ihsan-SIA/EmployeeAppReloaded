namespace Presentation.Models
{
    public class EmployeeViewModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public Guid DepartmentId { get; set; } = default!;
    }
    public class EmployeesViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; } = default!;
        public Guid DepartmentId { get; set; }
    }
}
