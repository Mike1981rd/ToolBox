/**
 * XRay Life User Answers Module JavaScript
 * Handles user answers functionality for life area questions
 */

document.addEventListener('DOMContentLoaded', function() {
    // Configuration
    const config = {
        currentAreaId: null,
        currentQuestions: [],
        baseUrl: '/XRayLife',
        isLoading: false
    };

    // Initialize module
    initAnswersModule();

    function initAnswersModule() {
        // Check if we're in answer mode
        const answerModeBtn = document.getElementById('toggleAnswerMode');
        if (!answerModeBtn) {
            // Create answer mode toggle button
            createAnswerModeToggle();
        }
    }

    function createAnswerModeToggle() {
        const addQuestionBtn = document.getElementById('addNewXRayQuestionBtnMain');
        if (addQuestionBtn && addQuestionBtn.parentElement) {
            const toggleBtn = document.createElement('button');
            toggleBtn.id = 'toggleAnswerMode';
            toggleBtn.className = 'btn btn-info ms-2';
            toggleBtn.innerHTML = '<i class="fas fa-clipboard-check me-2"></i>Modo Responder';
            toggleBtn.type = 'button';
            toggleBtn.onclick = toggleAnswerMode;
            addQuestionBtn.parentElement.appendChild(toggleBtn);
        }
    }

    function toggleAnswerMode() {
        const isInAnswerMode = document.body.classList.contains('answer-mode');
        
        if (!isInAnswerMode) {
            // Enter answer mode
            enterAnswerMode();
        } else {
            // Exit answer mode
            exitAnswerMode();
        }
    }

    function enterAnswerMode() {
        document.body.classList.add('answer-mode');
        
        // Update button
        const toggleBtn = document.getElementById('toggleAnswerMode');
        toggleBtn.className = 'btn btn-secondary ms-2';
        toggleBtn.innerHTML = '<i class="fas fa-edit me-2"></i>Modo Administrar';
        
        // Hide admin controls
        document.querySelectorAll('.edit-xray-question-btn, .delete-xray-question-btn').forEach(btn => {
            btn.style.display = 'none';
        });
        
        // Hide add question button
        const addBtn = document.getElementById('addNewXRayQuestionBtnMain');
        if (addBtn) addBtn.style.display = 'none';
        
        // Load questions with answer inputs
        const activeArea = document.querySelector('.life-areas-nav .nav-link.active');
        if (activeArea) {
            const areaId = activeArea.dataset.areaId;
            loadQuestionsForAnswering(areaId);
        }
    }

    function exitAnswerMode() {
        document.body.classList.remove('answer-mode');
        
        // Update button
        const toggleBtn = document.getElementById('toggleAnswerMode');
        toggleBtn.className = 'btn btn-info ms-2';
        toggleBtn.innerHTML = '<i class="fas fa-clipboard-check me-2"></i>Modo Responder';
        
        // Show admin controls
        document.querySelectorAll('.edit-xray-question-btn, .delete-xray-question-btn').forEach(btn => {
            btn.style.display = '';
        });
        
        // Show add question button
        const addBtn = document.getElementById('addNewXRayQuestionBtnMain');
        if (addBtn) addBtn.style.display = '';
        
        // Remove answer inputs
        document.querySelectorAll('.answer-input-section').forEach(section => {
            section.remove();
        });
        
        // Remove save answers button
        const saveBtn = document.getElementById('saveAnswersSection');
        if (saveBtn) saveBtn.remove();
    }

    async function loadQuestionsForAnswering(areaId) {
        if (config.isLoading) return;
        
        config.isLoading = true;
        config.currentAreaId = parseInt(areaId);
        
        try {
            const response = await fetch(`${config.baseUrl}/GetUserAnswersByArea?areaId=${areaId}`);
            const result = await response.json();
            
            if (result.success) {
                config.currentQuestions = result.questions;
                addAnswerInputsToQuestions(result.questions);
                showSaveAnswersButton();
                updateProgressDisplay(result.progress);
            } else {
                showToast(result.message || 'Error al cargar las preguntas', 'error');
            }
        } catch (error) {
            console.error('Error:', error);
            showToast('Error de conexión', 'error');
        } finally {
            config.isLoading = false;
        }
    }

    function addAnswerInputsToQuestions(questions) {
        questions.forEach(question => {
            const accordionItem = document.querySelector(`[data-question-id="${question.questionId}"]`);
            if (accordionItem) {
                const accordionBody = accordionItem.querySelector('.accordion-body');
                if (accordionBody) {
                    // Remove existing answer section if any
                    const existingSection = accordionBody.querySelector('.answer-input-section');
                    if (existingSection) existingSection.remove();
                    
                    // Create answer input section
                    const answerSection = document.createElement('div');
                    answerSection.className = 'answer-input-section mt-3 p-3 bg-light rounded';
                    answerSection.innerHTML = `
                        <div class="row align-items-center">
                            <div class="col-md-8">
                                <label class="form-label fw-semibold">Tu respuesta (1-10):</label>
                                <div class="d-flex align-items-center">
                                    <input type="number" 
                                           class="form-control answer-input" 
                                           min="1" 
                                           max="10" 
                                           value="${question.existingScore || ''}"
                                           data-question-id="${question.questionId}"
                                           style="width: 100px;">
                                    <span class="ms-3 text-muted">
                                        1 = Totalmente en desacuerdo | 10 = Totalmente de acuerdo
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-4 text-end">
                                ${question.existingScore ? 
                                    '<span class="badge bg-success"><i class="fas fa-check me-1"></i>Respondida</span>' : 
                                    '<span class="badge bg-secondary"><i class="fas fa-clock me-1"></i>Sin responder</span>'
                                }
                            </div>
                        </div>
                    `;
                    
                    // Insert before the action buttons
                    const actionButtons = accordionBody.querySelector('.text-end');
                    accordionBody.insertBefore(answerSection, actionButtons);
                    
                    // Auto-expand accordion
                    const collapseElement = accordionItem.querySelector('.accordion-collapse');
                    if (collapseElement && !collapseElement.classList.contains('show')) {
                        const accordionButton = accordionItem.querySelector('.accordion-button');
                        if (accordionButton) accordionButton.click();
                    }
                }
            }
        });

        // Add input event listeners
        document.querySelectorAll('.answer-input').forEach(input => {
            input.addEventListener('input', function() {
                const value = parseInt(this.value);
                if (value < 1) this.value = 1;
                if (value > 10) this.value = 10;
                updateSaveButtonState();
            });
        });
    }

    function showSaveAnswersButton() {
        // Remove existing save button if any
        let saveSection = document.getElementById('saveAnswersSection');
        if (!saveSection) {
            saveSection = document.createElement('div');
            saveSection.id = 'saveAnswersSection';
            saveSection.className = 'text-center mt-4 mb-4 p-4 bg-light rounded';
            
            const container = document.querySelector('.col-lg-9');
            if (container) {
                container.appendChild(saveSection);
            }
        }
        
        saveSection.innerHTML = `
            <button id="saveAnswersBtn" class="btn btn-success btn-lg" disabled>
                <i class="fas fa-save me-2"></i>Guardar Respuestas
            </button>
            <p class="text-muted mt-3 mb-0">
                Puede guardar sus respuestas aunque no haya completado todas las preguntas.
            </p>
            <div id="progressInfo" class="mt-2"></div>
        `;
        
        // Add event listener
        document.getElementById('saveAnswersBtn').addEventListener('click', saveAnswers);
        
        // Update button state
        updateSaveButtonState();
    }

    function updateProgressDisplay(progress) {
        const progressInfo = document.getElementById('progressInfo');
        if (progressInfo && progress) {
            const percentage = progress.percentage || 0;
            progressInfo.innerHTML = `
                <div class="progress mt-3" style="height: 25px;">
                    <div class="progress-bar bg-success" role="progressbar" 
                         style="width: ${percentage}%;" 
                         aria-valuenow="${percentage}" 
                         aria-valuemin="0" 
                         aria-valuemax="100">
                        ${progress.answered} de ${progress.total} preguntas respondidas (${percentage}%)
                    </div>
                </div>
            `;
        }
    }

    function updateSaveButtonState() {
        const inputs = document.querySelectorAll('.answer-input');
        const filledInputs = Array.from(inputs).filter(input => {
            const value = parseInt(input.value);
            return value >= 1 && value <= 10;
        });
        
        const saveBtn = document.getElementById('saveAnswersBtn');
        if (saveBtn) {
            // Enable if at least one answer is provided
            saveBtn.disabled = filledInputs.length === 0;
            
            // Update progress text
            const progressText = `${filledInputs.length} de ${inputs.length} preguntas respondidas`;
            const progressInfo = document.getElementById('progressInfo');
            if (progressInfo && !progressInfo.querySelector('.progress')) {
                progressInfo.insertAdjacentHTML('afterbegin', `<p class="mb-2">${progressText}</p>`);
            }
        }
    }

    async function saveAnswers() {
        const answers = [];
        document.querySelectorAll('.answer-input').forEach(input => {
            const questionId = parseInt(input.dataset.questionId);
            const value = parseInt(input.value);
            
            if (questionId && value >= 1 && value <= 10) {
                answers.push({
                    questionId: questionId,
                    score: value
                });
            }
        });

        if (answers.length === 0) {
            showToast('Por favor responda al menos una pregunta antes de guardar', 'error');
            return;
        }

        // Get CSRF token
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

        // Prepare request data
        const requestData = {
            areaId: config.currentAreaId,
            answers: answers
        };

        // Update button state
        const saveBtn = document.getElementById('saveAnswersBtn');
        const originalBtnHtml = saveBtn.innerHTML;
        saveBtn.disabled = true;
        saveBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Guardando...';

        try {
            const response = await fetch(`${config.baseUrl}/SaveAnswers`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify(requestData)
            });

            const result = await response.json();

            if (result.success) {
                showToast(result.message || 'Respuestas guardadas exitosamente', 'success');
                
                // Update UI to show answers are saved
                document.querySelectorAll('.answer-input').forEach(input => {
                    if (input.value) {
                        const accordionItem = input.closest('[data-question-id]');
                        const badge = accordionItem.querySelector('.badge');
                        if (badge) {
                            badge.className = 'badge bg-success';
                            badge.innerHTML = '<i class="fas fa-check me-1"></i>Respondida';
                        }
                    }
                });
                
                // Reload questions to get updated state
                loadQuestionsForAnswering(config.currentAreaId);
            } else {
                showToast(result.message || 'Error al guardar las respuestas', 'error');
            }
        } catch (error) {
            console.error('Error:', error);
            showToast('Error de conexión. Por favor intente nuevamente.', 'error');
        } finally {
            saveBtn.disabled = false;
            saveBtn.innerHTML = originalBtnHtml;
        }
    }

    function showToast(message, type = 'success') {
        // Use existing toast function from xray-life.js if available
        if (window.showToast) {
            window.showToast(message, type);
            return;
        }
        
        // Otherwise, create our own
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
        
        toastElement.addEventListener('hidden.bs.toast', function() {
            toastElement.remove();
        });
    }

    // Listen for area navigation clicks in answer mode
    document.addEventListener('click', function(e) {
        if (document.body.classList.contains('answer-mode')) {
            const areaLink = e.target.closest('.life-areas-nav .nav-link');
            if (areaLink) {
                setTimeout(() => {
                    const areaId = areaLink.dataset.areaId;
                    loadQuestionsForAnswering(areaId);
                }, 100);
            }
        }
    });
});