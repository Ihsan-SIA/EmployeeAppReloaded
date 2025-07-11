using System.ComponentModel.DataAnnotations;


namespace Data.Model;

public class Employee
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = default!;
    public Address Address {  get; set; } = default!;
}
public class Address
{
    [Required(ErrorMessage = "Street number and name is required")]
    public string Street { get; set; } = default!;

    [Required(ErrorMessage = "State is required")]
    public string State { get; set; } = default!;
}