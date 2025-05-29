# ToolBox Admin Dashboard Project

## 🔧 Solución de Errores Comunes de Consola

### Error: Failed to load resource: net::ERR_NAME_NOT_RESOLVED (via.placeholder.com)

**Problema**: Las vistas utilizaban imágenes placeholder de `via.placeholder.com`, un servicio externo que puede fallar sin conexión a internet o cuando el servicio no está disponible.

**Síntomas**: 
- Error en consola: `Failed to load resource: net::ERR_NAME_NOT_RESOLVED via.placeholder.com/250x250/DFE3E7/8C98A4?text=User:1`
- Las imágenes de avatar no se cargan correctamente

**Solución Implementada**:
Reemplazar todas las referencias a `via.placeholder.com` con la imagen local `/img/default-avatar.png`:

```html
<!-- ❌ ANTES (causa error) -->
<img src="https://via.placeholder.com/250x250/DFE3E7/8C98A4?text=User">

<!-- ✅ DESPUÉS (usa imagen local) -->
<img src="/img/default-avatar.png">
```

**Archivos Modificados**:
1. `/Views/Users/_AddUserOffcanvas.cshtml`
2. `/Views/Customers/_AddCustomerOffcanvas.cshtml`
3. `/Views/Instructors/_InstructorAccountTab.cshtml`
4. `/Views/Users/Details.cshtml`
5. `/wwwroot/js/instructors.js` (variable `defaultInstructorAvatarSrc`)

**Nota**: El proyecto ya incluye `/wwwroot/img/default-avatar.png` como imagen por defecto.

## ⚠️ Notas Críticas sobre Fechas y PostgreSQL

### Problema con Fechas en PostgreSQL/Npgsql
Cuando se envían fechas desde JavaScript a ASP.NET Core con PostgreSQL, es **CRÍTICO** usar el formato ISO 8601 completo:

```javascript
// ❌ INCORRECTO - Causará error al guardar
const fechaLimite = "2024-05-24";

// ✅ CORRECTO - Formato ISO completo
const dateObj = new Date(fechaLimiteValue + 'T00:00:00');
const fechaLimite = dateObj.toISOString(); // "2024-05-24T00:00:00.000Z"
```

**Razón**: Los campos `DateTime?` en C# se mapean a `timestamp` en PostgreSQL, que requiere fecha Y hora completa.

## 🎯 Stack Tecnológico
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core (PostgreSQL)
- Bootstrap 5.3 + Font Awesome
- JavaScript vanilla (sin jQuery en nuevos módulos)
- QuestPDF para PDFs, ClosedXML para Excel

## 📋 Patrones de Desarrollo Rápido

### Estructura de Archivos para CRUD
```
/Controllers/{Module}Controller.cs
/Services/{Module}Service.cs + /Interfaces/I{Module}Service.cs
/Models/{Module}.cs + ViewModels/{Module}ViewModels.cs
/Views/{Module}/Index.cshtml + _Add{Module}Offcanvas.cshtml
/wwwroot/js/{module}-script.js
```

### Registro en Program.cs
```csharp
builder.Services.AddScoped<I{Module}Service, {Module}Service>();
QuestPDF.Settings.License = LicenseType.Community; // Solo si usas PDF
```

### Sistema de Traducciones
- Usar `data-translate-key` en HTML
- Agregar traducciones en `/wwwroot/js/admin-script.js`
- Nomenclatura: `modulo_elemento_tipo`

## 🚀 Checklist Rápido para Nuevos Módulos

### Paso 1: Modelo y Migración
```csharp
public class Module
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

### Paso 2: Service Pattern
```csharp
// Interfaz
Task<IEnumerable<Module>> GetAllAsync(bool includeInactive = false);
Task<Module?> GetByIdAsync(int id);
Task<bool> CreateAsync(Module entity);
Task<bool> UpdateAsync(Module entity);
Task<(bool Success, string Message)> ToggleStatusAsync(int id);
```

### Paso 3: ViewModel Pattern
```csharp
public class ModuleListViewModel { /* props para lista */ }
public class ModuleCreateViewModel { /* props para crear */ }
public class ModuleEditViewModel { /* props para editar */ }
```

### Paso 4: Problemas Comunes y Soluciones

#### 🔴 Migraciones con Índices Únicos
```bash
# Paso 1: Migración sin índices
dotnet ef migrations add InitialCreate
# Paso 2: Limpiar duplicados en BD
# Paso 3: Migración con índices
dotnet ef migrations add AddUniqueIndexes
```

#### 🔴 Validación de Archivos
```csharp
if (file.Length > 800 * 1024) // 800KB max
    ModelState.AddModelError("File", "Archivo muy grande");
    
