/**
 * Customers Management JavaScript
 * Handles form validation, avatar upload preview, status toggling, and filters
 */

document.addEventListener('DOMContentLoaded', function() {
    // Get references to all interactive elements
    const addCustomerForm = document.getElementById('addNewCustomerForm');
    const uploadCustomerAvatarInput = document.getElementById('uploadCustomerAvatarInput');
    const resetCustomerAvatarButton = document.getElementById('resetCustomerAvatarButton');
    const customerAvatarPreview = document.getElementById('customerAvatarPreview');
    const customersTable = document.getElementById('customersTable');
    const statusFilter = document.getElementById('selectStatus');
    const searchInput = document.querySelector('input[type="search"]');
    
    // Mobile filter elements
    const mobileStatusFilter = document.getElementById('mobileSelectStatus');
    const applyMobileFiltersBtn = document.getElementById('applyMobileFilters');
    const filtersOffcanvas = document.getElementById('filtersOffcanvas');
    
    // Avatar por defecto local
    const defaultAvatarUrl = '/img/default-avatar.png';
    
    // 1. Avatar Upload Preview - Mejorado
    if (uploadCustomerAvatarInput && customerAvatarPreview) {
        uploadCustomerAvatarInput.addEventListener('change', function(event) {
            const file = event.target.files[0];
            if (file) {
                // Validación de tamaño (800KB max)
                if (file.size > 800 * 1024) {
                    alert('El archivo es demasiado grande. El tamaño máximo permitido es 800KB.');
                    this.value = ''; // Limpiar input
                    return;
                }
                
                // Validación de tipo (solo imágenes)
                if (!file.type.match('image.*')) {
                    alert('Por favor, selecciona una imagen válida (JPG, PNG o GIF).');
                    this.value = ''; // Limpiar input
                    return;
                }
                
                // Usar URL.createObjectURL para mejor rendimiento
                const objectUrl = URL.createObjectURL(file);
                customerAvatarPreview.src = objectUrl;
                customerAvatarPreview.classList.add('avatar-highlight'); // Opcional: efecto visual
                setTimeout(() => customerAvatarPreview.classList.remove('avatar-highlight'), 500);
                
                // Limpiar el objeto URL cuando se cambie la imagen
                customerAvatarPreview.onload = function() {
                    URL.revokeObjectURL(objectUrl);
                };
            }
        });
    }
    
    // 2. Reset Avatar to Default
    if (resetCustomerAvatarButton && customerAvatarPreview) {
        resetCustomerAvatarButton.addEventListener('click', function() {
            // Usar la imagen por defecto local
            customerAvatarPreview.src = defaultAvatarUrl;
            
            if (uploadCustomerAvatarInput) {
                uploadCustomerAvatarInput.value = ''; // Limpiar input file
            }
        });
    }
    
    // 3. Form Validation for Offcanvas
    if (addCustomerForm) {
        addCustomerForm.addEventListener('submit', function(event) {
            event.preventDefault();
            
            // Add was-validated class to show validation feedback
            this.classList.add('was-validated');
            
            // If form is valid
            if (this.checkValidity()) {
                // Preparar los datos del formulario
                const formData = new FormData();
                formData.append('FirstName', document.getElementById('add-customer-firstname').value);
                formData.append('LastName', document.getElementById('add-customer-lastname').value);
                formData.append('Email', document.getElementById('add-customer-email').value);
                formData.append('PhoneNumber', document.getElementById('add-customer-phone').value);
                formData.append('CompanyName', document.getElementById('add-customer-company').value || '');
                formData.append('Country', document.getElementById('add-customer-country').value || '');
                formData.append('IsActive', document.getElementById('add-customer-status').value);
                
                // Agregar el archivo de avatar si existe
                const avatarInput = document.getElementById('uploadCustomerAvatarInput');
                if (avatarInput && avatarInput.files && avatarInput.files[0]) {
                    formData.append('AvatarFile', avatarInput.files[0]);
                }
                
                // Obtener el token antiforgery
                const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
                formData.append('__RequestVerificationToken', token);
                
                // Enviar la petición al servidor
                fetch('/Customers/Create', {
                    method: 'POST',
                    body: formData
                })
                .then(response => {
                    if (response.redirected) {
                        // Si hay redirección, probablemente el cliente se creó exitosamente
                        // Cerrar el offcanvas y resetear el formulario
                        const offcanvas = bootstrap.Offcanvas.getInstance(document.getElementById('addCustomerOffcanvas'));
                        if (offcanvas) {
                            offcanvas.hide();
                        }
                        // Resetear el formulario
                        addCustomerForm.reset();
                        addCustomerForm.classList.remove('was-validated');
                        // Resetear el avatar
                        if (customerAvatarPreview) {
                            customerAvatarPreview.src = defaultAvatarUrl;
                        }
                        // Recargar la página para mostrar el nuevo cliente
                        setTimeout(() => {
                            window.location.reload();
                        }, 300);
                    } else {
                        return response.text();
                    }
                })
                .then(html => {
                    if (html) {
                        // Si hay HTML, probablemente hay errores de validación
                        alert('Error al crear el cliente. Por favor revise los datos.');
                        console.error('Validation errors occurred');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('Ocurrió un error al crear el cliente.');
                });
            }
        });
    }
    
    // 4. Status Filter functionality with smooth transition
    if (statusFilter) {
        statusFilter.addEventListener('change', function() {
            showTableLoading(true);
            const params = new URLSearchParams();
            params.append('statusFilter', this.value);
            
            // Mantener término de búsqueda si existe
            const currentSearchTerm = searchInput ? searchInput.value.trim() : '';
            if (currentSearchTerm) {
                params.append('searchTerm', currentSearchTerm);
            }
            
            // Transición suave en lugar de recarga agresiva
            setTimeout(() => {
                window.location.href = window.location.pathname + '?' + params.toString();
            }, 200);
        });
    }
    
    // 5. Search functionality with smooth transitions
    if (searchInput || mobileSearchInput) {
        let searchTimeout;
        
        // Establecer el valor inicial desde los parámetros de URL
        const urlParams = new URLSearchParams(window.location.search);
        const initialSearchTerm = urlParams.get('searchTerm');
        if (initialSearchTerm) {
            if (searchInput) searchInput.value = initialSearchTerm;
            if (mobileSearchInput) mobileSearchInput.value = initialSearchTerm;
        }
        
        // Sincronizar búsqueda móvil con desktop
        if (mobileSearchInput && searchInput) {
            // Sincronizar al escribir en móvil
            mobileSearchInput.addEventListener('input', function() {
                searchInput.value = this.value;
                searchInput.dispatchEvent(new Event('input'));
            });
            
            // Sincronizar al escribir en desktop
            searchInput.addEventListener('input', function() {
                mobileSearchInput.value = this.value;
            });
        }
        
        // Función para filtro en tiempo real (sin recarga)
        function filterTableClientSide(searchTerm) {
            const tbody = document.querySelector('#customersTable tbody');
            const rows = tbody.querySelectorAll('tr');
            
            rows.forEach(row => {
                const text = row.textContent.toLowerCase();
                const matches = searchTerm === '' || text.includes(searchTerm.toLowerCase());
                
                if (matches) {
                    row.style.display = '';
                    row.style.opacity = '1';
                } else {
                    row.style.opacity = '0';
                    setTimeout(() => {
                        if (row.style.opacity === '0') {
                            row.style.display = 'none';
                        }
                    }, 200);
                }
            });
        }
        
        const handleSearch = function() {
            clearTimeout(searchTimeout);
            const searchTerm = this.value.trim();
            
            // Filtro inmediato para respuesta instantánea
            filterTableClientSide(searchTerm);
            
            // Búsqueda en servidor con debounce más largo para mantener sincronía
            searchTimeout = setTimeout(() => {
                const params = new URLSearchParams();
                
                // Mantener el filtro de estado actual
                if (statusFilter && statusFilter.value) {
                    params.append('statusFilter', statusFilter.value);
                }
                
                // Agregar término de búsqueda si existe
                if (searchTerm) {
                    params.append('searchTerm', searchTerm);
                }
                
                // Actualizar URL sin recarga agresiva
                const newUrl = window.location.pathname + '?' + params.toString();
                window.history.replaceState({}, '', newUrl);
                
                // Actualizar enlaces de exportación móviles
                updateMobileExportLinks();
                
            }, 1500); // Solo actualizar URL después de 1.5 segundos de inactividad
        };
        
        if (searchInput) {
            searchInput.addEventListener('input', handleSearch);
        }
        if (mobileSearchInput) {
            mobileSearchInput.addEventListener('input', handleSearch);
        }
    }
    
    // 6. Update mobile export links
    function updateMobileExportLinks() {
        const params = new URLSearchParams();
        
        // Obtener valores de los filtros
        const statusValue = statusFilter ? statusFilter.value : '';
        const searchValue = mobileSearchInput ? mobileSearchInput.value : '';
        
        // Agregar parámetros si tienen valor
        if (statusValue) params.append('statusFilter', statusValue);
        if (searchValue) params.append('searchTerm', searchValue);
        
        const queryString = params.toString();
        const suffix = queryString ? '?' + queryString : '';
        
        // Actualizar enlaces de exportación móviles
        document.querySelectorAll('.mobile-export-link').forEach(link => {
            const baseUrl = link.href.split('?')[0];
            link.href = baseUrl + suffix;
        });
    }
    
    // Actualizar al cambiar filtros o búsqueda
    if (statusFilter) {
        statusFilter.addEventListener('change', updateMobileExportLinks);
    }
    
    // Función para mostrar estado de carga suave
    function showTableLoading(show) {
        const tbody = document.querySelector('#customersTable tbody');
        if (tbody) {
            tbody.style.opacity = show ? '0.5' : '1';
            tbody.style.pointerEvents = show ? 'none' : 'auto';
        }
    }
    
    // 6. Toggle Status functionality
    document.addEventListener('click', function(e) {
        if (e.target.closest('.toggle-status-btn')) {
            const button = e.target.closest('.toggle-status-btn');
            const customerId = button.dataset.customerId;
            const customerName = button.dataset.customerName;
            const currentStatus = button.dataset.currentStatus;
            const action = button.dataset.action;
            
            const actionText = action === 'activate' ? 'activar' : 'desactivar';
            const confirmMessage = `¿Está seguro que desea ${actionText} el cliente "${customerName}"?`;
            
            if (confirm(confirmMessage)) {
                toggleCustomerStatus(customerId, button);
            }
        }
    });
    
    // 7. Initialize customer table checkboxes
    initCustomerTable();
    
    // 8. Initialize tooltips
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[title]'));
    tooltipTriggerList.map(function(tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl, {
            boundary: document.body
        });
    });
    
    // 9. Initialize translation if available
    if (typeof translatePage === 'function') {
        translatePage();
    }
});

