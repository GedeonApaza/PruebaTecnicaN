using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaN.Models
{
    public class Procedures
    {
        // [Key] Indica que esta propiedad será la clave primaria en la BD
        [Key]
        public int ProcedureID { get; set; }

        // Nombre del procedimiento
        [Required]
        public string ProcedureName { get; set; } = null!;

        // Descripción del procedimiento
        public string? Description { get; set; }

        // Para saber Usuario que creó este procedimiento
        [ForeignKey("Users")]
        public int CreatedByUserID { get; set; }

        public DateTime CreatedDate { get; set; }

        // Para saber que Usuario lo modificó por última vez
        [ForeignKey("Users")]
        public int? LastModifiedUserID { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
