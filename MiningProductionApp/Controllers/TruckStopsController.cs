using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiningProductionApp.Data;
using MiningProductionApp.Models;

namespace MiningProductionApp.Controllers
{
    public class TruckStopsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TruckStopsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TruckStops
        public async Task<IActionResult> Index()
        {
            var truckStops = await _context.TruckStops
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            ViewBag.TotalStops = truckStops.Count;
            ViewBag.TotalDuration = truckStops.Sum(t => t.Duration);
            ViewBag.AverageDuration = truckStops.Any() ? truckStops.Average(t => t.Duration) : 0;

            return View(truckStops);
        }

        // GET: TruckStops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckStop = await _context.TruckStops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckStop == null)
            {
                return NotFound();
            }

            return View(truckStop);
        }

        // GET: TruckStops/Create
        public IActionResult Create()
        {
            var truckStop = new TruckStop
            {
                Date = DateTime.Today,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                StopNumber = GenerateNextStopNumber()
            };
            return View(truckStop);
        }

        // POST: TruckStops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StopNumber,Date,Vehicle,Driver,StopType,StartTime,EndTime,Duration,Observations")] TruckStop truckStop)
        {
            if (ModelState.IsValid)
            {
                truckStop.CreatedDate = DateTime.Now;
                _context.Add(truckStop);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Arrêt créé avec succès!";
                return RedirectToAction(nameof(Index));
            }
            return View(truckStop);
        }

        // GET: TruckStops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckStop = await _context.TruckStops.FindAsync(id);
            if (truckStop == null)
            {
                return NotFound();
            }
            return View(truckStop);
        }

        // POST: TruckStops/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StopNumber,Date,Vehicle,Driver,StopType,StartTime,EndTime,Duration,Observations,CreatedDate")] TruckStop truckStop)
        {
            if (id != truckStop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckStop);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Arrêt modifié avec succès!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckStopExists(truckStop.Id))
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
            return View(truckStop);
        }

        // GET: TruckStops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckStop = await _context.TruckStops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckStop == null)
            {
                return NotFound();
            }

            return View(truckStop);
        }

        // POST: TruckStops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var truckStop = await _context.TruckStops.FindAsync(id);
            if (truckStop != null)
            {
                _context.TruckStops.Remove(truckStop);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Arrêt supprimé avec succès!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TruckStopExists(int id)
        {
            return _context.TruckStops.Any(e => e.Id == id);
        }

        private string GenerateNextStopNumber()
        {
            var lastStop = _context.TruckStops
                .OrderByDescending(t => t.Id)
                .FirstOrDefault();

            if (lastStop == null)
            {
                return "A001";
            }

            var lastNumber = lastStop.StopNumber.Substring(1);
            if (int.TryParse(lastNumber, out int number))
            {
                return $"A{(number + 1):D3}";
            }

            return "A001";
        }
    }
}