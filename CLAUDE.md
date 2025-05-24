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

# Sistema de Filtros Active/Inactive con Actualizaciones en Tiempo Real

## Implementación del Patrón (Users Module)

### 1. Controlador - Configuración del Filtro
```csharp
[HttpGet]
public async Task<IActionResult> Index(string? roleFilter = null, string? statusFilter = null, string? searchTerm = null)
{
    // Por defecto mostrar usuarios activos
    statusFilter = statusFilter ?? "active";
    var users = await _userService.GetAllUsersAsync(roleFilter, statusFilter, searchTerm);
    
    // Configurar ViewBag para mantener selección
    ViewBag.CurrentStatusFilter = statusFilter ?? "active";
    ViewBag.CurrentRoleFilter = roleFilter;
    ViewBag.CurrentSearchTerm = searchTerm;
    
    return View(userViewModels);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ToggleStatus(int id)
{
    var (success, newStatusMessage, newIsActiveState) = await _userService.ToggleUserStatusAsync(id);
    
    if (success)
    {
        return Json(new { 
            success = true, 
            message = $"El estado del usuario ha sido cambiado a '{newStatusMessage}'.",
            newIsActiveState = newIsActiveState,
            newStatusMessage = newStatusMessage
        });
    }
    else
    {
        return Json(new { success = false, message = newStatusMessage });
    }
}
```

### 2. Servicio - Lógica de Filtrado
```csharp
public async Task<IEnumerable<User>> GetAllUsersAsync(string? roleFilter = null, string? statusFilter = null, string? searchTerm = null)
{
    var query = _context.Users.Include(u => u.Role).AsQueryable();

    // Filtrar por estado si se proporciona
    if (!string.IsNullOrEmpty(statusFilter))
    {
        if (statusFilter.ToLower() == "active")
        {
            query = query.Where(u => u.IsActive);
        }
        else if (statusFilter.ToLower() == "inactive")
        {
            query = query.Where(u => !u.IsActive);
        }
    }
    
    // Otros filtros...
    return await query.OrderByDescending(u => u.CreatedAt).ToListAsync();
}

public async Task<(bool Success, string NewStatusMessage, bool NewIsActiveState)> ToggleUserStatusAsync(int userId)
{
    using var transaction = await _context.Database.BeginTransactionAsync();
    try
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return (false, "Usuario no encontrado.", false);

        user.IsActive = !user.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        int changes = await _context.SaveChangesAsync();

        if (changes > 0)
        {
            await transaction.CommitAsync();
            string newStatusMessage = user.IsActive ? "Activo" : "Inactivo";
            return (true, newStatusMessage, user.IsActive);
        }
        
        await transaction.RollbackAsync();
        return (false, "No se pudo actualizar el estado del usuario.", user.IsActive);
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        return (false, "Error al actualizar el estado del usuario.", false);
    }
}
```

### 3. Vista - Select de Filtro
```html
<div class="col-md-4">
    <label class="form-label" for="selectStatus">Select Status</label>
    <select id="selectStatus" class="form-select filter-select">
        @if (ViewBag.CurrentStatusFilter == "active")
        {
            <option value="active" selected>Active</option>
            <option value="inactive">Inactive</option>
        }
        else
        {
            <option value="active">Active</option>
            <option value="inactive" selected>Inactive</option>
        }
    </select>
</div>
```

