Prueba Técnica .NET – API RESTful de Trazabilidad
Descripción

Esta solución implementa una API RESTful en .NET 6 para un sistema de trazabilidad, que gestiona usuarios, roles, procedimientos y datasets.
Incluye autenticación con JWT y control de acceso por roles (Admin).

Tecnologías

.NET 6

Entity Framework Core

SQL Server

JWT para autenticación y autorización

Swagger para documentación de API

Funcionalidades

Obtener todos los DataSets asociados a un UserID a través de los Procedures que ha creado o modificado.

Crear un nuevo DataSet y asociarlo a un Procedure existente y a un Field específico.

Obtener todos los DataSets de un Procedure con el tipo de Field.

Crear un Procedure (solo usuarios con rol Admin).

Autenticación y autorización mediante JWT.

Cómo ejecutar

Clonar el repositorio:

git clone https://github.com/GedeonApaza/PruebaTecnicaN.git


Abrir el proyecto en Visual Studio o VS Code.

Configurar la cadena de conexión a SQL Server en appsettings.json.

Ejecutar la aplicación (dotnet run o F5).

Abrir Swagger para probar los endpoints:

https://localhost:7098/swagger


Para endpoints protegidos, primero hacer login y usar el token JWT.

Base de datos

Migraciones automáticas al iniciar la API.

Datos iniciales cargados desde Data/datasets.json.

Notas
