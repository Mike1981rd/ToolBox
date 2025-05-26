/**
 * Topics Management JavaScript
 * Handles CRUD operations for the Topics module
 */

let topicsData = [];
let currentEditingTopicId = 0;
let currentPage = 1;
let pageSize = 10;
let totalCount = 0;
let currentSearchTerm = '';
let currentStatusFilter = 'active';
let searchDebounceTimer = null;

document.addEventListener('DOMContentLoaded', function() {
    initializeTopicsModule();
});

function initializeTopicsModule() {
    // Set initial values from elements
    const pageSizeSelect = document.getElementById('pageSize');
    if (pageSizeSelect) {
        pageSize = parseInt(pageSizeSelect.value);
        
        // Add page size change listener
        pageSizeSelect.addEventListener('change', function() {
            pageSize = parseInt(this.value);
            currentPage = 1; // Reset to first page
            loadTopics();
        });
    }
    
    const statusFilter = document.getElementById('statusFilter');
    if (statusFilter) {
        currentStatusFilter = statusFilter.value;
        
        // Add status filter change listener
        statusFilter.addEventListener('change', function() {
            currentStatusFilter = this.value;
            currentPage = 1; // Reset to first page
            loadTopics();
        });
    }
    
    // Initialize search functionality with client-side filtering
    const searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const searchTerm = this.value.toLowerCase().trim();
            
            // Client-side filtering for instant results
            if (topicsData.length > 0) {
                const tableRows = document.querySelectorAll('#topicsTableBody tr');
                let visibleCount = 0;
                
                tableRows.forEach(row => {
                    // Skip no data row
                    if (row.cells.length === 1) return;
                    
                    // Get text from name and description
                    const topicCell = row.cells[1]; // Topic column
                    const name = topicCell.querySelector('.fw-semibold')?.textContent.toLowerCase() || '';
                    const description = topicCell.querySelector('.text-muted')?.textContent.toLowerCase() || '';
                    
                    // Check if search term matches
                    const matches = name.includes(searchTerm) || description.includes(searchTerm);
                    
                    // Show/hide row with smooth transition
                    if (matches) {
                        row.style.display = '';
                        row.style.opacity = '1';
                        visibleCount++;
                    } else {
                        row.style.opacity = '0';
                        setTimeout(() => {
                            row.style.display = 'none';
                        }, 200);
                    }
                });
                
                // Show no results message if needed
                if (visibleCount === 0) {
                    const tbody = document.querySelector('#topicsTableBody');
                    const noResultsRow = tbody.querySelector('.no-results-row');
                    
                    if (!noResultsRow) {
                        const newRow = tbody.insertRow();
                        newRow.className = 'no-results-row';
                        newRow.innerHTML = '<td colspan="4" class="text-center">No se encontraron temas que coincidan con la búsqueda</td>';
                    }
                } else {
                    const noResultsRow = document.querySelector('.no-results-row');
                    if (noResultsRow) {
                        noResultsRow.remove();
                    }
                }
            }
            
            // Also update server-side search after debounce for pagination
            clearTimeout(searchDebounceTimer);
            searchDebounceTimer = setTimeout(() => {
                currentSearchTerm = this.value.trim();
                currentPage = 1;
                loadTopics();
            }, 500);
        });
    }
    
    // Load initial topics data
    loadTopics();
    
    // Set up event listeners
    setupEventListeners();
}

function setupEventListeners() {
    // Add New Topic button
    const addNewTopicBtn = document.getElementById('addNewTopicBtn');
    if (addNewTopicBtn) {
        addNewTopicBtn.addEventListener('click', function() {
            openAddTopicOffcanvas();
        });
    }
    
    // Form submission
    const topicForm = document.getElementById('addEditTopicForm');
    if (topicForm) {
        topicForm.addEventListener('submit', function(e) {
            e.preventDefault();
            saveTopic();
        });
    }
    
    // Offcanvas reset on close
    const offcanvas = document.getElementById('addEditTopicOffcanvas');
    if (offcanvas) {
        offcanvas.addEventListener('hidden.bs.offcanvas', function() {
            resetForm();
        });
    }
}

