// Feedback 360 Manage Raters functionality
document.addEventListener('DOMContentLoaded', function() {
    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });
    
    // Handle add rater form submission
    const addRaterForm = document.getElementById('addRaterForm');
    if (addRaterForm) {
        addRaterForm.addEventListener('submit', async function(e) {
            e.preventDefault();
            
            const instanceId = document.getElementById('instanceId').value;
            const raterName = document.getElementById('raterName').value;
            const raterEmail = document.getElementById('raterEmail').value;
            const raterCategory = document.getElementById('raterCategory').value;
            
            // Validate
            if (!raterEmail || !raterCategory || raterCategory === '') {
                showAlert('Please fill in all required fields', 'warning');
                return;
            }
            
            // Validate category is a valid number
            const categoryInt = parseInt(raterCategory);
            if (isNaN(categoryInt)) {
                showAlert('Please select a valid category', 'warning');
                return;
            }
            
            // Debug logging
            console.log('Adding rater with data:', {
                instanceId: instanceId,
                raterName: raterName,
                raterEmail: raterEmail,
                raterCategory: raterCategory
            });
            
            // Disable form while submitting
            const submitBtn = addRaterForm.querySelector('button[type="submit"]');
            const originalBtnText = submitBtn.innerHTML;
            submitBtn.disabled = true;
            submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Adding...';
            
            try {
                const viewModel = {
                    instanceId: parseInt(instanceId),
                    raterName: raterName || null,
                    raterEmail: raterEmail,
                    category: categoryInt, // Use pre-validated integer
                    existingUserId: null // TODO: implement existing user selection
                };
                
                console.log('Sending viewModel:', viewModel);
                
                const response = await fetch('/Feedback360Process/AddRater', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'X-CSRF-TOKEN': getAntiForgeryToken()
                    },
                    body: JSON.stringify(viewModel)
                });
                
                console.log('Response status:', response.status);
                
                if (!response.ok) {
                    console.error('Response not OK:', response.statusText);
                    const errorText = await response.text();
                    console.error('Error response:', errorText);
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                
                const result = await response.json();
                console.log('Response data:', result);
                
                if (result.success) {
                    // Add the new rater to the table
                    addRaterToTable(result.rater);
                    
                    // Clear form
                    addRaterForm.reset();
                    
                    // Update rater count
                    updateRaterCount();
                    
                    // Check if we can now send invitations
                    checkSendInvitationsButton();
                    
                    showAlert('Evaluator added successfully!', 'success');
                } else {
                    showAlert(result.message || 'Failed to add evaluator', 'danger');
                }
            } catch (error) {
                console.error('Error adding rater:', error);
                showAlert('An error occurred while adding the evaluator', 'danger');
            } finally {
                // Re-enable form
                submitBtn.disabled = false;
                submitBtn.innerHTML = originalBtnText;
            }
        });
    }
});

// Remove rater function
async function removeRater(raterId) {
    if (!confirm('Are you sure you want to remove this evaluator?')) {
        return;
    }
    
    try {
        const response = await fetch('/Feedback360Process/RemoveRater', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-CSRF-TOKEN': getAntiForgeryToken()
            },
            body: JSON.stringify(raterId)
        });
        
        const result = await response.json();
        
        if (result.success) {
            // Remove the row with animation
            const row = document.querySelector(`tr[data-rater-id="${raterId}"]`);
            if (row) {
                row.style.opacity = '0';
                row.style.transition = 'opacity 0.3s ease-out';
                setTimeout(() => {
                    row.remove();
                    updateRaterCount();
                    checkSendInvitationsButton();
                    checkEmptyTable();
                }, 300);
            }
            
            showAlert('Evaluator removed successfully!', 'success');
        } else {
            showAlert(result.message || 'Failed to remove evaluator', 'danger');
        }
    } catch (error) {
        console.error('Error removing rater:', error);
        showAlert('An error occurred while removing the evaluator', 'danger');
    }
}

