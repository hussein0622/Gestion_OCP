using MiningProductionApp.Models;

namespace MiningProductionApp.ViewModels
{
    public class CombinedPerformanceViewModel
    {
        public IEnumerable<DriverPerformance> DriverPerformances { get; set; }
        public IEnumerable<TruckPerformance> TruckPerformances { get; set; }
    }
}