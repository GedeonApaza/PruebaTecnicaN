namespace PruebaTecnicaN.Models
{
    // Clase raíz que representa toda la estructura del JSON.
    // Se usa para deserializar los datos desde data.json y luego
    // insertar las listas correspondientes en la base de datos SQL Server.
    public class RootObject
    {
        // Lista de usuarios
        public List<Users> Users { get; set; } = new List<Users>();
        // Lista de roles
        public List<Roles> Roles { get; set; } = new List<Roles>();
        // Relación usuario-rol
        public List<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
        // Lista Procedimiento
        public List<Procedures> Procedures { get; set; } = new List<Procedures>();
        // Lista Fiels
        public List<Fields> Fields { get; set; } = new List<Fields>();
        // Lista Datasets
        public List<Datasets> DataSets { get; set; } = new List<Datasets>();
    }
}
