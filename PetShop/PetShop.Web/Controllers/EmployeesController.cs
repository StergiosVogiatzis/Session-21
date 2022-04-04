#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.EF.Repos;
using PetShop.Model;
using PetShop.Web.Models;

namespace PetShop.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly PetShopContext _context;
        private readonly IEntityRepo<Employee> _employeeRepo;

        public EmployeesController(IEntityRepo<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _employeeRepo.GetAllAsync());
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,SallaryPerMonth,EmployeeType")] EmployeeCreateViewModel employeeView)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee { Name = employeeView.Name, Surname = employeeView.Surname, SallaryPerMonth = employeeView.SallaryPerMonth, EmployeeType = employeeView.EmployeeType };
                await _employeeRepo.AddAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeView);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepo.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeView = new EmployeeUpdateViewModel { Name = employee.Name, Surname = employee.Surname, SallaryPerMonth = employee.SallaryPerMonth, EmployeeType = employee.EmployeeType };
            return View(employeeView);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Surname,SallaryPerMonth,EmployeeType,ID")] EmployeeUpdateViewModel employeeView)
        {
            if (id != employeeView.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currentEmployee = await _employeeRepo.GetByIdAsync(id);
                if (currentEmployee == null)
                    return BadRequest("Could not find Employee");
                currentEmployee.Name = employeeView.Name;
                currentEmployee.Surname = employeeView.Surname;
                currentEmployee.SallaryPerMonth = employeeView.SallaryPerMonth;
                currentEmployee.EmployeeType = employeeView.EmployeeType;
                await _employeeRepo.UpdateAsync(id, currentEmployee);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeView);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepo.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeView = new EmployeeDeleteViewModel { ID = employee.ID, Name = employee.Name, Surname = employee.Surname, SallaryPerMonth = employee.SallaryPerMonth, EmployeeType = employee.EmployeeType};
            return View(employeeView);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _employeeRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
