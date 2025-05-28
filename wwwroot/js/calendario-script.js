// Calendar Module JavaScript
document.addEventListener('DOMContentLoaded', function() {
    
    // Configuration
    const config = {
        calendar: null,
        miniCalendar: null,
        startDatePicker: null,
        endDatePicker: null,
        usersSelect: null,
        currentEvent: null,
        isEditMode: false,
        baseUrl: '/api/calendario'
    };

    // State colors mapping
    const stateColors = {
        'Programada': '#7367f0',
        'Confirmada': '#28c76f',
        'EnProgreso': '#ff9f43',
        'Completada': '#00cfe8',
        'Cancelada': '#ea5455'
    };

    // Initialize main calendar
    function initializeCalendar() {
        const calendarEl = document.getElementById('calendar');
        
        config.calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            locale: localStorage.getItem('selectedLanguage') || 'es',
            height: 'auto',
            aspectRatio: 1.8,
            headerToolbar: false, // Disable built-in toolbar since we have custom controls
            buttonText: {
                today: 'Hoy',
                month: 'Mes',
                week: 'Semana',
                day: 'Día',
                list: 'Lista'
            },
            weekNumbers: false,
            dayMaxEventRows: 3,
            editable: true,
            selectable: true,
            selectMirror: true,
            nowIndicator: true,
            businessHours: {
                daysOfWeek: [1, 2, 3, 4, 5],
                startTime: '08:00',
                endTime: '18:00'
            },
            slotMinTime: '07:00:00',
            slotMaxTime: '21:00:00',
            slotDuration: '00:30:00',
            slotLabelInterval: '01:00',
            
            // Event sources
            events: fetchCalendarEvents,
            
            // Event handling
            select: handleDateSelect,
            eventClick: handleEventClick,
            eventDrop: handleEventDrop,
            eventResize: handleEventResize,
            
            // Event display
            eventClassNames: function(arg) {
                const status = arg.event.extendedProps.estadoSesion || 'Programada';
                return ['fc-event-' + status.toLowerCase()];
            },
            
            eventDidMount: function(info) {
                // Add tooltip
                if (info.event.extendedProps.descripcion) {
                    info.el.setAttribute('data-bs-toggle', 'tooltip');
                    info.el.setAttribute('data-bs-placement', 'top');
                    info.el.setAttribute('title', info.event.extendedProps.descripcion);
                    new bootstrap.Tooltip(info.el);
                }
                
                // Add fade-in animation
                info.el.style.animation = 'fadeIn 0.3s ease-out';
            },
            
            // Calendar ready
            loading: function(isLoading) {
                const spinner = document.querySelector('.calendar-spinner');
                if (spinner) {
                    spinner.classList.toggle('show', isLoading);
                }
            }
        });
        
        config.calendar.render();
    }

    // Initialize mini calendar
    function initializeMiniCalendar() {
        const miniCalendarEl = document.getElementById('calendarMini');
        
        // Solo inicializar si el elemento existe
        if (!miniCalendarEl) {
            console.log('Mini calendar element not found, skipping initialization');
            return;
        }
        
        config.miniCalendar = new FullCalendar.Calendar(miniCalendarEl, {
            initialView: 'dayGridMonth',
            locale: localStorage.getItem('selectedLanguage') || 'es',
            height: 250,
            headerToolbar: {
                left: 'prev',
                center: 'title',
                right: 'next'
            },
            dayMaxEventRows: false,
            events: [],
            
            dateClick: function(info) {
                config.calendar.gotoDate(info.date);
            }
        });
        
        config.miniCalendar.render();
    }

    // Fetch calendar events from API
    async function fetchCalendarEvents(fetchInfo, successCallback, failureCallback) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            
            const params = new URLSearchParams({
                start: fetchInfo.start.toISOString(),
                end: fetchInfo.end.toISOString()
            });

            const response = await fetch(`/api/sesionescalendario?${params}`, {
                headers: {
                    'RequestVerificationToken': token
                }
            });

            if (!response.ok) throw new Error('Error fetching events');

            const sessions = await response.json();
            console.log('Sessions loaded from API:', sessions);
            
            if (sessions && Array.isArray(sessions)) {
                const events = sessions.map(sesion => {
                    // Handle both users and clients properties
                    const users = sesion.users || [];
                    const clients = sesion.clients || [];
                    
                    return {
                        id: sesion.id,
                        title: sesion.title,
                        start: sesion.start,
                        end: sesion.end,
                        backgroundColor: stateColors[sesion.estadoSesion] || '#7367f0',
                        borderColor: stateColors[sesion.estadoSesion] || '#7367f0',
                        extendedProps: {
                            descripcion: sesion.descripcion,
                            ubicacion: sesion.ubicacion,
                            estadoSesion: sesion.estadoSesion,
                            users: users,
                            clients: clients,
                            coach: sesion.coach,
                            userCount: sesion.userCount || users.length,
                            clientCount: sesion.clientCount || clients.length
                        }
                    };
                });
                
                console.log('Events processed for calendar:', events);
                successCallback(events);
                
                // Filters are already set up in setupEventFilters()
            }
        } catch (error) {
            console.error('Error loading calendar events:', error);
            failureCallback(error);
            showToast('Error al cargar las sesiones', 'error');
        }
    }

    // Handle date selection
    function handleDateSelect(selectInfo) {
        openSessionModal(null, selectInfo.start, selectInfo.end);
        config.calendar.unselect();
    }

    // Handle event click
    function handleEventClick(clickInfo) {
        openSessionModal(clickInfo.event);
    }

    // Handle event drop (drag and drop)
    async function handleEventDrop(dropInfo) {
        try {
            const sessionId = dropInfo.event.id;
            const updateData = {
                titulo: dropInfo.event.title,
                descripcion: dropInfo.event.extendedProps.descripcion || '',
                fechaHoraInicio: dropInfo.event.start,
                fechaHoraFin: dropInfo.event.end,
                ubicacionOEnlace: dropInfo.event.extendedProps.ubicacionOEnlace || '',
                userIds: dropInfo.event.extendedProps.usuarios.map(u => u.userId)
            };

            const success = await updateSession(sessionId, updateData);
            
            if (!success) {
                dropInfo.revert();
            }
        } catch (error) {
            console.error('Error updating session:', error);
            dropInfo.revert();
            showToast('Error al actualizar la sesión', 'error');
        }
    }

    // Handle event resize
    async function handleEventResize(resizeInfo) {
        try {
            const sessionId = resizeInfo.event.id;
            const updateData = {
                titulo: resizeInfo.event.title,
                descripcion: resizeInfo.event.extendedProps.descripcion || '',
                fechaHoraInicio: resizeInfo.event.start,
                fechaHoraFin: resizeInfo.event.end,
                ubicacionOEnlace: resizeInfo.event.extendedProps.ubicacionOEnlace || '',
                userIds: resizeInfo.event.extendedProps.usuarios.map(u => u.userId)
            };

            const success = await updateSession(sessionId, updateData);
            
            if (!success) {
                resizeInfo.revert();
            }
        } catch (error) {
            console.error('Error updating session:', error);
            resizeInfo.revert();
            showToast('Error al actualizar la sesión', 'error');
        }
    }

    // Initialize date pickers
    function initializeDatePickers() {
        const startDateInput = document.getElementById('fechaHoraInicio');
        const endDateInput = document.getElementById('fechaHoraFin');
        const allDayCheck = document.getElementById('allDay');
        
        if (!startDateInput || !endDateInput) {
            console.warn('Date picker elements not found');
            return;
        }

        // Destroy existing instances to prevent duplicates
        if (config.startDatePicker) {
            config.startDatePicker.destroy();
            config.startDatePicker = null;
        }
        if (config.endDatePicker) {
            config.endDatePicker.destroy();
            config.endDatePicker = null;
        }

        try {
            config.startDatePicker = flatpickr(startDateInput, {
                enableTime: true,
                dateFormat: 'Y-m-d H:i',
                time_24hr: true,
                locale: localStorage.getItem('selectedLanguage') || 'es',
                onChange: function(selectedDates) {
                    if (selectedDates.length > 0 && config.endDatePicker) {
                        // Update end date minimum
                        config.endDatePicker.set('minDate', selectedDates[0]);
                        
                        // If end date is before start date, update it
                        const endDate = config.endDatePicker.selectedDates[0];
                        if (endDate && endDate < selectedDates[0]) {
                            const newEndDate = new Date(selectedDates[0]);
                            newEndDate.setHours(newEndDate.getHours() + 1);
                            config.endDatePicker.setDate(newEndDate);
                        }
                    }
                }
            });
            
            config.endDatePicker = flatpickr(endDateInput, {
                enableTime: true,
                dateFormat: 'Y-m-d H:i',
                time_24hr: true,
                locale: localStorage.getItem('selectedLanguage') || 'es'
            });
            
            // Handle all day checkbox
            if (allDayCheck) {
                allDayCheck.addEventListener('change', function() {
                    const checked = this.checked;
                    const enableTime = !checked;
                    
                    if (config.startDatePicker && config.endDatePicker) {
                        config.startDatePicker.set('enableTime', enableTime);
                        config.endDatePicker.set('enableTime', enableTime);
                        
                        if (checked) {
                            // Set times to 00:00 for start and 23:59 for end
                            const startDate = config.startDatePicker.selectedDates[0];
                            const endDate = config.endDatePicker.selectedDates[0];
                            
                            if (startDate) {
                                startDate.setHours(0, 0, 0, 0);
                                config.startDatePicker.setDate(startDate);
                            }
                            
                            if (endDate) {
                                endDate.setHours(23, 59, 59, 999);
                                config.endDatePicker.setDate(endDate);
                            }
                        }
                    }
                });
            }
        } catch (error) {
            console.error('Error initializing date pickers:', error);
        }
    }

    // Initialize users select
    function initializeClientsSelect() {
        console.log('Initializing Choices...');
        
        const selectElement = document.getElementById('userIds');
        
        if (!selectElement) {
            console.error('Select element #userIds not found');
            return;
        }
        
        // Destroy existing instance if any
        if (config.usersSelect) {
            config.usersSelect.destroy();
        }

        config.usersSelect = new Choices(selectElement, {
            removeItemButton: true,
            searchEnabled: true,
            searchPlaceholderValue: 'Buscar usuarios...',
            placeholder: true,
            placeholderValue: 'Seleccionar usuarios...',
            classNames: {
                containerOuter: 'choices',
                containerInner: 'choices__inner',
                input: 'choices__input',
                inputCloned: 'choices__input--cloned',
                list: 'choices__list',
                listItems: 'choices__list--multiple',
                listSingle: 'choices__list--single',
                listDropdown: 'choices__list--dropdown',
                item: 'choices__item',
                itemSelectable: 'choices__item--selectable',
                itemDisabled: 'choices__item--disabled',
                itemChoice: 'choices__item--choice',
                placeholder: 'choices__placeholder',
                group: 'choices__group',
                groupHeading: 'choices__heading',
                button: 'choices__button'
            }
        });

        console.log('Choices initialized');

        // Add change event listener for debugging
        selectElement.addEventListener('change', function() {
            console.log('Choices change event:', {
                currentValue: this.value,
                selectedOptions: Array.from(this.selectedOptions).map(opt => ({
                    value: opt.value,
                    text: opt.text
                }))
            });
        });
        
        loadUsers();
    }

    // Create session
    async function createSession(sessionData) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            const response = await fetch('/api/sesionescalendario', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify(sessionData)
            });

            if (!response.ok) {
                const error = await response.text();
                throw new Error(error || 'Error creating session');
            }

            const result = await response.json();
            return true;
        } catch (error) {
            console.error('Error creating session:', error);
            throw error;
        }
    }

    // Update session
    async function updateSession(sessionId, sessionData) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            const response = await fetch(`/api/sesionescalendario/${sessionId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify(sessionData)
            });

            if (!response.ok) {
                const error = await response.text();
                throw new Error(error || 'Error updating session');
            }

            return true;
        } catch (error) {
            console.error('Error updating session:', error);
            throw error;
        }
    }

    // Delete session
    async function deleteSession() {
        if (!config.currentEvent || !confirm('¿Está seguro de eliminar esta sesión?')) {
            return;
        }

        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            const response = await fetch(`/api/sesionescalendario/${config.currentEvent.id}`, {
                method: 'DELETE',
                headers: {
                    'RequestVerificationToken': token
                }
            });

            if (!response.ok) throw new Error('Error deleting session');

            config.calendar.getEventById(config.currentEvent.id)?.remove();
            const offcanvasEl = document.getElementById('sessionOffcanvas');
            const offcanvas = bootstrap.Offcanvas.getInstance(offcanvasEl);
            offcanvas.hide();
            
            showToast('Sesión eliminada exitosamente', 'success');
        } catch (error) {
            console.error('Error deleting session:', error);
            showToast('Error al eliminar la sesión', 'error');
        }
    }

    // Load users from API
    async function loadUsers() {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            const response = await fetch(`/api/users/invitable`, {
                headers: {
                    'RequestVerificationToken': token
                }
            });

            if (!response.ok) throw new Error('Error loading users');

            const users = await response.json();
            
            if (users && Array.isArray(users)) {
                console.log('Loading users into Choices:', users);
                
                const choices = users.map(user => ({
                    value: user.id.toString(),
                    label: user.fullName || user.userName,
                    selected: false
                }));
                
                config.usersSelect.setChoices(choices, 'value', 'label', true);
            }
        } catch (error) {
            console.error('Error loading users:', error);
            showToast('Error al cargar usuarios', 'error');
        }
    }

    // Save session
    async function saveSession() {
        const form = document.getElementById('sessionForm');
        
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }
        
        const selectedUsers = config.usersSelect ? config.usersSelect.getValue(true) : [];
        if (selectedUsers.length === 0) {
            showToast('Debe seleccionar al menos un usuario', 'warning');
            return;
        }
        
        const sessionData = {
            titulo: document.getElementById('titulo').value,
            descripcion: document.getElementById('descripcion').value,
            fechaHoraInicio: config.startDatePicker.selectedDates[0].toISOString(),
            fechaHoraFin: config.endDatePicker.selectedDates[0].toISOString(),
            ubicacion: document.getElementById('ubicacion').value,
            estadoSesion: document.getElementById('estadoSesion').value,
            tipoSesion: document.getElementById('tipoSesion').value,
            coachId: parseInt(document.getElementById('coachId').value),
            userIds: selectedUsers.map(id => parseInt(id))
        };
        
        try {
            let success = false;
            
            if (config.isEditMode) {
                const sessionId = config.currentEvent.id;
                success = await updateSession(sessionId, sessionData);
            } else {
                success = await createSession(sessionData);
            }
            
            if (success) {
                bootstrap.Offcanvas.getInstance(document.getElementById('sessionOffcanvas')).hide();
                config.calendar.refetchEvents();
                showToast(config.isEditMode ? 'Sesión actualizada exitosamente' : 'Sesión creada exitosamente', 'success');
            }
        } catch (error) {
            console.error('Error saving session:', error);
            showToast('Error al guardar la sesión', 'error');
        }
    }


    // Load session notes (placeholder for future implementation)
    async function loadSessionNotes(sessionId) {
        // TODO: Implement when notes API is ready
        console.log('Loading notes for session:', sessionId);
    }

    // Save session notes (placeholder for future implementation)
    async function saveSessionNotes(sessionId) {
        // TODO: Implement when notes API is ready
        const notesData = {
            keyPoints: document.getElementById('keyPoints').value,
            waysToDevelop: document.getElementById('waysToDevelop').value,
            assignments: document.getElementById('assignments').value,
            challenges: document.getElementById('challenges').value,
            feedback: document.getElementById('feedback').value,
            nextSession: document.getElementById('nextSession').value
        };
        
        console.log('Saving notes for session:', sessionId, notesData);
    }

    // Update event filters
    function updateEventFilters() {
        const filterCheckboxes = document.querySelectorAll('.filter-checkboxes input[type="checkbox"]');
        
        filterCheckboxes.forEach(checkbox => {
            checkbox.addEventListener('change', function() {
                const activeFilters = Array.from(filterCheckboxes)
                    .filter(cb => cb.checked)
                    .map(cb => cb.value);
                
                // Filter events
                const events = config.calendar.getEvents();
                events.forEach(event => {
                    const shouldShow = activeFilters.includes(event.extendedProps.estadoSesion);
                    event.setProp('display', shouldShow ? 'auto' : 'none');
                });
            });
        });
    }

    // Show toast notification
    function showToast(message, type = 'info') {
        // Create toast element
        const toastHtml = `
            <div class="toast align-items-center text-white bg-${type === 'success' ? 'success' : type === 'error' ? 'danger' : type === 'warning' ? 'warning' : 'info'}" role="alert">
                <div class="d-flex">
                    <div class="toast-body">
                        ${message}
                    </div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
                </div>
            </div>
        `;
        
        // Add to container
        let container = document.getElementById('toastContainer');
        if (!container) {
            container = document.createElement('div');
            container.id = 'toastContainer';
            container.className = 'toast-container position-fixed top-0 end-0 p-3';
            container.style.zIndex = '1080';
            document.body.appendChild(container);
        }
        
        container.insertAdjacentHTML('beforeend', toastHtml);
        
        // Show toast
        const toastEl = container.lastElementChild;
        const toast = new bootstrap.Toast(toastEl, { delay: 3000 });
        toast.show();
        
        // Remove after hidden
        toastEl.addEventListener('hidden.bs.toast', () => toastEl.remove());
    }

    // Load current user and coaches
    async function loadCoaches() {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            
            // First, get current user
            const currentUserResponse = await fetch('/api/users/current', {
                headers: {
                    'RequestVerificationToken': token
                }
            });
            
            if (currentUserResponse.ok) {
                const currentUser = await currentUserResponse.json();
                
                // Set current user as default coach
                const coachSelect = document.getElementById('coachId');
                if (coachSelect) {
                    // Clear existing options
                    coachSelect.innerHTML = '';
                    
                    // Add current user as the first and selected option
                    const option = document.createElement('option');
                    option.value = currentUser.id;
                    option.textContent = currentUser.fullName || currentUser.userName;
                    option.selected = true;
                    coachSelect.appendChild(option);
                }
            }
            
            // Then load all active users as potential coaches
            const usersResponse = await fetch('/api/users/active', {
                headers: {
                    'RequestVerificationToken': token
                }
            });
            
            if (usersResponse.ok) {
                const users = await usersResponse.json();
                const coachSelect = document.getElementById('coachId');
                
                if (coachSelect && users) {
                    // Add other users as options
                    users.forEach(user => {
                        // Skip if it's the current user (already added)
                        if (coachSelect.querySelector(`option[value="${user.id}"]`)) {
                            return;
                        }
                        
                        const option = document.createElement('option');
                        option.value = user.id;
                        option.textContent = user.fullName || user.userName;
                        coachSelect.appendChild(option);
                    });
                }
            }
        } catch (error) {
            console.error('Error loading coaches:', error);
        }
    }

    // Open session modal
    function openSessionModal(event = null) {
        config.isEditMode = !!event;
        config.currentEvent = event;
        
        // Reset form
        const form = document.getElementById('sessionForm');
        form.reset();
        
        // Update offcanvas title
        const offcanvasTitle = document.querySelector('#sessionOffcanvas .offcanvas-title');
        if (offcanvasTitle) {
            offcanvasTitle.textContent = config.isEditMode ? 'Editar Sesión' : 'Agregar Sesión';
        }
        
        // Show/hide delete button
        const deleteBtn = document.getElementById('btnDeleteSession');
        if (deleteBtn) {
            deleteBtn.classList.toggle('d-none', !config.isEditMode);
        }
        
        // Fill form if editing
        if (config.isEditMode && event) {
            document.getElementById('titulo').value = event.title || '';
            document.getElementById('descripcion').value = event.extendedProps.descripcion || '';
            document.getElementById('ubicacion').value = event.extendedProps.ubicacionOEnlace || '';
            document.getElementById('estadoSesion').value = event.extendedProps.estadoSesion || 'Programada';
            document.getElementById('tipoSesion').value = event.extendedProps.tipoSesion || 'Individual';
            
            // Set coach if available
            const coachId = event.extendedProps.coach?.id;
            if (coachId) {
                const coachSelect = document.getElementById('coachId');
                if (coachSelect) {
                    coachSelect.value = coachId;
                }
            }
            
            // Set dates
            if (config.startDatePicker && event.start) {
                config.startDatePicker.setDate(event.start);
            }
            if (config.endDatePicker && event.end) {
                config.endDatePicker.setDate(event.end);
            }
            
            // Set all day
            const allDayCheck = document.getElementById('allDay');
            if (allDayCheck) {
                allDayCheck.checked = event.allDay || false;
            }
            
            // Set selected users if editing
            if (event.extendedProps.users && config.usersSelect) {
                const userIds = event.extendedProps.users.map(u => u.id.toString());
                config.usersSelect.setChoiceByValue(userIds);
            }
        }
        
        // Show offcanvas
        const offcanvasEl = document.getElementById('sessionOffcanvas');
        const offcanvas = new bootstrap.Offcanvas(offcanvasEl);
        offcanvas.show();
        
        // Initialize date pickers when offcanvas is shown
        offcanvasEl.addEventListener('shown.bs.offcanvas', function() {
            initializeDatePickers();
            initializeClientsSelect();
            loadCoaches();
        }, { once: true });
    }

    // Setup event filters
    function setupEventFilters() {
        const filterCheckboxes = document.querySelectorAll('.form-check-input[id^="filter"]');
        const filterAll = document.getElementById('filterAll');
        
        if (filterAll) {
            filterAll.addEventListener('change', function() {
                filterCheckboxes.forEach(checkbox => {
                    if (checkbox.id !== 'filterAll') {
                        checkbox.checked = this.checked;
                    }
                });
                applyEventFilters();
            });
        }
        
        filterCheckboxes.forEach(checkbox => {
            if (checkbox.id !== 'filterAll') {
                checkbox.addEventListener('change', function() {
                    // Update "Ver Todos" checkbox
                    const allChecked = Array.from(filterCheckboxes)
                        .filter(cb => cb.id !== 'filterAll')
                        .every(cb => cb.checked);
                    if (filterAll) {
                        filterAll.checked = allChecked;
                    }
                    applyEventFilters();
                });
            }
        });
    }
    
    // Apply event filters
    function applyEventFilters() {
        const activeFilters = [];
        const filterCheckboxes = document.querySelectorAll('.form-check-input[id^="filter"]:checked');
        
        filterCheckboxes.forEach(checkbox => {
            if (checkbox.id !== 'filterAll' && checkbox.checked) {
                activeFilters.push(checkbox.value);
            }
        });
        
        // Remove all events and re-add filtered ones
        config.calendar.refetchEvents();
    }

    // Setup custom navigation controls
    function setupCalendarControls() {
        // Navigation buttons
        const btnPrev = document.getElementById('btnPrev');
        const btnNext = document.getElementById('btnNext');
        const btnRefresh = document.getElementById('btnRefresh');
        
        if (btnPrev) {
            btnPrev.addEventListener('click', () => {
                config.calendar.prev();
                updateCalendarTitle();
            });
        }
        
        if (btnNext) {
            btnNext.addEventListener('click', () => {
                config.calendar.next();
                updateCalendarTitle();
            });
        }
        
        if (btnRefresh) {
            btnRefresh.addEventListener('click', () => {
                config.calendar.refetchEvents();
            });
        }
        
        // View buttons
        const viewButtons = document.querySelectorAll('[data-view]');
        viewButtons.forEach(button => {
            button.addEventListener('click', function() {
                const view = this.getAttribute('data-view');
                config.calendar.changeView(view);
                
                // Update active state
                viewButtons.forEach(btn => btn.classList.remove('active'));
                this.classList.add('active');
                
                updateCalendarTitle();
            });
        });
    }
    
    // Update calendar title
    function updateCalendarTitle() {
        const calendarTitle = document.getElementById('calendarTitle');
        if (calendarTitle && config.calendar) {
            calendarTitle.textContent = config.calendar.view.title;
        }
    }

    // Event listeners
    document.getElementById('btnAddSession').addEventListener('click', () => openSessionModal());
    document.getElementById('btnSaveSession').addEventListener('click', saveSession);
    document.getElementById('btnDeleteSession').addEventListener('click', deleteSession);

    // Initialize everything
    initializeCalendar();
    setupCalendarControls();
    setupEventFilters();
    updateCalendarTitle();
    
    // Solo inicializar el mini calendario si está habilitado en la configuración
    if (document.getElementById('calendarMini')) {
        initializeMiniCalendar();
    }
    
    // Don't initialize date pickers here - they will be initialized when offcanvas opens
    // initializeDatePickers();
    // initializeClientsSelect();
    
    // Apply translations
    if (typeof applyTranslations === 'function') {
        applyTranslations();
    }
});