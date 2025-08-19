using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiningProductionApp.Models
{
    public class Truck
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Code Camion")]
        public string TruckCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nom du Camion")]
        public string TruckName { get; set; }

        [StringLength(50)]
        [Display(Name = "Marque")]
        public string? Brand { get; set; }

        [StringLength(50)]
        [Display(Name = "Modèle")]
        public string? Model { get; set; }

        [StringLength(20)]
        [Display(Name = "Numéro d'Immatriculation")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "Capacité (T)")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Capacity { get; set; }

        [Display(Name = "Année de Fabrication")]
        public int? ManufacturingYear { get; set; }

        [Display(Name = "Date d'Acquisition")]
        [DataType(DataType.Date)]
        public DateTime? AcquisitionDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Statut")]
        public string Status { get; set; } = "Opérationnel";

        [StringLength(200)]
        [Display(Name = "Observations")]
        public string? Observations { get; set; }

        [Display(Name = "Date de Création")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
} 