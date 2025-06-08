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
            var viewModel = employees.ToViewModel();
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create(Guid id)
        {
            var model = new CreateEmployeeModel()
            {
                DepartmentId = id
            };
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeModel employee)
        {
            if (!ModelState.IsValid)
            {
                SetFlashMessage("Please fill all fields correctly", "error");
                return View(employee);
            }
            var employeeModel = new CreateEmployeeDto()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Salary = employee.Salary,
                HireDate = employee.HireDate,
                DepartmentId = employee.DepartmentId,
            };
            var result = await _employeeService.CreateEmployeeAsync(employeeModel);
            if (result is null)
            {
                SetFlashMessage("An error occurred while creating the department. Please check if there's any problem or department already exist and try again.", "error");
                return View(employee);
            }
            SetFlashMessage("Employee added successfully!", "success");
            var allEmployees = await _employeeService.GetAllEmployeesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid employeeId)
        {
            var allEmployees = await _employeeService.GetAllEmployeesAsync();
            try
            {
                var employee = _employeeService.DeleteEmployeeAsync(employeeId);
                TempData["Message"] = "Department deleted successfully!";
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