### 4. JavaScript - Actualizaciones en Tiempo Real
```javascript
document.addEventListener('DOMContentLoaded', function() {
    // Variables globales
    let currentToggleButton = null;
    const confirmModal = new bootstrap.Modal(document.getElementById('confirmStatusModal'));
    
    // Manejo de filtros
    const statusFilter = document.getElementById('selectStatus');
    function applyFilters() {
        const params = new URLSearchParams();
        const statusValue = statusFilter.value;
        if (statusValue) params.append('statusFilter', statusValue);
        window.location.href = '@Url.Action("Index", "Users")' + (params.toString() ? '?' + params.toString() : '');
    }
    
    if (statusFilter) {
        statusFilter.addEventListener('change', applyFilters);
    }
    
    // Manejo de cambio de estado con AJAX
    document.addEventListener('click', function(e) {
        if (e.target.closest('.toggle-status-btn')) {
            const button = e.target.closest('.toggle-status-btn');
            currentToggleButton = button;
            const userId = button.dataset.userId;
            const userName = button.dataset.userName;
            const action = button.dataset.action;
            
            // Configurar modal
            const message = document.getElementById('confirmStatusMessage');
            if (action === 'deactivate') {
                message.innerHTML = `¿Está seguro de que desea <strong class="text-warning">desactivar</strong> al usuario <strong>${userName}</strong>?`;
            } else {
                message.innerHTML = `¿Está seguro de que desea <strong class="text-success">activar</strong> al usuario <strong>${userName}</strong>?`;
            }
            
            document.getElementById('confirmStatusBtn').dataset.userId = userId;
            confirmModal.show();
        }
    });
    
    // Confirmación de cambio de estado
    document.getElementById('confirmStatusBtn').addEventListener('click', async function() {
        const userId = this.dataset.userId;
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        
        try {
            const formData = new URLSearchParams();
            formData.append('__RequestVerificationToken', token);
            
            const response = await fetch(`@Url.Action("ToggleStatus", "Users")/${userId}`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: formData
            });
            
            const result = await response.json();
            
            if (result.success) {
                // Obtener el filtro actual de estado
                const currentFilter = document.getElementById('selectStatus').value;
                const row = currentToggleButton.closest('tr');
                
                // Si el usuario ya no coincide con el filtro actual, remover la fila
                if ((currentFilter === 'active' && !result.newIsActiveState) || 
                    (currentFilter === 'inactive' && result.newIsActiveState)) {
                    
                    // Animar la fila antes de removerla
                    row.style.transition = 'opacity 0.3s ease-out';
                    row.style.opacity = '0';
                    
                    setTimeout(() => {
                        row.remove();
                        
                        // Verificar si ya no hay más usuarios en la tabla
                        const tbody = document.querySelector('.table tbody');
                        if (tbody.querySelectorAll('tr').length === 0) {
                            tbody.innerHTML = '<tr><td colspan="6" class="text-center">No hay usuarios ' + 
                                            (currentFilter === 'active' ? 'activos' : 'inactivos') + '</td></tr>';
                        }
                    }, 300);
                    
                } else {
                    // Actualizar el botón y el badge de estado in-place
                    const statusBadge = row.querySelector('.badge');
                    
                    if (result.newIsActiveState) {
                        currentToggleButton.className = 'btn btn-sm btn-icon text-warning toggle-status-btn';
                        currentToggleButton.title = 'Desactivar Usuario';
                        currentToggleButton.dataset.currentStatus = 'active';
                        currentToggleButton.dataset.action = 'deactivate';
                        currentToggleButton.innerHTML = '<i class="fas fa-user-slash"></i>';
                        
                        statusBadge.className = 'badge bg-success-subtle text-success-emphasis';
                        statusBadge.textContent = 'Active';
                    } else {
                        currentToggleButton.className = 'btn btn-sm btn-icon text-success toggle-status-btn';
                        currentToggleButton.title = 'Activar Usuario';
                        currentToggleButton.dataset.currentStatus = 'inactive';
                        currentToggleButton.dataset.action = 'activate';
                        currentToggleButton.innerHTML = '<i class="fas fa-user-check"></i>';
                        
                        statusBadge.className = 'badge bg-secondary-subtle text-secondary-emphasis';
                        statusBadge.textContent = 'Inactive';
                    }
                }
                
                showToast(result.message, 'success');
            } else {
                showToast(result.message, 'error');
            }
            
            confirmModal.hide();
            
        } catch (error) {
            showToast('Error al cambiar el estado del usuario', 'error');
            confirmModal.hide();
        }
    });
    
    // Toast notifications profesionales
    function showToast(message, type = 'success') {
        const toastElement = document.getElementById('notificationToast');
        const toastIcon = document.getElementById('toastIcon');
        const toastMessage = document.getElementById('toastMessage');
        
        if (type === 'success') {
            toastIcon.className = 'fas fa-check-circle text-white me-2';
            toastElement.className = 'toast align-items-center border-0 bg-success text-white';
        } else if (type === 'error') {
            toastIcon.className = 'fas fa-times-circle text-white me-2';
            toastElement.className = 'toast align-items-center border-0 bg-danger text-white';
        }
        
        toastMessage.textContent = message;
        
        const toast = new bootstrap.Toast(toastElement, {
            autohide: true,
            delay: 3000
        });
        toast.show();
    }
});
```

