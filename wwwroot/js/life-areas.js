/**
 * Life Areas Module JavaScript
 * Handles life areas CRUD operations, icon selection and offcanvas functionality
 */

document.addEventListener('DOMContentLoaded', function () {
    // Elements
    const addEditOffcanvasElement = document.getElementById('addEditLifeAreaOffcanvas');
    const offcanvasForm = document.getElementById('addEditLifeAreaForm');
    const offcanvasTitle = document.getElementById('addEditLifeAreaOffcanvasLabel');
    const lifeAreaIdInput = document.getElementById('lifeAreaId');
    const submitButton = document.getElementById('submitBtn');
    const submitButtonText = document.getElementById('submitBtnText');
    const spinner = submitButton.querySelector('.spinner-border');
    const iconSelectorModal = new bootstrap.Modal(document.getElementById('iconSelectorModal'));
    const offcanvas = new bootstrap.Offcanvas(addEditOffcanvasElement);
    
    // Filter elements
    const searchInput = document.getElementById('searchLifeArea');
    const statusFilter = document.getElementById('selectStatus');
    
    // Variables for icon selection
    let selectedIcon = 'fas fa-circle';
    let availableIcons = [];
    
    // Load available icons
    loadAvailableIcons();
    
    // Setup search filter (client-side only)
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const searchTerm = this.value.toLowerCase().trim();
            const tableRows = document.querySelectorAll('#lifeAreasTable tbody tr');
            let visibleCount = 0;
            
            tableRows.forEach(row => {
                // Skip no data row
                if (row.cells.length === 1) return;
                
                // Get text from title and description
                const titleCell = row.cells[1];
                const title = titleCell.querySelector('.fw-semibold')?.textContent.toLowerCase() || '';
                const description = titleCell.querySelector('.text-muted')?.textContent.toLowerCase() || '';
                
                // Check if search term matches
                const matches = title.includes(searchTerm) || description.includes(searchTerm);
                
                // Show/hide row based on search match
                row.style.display = matches ? '' : 'none';
                
                if (matches) visibleCount++;
            });
            
            // Update visible count
            updateTableInfo(visibleCount);
            
            // Show no results message if needed
            handleNoResults(visibleCount);
        });
        
        // Prevent form submission on Enter
        searchInput.addEventListener('keypress', function(e) {
            if (e.key === 'Enter') {
                e.preventDefault();
            }
        });
    }
    
    // Setup status filter
    if (statusFilter) {
        statusFilter.addEventListener('change', function() {
            const params = new URLSearchParams();
            const statusValue = this.value;
            
            // Add status filter parameter
            if (statusValue) params.append('statusFilter', statusValue);
            
            // Redirect with new parameters - use current path to maintain correct routing
            const currentPath = window.location.pathname;
            window.location.href = currentPath + (params.toString() ? '?' + params.toString() : '');
        });
    }
    
    function updateTableInfo(visibleCount) {
        const totalEntries = document.querySelectorAll('#lifeAreasTable tbody tr').length - 1; // Exclude no-results row if exists
        document.getElementById('showingFrom').textContent = visibleCount > 0 ? '1' : '0';
        document.getElementById('showingTo').textContent = visibleCount;
        document.getElementById('totalEntries').textContent = totalEntries;
    }
    
    function handleNoResults(visibleCount) {
        const tbody = document.querySelector('#lifeAreasTable tbody');
        const noResultsRow = tbody.querySelector('.no-results-row');
        
        if (visibleCount === 0) {
            if (!noResultsRow) {
                const newRow = tbody.insertRow();
                newRow.className = 'no-results-row';
                newRow.innerHTML = '<td colspan="3" class="text-center">No se encontraron áreas de vida que coincidan con la búsqueda</td>';
            }
        } else {
            if (noResultsRow) {
                noResultsRow.remove();
            }
        }
    }
    
    // Setup icon color synchronization
    const colorPicker = document.getElementById('lifeAreaColor');
    const colorText = document.getElementById('lifeAreaColorText');
    const iconPreview = document.getElementById('iconPreview');
    const selectedIconPreview = document.getElementById('selectedIconPreview');
    
    colorPicker.addEventListener('input', function() {
        const color = this.value;
        colorText.value = color;
        updateIconPreview(selectedIcon, color);
    });
    
    colorText.addEventListener('input', function() {
        const color = this.value;
        if (/^#[0-9A-Fa-f]{6}$/.test(color)) {
            colorPicker.value = color;
            updateIconPreview(selectedIcon, color);
        }
    });
    
    // Setup icon search and filter
    const iconSearch = document.getElementById('iconSearch');
    const iconCategoryFilter = document.getElementById('iconCategoryFilter');
    
    iconSearch.addEventListener('input', filterIcons);
    iconCategoryFilter.addEventListener('change', filterIcons);
    
    // Add new life area button
    const addNewLifeAreaBtn = document.getElementById('addNewLifeAreaBtn');
    if (addNewLifeAreaBtn) {
        addNewLifeAreaBtn.addEventListener('click', function () {
            resetForm();
            offcanvasTitle.textContent = 'Agregar Nueva Área de Vida';
            submitButtonText.textContent = 'Agregar';
        });
    }
    
    // Edit life area buttons
    document.addEventListener('click', function(e) {
        if (e.target.closest('.edit-life-area-btn')) {
            const button = e.target.closest('.edit-life-area-btn');
            const areaId = button.dataset.id;
            loadLifeAreaForEdit(areaId);
        }
    });
    
    // Form submission
    offcanvasForm.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        if (!validateForm()) {
            return;
        }
        
        const formData = new FormData(offcanvasForm);
        const data = {
            Id: parseInt(formData.get('Id')),
            Title: formData.get('Title'),
            Description: formData.get('Description'),
            IconClass: formData.get('IconClass'),
            IconColor: formData.get('IconColor'),
            IsActive: formData.get('IsActive') === 'true',
            DisplayOrder: parseInt(formData.get('DisplayOrder') || 0)
        };
        
        const isEdit = data.Id > 0;
        
        // Show loading state
        showLoading(true);
        
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            const url = isEdit ? 
                `/LifeAreas/Update/${data.Id}` : 
                '/LifeAreas/Create';
            
            const response = await fetch(url, {
                method: isEdit ? 'PUT' : 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify(data)
            });
            
            const result = await response.json();
            
            if (result.success) {
                showToast(result.message, 'success');
                offcanvas.hide();
                await refreshTable();
            } else {
                showToast(result.message || 'Error al guardar', 'error');
                if (result.errors) {
                    showValidationErrors(result.errors);
                }
            }
        } catch (error) {
            showToast('Error de conexión', 'error');
        } finally {
            showLoading(false);
        }
    });
    
    // Variable to store current toggle button
    let currentToggleButton = null;
    
    // Toggle status buttons
    document.addEventListener('click', async function(e) {
        if (e.target.closest('.toggle-status-btn')) {
            const button = e.target.closest('.toggle-status-btn');
            currentToggleButton = button; // Store reference to button
            const areaId = button.dataset.areaId;
            const areaTitle = button.dataset.areaTitle;
            const action = button.dataset.action;
            
            if (confirm(`¿Está seguro de que desea ${action === 'deactivate' ? 'desactivar' : 'activar'} el área "${areaTitle}"?`)) {
                await toggleLifeAreaStatus(areaId, button);
            }
        }
    });
    
    // Icon selector confirm button
    document.getElementById('confirmIconSelection').addEventListener('click', function() {
        const selectedIconElement = document.querySelector('.icon-grid .icon-option.selected');
        if (selectedIconElement) {
            selectedIcon = selectedIconElement.dataset.icon;
            document.getElementById('lifeAreaIconClass').value = selectedIcon;
            updateIconPreview(selectedIcon, colorPicker.value);
        }
    });
    
    // Functions
    async function loadAvailableIcons() {
        try {
            const response = await fetch('/LifeAreas/GetIconOptions');
            availableIcons = await response.json();
            renderIconGrid();
        } catch (error) {
            console.error('Error loading icons:', error);
        }
    }
    
    function renderIconGrid() {
        const iconGrid = document.getElementById('iconGrid');
        const filteredIcons = filterIconsList();
        
        iconGrid.innerHTML = filteredIcons.map(icon => `
            <div class="icon-option p-3 text-center cursor-pointer" data-icon="${icon.class}" data-name="${icon.name}" data-category="${icon.category}">
                <i class="${icon.class} fa-2x mb-2"></i>
                <div class="small">${icon.name}</div>
            </div>
        `).join('');
        
        // Add click handlers
        iconGrid.querySelectorAll('.icon-option').forEach(option => {
            option.addEventListener('click', function() {
                iconGrid.querySelectorAll('.icon-option').forEach(o => o.classList.remove('selected'));
                this.classList.add('selected');
            });
        });
    }
    
    function filterIcons() {
        renderIconGrid();
    }
    
    function filterIconsList() {
        const searchTerm = iconSearch.value.toLowerCase();
        const category = iconCategoryFilter.value;
        
        return availableIcons.filter(icon => {
            const matchesSearch = !searchTerm || 
                icon.name.toLowerCase().includes(searchTerm) || 
                icon.class.toLowerCase().includes(searchTerm);
            const matchesCategory = !category || icon.category === category;
            return matchesSearch && matchesCategory;
        });
    }
    
    function updateIconPreview(iconClass, color) {
        selectedIconPreview.className = iconClass;
        selectedIconPreview.style.color = color;
        selectedIconPreview.style.fontSize = '2rem';
        iconPreview.style.backgroundColor = color;
    }
    
    async function loadLifeAreaForEdit(id) {
        try {
            const response = await fetch(`/LifeAreas/GetById/${id}`);
            const data = await response.json();
            
            resetForm();
            
            // Populate form
            lifeAreaIdInput.value = data.id;
            document.getElementById('lifeAreaTitle').value = data.title;
            document.getElementById('lifeAreaDescription').value = data.description || '';
            document.getElementById('lifeAreaIconClass').value = data.iconClass;
            document.getElementById('lifeAreaColor').value = data.iconColor;
            document.getElementById('lifeAreaColorText').value = data.iconColor;
            document.getElementById('lifeAreaStatus').value = data.isActive.toString();
            document.getElementById('lifeAreaDisplayOrder').value = data.displayOrder;
            
            // Update icon preview
            selectedIcon = data.iconClass;
            updateIconPreview(data.iconClass, data.iconColor);
            
            // Update UI
            offcanvasTitle.textContent = 'Editar Área de Vida';
            submitButtonText.textContent = 'Guardar Cambios';
            
        } catch (error) {
            showToast('Error al cargar los datos', 'error');
        }
    }
    
    async function toggleLifeAreaStatus(id, button) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            const row = button.closest('tr');
            
            const response = await fetch(`/LifeAreas/ToggleStatus/${id}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: `__RequestVerificationToken=${encodeURIComponent(token)}`
            });
            
            const result = await response.json();
            
            if (result.success) {
                // Always update button status
                if (result.newIsActiveState) {
                    // Now active
                    button.className = 'btn btn-sm btn-icon text-warning toggle-status-btn';
                    button.title = 'Desactivar área de vida';
                    button.dataset.currentStatus = 'active';
                    button.dataset.action = 'deactivate';
                    button.innerHTML = '<i class="fas fa-eye-slash"></i>';
                } else {
                    // Now inactive
                    button.className = 'btn btn-sm btn-icon text-success toggle-status-btn';
                    button.title = 'Activar área de vida';
                    button.dataset.currentStatus = 'inactive';
                    button.dataset.action = 'activate';
                    button.innerHTML = '<i class="fas fa-eye"></i>';
                }
                
                showToast(result.message, 'success');
                
                // Reload page after short delay to update view based on filters
                setTimeout(() => {
                    window.location.reload();
                }, 1000);
            } else {
                showToast(result.message, 'error');
            }
        } catch (error) {
            showToast('Error al cambiar el estado', 'error');
        }
    }
    
    async function refreshTable() {
        // Reload the page but maintain filter state
        const currentStatus = statusFilter.value;
        const searchTerm = searchInput.value;
        
        // Save search term temporarily
        if (searchTerm) {
            sessionStorage.setItem('lifeAreaSearchTerm', searchTerm);
        }
        
        window.location.reload();
    }
    
    function resetForm() {
        offcanvasForm.reset();
        lifeAreaIdInput.value = '0';
        selectedIcon = 'fas fa-circle';
        colorPicker.value = '#6c757d';
        colorText.value = '#6c757d';
        updateIconPreview(selectedIcon, '#6c757d');
        clearValidationErrors();
    }
    
    function validateForm() {
        clearValidationErrors();
        let isValid = true;
        
        const title = document.getElementById('lifeAreaTitle').value.trim();
        if (!title) {
            showFieldError('lifeAreaTitle', 'El título es requerido');
            isValid = false;
        }
        
        const colorText = document.getElementById('lifeAreaColorText').value;
        if (!/^#[0-9A-Fa-f]{6}$/.test(colorText)) {
            showFieldError('lifeAreaColorText', 'Formato de color inválido');
            isValid = false;
        }
        
        return isValid;
    }
    
    function showFieldError(fieldId, message) {
        const field = document.getElementById(fieldId);
        field.classList.add('is-invalid');
        const feedback = field.nextElementSibling;
        if (feedback && feedback.classList.contains('invalid-feedback')) {
            feedback.textContent = message;
        }
    }
    
    function clearValidationErrors() {
        offcanvasForm.querySelectorAll('.is-invalid').forEach(field => {
            field.classList.remove('is-invalid');
        });
    }
    
    function showValidationErrors(errors) {
        errors.forEach(error => {
            const fieldName = error.key.charAt(0).toLowerCase() + error.key.slice(1);
            const field = document.querySelector(`[name="${fieldName}"]`);
            if (field) {
                showFieldError(field.id, error.errorMessage);
            }
        });
    }
    
    function showLoading(show) {
        submitButton.disabled = show;
        spinner.classList.toggle('d-none', !show);
    }
    
    function showToast(message, type = 'success') {
        // Create toast element if it doesn't exist
        let toastContainer = document.querySelector('.toast-container');
        if (!toastContainer) {
            toastContainer = document.createElement('div');
            toastContainer.className = 'toast-container position-fixed top-0 end-0 p-3';
            toastContainer.style.zIndex = '1050';
            document.body.appendChild(toastContainer);
        }
        
        const toastId = 'toast-' + Date.now();
        const toastHtml = `
            <div id="${toastId}" class="toast align-items-center text-white bg-${type === 'success' ? 'success' : 'danger'} border-0" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="d-flex">
                    <div class="toast-body">
                        <i class="fas fa-${type === 'success' ? 'check-circle' : 'exclamation-triangle'} me-2"></i>
                        ${message}
                    </div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            </div>
        `;
        
        toastContainer.insertAdjacentHTML('beforeend', toastHtml);
        const toastElement = document.getElementById(toastId);
        const toast = new bootstrap.Toast(toastElement, { autohide: true, delay: 3000 });
        toast.show();
        
        // Remove toast element after it's hidden
        toastElement.addEventListener('hidden.bs.toast', function() {
            toastElement.remove();
        });
    }
});

// CSS for icon selector
const style = document.createElement('style');
style.textContent = `
    .icon-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(100px, 1fr));
        gap: 10px;
    }
    
    .icon-option {
        border: 2px solid transparent;
        border-radius: 8px;
        transition: all 0.2s;
        cursor: pointer;
    }
    
    .icon-option:hover {
        background-color: #f8f9fa;
        border-color: #dee2e6;
    }
    
    .icon-option.selected {
        background-color: #e7f1ff;
        border-color: #0d6efd;
    }
    
    .cursor-pointer {
        cursor: pointer;
    }
`;
document.head.appendChild(style);