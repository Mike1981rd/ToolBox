// Questionnaire Templates Management - Index Page
document.addEventListener('DOMContentLoaded', function() {
    'use strict';

    // Configuration
    const API_BASE_URL = '/api/questionnaire-templates';
    const PAGE_SIZE = 10;

    // State
    let currentPage = 1;
    let currentSearchTerm = '';
    let currentStatusFilter = 'active';
    let isLoading = false;

    // DOM Elements
    const loadingState = document.getElementById('loadingState');
    const templatesTableBody = document.getElementById('templatesTableBody');
    const searchInput = document.getElementById('searchInput');
    const statusFilter = document.getElementById('statusFilter');
    const paginationContainer = document.getElementById('paginationContainer');
    const prevPageLink = document.getElementById('prevPageLink');
    const nextPageLink = document.getElementById('nextPageLink');
    const currentPageSpan = document.getElementById('currentPage');

    // Initialize
    loadTemplates();
    setupEventListeners();

    function setupEventListeners() {
        // Search input
        if (searchInput) {
            searchInput.addEventListener('input', debounce(function() {
                currentSearchTerm = this.value.trim();
                currentPage = 1;
                loadTemplates();
            }, 500));

            // Prevent form submission on Enter
            searchInput.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    e.preventDefault();
                }
            });
        }

        // Status filter
        if (statusFilter) {
            statusFilter.addEventListener('change', function() {
                currentStatusFilter = this.value;
                currentPage = 1;
                loadTemplates();
            });
        }

        // Pagination
        if (prevPageLink) {
            prevPageLink.addEventListener('click', function(e) {
                e.preventDefault();
                if (currentPage > 1 && !isLoading) {
                    currentPage--;
                    loadTemplates();
                }
            });
        }

        if (nextPageLink) {
            nextPageLink.addEventListener('click', function(e) {
                e.preventDefault();
                if (!isLoading) {
                    currentPage++;
                    loadTemplates();
                }
            });
        }
    }

    async function loadTemplates() {
        if (isLoading) return;

        try {
            isLoading = true;
            showLoading(true);

            const params = new URLSearchParams({
                page: currentPage,
                pageSize: PAGE_SIZE,
                statusFilter: currentStatusFilter
            });

            if (currentSearchTerm) {
                params.append('searchTerm', currentSearchTerm);
            }

            const response = await fetch(`${API_BASE_URL}?${params}`);
            const data = await response.json();

            if (response.ok) {
                renderTemplates(data.templates);
                updatePagination(data);
            } else {
                showError('Error al cargar las plantillas');
            }
        } catch (error) {
            console.error('Error loading templates:', error);
            showError('Error de conexión al cargar las plantillas');
        } finally {
            isLoading = false;
            showLoading(false);
        }
    }

    function renderTemplates(templates) {
        if (!templatesTableBody) return;

        if (templates.length === 0) {
            templatesTableBody.innerHTML = `
                <tr>
                    <td colspan="6" class="text-center py-4">
                        <div class="text-muted">
                            <i class="fas fa-search fa-2x mb-3 d-block"></i>
                            <span>No se encontraron plantillas</span>
                        </div>
                    </td>
                </tr>
            `;
            return;
        }

        templatesTableBody.innerHTML = templates.map(template => `
            <tr>
                <td>
                    <div class="fw-bold">${escapeHtml(template.title)}</div>
                </td>
                <td>
                    <div class="text-muted">
                        ${template.shortDescription ? escapeHtml(template.shortDescription) : '<em>Sin descripción</em>'}
                    </div>
                </td>
                <td>
                    <span class="badge bg-primary">${template.questionCount}</span>
                </td>
                <td>
                    <span class="badge ${template.isActive ? 'bg-success' : 'bg-secondary'}">
                        ${template.isActive ? 'Activa' : 'Inactiva'}
                    </span>
                </td>
                <td>
                    <small class="text-muted">${formatDate(template.createdAt)}</small>
                </td>
                <td>
                    <div class="d-inline-block text-nowrap">
                        <a href="/QuestionnaireTemplates/Edit/${template.id}" class="btn btn-sm btn-icon" title="Editar">
                            <i class="fas fa-edit"></i>
                        </a>
                        <a href="/QuestionnaireTemplates/Assign/${template.id}" class="btn btn-sm btn-icon text-info" title="Asignar">
                            <i class="fas fa-paper-plane"></i>
                        </a>
                        <a href="/QuestionnaireTemplates/AllAssignments" class="btn btn-sm btn-icon" title="Ver Asignaciones">
                            <i class="fas fa-users"></i>
                        </a>
                        <button type="button" class="btn btn-sm btn-icon ${template.isActive ? 'text-warning' : 'text-success'}" 
                                onclick="toggleTemplateStatus(${template.id}, this)" 
                                title="${template.isActive ? 'Desactivar' : 'Activar'}">
                            <i class="fas fa-${template.isActive ? 'eye-slash' : 'eye'}"></i>
                        </button>
                    </div>
                </td>
            </tr>
        `).join('');
    }

    function updatePagination(data) {
        if (!paginationContainer) return;

        const hasPrevious = data.hasPreviousPage;
        const hasNext = data.hasNextPage;

        // Update current page
        if (currentPageSpan) {
            currentPageSpan.textContent = data.currentPage;
        }

        // Update prev button
        if (prevPageLink) {
            const prevItem = prevPageLink.closest('.page-item');
            if (hasPrevious) {
                prevItem.classList.remove('disabled');
            } else {
                prevItem.classList.add('disabled');
            }
        }

        // Update next button
        if (nextPageLink) {
            const nextItem = nextPageLink.closest('.page-item');
            if (hasNext) {
                nextItem.classList.remove('disabled');
            } else {
                nextItem.classList.add('disabled');
            }
        }

        // Show/hide pagination
        if (data.totalPages > 1) {
            paginationContainer.style.display = 'block';
        } else {
            paginationContainer.style.display = 'none';
        }
    }

    // Global function for template status toggle
    window.toggleTemplateStatus = async function(templateId, button) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
            
            const response = await fetch(`${API_BASE_URL}/${templateId}/toggle-status`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'X-CSRF-TOKEN': token
                }
            });

            const result = await response.json();

            if (result.success) {
                showSuccess(result.message);
                
                // Update button and row based on current filter
                const row = button.closest('tr');
                if ((currentStatusFilter === 'active' && !result.newIsActiveState) || 
                    (currentStatusFilter === 'inactive' && result.newIsActiveState)) {
                    // Remove row with animation if it doesn't match current filter
                    row.style.opacity = '0';
                    setTimeout(() => {
                        loadTemplates(); // Refresh the list
                    }, 300);
                } else {
                    // Just refresh to update the status
                    loadTemplates();
                }
            } else {
                showError(result.message || 'Error al cambiar el estado de la plantilla');
            }
        } catch (error) {
            console.error('Error toggling template status:', error);
            showError('Error de conexión al cambiar el estado');
        }
    };

    function showLoading(show) {
        if (loadingState) {
            loadingState.style.display = show ? 'block' : 'none';
        }
    }

    function showSuccess(message) {
        showToast(message, 'success');
    }

    function showError(message) {
        showToast(message, 'error');
    }

    function showToast(message, type = 'info') {
        const toast = document.getElementById('notificationToast');
        const toastBody = document.getElementById('notificationToastBody');
        
        if (toast && toastBody) {
            // Update icon based on type
            const icon = toast.querySelector('.toast-header i');
            if (icon) {
                icon.className = `fas fa-${type === 'success' ? 'check-circle text-success' : type === 'error' ? 'exclamation-circle text-danger' : 'info-circle text-primary'} me-2`;
            }
            
            toastBody.textContent = message;
            
            const bsToast = new bootstrap.Toast(toast);
            bsToast.show();
        }
    }

    function debounce(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }

    function escapeHtml(text) {
        const map = {
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            '"': '&quot;',
            "'": '&#039;'
        };
        return text.replace(/[&<>"']/g, function(m) { return map[m]; });
    }

    function formatDate(dateString) {
        const date = new Date(dateString);
        return date.toLocaleDateString('es-ES', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
    }
});