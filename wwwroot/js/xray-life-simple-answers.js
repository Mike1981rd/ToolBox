/**
 * Simple XRay Life User Answers Integration
 * Adds answer functionality to each question in the accordion
 */

document.addEventListener('DOMContentLoaded', function() {
    // Wait for main xray-life.js to initialize
    setTimeout(initSimpleAnswers, 500);
    
    let userAnswers = {};
    
    function initSimpleAnswers() {
        console.log('Initializing simple answers module...');
        
        // Add answer inputs to existing questions
        addAnswerInputsToExistingQuestions();
        
        // Add save button
        addSaveAnswersButton();
        
        // Listen for accordion changes to add inputs to dynamically loaded questions
        observeAccordionChanges();
    }
    
    function addAnswerInputsToExistingQuestions() {
        const accordionItems = document.querySelectorAll('.accordion-item');
        accordionItems.forEach(item => {
            addAnswerInputToQuestion(item);
        });
    }
    
    function addAnswerInputToQuestion(accordionItem) {
        const questionId = accordionItem.dataset.questionId;
        if (!questionId) return;
        
        const accordionBody = accordionItem.querySelector('.accordion-body');
        if (!accordionBody) return;
        
        // Check if answer input already exists
        if (accordionBody.querySelector('.simple-answer-section')) return;
        
        // Create answer section
        const answerSection = document.createElement('div');
        answerSection.className = 'simple-answer-section border-top pt-3 mt-3';
        answerSection.innerHTML = `
            <div class="row align-items-center">
                <div class="col-md-8">
                    <label class="form-label text-primary fw-bold">
                        <i class="fas fa-star me-1"></i>Mi Respuesta:
                    </label>
                    <div class="input-group" style="max-width: 300px;">
                        <input type="number" 
                               class="form-control simple-answer-input" 
                               min="1" 
                               max="10" 
                               placeholder="1-10"
                               data-question-id="${questionId}"
                               value="${userAnswers[questionId] || ''}">
                        <span class="input-group-text">
                            <small>1=Bajo | 10=Alto</small>
                        </span>
                    </div>
                </div>
                <div class="col-md-4 text-end">
                    <span class="answer-status badge ${userAnswers[questionId] ? 'bg-success' : 'bg-secondary'}">
                        ${userAnswers[questionId] ? '<i class="fas fa-check me-1"></i>Respondida' : '<i class="fas fa-clock me-1"></i>Sin responder'}
                    </span>
                </div>
            </div>
        `;
        
        // Insert at the beginning of accordion body
        accordionBody.insertBefore(answerSection, accordionBody.firstChild);
        
        // Add event listener
        const input = answerSection.querySelector('.simple-answer-input');
        input.addEventListener('input', function() {
            const value = parseInt(this.value);
            if (value < 1) this.value = 1;
            if (value > 10) this.value = 10;
            
            // Update local storage
            if (value >= 1 && value <= 10) {
                userAnswers[questionId] = value;
                updateAnswerStatus(accordionItem, true);
            } else {
                delete userAnswers[questionId];
                updateAnswerStatus(accordionItem, false);
            }
            
            updateSaveButtonState();
        });
    }
    
    function updateAnswerStatus(accordionItem, isAnswered) {
        const statusBadge = accordionItem.querySelector('.answer-status');
        if (statusBadge) {
            if (isAnswered) {
                statusBadge.className = 'answer-status badge bg-success';
                statusBadge.innerHTML = '<i class="fas fa-check me-1"></i>Respondida';
            } else {
                statusBadge.className = 'answer-status badge bg-secondary';
                statusBadge.innerHTML = '<i class="fas fa-clock me-1"></i>Sin responder';
            }
        }
    }
    
    function addSaveAnswersButton() {
        // Check if button already exists
        if (document.getElementById('simpleSaveAnswersBtn')) return;
        
        // Find the main button container
        const mainButtonContainer = document.querySelector('.text-center.mt-4.mb-4');
        if (!mainButtonContainer) return;
        
        // Create save answers button
        const saveButton = document.createElement('button');
        saveButton.id = 'simpleSaveAnswersBtn';
        saveButton.className = 'btn btn-success ms-2';
        saveButton.innerHTML = '<i class="fas fa-save me-2"></i>Guardar Mis Respuestas';
        saveButton.disabled = true;
        saveButton.onclick = saveAnswers;
        
        // Add progress indicator
        const progressDiv = document.createElement('div');
        progressDiv.id = 'answersProgress';
        progressDiv.className = 'mt-3 text-muted';
        progressDiv.innerHTML = '<small>Responda al menos una pregunta para guardar</small>';
        
        mainButtonContainer.appendChild(saveButton);
        mainButtonContainer.appendChild(progressDiv);
    }
    
    function updateSaveButtonState() {
        const saveBtn = document.getElementById('simpleSaveAnswersBtn');
        const progressDiv = document.getElementById('answersProgress');
        
        if (!saveBtn) return;
        
        const answeredCount = Object.keys(userAnswers).length;
        const totalQuestions = document.querySelectorAll('.accordion-item').length;
        
        saveBtn.disabled = answeredCount === 0;
        
        if (progressDiv) {
            if (answeredCount > 0) {
                const percentage = Math.round((answeredCount / totalQuestions) * 100);
                progressDiv.innerHTML = `
                    <small class="text-success">
                        <i class="fas fa-chart-line me-1"></i>
                        ${answeredCount} de ${totalQuestions} preguntas respondidas (${percentage}%)
                    </small>
                `;
            } else {
                progressDiv.innerHTML = '<small class="text-muted">Responda al menos una pregunta para guardar</small>';
            }
        }
    }
    
    async function saveAnswers() {
        const saveBtn = document.getElementById('simpleSaveAnswersBtn');
        if (!saveBtn || saveBtn.disabled) return;
        
        // Get current area ID
        const activeArea = document.querySelector('.life-areas-nav .nav-link.active');
        if (!activeArea) {
            showSimpleToast('Por favor seleccione un área de vida', 'error');
            return;
        }
        
        const areaId = parseInt(activeArea.dataset.areaId);
        
        // Prepare answers array
        const answers = Object.entries(userAnswers).map(([questionId, score]) => ({
            questionId: parseInt(questionId),
            score: score
        }));
        
        if (answers.length === 0) {
            showSimpleToast('Por favor responda al menos una pregunta', 'error');
            return;
        }
        
        // Update button state
        const originalText = saveBtn.innerHTML;
        saveBtn.disabled = true;
        saveBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Guardando...';
        
        try {
            // Get CSRF token
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            
            const response = await fetch('/XRayLife/SaveAnswers', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify({
                    areaId: areaId,
                    answers: answers
                })
            });
            
            const result = await response.json();
            
            if (result.success) {
                showSimpleToast('✅ ' + (result.message || 'Respuestas guardadas exitosamente'), 'success');
                
                // Update all answered questions to show saved state
                document.querySelectorAll('.simple-answer-input').forEach(input => {
                    if (input.value) {
                        const accordionItem = input.closest('.accordion-item');
                        updateAnswerStatus(accordionItem, true);
                    }
                });
            } else {
                showSimpleToast('❌ ' + (result.message || 'Error al guardar las respuestas'), 'error');
            }
        } catch (error) {
            console.error('Error:', error);
            showSimpleToast('❌ Error de conexión. Por favor intente nuevamente.', 'error');
        } finally {
            saveBtn.disabled = false;
            saveBtn.innerHTML = originalText;
            updateSaveButtonState();
        }
    }
    
    function showSimpleToast(message, type = 'success') {
        // Create a simple alert div at the top of the accordion
        const accordionContainer = document.getElementById('xrayQuestionsAccordion');
        if (!accordionContainer) return;
        
        // Remove existing alerts
        const existingAlert = document.querySelector('.simple-answer-alert');
        if (existingAlert) existingAlert.remove();
        
        const alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type === 'success' ? 'success' : 'danger'} alert-dismissible fade show simple-answer-alert`;
        alertDiv.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;
        
        accordionContainer.parentElement.insertBefore(alertDiv, accordionContainer);
        
        // Auto-dismiss after 5 seconds
        setTimeout(() => {
            if (alertDiv.parentElement) {
                alertDiv.remove();
            }
        }, 5000);
    }
    
    function observeAccordionChanges() {
        const accordionContainer = document.getElementById('xrayQuestionsAccordion');
        if (!accordionContainer) return;
        
        // Create observer to watch for new questions
        const observer = new MutationObserver(function(mutations) {
            mutations.forEach(function(mutation) {
                if (mutation.type === 'childList') {
                    mutation.addedNodes.forEach(function(node) {
                        if (node.nodeType === 1 && node.classList.contains('accordion-item')) {
                            setTimeout(() => addAnswerInputToQuestion(node), 100);
                        }
                    });
                }
            });
        });
        
        observer.observe(accordionContainer, { childList: true });
    }
    
    // Load existing answers when area changes
    document.addEventListener('click', async function(e) {
        if (e.target.closest('.life-areas-nav .nav-link')) {
            // Clear current answers
            userAnswers = {};
            
            // Wait for questions to load
            setTimeout(async () => {
                const activeArea = document.querySelector('.life-areas-nav .nav-link.active');
                if (activeArea) {
                    const areaId = activeArea.dataset.areaId;
                    await loadExistingAnswers(areaId);
                }
            }, 500);
        }
    });
    
    async function loadExistingAnswers(areaId) {
        try {
            const response = await fetch(`/XRayLife/GetUserAnswersByArea?areaId=${areaId}`);
            const result = await response.json();
            
            if (result.success && result.questions) {
                // Clear and populate userAnswers
                userAnswers = {};
                result.questions.forEach(q => {
                    if (q.existingScore) {
                        userAnswers[q.questionId] = q.existingScore;
                    }
                });
                
                // Update all inputs
                document.querySelectorAll('.simple-answer-input').forEach(input => {
                    const questionId = input.dataset.questionId;
                    if (userAnswers[questionId]) {
                        input.value = userAnswers[questionId];
                        const accordionItem = input.closest('.accordion-item');
                        updateAnswerStatus(accordionItem, true);
                    }
                });
                
                updateSaveButtonState();
            }
        } catch (error) {
            console.error('Error loading existing answers:', error);
        }
    }
    
    // Initial load
    const activeArea = document.querySelector('.life-areas-nav .nav-link.active');
    if (activeArea) {
        const areaId = activeArea.dataset.areaId;
        setTimeout(() => loadExistingAnswers(areaId), 1000);
    }
});