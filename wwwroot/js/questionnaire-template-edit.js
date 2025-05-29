// Questionnaire Template Edit - Questions Management
document.addEventListener('DOMContentLoaded', function() {
    'use strict';

    // Configuration
    const API_BASE_URL = '/api/questionnaire-templates';

    // State
    let isEditMode = false;
    let currentQuestionId = null;
    let templateId = null;

    // DOM Elements
    const questionModal = document.getElementById('questionModal');
    const questionForm = document.getElementById('questionForm');
    const addQuestionBtn = document.getElementById('addQuestionBtn');
    const addFirstQuestionBtn = document.getElementById('addFirstQuestionBtn');
    const questionsList = document.getElementById('questionsList');
    const questionsCount = document.getElementById('questionsCount');
    const emptyQuestionsState = document.getElementById('emptyQuestionsState');
    
    // Form fields
    const questionIdField = document.getElementById('questionId');
    const templateIdField = document.getElementById('templateId');
    const questionTextArea = document.getElementById('questionText');
    const questionTypeSelect = document.getElementById('questionType');
    const isRequiredCheckbox = document.getElementById('isRequired');
    const optionsSection = document.getElementById('optionsSection');
    const likertSection = document.getElementById('likertSection');
    const optionsList = document.getElementById('optionsList');
    const likertOptionsList = document.getElementById('likertOptionsList');
    const addOptionBtn = document.getElementById('addOptionBtn');
    const addLikertOptionBtn = document.getElementById('addLikertOptionBtn');

    // Initialize
    templateId = templateIdField?.value;
    setupEventListeners();
    initializeSortable();

    function setupEventListeners() {
        // Add question buttons
        if (addQuestionBtn) {
            addQuestionBtn.addEventListener('click', openAddQuestionModal);
        }
        if (addFirstQuestionBtn) {
            addFirstQuestionBtn.addEventListener('click', openAddQuestionModal);
        }

        // Question form
        if (questionForm) {
            questionForm.addEventListener('submit', handleQuestionSubmit);
        }

        // Question type change
        if (questionTypeSelect) {
            questionTypeSelect.addEventListener('change', handleQuestionTypeChange);
        }

        // Add option buttons
        if (addOptionBtn) {
            addOptionBtn.addEventListener('click', function() {
                addOptionField();
            });
        }
        if (addLikertOptionBtn) {
            addLikertOptionBtn.addEventListener('click', function() {
                // Calculate next suggested value
                const existingItems = likertOptionsList.querySelectorAll('.likert-option-item');
                const existingValues = Array.from(existingItems)
                    .map(item => {
                        const value = parseInt(item.querySelector('.likert-value-input').value.trim(), 10);
                        return isNaN(value) ? 0 : value;
                    })
                    .filter(v => v > 0);
                
                const nextValue = existingValues.length > 0 ? Math.max(...existingValues) + 1 : 1;
                addLikertOptionField(nextValue, `Opción ${nextValue}`);
            });
        }

        // Edit and delete buttons for existing questions
        document.addEventListener('click', function(e) {
            if (e.target.closest('.edit-question-btn')) {
                const questionId = e.target.closest('.edit-question-btn').dataset.questionId;
                openEditQuestionModal(questionId);
            }
            
            if (e.target.closest('.delete-question-btn')) {
                const questionId = e.target.closest('.delete-question-btn').dataset.questionId;
                confirmDeleteQuestion(questionId);
            }
        });

        // Modal events
        if (questionModal) {
            questionModal.addEventListener('hidden.bs.modal', resetQuestionForm);
        }
    }

    function initializeSortable() {
        // Initialize sortable for drag and drop reordering
        if (questionsList && typeof Sortable !== 'undefined') {
            new Sortable(questionsList, {
                handle: '.question-drag-handle',
                animation: 150,
                onEnd: function(evt) {
                    handleQuestionsReorder();
                }
            });
        }
    }

    function openAddQuestionModal() {
        isEditMode = false;
        currentQuestionId = null;
        resetQuestionForm();
        
        const modalTitle = document.getElementById('questionModalLabel');
        if (modalTitle) {
            modalTitle.textContent = 'Agregar Pregunta';
        }
        
        // Set default type and trigger change event
        questionTypeSelect.value = 'SingleChoice';
        handleQuestionTypeChange();
        
        const modal = new bootstrap.Modal(questionModal);
        modal.show();
    }

    function openEditQuestionModal(questionId) {
        isEditMode = true;
        currentQuestionId = questionId;
        
        // Find question data from DOM
        const questionItem = document.querySelector(`[data-question-id="${questionId}"]`);
        if (!questionItem) return;

        const questionText = questionItem.querySelector('.question-text').textContent;
        const typeBadge = questionItem.querySelector('.badge');
        const isRequired = questionItem.querySelector('.badge.bg-warning') !== null;
        
        // Populate form
        questionTextArea.value = questionText;
        isRequiredCheckbox.checked = isRequired;
        questionIdField.value = questionId;
        
        // Set question type based on badge text
        const typeText = typeBadge.textContent;
        const typeMapping = {
            'Opción Múltiple (Una respuesta)': 'SingleChoice',
            'Opción Múltiple (Múltiples respuestas)': 'MultipleChoice',
            'Respuesta Corta': 'ShortText',
            'Respuesta Larga': 'LongText',
            'Escala Likert': 'LikertScale',
            'Verdadero/Falso': 'TrueFalse'
        };
        
        const questionType = typeMapping[typeText] || 'ShortText';
        questionTypeSelect.value = questionType;
        
        // Load existing options
        loadExistingOptions(questionItem, questionType);
        
        handleQuestionTypeChange();
        
        const modalTitle = document.getElementById('questionModalLabel');
        if (modalTitle) {
            modalTitle.textContent = 'Editar Pregunta';
        }
        
        const modal = new bootstrap.Modal(questionModal);
        modal.show();
    }

    function loadExistingOptions(questionItem, questionType) {
        const optionsContainer = questionItem.querySelector('.question-options ul');
        if (!optionsContainer) return;

        const optionItems = optionsContainer.querySelectorAll('li small');
        
        if (questionType === 'LikertScale') {
            // Parse Likert options (format: "1 - Malo")
            optionItems.forEach(item => {
                const text = item.textContent.trim();
                const match = text.match(/^(\d+)\s*-\s*(.+)$/);
                if (match) {
                    const value = match[1];
                    const label = match[2];
                    addLikertOptionField(value, label);
                }
            });
        } else {
            // Parse regular options
            optionItems.forEach(item => {
                const text = item.textContent.trim();
                if (text) {
                    addOptionField(text);
                }
            });
        }
    }

    function handleQuestionTypeChange() {
        const selectedType = questionTypeSelect.value;
        
        // Hide all option sections
        optionsSection.style.display = 'none';
        likertSection.style.display = 'none';
        
        // Clear previous options when switching types
        optionsList.innerHTML = '';
        likertOptionsList.innerHTML = '';
        
        // Show relevant section based on type
        if (selectedType === 'SingleChoice' || selectedType === 'MultipleChoice') {
            optionsSection.style.display = 'block';
            // Always add default options for choice questions
            addOptionField();
            addOptionField();
        } else if (selectedType === 'TrueFalse') {
            // For True/False, we'll use the options section with fixed options
            optionsSection.style.display = 'block';
            optionsList.innerHTML = ''; // Clear existing options
            // Add fixed True/False options
            const trueDiv = document.createElement('div');
            trueDiv.className = 'input-group mb-2 option-item';
            trueDiv.innerHTML = `
                <input type="text" class="form-control option-input" value="Verdadero" readonly>
            `;
            optionsList.appendChild(trueDiv);
            
            const falseDiv = document.createElement('div');
            falseDiv.className = 'input-group mb-2 option-item';
            falseDiv.innerHTML = `
                <input type="text" class="form-control option-input" value="Falso" readonly>
            `;
            optionsList.appendChild(falseDiv);
        } else if (selectedType === 'LikertScale') {
            likertSection.style.display = 'block';
            if (likertOptionsList.children.length === 0) {
                // Add default 5-point scale
                for (let i = 1; i <= 5; i++) {
                    addLikertOptionField(i, `Nivel ${i}`);
                }
            }
        }
    }

    function addOptionField(value = '') {
        if (!optionsList) {
            console.error('optionsList is null!');
            return;
        }
        
        const optionDiv = document.createElement('div');
        optionDiv.className = 'd-flex align-items-center mb-2 option-item gap-2';
        optionDiv.innerHTML = `
            <input type="text" class="form-control option-input" value="${escapeHtml(value)}" placeholder="Opción...">
            <button type="button" class="btn btn-sm btn-outline-danger remove-option-btn" style="padding: 0.375rem 0.75rem;">
                <i class="fas fa-trash-alt" style="font-size: 0.875rem;"></i>
            </button>
        `;
        
        optionsList.appendChild(optionDiv);
        
        // Add remove functionality
        optionDiv.querySelector('.remove-option-btn').addEventListener('click', function() {
            optionDiv.remove();
        });
        
        // Focus on the new input
        optionDiv.querySelector('.option-input').focus();
    }

    function addLikertOptionField(value = '', text = '') {
        const optionDiv = document.createElement('div');
        optionDiv.className = 'd-flex align-items-center mb-2 likert-option-item gap-2';
        
        // Ensure value is properly handled - if empty string or falsy, don't set value attribute
        const valueAttr = value !== '' && value !== null && value !== undefined ? `value="${value}"` : '';
        
        optionDiv.innerHTML = `
            <div class="input-group flex-grow-1">
                <input type="number" class="form-control likert-value-input" ${valueAttr} placeholder="Valor" style="max-width: 80px;">
                <span class="input-group-text">-</span>
                <input type="text" class="form-control likert-text-input" value="${escapeHtml(text)}" placeholder="Etiqueta">
            </div>
            <button type="button" class="btn btn-sm btn-outline-danger remove-likert-option-btn" style="padding: 0.375rem 0.75rem;">
                <i class="fas fa-trash-alt" style="font-size: 0.875rem;"></i>
            </button>
        `;
        
        likertOptionsList.appendChild(optionDiv);
        
        // Add remove functionality
        optionDiv.querySelector('.remove-likert-option-btn').addEventListener('click', function() {
            optionDiv.remove();
        });
        
        // Focus on the new input
        if (!value || value === '') {
            optionDiv.querySelector('.likert-value-input').focus();
        }
    }

    async function handleQuestionSubmit(e) {
        e.preventDefault();
        
        // Validate form
        if (!validateQuestionForm()) {
            return;
        }
        
        const formData = collectFormData();
        
        try {
            let response;
            
            if (isEditMode) {
                response = await fetch(`${API_BASE_URL}/questions/${currentQuestionId}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                        'X-CSRF-TOKEN': getAntiForgeryToken()
                    },
                    body: JSON.stringify(formData)
                });
            } else {
                response = await fetch(`${API_BASE_URL}/${templateId}/questions`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'X-CSRF-TOKEN': getAntiForgeryToken()
                    },
                    body: JSON.stringify(formData)
                });
            }
            
            const result = await response.json();
            
            if (result.success) {
                showSuccess(isEditMode ? 'Pregunta actualizada exitosamente' : 'Pregunta creada exitosamente');
                
                // Close modal
                const modal = bootstrap.Modal.getInstance(questionModal);
                modal.hide();
                
                // Refresh questions list
                setTimeout(() => {
                    location.reload(); // Simple refresh for now
                }, 500);
            } else {
                showError(result.message || 'Error al guardar la pregunta');
            }
        } catch (error) {
            console.error('Error saving question:', error);
            showError('Error de conexión al guardar la pregunta');
        }
    }

    function validateQuestionForm() {
        let isValid = true;
        
        // Clear previous errors
        document.querySelectorAll('.is-invalid').forEach(el => el.classList.remove('is-invalid'));
        document.querySelectorAll('.invalid-feedback').forEach(el => el.textContent = '');
        
        // Validate question text
        if (!questionTextArea.value.trim()) {
            questionTextArea.classList.add('is-invalid');
            document.getElementById('questionTextError').textContent = 'El texto de la pregunta es requerido';
            isValid = false;
        }
        
        // Validate options for choice questions
        const selectedType = questionTypeSelect.value;
        if (selectedType === 'SingleChoice' || selectedType === 'MultipleChoice') {
            const options = optionsList.querySelectorAll('.option-input');
            const validOptions = Array.from(options).filter(input => input.value.trim());
            
            if (validOptions.length < 2) {
                showError('Debes agregar al menos 2 opciones para preguntas de opción múltiple');
                isValid = false;
            }
        } else if (selectedType === 'TrueFalse') {
            // True/False always has valid options, no validation needed
        } else if (selectedType === 'LikertScale') {
            const likertOptions = likertOptionsList.querySelectorAll('.likert-option-item');
            
            if (likertOptions.length < 2) {
                showError('Debes agregar al menos 2 opciones para la escala Likert');
                isValid = false;
            }
            
            // Validate that all Likert options have valid values and text
            let hasInvalidLikert = false;
            Array.from(likertOptions).forEach(item => {
                const valueInput = item.querySelector('.likert-value-input').value.trim();
                const textInput = item.querySelector('.likert-text-input').value.trim();
                
                if (valueInput === '' || textInput === '') {
                    hasInvalidLikert = true;
                    item.querySelector('.likert-value-input').classList.add('is-invalid');
                    item.querySelector('.likert-text-input').classList.add('is-invalid');
                }
                
                // Check for duplicate values
                const currentValue = parseInt(valueInput, 10);
                if (!isNaN(currentValue)) {
                    const otherValues = Array.from(likertOptions)
                        .filter(otherItem => otherItem !== item)
                        .map(otherItem => parseInt(otherItem.querySelector('.likert-value-input').value.trim(), 10))
                        .filter(v => !isNaN(v));
                    
                    if (otherValues.includes(currentValue)) {
                        hasInvalidLikert = true;
                        item.querySelector('.likert-value-input').classList.add('is-invalid');
                    }
                }
            });
            
            if (hasInvalidLikert) {
                showError('Todas las opciones Likert deben tener valores únicos y etiquetas válidas');
                isValid = false;
            }
        }
        
        return isValid;
    }

    function collectFormData() {
        const selectedType = questionTypeSelect.value;
        let optionsJson = null;
        
        // Mapear el tipo string al valor numérico del enum
        const typeEnumMap = {
            'SingleChoice': 0,
            'MultipleChoice': 1,
            'ShortText': 2,
            'LongText': 3,
            'LikertScale': 4,
            'TrueFalse': 5
        };
        
        if (selectedType === 'SingleChoice' || selectedType === 'MultipleChoice' || selectedType === 'TrueFalse') {
            const options = Array.from(optionsList.querySelectorAll('.option-input'))
                .map(input => input.value.trim())
                .filter(value => value);
            
            if (options.length > 0) {
                optionsJson = JSON.stringify(options);
            }
        } else if (selectedType === 'LikertScale') {
            const likertOptions = Array.from(likertOptionsList.querySelectorAll('.likert-option-item'))
                .map(item => {
                    const valueInput = item.querySelector('.likert-value-input').value.trim();
                    const text = item.querySelector('.likert-text-input').value.trim();
                    
                    // Only convert to number if there's actually a value
                    const value = valueInput !== '' ? parseInt(valueInput, 10) : null;
                    return { value, text };
                })
                .filter(option => option.value !== null && !isNaN(option.value) && option.text);
            
            if (likertOptions.length > 0) {
                optionsJson = JSON.stringify(likertOptions);
            }
        }
        
        const formData = {
            questionText: questionTextArea.value.trim(),
            type: typeEnumMap[selectedType] !== undefined ? typeEnumMap[selectedType] : 0,
            optionsJson: optionsJson,
            isRequired: isRequiredCheckbox.checked
        };
        
        return formData;
    }

    async function confirmDeleteQuestion(questionId) {
        if (confirm('¿Estás seguro de que quieres eliminar esta pregunta? Esta acción no se puede deshacer.')) {
            await deleteQuestion(questionId);
        }
    }

    async function deleteQuestion(questionId) {
        try {
            const response = await fetch(`${API_BASE_URL}/questions/${questionId}`, {
                method: 'DELETE',
                headers: {
                    'X-CSRF-TOKEN': getAntiForgeryToken()
                }
            });
            
            const result = await response.json();
            
            if (result.success) {
                showSuccess('Pregunta eliminada exitosamente');
                
                // Remove question from DOM with animation
                const questionItem = document.querySelector(`[data-question-id="${questionId}"]`);
                if (questionItem) {
                    questionItem.style.opacity = '0';
                    setTimeout(() => {
                        questionItem.remove();
                        updateQuestionsCount();
                        checkEmptyState();
                    }, 300);
                }
            } else {
                showError(result.message || 'Error al eliminar la pregunta');
            }
        } catch (error) {
            console.error('Error deleting question:', error);
            showError('Error de conexión al eliminar la pregunta');
        }
    }

    async function handleQuestionsReorder() {
        const questionItems = questionsList.querySelectorAll('.question-item');
        const reorderData = {
            questions: Array.from(questionItems).map((item, index) => ({
                id: parseInt(item.dataset.questionId),
                order: index + 1
            }))
        };
        
        try {
            const response = await fetch(`${API_BASE_URL}/${templateId}/questions/reorder`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'X-CSRF-TOKEN': getAntiForgeryToken()
                },
                body: JSON.stringify(reorderData)
            });
            
            const result = await response.json();
            
            if (result.success) {
                showSuccess('Preguntas reordenadas exitosamente');
            } else {
                showError(result.message || 'Error al reordenar las preguntas');
                location.reload(); // Reload to restore original order
            }
        } catch (error) {
            console.error('Error reordering questions:', error);
            showError('Error de conexión al reordenar las preguntas');
            location.reload(); // Reload to restore original order
        }
    }

    function resetQuestionForm() {
        // Reset form fields
        if (questionForm) {
            questionForm.reset();
        }
        
        // Clear dynamic sections
        if (optionsList) {
            optionsList.innerHTML = '';
        }
        if (likertOptionsList) {
            likertOptionsList.innerHTML = '';
        }
        
        // Hide option sections
        if (optionsSection) {
            optionsSection.style.display = 'none';
        }
        if (likertSection) {
            likertSection.style.display = 'none';
        }
        
        // Reset state
        isEditMode = false;
        currentQuestionId = null;
        if (questionIdField) {
            questionIdField.value = '';
        }
        
        // Clear validation
        document.querySelectorAll('.is-invalid').forEach(el => el.classList.remove('is-invalid'));
        document.querySelectorAll('.invalid-feedback').forEach(el => el.textContent = '');
    }

    function updateQuestionsCount() {
        const count = questionsList.querySelectorAll('.question-item').length;
        if (questionsCount) {
            questionsCount.textContent = count;
        }
    }

    function checkEmptyState() {
        const hasQuestions = questionsList.querySelectorAll('.question-item').length > 0;
        
        if (emptyQuestionsState) {
            emptyQuestionsState.style.display = hasQuestions ? 'none' : 'block';
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
            const icon = toast.querySelector('.toast-header i');
            if (icon) {
                icon.className = `fas fa-${type === 'success' ? 'check-circle text-success' : type === 'error' ? 'exclamation-circle text-danger' : 'info-circle text-primary'} me-2`;
            }
            
            toastBody.textContent = message;
            
            const bsToast = new bootstrap.Toast(toast);
            bsToast.show();
        }
    }

    function getAntiForgeryToken() {
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        return token ? token.value : '';
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
});