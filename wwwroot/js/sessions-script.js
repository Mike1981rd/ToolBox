// Sessions management script
document.addEventListener('DOMContentLoaded', function() {
    const statusFilter = document.getElementById('statusFilter');
    const mobileSearchInput = document.getElementById('mobileSearchInput');
    const hiddenMobileSearch = document.getElementById('hiddenMobileSearch');
    const currentFilter = statusFilter ? statusFilter.value : 'active';
    
    // Desktop status filter change
    if (statusFilter) {
        statusFilter.addEventListener('change', function() {
            const searchQuery = document.querySelector('input[name="searchQuery"]').value;
            const params = new URLSearchParams();
            params.append('statusFilter', this.value);
            if (searchQuery) params.append('searchQuery', searchQuery);
            window.location.href = window.location.pathname + '?' + params.toString();
        });
    }

    // Mobile search sync
    if (mobileSearchInput && hiddenMobileSearch) {
        mobileSearchInput.addEventListener('input', function() {
            hiddenMobileSearch.value = this.value;
        });
    }

    // Toggle session status
    document.addEventListener('click', function(e) {
        if (e.target.closest('.btn-toggle-status')) {
            e.preventDefault();
            const button = e.target.closest('.btn-toggle-status');
            const sessionId = button.dataset.sessionId;
            toggleSessionStatus(sessionId, button);
        }
    });

    // Toggle session status function
    function toggleSessionStatus(sessionId, button) {
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        
        fetch(`/Sessions/ToggleStatus`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': token
            },
            body: JSON.stringify({ id: sessionId })
        })
        .then(response => response.json())
        .then(result => {
            if (result.success) {
                // Show success toast
                showToast(result.message, 'success');
                
                // Update UI
                const row = button.closest('tr');
                const statusBadge = row.querySelector('.badge');
                const icon = button.querySelector('i');
                const text = button.querySelector('span');
                
                if (result.newIsActiveState) {
                    statusBadge.classList.remove('bg-secondary');
                    statusBadge.classList.add('bg-success');
                    statusBadge.textContent = 'Activo';
                    icon.className = 'fas fa-toggle-off me-2';
                    text.textContent = 'Desactivar';
                    button.dataset.currentStatus = 'true';
                } else {
                    statusBadge.classList.remove('bg-success');
                    statusBadge.classList.add('bg-secondary');
                    statusBadge.textContent = 'Inactivo';
                    icon.className = 'fas fa-toggle-on me-2';
                    text.textContent = 'Activar';
                    button.dataset.currentStatus = 'false';
                }

                // Remove row if it doesn't match current filter
                if ((currentFilter === 'active' && !result.newIsActiveState) || 
                    (currentFilter === 'inactive' && result.newIsActiveState)) {
                    row.style.opacity = '0';
                    row.style.transition = 'opacity 0.3s ease';
                    setTimeout(() => {
                        row.remove();
                        // Check if table is empty
                        checkEmptyTable();
                    }, 300);
                }
            } else {
                showToast(result.message || 'Error al cambiar el estado', 'error');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showToast('Error al procesar la solicitud', 'error');
        });
    }

    // Check if table is empty and show message
    function checkEmptyTable() {
        const tbody = document.querySelector('#sessionsTable tbody');
        const rows = tbody.querySelectorAll('tr[data-session-id]');
        
        if (rows.length === 0) {
            tbody.innerHTML = `
                <tr>
                    <td colspan="9" class="text-center py-4">
                        <div class="text-muted">
                            <i class="fas fa-inbox fa-2x mb-3 d-block"></i>
                            <span data-translate-key="no_sessions_found">No se encontraron sesiones</span>
                        </div>
                    </td>
                </tr>
            `;
        }
    }

    // Show toast notification
    function showToast(message, type = 'info') {
        const toastElement = document.getElementById('notificationToast');
        if (toastElement) {
            const toastBody = toastElement.querySelector('.toast-body');
            toastBody.textContent = message;
            
            // Remove previous color classes
            toastElement.classList.remove('bg-success', 'bg-danger', 'bg-info', 'text-white');
            
            // Add color based on type
            if (type === 'success') {
                toastElement.classList.add('bg-success', 'text-white');
            } else if (type === 'error') {
                toastElement.classList.add('bg-danger', 'text-white');
            }
            
            const toast = new bootstrap.Toast(toastElement);
            toast.show();
        }
    }

    // Apply translations if available
    if (typeof applyTranslations === 'function') {
        applyTranslations();
    }
});