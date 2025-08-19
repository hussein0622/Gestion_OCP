using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiningProductionApp.Models
{
    public class DriverPerformance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Code Performance")]
        public string PerformanceCode { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Chauffeur")]
        public string Driver { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Nombre de Voyages")]
        public decimal TotalTrips { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Tonnage Total (T)")]
        public decimal TotalTonnage { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Heures de Travail")]
        public decimal WorkingHours { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Consommation Carburant (L)")]
        public decimal FuelConsumption { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Score de Sécurité (%)")]
        public decimal SafetyScore { get; set; }

        [StringLength(200)]
        [Display(Name = "Observations")]
        public string? Observations { get; set; }

        [Display(Name = "Date de Création")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}