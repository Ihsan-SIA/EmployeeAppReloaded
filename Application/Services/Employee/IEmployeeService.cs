using Application.Dtos;

namespace Application.Services.Employee;

public interface IEmployeeService
{
    Task<EmployeesDto> GetAllEmployeesAsync();
    Task<EmployeeDto> GetByIdAsync(Guid employeeId);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
    Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employeeDto);
    Task<EmployeeDto> DeleteEmployeeAsync(Guid employeeId);
}
