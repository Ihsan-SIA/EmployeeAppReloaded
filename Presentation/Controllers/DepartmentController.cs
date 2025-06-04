using Application.Dtos;
using Application.Services.Department;
using Microsoft.AspNetCore.Mvc;
using Presentation.DtoMapping;
using Presentation.Models;
using Presentation.Views.Department;

namespace Presentation.Controllers;

public class DepartmentController : BaseController
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await _departmentService.GetAllDepartmentsAsync();

        if(departments is null)
        {
            return View(departments);
        }

        var viewModel = departments.ToViewModel();

        return View(viewModel);
    }

    [HttpGet]
    public async Task<ActionResult> Detail(Guid id)
    {
        var department = await _departmentService.GetDepartmentByIdAsync(id);
        if (department is null)
        {
            SetFlashMessage("Department does not exist", "error");
            return RedirectToAction("Index");
        }
        var details = new DepartmentViewModel()
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
        return View(details);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Guid id, CreateDepartmentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            SetFlashMessage("Please fill in all required fields correctly.", "error");
            return View(model);
        }

        var viewModel = new CreateDepartmentDto
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Description = model.Description
        };
        var result = await _departmentService.CreateDepartmentAsync(viewModel);
        if (result == null)
        {
            SetFlashMessage("An error occurred while creating the department. Please check if there's any problem or department already exist and try again.", "error");
            return View(model);
        }

        SetFlashMessage("Department created successfully.", "success");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult> Edit(Guid id)
    {
        var department = await _departmentService.GetDepartmentByIdAsync(id);
        if (department is null)
        {
            TempData["ErrorMessage"] = "Department not found";
            return Redirect("Index");
        }
        var departmentDTO = new UpdateDepartmentModel()
        {
            Name = department.Name,
            Description = department.Description,
        };
        ViewData["Title"] = "Edit Department Record";
        return View(departmentDTO);
    }

    [HttpPost]
    public async Task<ActionResult> Edit([FromRoute]Guid id, UpdateDepartmentModel updateModel)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please fill all fields correctly";
            return View(updateModel);
        }
        var departmentDTO = new DepartmentDto()
        {
            Id = id,
            Name = updateModel.Name,
            Description = updateModel.Description,
        };

        var result = await _departmentService.UpdateDepartmentAsync(departmentDTO);
        if (result is null)
        {
            TempData["ErrorMessage"] = "Failed to update department!!";
            return View(updateModel);
        }

        TempData["ErrorMessage"] = "Department updated successfully";
        return RedirectToAction("Index");
    }

    // POST: DepartmentController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(Guid id)
    {
        try
        {
            var department = _departmentService.DeleteDepartmentAsync(id);
            TempData["Message"] = "Department deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
