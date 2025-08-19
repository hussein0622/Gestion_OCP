using MiningProductionApp.Models;

namespace MiningProductionApp.ViewModels
{
    public class DriversTrucksViewModel
    {
        public IEnumerable<Driver> Drivers { get; set; }
        public IEnumerable<Truck> Trucks { get; set; }
    }
} 