var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
if (!allowedExtensions.Contains(Path.GetExtension(file.FileName)))
    ModelState.AddModelError("File", "Tipo no permitido");
```

#### 🔴 Exportación Excel/PDF
```csharp
// Excel con ClosedXML (sin problemas de licencia)
using var workbook = new XLWorkbook();
var worksheet = workbook.Worksheets.Add("Data");

// PDF requiere en Program.cs:
QuestPDF.Settings.License = LicenseType.Community;
```

## 📦 Patrón de Filtros Active/Inactive con AJAX

### Controller Pattern
```csharp
// Index con filtro de estado (default: active)
public async Task<IActionResult> Index(string? statusFilter = null)
{
    statusFilter = statusFilter ?? "active";
    ViewBag.CurrentStatusFilter = statusFilter;
    // filtrar y retornar vista
}

// Toggle con respuesta JSON
[HttpPost]
public async Task<IActionResult> ToggleStatus(int id)
{
    var (success, message, newState) = await _service.ToggleStatusAsync(id);
    return Json(new { success, message, newIsActiveState = newState });
}
```

### JavaScript Pattern (Reutilizable)
```javascript
// Filtro con recarga de página
statusFilter.addEventListener('change', function() {
    const params = new URLSearchParams();
    params.append('statusFilter', this.value);
    window.location.href = window.location.pathname + '?' + params.toString();
});

// Toggle con AJAX y animación
if (result.success) {
    // Si no coincide con filtro actual, remover con animación
    if ((currentFilter === 'active' && !result.newIsActiveState) || 
        (currentFilter === 'inactive' && result.newIsActiveState)) {
        row.style.opacity = '0';
        setTimeout(() => row.remove(), 300);
    }
}
```

### Toast Notifications (copiar tal cual)
```html
<div class="position-fixed top-0 end-0 p-3" style="z-index: 1050">
    <div id="notificationToast" class="toast"><!-- contenido --></div>
</div>
```

## 🎓 Módulos Implementados y Lecciones Clave

### 1. Usuarios (Referencia Base)
- **Patrón completo**: CRUD + Filtros + Export + Upload
- **Lección clave**: Event delegation para elementos dinámicos
- **Problema resuelto**: Índices únicos requieren migración en 2 pasos

### 2. Áreas de Vida (Life Areas) 
- **Feature único**: Selector visual de iconos con preview
- **Lección clave**: Sincronización bidireccional color picker/text
- **Problema resuelto**: `window.location.pathname` evita errores 404

### 3. XRay Life (Questions)
- **Feature único**: Navegación lateral + carga dinámica sin recarga
- **Lección clave**: Hard delete a veces es mejor que soft delete
- **Problema resuelto**: Reutilizar estructura existente vs crear desde cero

### 4. Calendario (Calendar Events)
- **Feature único**: Invitación de múltiples usuarios con notificaciones automáticas
- **Lección clave**: Relación many-to-many con tabla intermedia (SesionUsuario)
- **Problema resuelto**: Transición de Customers a Users para invitaciones
- **Implementación**: 
  - Tabla `SesionesUsuarios` para relación N:N entre eventos y usuarios
  - API endpoint `/api/users/invitable` excluye al coach actual
  - Notificaciones automáticas al crear eventos con usuarios invitados
  - Select2 múltiple para selección de usuarios

## 🚨 PROBLEMA CRÍTICO: GetCurrentUserId() Hardcodeado

### Descripción del Problema
Múltiples controladores tienen el método `GetCurrentUserId()` hardcodeado que retorna siempre ID=1:

**Controladores Afectados:**
- TasksController ✅
- UsersController ✅ 
- WheelOfLifeController ✅
- WheelOfProgressController ✅
- XRayLifeController ✅

**Código Problemático Actual:**
```csharp
private int GetCurrentUserId()
{
    // TODO: Obtener el usuario actual desde la sesión o claims
    // Por ahora retornamos un ID hardcodeado para pruebas
    return 1;
}
```

### Problemas en Producción
1. **Todas las tareas/datos se asignan al mismo usuario** (ID=1)
2. **No hay diferenciación entre usuarios reales**
3. **Problemas de seguridad** - cualquiera puede ver/editar datos de todos
4. **Foreign Key Issues** - Si no existe usuario con ID=1, falla la creación

### Solución Temporal Actual (Desarrollo)
Para desarrollo, se creó manualmente un usuario con ID=1:
```sql
INSERT INTO "Users" ("Id", "FullName", "UserName", "Email", "PasswordHash", "RoleId", "IsActive", "CreatedAt", "UpdatedAt")
VALUES (1, 'Test User', 'testuser', 'test@example.com', 'temp', 2, true, NOW(), NOW());
```

**IMPORTANTE:** Este usuario temporal debe eliminarse antes de producción.

### Solución para Implementación de Autenticación Real

#### 1. Configurar Autenticación en Program.cs
```csharp
// En Program.cs - Agregar Identity o JWT
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
    });