### 5. Toast Notifications HTML
```html
<!-- Toast para Notificaciones -->
<div class="position-fixed top-0 end-0 p-3" style="z-index: 1050">
    <div id="notificationToast" class="toast align-items-center border-0" role="alert" aria-live="assertive" aria-atomic="true" data-bs-autohide="true" data-bs-delay="3000">
        <div class="d-flex">
            <div class="toast-body">
                <i class="fas fa-check-circle me-2" id="toastIcon"></i>
                <span id="toastMessage"></span>
            </div>
        </div>
    </div>
</div>

<!-- Token CSRF para peticiones AJAX -->
@Html.AntiForgeryToken()
```

## Características Clave del Patrón

### ✅ Comportamiento del Filtro
- **Por defecto**: Muestra usuarios activos
- **Solo dos opciones**: Active/Inactive (sin "All")
- **Mantiene selección**: Al recargar página mantiene filtro seleccionado
- **URL limpia**: Parámetros en query string para bookmarkeable

### ✅ Actualizaciones en Tiempo Real
- **Sin recarga de página**: Usa AJAX para cambios de estado
- **Animaciones suaves**: Fade out de 0.3s al remover filas
- **Filtro inteligente**: Si el usuario no coincide con filtro actual, se remueve de la vista
- **Lista vacía**: Muestra mensaje contextual cuando no hay resultados

### ✅ UX Profesional
- **Toast auto-dismiss**: Se cierran automáticamente en 3 segundos
- **Posición superior**: Toast en top-right, no molesta workflow
- **Colores distintivos**: Verde para éxito, rojo para error
- **Feedback inmediato**: Usuario ve cambios instantáneamente

### ✅ Robustez Técnica
- **CSRF Protection**: Token incluido en peticiones AJAX
- **Transacciones**: Operaciones atómicas en base de datos
- **Error handling**: Manejo graceful de errores de red/servidor
- **Event delegation**: JavaScript que funciona con contenido dinámico

## Replicación en Otros Módulos

Para implementar este patrón en otros módulos (Roles, Categories, etc):

1. **Copiar estructura del controlador**: Index con filtros y ToggleStatus con JSON
2. **Adaptar servicio**: GetAllAsync con filtros y ToggleStatusAsync
3. **Reusar JavaScript**: Cambiar solo los selectores CSS y URLs
4. **Mantener HTML**: Toast notifications y modal structure igual
5. **Ajustar mensajes**: Textos específicos del dominio

Este patrón está probado y optimizado para User Management y debe replicarse exactamente en módulos similares para mantener consistencia en la aplicación.

# Implementación Completa del Módulo de Usuarios - Resumen y Lecciones Aprendidas

## 📋 Resumen General

El módulo de usuarios se implementó completamente desde cero siguiendo las mejores prácticas de ASP.NET Core MVC. Este documento resume todos los problemas encontrados, las soluciones implementadas y las lecciones aprendidas para evitar estos problemas en futuros módulos.

## 🏗️ Arquitectura Implementada

### **Capas del Sistema:**
1. **Modelo de Datos** - `Models/User.cs` con relación a `Role`
2. **Capa de Servicio** - `IUserService` e implementación `UserService` 
3. **Controlador** - `UsersController` con patrón service injection
4. **ViewModels** - DTOs específicos para cada vista
5. **Vistas** - Razor views con patrón CRUD completo
6. **Documentos** - Clases para exportación PDF

### **Patrones Utilizados:**
- ✅ **Service Layer Pattern** - Separación de lógica de negocio
- ✅ **Repository Pattern** (via Entity Framework)
- ✅ **ViewModel Pattern** - DTOs para transferencia de datos
- ✅ **Dependency Injection** - Servicios registrados en Program.cs

## 🚨 Problemas Encontrados y Soluciones

### **1. CONFIGURACIÓN DE BASE DE DATOS**

#### **Problema:** Índices únicos en datos duplicados
```
23505: could not create unique index "IX_Users_UserName"
```

#### **Solución:** Migración en dos pasos
```csharp
// Paso 1: Crear entidades sin índices únicos
// Paso 2: Limpiar datos duplicados  
// Paso 3: Agregar índices únicos en migración separada

// En ApplicationDbContext.cs
modelBuilder.Entity<User>()
    .HasIndex(u => u.UserName)
    .IsUnique();
    
modelBuilder.Entity<User>()
    .HasIndex(u => u.Email)
    .IsUnique();
```

#### **Lección:** Siempre verificar datos existentes antes de agregar restricciones únicas

### **2. INYECCIÓN DE DEPENDENCIAS**