async function loadTopics() {
    // Show loading state
    showLoadingState();
    
    try {
        // Build query parameters
        const params = new URLSearchParams({
            page: currentPage,
            pageSize: pageSize,
            searchTerm: currentSearchTerm,
            statusFilter: currentStatusFilter
        });
        
        // Make AJAX call to get topics
        console.log('Fetching topics with params:', params.toString());
        const response = await fetch(`/Topics/GetTopics?${params}`);
        
        if (!response.ok) {
            console.error('Response not OK:', response.status, response.statusText);
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        // Check if response is JSON
        const contentType = response.headers.get("content-type");
        if (!contentType || !contentType.includes("application/json")) {
            const text = await response.text();
            console.error('Non-JSON response:', text.substring(0, 200));
            throw new Error("Server returned non-JSON response");
        }
        
        const data = await response.json();
        
        if (data.success) {
            topicsData = data.topics || data.data || [];
            totalCount = data.totalCount || topicsData.length;
            renderTopicsTable();
            updatePagination();
            updateTableInfo();
        } else {
            console.error('Error loading topics:', data.message);
            topicsData = []; // Ensure topicsData is an empty array
            renderTopicsTable(); // Show empty state
            showError(data.message || 'Error loading topics');
        }
    } catch (error) {
        console.error('Error:', error);
        topicsData = []; // Ensure topicsData is an empty array
        renderTopicsTable(); // Show empty state
        showError('Error loading topics: ' + error.message);
    }
}

function renderTopicsTable() {
    const tbody = document.getElementById('topicsTableBody');
    if (!tbody) return;
    
    // Ensure topicsData is an array
    if (!Array.isArray(topicsData)) {
        topicsData = [];
    }
    
    // Fade out existing content
    tbody.style.transition = 'opacity 0.2s ease';
    tbody.style.opacity = '0';
    
    setTimeout(() => {
        if (topicsData.length === 0) {
            tbody.innerHTML = `
                <tr>
                    <td colspan="4" class="text-center text-muted py-4">
                        <i class="fas fa-search fa-2x mb-2 d-block"></i>
                        <span data-translate-key="no_topics_found">No topics found</span>
                    </td>
                </tr>
            `;
        } else {
            tbody.innerHTML = topicsData.map(topic => `
        <tr>
            <td class="dt-checkboxes-cell">
                <input type="checkbox" class="form-check-input dt-checkboxes" value="${topic.id}">
            </td>
            <td>
                <div class="d-flex justify-content-start align-items-center">
                    <div class="avatar-wrapper me-3">
                        <div class="avatar avatar-sm ${topic.iconBackground || 'bg-label-primary'} bg-opacity-25">
                            <i class="${topic.iconClass || 'fas fa-lightbulb'}"></i>
                        </div>
                    </div>
                    <div class="d-flex flex-column">
                        <span class="text-body text-truncate fw-semibold">${escapeHtml(topic.name)}</span>
                        <small class="text-muted">${escapeHtml(topic.description || 'No description')}</small>
                    </div>
                </div>
            </td>
            <td>
                <span class="badge bg-${topic.isActive ? 'success' : 'secondary'}">
                    ${topic.isActive ? getTranslation('status_active') || 'Active' : getTranslation('status_inactive') || 'Inactive'}
                </span>
            </td>
            <td>
                <div class="d-inline-block text-nowrap">
                    <button class="btn btn-sm btn-icon edit-topic-btn" data-id="${topic.id}" title="Edit Topic">
                        <i class="fas fa-edit"></i>
                    </button>
                    <button class="btn btn-sm btn-icon delete-topic-btn" data-id="${topic.id}" title="Delete Topic">
                        <i class="fas fa-trash-alt text-danger"></i>
                    </button>
                </div>
            </td>
        </tr>
            `).join('');
        }
        
        // Fade in new content
        tbody.style.opacity = '1';
        
        // Attach event listeners to action buttons
        attachActionListeners();
    }, 200);
}

function updatePagination() {
    const paginationContainer = document.getElementById('paginationContainer');
    if (!paginationContainer) return;
    
    const totalPages = Math.ceil(totalCount / pageSize);
    
    if (totalPages <= 1) {
        paginationContainer.innerHTML = '';
        return;
    }
    
    let paginationHTML = '';
    
    // Previous button
    paginationHTML += `
        <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
            <a class="page-link" href="#" data-page="${currentPage - 1}" aria-label="Previous">
                <span aria-hidden="true">&laquo;</span>
            </a>
        </li>
    `;
    
    // Page numbers
    for (let i = 1; i <= totalPages; i++) {
        if (i === 1 || i === totalPages || (i >= currentPage - 2 && i <= currentPage + 2)) {
            paginationHTML += `
                <li class="page-item ${i === currentPage ? 'active' : ''}">
                    <a class="page-link" href="#" data-page="${i}">${i}</a>
                </li>
            `;
        } else if (i === currentPage - 3 || i === currentPage + 3) {
            paginationHTML += '<li class="page-item disabled"><span class="page-link">...</span></li>';
        }
    }
    
    // Next button
    paginationHTML += `
        <li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">
            <a class="page-link" href="#" data-page="${currentPage + 1}" aria-label="Next">
                <span aria-hidden="true">&raquo;</span>
            </a>
        </li>
    `;
    
    paginationContainer.innerHTML = paginationHTML;
    
    // Add pagination click handlers
    paginationContainer.querySelectorAll('.page-link').forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            const page = parseInt(this.dataset.page);
            if (!isNaN(page) && page >= 1 && page <= totalPages && page !== currentPage) {
                currentPage = page;
                loadTopics();
            }
        });
    });
}

