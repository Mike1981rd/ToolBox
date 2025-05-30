// Feedback 360 Response functionality
document.addEventListener('DOMContentLoaded', function() {
    // Progress tracking
    updateProgress();
    
    // Listen for changes in scale inputs
    document.querySelectorAll('.scale-input').forEach(input => {
        input.addEventListener('change', function() {
            updateProgress();
            // Auto-save draft after each answer (optional)
            if (window.autoSaveEnabled) {
                debouncedSaveDraft();
            }
        });
    });
    
    // Save draft functionality
    const saveDraftBtn = document.getElementById('saveDraftBtn');
    if (saveDraftBtn) {
        saveDraftBtn.addEventListener('click', function() {
            saveDraft();
        });
    }
    
    // Form submission confirmation
    const feedbackForm = document.getElementById('feedbackForm');
    if (feedbackForm) {
        feedbackForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            // Check if all required questions are answered
            const unansweredRequired = getUnansweredRequiredQuestions();
            if (unansweredRequired.length > 0) {
                showValidationAlert(unansweredRequired);
                return;
            }
            
            // Confirm submission
            if (confirm(getTranslation('feedback360_confirm_submit', 'Are you sure you want to submit your feedback? You cannot modify your responses after submission.'))) {
                this.submit();
            }
        });
    }
    
    // Smooth scroll to first error
    if (document.querySelector('.field-validation-error')) {
        const firstError = document.querySelector('.field-validation-error');
        firstError.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
});

// Update progress bar
function updateProgress() {
    const totalQuestions = document.querySelectorAll('.scale-input[type="radio"]').length / 5; // 5 options per question
    const answeredQuestions = document.querySelectorAll('.scale-input:checked').length;
    
    const percentage = totalQuestions > 0 ? Math.round((answeredQuestions / totalQuestions) * 100) : 0;
    
    document.getElementById('answeredCount').textContent = answeredQuestions;
    document.getElementById('totalQuestions').textContent = totalQuestions;
    
    const progressBar = document.getElementById('progressBar');
    if (progressBar) {
        progressBar.style.width = percentage + '%';
        progressBar.setAttribute('aria-valuenow', percentage);
        
        // Change color based on progress
        progressBar.classList.remove('bg-danger', 'bg-warning', 'bg-success');
        if (percentage < 33) {
            progressBar.classList.add('bg-danger');
        } else if (percentage < 66) {
            progressBar.classList.add('bg-warning');
        } else {
            progressBar.classList.add('bg-success');
        }
    }
}

// Get unanswered required questions
function getUnansweredRequiredQuestions() {
    const unanswered = [];
    const competencies = document.querySelectorAll('.competency-block');
    
    competencies.forEach((competency, index) => {
        const competencyName = competency.querySelector('h5').textContent.trim();
        const questions = competency.querySelectorAll('.question-block');
        
        questions.forEach((question, qIndex) => {
            const isRequired = question.querySelector('input[type="hidden"][name*="IsRequired"]')?.value === 'True';
            if (isRequired) {
                const radioGroup = question.querySelectorAll('input[type="radio"]');
                const isAnswered = Array.from(radioGroup).some(radio => radio.checked);
                
                if (!isAnswered) {
                    const questionText = question.querySelector('label').textContent.trim();
                    unanswered.push({
                        competency: competencyName,
                        question: questionText,
                        index: index + 1,
                        questionIndex: qIndex + 1
                    });
                }
            }
        });
    });
    
    return unanswered;
}

