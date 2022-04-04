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
    public class PetFoodsController : Controller
    {
        private readonly PetShopContext _context;
        private readonly IEntityRepo<PetFood> _petFoodRepo;


        public PetFoodsController(IEntityRepo<PetFood> petRepo)
        {
            _petFoodRepo = petRepo;
        }

        // GET: PetFoods
        public async Task<IActionResult> Index()
        {
            return View(await _petFoodRepo.GetAllAsync());
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
        public async Task<IActionResult> Create([Bind("AnimalType,Cost,Price")] PetFoodViewModel petFoodView)
        {
            if (ModelState.IsValid)
            {
                var petFood = new PetFood
                {
                    AnimalType = petFoodView.AnimalType,
                    Cost = petFoodView.Cost,
                    Price = petFoodView.Price,
                };
                await _petFoodRepo.AddAsync(petFood);
                return RedirectToAction(nameof(Index));
            }
            return View(petFoodView);
        }

        // GET: PetFoods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petFood = await _petFoodRepo.GetByIdAsync(id.Value);
            if (petFood == null)
            {
                return NotFound();
            }
            var PetView = new PetFoodUpdateViewModel
            {
                AnimalType = petFood.AnimalType,
                Cost = petFood.Cost,
                Price = petFood.Price,
            };
            return View(PetView);
        }

        // POST: PetFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AnimalType,Cost,Price,ID")] PetFoodUpdateViewModel petFoodView)
        {
            if (id != petFoodView.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currPetFood=await _petFoodRepo.GetByIdAsync(petFoodView.ID);
                if (currPetFood == null)
                    return BadRequest("Could not find PetFood");
                currPetFood.AnimalType = petFoodView.AnimalType;
                currPetFood.Cost = petFoodView.Cost;
                currPetFood.Price = petFoodView.Price;
                await _petFoodRepo.UpdateAsync(id, currPetFood);
                return RedirectToAction(nameof(Index));
            }
            return View(petFoodView);
        }

        // GET: PetFoods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petFood = await _petFoodRepo.GetByIdAsync(id.Value);
            if (petFood == null)
            {
                return NotFound();
            }
            var petFoodView = new PetFoodDeleteViewModel
            {
                AnimalType = petFood.AnimalType,
                Price = petFood.Price,
                Cost = petFood.Cost
            };

            return View(petFoodView);
        }

        // POST: PetFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            await _petFoodRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PetFoodExists(Guid id)
        {
            return _context.PetFoods.Any(e => e.ID == id);
        }
    }
}
