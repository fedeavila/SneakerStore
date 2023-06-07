using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SneakerStore.Models;
using SneakerStore.ViewModel;

namespace SneakerStore.Controllers
{
    public class SneakersController : Controller
    {
        private readonly SneakerDbContext _context;

        public SneakersController(SneakerDbContext context)
        {
            _context = context;
        }

        // GET: Sneakers
        public async Task<IActionResult> Index(string filtroMarca)
        {
            IQueryable<Sneaker> query = _context.Sneaker.Include(s => s.Brand);

            //if (!string.IsNullOrEmpty(filtroMarca))
            //{
            //    query = query.Where(z =>
            //        z.Brand.BrandName.ToLower() == filtroMarca.ToLower() ||
            //        z.Model.ToLower() == filtroMarca.ToLower());
            //}

            if (!string.IsNullOrEmpty(filtroMarca))
            {
                query = query.Where(z =>
                    z.Brand.BrandName.ToLower().Contains(filtroMarca.ToLower()) ||
                    z.Model.ToLower().Contains(filtroMarca.ToLower()));
            }

            var sneakersVM = new SneakerViewModel()
            {
                sneakers = await query.ToListAsync()
            };
            return View(sneakersVM);
        }

        // GET: Sneakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sneaker == null)
            {
                return NotFound();
            }

            var sneaker = await _context.Sneaker
                .Include(s => s.Brand)
                .FirstOrDefaultAsync(m => m.SneakerId == id);
            if (sneaker == null)
            {
                return NotFound();
            }

            return View(sneaker);
        }

        // GET: Sneakers/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "BrandName");
            return View();
        }

        // POST: Sneakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SneakerId,Model,BrandId,Price")] Sneaker sneaker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sneaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "BrandName", sneaker.BrandId);
            return View(sneaker);
        }

        // GET: Sneakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sneaker == null)
            {
                return NotFound();
            }

            var sneaker = await _context.Sneaker.FindAsync(id);
            if (sneaker == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "BrandName", sneaker.BrandId);
            return View(sneaker);
        }

        // POST: Sneakers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SneakerId,Model,BrandId,Price")] Sneaker sneaker)
        {
            if (id != sneaker.SneakerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sneaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SneakerExists(sneaker.SneakerId))
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
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "BrandName", sneaker.BrandId);
            return View(sneaker);
        }

        // GET: Sneakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sneaker == null)
            {
                return NotFound();
            }

            var sneaker = await _context.Sneaker
                .Include(s => s.Brand)
                .FirstOrDefaultAsync(m => m.SneakerId == id);
            if (sneaker == null)
            {
                return NotFound();
            }

            return View(sneaker);
        }

        // POST: Sneakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sneaker == null)
            {
                return Problem("Entity set 'SneakerDbContext.Sneaker'  is null.");
            }
            var sneaker = await _context.Sneaker.FindAsync(id);
            if (sneaker != null)
            {
                _context.Sneaker.Remove(sneaker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SneakerExists(int id)
        {
            return (_context.Sneaker?.Any(e => e.SneakerId == id)).GetValueOrDefault();
        }
    }
}
