using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiningProductionApp.Models
{
    public class Quality
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Numéro d'Échantillon")]
        public string SampleNumber { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Zone de Production")]
        public string ProductionZone { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Teneur (%)")]
        public decimal Grade { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Humidité (%)")]
        public decimal Moisture { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Type de Minerai")]
        public string OreType { get; set; }

        [StringLength(200)]
        [Display(Name = "Observations")]
        public string? Observations { get; set; }

        [Display(Name = "Date de Création")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}