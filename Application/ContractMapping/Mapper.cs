using Application.Dtos;
using Data.Model;

namespace Application.ContractMapping;
public static class Mapper
{
    public static EmployeeDto ToDto(this Employee employee)
    {
        if (employee == null) return null;

        return new EmployeeDto()
        {
            EmployeeId = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            HireDate = employee.HireDate,
            Salary = $"{employee.Salary:N2}",
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.Name,
            Address = employee.Address
        };
    }
    public static DepartmentDto ToDto(this Department department)
    {
        if (department == null) return null!;
        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }
    public static Employee ToModel(this CreateEmployeeDto createEmployeeDto)
    {
        if (createEmployeeDto == null) return null!;
        return new Employee()
        {
            Id = createEmployeeDto.EmployeeId,
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            Email = createEmployeeDto.Email,
            HireDate = createEmployeeDto.HireDate,
            Salary = createEmployeeDto.Salary,
            DepartmentId = createEmployeeDto.DepartmentId,
            Address = createEmployeeDto.Address,
        };
    }
    public static Department ToModel(this CreateDepartmentDto createDepartmentDto)
    {
        if (createDepartmentDto == null) return null!;
        return new Department
        {
            Id = Guid.NewGuid(),
            Name = createDepartmentDto.Name,
            Description = createDepartmentDto.Description
        };
    }
    public static EmployeesDto EmployeesDto(this List<Employee> employees)
    {
        if (employees == null || !employees.Any()) return null;
        return new EmployeesDto()
        {
            Employees = employees.Select(x => x.ToDto()).ToList()
        };
    }
    public static DepartmentsDto DepartmentsDto(this List<Department> departments)
    {
        if (departments == null || !departments.Any()) return null!;
        return new DepartmentsDto
        {
            Departments = departments.Select(d => d.ToDto()).ToList()
        };
    }
}
