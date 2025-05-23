# ToolBox Admin Dashboard Project

## Project Structure

- **Views/Shared/_AdminLayout.cshtml** - Main layout for admin pages with sidebar and navbar
- **Views/Admin/** - Admin views:
  - Dashboard.cshtml - Main dashboard with stats and charts
  - Users.cshtml - User management placeholder
  - Analytics.cshtml - Analytics placeholder
  - Settings.cshtml - Settings placeholder
  
- **Controllers/AdminController.cs** - Controller for admin views
- **wwwroot/css/admin-style.css** - Custom styles for the admin dashboard
- **wwwroot/js/admin-script.js** - JavaScript functions for the admin dashboard
- **wwwroot/img/logo.png** - Logo for the dashboard

## Notes

- Uses dark sidebar (#010813) and responsive layout
- Sidebar toggles on mobile and small screens
- Theme switching between light/dark modes
- Language selection (English/Spanish)
- Uses Bootstrap 5.3 and Font Awesome icons

## How to use

1. Run the application
2. Navigate to the root URL (/) which will redirect to the admin dashboard
3. Test responsiveness by resizing browser window
4. Test theme switching with the moon/sun icon in the navbar
5. Test language switching with the globe icon in the navbar
6. Test sidebar collapsing with the hamburger menu icon

# Sistema de Traducciones del Proyecto

## Cómo funciona
- NO usar IViewLocalizer (no está configurado)
- Usar atributos `data-translate-key` en elementos HTML
- Las traducciones están en `/wwwroot/js/admin-script.js`
- Se inicializa automáticamente con `initLanguage()` al cargar páginas
- Guarda preferencia en localStorage

## Implementación para nuevos módulos
1. En las vistas (.cshtml):
   ```html
   <label data-translate-key="field_name">Field Name</label>
   <button data-translate-key="save_button">Save</button>
   ```

2. En admin-script.js agregar:
   ```javascript
   en: {
       field_name: "Field Name",
       save_button: "Save"
   },
   es: {
       field_name: "Nombre del Campo",
       save_button: "Guardar"
   }
   ```

3. Asegurarse que la vista use `Layout = "_AdminLayout"`

## Convenciones de nomenclatura para claves
- módulo_elemento_tipo (ej: roles_menu, role_name, create_role_button)
- Usar minúsculas y guiones bajos
- Ser descriptivo pero conciso

# Patrones de Desarrollo Establecidos

## Estructura de ViewModels
- Usar nullable types (`string?`) para campos opcionales
- Inicializar listas en el constructor
- Agregar valores por defecto a propiedades requeridas

## Validación de campos opcionales
Para hacer un campo verdaderamente opcional:
1. No agregar `[Required]` en el ViewModel
2. Usar nullable type (`string?`)
3. Si hay problemas con validación del cliente, agregar JavaScript:
   ```javascript
   $('#FieldName').removeAttr('data-val-required');
   ```

## Estructura de Servicios
- Interfaz en `/Interfaces/IServiceName.cs`
- Implementación en `/Services/ServiceName.cs`
- Registrar en Program.cs: `builder.Services.AddScoped<IService, Service>();`
- Usar transacciones para operaciones múltiples
- Retornar bool para operaciones de éxito/fallo

## Controladores
- Inyectar servicios por constructor
- Usar ViewModels para vistas, no entidades directamente
- Implementar TempData para mensajes de feedback
- Validar permisos y reglas de negocio antes de operaciones

## Vistas CRUD
- Create/Edit: Usar formularios con ViewModels específicos
- Delete: Crear vista de confirmación con información del registro
- Index: Mostrar mensajes TempData para feedback
- Todas las vistas deben tener breadcrumbs y usar traducciones

## Manejo de Eliminación con Dependencias
- Validar dependencias antes de eliminar (ej: roles con usuarios)
- Mostrar mensajes específicos explicando por qué no se puede eliminar
- Usar condicionales en la UI para mostrar/ocultar botón de eliminar

## CSS y Estilos
- Archivos CSS específicos por módulo en `/wwwroot/css/`
- Usar variables CSS para consistencia
- Agregar efectos hover y transiciones
- Soportar modo oscuro con `[data-theme="dark"]`

## Manejo de Migraciones con Índices Únicos

### Problema
Cuando se agregan índices únicos a columnas que ya tienen datos duplicados, la migración fallará con errores como:
```
23505: could not create unique index "IX_Users_UserName"
DETAIL: Detail redacted as it may contain sensitive data. Specify 'Include Error Detail' in the connection string to include this information.
```

### Solución - Migración en Dos Pasos

1. **Primera Migración - Sin Índices Únicos**
   - Comentar temporalmente los índices únicos en ApplicationDbContext:
   ```csharp
   // COMENTADO TEMPORALMENTE para primera migración
   // modelBuilder.Entity<User>()
   //     .HasIndex(u => u.UserName)
   //     .IsUnique();
   ```
   - Crear y aplicar la migración:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

2. **Limpiar Datos Duplicados**
   - Identificar y resolver duplicados en la base de datos
   - Asegurar que no haya valores nulos en columnas requeridas

3. **Segunda Migración - Agregar Índices**
   - Descomentar los índices únicos en ApplicationDbContext
   - Crear nueva migración:
   ```bash
   dotnet ef migrations add AddUniqueIndexes
   dotnet ef database update
   ```

### Importante
- Siempre revisar los datos existentes antes de agregar restricciones únicas
- Documentar qué columnas tienen índices únicos
- Considerar scripts de limpieza de datos si es necesario