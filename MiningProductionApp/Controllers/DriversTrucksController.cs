using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiningProductionApp.Data;
using MiningProductionApp.Models;
using MiningProductionApp.ViewModels;

namespace MiningProductionApp.Controllers
{
    public class DriversTrucksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DriversTrucksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DriversTrucks
        public async Task<IActionResult> Index()
        {
            var drivers = await _context.Drivers
                .OrderBy(d => d.DriverCode)
                .ToListAsync();

            var trucks = await _context.Trucks
                .OrderBy(t => t.TruckCode)
                .ToListAsync();

            // Statistics
            ViewBag.TotalDrivers = drivers.Count;
            ViewBag.ActiveDrivers = drivers.Count(d => d.Status == "Actif");
            ViewBag.TotalTrucks = trucks.Count;
            ViewBag.OperationalTrucks = trucks.Count(t => t.Status == "Opérationnel");

            var viewModel = new DriversTrucksViewModel
            {
                Drivers = drivers,
                Trucks = trucks
            };

            return View(viewModel);
        }

        // GET: DriversTrucks/CreateDriver
        public IActionResult CreateDriver()
        {
            var driver = new Driver
            {
                DriverCode = GenerateNextDriverCode(),
                Status = "Actif"
            };
            return View(driver);
        }

        // POST: DriversTrucks/CreateDriver
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDriver([Bind("DriverCode,DriverName,PhoneNumber,Address,LicenseNumber,HireDate,Status,Observations")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                if (await DriverExists(driver.DriverCode))
                {
                    ModelState.AddModelError("DriverCode", "Ce code conducteur existe déjà.");
                    return View(driver);
                }

                driver.CreatedDate = DateTime.Now;
                _context.Add(driver);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Conducteur créé avec succès!";
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }

        // GET: DriversTrucks/EditDriver/DR001
        public async Task<IActionResult> EditDriver(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        // POST: DriversTrucks/EditDriver/DR001
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDriver(string id, [Bind("DriverCode,DriverName,PhoneNumber,Address,LicenseNumber,HireDate,Status,Observations,CreatedDate")] Driver driver)
        {
            if (id != driver.DriverCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Conducteur modifié avec succès!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.DriverCode).Result)
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
            return View(driver);
        }

        // GET: DriversTrucks/DeleteDriver/DR001
        public async Task<IActionResult> DeleteDriver(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.DriverCode == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: DriversTrucks/DeleteDriver/DR001
        [HttpPost, ActionName("DeleteDriver")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDriverConfirmed(string id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver != null)
            {
                _context.Drivers.Remove(driver);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Conducteur supprimé avec succès!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: DriversTrucks/CreateTruck
        public IActionResult CreateTruck()
        {
            var truck = new Truck
            {
                TruckCode = GenerateNextTruckCode(),
                Status = "Opérationnel"
            };
            return View(truck);
        }

        // POST: DriversTrucks/CreateTruck
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTruck([Bind("TruckCode,TruckName,Brand,Model,RegistrationNumber,Capacity,ManufacturingYear,AcquisitionDate,Status,Observations")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                if (await TruckExists(truck.TruckCode))
                {
                    ModelState.AddModelError("TruckCode", "Ce code camion existe déjà.");
                    return View(truck);
                }

                truck.CreatedDate = DateTime.Now;
                _context.Add(truck);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Camion créé avec succès!";
                return RedirectToAction(nameof(Index));
            }
            return View(truck);
        }

        // GET: DriversTrucks/EditTruck/TR001
        public async Task<IActionResult> EditTruck(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }
            return View(truck);
        }

        // POST: DriversTrucks/EditTruck/TR001
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTruck(string id, [Bind("TruckCode,TruckName,Brand,Model,RegistrationNumber,Capacity,ManufacturingYear,AcquisitionDate,Status,Observations,CreatedDate")] Truck truck)
        {
            if (id != truck.TruckCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truck);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Camion modifié avec succès!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckExists(truck.TruckCode).Result)
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
            return View(truck);
        }

        // GET: DriversTrucks/DeleteTruck/TR001
        public async Task<IActionResult> DeleteTruck(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks
                .FirstOrDefaultAsync(m => m.TruckCode == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // POST: DriversTrucks/DeleteTruck/TR001
        [HttpPost, ActionName("DeleteTruck")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTruckConfirmed(string id)
        {
            var truck = await _context.Trucks.FindAsync(id);
            if (truck != null)
            {
                _context.Trucks.Remove(truck);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Camion supprimé avec succès!";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DriverExists(string id)
        {
            return await _context.Drivers.AnyAsync(e => e.DriverCode == id);
        }

        private async Task<bool> TruckExists(string id)
        {
            return await _context.Trucks.AnyAsync(e => e.TruckCode == id);
        }

        private string GenerateNextDriverCode()
        {
            var lastDriver = _context.Drivers
                .OrderByDescending(d => d.DriverCode)
                .FirstOrDefault();

            if (lastDriver == null)
            {
                return "DR001";
            }

            var lastNumber = lastDriver.DriverCode.Substring(2);
            if (int.TryParse(lastNumber, out int number))
            {
                return $"DR{(number + 1):D3}";
            }

            return "DR001";
        }

        private string GenerateNextTruckCode()
        {
            var lastTruck = _context.Trucks
                .OrderByDescending(t => t.TruckCode)
                .FirstOrDefault();

            if (lastTruck == null)
            {
                return "TR001";
            }

            var lastNumber = lastTruck.TruckCode.Substring(2);
            if (int.TryParse(lastNumber, out int number))
            {
                return $"TR{(number + 1):D3}";
            }

            return "TR001";
        }
    }
} 