function updateTableInfo() {
    const infoElement = document.getElementById('topicsTableInfo');
    if (infoElement) {
        const start = totalCount > 0 ? ((currentPage - 1) * pageSize) + 1 : 0;
        const end = Math.min(currentPage * pageSize, totalCount);
        infoElement.textContent = `Showing ${start} to ${end} of ${totalCount} entries`;
    }
}

function attachActionListeners() {
    // Edit buttons
    document.querySelectorAll('.edit-topic-btn').forEach(btn => {
        btn.addEventListener('click', function() {
            const topicId = parseInt(this.getAttribute('data-id'));
            editTopic(topicId);
        });
    });
    
    // Delete buttons
    document.querySelectorAll('.delete-topic-btn').forEach(btn => {
        btn.addEventListener('click', function() {
            const topicId = parseInt(this.getAttribute('data-id'));
            deleteTopic(topicId);
        });
    });
}

function openAddTopicOffcanvas() {
    currentEditingTopicId = 0;
    resetForm();
    
    // Update offcanvas title
    updateOffcanvasTitle('offcanvas_titles.addNewTopic');
    
    // Update submit button text
    updateSubmitButton('buttons.save');
    
    // Show offcanvas
    const offcanvas = new bootstrap.Offcanvas(document.getElementById('addEditTopicOffcanvas'));
    offcanvas.show();
}

function editTopic(topicId) {
    const topic = topicsData.find(t => t.id === topicId);
    if (!topic) {
        showError('Topic not found');
        return;
    }
    
    currentEditingTopicId = topicId;
    
    // Populate form with topic data
    document.getElementById('topicId').value = topic.id;
    document.getElementById('topicName').value = topic.name;
    document.getElementById('topicDescription').value = topic.description || '';
    document.getElementById('topicStatus').value = topic.isActive ? 'active' : 'inactive';
    
    // Update offcanvas title
    updateOffcanvasTitle('offcanvas_titles.editTopic');
    
    // Update submit button text
    updateSubmitButton('buttons.save');
    
    // Show offcanvas
    const offcanvas = new bootstrap.Offcanvas(document.getElementById('addEditTopicOffcanvas'));
    offcanvas.show();
}

