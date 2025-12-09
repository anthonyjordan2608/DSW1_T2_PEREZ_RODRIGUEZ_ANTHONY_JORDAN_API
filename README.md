# Biblioteca Universitaria - API Backend

## Descripción
API REST para la gestión de libros y préstamos en una biblioteca universitaria, desarrollada con .NET 8 y arquitectura hexagonal.

## Tecnologías
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- MySQL (Pomelo.EntityFrameworkCore.MySql)
- AutoMapper
- Swagger/OpenAPI

## Estructura del Proyecto
-LibrarySystem/
├── src/
│ ├── Library.API/ # Capa de presentación
│ ├── Library.Application/ # Capa de aplicación (servicios, DTOs)
│ ├── Library.Domain/ # Capa de dominio (entidades, interfaces)
│ └── Library.Infrastructure/# Capa de infraestructura (repositorios, DbContext)
└── tests/ # Pruebas unitarias

## Requisitos Previos
- .NET 8 SDK
- MySQL Server 8+
- Visual Studio 2022 o VS Code

## Configuración

### 1. Base de Datos
```sql
CREATE DATABASE LibraryDB;


2. Configuración de conexión
En Library.API/appsettings.json:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=LibraryDB;User=root;Password=tu_password;SslMode=None"
  }
}

3. Migraciones de Base de Datos

powershell
# En la Consola del Administrador de Paquetes:

Add-Migration InitialCreate -Context ApplicationDbContext -OutputDir Data/Migrations
Update-Database -Context ApplicationDbContext

Ejecutar la Aplicación

Visual Studio

Abrir LibrarySystem.sln

Establecer Library.API como proyecto de inicio

Presionar F5

CLI

bash
cd src/Library.API
dotnet run

La API estará disponible en: https://localhost:7261

Documentación API

Swagger UI disponible en: https://localhost:7261/swagger

Endpoints Principales

Libros
GET /api/books - Listar todos los libros

GET /api/books/{id} - Obtener libro por ID

POST /api/books - Crear nuevo libro

PUT /api/books/{id} - Actualizar libro

DELETE /api/books/{id} - Eliminar libro

Préstamos
GET /api/loans - Listar todos los préstamos

GET /api/loans/active - Listar préstamos activos

POST /api/loans - Crear nuevo préstamo

PUT /api/loans/{id}/return - Devolver préstamo

Reglas de Negocio

-ISBN único por libro

-No se puede prestar libro con stock = 0

-Stock disminuye al prestar, aumenta al devolver

-Validación de datos en DTOs

Autor
Anthony Jordan Pérez Rodríguez
Cibertec - 5to Ciclo - DESARROLLO DE SERVICIOS WEB I

License
This project is licensed under the MIT License.