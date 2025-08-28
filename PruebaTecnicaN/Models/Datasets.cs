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

        // [ForeignKey("Procedures")] Cada DataSet pertenece a un Procedure
        [ForeignKey("Procedures")]
        public int ProcedureID { get; set; }

        // [ForeignKey("Fields")] Cada DataSet se asocia a un Field
        [ForeignKey("Fields")]
        public int FieldID { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate
        {
            get; set;
        }
    }
}
