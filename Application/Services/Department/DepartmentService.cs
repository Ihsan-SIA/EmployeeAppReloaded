using Application.ContractMapping;
using Application.Dtos;
using Data.Context;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        var department = data.ToModel();

        try
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return department.ToDto();
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred while creating the department.", ex);
            return new DepartmentDto();
        }
    }

    public Task DeleteDepartmentAsync(Guid departmentId)
    {
        throw new NotImplementedException();
    }

    public async Task<DepartmentsDto> GetAllDepartmentsAsync()
    {
        var departments = await _context.Departments.ToListAsync();

        return departments.DepartmentsDto();
    }

    public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid departmentId)
    {
        var departments = await _context.Departments.FirstOrDefaultAsync(x => x.Id == departmentId);
        if (departments is null)
        {
            return null;
        }
        var departmentDTO = new DepartmentDto()
        {
            Id = departmentId,
            Name = departments.Name,
            Description = departments.Description
        };
        return departmentDTO;
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

        //var data = new CreateDepartmentDto
        //{
        //    Id = Guid.NewGuid(),
        //    Name = departmentDto.Name,
        //    Description = departmentDto.Description
        //};

        //department = data.ToModel();

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
