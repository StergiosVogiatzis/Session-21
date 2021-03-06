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
using PetShop.Web.Handlers;
using PetShop.Web.Models;

namespace PetShop.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly PetShopContext _context;
        private readonly IEntityRepo<Transaction> _transactionRepo;
        private TransactionHandler _transactionHandler;
        // private readonly TransactionHandler
        public TransactionsController(IEntityRepo<Transaction> transactionRepo, PetShopContext context)
        {
            _transactionRepo = transactionRepo;
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            
            return View(await _transactionRepo.GetAllAsync());
        }
        // GET: Transactions/Create
        public IActionResult Create()
        {
            _transactionHandler=new TransactionHandler(_context); 
            var transaction = _transactionHandler.GetAvailablePets();
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "FullName");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName");
            ViewData["PetID"] = new SelectList(transaction, "ID", "Breed");
            ViewData["PetFoodID"] = new SelectList(_context.PetFoods, "ID", "AnimalType");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,PetID,CustomerID,EmployeeID,PetFoodID,PetPrice,PetFoodQty,PetFoodPrice,TotalPrice")] TransactionCreateViewModel transactionView)
        {
            if (ModelState.IsValid)
            {
                
                var transaction = new Transaction
                {
                    Date = transactionView.Date,
                    PetID = transactionView.PetID,
                    CustomerID = transactionView.CustomerID,
                    EmployeeID = transactionView.EmployeeID,
                    PetFoodQty = transactionView.PetFoodQty,
                };
                transaction.Pet = _context.Pets.FirstOrDefault(pet => pet.ID == transactionView.PetID);
                transaction.Customer = _context.Customers.FirstOrDefault(customer => customer.ID == transactionView.CustomerID);
                transaction.Employee = _context.Employees.FirstOrDefault(emp => emp.ID == transactionView.EmployeeID);
               
                transaction.PetPrice = _context.Pets.FirstOrDefault(pet => pet.ID == transactionView.PetID).Price;
               
                _transactionHandler = new TransactionHandler(transaction, _context);
                if (!await _transactionHandler.SetPetFood())
                {
                    ViewData["ErrorMessage"] = "Could not find PetFood for the particular animal you want!";
                    return View(transactionView);
                }

                transaction.TotalPrice = _transactionHandler.GetTotalPrice();

                transaction.Pet.PetStatus = PetStatus.Sold;

                await _transactionRepo.AddAsync(transaction);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Name", transactionView.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "Name", transactionView.EmployeeID);
            ViewData["PetID"] = new SelectList(_context.Pets, "ID", "Breed", transactionView.PetID);
            
            return View(transactionView);
        }

        

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionRepo.GetByIdAsync(id.Value);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            transaction.Pet = _context.Pets.FirstOrDefault(pet => pet.ID == transaction.PetID);
            transaction.Pet.PetStatus = PetStatus.OK;
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
