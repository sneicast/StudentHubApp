# StudentHub.API

## Descripci�n

Este proyecto es una API desarrollada en .NET 8 para la gesti�n de estudiantes.

## Configuraci�n de la cadena de conexi�n

Por seguridad, la cadena de conexi�n a la base de datos **no debe incluirse directamente en el archivo `appsettings.json`**. En su lugar, se debe establecer mediante una variable de entorno.

### Estructura esperada en `appsettings.json`

### Uso de variable de entorno para la cadena de conexi�n

Por seguridad, la cadena de conexi�n a la base de datos debe establecerse mediante una variable de entorno y no directamente en el archivo `appsettings.json`.

#### Nombre de la variable de entorno
ConnectionStrings__DefaultConnection
#### Ejemplo de valor
server=localhost;port=3306;database=university_app;user=university_user;password=university_password;AllowPublicKeyRetrieval=True;SslMode=None;
#### C�mo establecer la variable de entorno

- **Windows (CMD):**setx ConnectionStrings__DefaultConnection "server=localhost;port=3306;database=university_app;user=university_user;password=university_password;AllowPublicKeyRetrieval=True;SslMode=None;"
- **Windows (PowerShell):**[System.Environment]::SetEnvironmentVariable("ConnectionStrings__DefaultConnection", "server=localhost;port=3306;database=university_app;user=university_user;password=university_password;AllowPublicKeyRetrieval=True;SslMode=None;", "User")
- **Linux/macOS (bash):**export ConnectionStrings__DefaultConnection="server=localhost;port=3306;database=university_app;user=university_user;password=university_password;AllowPublicKeyRetrieval=True;SslMode=None;"
#### Notas

- .NET detecta autom�ticamente la variable de entorno y la utiliza en lugar del valor de `appsettings.json`.
- No incluyas informaci�n sensible en archivos que se suban al repositorio.
