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
    public class MonthlyLedgersController : Controller
    {
        private readonly PetShopContext _context;
        private readonly IEntityRepo<MonthlyLedger> _logger;
        private MonthlyLedgerHandler _monthlyLedgerHandler;

        public MonthlyLedgersController(IEntityRepo<MonthlyLedger> entity, PetShopContext context)
        {
            _logger = entity;
            _context = context;
        }

        // GET: MonthlyLedgers
        public async Task<IActionResult> Index()
        {
            return View(await _logger.GetAllAsync());
        }

        // GET: MonthlyLedgers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyLedger = await _context.MonthlyLedgers
                .FirstOrDefaultAsync(m => m.Year == id);
            if (monthlyLedger == null)
            {
                return NotFound();
            }

            return View(monthlyLedger);
        }

        // GET: MonthlyLedgers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MonthlyLedgers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Year,Month,Income,Expenses,Total")] MonthlyLedgerCreateViewModel monthlyLedgerView)
        {
            if (ModelState.IsValid)
            {
                var monthlyLedger = new MonthlyLedger
                {
                    Month = monthlyLedgerView.Month,
                    Year = monthlyLedgerView.Year,
                };

                _monthlyLedgerHandler = new MonthlyLedgerHandler(_context, monthlyLedger);
                monthlyLedger.Income= await _monthlyLedgerHandler.GetIncome();
                monthlyLedger.Expenses=await _monthlyLedgerHandler.GetMonthlyExpenses();
                monthlyLedger.Total =_monthlyLedgerHandler.GetTotal(monthlyLedger);
                if (await _monthlyLedgerHandler.MonthlyLedgerExists(monthlyLedger))
                {
                    ViewData["ErrorMessage"] = "This Monthly Ledger already exists!";
                    return View(monthlyLedger);
                }
                await _logger.AddAsync(monthlyLedger);
                return RedirectToAction(nameof(Index));
            }

            return View(monthlyLedgerView);
        }

        // GET: MonthlyLedgers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyLedger = await _context.MonthlyLedgers.FindAsync(id);
            if (monthlyLedger == null)
            {
                return NotFound();
            }
            return View(monthlyLedger);
        }

        // POST: MonthlyLedgers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Year,Month,Income,Expenses,Total")] MonthlyLedger monthlyLedger)
        {
            if (id != monthlyLedger.Year)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monthlyLedger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonthlyLedgerExists(monthlyLedger.Year))
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
            return View(monthlyLedger);
        }

        // GET: MonthlyLedgers/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyLedger = await _logger.GetByIdAsync(id);
            if (monthlyLedger == null)
            {
                return NotFound();
            }
            var mothlyLedgerView = new MonthlyLedgerViewModel()
            {
                Year = monthlyLedger.Year,
                Month=monthlyLedger.Month,
                Income=monthlyLedger.Income,
                Expenses=monthlyLedger.Expenses,
                Total=monthlyLedger.Total,
            };
            return View(monthlyLedger);
        }

        // POST: MonthlyLedgers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _logger.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MonthlyLedgerExists(string id)
        {
            return _context.MonthlyLedgers.Any(e => e.Year == id);
        }
    }
}
