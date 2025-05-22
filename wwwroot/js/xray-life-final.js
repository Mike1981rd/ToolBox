/**
 * X-Ray Life Module - Final Robust Implementation
 * Handles combined filtering by life area and search text with complete case-insensitivity
 */
document.addEventListener('DOMContentLoaded', function () {
    console.log('🚀 X-Ray Life Module - Initializing...');
    
    // Core DOM elements
    const lifeAreaLinks = document.querySelectorAll('.life-areas-nav .nav-link');
    const xrayQuestionsAccordion = document.getElementById('xrayQuestionsAccordion');
    const allQuestionItems = xrayQuestionsAccordion ? Array.from(xrayQuestionsAccordion.querySelectorAll('.accordion-item')) : [];
    const questionSearchInput = document.getElementById('xrayQuestionSearchInput');

    // Verification of initialization
    console.log('📊 Initialization Status:', {
        lifeAreaLinks: lifeAreaLinks.length,
        accordion: !!xrayQuestionsAccordion,
        questionItems: allQuestionItems.length,
        searchInput: !!questionSearchInput
    });

    // Verify data attributes in questions
    console.log('🔍 Verifying question data attributes:');
    allQuestionItems.forEach((item, index) => {
        const questionAreaId = item.dataset.questionAreaId;
        const questionId = item.dataset.questionId;
        console.log(`Question ${index + 1}: Area ID="${questionAreaId}", Question ID="${questionId}"`);
        
        if (!questionAreaId) {
            console.warn(`⚠️ Question ${index + 1} missing data-question-area-id attribute`);
        }
    });

    // Verify data attributes in area links
    console.log('🔍 Verifying area link data attributes:');
    lifeAreaLinks.forEach((link, index) => {
        const areaId = link.dataset.areaId;
        const linkText = link.textContent.trim();
        console.log(`Area Link ${index + 1}: ID="${areaId}", Text="${linkText}"`);
        
        if (!areaId) {
            console.warn(`⚠️ Area link ${index + 1} missing data-area-id attribute`);
        }
    });

    /**
     * CORE FILTERING FUNCTION - Robust and Case-Insensitive
     * @param {string} selectedAreaId - The area ID to filter by ('all' for all areas)
     * @param {string} searchText - The search text to filter by
     */
    function filterQuestions(selectedAreaId, searchText) {
        console.log('\n🔍 === FILTERING STARTED ===');
        console.log(`Selected Area ID: "${selectedAreaId}"`);
        console.log(`Search Text: "${searchText}"`);
        
        let questionsFound = false;
        let questionsShown = 0;
        let questionsHidden = 0;
        
        // Normalize inputs - Case insensitive and trimmed
        const normalizedSearchText = searchText ? searchText.toLowerCase().trim() : '';
        const normalizedSelectedAreaId = selectedAreaId ? selectedAreaId.toLowerCase().trim() : 'all';
        
        console.log(`Normalized - Area: "${normalizedSelectedAreaId}", Search: "${normalizedSearchText}"`);

        allQuestionItems.forEach((item, index) => {
            // Get question area ID and normalize
            const itemAreaIdAttribute = item.dataset.questionAreaId;
            const normalizedItemAreaId = itemAreaIdAttribute ? itemAreaIdAttribute.toLowerCase().trim() : '';

            // Get question text and normalize
            const questionButton = item.querySelector('.accordion-button span[data-translate-key]');
            const questionText = questionButton ? questionButton.textContent.toLowerCase().trim() : '';

            console.log(`\n📝 Question ${index + 1}:`);
            console.log(`   Raw Area ID: "${itemAreaIdAttribute}"`);
            console.log(`   Normalized Area ID: "${normalizedItemAreaId}"`);
            console.log(`   Question Text: "${questionText.substring(0, 40)}${questionText.length > 40 ? '...' : ''}"`);

            // FILTERING LOGIC
            // Area matches if: selected is 'all' OR area IDs match exactly
            const matchesArea = (normalizedSelectedAreaId === 'all' || normalizedItemAreaId === normalizedSelectedAreaId);
            
            // Search matches if: no search text OR question text contains search text
            const matchesSearch = (normalizedSearchText === '' || questionText.includes(normalizedSearchText));

            console.log(`   ✓ Matches Area: ${matchesArea} (looking for "${normalizedSelectedAreaId}", found "${normalizedItemAreaId}")`);
            console.log(`   ✓ Matches Search: ${matchesSearch} (looking for "${normalizedSearchText}" in text)`);

            // Show/Hide based on combined criteria
            if (matchesArea && matchesSearch) {
                item.style.display = '';
                questionsFound = true;
                questionsShown++;
                console.log(`   ✅ SHOWN`);
            } else {
                item.style.display = 'none';
                questionsHidden++;
                console.log(`   ❌ HIDDEN`);
            }
        });

        // Handle "no results" message
        handleNoResultsMessage(!questionsFound, allQuestionItems.length > 0, normalizedSelectedAreaId, normalizedSearchText);
        
        console.log(`\n🎯 FILTERING RESULTS:`);
        console.log(`   Questions Shown: ${questionsShown}`);
        console.log(`   Questions Hidden: ${questionsHidden}`);
        console.log(`   Any Results Found: ${questionsFound}`);
        console.log('=== FILTERING COMPLETED ===\n');
        
        return questionsFound;
    }

    /**
     * Handle "No Results" message display
     */
    function handleNoResultsMessage(showMessage, hasItems, areaFilter, searchFilter) {
        let existingNoResultsMessage = xrayQuestionsAccordion.querySelector('.no-results-message');
        
        if (showMessage && hasItems) {
            if (!existingNoResultsMessage) {
                existingNoResultsMessage = document.createElement('div');
                existingNoResultsMessage.className = 'text-center p-5 text-muted no-results-message';
                
                let filterDescription = '';
                if (areaFilter !== 'all' && searchFilter) {
                    filterDescription = `in this area with "${searchFilter}"`;
                } else if (areaFilter !== 'all') {
                    filterDescription = 'in this area';
                } else if (searchFilter) {
                    filterDescription = `matching "${searchFilter}"`;
                }
                
                existingNoResultsMessage.innerHTML = `
                    <i class="fas fa-search fa-3x mb-3 text-secondary"></i>
                    <h5 data-translate-key="no_questions_found_filter">No questions found ${filterDescription}</h5>
                    <p class="mb-0 text-muted">Try adjusting your search or selecting a different area</p>
                `;
                xrayQuestionsAccordion.appendChild(existingNoResultsMessage);
                
                // Apply translation
                applySingleTranslation(existingNoResultsMessage.querySelector('[data-translate-key]'));
            }
            existingNoResultsMessage.style.display = 'block';
        } else if (existingNoResultsMessage) {
            existingNoResultsMessage.style.display = 'none';
        }
    }

    /**
     * Apply translation to a single element
     */
    function applySingleTranslation(element) {
        if (element && typeof getTranslation === 'function') {
            const key = element.dataset.translateKey;
            if (key) {
                element.textContent = getTranslation(key);
            }
        }
    }

    /**
     * EVENT LISTENERS - Life Area Filter Links
     */
    lifeAreaLinks.forEach(link => {
        link.addEventListener('click', function (event) {
            event.preventDefault();
            console.log(`🖱️ Area link clicked: "${this.dataset.areaId}"`);
            
            // Update active state visually
            lifeAreaLinks.forEach(l => l.classList.remove('active'));
            this.classList.add('active');
            
            // Get current filter values
            const selectedAreaId = this.dataset.areaId;
            const currentSearchText = questionSearchInput ? questionSearchInput.value : '';
            
            console.log(`Applying filter - Area: "${selectedAreaId}", preserving search: "${currentSearchText}"`);
            
            // Apply combined filter
            filterQuestions(selectedAreaId, currentSearchText);
        });
    });

    /**
     * EVENT LISTENER - Search Input
     */
    if (questionSearchInput) {
        questionSearchInput.addEventListener('input', function () {
            console.log(`🔤 Search input changed: "${this.value}"`);
            
            // Get currently active area
            const activeAreaLink = document.querySelector('.life-areas-nav .nav-link.active');
            const selectedAreaId = activeAreaLink ? activeAreaLink.dataset.areaId : 'all';
            
            console.log(`Applying filter - Search: "${this.value}", preserving area: "${selectedAreaId}"`);
            
            // Apply combined filter
            filterQuestions(selectedAreaId, this.value);
        });
        
        // Also handle 'keyup' for better responsiveness
        questionSearchInput.addEventListener('keyup', function () {
            if (this.value !== this.dataset.lastValue) {
                this.dataset.lastValue = this.value;
                const activeAreaLink = document.querySelector('.life-areas-nav .nav-link.active');
                const selectedAreaId = activeAreaLink ? activeAreaLink.dataset.areaId : 'all';
                filterQuestions(selectedAreaId, this.value);
            }
        });
    }

    /**
     * INITIAL FILTER APPLICATION
     * Apply filter based on initial state when page loads
     */
    function applyInitialFilter() {
        console.log('🚀 Applying initial filter...');
        
        const initialActiveAreaLink = document.querySelector('.life-areas-nav .nav-link.active');
        const initialAreaId = initialActiveAreaLink ? initialActiveAreaLink.dataset.areaId : 'all';
        const initialSearchText = questionSearchInput ? questionSearchInput.value : '';
        
        console.log(`Initial state - Area: "${initialAreaId}", Search: "${initialSearchText}"`);
        
        if (allQuestionItems.length > 0) {
            filterQuestions(initialAreaId, initialSearchText);
        } else {
            console.warn('⚠️ No questions found to filter');
        }
    }

    // Apply initial filter
    applyInitialFilter();

    /**
     * OFFCANVAS FUNCTIONALITY
     */
    const addEditOffcanvasElementXRay = document.getElementById('addEditXRayQuestionOffcanvas');
    const offcanvasFormXRay = document.getElementById('addEditXRayQuestionForm');
    const offcanvasTitleXRay = document.getElementById('addEditXRayQuestionOffcanvasLabel');
    const xrayQuestionIdInput = document.getElementById('xrayQuestionId');
    const submitButtonXRay = offcanvasFormXRay ? offcanvasFormXRay.querySelector('.data-submit') : null;

    function setupOffcanvasForNewXRayQuestion() {
        if (!offcanvasFormXRay || !xrayQuestionIdInput || !offcanvasTitleXRay || !submitButtonXRay) return;
        
        console.log('📝 Setting up offcanvas for new question');
        
        offcanvasFormXRay.reset();
        xrayQuestionIdInput.value = '0';
        offcanvasTitleXRay.dataset.translateKey = 'offcanvas_add_xray_question_title';
        offcanvasTitleXRay.textContent = getTranslation('offcanvas_add_xray_question_title');
        submitButtonXRay.dataset.translateKey = 'add_button';
        submitButtonXRay.textContent = getTranslation('add_button');
        
        // Pre-select current area filter if not "all"
        const activeAreaFilterLink = document.querySelector('.life-areas-nav .nav-link.active');
        const lifeAreaSelect = document.getElementById('xrayLifeAreaCategory');
        
        if (activeAreaFilterLink && activeAreaFilterLink.dataset.areaId !== 'all' && lifeAreaSelect) {
            lifeAreaSelect.value = activeAreaFilterLink.dataset.areaId;
            console.log(`Pre-selected area: ${activeAreaFilterLink.dataset.areaId}`);
        } else if (lifeAreaSelect) {
            lifeAreaSelect.value = "";
        }
    }
    
    // Add question button
    document.getElementById('addNewXRayQuestionBtnGlobal')?.addEventListener('click', setupOffcanvasForNewXRayQuestion);

    // Edit question buttons
    document.querySelectorAll('.edit-xray-question-btn').forEach(button => {
        button.addEventListener('click', function () {
            if (!offcanvasFormXRay || !xrayQuestionIdInput || !offcanvasTitleXRay || !submitButtonXRay) return;
            
            const questionId = this.dataset.id;
            console.log(`✏️ Setting up offcanvas for editing question: ${questionId}`);
            
            const questionData = getXRayQuestionMockData(questionId);
            
            offcanvasFormXRay.reset();
            xrayQuestionIdInput.value = questionId;
            
            const lifeAreaSelect = document.getElementById('xrayLifeAreaCategory');
            const questionTextarea = document.getElementById('xrayQuestionText');
            
            if (lifeAreaSelect) {
                lifeAreaSelect.value = questionData.areaId;
                console.log(`Pre-selected area for edit: ${questionData.areaId}`);
            }
            
            if (questionTextarea) {
                questionTextarea.value = getTranslation(questionData.questionTextKey);
            }

            offcanvasTitleXRay.dataset.translateKey = 'offcanvas_edit_xray_question_title';
            offcanvasTitleXRay.textContent = getTranslation('offcanvas_edit_xray_question_title');
            submitButtonXRay.dataset.translateKey = 'save_changes_button';
            submitButtonXRay.textContent = getTranslation('save_changes_button');
        });
    });
    
    function getXRayQuestionMockData(id) {
        const mockQuestions = {
            "q1": { areaId: "1", questionTextKey: "xray_q_spirit_1" },
            "q2": { areaId: "1", questionTextKey: "xray_q_spirit_2" },
            "q3": { areaId: "2", questionTextKey: "xray_q_growth_1" },
            "q4": { areaId: "3", questionTextKey: "xray_q_social_1" },
            "q5": { areaId: "4", questionTextKey: "xray_q_family_1" }
        };
        return mockQuestions[id] || { areaId: "1", questionTextKey: "Unknown Question" };
    }

    // Form submission
    if (offcanvasFormXRay) {
        offcanvasFormXRay.addEventListener('submit', function (event) {
            event.preventDefault();
            console.log('💾 Form submitted');
            
            // Here you would normally handle the AJAX submission
            // For now, just close the offcanvas
            const offcanvasInstance = bootstrap.Offcanvas.getInstance(addEditOffcanvasElementXRay);
            if (offcanvasInstance) {
                offcanvasInstance.hide();
            }
        });
    }

    console.log('✅ X-Ray Life Module - Initialization completed successfully');
    
    /**
     * TESTING HELPER FUNCTIONS
     * These functions can be called from browser console for testing
     */
    window.xrayLifeTest = {
        // Test all area filters
        testAllAreas: function() {
            console.log('🧪 Testing all area filters...');
            lifeAreaLinks.forEach(link => {
                const areaId = link.dataset.areaId;
                console.log(`Testing area: ${areaId}`);
                filterQuestions(areaId, '');
            });
        },
        
        // Test search functionality
        testSearch: function(searchTerm) {
            console.log(`🧪 Testing search: "${searchTerm}"`);
            filterQuestions('all', searchTerm);
        },
        
        // Test combined filtering
        testCombined: function(areaId, searchTerm) {
            console.log(`🧪 Testing combined filter - Area: "${areaId}", Search: "${searchTerm}"`);
            filterQuestions(areaId, searchTerm);
        },
        
        // Get current filter state
        getState: function() {
            const activeAreaLink = document.querySelector('.life-areas-nav .nav-link.active');
            const currentArea = activeAreaLink ? activeAreaLink.dataset.areaId : 'none';
            const currentSearch = questionSearchInput ? questionSearchInput.value : '';
            
            console.log('Current state:', {
                area: currentArea,
                search: currentSearch,
                visibleQuestions: allQuestionItems.filter(item => item.style.display !== 'none').length,
                totalQuestions: allQuestionItems.length
            });
        }
    };
    
    console.log('🧪 Test functions available: xrayLifeTest.testAllAreas(), xrayLifeTest.testSearch("term"), xrayLifeTest.testCombined("1", "spirit"), xrayLifeTest.getState()');
});