builder.Services.AddAuthorization();

// En el pipeline
app.UseAuthentication();
app.UseAuthorization();
```

#### 2. Actualizar GetCurrentUserId() en TODOS los Controladores
```csharp
private int GetCurrentUserId()
{
    // Implementación real con Claims
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (int.TryParse(userIdClaim, out int userId))
    {
        return userId;
    }
    
    // Fallback para desarrollo
    throw new UnauthorizedAccessException("Usuario no autenticado");
}
```

#### 3. Checklist de Migración a Autenticación Real
- [ ] Configurar autenticación en Program.cs
- [ ] Crear AuthController con Login/Logout
- [ ] Actualizar GetCurrentUserId() en TasksController
- [ ] Actualizar GetCurrentUserId() en UsersController
- [ ] Actualizar GetCurrentUserId() en WheelOfLifeController
- [ ] Actualizar GetCurrentUserId() en WheelOfProgressController
- [ ] Actualizar GetCurrentUserId() en XRayLifeController
- [ ] Agregar [Authorize] a controladores protegidos
- [ ] Crear vistas de Login/Register
- [ ] Eliminar usuario temporal ID=1
- [ ] Testear flujo completo de autenticación

## 🚫 PROCESO CRÍTICO PARA NUEVOS MÓDULOS: MIGRACIONES Y PERMISOS

### ⚠️ IMPORTANTE: NUNCA CREAR ARCHIVOS DE MIGRACIÓN MANUALMENTE
**Problema Recurrente**: Crear archivos de migración manualmente causa que Visual Studio no los reconozca y genera conflictos con IDs de permisos.

### 🔴 PROBLEMA ESPECÍFICO CON PERMISOS - PROBLEMA REAL Y SOLUCIÓN DEFINITIVA

#### EL PROBLEMA REAL:
1. La tabla `Permissions` tiene un campo `Id` que NO es autoincremental (IDENTITY)
2. El método `SeedPermissions` en `ApplicationDbContext` usa IDs fijos secuenciales
3. Cuando EF genera una migración para un nuevo módulo, intenta insertar permisos con IDs que YA EXISTEN
4. Esto causa: `duplicate key value violates unique constraint "PK_Permissions"`

#### ERRORES QUE VERÁS:
- Durante `dotnet ef database update`: "already exists with the key value '{Id: 61}'"
- Al ejecutar SQL sin ID: "duplicate key value violates unique constraint"

### ✅ SOLUCIÓN DEFINITIVA PARA NUEVOS MÓDULOS

#### PASO 1: NUNCA agregues el módulo al array en ApplicationDbContext
```csharp
// ❌ NO HAGAS ESTO:
var modules = new[]
{
    "Dashboard",
    "Users",
    "NuevoModulo" // ❌ NO LO AGREGUES AQUÍ
};
```

#### PASO 2: Si ya creaste una migración con permisos, ELIMÍNALA
```bash
# Si ves archivos como 20250527193725_AddNotificationPermissions.cs
rm Migrations/*AddNotificationPermissions*
```

#### PASO 3: Crea los permisos con SQL que incluya ID automático
```sql
-- SCRIPT COMPLETO Y FUNCIONAL para add_[modulo]_permissions.sql
DO $$
DECLARE
    next_id INTEGER;
BEGIN
    -- Permiso Read
    SELECT COALESCE(MAX("Id"), 0) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'NuevoModulo' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'NuevoModulo', 'Read', 'Ver y listar en Nuevo Módulo', 'General');
    END IF;

    -- Permiso Write
    SELECT COALESCE(MAX("Id"), 0) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'NuevoModulo' AND "ActionName" = 'Write'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'NuevoModulo', 'Write', 'Editar y actualizar en Nuevo Módulo', 'General');
    END IF;

    -- Permiso Create
    SELECT COALESCE(MAX("Id"), 0) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'NuevoModulo' AND "ActionName" = 'Create'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'NuevoModulo', 'Create', 'Crear nuevos registros en Nuevo Módulo', 'General');
    END IF;
END $$;

-- Asignar a roles (ejecutar por separado)
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 1, p."Id"
FROM "Permissions" p
WHERE p."ModuleName" = 'NuevoModulo'
AND NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 1 AND rp."PermissionId" = p."Id"
);
```

#### PASO 4: Para el sidebar, agrega la condición pero NO el módulo al seed
```csharp
// En _AdminLayout.cshtml
@if (readableModules.Contains("NuevoModulo"))
{
    <a href="@Url.Action("Index", "NuevoModulo")" class="list-group-item">
        <i class="fas fa-icon"></i><span>Nuevo Módulo</span>
    </a>
}
```

### ✅ PROCESO CORRECTO PARA NUEVOS MÓDULOS

#### Paso 1: Preparar SOLO los Modelos y DbContext
```csharp
// 1. Crear el modelo en /Models/
public class NuevoModulo
{
    public int Id { get; set; }
    // ... propiedades
}

// 2. Actualizar ApplicationDbContext.cs
public DbSet<NuevoModulo> NuevoModulos { get; set; }

// 3. NO AGREGAR permisos en SeedPermissions todavía
```

#### Paso 2: El Usuario Ejecuta la Migración
```bash
# El USUARIO ejecuta esto en Visual Studio Package Manager Console:
Add-Migration CreateNuevoModuloTable
```

#### Paso 3: Verificar Permisos Existentes ANTES de Agregar Nuevos
```sql
-- El usuario debe ejecutar para ver el último ID usado:
SELECT MAX("Id") FROM "Permissions";
```

#### Paso 4: Agregar Permisos en ApplicationDbContext
```csharp
// En ApplicationDbContext.cs - método SeedPermissions
// USAR IDs que NO existan (verificar con la consulta anterior)
new Permission { Id = 61, ModuleName = "NuevoModulo", ActionName = "Read", ... },
new Permission { Id = 62, ModuleName = "NuevoModulo", ActionName = "Write", ... },
new Permission { Id = 63, ModuleName = "NuevoModulo", ActionName = "Create", ... }
```

#### Paso 5: Segunda Migración para Permisos
```bash
# El USUARIO ejecuta:
Add-Migration AddNuevoModuloPermissions
Update-Database
```

### 📝 CHECKLIST OBLIGATORIO PARA NUEVOS MÓDULOS

1. **ANTES de crear el módulo:**
   - [ ] NO crear archivos de migración manualmente
   - [ ] NO agregar permisos en la primera migración
   - [ ] Verificar último ID de permisos en la BD

2. **Crear estructura básica:**
   - [ ] Modelo en /Models/
   - [ ] DbSet en ApplicationDbContext
   - [ ] NO tocar SeedPermissions todavía

3. **Primera migración (solo tablas):**
   - [ ] Usuario ejecuta: `Add-Migration CreateXXXTable`
   - [ ] Usuario ejecuta: `Update-Database`

4. **Agregar permisos después:**
   - [ ] Verificar último ID: `SELECT MAX("Id") FROM "Permissions"`
   - [ ] Agregar en SeedPermissions con IDs nuevos
   - [ ] Agregar módulo a la lista de modules en SeedPermissions

5. **Segunda migración (permisos):**
   - [ ] Usuario ejecuta: `Add-Migration AddXXXPermissions`
   - [ ] Usuario ejecuta: `Update-Database`

6. **Verificar permisos:**
   - [ ] Agregar módulo en _AdminLayout.cshtml
   - [ ] Asignar permisos al rol en UI de Roles

## 🔍 PATRÓN FILTROS Y BÚSQUEDA - GUÍA COMPLETA DE IMPLEMENTACIÓN

### ⚠️ REGLA FUNDAMENTAL
- **Filtro de Estado**: Del lado servidor (recarga página)
- **Búsqueda de Texto**: Del lado cliente (tiempo real)
- **AJAX**: Siempre form-data, nunca JSON

---

### 📋 CHECKLIST RÁPIDO PARA IMPLEMENTAR FILTROS

#### Paso 1: Controlador (2 cambios)
```csharp
// ✅ PATRÓN CORRECTO
public async Task<IActionResult> Index(string? statusFilter = null)
{
    statusFilter = statusFilter ?? "active"; // 🚨 DEFAULT ACTIVE
    ViewBag.CurrentStatusFilter = statusFilter; // 🚨 IMPORTANTE PARA VISTA
    
    var query = _context.Entidades.AsQueryable();
    
    // ✅ Solo filtro de estado
    if (statusFilter == "active") query = query.Where(x => x.Status == true);
    else if (statusFilter == "inactive") query = query.Where(x => x.Status == false);
    // "all" no filtra
    
    // ❌ NO HACER BÚSQUEDA AQUÍ
    
    var entities = await query.ToListAsync();
    return View(new IndexViewModel { Entities = entities, CurrentStatusFilter = statusFilter });
}
```

#### Paso 2: HTML Vista (Copiar exacto)
```html
<!-- ✅ ESTRUCTURA CORRECTA -->
<form method="get" class="d-flex gap-2">
    <div class="input-group">
        <span class="input-group-text"><i class="fas fa-search"></i></span>
        <input type="search" name="searchQuery" class="form-control" 
               placeholder="Buscar..." id="searchInput">
    </div>
    
    <select name="statusFilter" id="statusFilter" class="form-select">
        @if (Model.CurrentStatusFilter == "all")
        {
            <option value="all" selected>Todos</option>
        }
        else
        {
            <option value="all">Todos</option>
        }
        
        @if (Model.CurrentStatusFilter == "active")
        {
            <option value="active" selected>Activos</option>
        }
        else
        {
            <option value="active">Activos</option>
        }
        
        @if (Model.CurrentStatusFilter == "inactive")
        {
            <option value="inactive" selected>Inactivos</option>
        }
        else
        {
            <option value="inactive">Inactivos</option>
        }
    </select>
    
    <button type="submit" class="btn btn-primary">
        <i class="fas fa-search"></i>
    </button>
</form>
```

#### Paso 3: JavaScript (Copiar exacto)
```javascript
document.addEventListener('DOMContentLoaded', function() {
    // 🚨 FILTRO DE ESTADO - Recarga página
    const statusFilter = document.getElementById('statusFilter');
    
    function applyFilters() {
        const params = new URLSearchParams();
        const statusValue = statusFilter ? statusFilter.value : '';
        
        if (statusValue && statusValue !== 'all') params.append('statusFilter', statusValue);
        
        window.location.href = '@Url.Action("Index", "ControllerName")' + 
                              (params.toString() ? '?' + params.toString() : '');
    }
    
    if (statusFilter) {
        statusFilter.addEventListener('change', applyFilters);
    }
    
    // 🚨 BÚSQUEDA - Tiempo real, lado cliente
    const searchInput = document.querySelector('input[name="searchQuery"]');
    
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const searchTerm = this.value.toLowerCase().trim();
            const tableRows = document.querySelectorAll('#tablaId tbody tr');
            
            tableRows.forEach(row => {
                if (row.cells.length === 1) return; // Skip "no results" row
                
                // 🚨 AJUSTAR SEGÚN COLUMNAS DE CADA MÓDULO
                const cell1 = row.cells[0]?.textContent.toLowerCase() || '';
                const cell2 = row.cells[1]?.textContent.toLowerCase() || '';
                // Agregar más celdas según necesidad
                
                const matches = cell1.includes(searchTerm) || 
                              cell2.includes(searchTerm);
                
                row.style.display = matches ? '' : 'none';
            });
            
            // Mensaje "No encontrado"
            const visibleRows = Array.from(tableRows).filter(row => 
                row.style.display !== 'none' && row.cells.length > 1
            );
            
            if (visibleRows.length === 0) {
                const tbody = document.querySelector('#tablaId tbody');
                const noResultsRow = tbody.querySelector('.no-results-row');
                
                if (!noResultsRow) {
                    const newRow = tbody.insertRow();
                    newRow.className = 'no-results-row';
                    newRow.innerHTML = '<td colspan="X" class="text-center py-4"><div class="text-muted"><i class="fas fa-search fa-2x mb-3 d-block"></i><span>No se encontraron resultados</span></div></td>';
                }
            } else {
                const noResultsRow = document.querySelector('.no-results-row');
                if (noResultsRow) noResultsRow.remove();
            }
        });
        
        // Prevenir submit en Enter
        searchInput.addEventListener('keypress', function(e) {
            if (e.key === 'Enter') e.preventDefault();
        });
    }
});
```

#### Paso 4: ViewModel (Agregar propiedad)
```csharp
public class ModuloIndexViewModel
{
    public List<Entidad> Entidades { get; set; } = new List<Entidad>();
    public string? CurrentStatusFilter { get; set; } // 🚨 OBLIGATORIO
}
```

#### Paso 5: AJAX Toggle Status (Copiar exacto)
```javascript
// 🚨 PATRÓN AJAX FORM-DATA
function toggleStatus(entityId, button) {
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    
    fetch('/Controller/ToggleStatus', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: new URLSearchParams({
            'id': entityId,
            '__RequestVerificationToken': token
        })
    })
    .then(response => response.json())
    .then(result => {
        if (result.success) {
            // Actualizar UI según filtro actual
            const currentFilter = '@Model.CurrentStatusFilter';
            const row = button.closest('tr');
            
            if ((currentFilter === 'active' && !result.newIsActiveState) || 
                (currentFilter === 'inactive' && result.newIsActiveState)) {
                // Remover con animación si no coincide con filtro
                row.style.opacity = '0';
                setTimeout(() => row.remove(), 300);
            }
        }
    });
}
```

---

### 🚨 ERRORES COMUNES QUE SIEMPRE EVITAR

1. **❌ NO usar JSON en AJAX**
   ```javascript
   // ❌ INCORRECTO
   headers: { 'Content-Type': 'application/json' },
   body: JSON.stringify({ id: entityId })
   ```

2. **❌ NO hacer búsqueda en servidor**
   ```csharp
   // ❌ INCORRECTO
   if (!string.IsNullOrWhiteSpace(searchQuery)) {
       query = query.Where(x => x.Name.Contains(searchQuery));
   }
   ```

3. **❌ NO olvidar ViewBag.CurrentStatusFilter**
   ```csharp
   // ❌ INCORRECTO - Vista no sabrá qué opción seleccionar
   return View(viewModel);
   
   // ✅ CORRECTO
   ViewBag.CurrentStatusFilter = statusFilter;
   return View(viewModel);
   ```

4. **❌ NO olvidar default "active"**
   ```csharp
   // ❌ INCORRECTO - Sin default
   public async Task<IActionResult> Index(string? statusFilter = null)
   
   // ✅ CORRECTO - Con default
   statusFilter = statusFilter ?? "active";
   ```

---

### 📝 PLANTILLA RÁPIDA PARA COPIAR Y PEGAR

#### JavaScript completo para cualquier módulo:
```javascript
// 🚨 CAMBIAR: ControllerName, tablaId, columnas de búsqueda, colspan
document.addEventListener('DOMContentLoaded', function() {
    const statusFilter = document.getElementById('statusFilter');
    const searchInput = document.querySelector('input[name="searchQuery"]');
    
    // Filtro estado
    if (statusFilter) {
        statusFilter.addEventListener('change', function() {
            const params = new URLSearchParams();
            if (this.value && this.value !== 'all') params.append('statusFilter', this.value);
            window.location.href = '@Url.Action("Index", "CAMBIAR_CONTROLLER")' + 
                                  (params.toString() ? '?' + params.toString() : '');
        });
    }
    
    // Búsqueda cliente
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const searchTerm = this.value.toLowerCase().trim();
            const tableRows = document.querySelectorAll('#CAMBIAR_TABLA_ID tbody tr');
            
            tableRows.forEach(row => {
                if (row.cells.length === 1) return;
                
                // 🚨 CAMBIAR COLUMNAS SEGÚN MÓDULO
                const matches = row.cells[0]?.textContent.toLowerCase().includes(searchTerm) ||
                              row.cells[1]?.textContent.toLowerCase().includes(searchTerm);
                
                row.style.display = matches ? '' : 'none';
            });
            
            const visibleRows = Array.from(tableRows).filter(row => 
                row.style.display !== 'none' && row.cells.length > 1);
            
            if (visibleRows.length === 0) {
                const tbody = document.querySelector('#CAMBIAR_TABLA_ID tbody');
                if (!tbody.querySelector('.no-results-row')) {
                    const newRow = tbody.insertRow();
                    newRow.className = 'no-results-row';
                    newRow.innerHTML = '<td colspan="CAMBIAR_NUMERO" class="text-center py-4"><div class="text-muted"><i class="fas fa-search fa-2x mb-3 d-block"></i><span>No se encontraron resultados</span></div></td>';
                }
            } else {
                const noResultsRow = document.querySelector('.no-results-row');
                if (noResultsRow) noResultsRow.remove();
            }
        });
        
        searchInput.addEventListener('keypress', function(e) {
            if (e.key === 'Enter') e.preventDefault();
        });
    }
});
```

### 🔴 ERRORES COMUNES QUE EVITAR

1. **NO hacer esto:**
   ```csharp
   // ❌ NUNCA crear migración manualmente
   public partial class CreateSessionTables : Migration { }
   ```

2. **NO agregar permisos en primera migración:**
   ```csharp
   // ❌ INCORRECTO - causará conflicto de IDs
   migrationBuilder.InsertData(
       table: "Permissions",
       values: new object[] { 55, "Read", ... }
   ```

3. **NO asumir IDs de permisos:**
   ```csharp
   // ❌ INCORRECTO - verificar primero qué IDs están usados
   new Permission { Id = 55, ... } // Este ID puede ya existir
   ```

### 💡 SOLUCIÓN SI YA HAY CONFLICTOS

Si ya se crearon migraciones con conflictos:
1. Eliminar archivos de migración problemáticos
2. Usuario ejecuta: `Add-Migration NombreNuevo`
3. Comentar InsertData/DeleteData de permisos si hay conflictos
4. Usuario ejecuta: `Update-Database`

## 🔧 SOLUCIÓN DEFINITIVA: GRÁFICAS BORROSAS EN CHART.JS

### 🚨 PROBLEMA RECURRENTE
**Síntomas:**
- Texto borroso en gráficas de Chart.js
- Líneas y puntos se ven pixelados
- Mala calidad visual en pantallas de alta resolución (Retina, 4K)

**Causas:**
- Chart.js no maneja automáticamente el `devicePixelRatio`
- Fuentes predeterminadas no están optimizadas
- Canvas no está configurado para pantallas de alta densidad

### ✅ SOLUCIÓN COMPLETA Y FUNCIONAL

#### Paso 1: CSS para Canvas (obligatorio)
```css
.chart-container canvas {
    image-rendering: -webkit-optimize-contrast;
    image-rendering: crisp-edges;
    image-rendering: pixelated;
}
```

#### Paso 2: Configuración Chart.js (copiar exacto)
```javascript
new Chart(ctx.getContext('2d'), {
    type: 'line',
    data: { /* tus datos */ },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        
        // 🚨 CRÍTICO: Configuración para pantallas de alta resolución
        devicePixelRatio: window.devicePixelRatio || 2,
        
        // 🚨 CRÍTICO: Configuración de fuentes optimizada
        scales: {
            y: {
                ticks: {
                    font: {
                        family: 'Inter, system-ui, -apple-system, sans-serif',
                        size: 12,
                        weight: '500'
                    }
                }
            },
            x: {
                ticks: {
                    font: {
                        family: 'Inter, system-ui, -apple-system, sans-serif',
                        size: 12,
                        weight: '500'
                    }
                },
                title: {
                    display: true,
                    text: 'Título del Eje',
                    font: {
                        family: 'Inter, system-ui, -apple-system, sans-serif',
                        size: 13,
                        weight: '600'
                    }
                }
            }
        }
    }
});
```

#### Paso 3: Configuración del Contenedor
```css
.chart-container {
    position: relative;
    height: 400px;
    margin: 2rem 0;
}
```

### 🎯 IMPLEMENTACIÓN RÁPIDA (5 MINUTOS)

Para cualquier gráfica nueva, copiar exactamente:

```javascript
// Plantilla completa para gráficas nítidas
new Chart(document.getElementById('miGrafica').getContext('2d'), {
    type: 'line', // o 'bar', 'pie', etc.
    data: {
        labels: tusDatos.labels,
        datasets: tusDatos.datasets
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        devicePixelRatio: window.devicePixelRatio || 2, // 🚨 OBLIGATORIO
        
        scales: {
            y: {
                ticks: {
                    font: {
                        family: 'Inter, system-ui, -apple-system, sans-serif',
                        size: 12,
                        weight: '500'
                    }
                }
            },
            x: {
                ticks: {
                    font: {
                        family: 'Inter, system-ui, -apple-system, sans-serif',
                        size: 12,
                        weight: '500'
                    }
                }
            }
        }
    }
});
```

### 📝 CHECKLIST PARA GRÁFICAS NÍTIDAS

- [ ] Agregar CSS `image-rendering` al canvas
- [ ] Configurar `devicePixelRatio: window.devicePixelRatio || 2`
- [ ] Definir fuentes personalizadas en `ticks.font`
- [ ] Usar familia de fuente optimizada: `'Inter, system-ui, -apple-system, sans-serif'`
- [ ] Configurar `responsive: true` y `maintainAspectRatio: false`

### ⚠️ ERRORES QUE EVITAR

```javascript
// ❌ NO HAGAS ESTO
new Chart(ctx, {
    // Sin devicePixelRatio - causará texto borroso
    // Sin configuración de fuentes - usará defaults pixelados
});

// ❌ NO OLVIDES EL CSS
// Sin image-rendering en canvas - no funcionará
```

**RESULTADO:** Gráficas perfectamente nítidas en cualquier pantalla (1080p, 4K, Retina).

---

## 💡 Soluciones Rápidas a Problemas Comunes

```csharp
// Servicio no registrado
builder.Services.AddScoped<IService, Service>();

// Cambio de propiedad en modelo
// user.Name → user.FullName (actualizar TODOS los archivos)

// Validación archivo upload
if (file.Length > 800 * 1024) return "Muy grande";
var allowed = new[] { ".jpg", ".png" };
if (!allowed.Contains(ext)) return "Tipo inválido";

// Nombres únicos para archivos
var fileName = Guid.NewGuid() + extension;

// Event delegation JavaScript
document.addEventListener('click', function(e) {
    if (e.target.closest('.btn-class')) { }
});

// CSS para imágenes nítidas
.avatar { 
    width: 48px; 
    height: 48px;
    object-fit: cover;
    image-rendering: optimizeQuality;
}
```