#### **Problema:** Servicios no registrados
```
Unable to resolve service for type 'IUserService'
```

#### **Solución:** Registro correcto en Program.cs
```csharp
// En Program.cs
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
```

#### **Lección:** Registrar TODOS los servicios antes de `builder.Build()`

### **3. CAMBIOS EN EL MODELO DE DATOS**

#### **Problema:** Referencias a propiedades obsoletas
```
Property 'Name' does not exist on type 'User'
```

#### **Solución:** Actualización sistemática
```csharp
// Cambio: user.Name → user.FullName
// Archivos afectados:
// - DbSeeder.cs
// - Todas las vistas que usen User
// - ViewModels relacionados
```

#### **Lección:** Hacer refactoring completo cuando se cambian nombres de propiedades

### **4. MANEJO DE ARCHIVOS (AVATARES)**

#### **Problema:** Validación y almacenamiento de imágenes

#### **Solución Completa:**
```csharp
// En ViewModel
[Display(Name = "Avatar")]
public IFormFile? AvatarFile { get; set; }

// En Controller - Validación
if (model.AvatarFile != null && model.AvatarFile.Length > 0)
{
    // Validar tamaño (800KB max)
    if (model.AvatarFile.Length > 800 * 1024)
    {
        ModelState.AddModelError("AvatarFile", "Archivo demasiado grande.");
        return View(model);
    }
    
    // Validar tipo
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
    var fileExtension = Path.GetExtension(model.AvatarFile.FileName).ToLowerInvariant();
    if (!allowedExtensions.Contains(fileExtension))
    {
        ModelState.AddModelError("AvatarFile", "Solo imágenes permitidas.");
        return View(model);
    }
    
    // Guardar archivo
    var fileName = Guid.NewGuid().ToString() + fileExtension;
    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "avatars");
    Directory.CreateDirectory(uploadPath);
    var filePath = Path.Combine(uploadPath, fileName);
    
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await model.AvatarFile.CopyToAsync(stream);
    }
    
    newAvatarUrl = "/uploads/avatars/" + fileName;
}
```

#### **Lección:** Validar siempre tamaño, tipo y usar nombres únicos para archivos

### **5. EXPORTACIÓN DE DATOS**

#### **Problema A:** Configuración de licencia EPPlus 8+
```
LicenseContextPropertyObsoleteException: Please use the static 'ExcelPackage.License' property
```

#### **Solución:** Migración a ClosedXML
```csharp
// Reemplazo completo de EPPlus por ClosedXML
using ClosedXML.Excel;

using (var workbook = new XLWorkbook())
{
    var worksheet = workbook.Worksheets.Add("Usuarios");
    // ... implementación sin problemas de licencia
}
```

#### **Problema B:** Configuración QuestPDF
```
Please set the license using QuestPDF.Settings.License = LicenseType.Community
```

#### **Solución:** Configuración en Program.cs
```csharp
// En Program.cs
using QuestPDF.Infrastructure;
QuestPDF.Settings.License = LicenseType.Community;
```

#### **Lección:** 
- **ClosedXML** es más fácil que EPPlus (sin problemas de licencia)
- **QuestPDF** requiere configuración de licencia al inicio

### **6. FILTROS DINÁMICOS CON AJAX**

#### **Problema:** Botones de estado no funcionan después de cambios AJAX

#### **Solución:** Event delegation y actualización dinámica
```javascript
// Event delegation para elementos dinámicos
document.addEventListener('click', function(e) {
    if (e.target.closest('.toggle-status-btn')) {
        const button = e.target.closest('.toggle-status-btn');
        // ... lógica de manejo
    }
});

// Actualización dinámica de UI
if (result.success) {
    const currentFilter = document.getElementById('selectStatus').value;
    const row = currentToggleButton.closest('tr');
    
    // Si usuario no coincide con filtro, remover fila
    if ((currentFilter === 'active' && !result.newIsActiveState) || 
        (currentFilter === 'inactive' && result.newIsActiveState)) {
        row.style.transition = 'opacity 0.3s ease-out';
        row.style.opacity = '0';
        setTimeout(() => row.remove(), 300);
    } else {
        // Actualizar estado del botón dinámicamente
        updateButtonState(currentToggleButton, result.newIsActiveState);
    }
}
```

#### **Lección:** Usar event delegation para elementos que cambian dinámicamente

### **7. CALIDAD DE IMÁGENES EN TABLA**

#### **Problema:** Avatares borrosos en la tabla

