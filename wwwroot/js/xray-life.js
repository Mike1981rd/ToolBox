/**
 * XRay Life Module JavaScript
 * Handles questions CRUD operations for life areas
 */

document.addEventListener('DOMContentLoaded', function() {
    // Elements
    const offcanvasElement = document.getElementById('addEditXRayQuestionOffcanvas');
    const offcanvas = new bootstrap.Offcanvas(offcanvasElement);
    const offcanvasForm = document.getElementById('addEditXRayQuestionForm');
    const offcanvasTitle = document.getElementById('addEditXRayQuestionOffcanvasLabel');
    const questionIdInput = document.getElementById('xrayQuestionId');
    const lifeAreaSelect = document.getElementById('xrayLifeAreaCategory');
    const questionTextarea = document.getElementById('xrayQuestionText');
    const searchInput = document.getElementById('xrayQuestionSearchInput');
    
    // Variables
    let currentAreaId = null;
    let isEditMode = false;
    
    // Initialize - Set first area as active
    const firstArea = document.querySelector('.life-areas-nav .nav-link');
    if (firstArea) {
        currentAreaId = firstArea.dataset.areaId;
    }
    
    // Life Area Navigation Click Handler
    document.querySelectorAll('.life-areas-nav .nav-link').forEach(link => {
        link.addEventListener('click', async function(e) {
            e.preventDefault();
            
            // Update active state
            document.querySelectorAll('.life-areas-nav .nav-link').forEach(l => l.classList.remove('active'));
            this.classList.add('active');
            
            // Load questions for selected area
            currentAreaId = this.dataset.areaId;
            await loadQuestionsByArea(currentAreaId);
        });
    });
    
    // Add New Question Button
    const addNewBtn = document.getElementById('addNewXRayQuestionBtnMain');
    if (addNewBtn) {
        addNewBtn.addEventListener('click', function() {
            resetForm();
            isEditMode = false;
            offcanvasTitle.textContent = 'Agregar Nueva Pregunta';
            
            // Pre-select current area if available
            if (currentAreaId && lifeAreaSelect) {
                lifeAreaSelect.value = currentAreaId;
            }
        });
    }
    
    // Edit Question Button Click Handler (Event Delegation)
    document.addEventListener('click', async function(e) {
        if (e.target.closest('.edit-xray-question-btn')) {
            e.preventDefault();
            const button = e.target.closest('.edit-xray-question-btn');
            const questionId = button.dataset.id;
            
            await loadQuestionForEdit(questionId);
        }
        
        // Delete Question Button Click Handler
        if (e.target.closest('.delete-xray-question-btn')) {
            e.preventDefault();
            const button = e.target.closest('.delete-xray-question-btn');
            const questionId = button.dataset.id;
            
            if (confirm('¿Está seguro de que desea eliminar esta pregunta? Esta acción no se puede deshacer.')) {
                await deleteQuestion(questionId);
            }
        }
    });
    
    // Form Submit Handler
    offcanvasForm.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        const formData = {
            QuestionText: questionTextarea.value.trim(),
            LifeAreaId: parseInt(lifeAreaSelect.value)
        };
        
        if (!formData.QuestionText || !formData.LifeAreaId) {
            showToast('Por favor complete todos los campos requeridos', 'error');
            return;
        }
        
        if (isEditMode) {
            formData.Id = parseInt(questionIdInput.value);
            await updateQuestion(formData);
        } else {
            await createQuestion(formData);
        }
    });
    
    // Search Input Handler
    let searchTimeout;
    searchInput.addEventListener('input', function() {
        clearTimeout(searchTimeout);
        const searchTerm = this.value.trim();
        
        searchTimeout = setTimeout(async () => {
            if (searchTerm.length >= 3 || searchTerm.length === 0) {
                await searchQuestions(searchTerm, currentAreaId);
            }
        }, 300);
    });
    
    // Functions
    
    async function loadQuestionsByArea(areaId) {
        try {
            const response = await fetch(`/XRayLife/GetQuestionsByArea?areaId=${areaId}`);
            const result = await response.json();
            
            if (result.success) {
                renderQuestions(result.questions);
            } else {
                showToast('Error al cargar las preguntas', 'error');
            }
        } catch (error) {
            console.error('Error:', error);
            showToast('Error de conexión', 'error');
        }
    }
    
    async function loadQuestionForEdit(questionId) {
        try {
            const response = await fetch(`/XRayLife/GetQuestionById?id=${questionId}`);
            const result = await response.json();
            
            if (result.success) {
                const question = result.question;
                
                // Set form to edit mode
                isEditMode = true;
                offcanvasTitle.textContent = 'Editar Pregunta';
                questionIdInput.value = question.id;
                lifeAreaSelect.value = question.lifeAreaId;
                questionTextarea.value = question.questionText;
                
                // Show offcanvas
                offcanvas.show();
            } else {
                showToast(result.message || 'Error al cargar la pregunta', 'error');
            }
        } catch (error) {
            console.error('Error:', error);
            showToast('Error de conexión', 'error');
        }
    }
    
    async function createQuestion(data) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            
            const response = await fetch('/XRayLife/CreateQuestion', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify(data)
            });
            
            const result = await response.json();
            
            if (result.success) {
                showToast(result.message || 'Pregunta creada exitosamente', 'success');
                offcanvas.hide();
                resetForm();
                
                // Reload questions for current area
                if (currentAreaId) {
                    await loadQuestionsByArea(currentAreaId);
                }
            } else {
                showToast(result.message || 'Error al crear la pregunta', 'error');
                if (result.errors) {
                    console.error('Validation errors:', result.errors);
                }
            }
        } catch (error) {
            console.error('Error:', error);
            showToast('Error de conexión', 'error');
        }
    }
    
    async function updateQuestion(data) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            
            const response = await fetch(`/XRayLife/UpdateQuestion/${data.Id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify(data)
            });
            
            const result = await response.json();
            
            if (result.success) {
                showToast(result.message || 'Pregunta actualizada exitosamente', 'success');
                offcanvas.hide();
                resetForm();
                
                // Reload questions for current area
                if (currentAreaId) {
                    await loadQuestionsByArea(currentAreaId);
                }
            } else {
                showToast(result.message || 'Error al actualizar la pregunta', 'error');
                if (result.errors) {
                    console.error('Validation errors:', result.errors);
                }
            }
        } catch (error) {
            console.error('Error:', error);
            showToast('Error de conexión', 'error');
        }
    }
    
    async function deleteQuestion(questionId) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            const formData = new URLSearchParams();
            formData.append('__RequestVerificationToken', token);
            
            const response = await fetch(`/XRayLife/DeleteQuestion/${questionId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                },
                body: formData
            });
            
            const result = await response.json();
            
            if (result.success) {
                showToast(result.message || 'Pregunta eliminada exitosamente', 'success');
                
                // Remove question from DOM
                const questionElement = document.querySelector(`[data-question-id="${questionId}"]`);
                if (questionElement) {
                    questionElement.style.transition = 'opacity 0.3s ease-out';
                    questionElement.style.opacity = '0';
                    setTimeout(() => {
                        questionElement.remove();
                        
                        // Check if there are no more questions
                        const remainingQuestions = document.querySelectorAll('.accordion-item');
                        if (remainingQuestions.length === 0) {
                            renderNoQuestionsMessage();
                        }
                    }, 300);
                }
            } else {
                showToast(result.message || 'Error al eliminar la pregunta', 'error');
            }
        } catch (error) {
            console.error('Error:', error);
            showToast('Error de conexión', 'error');
        }
    }
    
    async function searchQuestions(searchTerm, areaId) {
        try {
            const params = new URLSearchParams();
            params.append('searchTerm', searchTerm);
            if (areaId) params.append('areaId', areaId);
            
            const response = await fetch(`/XRayLife/SearchQuestions?${params}`);
            const result = await response.json();
            
            if (result.success) {
                renderQuestions(result.questions);
            } else {
                showToast('Error al buscar preguntas', 'error');
            }
        } catch (error) {
            console.error('Error:', error);
            showToast('Error de conexión', 'error');
        }
    }
    
    function renderQuestions(questions) {
        const accordionContainer = document.getElementById('xrayQuestionsAccordion');
        
        if (!questions || questions.length === 0) {
            renderNoQuestionsMessage();
            return;
        }
        
        const questionsHtml = questions.map(q => `
            <div class="accordion-item mb-3 shadow-sm" data-question-area-id="${q.lifeAreaId}" data-question-id="${q.id}">
                <h2 class="accordion-header" id="heading_${q.id}">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse_${q.id}" aria-expanded="false" aria-controls="collapse_${q.id}">
                        <span>${escapeHtml(q.questionText)}</span>
                    </button>
                </h2>
                <div id="collapse_${q.id}" class="accordion-collapse collapse" aria-labelledby="heading_${q.id}" data-bs-parent="#xrayQuestionsAccordion">
                    <div class="accordion-body">
                        <div class="mt-2 text-end">
                            <button class="btn btn-sm btn-outline-primary edit-xray-question-btn" data-id="${q.id}">
                                <i class="fas fa-edit me-1"></i> <span>Editar</span>
                            </button>
                            <button class="btn btn-sm btn-outline-danger delete-xray-question-btn" data-id="${q.id}">
                                <i class="fas fa-trash me-1"></i> <span>Eliminar</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        `).join('');
        
        accordionContainer.innerHTML = questionsHtml;
    }
    
    function renderNoQuestionsMessage() {
        const accordionContainer = document.getElementById('xrayQuestionsAccordion');
        accordionContainer.innerHTML = `
            <div class="card shadow-sm">
                <div class="card-body text-center">
                    <i class="fas fa-question-circle fa-3x text-muted mb-3"></i>
                    <p>No se encontraron preguntas para esta área.</p>
                </div>
            </div>
        `;
    }
    
    function resetForm() {
        offcanvasForm.reset();
        questionIdInput.value = '0';
        isEditMode = false;
    }
    
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
    
    function showToast(message, type = 'success') {
        // Create toast container if it doesn't exist
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
        
        // Remove toast element after it's hidden
        toastElement.addEventListener('hidden.bs.toast', function() {
            toastElement.remove();
        });
    }
});

// Add CSRF token to page if not present
document.addEventListener('DOMContentLoaded', function() {
    if (!document.querySelector('input[name="__RequestVerificationToken"]')) {
        const tokenInput = document.createElement('input');
        tokenInput.type = 'hidden';
        tokenInput.name = '__RequestVerificationToken';
        tokenInput.value = document.querySelector('meta[name="csrf-token"]')?.content || '';
        document.body.appendChild(tokenInput);
    }
});