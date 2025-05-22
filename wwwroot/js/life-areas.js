/**
 * Life Areas Module JavaScript
 * Handles life areas CRUD operations and offcanvas functionality
 */

document.addEventListener('DOMContentLoaded', function () {
    const addEditOffcanvasElement = document.getElementById('addEditLifeAreaOffcanvas');
    const offcanvasForm = document.getElementById('addEditLifeAreaForm');
    const offcanvasTitle = document.getElementById('addEditLifeAreaOffcanvasLabel');
    const lifeAreaIdInput = document.getElementById('lifeAreaId');
    const submitButton = offcanvasForm ? offcanvasForm.querySelector('.data-submit') : null;

    // Resetear y configurar offcanvas para "Añadir Nueva Área"
    const addNewLifeAreaBtn = document.getElementById('addNewLifeAreaBtn');
    if (addNewLifeAreaBtn) {
        addNewLifeAreaBtn.addEventListener('click', function () {
            offcanvasForm.reset();
            lifeAreaIdInput.value = '0'; // Indica modo creación
            offcanvasTitle.dataset.translateKey = 'offcanvas_add_life_area_title';
            offcanvasTitle.textContent = getTranslation('offcanvas_add_life_area_title'); // Actualizar texto
            if(submitButton) {
                submitButton.dataset.translateKey = 'add_button';
                submitButton.textContent = getTranslation('add_button');
            }
            document.getElementById('currentAttachmentPreview').innerHTML = '';
        });
    }

    // Configurar offcanvas para "Editar Área" (cuando se hace clic en el botón de editar en la tabla)
    document.querySelectorAll('.edit-life-area-btn').forEach(button => {
        button.addEventListener('click', function () {
            const areaId = this.dataset.id;
            // Simular carga de datos para edición (reemplazar con AJAX en app real)
            const areaData = getLifeAreaMockData(areaId); // Función placeholder
            
            offcanvasForm.reset(); // Resetear primero
            lifeAreaIdInput.value = areaId;
            document.getElementById('lifeAreaTitle').value = getTranslation(areaData.titleKey); // Asume que titleKey se traduce
            document.getElementById('lifeAreaDescription').value = getTranslation(areaData.descriptionKey); // Asume que descKey se traduce
            document.getElementById('lifeAreaStatus').value = areaData.status.toLowerCase();
            
            // Mostrar previsualización de adjunto (simulado)
            const previewDiv = document.getElementById('currentAttachmentPreview');
            if (areaData.iconClass) { // Suponiendo que tenemos un icono o imagen
                previewDiv.innerHTML = `<p class="mb-1 small text-muted" data-translate-key="form_current_attachment">Current Attachment:</p><i class="${areaData.iconClass} fa-2x ${areaData.iconBg.replace('-label', '')} bg-opacity-100 p-2 rounded"></i>`;
            } else {
                previewDiv.innerHTML = '';
            }

            offcanvasTitle.dataset.translateKey = 'offcanvas_edit_life_area_title';
            offcanvasTitle.textContent = getTranslation('offcanvas_edit_life_area_title');
             if(submitButton) {
                submitButton.dataset.translateKey = 'save_changes_button'; // Usar save_changes_button
                submitButton.textContent = getTranslation('save_changes_button');
            }
        });
    });

    // Placeholder para obtener datos de un área (simulado)
    function getLifeAreaMockData(id) {
        const mockData = {
            "1": { titleKey: "life_area_spirituality", descriptionKey: "description_spirituality", status: "active", iconClass: "fas fa-pray", iconBg: "bg-label-primary" },
            "2": { titleKey: "life_area_growth_learning", descriptionKey: "description_growth_learning", status: "active", iconClass: "fas fa-brain", iconBg: "bg-label-success" },
            "3": { titleKey: "life_area_social_love", descriptionKey: "description_social_love", status: "active", iconClass: "fas fa-heart", iconBg: "bg-label-danger" },
            "4": { titleKey: "life_area_family_friends", descriptionKey: "description_family_friends", status: "active", iconClass: "fas fa-users", iconBg: "bg-label-warning" },
            "5": { titleKey: "life_area_community", descriptionKey: "description_community", status: "active", iconClass: "fas fa-city", iconBg: "bg-label-info" },
            "6": { titleKey: "life_area_environment", descriptionKey: "description_environment", status: "active", iconClass: "fas fa-leaf", iconBg: "bg-label-success" },
            "7": { titleKey: "life_area_work_mission", descriptionKey: "description_work_mission", status: "active", iconClass: "fas fa-briefcase", iconBg: "bg-label-primary" },
            "8": { titleKey: "life_area_economy_finance", descriptionKey: "description_economy_finance", status: "active", iconClass: "fas fa-dollar-sign", iconBg: "bg-label-warning" },
            "9": { titleKey: "life_area_health_body", descriptionKey: "description_health_body", status: "active", iconClass: "fas fa-heartbeat", iconBg: "bg-label-danger" },
            "10": { titleKey: "life_area_recreation_fun", descriptionKey: "description_recreation_fun", status: "active", iconClass: "fas fa-gamepad", iconBg: "bg-label-info" }
        };
        return mockData[id] || { titleKey: "Unknown Area", descriptionKey: "No description", status: "inactive" };
    }

    // Enviar formulario (simulado, solo UI)
    if (offcanvasForm) {
        offcanvasForm.addEventListener('submit', function (event) {
            event.preventDefault();
            const formData = new FormData(this);
            const lifeAreaId = formData.get('LifeAreaId');
            console.log(lifeAreaId === '0' ? 'Creating new area:' : 'Updating area ID: ' + lifeAreaId, Object.fromEntries(formData));
            
            // Aquí iría la lógica AJAX para guardar en backend
            // Por ahora, solo simular success
            alert(lifeAreaId === '0' ? 'Life area created successfully!' : 'Life area updated successfully!');
            
            // Cerrar el offcanvas
            const offcanvasInstance = bootstrap.Offcanvas.getInstance(addEditOffcanvasElement);
            if (offcanvasInstance) {
                offcanvasInstance.hide();
            }
            // Opcional: Recargar la tabla o actualizarla dinámicamente
        });
    }

    // Manejar desactivación de áreas de vida
    document.querySelectorAll('.deactivate-life-area-btn').forEach(button => {
        button.addEventListener('click', function () {
            const areaId = this.dataset.id;
            const areaData = getLifeAreaMockData(areaId);
            const areaName = getTranslation(areaData.titleKey);
            
            if (confirm(`Are you sure you want to deactivate "${areaName}"?`)) {
                console.log('Deactivating life area ID:', areaId);
                // Aquí iría la lógica AJAX para desactivar
                alert(`Life area "${areaName}" has been deactivated.`);
                // Opcional: actualizar el estado en la tabla
            }
        });
    });

    // Helper function para obtener traducciones (usa la función global si existe)
    function getTranslation(key) {
        if (typeof window.getTranslation === 'function') {
            return window.getTranslation(key);
        }
        
        // Fallback: usar el sistema de traducciones existente
        const currentLang = localStorage.getItem('selectedLanguage') || 'en';
        if (typeof translations !== 'undefined' && translations[currentLang] && translations[currentLang][key]) {
            return translations[currentLang][key];
        }
        
        // Si no se encuentra traducción, devolver la clave
        return key;
    }
});