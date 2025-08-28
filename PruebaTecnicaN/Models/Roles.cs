using System.ComponentModel.DataAnnotations;


namespace PruebaTecnicaN.Models
{
    public class Roles
    {
        // [Key] Indica que esta propiedad será la clave primaria en la BD
        [Key]
        public int RoleID { get; set; }
        // El nombre del Rol
        // Se inicializa con null! porque la propiedad no acepta valores NULL.
        public string RoleName { get; set; } = null!;
        // La descripcion del Rol
        public string RoleDescription { get; set; }= null!;
    }
}
