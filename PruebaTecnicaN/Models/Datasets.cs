using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaN.Models
{
    public class Datasets
    {
        // [Key] Indica que esta propiedad será la clave primaria en la BD
        [Key]
        public int DataSetID { get; set; }

        // Nombre del conjunto de datos
        public string DataSetName { get; set; } = null!;

        // Descripción opcional
        public string? Description { get; set; }

        // FK a Procedure
        [Required]
        public int ProcedureID { get; set; }
        [ForeignKey("ProcedureID")]
        public virtual Procedures Procedure { get; set; } = null!;

        // FK a Field
        [Required]
        public int FieldId { get; set; }
        [ForeignKey("FieldId")]
        public virtual Fields Field { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? LastModifiedDate { get; set; }

    }
}
