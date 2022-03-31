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
    public class PetFoodsController : Controller
    {
        private readonly PetShopContext _context;

        public PetFoodsController(PetShopContext context)
        {
            _context = context;
        }

        // GET: PetFoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetFoods.ToListAsync());
        }

        // GET: PetFoods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petFood = await _context.PetFoods
                .FirstOrDefaultAsync(m => m.ID == id);
            if (petFood == null)
            {
                return NotFound();
            }

            return View(petFood);
        }

        // GET: PetFoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalType,Cost,Price,ID")] PetFood petFood)
        {
            if (ModelState.IsValid)
            {
                petFood.ID = Guid.NewGuid();
                _context.Add(petFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petFood);
        }

        // GET: PetFoods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petFood = await _context.PetFoods.FindAsync(id);
            if (petFood == null)
            {
                return NotFound();
            }
            return View(petFood);
        }

        // POST: PetFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AnimalType,Cost,Price,ID")] PetFood petFood)
        {
            if (id != petFood.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetFoodExists(petFood.ID))
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
            return View(petFood);
        }

        // GET: PetFoods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petFood = await _context.PetFoods
                .FirstOrDefaultAsync(m => m.ID == id);
            if (petFood == null)
            {
                return NotFound();
            }

            return View(petFood);
        }

        // POST: PetFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var petFood = await _context.PetFoods.FindAsync(id);
            _context.PetFoods.Remove(petFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetFoodExists(Guid id)
        {
            return _context.PetFoods.Any(e => e.ID == id);
        }
    }
}
