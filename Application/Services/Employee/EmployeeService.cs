﻿using Application.ContractMapping;
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
            var checkEmployee = _context.Employees.FirstOrDefault(x => x.EmployeeId == createEmployeeDto.EmployeeId);
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
            //throw new NotImplementedException();
            return null;
        }

        public async Task<EmployeesDto> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees.EmployeesDto();
        }

        public async Task<EmployeeDto> GetByIdAsync(Guid employeeId)
        {
            var employees = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (employees is null)
            {
                return null;
            }
            var employeeDto = new EmployeeDto()
            {
                EmployeeId = employees.EmployeeId,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                Email = employees.Email,
                Salary = employees.Salary,
                HireDate = employees.HireDate,
            };
            return employeeDto;
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeDto.EmployeeId);
            if (employee is null)
            {
                return null;
            }

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.HireDate = employeeDto.HireDate;
            employee.Salary = employeeDto.Salary;
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
