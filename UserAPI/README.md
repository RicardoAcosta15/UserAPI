# UserApi

## Requisitos
- .NET 8
- SQL Server

## Pasos
1. Configura la cadena de conexión en `appsettings.json`
2. Ejecuta `dotnet ef migrations add Initial` y `dotnet ef database update`
3. Ejecuta `dotnet run`
4. Usa Swagger

## Validaciones
- Email: debe ser válido
- Password: mínimo 8 caracteres, 1 mayúscula y 1 número

## Errores
- Formato de respuesta: `{ "mensaje": "texto" }`