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
    public class PetReportsController : Controller
    {
        private readonly IEntityRepo<PetReport> _petReportRepo;
        private readonly PetReportHandler _petReportHandler;
        public PetReportsController(IEntityRepo<PetReport> context , PetReportHandler petReportHandler)
        {
            _petReportHandler = petReportHandler;
            _petReportRepo = context;
        }

        // GET: PetReports
        public async Task<IActionResult> Index()
        {
            return View(await _petReportRepo.GetAllAsync());
        }

        // GET: PetReports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petReport = await _petReportRepo.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create([Bind("Year,Month,AnimalType")] PetReportCreateViewModel petReportView)
        {
            if (ModelState.IsValid)
            {
                var petReport = new PetReport { Year = petReportView.Year, Month = petReportView.Month, AnimalType = petReportView.AnimalType };
                petReport.TotalSold = _petReportHandler.TotalSold(petReport);
                if (await _petReportHandler.PetReportExists(petReport))
                {
                    ViewData["ErrorMessage"] = "This Pet Report already exists!";
                    return View(petReportView);
                }
                await _petReportRepo.AddAsync(petReport);
                return RedirectToAction(nameof(Index));
            }
            return View(petReportView);
        }

        // GET: PetReports/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petReport = await _petReportRepo.GetByIdAsync(id.Value);
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
                    await _petReportRepo.UpdateAsync(id, petReport);
                     
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!PetReportExists(petReport.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                        throw;
                    //}
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

            var petReport = await _petReportRepo.GetByIdAsync(id.Value);
            if (petReport == null)
            {
                return NotFound();
            }
            var petReportView = new PetReportDeleteViewModel { ID = petReport.ID, Year =petReport.Year, Month = petReport.Month, AnimalType = petReport.AnimalType, TotalSold = petReport.TotalSold }; 
            return View(petReportView);
        }

        // POST: PetReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            await _petReportRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PetReportExists(Guid id)
        {
           return await _petReportRepo.GetByIdAsync(id) is null ? false : true ;
        }
    }
}
