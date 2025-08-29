using PruebaTecnicaN.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaN.DTOs
{
    public class DatasetsDto
    {
        public int DataSetID { get; set; }

        // Nombre del conjunto de datos
        public string DataSetName { get; set; } = null!;

        // Descripción opcional
        public string? Description { get; set; }

        public int ProcedureID { get; set; }

        // FK a Field
        public int FieldId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? LastModifiedDate { get; set; }
    }
}
