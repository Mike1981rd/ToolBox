document.addEventListener('DOMContentLoaded', function () {
    const lifeAreaLinks = document.querySelectorAll('.life-areas-nav .nav-link');
    const xrayQuestionsAccordion = document.getElementById('xrayQuestionsAccordion');
    const allQuestionItems = xrayQuestionsAccordion ? Array.from(xrayQuestionsAccordion.querySelectorAll('.accordion-item')) : [];
    const questionSearchInput = document.getElementById('xrayQuestionSearchInput');

    console.log('X-Ray Life initialized:', {
        lifeAreaLinks: lifeAreaLinks.length,
        accordion: !!xrayQuestionsAccordion,
        questionItems: allQuestionItems.length,
        searchInput: !!questionSearchInput
    });

    // Verificar que todas las preguntas tengan data-question-area-id
    allQuestionItems.forEach((item, index) => {
        const areaId = item.dataset.questionAreaId;
        console.log(`Question ${index + 1} - Area ID: ${areaId}`);
    });

    // Filtrado inicial al cargar la página
    const initialActiveAreaLink = document.querySelector('.life-areas-nav .nav-link.active');
    const initialAreaId = initialActiveAreaLink ? initialActiveAreaLink.dataset.areaId : 'all';
    const initialSearchText = questionSearchInput ? questionSearchInput.value : '';
    
    if (allQuestionItems.length > 0) {
        console.log('🚀 Applying initial filter...');
        filterQuestions(initialAreaId, initialSearchText);
    }

    // Filtrado por Área de Vida
    lifeAreaLinks.forEach(link => {
        link.addEventListener('click', function (event) {
            event.preventDefault();
            console.log('🖱️ Area clicked:', this.dataset.areaId);
            
            // Actualizar estado activo
            lifeAreaLinks.forEach(l => l.classList.remove('active'));
            this.classList.add('active');
            
            const selectedAreaId = this.dataset.areaId;
            const currentSearchText = questionSearchInput ? questionSearchInput.value : '';
            filterQuestions(selectedAreaId, currentSearchText);
        });
    });

    // Filtrado por Búsqueda de Texto
    if (questionSearchInput) {
        questionSearchInput.addEventListener('input', function () {
            console.log('🔤 Search input changed:', this.value);
            const activeAreaLink = document.querySelector('.life-areas-nav .nav-link.active');
            const selectedAreaId = activeAreaLink ? activeAreaLink.dataset.areaId : 'all';
            filterQuestions(selectedAreaId, this.value);
        });
    }

    function filterQuestions(selectedAreaId, searchText) {
        let questionsFound = false;
        const lowerSearchText = searchText ? searchText.toLowerCase().trim() : '';
        const lowerSelectedAreaId = selectedAreaId ? selectedAreaId.toLowerCase() : 'all';

        console.clear(); // Limpiar consola para facilitar debugging
        console.log(`🔍 FILTERING - Selected Area ID: "${lowerSelectedAreaId}", Search Text: "${lowerSearchText}"`);
        console.log(`📊 Total questions to filter: ${allQuestionItems.length}`);

        allQuestionItems.forEach((item, index) => {
            const itemAreaIdAttribute = item.dataset.questionAreaId;
            const lowerItemAreaId = itemAreaIdAttribute ? itemAreaIdAttribute.toLowerCase() : '';

            const questionButton = item.querySelector('.accordion-button span[data-translate-key]');
            const questionText = questionButton ? questionButton.textContent.toLowerCase() : '';

            console.log(`\n📝 Question ${index + 1}:`);
            console.log(`   Area ID: "${lowerItemAreaId}"`);
            console.log(`   Text: "${questionText.substring(0, 50)}${questionText.length > 50 ? '...' : ''}"`);

            // Lógica de coincidencia mejorada
            const matchesArea = (lowerSelectedAreaId === 'all' || lowerItemAreaId === lowerSelectedAreaId);
            const matchesSearch = (lowerSearchText === '' || questionText.includes(lowerSearchText));

            console.log(`   ✓ Matches Area: ${matchesArea} (selected: "${lowerSelectedAreaId}", item: "${lowerItemAreaId}")`);
            console.log(`   ✓ Matches Search: ${matchesSearch} (search: "${lowerSearchText}")`);

            if (matchesArea && matchesSearch) {
                item.style.display = '';
                questionsFound = true;
                console.log(`   ✅ SHOWN`);
            } else {
                item.style.display = 'none';
                console.log(`   ❌ HIDDEN`);
            }
        });

        // Manejar mensaje de "no encontrado"
        handleNoResultsMessage(!questionsFound, allQuestionItems.length > 0);
        
        console.log(`\n🎯 Final result: ${questionsFound ? 'Questions found' : 'No questions match criteria'}`);
    }

    function handleNoResultsMessage(showMessage, hasItems) {
        let existingNoResultsMessage = xrayQuestionsAccordion.querySelector('.no-results-message');
        
        if (showMessage && hasItems) {
            if (!existingNoResultsMessage) {
                existingNoResultsMessage = document.createElement('div');
                existingNoResultsMessage.className = 'text-center p-5 text-muted no-results-message';
                existingNoResultsMessage.innerHTML = `
                    <i class="fas fa-search fa-3x mb-3 text-secondary"></i>
                    <h5 data-translate-key="no_questions_found_filter">No questions match your criteria</h5>
                    <p class="mb-0 text-muted">Try adjusting your search or selecting a different area</p>
                `;
                xrayQuestionsAccordion.appendChild(existingNoResultsMessage);
                
                // Aplicar traducción usando la función disponible
                applySingleTranslation(existingNoResultsMessage.querySelector('[data-translate-key]'));
            }
            existingNoResultsMessage.style.display = 'block';
        } else if (existingNoResultsMessage) {
            existingNoResultsMessage.style.display = 'none';
        }
    }

    // Función para aplicar traducción a un elemento individual
    function applySingleTranslation(element) {
        if (element && typeof getTranslation === 'function') {
            const key = element.dataset.translateKey;
            if (key) {
                element.textContent = getTranslation(key);
            }
        }
    }

    function toggleNoResultsMessage(show, hasFilters) {
        let noResultsMsg = document.getElementById('noQuestionsMessage');
        
        if (show && hasFilters && !noResultsMsg) {
            // Crear mensaje de no resultados
            noResultsMsg = document.createElement('div');
            noResultsMsg.id = 'noQuestionsMessage';
            noResultsMsg.className = 'text-center py-5 text-muted';
            noResultsMsg.innerHTML = `
                <i class="fas fa-search fa-3x mb-3"></i>
                <h5 data-translate-key="no_questions_found">No questions found</h5>
                <p>Try adjusting your search or filter criteria</p>
            `;
            
            const accordion = document.getElementById('xrayQuestionsAccordion');
            if (accordion) {
                accordion.appendChild(noResultsMsg);
            }
        } else if (!show && noResultsMsg) {
            noResultsMsg.remove();
        }
    }

    // Lógica para Offcanvas (similar a otros módulos)
    const addEditOffcanvasElementXRay = document.getElementById('addEditXRayQuestionOffcanvas');
    const offcanvasFormXRay = document.getElementById('addEditXRayQuestionForm');
    const offcanvasTitleXRay = document.getElementById('addEditXRayQuestionOffcanvasLabel');
    const xrayQuestionIdInput = document.getElementById('xrayQuestionId');
    const submitButtonXRay = offcanvasFormXRay ? offcanvasFormXRay.querySelector('.data-submit') : null;

    function setupOffcanvasForNewXRayQuestion() {
        if (!offcanvasFormXRay || !xrayQuestionIdInput || !offcanvasTitleXRay || !submitButtonXRay) return;
        offcanvasFormXRay.reset();
        xrayQuestionIdInput.value = '0';
        offcanvasTitleXRay.dataset.translateKey = 'offcanvas_add_xray_question_title';
        offcanvasTitleXRay.textContent = getTranslation('offcanvas_add_xray_question_title');
        submitButtonXRay.dataset.translateKey = 'add_button';
        submitButtonXRay.textContent = getTranslation('add_button');
        // Pre-seleccionar Área de Vida si un filtro está activo
        const activeAreaFilterLink = document.querySelector('.life-areas-nav .nav-link.active');
        if (activeAreaFilterLink && activeAreaFilterLink.dataset.areaId !== 'all') {
            document.getElementById('xrayLifeAreaCategory').value = activeAreaFilterLink.dataset.areaId;
        } else {
             document.getElementById('xrayLifeAreaCategory').value = "";
        }
    }
    
    document.getElementById('addNewXRayQuestionBtnGlobal')?.addEventListener('click', setupOffcanvasForNewXRayQuestion);
    // Reusar este para el botón dentro del panel izquierdo si hay uno:
    // document.getElementById('addNewXRayQuestionBtnSidebar')?.addEventListener('click', setupOffcanvasForNewXRayQuestion);


    document.querySelectorAll('.edit-xray-question-btn').forEach(button => {
        button.addEventListener('click', function () {
            if (!offcanvasFormXRay || !xrayQuestionIdInput || !offcanvasTitleXRay || !submitButtonXRay) return;
            const questionId = this.dataset.id;
            // Simular carga de datos para edición
            const questionData = getXRayQuestionMockData(questionId); // Placeholder
            
            offcanvasFormXRay.reset();
            xrayQuestionIdInput.value = questionId;
            document.getElementById('xrayLifeAreaCategory').value = questionData.areaId;
            document.getElementById('xrayQuestionText').value = getTranslation(questionData.questionTextKey); // Asume traducción

            offcanvasTitleXRay.dataset.translateKey = 'offcanvas_edit_xray_question_title';
            offcanvasTitleXRay.textContent = getTranslation('offcanvas_edit_xray_question_title');
            submitButtonXRay.dataset.translateKey = 'save_changes_button';
            submitButtonXRay.textContent = getTranslation('save_changes_button');
        });
    });
    
    function getXRayQuestionMockData(id) { // Placeholder
        const mockQuestions = {
            "q1": { areaId: "1", questionTextKey: "xray_q_spirit_1" },
            "q2": { areaId: "1", questionTextKey: "xray_q_spirit_2" },
            "q3": { areaId: "2", questionTextKey: "xray_q_growth_1" },
            "q4": { areaId: "3", questionTextKey: "xray_q_social_1" },
            "q5": { areaId: "4", questionTextKey: "xray_q_family_1" }
        };
        return mockQuestions[id] || { areaId: "1", questionTextKey: "Unknown Question" };
    }

    if (offcanvasFormXRay) {
        offcanvasFormXRay.addEventListener('submit', function (event) {
            event.preventDefault();
            // Lógica AJAX para guardar...
            const offcanvasInstance = bootstrap.Offcanvas.getInstance(addEditOffcanvasElementXRay);
            if (offcanvasInstance) offcanvasInstance.hide();
        });
    }
});