# StudentHub.API

API desarrollada en .NET 8 para la gestión de estudiantes.

## Configuración de Base de Datos

La cadena de conexión debe configurarse mediante una **variable de entorno** por seguridad.

### Variable de Entorno Requerida

```
ConnectionStrings__DefaultConnection
```

### Configurar la Variable de Entorno

**Windows (CMD):**
```cmd
setx ConnectionStrings__DefaultConnection "server=localhost;port=3306;database=university_app;user=university_user;password=university_password;AllowPublicKeyRetrieval=True;SslMode=None;"
```

**Windows (PowerShell):**
```powershell
[System.Environment]::SetEnvironmentVariable("ConnectionStrings__DefaultConnection", "server=localhost;port=3306;database=university_app;user=university_user;password=university_password;AllowPublicKeyRetrieval=True;SslMode=None;", "User")
```

**Linux/macOS:**
```bash
export ConnectionStrings__DefaultConnection="server=localhost;port=3306;database=university_app;user=university_user;password=university_password;AllowPublicKeyRetrieval=True;SslMode=None;"
```

### Configuración en appsettings.json

Deja el archivo `appsettings.json` sin la cadena de conexión real:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
}
```

.NET automáticamente usará la variable de entorno cuando esté disponible.

## Ejecución

1. Configura la variable de entorno
2. Ejecuta `dotnet run`
3. La API estará disponible en `https://localhost:5001`

