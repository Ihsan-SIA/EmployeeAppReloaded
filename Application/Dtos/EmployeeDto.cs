using Application.Services.Employee;

namespace Application.Dtos;

public class EmployeeDto
{
    public Guid EmployeeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; } = default!;
    //public string? DepartmentName { get; set; }
}

public class  EmployeesDto
{
    public List<EmployeeDto> Employees { get; set; } = default!;
}

public class  CreateEmployeeDto
{
    public Guid EmployeeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; } = default!;
}
