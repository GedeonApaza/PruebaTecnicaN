using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaN.Models
{
    public class UserRoles
    {
        // [Key] Indica que esta propiedad será la clave primaria en la BD
        [Key]
        public int ID { get; set; }
        
        // [ForeignKey("Users")] Define que UserID es una clave foránea 
        // que apunta a la tabla Users
        [ForeignKey("Users")]
        public int UserID { get; set; }

        // [ForeignKey("Roles")] Define que UserID es una clave foránea 
        // que apunta a la tabla Roles
        [ForeignKey("Roles")]
        public int RoleID { get; set; }
    }
}
