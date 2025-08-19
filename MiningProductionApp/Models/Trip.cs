using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiningProductionApp.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Numéro de Voyage")]
        public string TripNumber { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Véhicule")]
        public string Vehicle { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Chauffeur")]
        public string Driver { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Zone de Production")]
        public string ProductionZone { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Tonnage (T)")]
        public decimal Tonnage { get; set; }

        [StringLength(200)]
        [Display(Name = "Observations")]
        public string? Observations { get; set; }

        [Display(Name = "Date de Création")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}