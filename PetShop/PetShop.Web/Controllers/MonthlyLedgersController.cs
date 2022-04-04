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
        
        private readonly IEntityRepo<MonthlyLedger> _monthlyLedgerRepo;
        private MonthlyLedgerHandler _monthlyLedgerHandler;

        public MonthlyLedgersController(IEntityRepo<MonthlyLedger> entity, MonthlyLedgerHandler monthlyLedgerHandler) 
        {
            _monthlyLedgerRepo = entity; 
            _monthlyLedgerHandler = monthlyLedgerHandler; 
        }

        // GET: MonthlyLedgers
        public async Task<IActionResult> Index()
        {
            return View(await _monthlyLedgerRepo.GetAllAsync());
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
                monthlyLedger.Income= await _monthlyLedgerHandler.GetIncome(monthlyLedger);
                monthlyLedger.Expenses=await _monthlyLedgerHandler.GetMonthlyExpenses(monthlyLedger);
                monthlyLedger.Total =_monthlyLedgerHandler.GetTotal(monthlyLedger);
                if (await _monthlyLedgerHandler.MonthlyLedgerExists(monthlyLedger))
                {
                    ViewData["ErrorMessage"] = "This Monthly Ledger already exists!";
                    return View(monthlyLedger);
                }
                await _monthlyLedgerRepo.AddAsync(monthlyLedger);
                return RedirectToAction(nameof(Index));
            }

            return View(monthlyLedgerView);
        }

       
        // GET: MonthlyLedgers/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyLedger = await _monthlyLedgerRepo.GetByIdAsync(id);
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
            await _monthlyLedgerRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
