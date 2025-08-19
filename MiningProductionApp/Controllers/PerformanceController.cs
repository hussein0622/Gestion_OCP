using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiningProductionApp.Data;
using MiningProductionApp.Models;
using MiningProductionApp.ViewModels;

namespace MiningProductionApp.Controllers
{
    public class PerformanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Performance
        public async Task<IActionResult> Index()
        {
            var driverPerformances = await _context.DriverPerformances
                .OrderByDescending(d => d.Date)
                .ToListAsync();

            var truckPerformances = await _context.TruckPerformances
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            // Driver Performance Statistics
            ViewBag.TotalDrivers = driverPerformances.Select(d => d.Driver).Distinct().Count();
            ViewBag.TotalDriverTrips = driverPerformances.Sum(d => d.TotalTrips);
            ViewBag.AverageDriverTonnage = driverPerformances.Any() ? driverPerformances.Average(d => d.TotalTonnage) : 0;
            ViewBag.AverageSafetyScore = driverPerformances.Any() ? driverPerformances.Average(d => d.SafetyScore) : 0;

            // Truck Performance Statistics
            ViewBag.TotalTrucks = truckPerformances.Select(t => t.Vehicle).Distinct().Count();
            ViewBag.TotalTruckTrips = truckPerformances.Sum(t => t.TotalTrips);
            ViewBag.AverageTruckTonnage = truckPerformances.Any() ? truckPerformances.Average(t => t.TotalTonnage) : 0;
            ViewBag.AverageAvailability = truckPerformances.Any() ? truckPerformances.Average(t => t.Availability) : 0;

            var viewModel = new CombinedPerformanceViewModel
            {
                DriverPerformances = driverPerformances,
                TruckPerformances = truckPerformances
            };

            return View(viewModel);
        }

        // GET: Performance/DriverDetails/5
        public async Task<IActionResult> DriverDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverPerformance = await _context.DriverPerformances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverPerformance == null)
            {
                return NotFound();
            }

            return View(driverPerformance);
        }

        // GET: Performance/TruckDetails/5
        public async Task<IActionResult> TruckDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckPerformance = await _context.TruckPerformances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truckPerformance == null)
            {
                return NotFound();
            }

            return View(truckPerformance);
        }

        // GET: Performance/CreateDriver
        public IActionResult CreateDriver()
        {
            var performance = new DriverPerformance
            {
                Date = DateTime.Today,
                PerformanceCode = GenerateNextDriverPerformanceCode()
            };
            return View(performance);
        }

        // POST: Performance/CreateDriver
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDriver([Bind("PerformanceCode,Date,Driver,TotalTrips,TotalTonnage,WorkingHours,FuelConsumption,SafetyScore,Observations")] DriverPerformance performance)
        {
            if (ModelState.IsValid)
            {
                performance.CreatedDate = DateTime.Now;
                _context.Add(performance);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Performance conducteur créée avec succès!";
                return RedirectToAction(nameof(Index));
            }
            return View(performance);
        }

        // GET: Performance/CreateTruck
        public IActionResult CreateTruck()
        {
            var performance = new TruckPerformance
            {
                Date = DateTime.Today,
                PerformanceCode = GenerateNextTruckPerformanceCode()
            };
            return View(performance);
        }

        // POST: Performance/CreateTruck
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTruck([Bind("PerformanceCode,Date,Vehicle,TotalTrips,TotalTonnage,OperatingHours,MaintenanceHours,FuelConsumption,Availability,Observations")] TruckPerformance performance)
        {
            if (ModelState.IsValid)
            {
                performance.CreatedDate = DateTime.Now;
                _context.Add(performance);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Performance camion créée avec succès!";
                return RedirectToAction(nameof(Index));
            }
            return View(performance);
        }

        private string GenerateNextDriverPerformanceCode()
        {
            var lastPerformance = _context.DriverPerformances
                .OrderByDescending(d => d.Id)
                .FirstOrDefault();

            if (lastPerformance == null)
            {
                return "DP001";
            }

            var lastNumber = lastPerformance.PerformanceCode.Substring(2);
            if (int.TryParse(lastNumber, out int number))
            {
                return $"DP{(number + 1):D3}";
            }

            return "DP001";
        }

        private string GenerateNextTruckPerformanceCode()
        {
            var lastPerformance = _context.TruckPerformances
                .OrderByDescending(t => t.Id)
                .FirstOrDefault();

            if (lastPerformance == null)
            {
                return "TP001";
            }

            var lastNumber = lastPerformance.PerformanceCode.Substring(2);
            if (int.TryParse(lastNumber, out int number))
            {
                return $"TP{(number + 1):D3}";
            }

            return "TP001";
        }
    }
}