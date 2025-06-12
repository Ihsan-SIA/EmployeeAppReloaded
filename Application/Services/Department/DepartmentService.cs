using Application.ContractMapping;
using Application.Dtos;
using Data.Context;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using MySqlX.XDevAPI.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services.Department;

public class DepartmentService : IDepartmentService
{
    private readonly EmployeeAppDbContext _context;

    public DepartmentService(EmployeeAppDbContext context)
    {
        _context = context;
    }

    public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto)
    {
        var data = new CreateDepartmentDto
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description
        };
        var checkDepartment = _context.Departments.FirstOrDefault(x => x.Name.ToLower() == dto.Name.ToLower());
        if (checkDepartment is not null)
        {
            return null;
        }
        var department = data.ToModel();

        try
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return department.ToDto();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating the department {ex.Message}.");
            return new DepartmentDto();
        }
    }

    public async Task DeleteDepartmentAsync(Guid departmentId)
    {
        var department = _context.Departments.FirstOrDefault(x => x.Id == departmentId);
        if (department is not null)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<DepartmentsDto> GetAllDepartmentsAsync()
    {
        var departments = await _context.Departments.ToListAsync();

        return departments.DepartmentsDto();
    }

    public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid departmentId)
    {
        //var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == departmentId);
        var department = await _context.Departments
            .Include(y => y.Employees)
            .FirstOrDefaultAsync(x => x.Id == departmentId);
        if (department is null)
        {
            return null;
        }
        return new DepartmentDto()
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            Employees = department.Employees.Select(x => new EmployeeDto
            {
                EmployeeId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                HireDate = x.HireDate,
                Salary = $"{x.Salary:N2}"
            }).ToList()
        };
    }

    public async Task<DepartmentDto> UpdateDepartmentAsync(DepartmentDto departmentDto)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == departmentDto.Id);
        if (department is null)
        {
            return null;
        }
        department.Name = departmentDto.Name;
        department.Description = departmentDto.Description;

        try
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department.ToDto();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating the department: {ex.Message}");
            return new DepartmentDto();
        }
    }
}
