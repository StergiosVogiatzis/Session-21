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
    public class PetReportsController : Controller
    {
        private readonly PetShopContext _context;

        public PetReportsController(PetShopContext context)
        {
            _context = context;
        }

        // GET: PetReports
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetReports.ToListAsync());
        }

        // GET: PetReports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petReport = await _context.PetReports
                .FirstOrDefaultAsync(m => m.ID == id);
            if (petReport == null)
            {
                return NotFound();
            }

            return View(petReport);
        }

        // GET: PetReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Year,Month,AnimalType,TotalSold,ID")] PetReport petReport)
        {
            if (ModelState.IsValid)
            {
                petReport.ID = Guid.NewGuid();
                _context.Add(petReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petReport);
        }

        // GET: PetReports/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petReport = await _context.PetReports.FindAsync(id);
            if (petReport == null)
            {
                return NotFound();
            }
            return View(petReport);
        }

        // POST: PetReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Year,Month,AnimalType,TotalSold,ID")] PetReport petReport)
        {
            if (id != petReport.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetReportExists(petReport.ID))
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
            return View(petReport);
        }

        // GET: PetReports/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petReport = await _context.PetReports
                .FirstOrDefaultAsync(m => m.ID == id);
            if (petReport == null)
            {
                return NotFound();
            }

            return View(petReport);
        }

        // POST: PetReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var petReport = await _context.PetReports.FindAsync(id);
            _context.PetReports.Remove(petReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetReportExists(Guid id)
        {
            return _context.PetReports.Any(e => e.ID == id);
        }
    }
}