// Add rater to table
function addRaterToTable(rater) {
    console.log('Adding rater to table:', rater);
    
    const tbody = document.getElementById('ratersTableBody');
    const noRatersRow = document.getElementById('noRatersRow');
    
    // Remove "no raters" row if it exists
    if (noRatersRow) {
        noRatersRow.remove();
    }
    
    // Get the template
    const template = document.getElementById('raterRowTemplate');
    const newRow = template.content.cloneNode(true);
    
    // Map numeric category to string
    const categoryMap = {
        0: 'self',
        1: 'manager',
        2: 'peer',
        3: 'directreport',
        4: 'client',
        5: 'other'
    };
    
    const categoryString = typeof rater.category === 'number' 
        ? categoryMap[rater.category] || 'other'
        : rater.category.toLowerCase();
    
    // Replace placeholders
    const html = newRow.querySelector('tr').outerHTML
        .replace(/__ID__/g, rater.id)
        .replace(/__NAME__/g, escapeHtml(rater.raterNameOrEmail))
        .replace(/__EMAIL__/g, escapeHtml(rater.email))
        .replace(/__CATEGORY__/g, escapeHtml(rater.categoryDisplay))
        .replace(/__CATEGORY_LOWER__/g, categoryString);
    
    // Add to table
    tbody.insertAdjacentHTML('beforeend', html);
    
    // Initialize new tooltip
    const newTooltip = tbody.lastElementChild.querySelector('[data-bs-toggle="tooltip"]');
    if (newTooltip) {
        new bootstrap.Tooltip(newTooltip);
    }
    
    // Highlight the new row
    const addedRow = tbody.lastElementChild;
    addedRow.classList.add('table-success');
    setTimeout(() => {
        addedRow.classList.remove('table-success');
        addedRow.style.transition = 'background-color 0.5s ease-out';
    }, 100);
}

// Update rater count badge
function updateRaterCount() {
    const tbody = document.getElementById('ratersTableBody');
    const raterCount = tbody.querySelectorAll('tr[data-rater-id]').length;
    const countBadge = document.getElementById('raterCount');
    if (countBadge) {
        countBadge.textContent = raterCount;
    }
}

// Check if we should show/enable send invitations button
function checkSendInvitationsButton() {
    const tbody = document.getElementById('ratersTableBody');
    const raterCount = tbody.querySelectorAll('tr[data-rater-id]').length;
    const sendBtn = document.getElementById('sendInvitationsBtn');
    
    if (sendBtn) {
        // Enable/disable based on minimum raters (3)
        sendBtn.disabled = raterCount < 3;
        
        if (raterCount < 3) {
            sendBtn.setAttribute('data-bs-toggle', 'tooltip');
            sendBtn.setAttribute('data-bs-title', 'Add at least 3 evaluators to continue');
            new bootstrap.Tooltip(sendBtn);
        } else {
            const tooltip = bootstrap.Tooltip.getInstance(sendBtn);
            if (tooltip) {
                tooltip.dispose();
            }
        }
    }
}

// Check if table is empty and show message
function checkEmptyTable() {
    const tbody = document.getElementById('ratersTableBody');
    const raterRows = tbody.querySelectorAll('tr[data-rater-id]');
    
    if (raterRows.length === 0) {
        tbody.innerHTML = `
            <tr id="noRatersRow">
                <td colspan="5" class="text-center py-4">
                    <span class="text-muted" data-translate-key="feedback360_no_evaluators">
                        No evaluators added yet. Add at least 3 evaluators to continue.
                    </span>
                </td>
            </tr>
        `;
    }
}

// Show alert message
function showAlert(message, type = 'info') {
    const alertHtml = `
        <div class="alert alert-${type} alert-dismissible fade show" role="alert">
            <i class="fas fa-${getAlertIcon(type)} me-2"></i>
            ${escapeHtml(message)}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;
    
    const cardBody = document.querySelector('.card-body');
    const existingAlert = cardBody.querySelector('.alert');
    
    if (existingAlert) {
        existingAlert.remove();
    }
    
    cardBody.insertAdjacentHTML('afterbegin', alertHtml);
    
    // Auto-dismiss after 5 seconds
    setTimeout(() => {
        const alert = cardBody.querySelector('.alert');
        if (alert) {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }
    }, 5000);
}

// Get alert icon based on type
function getAlertIcon(type) {
    const icons = {
        'success': 'check-circle',
        'danger': 'exclamation-circle',
        'warning': 'exclamation-triangle',
        'info': 'info-circle'
    };
    return icons[type] || 'info-circle';
}

// Get anti-forgery token
function getAntiForgeryToken() {
    return document.querySelector('input[name="__RequestVerificationToken"]')?.value || '';
}

// Escape HTML to prevent XSS
function escapeHtml(text) {
    const map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#039;'
    };
    return text.replace(/[&<>"']/g, m => map[m]);
}