// Show validation alert
function showValidationAlert(unansweredQuestions) {
    let message = getTranslation('feedback360_required_questions', 'Please answer all required questions before submitting:') + '\n\n';
    
    unansweredQuestions.forEach(item => {
        message += `• ${item.competency} - Question ${item.questionIndex}\n`;
    });
    
    alert(message);
    
    // Scroll to first unanswered question
    if (unansweredQuestions.length > 0) {
        const firstUnanswered = unansweredQuestions[0];
        const competencyBlocks = document.querySelectorAll('.competency-block');
        if (competencyBlocks[firstUnanswered.index - 1]) {
            competencyBlocks[firstUnanswered.index - 1].scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
    }
}

// Save draft via AJAX
async function saveDraft() {
    const saveDraftBtn = document.getElementById('saveDraftBtn');
    const originalText = saveDraftBtn.innerHTML;
    
    // Show loading state
    saveDraftBtn.disabled = true;
    saveDraftBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>' + getTranslation('feedback360_saving', 'Saving...');
    
    try {
        // Collect form data
        const formData = collectFormData();
        
        const response = await fetch('/Feedback360Response/SaveDraft', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': getAntiForgeryToken()
            },
            body: JSON.stringify(formData)
        });
        
        const result = await response.json();
        
        if (result.success) {
            // Show success message
            showToast(result.message || getTranslation('feedback360_draft_saved', 'Draft saved successfully'), 'success');
            
            // Update button temporarily
            saveDraftBtn.innerHTML = '<i class="fas fa-check me-2"></i>' + getTranslation('feedback360_saved', 'Saved');
            setTimeout(() => {
                saveDraftBtn.innerHTML = originalText;
            }, 2000);
        } else {
            showToast(result.message || getTranslation('feedback360_save_error', 'Error saving draft'), 'danger');
        }
    } catch (error) {
        console.error('Error saving draft:', error);
        showToast(getTranslation('feedback360_save_error', 'Error saving draft'), 'danger');
    } finally {
        saveDraftBtn.disabled = false;
    }
}

// Debounced save draft
const debouncedSaveDraft = debounce(saveDraft, 2000);

// Collect form data
function collectFormData() {
    const formData = {
        token: document.querySelector('input[name="Token"]').value,
        competencies: [],
        openEndedQuestions: []
    };
    
    // Collect scale responses
    const competencyBlocks = document.querySelectorAll('.competency-block');
    competencyBlocks.forEach((block, index) => {
        const competency = {
            competencyCode: document.querySelector(`input[name="Competencies[${index}].CompetencyCode"]`).value,
            competencyName: document.querySelector(`input[name="Competencies[${index}].CompetencyName"]`).value,
            scaleQuestions: []
        };
        
        const questions = block.querySelectorAll('.question-block');
        questions.forEach((question, qIndex) => {
            const selectedRadio = question.querySelector('input[type="radio"]:checked');
            competency.scaleQuestions.push({
                questionCode: question.querySelector('input[name*="QuestionCode"]').value,
                score: selectedRadio ? parseInt(selectedRadio.value) : null
            });
        });
        
        formData.competencies.push(competency);
    });
    
    // Collect open-ended responses
    const openQuestions = document.querySelectorAll('.open-questions-section textarea');
    openQuestions.forEach((textarea, index) => {
        formData.openEndedQuestions.push({
            questionCode: document.querySelector(`input[name="OpenEndedQuestions[${index}].QuestionCode"]`).value,
            responseText: textarea.value
        });
    });
    
    return formData;
}

// Show toast notification
function showToast(message, type = 'info') {
    // Create toast container if it doesn't exist
    let toastContainer = document.getElementById('toastContainer');
    if (!toastContainer) {
        toastContainer = document.createElement('div');
        toastContainer.id = 'toastContainer';
        toastContainer.className = 'position-fixed bottom-0 end-0 p-3';
        toastContainer.style.zIndex = '1050';
        document.body.appendChild(toastContainer);
    }
    
    const toastId = 'toast_' + Date.now();
    const toastHtml = `
        <div id="${toastId}" class="toast align-items-center text-white bg-${type} border-0" role="alert">
            <div class="d-flex">
                <div class="toast-body">
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    `;
    
    toastContainer.insertAdjacentHTML('beforeend', toastHtml);
    
    const toastElement = document.getElementById(toastId);
    const toast = new bootstrap.Toast(toastElement);
    toast.show();
    
    // Remove toast after it's hidden
    toastElement.addEventListener('hidden.bs.toast', function () {
        toastElement.remove();
    });
}

// Get anti-forgery token
function getAntiForgeryToken() {
    return document.querySelector('input[name="__RequestVerificationToken"]')?.value || '';
}

// Simple translation helper
function getTranslation(key, defaultText) {
    // In a real implementation, this would look up translations
    return defaultText;
}

// Debounce utility
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

// Responsive adjustments
window.addEventListener('resize', function() {
    // Adjust likert scale layout for mobile
    const width = window.innerWidth;
    const likertContainers = document.querySelectorAll('.likert-scale-container');
    
    likertContainers.forEach(container => {
        if (width < 576) {
            container.classList.add('mobile-view');
        } else {
            container.classList.remove('mobile-view');
        }
    });
});