using Application.Dtos;
using Presentation.Models;

namespace Presentation.DtoMapping;

public static class Mapperly
{
    public static EmployeeViewModel ToViewModel(this EmployeeDto model)
    {
        return new EmployeeViewModel()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            HireDate = model.HireDate,
            Salary = model.Salary,
            DepartmentId = model.DepartmentId,
        };
    }
    // DepartmentViewModel <-> DepartmentDto
    public static DepartmentViewModel ToViewModel(this DepartmentDto dto)
    {
        return new DepartmentViewModel()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Employees = dto.Employees?
            .Select(e => e.ToViewModel()).ToList() ?? new List<EmployeeViewModel>()
        };
    }
    public static EmployeeDto ToDto(this EmployeeViewModel model)
    {
        return new EmployeeDto()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Salary = model.Salary,
            HireDate = model.HireDate,
            DepartmentId= model.DepartmentId,
        };
    }
    public static DepartmentDto ToDto(this DepartmentViewModel vm)
    {
        return new DepartmentDto()
        {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description,
            Employees = []
        };
    }

    public static EmployeesViewModel ToViewModel(this EmployeesDto dto)
    {
        return new EmployeesViewModel()
        {
            Employees = dto.Employees.Select(x => x.ToViewModel()).ToList()
        };
    }
    // DepartmentsViewModel <-> DepartmentsDto
    public static DepartmentsViewModel ToViewModel(this DepartmentsDto dto)
    {
        return new DepartmentsViewModel()
        {
            Departments = dto.Departments.Select(d => d.ToViewModel()).ToList()
        };
    }

    public static EmployeesDto ToDto(this EmployeesViewModel viewModel)
    {
        return new EmployeesDto()
        {
            Employees = viewModel.Employees.Select(x => x.ToDto()).ToList()
        };
    }
    public static DepartmentsDto ToDto(this DepartmentsViewModel viewModel)
    {
        return new DepartmentsDto()
        {
            Departments = viewModel.Departments.Select(d => d.ToDto()).ToList()
        };
    }
}
