using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiningProductionApp.Data;
using MiningProductionApp.Models;

namespace MiningProductionApp.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var trips = await _context.Trips
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            ViewBag.TotalTrips = trips.Count;
            ViewBag.TotalTonnage = trips.Sum(t => t.Tonnage);
            ViewBag.AverageTonnage = trips.Any() ? trips.Average(t => t.Tonnage) : 0;

            return View(trips);
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        public IActionResult Create()
        {
            var trip = new Trip
            {
                Date = DateTime.Today,
                TripNumber = GenerateNextTripNumber()
            };
            return View(trip);
        }

        // POST: Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripNumber,Date,Vehicle,Driver,ProductionZone,Tonnage,Observations")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.CreatedDate = DateTime.Now;
                _context.Add(trip);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Voyage créé avec succès!";
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TripNumber,Date,Vehicle,Driver,ProductionZone,Tonnage,Observations,CreatedDate")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Voyage modifié avec succès!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
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
            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip != null)
            {
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Voyage supprimé avec succès!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }

        private string GenerateNextTripNumber()
        {
            var lastTrip = _context.Trips
                .OrderByDescending(t => t.Id)
                .FirstOrDefault();

            if (lastTrip == null)
            {
                return "V001";
            }

            var lastNumber = lastTrip.TripNumber.Substring(1);
            if (int.TryParse(lastNumber, out int number))
            {
                return $"V{(number + 1):D3}";
            }

            return "V001";
        }
    }
}