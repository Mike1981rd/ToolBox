/**
 * Roles functionality
 * Handles permission toggles and other role-related interactions
 */

document.addEventListener('DOMContentLoaded', function () {
    // Initialize tooltips with translation support
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        // Check if element has a translation key for tooltip
        const tooltipKey = tooltipTriggerEl.getAttribute('data-translate-tooltip-key');
        if (tooltipKey) {
            // Get current language
            const currentLang = localStorage.getItem('language') || 'en';
            // Get tooltip text from translations if available
            if (translations && translations[currentLang] && translations[currentLang][tooltipKey]) {
                tooltipTriggerEl.setAttribute('title', translations[currentLang][tooltipKey]);
            }
        }
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Lógica para "Select All" en el modal de permisos
    const selectAllCheckbox = document.getElementById('selectAllPermissions');
    const permissionToggles = document.querySelectorAll('.permission-toggle'); // Asumiendo que los toggles individuales tienen esta clase

    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener('change', function() {
            permissionToggles.forEach(toggle => {
                toggle.checked = selectAllCheckbox.checked;
            });
        });
    }
    
    // Opcional: si cualquier toggle individual se desmarca, desmarcar "Select All"
    permissionToggles.forEach(toggle => {
        toggle.addEventListener('change', function() {
            if (!this.checked) {
                selectAllCheckbox.checked = false;
            } else {
                // Verificar si todos están marcados para activar "Select All"
                const allChecked = Array.from(permissionToggles).every(toggle => toggle.checked);
                if (allChecked) {
                    selectAllCheckbox.checked = true;
                }
            }
        });
    });
    
    // Manejar el envío del formulario de nuevo rol (sin implementar backend)
    const addRoleForm = document.getElementById('addRoleForm');
    if (addRoleForm) {
        addRoleForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            // Simulamos que el rol se guardó exitosamente
            const roleNameInput = document.getElementById('roleNameInput');
            const roleName = roleNameInput.value.trim();
            
            if (roleName) {
                // Aquí se conectaría con el backend en el futuro
                console.log('Nuevo rol creado:', roleName);
                
                // Cerrar el modal
                const modal = bootstrap.Modal.getInstance(document.getElementById('addRoleModal'));
                modal.hide();
                
                // Mostrar mensaje de éxito (podría implementarse con una librería de toasts/alerts)
                alert('Rol creado exitosamente');
                
                // Limpiar el formulario
                addRoleForm.reset();
            } else {
                // Mostrar mensaje de error si no hay nombre de rol
                alert('Por favor ingrese un nombre de rol');
            }
        });
    }
});