function saveTopic() {
    const form = document.getElementById('addEditTopicForm');
    const formData = new FormData(form);
    
    const topicData = {
        Id: currentEditingTopicId,
        Name: formData.get('Name'),
        Description: formData.get('Description'),
        IsActive: formData.get('Status') === 'active'
    };
    
    // Basic validation
    if (!topicData.Name.trim()) {
        showError('Topic name is required');
        return;
    }
    
    // Show loading state
    const submitBtn = document.getElementById('saveTopicBtn');
    const originalText = submitBtn.textContent;
    submitBtn.disabled = true;
    submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-1"></i> Saving...';
    
    // Make AJAX call
    fetch('/Topics/SaveTopic', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        },
        body: JSON.stringify(topicData)
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            showSuccess(currentEditingTopicId === 0 ? 'Topic created successfully' : 'Topic updated successfully');
            
            // Close offcanvas
            const offcanvas = bootstrap.Offcanvas.getInstance(document.getElementById('addEditTopicOffcanvas'));
            offcanvas.hide();
            
            // Reload topics
            loadTopics();
        } else {
            showError(data.message || 'Error saving topic');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showError('Error saving topic');
    })
    .finally(() => {
        // Reset button state
        submitBtn.disabled = false;
        submitBtn.textContent = originalText;
    });
}

function deleteTopic(topicId) {
    const topic = topicsData.find(t => t.id === topicId);
    if (!topic) {
        showError('Topic not found');
        return;
    }
    
    // Get translation for confirmation message
    const confirmMessage = getTranslation('confirm_messages.deleteTopic') || 'Are you sure you want to delete this topic?';
    
    if (!confirm(confirmMessage)) {
        return;
    }
    
    // Make AJAX call
    fetch('/Topics/DeleteTopic', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
        },
        body: JSON.stringify({ TopicId: topicId })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            showSuccess('Topic deleted successfully');
            loadTopics();
        } else {
            showError(data.message || 'Error deleting topic');
        }
    })
    .catch(error => {
        console.error('Error:', error);
        showError('Error deleting topic');
    });
}

function resetForm() {
    const form = document.getElementById('addEditTopicForm');
    if (form) {
        form.reset();
        document.getElementById('topicId').value = '0';
    }
    currentEditingTopicId = 0;
}

function updateOffcanvasTitle(translationKey) {
    const titleElement = document.getElementById('addEditTopicOffcanvasLabel');
    if (titleElement) {
        const translation = getTranslation(translationKey);
        if (translation) {
            titleElement.textContent = translation;
        }
    }
}

function updateSubmitButton(translationKey) {
    const submitBtn = document.getElementById('saveTopicBtn');
    if (submitBtn) {
        const translation = getTranslation(translationKey);
        if (translation) {
            submitBtn.textContent = translation;
        }
    }
}

function showLoadingState() {
    const tbody = document.getElementById('topicsTableBody');
    if (tbody) {
        // Add subtle loading indicator without clearing the table
        const existingRows = tbody.querySelectorAll('tr');
        existingRows.forEach(row => {
            row.style.opacity = '0.5';
            row.style.transition = 'opacity 0.2s ease';
        });
    }
}

function showSuccess(message) {
    // Use the global showToast function if available
    if (typeof showToast === 'function') {
        showToast(message, 'success');
    }
    // Remove the alert fallback to avoid incorrect messages
}

function showError(message) {
    // Use the global showToast function if available
    if (typeof showToast === 'function') {
        showToast(message, 'error');
    }
    // Remove the alert fallback to avoid incorrect messages
}

function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}

function capitalizeFirst(str) {
    return str.charAt(0).toUpperCase() + str.slice(1);
}

function getTranslation(key) {
    // Access the global translations object from admin-script.js
    if (typeof translations !== 'undefined') {
        const currentLang = localStorage.getItem('language') || 'en';
        return translations[currentLang] && translations[currentLang][key];
    }
    return null;
}