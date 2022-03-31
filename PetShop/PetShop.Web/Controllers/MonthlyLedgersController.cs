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
    public class MonthlyLedgersController : Controller
    {
        private readonly PetShopContext _context;

        public MonthlyLedgersController(PetShopContext context)
        {
            _context = context;
        }

        // GET: MonthlyLedgers
        public async Task<IActionResult> Index()
        {
            return View(await _context.MonthlyLedgers.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Year,Month,Income,Expenses,Total")] MonthlyLedger monthlyLedger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monthlyLedger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monthlyLedger);
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
        public async Task<IActionResult> Delete(string id)
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

        // POST: MonthlyLedgers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var monthlyLedger = await _context.MonthlyLedgers.FindAsync(id);
            _context.MonthlyLedgers.Remove(monthlyLedger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonthlyLedgerExists(string id)
        {
            return _context.MonthlyLedgers.Any(e => e.Year == id);
        }
    }
}
