# Data2Web

Generador de sitio web estático a partir de datos en SQL Server.

## Estructura

- **Data2Web.Data** → Modelos, repositorios y acceso a datos.
- **Data2Web.Logic** → Servicios, lógica de negocio, generadores (Razor, JSON).
- **Data2Web.Presentation** → Consola, plantillas Razor, assets y salida HTML.
- **Tests** → Pruebas unitarias.

## Requisitos
- .NET 8 SDK
- SQL Server (ejemplo: SQL Server Express o LocalDB)

## Configuración
1. Crear base de datos `Data2Web` en SQL Server usando el script en `/scripts/create_sqlserver.sql`.
2. Ajustar `appsettings.json` con tu cadena de conexión.
3. Ejecutar:
   ```bash
   dotnet run --project Data2Web.Presentation
