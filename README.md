# Student Hub

Sistema de gestión de estudiantes desarrollado con .NET Core 8 y Angular.

## Estructura del Proyecto

El proyecto contiene los siguientes directorios:

- **StudentHubBackend/**: Backend desarrollado en .NET Core 8
- **StudentHubWeb/**: Frontend desarrollado en Angular
- **docker-compose.yml**: Archivo para levantar la base de datos MySQL

## Requisitos Previos

- .NET Core 8 SDK
- Node.js y npm
- Visual Studio 2022 (recomendado)
- Docker Desktop (opcional, para usar la BD incluida)

## Instrucciones de Instalación

### 1. Configurar la Base de Datos

**Opción A: Usar Docker (Recomendado)**
```bash
docker compose up -d
```

**Opción B: Usar MySQL existente**
Si ya tienes MySQL instalado, asegúrate de que esté corriendo y actualiza los datos de conexión en el paso 2.

### 2. Configurar Variable de Entorno

Ejecuta el siguiente comando en PowerShell para establecer la cadena de conexión:

```powershell
setx ConnectionStrings__DefaultConnection "server=localhost;port=3306;database=student_hub_db;user=student_hub;password=student_hub_password;AllowPublicKeyRetrieval=True;SslMode=None;"
```

**⚠️ Importante**: Cierra todas las terminales después de ejecutar este comando para que la variable de entorno se actualice correctamente.

> **Nota**: Si usas una base de datos diferente a la del Docker Compose, actualiza los parámetros de conexión (server, port, database, user, password) en el comando anterior.

### 3. Configurar el Backend

1. Abre el proyecto **StudentHubBackend** en Visual Studio 2022
2. En la terminal integrada de Visual Studio, ejecuta la migración:
   ```bash
   Update-Database -Project StudentHub.Infrastructure -StartupProject StudentHub.API
   ```
3. Esto creará las tablas necesarias y cargará datos iniciales para pruebas
4. Ejecuta el proyecto backend (F5 o Ctrl+F5)

### 4. Configurar el Frontend

1. Navega a la carpeta **StudentHubWeb**
2. Si la URL del backend cambió, actualiza el archivo `environment.ts`:
   ```typescript
   apiUrl: 'http://localhost:5027'
   ```
3. Instala las dependencias:
   ```bash
   npm install
   ```
4. Ejecuta el proyecto:
   ```bash
   ng serve
   ```

### 5. Acceder a la Aplicación

1. Abre tu navegador y ve a `http://localhost:4200`
2. Haz clic en "Registrar" y completa los datos solicitados
3. Inicia sesión con la información ingresada
4. ¡Ya puedes registrar clases o darte de baja!

## Solución de Problemas

- **Error de conexión a BD**: Verifica que la variable de entorno esté configurada correctamente y que la base de datos esté corriendo
- **Error en migraciones**: Asegúrate de que Visual Studio esté ejecutándose como administrador
- **Error en frontend**: Verifica que la URL del backend en `environment.ts` coincida con la URL donde está corriendo el backend

## Tecnologías Utilizadas

- Backend: .NET Core 8, Entity Framework Core
- Frontend: Angular
- Base de Datos: MySQL
- Contenedores: Docker