/**
 * Initialize customer table functionality
 */
function initCustomerTable() {
    // Select all checkbox
    const selectAllCheckbox = document.querySelector('.dt-checkboxes-select-all');
    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener('change', function() {
            const checkboxes = document.querySelectorAll('.dt-checkboxes');
            checkboxes.forEach(checkbox => {
                checkbox.checked = this.checked;
            });
        });
    }
}

/**
 * Toggle customer status (activate/deactivate)
 */
function toggleCustomerStatus(customerId, buttonElement) {
    // Get the anti-forgery token
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value || 
                  document.querySelector('meta[name="RequestVerificationToken"]')?.getAttribute('content');
    
    const formData = new URLSearchParams();
    if (token) {
        formData.append('__RequestVerificationToken', token);
    }
    
    // Disable button during request
    buttonElement.disabled = true;
    const originalHtml = buttonElement.innerHTML;
    buttonElement.innerHTML = '<i class="fas fa-spinner fa-spin"></i>';
    
    fetch(`/Customers/ToggleStatus/${customerId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: formData
    })
    .then(response => response.json())
    .then(result => {
        if (result.success) {
            // Show success notification
            showNotification(result.message, 'success');
            
            // Get current filter
            const currentFilter = document.getElementById('selectStatus')?.value || 'active';
            
            // If the new status doesn't match the current filter, remove the row with animation
            if ((currentFilter === 'active' && !result.newIsActiveState) || 
                (currentFilter === 'inactive' && result.newIsActiveState)) {
                const row = buttonElement.closest('tr');
                if (row) {
                    row.style.transition = 'all 0.3s ease';
                    row.style.opacity = '0';
                    row.style.transform = 'translateX(20px)';
                    setTimeout(() => {
                        row.remove();
                        
                        // Check if table is empty and show empty state
                        const tbody = document.querySelector('#customersTable tbody');
                        if (tbody && tbody.children.length === 0) {
                            tbody.innerHTML = `
                                <tr>
                                    <td colspan="8" class="text-center py-4">
                                        <div class="empty-state">
                                            <i class="fas fa-users fa-3x text-muted mb-3"></i>
                                            <p class="text-muted mb-0">No se encontraron clientes</p>
                                        </div>
                                    </td>
                                </tr>
                            `;
                        }
                    }, 300);
                }
            } else {
                // Update the status badge and button in the same row
                const row = buttonElement.closest('tr');
                if (row) {
                    const statusBadge = row.querySelector('.badge');
                    const statusButton = row.querySelector('.toggle-status-btn');
                    
                    if (result.newIsActiveState) {
                        // Update to active status
                        statusBadge.className = 'badge rounded-pill bg-success text-white px-3 py-2';
                        statusBadge.innerHTML = '<i class="fas fa-check-circle me-1"></i><span class="fw-medium">Activo</span>';
                        
                        statusButton.className = 'btn btn-sm btn-icon text-warning toggle-status-btn';
                        statusButton.title = 'Desactivar Cliente';
                        statusButton.dataset.currentStatus = 'active';
                        statusButton.dataset.action = 'deactivate';
                        statusButton.innerHTML = '<i class="fas fa-user-slash"></i>';
                    } else {
                        // Update to inactive status
                        statusBadge.className = 'badge rounded-pill bg-warning text-dark px-3 py-2';
                        statusBadge.innerHTML = '<i class="fas fa-pause-circle me-1"></i><span class="fw-medium">Inactivo</span>';
                        
                        statusButton.className = 'btn btn-sm btn-icon text-success toggle-status-btn';
                        statusButton.title = 'Activar Cliente';
                        statusButton.dataset.currentStatus = 'inactive';
                        statusButton.dataset.action = 'activate';
                        statusButton.innerHTML = '<i class="fas fa-user-check"></i>';
                    }
                }
            }
        } else {
            // Show error notification
            showNotification(result.message || 'Error al cambiar el estado del cliente', 'error');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showNotification('Error de conexión al cambiar el estado del cliente', 'error');
    })
    .finally(() => {
        // Re-enable button
        buttonElement.disabled = false;
        buttonElement.innerHTML = originalHtml;
    });
}

/**
 * Show notification toast
 */
function showNotification(message, type = 'info') {
    const toast = document.getElementById('notificationToast');
    const toastMessage = document.getElementById('notificationMessage');
    
    if (toast && toastMessage) {
        // Set message
        toastMessage.textContent = message;
        
        // Set toast type class
        toast.className = 'toast';
        if (type === 'success') {
            toast.classList.add('bg-success', 'text-white');
        } else if (type === 'error') {
            toast.classList.add('bg-danger', 'text-white');
        } else {
            toast.classList.add('bg-info', 'text-white');
        }
        
        // Show toast
        const bsToast = new bootstrap.Toast(toast);
        bsToast.show();
        
        // Remove classes after hiding
        toast.addEventListener('hidden.bs.toast', function() {
            toast.className = 'toast';
        });
    }
}

/**
 * Función auxiliar para manejar errores graciosamente
 */
function handleError(element, message) {
    console.error(message);
    if (element) {
        // Añadir clase de error o hacer algo visual si es apropiado
    }
    return false;
}

    // 7. Mobile Filters Functionality
    // Sincronizar filtros móviles con desktop
    function syncMobileFilters() {
        if (statusFilter && mobileStatusFilter) {
            mobileStatusFilter.value = statusFilter.value;
        }
    }
    
    // Sincronizar al abrir el offcanvas
    if (filtersOffcanvas) {
        filtersOffcanvas.addEventListener('show.bs.offcanvas', syncMobileFilters);
    }
    
    // Aplicar filtros desde móvil
    if (applyMobileFiltersBtn) {
        applyMobileFiltersBtn.addEventListener('click', function() {
            // Sincronizar valores con desktop
            if (mobileStatusFilter && statusFilter) {
                statusFilter.value = mobileStatusFilter.value;
            }
            
            // Cerrar offcanvas
            const offcanvasInstance = bootstrap.Offcanvas.getInstance(filtersOffcanvas);
            if (offcanvasInstance) {
                offcanvasInstance.hide();
            }
            
            // Aplicar filtros (trigger change event)
            if (statusFilter) {
                statusFilter.dispatchEvent(new Event('change'));
            }
        });
    }
});