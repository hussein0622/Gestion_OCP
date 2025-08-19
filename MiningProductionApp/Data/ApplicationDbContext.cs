using Microsoft.EntityFrameworkCore;
using MiningProductionApp.Models;

namespace MiningProductionApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Quality> Qualities { get; set; }
        public DbSet<TruckStop> TruckStops { get; set; }
        public DbSet<DriverPerformance> DriverPerformances { get; set; }
        public DbSet<TruckPerformance> TruckPerformances { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Truck> Trucks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Trip entity
            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TripNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Vehicle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Driver).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ProductionZone).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Tonnage).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Observations).HasMaxLength(200);
            });

            // Configure Quality entity
            modelBuilder.Entity<Quality>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SampleNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ProductionZone).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Grade).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Moisture).HasColumnType("decimal(10,2)");
                entity.Property(e => e.OreType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Observations).HasMaxLength(200);
            });

            // Configure TruckStop entity
            modelBuilder.Entity<TruckStop>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StopNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Vehicle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Driver).IsRequired().HasMaxLength(100);
                entity.Property(e => e.StopType).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Duration).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Observations).HasMaxLength(200);
            });

            // Configure DriverPerformance entity
            modelBuilder.Entity<DriverPerformance>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PerformanceCode).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Driver).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TotalTrips).HasColumnType("decimal(10,2)");
                entity.Property(e => e.TotalTonnage).HasColumnType("decimal(10,2)");
                entity.Property(e => e.WorkingHours).HasColumnType("decimal(10,2)");
                entity.Property(e => e.FuelConsumption).HasColumnType("decimal(10,2)");
                entity.Property(e => e.SafetyScore).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Observations).HasMaxLength(200);
            });

            // Configure TruckPerformance entity
            modelBuilder.Entity<TruckPerformance>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PerformanceCode).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Vehicle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TotalTrips).HasColumnType("decimal(10,2)");
                entity.Property(e => e.TotalTonnage).HasColumnType("decimal(10,2)");
                entity.Property(e => e.OperatingHours).HasColumnType("decimal(10,2)");
                entity.Property(e => e.MaintenanceHours).HasColumnType("decimal(10,2)");
                entity.Property(e => e.FuelConsumption).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Availability).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Observations).HasMaxLength(200);
            });

            // Seed data
            modelBuilder.Entity<Trip>().HasData(
                new Trip
                {
                    Id = 1,
                    TripNumber = "V001",
                    Date = DateTime.Now.AddDays(-5),
                    Vehicle = "Camion-001",
                    Driver = "Mohamed Alami",
                    ProductionZone = "Zone A - Carrière Nord",
                    Tonnage = 25.5m,
                    Observations = "Transport normal",
                    CreatedDate = DateTime.Now.AddDays(-5)
                },
                new Trip
                {
                    Id = 2,
                    TripNumber = "V002",
                    Date = DateTime.Now.AddDays(-3),
                    Vehicle = "Camion-002",
                    Driver = "Ahmed Bennani",
                    ProductionZone = "Zone B - Carrière Sud",
                    Tonnage = 30.0m,
                    Observations = "Livraison urgente",
                    CreatedDate = DateTime.Now.AddDays(-3)
                },
                new Trip
                {
                    Id = 3,
                    TripNumber = "V003",
                    Date = DateTime.Now.AddDays(-1),
                    Vehicle = "Camion-003",
                    Driver = "Youssef Tazi",
                    ProductionZone = "Zone A - Carrière Nord",
                    Tonnage = 28.2m,
                    Observations = "",
                    CreatedDate = DateTime.Now.AddDays(-1)
                }
            );

            // Seed Quality data
            modelBuilder.Entity<Quality>().HasData(
                new Quality
                {
                    Id = 1,
                    SampleNumber = "Q001",
                    Date = DateTime.Now.AddDays(-5),
                    ProductionZone = "Zone A - Carrière Nord",
                    Grade = 65.5m,
                    Moisture = 8.2m,
                    OreType = "Phosphate",
                    Observations = "Qualité standard",
                    CreatedDate = DateTime.Now.AddDays(-5)
                },
                new Quality
                {
                    Id = 2,
                    SampleNumber = "Q002",
                    Date = DateTime.Now.AddDays(-3),
                    ProductionZone = "Zone B - Carrière Sud",
                    Grade = 68.2m,
                    Moisture = 7.8m,
                    OreType = "Phosphate",
                    Observations = "Haute teneur",
                    CreatedDate = DateTime.Now.AddDays(-3)
                },
                new Quality
                {
                    Id = 3,
                    SampleNumber = "Q003",
                    Date = DateTime.Now.AddDays(-1),
                    ProductionZone = "Zone A - Carrière Nord",
                    Grade = 63.8m,
                    Moisture = 8.5m,
                    OreType = "Phosphate",
                    Observations = "",
                    CreatedDate = DateTime.Now.AddDays(-1)
                }
            );

            // Seed TruckStop data
            modelBuilder.Entity<TruckStop>().HasData(
                new TruckStop
                {
                    Id = 1,
                    StopNumber = "A001",
                    Date = DateTime.Now.AddDays(-5),
                    Vehicle = "Camion-001",
                    Driver = "Mohamed Alami",
                    StopType = "Maintenance Préventive",
                    StartTime = DateTime.Now.AddDays(-5).AddHours(8),
                    EndTime = DateTime.Now.AddDays(-5).AddHours(10),
                    Duration = 2.0m,
                    Observations = "Changement d'huile et filtres",
                    CreatedDate = DateTime.Now.AddDays(-5)
                },
                new TruckStop
                {
                    Id = 2,
                    StopNumber = "A002",
                    Date = DateTime.Now.AddDays(-3),
                    Vehicle = "Camion-002",
                    Driver = "Ahmed Bennani",
                    StopType = "Panne Mécanique",
                    StartTime = DateTime.Now.AddDays(-3).AddHours(13),
                    EndTime = DateTime.Now.AddDays(-3).AddHours(16),
                    Duration = 3.0m,
                    Observations = "Réparation système de freinage",
                    CreatedDate = DateTime.Now.AddDays(-3)
                },
                new TruckStop
                {
                    Id = 3,
                    StopNumber = "A003",
                    Date = DateTime.Now.AddDays(-1),
                    Vehicle = "Camion-003",
                    Driver = "Youssef Tazi",
                    StopType = "Ravitaillement",
                    StartTime = DateTime.Now.AddDays(-1).AddHours(10),
                    EndTime = DateTime.Now.AddDays(-1).AddHours(10.5),
                    Duration = 0.5m,
                    Observations = "Plein de carburant",
                    CreatedDate = DateTime.Now.AddDays(-1)
                }
            );

            // Seed DriverPerformance data
            modelBuilder.Entity<DriverPerformance>().HasData(
                new DriverPerformance
                {
                    Id = 1,
                    PerformanceCode = "DP001",
                    Date = DateTime.Now.AddDays(-5),
                    Driver = "Mohamed Alami",
                    TotalTrips = 12,
                    TotalTonnage = 350.5m,
                    WorkingHours = 8.5m,
                    FuelConsumption = 120.5m,
                    SafetyScore = 95.5m,
                    Observations = "Excellent rendement",
                    CreatedDate = DateTime.Now.AddDays(-5)
                },
                new DriverPerformance
                {
                    Id = 2,
                    PerformanceCode = "DP002",
                    Date = DateTime.Now.AddDays(-3),
                    Driver = "Ahmed Bennani",
                    TotalTrips = 10,
                    TotalTonnage = 280.0m,
                    WorkingHours = 8.0m,
                    FuelConsumption = 115.0m,
                    SafetyScore = 92.0m,
                    Observations = "Bon rendement",
                    CreatedDate = DateTime.Now.AddDays(-3)
                }
            );

            // Seed TruckPerformance data
            modelBuilder.Entity<TruckPerformance>().HasData(
                new TruckPerformance
                {
                    Id = 1,
                    PerformanceCode = "TP001",
                    Date = DateTime.Now.AddDays(-5),
                    Vehicle = "Camion-001",
                    TotalTrips = 15,
                    TotalTonnage = 420.5m,
                    OperatingHours = 10.5m,
                    MaintenanceHours = 2.0m,
                    FuelConsumption = 150.5m,
                    Availability = 98.5m,
                    Observations = "Performance optimale",
                    CreatedDate = DateTime.Now.AddDays(-5)
                },
                new TruckPerformance
                {
                    Id = 2,
                    PerformanceCode = "TP002",
                    Date = DateTime.Now.AddDays(-3),
                    Vehicle = "Camion-002",
                    TotalTrips = 12,
                    TotalTonnage = 380.0m,
                    OperatingHours = 9.5m,
                    MaintenanceHours = 1.5m,
                    FuelConsumption = 140.0m,
                    Availability = 96.0m,
                    Observations = "Maintenance préventive effectuée",
                    CreatedDate = DateTime.Now.AddDays(-3)
                }
            );

            // Seed data for Driver
            modelBuilder.Entity<Driver>().HasData(
                new Driver
                {
                    DriverCode = "DR001",
                    DriverName = "Mohamed Alami",
                    PhoneNumber = "0612345678",
                    Address = "123 Rue Hassan II, Casablanca",
                    LicenseNumber = "LIC001",
                    HireDate = DateTime.Today.AddYears(-2),
                    Status = "Actif",
                    Observations = "Conducteur expérimenté, excellent dossier de sécurité",
                    CreatedDate = DateTime.Now.AddDays(-30)
                },
                new Driver
                {
                    DriverCode = "DR002",
                    DriverName = "Ahmed Benjelloun",
                    PhoneNumber = "0623456789",
                    Address = "456 Avenue Mohammed V, Rabat",
                    LicenseNumber = "LIC002",
                    HireDate = DateTime.Today.AddYears(-1),
                    Status = "Actif",
                    Observations = "Nouveau conducteur, formation en cours",
                    CreatedDate = DateTime.Now.AddDays(-20)
                },
                new Driver
                {
                    DriverCode = "DR003",
                    DriverName = "Fatima Zahra",
                    PhoneNumber = "0634567890",
                    Address = "789 Boulevard Mohammed VI, Marrakech",
                    LicenseNumber = "LIC003",
                    HireDate = DateTime.Today.AddMonths(-6),
                    Status = "Actif",
                    Observations = "Conductrice qualifiée, spécialisée en transport lourd",
                    CreatedDate = DateTime.Now.AddDays(-15)
                }
            );

            // Seed data for Truck
            modelBuilder.Entity<Truck>().HasData(
                new Truck
                {
                    TruckCode = "TR001",
                    TruckName = "Camion-001",
                    Brand = "Volvo",
                    Model = "FH16",
                    RegistrationNumber = "12345-A-6",
                    Capacity = 25.0m,
                    ManufacturingYear = 2020,
                    AcquisitionDate = DateTime.Today.AddYears(-2),
                    Status = "Opérationnel",
                    Observations = "Camion principal, excellent état mécanique",
                    CreatedDate = DateTime.Now.AddDays(-30)
                },
                new Truck
                {
                    TruckCode = "TR002",
                    TruckName = "Camion-002",
                    Brand = "Scania",
                    Model = "R500",
                    RegistrationNumber = "67890-B-6",
                    Capacity = 30.0m,
                    ManufacturingYear = 2021,
                    AcquisitionDate = DateTime.Today.AddYears(-1),
                    Status = "Opérationnel",
                    Observations = "Camion de secours, utilisé pour les charges lourdes",
                    CreatedDate = DateTime.Now.AddDays(-25)
                },
                new Truck
                {
                    TruckCode = "TR003",
                    TruckName = "Camion-003",
                    Brand = "Mercedes-Benz",
                    Model = "Actros",
                    RegistrationNumber = "11111-C-6",
                    Capacity = 20.0m,
                    ManufacturingYear = 2019,
                    AcquisitionDate = DateTime.Today.AddMonths(-18),
                    Status = "Maintenance",
                    Observations = "En maintenance préventive, retour prévu dans 2 jours",
                    CreatedDate = DateTime.Now.AddDays(-20)
                }
            );
        }
    }
}