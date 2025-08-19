using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiningProductionApp.Models
{
    public class TruckStop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Numéro d'Arrêt")]
        public string StopNumber { get; set; }

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
        [Display(Name = "Type d'Arrêt")]
        public string StopType { get; set; }

        [Required]
        [Display(Name = "Heure de Début")]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Heure de Fin")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Durée (heures)")]
        public decimal Duration { get; set; }

        [StringLength(200)]
        [Display(Name = "Observations")]
        public string? Observations { get; set; }

        [Display(Name = "Date de Création")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}