#### **Solución:** CSS optimizado para nitidez
```css
.table-avatar {
    width: 48px !important;
    height: 48px !important;
    object-fit: cover;
    object-position: center;
    /* Propiedades para mejorar nitidez */
    image-rendering: optimizeQuality;
    image-rendering: crisp-edges;
    transform: translateZ(0);
    -webkit-backface-visibility: hidden;
    backface-visibility: hidden;
}
```

#### **Lección:** 
- Usar tamaño adecuado (48px es óptimo para listas)
- Aplicar propiedades CSS específicas para nitidez
- La calidad de imagen original también importa

## 🎯 Mejores Prácticas Establecidas

### **1. Estructura de Archivos**
```
/Controllers/
  - UsersController.cs
/Services/
  - UserService.cs
/Interfaces/
  - IUserService.cs
/Models/
  - User.cs
  - UserViewModels.cs
/Views/Users/
  - Index.cshtml
  - Create.cshtml
  - Edit.cshtml
  - _AddUserOffcanvas.cshtml
/Documents/
  - UserListPdfDocument.cs
/wwwroot/uploads/avatars/
  - [archivos de avatar]
```

### **2. Patrón de ViewModels**
```csharp
// Para listas
public class UserListItemViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? RoleName { get; set; }
    public bool IsActive { get; set; }
    public string? AvatarUrl { get; set; }
}

// Para crear/editar
public class UserCreateViewModel
{
    [Required]
    public string FullName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public IFormFile? AvatarFile { get; set; }
    
    public SelectList? AvailableRoles { get; set; }
}
```

### **3. Patrón de Servicios**
```csharp
public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync(string? roleFilter = null, string? statusFilter = null, string? searchTerm = null);
    Task<User?> GetUserByIdAsync(int id);
    Task<bool> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task<(bool Success, string Message, bool NewState)> ToggleUserStatusAsync(int userId);
    Task<bool> IsEmailTakenAsync(string email, int? excludeUserId = null);
    Task<bool> IsUserNameTakenAsync(string userName, int? excludeUserId = null);
}
```

### **4. Configuraciones Necesarias en Program.cs**
```csharp
// Servicios
builder.Services.AddScoped<IUserService, UserService>();

// QuestPDF (si se usa)
QuestPDF.Settings.License = LicenseType.Community;

// NO necesario para ClosedXML (sin configuración de licencia)
```

## 🚀 Checklist para Futuros Módulos

### **Antes de empezar:**
- [ ] ✅ Definir entidad con relaciones correctas
- [ ] ✅ Crear migración sin índices únicos si hay datos existentes
- [ ] ✅ Implementar interfaz de servicio
- [ ] ✅ Crear ViewModels específicos para cada vista
- [ ] ✅ Registrar servicios en Program.cs

### **Durante implementación:**
- [ ] ✅ Usar transacciones para operaciones múltiples  
- [ ] ✅ Validar archivos si hay uploads (tamaño, tipo, nombre único)
- [ ] ✅ Implementar filtros con AJAX para mejor UX
- [ ] ✅ Usar event delegation para elementos dinámicos
- [ ] ✅ Aplicar CSS optimizado para imágenes

### **Para exportación:**
- [ ] ✅ Usar ClosedXML para Excel (sin problemas de licencia)
- [ ] ✅ Configurar QuestPDF license para PDF
- [ ] ✅ Implementar clase IDocument para PDFs complejos
- [ ] ✅ Incluir filtros en exportación

### **Testing final:**
- [ ] ✅ Probar CRUD completo
- [ ] ✅ Verificar filtros y búsqueda
- [ ] ✅ Testear subida de archivos
- [ ] ✅ Verificar exportación Excel/PDF
- [ ] ✅ Confirmar responsividad
- [ ] ✅ Validar traducción si aplica

## 📊 Métricas del Módulo de Usuarios

- **Tiempo total de implementación:** ~15-20 horas de desarrollo
- **Problemas principales encontrados:** 7 problemas críticos resueltos
- **Archivos creados/modificados:** ~25 archivos
- **Funcionalidades implementadas:** 
  - ✅ CRUD completo
  - ✅ Sistema de filtros dinámicos
  - ✅ Subida de avatares
  - ✅ Exportación Excel/PDF
  - ✅ Toggle de estado con AJAX
  - ✅ Validaciones de formulario
  - ✅ Responsive design

## 🔄 Patrón Replicable

