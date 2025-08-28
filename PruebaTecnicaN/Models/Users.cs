using System.ComponentModel.DataAnnotations;


namespace PruebaTecnicaN.Models
{
    public class Users
    {
        // [Key] Indica que esta propiedad será la clave primaria en la BD
        [Key]
        public int UserID { get; set; }
        //Nombre del Usuario
        // Se inicializa con null! porque la propiedad no acepta valores NULL.
        public string Username { get; set; } = null!;
        // Email del Usuario
        public string Email { get; set; } = null!;
        // Contrasena del Usuario
        public string Password { get; set; }= null!;

        // Relación 1 a N con UserRoles
        // virtual permite que EF cargue los UserRoles automáticamente cuando se accede (Lazy Loading)
        public virtual ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
