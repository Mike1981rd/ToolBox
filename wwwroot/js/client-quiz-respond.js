document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('quizForm');
    const saveDraftBtn = document.getElementById('saveDraftBtn');
    const submitBtn = document.getElementById('submitBtn');
    const progressBar = document.getElementById('progressBar');
    const answeredCount = document.getElementById('answeredCount');
    const totalCount = document.getElementById('totalCount');
    
    let autoSaveTimeout;
    let isSubmitting = false;
    
    // Initialize
    updateProgress();
    setupEventListeners();
    
    function setupEventListeners() {
        // Auto-save on input change
        const inputs = form.querySelectorAll('.question-input, .question-input-multiple');
        inputs.forEach(input => {
            input.addEventListener('change', function() {
                handleMultipleChoiceChange(this);
                updateProgress();
                scheduleAutoSave();
            });
            
            if (input.type === 'text' || input.tagName === 'TEXTAREA') {
                input.addEventListener('input', function() {
                    updateProgress();
                    scheduleAutoSave();
                });
            }
        });
        
        // Save draft button
        saveDraftBtn.addEventListener('click', function() {
            saveDraft();
        });
        
        // Submit form
        form.addEventListener('submit', function(e) {
            e.preventDefault();
            if (!isSubmitting) {
                submitQuiz();
            }
        });
    }
    
    function handleMultipleChoiceChange(changedInput) {
        if (!changedInput.classList.contains('question-input-multiple')) return;
        
        const questionId = changedInput.dataset.questionId;
        const checkboxes = form.querySelectorAll(`input[data-question-id="${questionId}"][type="checkbox"]`);
        const hiddenInput = form.querySelector(`input[data-question-id="${questionId}"].multiple-choice-hidden`);
        
        const selectedValues = Array.from(checkboxes)
            .filter(cb => cb.checked)
            .map(cb => cb.value);
        
        hiddenInput.value = JSON.stringify(selectedValues);
    }
    
    function updateProgress() {
        const total = parseInt(totalCount.textContent);
        let answered = 0;
        
        // Count answered questions
        const questions = form.querySelectorAll('.question-container');
        questions.forEach(questionContainer => {
            const questionId = questionContainer.dataset.questionId;
            
            // Check different input types
            const textInputs = questionContainer.querySelectorAll('input[type="text"], textarea');
            const radioInputs = questionContainer.querySelectorAll('input[type="radio"]:checked');
            const checkboxInputs = questionContainer.querySelectorAll('input[type="checkbox"]:checked');
            
            let hasAnswer = false;
            
            // Text/textarea inputs
            textInputs.forEach(input => {
                if (input.value.trim()) hasAnswer = true;
            });
            
            // Radio inputs
            if (radioInputs.length > 0) hasAnswer = true;
            
            // Checkbox inputs
            if (checkboxInputs.length > 0) hasAnswer = true;
            
            if (hasAnswer) answered++;
        });
        
        const percentage = total > 0 ? Math.round((answered / total) * 100) : 0;
        
        progressBar.style.width = percentage + '%';
        progressBar.setAttribute('aria-valuenow', percentage);
        answeredCount.textContent = answered;
        
        // Update submit button state
        submitBtn.disabled = answered === 0;
    }
    
    function scheduleAutoSave() {
        if (autoSaveTimeout) {
            clearTimeout(autoSaveTimeout);
        }
        
        autoSaveTimeout = setTimeout(() => {
            saveDraft(true); // Silent save
        }, 3000); // Save after 3 seconds of inactivity
    }
    
    function saveDraft(silent = false) {
        const answers = collectAnswers();
        const instanceId = form.querySelector('input[name="InstanceId"]').value;
        
        if (!silent) {
            saveDraftBtn.disabled = true;
            saveDraftBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Guardando...';
        }
        
        fetch('/ClientQuizzes/SaveDraft', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-CSRF-TOKEN': getAntiForgeryToken()
            },
            body: JSON.stringify({
                InstanceId: parseInt(instanceId),
                Answers: answers
            })
        })
        .then(response => response.json())
        .then(result => {
            if (!silent) {
                if (result.success) {
                    showToast(result.message, 'success');
                } else {
                    showToast(result.message || 'Error al guardar', 'danger');
                }
                
                saveDraftBtn.disabled = false;
                saveDraftBtn.innerHTML = '<i class="fas fa-save me-2"></i>Guardar Borrador';
            }
        })
        .catch(error => {
            console.error('Error saving draft:', error);
            if (!silent) {
                showToast('Error de conexión al guardar', 'danger');
                saveDraftBtn.disabled = false;
                saveDraftBtn.innerHTML = '<i class="fas fa-save me-2"></i>Guardar Borrador';
            }
        });
    }
    
    function submitQuiz() {
        const answers = collectAnswers();
        const instanceId = form.querySelector('input[name="InstanceId"]').value;
        
        // Validate required fields
        const requiredQuestions = form.querySelectorAll('.question-container');
        let missingRequired = [];
        
        requiredQuestions.forEach(questionContainer => {
            const requiredIndicator = questionContainer.querySelector('.text-danger');
            if (!requiredIndicator) return; // Not required
            
            const questionText = questionContainer.querySelector('.form-label').textContent.trim();
            const questionId = questionContainer.dataset.questionId;
            
            const hasAnswer = answers.some(answer => 
                answer.QuestionTemplateId.toString() === questionId && 
                answer.ResponseText && 
                answer.ResponseText.trim()
            );
            
            if (!hasAnswer) {
                missingRequired.push(questionText.replace(/^\d+\s*/, '').replace('*', '').trim());
            }
        });
        
        if (missingRequired.length > 0) {
            showToast(`Faltan respuestas obligatorias: ${missingRequired.slice(0, 2).join(', ')}${missingRequired.length > 2 ? '...' : ''}`, 'warning');
            return;
        }
        
        isSubmitting = true;
        submitBtn.disabled = true;
        submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Enviando...';
        
        // Create a form and submit traditionally (for antiforgery token)
        const submitForm = document.createElement('form');
        submitForm.method = 'POST';
        submitForm.action = '/ClientQuizzes/SubmitAnswers';
        
        // Add antiforgery token
        const tokenInput = document.createElement('input');
        tokenInput.type = 'hidden';
        tokenInput.name = '__RequestVerificationToken';
        tokenInput.value = getAntiForgeryToken();
        submitForm.appendChild(tokenInput);
        
        // Add instance ID
        const instanceInput = document.createElement('input');
        instanceInput.type = 'hidden';
        instanceInput.name = 'InstanceId';
        instanceInput.value = instanceId;
        submitForm.appendChild(instanceInput);
        
        // Add answers
        answers.forEach((answer, index) => {
            const questionIdInput = document.createElement('input');
            questionIdInput.type = 'hidden';
            questionIdInput.name = `Answers[${index}].QuestionTemplateId`;
            questionIdInput.value = answer.QuestionTemplateId;
            submitForm.appendChild(questionIdInput);
            
            const responseInput = document.createElement('input');
            responseInput.type = 'hidden';
            responseInput.name = `Answers[${index}].ResponseText`;
            responseInput.value = answer.ResponseText || '';
            submitForm.appendChild(responseInput);
        });
        
        document.body.appendChild(submitForm);
        submitForm.submit();
    }
    
    function collectAnswers() {
        const answers = [];
        const questions = form.querySelectorAll('.question-container');
        
        questions.forEach(questionContainer => {
            const questionId = questionContainer.dataset.questionId;
            let responseText = '';
            
            // Text inputs
            const textInput = questionContainer.querySelector('input[type="text"], textarea');
            if (textInput && textInput.value.trim()) {
                responseText = textInput.value.trim();
            }
            
            // Radio inputs
            const radioInput = questionContainer.querySelector('input[type="radio"]:checked');
            if (radioInput) {
                responseText = radioInput.value;
            }
            
            // Multiple choice (hidden input with JSON)
            const multipleHidden = questionContainer.querySelector('.multiple-choice-hidden');
            if (multipleHidden && multipleHidden.value) {
                responseText = multipleHidden.value;
            }
            
            if (responseText) {
                answers.push({
                    QuestionTemplateId: parseInt(questionId),
                    ResponseText: responseText
                });
            }
        });
        
        return answers;
    }
    
    function getAntiForgeryToken() {
        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        return tokenInput ? tokenInput.value : '';
    }
    
    function showToast(message, type = 'info') {
        const toastElement = document.getElementById('notificationToast');
        const toastBody = document.getElementById('notificationToastBody');
        
        if (!toastElement || !toastBody) return;
        
        toastBody.textContent = message;
        toastElement.className = `toast text-bg-${type}`;
        
        const toast = new bootstrap.Toast(toastElement);
        toast.show();
    }
    
    // Initialize multiple choice hidden inputs
    document.querySelectorAll('.question-input-multiple').forEach(checkbox => {
        handleMultipleChoiceChange(checkbox);
    });
});