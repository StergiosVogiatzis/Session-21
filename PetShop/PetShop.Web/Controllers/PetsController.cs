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
    public class PetsController : Controller
    {
        private readonly IEntityRepo<Pet> _petRepo;

        public PetsController(IEntityRepo<Pet> petRepo)
        {
            _petRepo = petRepo;
        }

        // GET: Pet
        public async Task<IActionResult> Index()
        {
            return View(await _petRepo.GetAllAsync());
        }

        // GET: Pet/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Breed,AnimalType,PetStatus,Price,Cost")] PetCreateViewModel petView)
        {
            if (ModelState.IsValid)
            {
                var pet = new Pet { Breed = petView.Breed,
                    AnimalType = petView.AnimalType,
                    PetStatus = petView.PetStatus,
                    Cost = petView.Cost,
                    Price = petView.Price };
                await _petRepo.AddAsync(pet);
                return RedirectToAction(nameof(Index));
            }
            return View(petView);
        }

        // GET: Pet/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petRepo.GetByIdAsync(id.Value);
            if (pet == null)
            {
                return NotFound();
            }
            var petView = new PetUpdateViewModel
            {
                Breed = pet.Breed,
                AnimalType = pet.AnimalType,
                PetStatus = pet.PetStatus,
                Cost = pet.Cost,
                Price = pet.Price
            };
            return View(petView);
        }

        // POST: Pet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Breed,AnimalType,PetStatus,Price,Cost,TransactionID,ID")] PetUpdateViewModel petView)
        {
            if (id != petView.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currentPet = await _petRepo.GetByIdAsync(id);
                if (currentPet == null)
                    return BadRequest("Could not find Pet");
                currentPet.Breed = petView.Breed;
                currentPet.AnimalType = petView.AnimalType;
                currentPet.PetStatus = petView.PetStatus;
                currentPet.Cost = petView.Cost;
                currentPet.Price = petView.Price;
                await _petRepo.UpdateAsync(id, currentPet);
                return RedirectToAction(nameof(Index));
            }
            return View(petView);
        }

        //GET: Pet/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _petRepo.GetByIdAsync(id.Value);
            if (pet == null)
            {
                return NotFound();
            }
            var petView = new PetDeleteViewModel {
                Breed = pet.Breed,
                AnimalType = pet.AnimalType,
                PetStatus = pet.PetStatus,
                Cost = pet.Cost,
                Price = pet.Price
            };
            return View(petView);
        }

        // POST: Pet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            await _petRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
