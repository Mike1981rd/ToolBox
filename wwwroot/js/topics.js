/**
 * Topics Management JavaScript
 * Handles CRUD operations for the Topics module
 */

let topicsData = [];
let currentEditingTopicId = 0;

document.addEventListener('DOMContentLoaded', function() {
    initializeTopicsModule();
});

function initializeTopicsModule() {
    // Load initial topics data
    loadTopics();
    
    // Set up event listeners
    setupEventListeners();
    
    // Initialize search functionality
    initializeSearch();
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

function initializeSearch() {
    const searchInput = document.getElementById('topicsSearchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const searchTerm = this.value;
            filterTopics(searchTerm);
        });
    }
}

function loadTopics(searchTerm = '') {
    // Show loading state
    showLoadingState();
    
    // Make AJAX call to get topics
    fetch(`/Topics/GetTopics?searchTerm=${encodeURIComponent(searchTerm)}`)
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                topicsData = data.data;
                renderTopicsTable();
                updateTableInfo();
            } else {
                console.error('Error loading topics:', data.message);
                showError('Error loading topics');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            showError('Error loading topics');
        });
}

function renderTopicsTable() {
    const tbody = document.getElementById('topicsTableBody');
    if (!tbody) return;
    
    if (topicsData.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="4" class="text-center text-muted py-4">
                    <i class="fas fa-search fa-2x mb-2 d-block"></i>
                    No topics found
                </td>
            </tr>
        `;
        return;
    }
    
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
                <span class="badge bg-${topic.status === 'active' ? 'success' : 'secondary'}">${capitalizeFirst(topic.status)}</span>
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
    
    // Attach event listeners to action buttons
    attachActionListeners();
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
    document.getElementById('topicStatus').value = topic.status;
    
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
        id: currentEditingTopicId,
        name: formData.get('Name'),
        description: formData.get('Description'),
        status: formData.get('Status')
    };
    
    // Basic validation
    if (!topicData.name.trim()) {
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
        },
        body: JSON.stringify({ topicId: topicId })
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

function filterTopics(searchTerm) {
    loadTopics(searchTerm);
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

function updateTableInfo() {
    const infoElement = document.getElementById('topicsTableInfo');
    if (infoElement && topicsData) {
        const total = topicsData.length;
        infoElement.textContent = `Showing 1 to ${total} of ${total} entries`;
    }
}

function showLoadingState() {
    const tbody = document.getElementById('topicsTableBody');
    if (tbody) {
        tbody.innerHTML = `
            <tr>
                <td colspan="4" class="text-center py-4">
                    <i class="fas fa-spinner fa-spin fa-2x mb-2 d-block"></i>
                    Loading topics...
                </td>
            </tr>
        `;
    }
}

function showSuccess(message) {
    // Simple alert for now - in a real app, you might use a toast notification
    alert(message);
}

function showError(message) {
    // Simple alert for now - in a real app, you might use a toast notification
    alert('Error: ' + message);
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