Este módulo establece el patrón estándar para todos los módulos futuros del sistema. La documentación completa permite replicar la misma calidad y evitar los problemas ya resueltos.

**Próximos módulos que seguirán este patrón:**
- Gestión de Roles (ya implementado)
- Categorías
- Productos/Servicios  
- Configuraciones
- Reportes

# Sistema de Filtros con Estado Persistente - Patrón para Módulos CRUD

## Problema Encontrado: Filtros de Estado con Toggle Dinámico

### Descripción del Problema
Al implementar filtros de estado (Active/Inactive) con toggle dinámico en el módulo de Áreas de Vida, encontramos que:

1. **Problema de Sincronización**: Cuando se desactivaba un elemento y luego se cambiaba al filtro "Inactivos", el elemento no aparecía sin recargar la página
2. **Error 404**: Al cambiar filtros, la URL se construía incorrectamente causando errores 404
3. **Inconsistencia**: El módulo funcionaba diferente a Users, creando una experiencia inconsistente

### Solución Implementada

#### 1. **Filtro de Estado del Lado del Servidor**
```csharp
// En el Controller - Agregar parámetro statusFilter
public async Task<IActionResult> Index(string? statusFilter = null)
{
    statusFilter = statusFilter ?? "active"; // Por defecto activos
    ViewBag.CurrentStatusFilter = statusFilter;
    
    var items = await _service.GetAllAsync(includeInactive: true);
    
    // Filtrar en el servidor
    if (statusFilter == "active")
        items = items.Where(x => x.IsActive);
    else if (statusFilter == "inactive")
        items = items.Where(x => !x.IsActive);
    
    return View(items);
}
```

#### 2. **Vista con Filtro Persistente**
```html
<!-- En la Vista - Select con estado actual -->
<select id="selectStatus" class="form-select filter-select">
    @if (ViewBag.CurrentStatusFilter == "active")
    {
        <option value="active" selected>Activos</option>
        <option value="inactive">Inactivos</option>
    }
    else
    {
        <option value="active">Activos</option>
        <option value="inactive" selected>Inactivos</option>
    }
</select>
```

#### 3. **JavaScript - Recarga con Parámetros**
```javascript
// IMPORTANTE: Usar window.location.pathname para evitar errores 404
if (statusFilter) {
    statusFilter.addEventListener('change', function() {
        const params = new URLSearchParams();
        const statusValue = this.value;
        
        if (statusValue) params.append('statusFilter', statusValue);
        
        // Usar pathname actual para mantener la ruta correcta
        const currentPath = window.location.pathname;
        window.location.href = currentPath + (params.toString() ? '?' + params.toString() : '');
    });
}
```

#### 4. **Toggle con Recarga Automática**
```javascript
async function toggleStatus(id, button) {
    // ... hacer petición AJAX ...
    
    if (result.success) {
        // Actualizar botón visualmente
        if (result.newIsActiveState) {
            button.className = 'btn btn-sm btn-icon text-warning toggle-status-btn';
            button.innerHTML = '<i class="fas fa-eye-slash"></i>';
            // ... actualizar otros atributos
        } else {
            button.className = 'btn btn-sm btn-icon text-success toggle-status-btn';
            button.innerHTML = '<i class="fas fa-eye"></i>';
            // ... actualizar otros atributos
        }
        
        showToast(result.message, 'success');
        
        // Recargar después de 1 segundo para sincronizar con filtros
        setTimeout(() => {
            window.location.reload();
        }, 1000);
    }
}
```

### Puntos Clave para Evitar Problemas

1. **NO usar rutas hardcodeadas**: 
   - ❌ `window.location.href = '/Module' + params`
   - ✅ `window.location.href = window.location.pathname + params`

2. **Recargar página después de toggle**: Garantiza que los filtros se apliquen correctamente

3. **Filtrar en el servidor**: No depender solo de JavaScript para filtros de estado

4. **Mantener estado en ViewBag**: Para que el select muestre la opción correcta después de recargar

### Patrón Recomendado para Nuevos Módulos

1. **Controller**: Aceptar parámetro `statusFilter` en Index
2. **Service**: Método que acepte `includeInactive` para traer todos
3. **Vista**: Select con opciones basadas en ViewBag
4. **JavaScript**: Recargar página con parámetros, NO filtrar solo en cliente
5. **Toggle**: Actualizar visualmente + recargar para sincronizar

Este patrón garantiza consistencia con el módulo de Users y evita problemas de sincronización.