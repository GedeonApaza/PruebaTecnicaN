using Microsoft.EntityFrameworkCore;
using PruebaTecnicaN.Data;
using PruebaTecnicaN.Models;
using System.Data;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PruebaTecnicaNContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PruebaTecnicaNContext") ?? throw new InvalidOperationException("Connection string 'PruebaTecnicaNContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Cargar datos desde JSON al iniciar la aplicación
await SeedDataFromJson(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task SeedDataFromJson(WebApplication app)
{
    // Crear un scope para obtener los servicios necesarios (DbContext)
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<PruebaTecnicaNContext>();

    try
    {
        // Asegurar que la base de datos existe, si no existe esta se crea
        await context.Database.EnsureCreatedAsync();

        // Verificar si ya hay datos
        if (await context.Users.AnyAsync())
        {
            Console.WriteLine("La base de datos ya contiene datos. Omitiendo seeding.");
            return;
        }

        // Construir la ruta al archivo JSON
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "data.json");

        // Revisar si el archivo JSON existe
        if (!File.Exists(jsonPath))
        {
            Console.WriteLine($"Archivo JSON no encontrado en: {jsonPath}");
            return;
        }

        // Leer todo el contenido del archivo JSON
        var json = await File.ReadAllTextAsync(jsonPath);

        // Opciones de deserialización para ignorar mayúsculas/minúsculas en los nombres de propiedad
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Deserializar el JSON a un objeto RootObject que contiene todas las listas
        var seedData = JsonSerializer.Deserialize<RootObject>(json, options);

        if (seedData == null)
        {
            Console.WriteLine("Error al deserializar el JSON");
            return;
        }

        Console.WriteLine("Iniciando carga de datos con IDs específicos...");

        // 1. Insertar Users con IDENTITY_INSERT 
        if (seedData.Users?.Any() == true)
        {
            // Iniciar una transacción para asegurar que todo se haga correctamente
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // Activar IDENTITY_INSERT para la tabla Users
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users ON");

                // Insertar uno por uno para asegurar que el comando se respete
                foreach (var user in seedData.Users)
                {
                    await context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO Users (UserID, Username, Email, Password) VALUES ({0}, {1}, {2}, {3})",
                        user.UserID, user.Username, user.Email, user.Password);
                }

                // Desactivar IDENTITY_INSERT una vez terminado
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users OFF");
                // Confirmar la transacción
                await transaction.CommitAsync();
                Console.WriteLine($"Agregados {seedData.Users.Count} usuarios con IDs específicos");
            }
            catch
            {
                // Si hay error, revertir la transacción
                await transaction.RollbackAsync();
                throw;
            }
        }
        // 2. Insertar Roles con transacción 
        if (seedData.Roles?.Any() == true)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // Activar IDENTITY_INSERT para la tabla Users
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles ON");
                
                // Insertar uno por uno para asegurar que el comando se respete
                foreach (var role in seedData.Roles)
                {
                    await context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO Roles (RoleID, RoleName, Description) VALUES ({0}, {1}, {2})",
                        role.RoleID, role.RoleName, role.Description);
                }
                // Desactivar IDENTITY_INSERT una vez terminado
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles OFF");
                await transaction.CommitAsync();
                Console.WriteLine($"? Agregados {seedData.Roles.Count} roles");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // 3. Insertar Fields con transacción 
        if (seedData.Fields?.Any() == true)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // Activar IDENTITY_INSERT para la tabla Users
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Fields ON");

                // Insertar uno por uno para asegurar que el comando se respete
                foreach (var field in seedData.Fields)
                {
                    await context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO Fields (FieldID, FieldName, DataType) VALUES ({0}, {1}, {2})",
                        field.FieldID, field.FieldName, field.DataType);
                }

                // Desactivar IDENTITY_INSERT una vez terminado
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Fields OFF");
                await transaction.CommitAsync();
                Console.WriteLine($"? Agregados {seedData.Fields.Count} campos");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // 4. Procedures con transacción 
        if (seedData.Procedures?.Any() == true)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // Activar IDENTITY_INSERT para la tabla Users
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Procedures ON");

                // Insertar uno por uno para asegurar que el comando se respete
                foreach (var proc in seedData.Procedures)
                {
                    await context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO Procedures (ProcedureID, ProcedureName, Description, CreatedByUserID, CreatedDate, LastModifiedUserID, LastModifiedDate) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                        proc.ProcedureID, proc.ProcedureName, proc.Description, proc.CreatedByUserID, DateTime.Now, proc.LastModifiedUserID, DateTime.Now);
                }

                // Desactivar IDENTITY_INSERT una vez terminado
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Procedures OFF");
                await transaction.CommitAsync();
                Console.WriteLine($"? Agregados {seedData.Procedures.Count} procedimientos");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // 5. UserRoles con transacción 
        if (seedData.UserRoles?.Any() == true)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // Activar IDENTITY_INSERT para la tabla Users
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT UserRoles ON");

                // Insertar uno por uno para asegurar que el comando se respete
                foreach (var userRole in seedData.UserRoles)
                {
                    await context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO UserRoles (ID, UserID, RoleID) VALUES ({0}, {1}, {2})",
                        userRole.ID, userRole.UserID, userRole.RoleID);
                }

                // Desactivar IDENTITY_INSERT una vez terminado
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT UserRoles OFF");
                await transaction.CommitAsync();
                Console.WriteLine($"? Agregados {seedData.UserRoles.Count} relaciones usuario-rol");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // 6. DataSets con transacción
        if (seedData.DataSets?.Any() == true)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // Activar IDENTITY_INSERT para la tabla Users
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Datasets ON");

                // Insertar uno por uno para asegurar que el comando se respete
                foreach (var dataset in seedData.DataSets)
                {
                    await context.Database.ExecuteSqlRawAsync(
                        "INSERT INTO Datasets (DataSetID, DataSetName, Description, ProcedureID, FieldId, CreatedDate) VALUES ({0}, {1}, {2}, {3}, {4}, {5})",
                        dataset.DataSetID, dataset.DataSetName, dataset.Description, dataset.ProcedureID, dataset.FieldId, DateTime.Now);
                }

                // Desactivar IDENTITY_INSERT una vez terminado
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Datasets OFF");
                await transaction.CommitAsync();
                Console.WriteLine($"? Agregados {seedData.DataSets.Count} datasets");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al cargar datos desde JSON: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");

    }
    Console.WriteLine("¡Datos cargados exitosamente desde JSON con IDs específicos!");
}
   

