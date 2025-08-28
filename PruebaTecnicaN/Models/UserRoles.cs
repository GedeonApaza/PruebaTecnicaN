using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaN.Models
{
    public class UserRoles
    {
        // [Key] Indica que esta propiedad será la clave primaria en la BD
        [Key]
        public int ID { get; set; }

        // FK a Users
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual Users User { get; set; } = null!;

        // FK a Roles
        [Required]
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual Roles Role { get; set; } = null!;
    }
}
