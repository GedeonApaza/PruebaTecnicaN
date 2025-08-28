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

        // FK a Users que creó el procedimiento
        [Required]
        public int CreatedByUserID { get; set; }
        [ForeignKey("CreatedByUserID")]
        public virtual Users CreatedByUser { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // FK a Users que modificó por última vez
        public int? LastModifiedUserID { get; set; }
        [ForeignKey("LastModifiedUserID")]
        public virtual Users? LastModifiedUser { get; set; }

        public DateTime LastModifiedDate { get; set; } = DateTime.Now;

        // Relación 1 a N con los datasets
        public virtual ICollection<Datasets> DataSets { get; set; } = new List<Datasets>();
    }
}
