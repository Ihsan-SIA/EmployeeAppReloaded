﻿using Data.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class UpdateEmployeeViewModel
    {
        public  Guid EmployeeId { get; set; }
        
        [Required(ErrorMessage = "Field is compulsory")]
        public string FirstName { get; set; } = default!;
        
        [Required(ErrorMessage = "Field is compulsory")] 
        public string LastName { get; set; } = default!;
        
        [Required(ErrorMessage = "Field is compulsory")]
        public string Email { get; set; } = default!;
        
        [Required(ErrorMessage = "Field is compulsory")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        
        [Required(ErrorMessage = "Field is compulsory")]
        public string Salary { get; set; } = default!;
        public Guid DepartmentId { get; set; } = default!;
        public Address Address { get; set; } = default!;

        [BindNever]
        public List<SelectListItem> States { get; set; } = new();
    }
}
