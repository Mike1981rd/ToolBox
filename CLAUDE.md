# ToolBox Admin Dashboard Project

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

