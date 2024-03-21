using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VivaVoyages.Filters;
using VivaVoyages.Models;

namespace VivaVoyages.Controllers
{
[ServiceFilter(typeof(AdminLoginFilter))]
    public class DestinationController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public DestinationController(VivaVoyagesContext context)
        {
            _context = context;
        }

        // GET: Destination
        public async Task<IActionResult> Index()
        {
            var vivaVoyagesContext = _context.Destinations.Include(d => d.Place).Include(d => d.Tour);
            return View(await vivaVoyagesContext.ToListAsync());
        }

            
        public IActionResult CreateAndReturn(int? tourId )
        {
            ViewData["Place"] = _context.Places.ToList();
            ViewData["TourId"] = tourId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAndReturn([Bind("DestinationId,PlaceId,TourId,Description,DateVisit")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destination);
                await _context.SaveChangesAsync();
                 return RedirectToAction("Edit", "Tour", new { id = destination.TourId });
            }
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceId", destination.PlaceId);
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourId", destination.TourId);
            return View(destination);
        }

        

        

        public async Task<IActionResult> EditAndReturn(int? id, int? tourId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceId", destination.PlaceId);
            ViewData["TourId"] = tourId; // Pass the TourId to the view
            return View(destination);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAndReturn(int id, [Bind("DestinationId,PlaceId,TourId,Description,DateVisit")] Destination destination)
        {
            if (id != destination.DestinationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinationExists(destination.DestinationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirect to tour edit view with the TourId
                return RedirectToAction("Edit", "Tour", new { id = destination.TourId });
            }
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceId", destination.PlaceId);
            return View(destination);
        }

        public async Task<IActionResult> DeleteAndReturn(int? id, int? tourId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations
                .Include(d => d.Place)
                .Include(d => d.Tour)
                .FirstOrDefaultAsync(m => m.DestinationId == id);
            if (destination == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = tourId; // Pass the TourId to the view
            return View(destination);
        }



        // POST: Destination/Delete/5
        [HttpPost, ActionName("DeleteAndReturn")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            var returnId = destination.TourId;
            if (destination != null)
            {
                _context.Destinations.Remove(destination);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Tour", new { id = returnId});
        }

        private bool DestinationExists(int id)
        {
            return _context.Destinations.Any(e => e.DestinationId == id);
        }
    }
}
