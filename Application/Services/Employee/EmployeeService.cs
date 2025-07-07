using Application.ContractMapping;
using Application.Dtos;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeAppDbContext _context;
        public EmployeeService(EmployeeAppDbContext context)
        {
            _context = context;
        }
        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            var checkEmployee = _context.Employees.FirstOrDefault(x => x.Id == createEmployeeDto.EmployeeId);
            if (checkEmployee is not null)
            {
                return null;
            }
            
            createEmployeeDto.EmployeeId = Guid.NewGuid();

            var employee = createEmployeeDto.ToModel();
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee.ToDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while creating new employee {ex.Message}");
                return new EmployeeDto();
            }
        }

        public async Task DeleteEmployeeAsync(Guid employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == employeeId);
            if (employee is not null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<EmployeesDto> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
            return employees.EmployeesDto();
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid employeeId)
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employees is null)
            {
                return null;
            }
            var employeeDto = new EmployeeDto()
            {
                EmployeeId = employees.Id,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                Email = employees.Email,
                Salary = $"{employees.Salary:N2}",
                HireDate = employees.HireDate,
                DepartmentName = employees.Department?.Name,
                Address = employees.Address
            };
            return employeeDto;
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employeeDto.EmployeeId);
            if (employee is null)
            {
                return null;
            }

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.HireDate = employeeDto.HireDate;
            employee.Address = employeeDto.Address?? new();
            employee.Salary = decimal.Parse(employeeDto.Salary);
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return employee.ToDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the department: {ex.Message}");
                return new EmployeeDto();
            }
        }
    }
}
