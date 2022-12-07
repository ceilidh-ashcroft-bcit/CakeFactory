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
    [Authorize]
    public class ToppingsController : Controller
    {
        private readonly CakeFactoryContext _context;

        public ToppingsController(CakeFactoryContext context)
        {
            _context = context;
        }

        // GET: Toppings
        public async Task<IActionResult> Index()
        {
              return View(await _context.Toppings.ToListAsync());
        }

        // GET: Toppings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Toppings == null)
            {
                return NotFound();
            }

            var topping = await _context.Toppings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // GET: Toppings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Flavor,PriceFactor,Description,IsActive")] Topping topping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topping);
        }

        // GET: Toppings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Toppings == null)
            {
                return NotFound();
            }

            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null)
            {
                return NotFound();
            }
            return View(topping);
        }

        // POST: Toppings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Flavor,PriceFactor,Description,IsActive")] Topping topping)
        {
            if (id != topping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToppingExists(topping.Id))
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
            return View(topping);
        }

        // GET: Toppings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Toppings == null)
            {
                return NotFound();
            }

            var topping = await _context.Toppings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // POST: Toppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Toppings == null)
            {
                return Problem("Entity set 'CakeFactoryContext.Toppings'  is null.");
            }
            var topping = await _context.Toppings.FindAsync(id);
            if (topping != null)
            {
                _context.Toppings.Remove(topping);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToppingExists(int id)
        {
          return _context.Toppings.Any(e => e.Id == id);
        }
    }
}
