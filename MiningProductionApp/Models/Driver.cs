using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiningProductionApp.Models
{
    public class Driver
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Code Conducteur")]
        public string DriverCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nom du Conducteur")]
        public string DriverName { get; set; }

        [StringLength(50)]
        [Display(Name = "Numéro de Téléphone")]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Adresse")]
        public string? Address { get; set; }

        [StringLength(50)]
        [Display(Name = "Numéro de Licence")]
        public string? LicenseNumber { get; set; }

        [Display(Name = "Date d'Embauche")]
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Statut")]
        public string Status { get; set; } = "Actif";

        [StringLength(200)]
        [Display(Name = "Observations")]
        public string? Observations { get; set; }

        [Display(Name = "Date de Création")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
} 