using Application.Dtos;

namespace Application.Services.Employee;

public interface IEmployeeService
{
    Task<EmployeesDto> GetAllEmployeesAsync();
    Task<EmployeeDto> GetEmployeeByIdAsync(Guid employeeId);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
    Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employeeDto);
    Task DeleteEmployeeAsync(Guid employeeId);
}