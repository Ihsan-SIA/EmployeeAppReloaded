namespace Presentation.Models;

public class DepartmentViewModel
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public IEnumerable<EmployeeViewModel> Employees { get; set; } = default!;
}

public class DepartmentsViewModel
{
    public List<DepartmentViewModel> Departments { get; set; } = default!;
}
