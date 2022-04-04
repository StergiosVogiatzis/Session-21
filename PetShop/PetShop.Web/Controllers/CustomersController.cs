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
    public class CustomersController : Controller
    {
        private readonly PetShopContext _context;
        private readonly IEntityRepo<Customer> _customerRepo;  

        public CustomersController(IEntityRepo<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            return View(await _customerRepo.GetAllAsync());
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Phone,TIN")] CustomerCreateViewModel customerView)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer { Name = customerView.Name, Surname = customerView.Surname, Phone = customerView.Phone, TIN = customerView.TIN };
                await _customerRepo.AddAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customerView);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepo.GetByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            var customerView = new CustomerUpdateViewModel { Name = customer.Name, Surname = customer.Surname, TIN = customer.TIN, Phone = customer.Phone };
            return View(customerView);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Surname,Phone,TIN,ID")] CustomerUpdateViewModel customerView)
        {
            if (id != customerView.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currentCustomer = await _customerRepo.GetByIdAsync(id);
                if (currentCustomer == null)
                    return BadRequest("Could not find Customer");
                currentCustomer.Name = customerView.Name;
                currentCustomer.Surname = customerView.Surname;
                currentCustomer.TIN = customerView.TIN;
                currentCustomer.Phone = customerView.Phone;
                await _customerRepo.UpdateAsync(id, currentCustomer);
                return RedirectToAction(nameof(Index));
            }

            return View(customerView);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerRepo.GetByIdAsync(id.Value);
            
            if (customer == null)
            {
                return NotFound();
            }
            var customerView = new CustomerDeleteViewModel { ID = customer.ID, Name = customer.Name, Surname = customer.Surname, Phone = customer.Phone, TIN = customer.TIN  };
            return View(customerView);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _customerRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }
    }
}
