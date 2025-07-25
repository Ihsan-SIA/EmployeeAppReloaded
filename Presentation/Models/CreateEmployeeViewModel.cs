﻿using Data.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class CreateEmployeeViewModel
    {
        [Required(ErrorMessage = "Department is required")]
        public Guid DepartmentId { get; set; } = default!;
        
        public Guid EmployeeId { get; set; }
        
        [Required(ErrorMessage = "Field is compulsory")]
        public string FirstName { get; set; } = default!;
        
        [Required(ErrorMessage = "Field is compulsory")]
        public string LastName { get; set; } = default!;
        
        [Required(ErrorMessage = "Field is compulsory")]
        public string Email { get; set; } = default!;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Field is compulsory")] 
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Field is compulsory")]
        public decimal Salary { get; set; }
        public Address Address { get; set; } = default!;

        [BindNever]
        public List<SelectListItem> States { get; set; } = new();

    }
}
