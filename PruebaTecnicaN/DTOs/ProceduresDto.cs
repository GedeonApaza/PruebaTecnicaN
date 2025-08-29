using PruebaTecnicaN.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaN.DTOs
{
    public class ProceduresDto
    {
        public int ProcedureID { get; set; }

        public string ProcedureName { get; set; } = null!;

        public string? Description { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // FK a Users que modificó por última vez
        public int? LastModifiedUserID { get; set; }

        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }
}
