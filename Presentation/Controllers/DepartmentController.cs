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

    public ActionResult Details(int id)
    {
        return View();
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateDepartmentViewModel model)
    {
        if(!ModelState.IsValid)
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
            SetFlashMessage("An error occurred while creating the department. Please try again.", "error");
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
        return View(departmentDTO);
    }

    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(Guid id, UpdateDepartmentModel updateModel)
    {
        var departmentDTO = new DepartmentDto()
        {
            Id = id,
            Name = updateModel.Name,
            Description = updateModel.Description,
        };
        var department = _departmentService.GetDepartmentByIdAsync(id);
        if (!ModelState.IsValid)
        {
            await _departmentService.UpdateDepartmentAsync(departmentDTO);
        }
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: DepartmentController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
