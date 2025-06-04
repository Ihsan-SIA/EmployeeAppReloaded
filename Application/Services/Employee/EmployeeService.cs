using Application.ContractMapping;
using Application.Dtos;
using Data.Context;
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
            var employeeData = new CreateEmployeeDto()
            {
                EmployeeId = Guid.NewGuid(),
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName,
                Email = createEmployeeDto.Email,
                HireDate = createEmployeeDto.HireDate,
                Salary = createEmployeeDto.Salary,
                DepartmentId = createEmployeeDto.DepartmentId
            };

            var employee = employeeData.ToModel();
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee.ToDto();
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured while creating new employee");
                return new EmployeeDto();
            }
        }

        public Task<EmployeeDto> DeleteEmployeeAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeesDto> GetAllEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> GetByIdAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
