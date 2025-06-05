using Application.Dtos;
using Presentation.DtoMapping;
using Presentation.Models;
using Application.Services.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            if (employees is null)
            {
                return View(employees);
            }
            //var viewModel = employees.ToViewModel();
            return View(viewModel);
        }
    }
}
