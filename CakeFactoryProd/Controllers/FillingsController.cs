using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeFactoryProd.Data;
using CakeFactoryProd.Models;
using Microsoft.AspNetCore.Authorization;

namespace CakeFactoryProd.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class FillingsController : Controller
    {
        private readonly CakeFactoryContext _context;

        public FillingsController(CakeFactoryContext context)
        {
            _context = context;
        }

        // GET: Fillings
        public async Task<IActionResult> Index()
        {
              return View(await _context.Fillings.ToListAsync());
        }

        // GET: Fillings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fillings == null)
            {
                return NotFound();
            }

            var filling = await _context.Fillings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filling == null)
            {
                return NotFound();
            }

            return View(filling);
        }

        // GET: Fillings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fillings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Flavor,PriceFactor,Description,IsActive")] Filling filling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filling);
        }

        // GET: Fillings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fillings == null)
            {
                return NotFound();
            }

            var filling = await _context.Fillings.FindAsync(id);
            if (filling == null)
            {
                return NotFound();
            }
            return View(filling);
        }

        // POST: Fillings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Flavor,PriceFactor,Description,IsActive")] Filling filling)
        {
            if (id != filling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FillingExists(filling.Id))
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
            return View(filling);
        }

        // GET: Fillings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fillings == null)
            {
                return NotFound();
            }

            var filling = await _context.Fillings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filling == null)
            {
                return NotFound();
            }

            return View(filling);
        }

        // POST: Fillings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fillings == null)
            {
                return Problem("Entity set 'CakeFactoryContext.Fillings'  is null.");
            }
            var filling = await _context.Fillings.FindAsync(id);
            if (filling != null)
            {
                _context.Fillings.Remove(filling);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FillingExists(int id)
        {
          return _context.Fillings.Any(e => e.Id == id);
        }
    }
}
