using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaN.Models
{
    public class Fields
    {
        // [Key] Indica que esta propiedad será la clave primaria en la BD
        [Key]
        public int FieldID { get; set; }

        // Nombre del campo
        public string FieldName { get; set; } = null!;

        // Tipo de dato del campo 
        public string DataType { get; set; } = null!;

        // Relación 1 a N con Datasets
        public virtual ICollection<Datasets> DataSets { get; set; } = new List<Datasets>();
    }
}
