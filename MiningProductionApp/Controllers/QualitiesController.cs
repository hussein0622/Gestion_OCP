using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiningProductionApp.Data;
using MiningProductionApp.Models;

namespace MiningProductionApp.Controllers
{
    public class QualitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QualitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Qualities
        public async Task<IActionResult> Index()
        {
            var qualities = await _context.Qualities
                .OrderByDescending(q => q.Date)
                .ToListAsync();

            ViewBag.TotalSamples = qualities.Count;
            ViewBag.AverageGrade = qualities.Any() ? qualities.Average(q => q.Grade) : 0;
            ViewBag.AverageMoisture = qualities.Any() ? qualities.Average(q => q.Moisture) : 0;

            return View(qualities);
        }

        // GET: Qualities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quality = await _context.Qualities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quality == null)
            {
                return NotFound();
            }

            return View(quality);
        }

        // GET: Qualities/Create
        public IActionResult Create()
        {
            var quality = new Quality
            {
                Date = DateTime.Today,
                SampleNumber = GenerateNextSampleNumber()
            };
            return View(quality);
        }

        // POST: Qualities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SampleNumber,Date,ProductionZone,Grade,Moisture,OreType,Observations")] Quality quality)
        {
            if (ModelState.IsValid)
            {
                quality.CreatedDate = DateTime.Now;
                _context.Add(quality);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Échantillon créé avec succès!";
                return RedirectToAction(nameof(Index));
            }
            return View(quality);
        }

        // GET: Qualities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quality = await _context.Qualities.FindAsync(id);
            if (quality == null)
            {
                return NotFound();
            }
            return View(quality);
        }

        // POST: Qualities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SampleNumber,Date,ProductionZone,Grade,Moisture,OreType,Observations,CreatedDate")] Quality quality)
        {
            if (id != quality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quality);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Échantillon modifié avec succès!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QualityExists(quality.Id))
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
            return View(quality);
        }

        // GET: Qualities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quality = await _context.Qualities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quality == null)
            {
                return NotFound();
            }

            return View(quality);
        }

        // POST: Qualities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quality = await _context.Qualities.FindAsync(id);
            if (quality != null)
            {
                _context.Qualities.Remove(quality);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Échantillon supprimé avec succès!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool QualityExists(int id)
        {
            return _context.Qualities.Any(e => e.Id == id);
        }

        private string GenerateNextSampleNumber()
        {
            var lastQuality = _context.Qualities
                .OrderByDescending(q => q.Id)
                .FirstOrDefault();

            if (lastQuality == null)
            {
                return "Q001";
            }

            var lastNumber = lastQuality.SampleNumber.Substring(1);
            if (int.TryParse(lastNumber, out int number))
            {
                return $"Q{(number + 1):D3}";
            }

            return "Q001";
        }
    }
}