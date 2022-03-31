#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.Model;

namespace PetShop.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly PetShopContext _context;

        public TransactionsController(PetShopContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var petShopContext = _context.Transactions.Include(t => t.Customer).Include(t => t.Employee).Include(t => t.Pet).Include(t => t.PetFood);
            return View(await petShopContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Employee)
                .Include(t => t.Pet)
                .Include(t => t.PetFood)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Name");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "Name");
            ViewData["PetID"] = new SelectList(_context.Pets, "ID", "Breed");
            ViewData["PetFoodID"] = new SelectList(_context.PetFoods, "ID", "ID");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,PetID,CustomerID,EmployeeID,PetFoodID,PetPrice,PetFoodQty,PetFoodPrice,TotalPrice,ID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.ID = Guid.NewGuid();
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Name", transaction.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "Name", transaction.EmployeeID);
            ViewData["PetID"] = new SelectList(_context.Pets, "ID", "Breed", transaction.PetID);
            ViewData["PetFoodID"] = new SelectList(_context.PetFoods, "ID", "ID", transaction.PetFoodID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Name", transaction.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "Name", transaction.EmployeeID);
            ViewData["PetID"] = new SelectList(_context.Pets, "ID", "Breed", transaction.PetID);
            ViewData["PetFoodID"] = new SelectList(_context.PetFoods, "ID", "ID", transaction.PetFoodID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Date,PetID,CustomerID,EmployeeID,PetFoodID,PetPrice,PetFoodQty,PetFoodPrice,TotalPrice,ID")] Transaction transaction)
        {
            if (id != transaction.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "Name", transaction.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "Name", transaction.EmployeeID);
            ViewData["PetID"] = new SelectList(_context.Pets, "ID", "Breed", transaction.PetID);
            ViewData["PetFoodID"] = new SelectList(_context.PetFoods, "ID", "ID", transaction.PetFoodID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Employee)
                .Include(t => t.Pet)
                .Include(t => t.PetFood)
                .FirstOrDefaultAsync(m => m.ID == id);
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
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(Guid id)
        {
            return _context.Transactions.Any(e => e.ID == id);
        }
    }
}
