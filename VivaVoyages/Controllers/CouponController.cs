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
    public class CouponController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public CouponController(VivaVoyagesContext context)
        {
            _context = context;
        }

        // GET: Coupon
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coupons.ToListAsync());
        }

        // GET: Coupon/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(m => m.CouponCode == id);
            if (coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }

        // GET: Coupon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coupon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CouponCode,Discount,DateStart,DateEnd")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                // Check if DateStart is before DateEnd
                if (coupon.DateStart >= coupon.DateEnd)
                {
                    ModelState.AddModelError("DateEnd", "End date must be after start date.");
                    return View(coupon);
                }

                _context.Add(coupon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coupon);
        }

        // GET: Coupon/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }

        // POST: Coupon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CouponCode,Discount,DateStart,DateEnd")] Coupon coupon)
        {
            if (id != coupon.CouponCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check if DateStart is before DateEnd
                if (coupon.DateStart >= coupon.DateEnd)
                {
                    ModelState.AddModelError("DateEnd", "End date must be after start date.");
                    return View(coupon);
                }
                try
                {
                    _context.Update(coupon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponExists(coupon.CouponCode))
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
            return View(coupon);
        }

        // GET: Coupon/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(m => m.CouponCode == id);
            if (coupon == null)
            {
                return NotFound();
            }

            return View(coupon);
        }

        // POST: Coupon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon != null)
            {
                _context.Coupons.Remove(coupon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouponExists(string id)
        {
            return _context.Coupons.Any(e => e.CouponCode == id);
        }
    }
}
