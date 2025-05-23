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