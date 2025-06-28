using Application.Dtos;
using Application.Services.Employee;
using Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.DtoMapping;
using Presentation.Models;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
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
        public IActionResult Create(Guid departmentId)
        {
            var model = new CreateEmployeeModel()
            {
                DepartmentId = departmentId
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
                EmployeeId = employee.EmployeeId,
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

        [HttpGet]
        public async Task<IActionResult> Update(Guid employeeId)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (employee is null)
            {
                TempData["Message"] = "Employee not found!";
                return RedirectToAction("Index");
            }

            var employeeDto = new UpdateEmployeeModel()
            {
                EmployeeId = employee.EmployeeId,
                DepartmentId = employee.DepartmentId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Salary = $"{employee.Salary:N2}",
                HireDate = employee.HireDate,
            };
            return View(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeModel updateEmployeeModel)
        {
            if (!ModelState.IsValid)
            {
                SetFlashMessage("Invalid input detected! Check again", "error");
                return View(updateEmployeeModel);
            }
            var employee = await _employeeService.GetEmployeeByIdAsync(updateEmployeeModel.EmployeeId);
            if (employee is null)
            {
                SetFlashMessage("Unable to update employee details!", "error");
                return View(updateEmployeeModel);
            }

            var employeeDto = new EmployeeDto()
            {
                EmployeeId = updateEmployeeModel.EmployeeId,
                FirstName = updateEmployeeModel.FirstName,
                LastName = updateEmployeeModel.LastName,
                Email = updateEmployeeModel.Email,
                Salary = updateEmployeeModel.Salary,
                HireDate = updateEmployeeModel.HireDate,
            };

            var result = await _employeeService.UpdateEmployeeAsync(employeeDto);
            if (result is null)
            {
                TempData["Message"] = "Failed to update employee details!";
                return View(updateEmployeeModel);
            }

            TempData["Message"] = "Successfully updated employee details!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid employeeId)
        {
            var allEmployees = await _employeeService.GetAllEmployeesAsync();
            try
            {
                var employee = _employeeService.DeleteEmployeeAsync(employeeId);
                TempData["Message"] = "Employee deleted successfully!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(allEmployees);
            }
        }
        
        public async Task<IActionResult> Detail(Guid employeeId)
        {
            var employees = await _employeeService.GetEmployeeByIdAsync(employeeId);
            if (employees == null)
            {
                SetFlashMessage("Unable to view employee!", "error");
                return RedirectToAction("Index");
            }

            var employeeModel = employees.ToViewModel();
            return View(employeeModel);
        }
    }
}
