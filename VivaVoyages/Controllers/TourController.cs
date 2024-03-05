using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VivaVoyages.Models;

namespace VivaVoyages.Controllers
{
    public class TourController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public TourController(VivaVoyagesContext context)
        {
            _context = context;
        }

        // GET: Tour
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tours.ToListAsync());
        }

        // GET: Tour/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(d => d.Destinations) //add them de view dc cai table trong view
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // GET: Tour/Create
        public IActionResult Create()
        {
            //Note: Add selectlist
            return View();
        }

        // POST: Tour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tour tour, List<Destination> destinations)
        {
            if (ModelState.IsValid)
            {
                // Add the tour to the context
                _context.Add(tour);
                await _context.SaveChangesAsync();


                // If there are destinations provided, associate them with the tour
                if (destinations != null && destinations.Any())
                {
                    using (var context = new VivaVoyagesContext())
                    {
                        foreach (var destination in destinations)
                        {
                            // Make sure to set the TourId for each destination to the newly created tour's Id
                            destination.TourId = tour.TourId;
                            context.Add(destination);
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: Tour/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(d => d.Destinations) // Include destinations
                .FirstOrDefaultAsync(m => m.TourId == id);

            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tour tour, List<Destination> destinations)
        {
            if (id != tour.TourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update tour
                    _context.Update(tour);
                    await _context.SaveChangesAsync();

                    // Update or add destinations
                    foreach (var destination in destinations)
                    {
                        // Check if the destination already exists in the database
                        if (destination.DestinationId > 0)
                        {
                            // Update existing destination
                            _context.Update(destination);
                        }
                        else
                        {
                            // Add new destination
                            destination.TourId = tour.TourId;
                            _context.Add(destination);
                        }
                    }
                    await _context.SaveChangesAsync(); // Save changes after the loop
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.TourId))
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
            return View(tour);
        }


        // GET: Tour/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(d => d.Destinations) //add them de view dc cai table trong view
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }

            // Delete associated destinations
            var destinations = await _context.Destinations.Where(d => d.TourId == id).ToListAsync();
            if (destinations != null && destinations.Any())
            {
                _context.Destinations.RemoveRange(destinations);
            }

            // Remove the tour
            _context.Tours.Remove(tour);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return _context.Tours.Any(e => e.TourId == id);
        }
    }
}
