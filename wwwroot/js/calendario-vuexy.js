// Vuexy Calendar Implementation
document.addEventListener('DOMContentLoaded', function() {
    
    // Configuration
    const config = {
        calendar: null,
        miniCalendar: null,
        currentEvent: null,
        isEditMode: false,
        baseUrl: '/api/sesionescalendario',
        startDatePicker: null,
        endDatePicker: null,
        clientsSelect: null
    };

    // State colors mapping (Vuexy style)
    const stateColors = {
        'Programada': '#7367f0',
        'Confirmada': '#28c76f',
        'EnProgreso': '#ff9f43',
        'Completada': '#00cfe8',
        'Cancelada': '#ea5455'
    };
    
    // Get event color based on status
    function getEventColor(status) {
        return stateColors[status] || stateColors['Programada'];
    }
    
    // Get current locale
    function getCurrentLocale() {
        return document.documentElement.lang || 'es';
    }
    
    // Show toast notification
    function showToast(message, type = 'success') {
        const toastEl = document.getElementById('calendarToast');
        const toastIcon = document.getElementById('toastIcon');
        const toastMessage = document.getElementById('toastMessage');
        
        if (type === 'success') {
            toastIcon.className = 'fas fa-check-circle text-success me-2';
            toastEl.classList.remove('bg-danger');
            toastEl.classList.add('bg-white');
        } else {
            toastIcon.className = 'fas fa-times-circle text-danger me-2';
            toastEl.classList.remove('bg-white');
            toastEl.classList.add('bg-danger', 'text-white');
        }
        
        toastMessage.textContent = message;
        
        const toast = new bootstrap.Toast(toastEl, {
            autohide: true,
            delay: 3000
        });
        toast.show();
    }
    
    // Format participant for Select2 dropdown
    function formatParticipant(participant) {
        if (!participant.id) {
            return participant.text;
        }
        
        const avatar = participant.element.getAttribute('data-avatar') || '/img/default-avatar.png';
        const $participant = $(
            '<div class="d-flex align-items-center">' +
                '<img src="' + avatar + '" class="me-2" style="width: 32px; height: 32px; border-radius: 50%; object-fit: cover;" />' +
                '<span>' + participant.text + '</span>' +
            '</div>'
        );
        
        return $participant;
    }
    
    // Format selected participant
    function formatParticipantSelection(participant) {
        if (!participant.id) {
            return participant.text;
        }
        
        const avatar = participant.element.getAttribute('data-avatar') || '/img/default-avatar.png';
        const $selection = $(
            '<span class="d-flex align-items-center">' +
                '<img src="' + avatar + '" class="me-1" style="width: 20px; height: 20px; border-radius: 50%; object-fit: cover;" />' +
                '<span>' + participant.text + '</span>' +
            '</span>'
        );
        
        return $selection;
    }

    // Initialize main calendar
    function initializeCalendar() {
        const calendarEl = document.getElementById('calendar');
        
        // Calendar configuration
        const calendarOptions = {
            initialView: 'dayGridMonth',
            locale: 'es',
            headerToolbar: false, // We use custom navigation
            height: 'auto',
            contentHeight: 'auto',
            editable: true,
            droppable: true,
            selectable: true,
            selectMirror: true,
            dayMaxEvents: 3,
            weekends: true,
            navLinks: true,
            businessHours: {
                daysOfWeek: [1, 2, 3, 4, 5],
                startTime: '08:00',
                endTime: '18:00'
            },
            select: handleDateSelect,
            eventClick: handleEventClick,
            eventDrop: handleEventDrop,
            eventResize: handleEventResize,
            events: fetchCalendarEvents,
            eventClassNames: function(arg) {
                const status = arg.event.extendedProps.estadoSesion || 'Programada';
                return [`fc-event-${status}`];
            },
            eventDidMount: function(info) {
                // Add data attribute for filtering
                info.el.setAttribute('data-status', info.event.extendedProps.estadoSesion || 'Programada');
            },
            eventsSet: function() {
                // Apply filters after events are rendered
                setTimeout(applyFilters, 100);
            }
        };
        
        try {
            config.calendar = new FullCalendar.Calendar(calendarEl, calendarOptions);
            console.log('Calendar instance created');
            
            // Force calendar to render
            config.calendar.render();
            console.log('Calendar rendered');
            
            // Setup custom navigation
            setupCustomNavigation();
            
            // Update title
            updateCalendarTitle();
        } catch (error) {
            console.error('Error creating calendar:', error);
        }
    }

    // Initialize mini calendar
    function initializeMiniCalendar() {
        const miniCalendarEl = document.getElementById('calendarMini');
        
        const miniCalendarOptions = {
            initialView: 'dayGridMonth',
            locale: 'es',
            headerToolbar: {
                start: 'prev',
                center: 'title',
                end: 'next'
            },
            height: 'auto',
            aspectRatio: 1,
            dayHeaderFormat: { weekday: 'narrow' },
            weekNumbers: false,
            selectable: true,
            fixedWeekCount: false,
            showNonCurrentDates: true,
            dateClick: function(info) {
                // Navigate main calendar to clicked date
                config.calendar.gotoDate(info.date);
                
                // Remove previous selection
                document.querySelectorAll('#calendarMini .fc-daygrid-day').forEach(day => {
                    day.classList.remove('selected');
                });
                
                // Add selection to clicked date
                info.dayEl.classList.add('selected');
            },
            dayCellDidMount: function(info) {
                // Remove default FullCalendar day numbers to customize
                const dayNumberEl = info.el.querySelector('.fc-daygrid-day-number');
                if (dayNumberEl) {
                    dayNumberEl.textContent = info.date.getDate();
                }
            }
        };
        
        config.miniCalendar = new FullCalendar.Calendar(miniCalendarEl, miniCalendarOptions);
        config.miniCalendar.render();
        
        // Sync mini calendar with main calendar navigation
        // Commented out since mini calendar is hidden
        // if (config.calendar) {
        //     config.calendar.on('datesSet', function(info) {
        //         config.miniCalendar.gotoDate(info.view.currentStart);
        //     });
        // }
    }

    // Setup custom navigation buttons
    function setupCustomNavigation() {
        // Previous button
        document.getElementById('btnPrev').addEventListener('click', function() {
            config.calendar.prev();
            updateCalendarTitle();
        });

        // Next button
        document.getElementById('btnNext').addEventListener('click', function() {
            config.calendar.next();
            updateCalendarTitle();
        });

        // View switcher buttons
        document.querySelectorAll('[data-view]').forEach(function(btn) {
            btn.addEventListener('click', function() {
                // Update active state
                document.querySelectorAll('[data-view]').forEach(b => b.classList.remove('active'));
                this.classList.add('active');
                
                // Change calendar view
                config.calendar.changeView(this.getAttribute('data-view'));
            });
        });

        // Initial title update
        updateCalendarTitle();
        
        // Refresh button
        const btnRefresh = document.getElementById('btnRefresh');
        if (btnRefresh) {
            btnRefresh.addEventListener('click', function() {
                console.log('Refreshing calendar events...');
                config.calendar.refetchEvents();
            });
        }
    }

    // Update calendar title
    function updateCalendarTitle() {
        const date = config.calendar.getDate();
        const view = config.calendar.view;
        let title = '';
        
        if (view.type === 'dayGridMonth' || view.type === 'listMonth') {
            title = date.toLocaleDateString(getCurrentLocale(), { month: 'long', year: 'numeric' });
        } else if (view.type === 'timeGridWeek') {
            const start = view.currentStart;
            const end = view.currentEnd;
            title = `${start.toLocaleDateString(getCurrentLocale(), { month: 'short', day: 'numeric' })} - ${end.toLocaleDateString(getCurrentLocale(), { month: 'short', day: 'numeric', year: 'numeric' })}`;
        } else if (view.type === 'timeGridDay') {
            title = date.toLocaleDateString(getCurrentLocale(), { weekday: 'long', month: 'long', day: 'numeric', year: 'numeric' });
        }
        
        document.getElementById('calendarTitle').textContent = title;
    }

    // Fetch calendar events from API
    async function fetchCalendarEvents(fetchInfo, successCallback, failureCallback) {
        try {
            const response = await fetch(`${config.baseUrl}?start=${fetchInfo.startStr}&end=${fetchInfo.endStr}`);
            if (!response.ok) throw new Error('Failed to fetch events');
            
            const data = await response.json();
            console.log('Events received from API:', data);
            
            // Transform data to FullCalendar format
            const events = data.map(session => ({
                id: session.id,
                title: session.title || session.titulo,
                start: session.start || session.fechaHoraInicio,
                end: session.end || session.fechaHoraFin,
                backgroundColor: getEventColor(session.estadoSesion),
                borderColor: getEventColor(session.estadoSesion),
                extendedProps: {
                    estadoSesion: session.estadoSesion,
                    descripcion: session.descripcion,
                    ubicacion: session.ubicacion,
                    coach: session.coach,
                    clients: session.clients,
                    tipoSesion: session.tipoSesion
                }
            }));
            
            console.log('Events transformed for calendar:', events);
            successCallback(events);
        } catch (error) {
            console.error('Error fetching events:', error);
            failureCallback(error);
        }
    }

    // Handle date selection
    function handleDateSelect(selectionInfo) {
        resetForm();
        config.isEditMode = false;
        
        // Set dates in form
        if (config.startDatePicker && config.endDatePicker) {
            config.startDatePicker.setDate(selectionInfo.start);
            config.endDatePicker.setDate(selectionInfo.end || selectionInfo.start);
        } else {
            // If date pickers not initialized yet, set values directly
            document.getElementById('fechaHoraInicio').value = selectionInfo.start.toISOString().slice(0, 16);
            document.getElementById('fechaHoraFin').value = (selectionInfo.end || selectionInfo.start).toISOString().slice(0, 16);
        }
        
        // Show offcanvas
        showOffcanvas('Add Session');
        
        // Clear selection
        config.calendar.unselect();
    }

    // Handle event click
    function handleEventClick(clickInfo) {
        config.currentEvent = clickInfo.event;
        config.isEditMode = true;
        
        // Populate form with event data
        document.getElementById('titulo').value = clickInfo.event.title;
        document.getElementById('estadoSesion').value = clickInfo.event.extendedProps.estadoSesion || 'Programada';
        
        config.startDatePicker.setDate(clickInfo.event.start);
        config.endDatePicker.setDate(clickInfo.event.end || clickInfo.event.start);
        
        document.getElementById('descripcion').value = clickInfo.event.extendedProps.descripcion || '';
        document.getElementById('ubicacion').value = clickInfo.event.extendedProps.ubicacion || '';
        
        // Set coach
        const coachId = clickInfo.event.extendedProps.coach?.id || clickInfo.event.extendedProps.coachId || '';
        document.getElementById('coachId').value = coachId;
        
        document.getElementById('tipoSesion').value = clickInfo.event.extendedProps.tipoSesion || 'Individual';
        
        // Set clients
        const clientIds = clickInfo.event.extendedProps.clients?.map(c => c.id) || clickInfo.event.extendedProps.clientIds || [];
        if (clientIds.length > 0) {
            $(config.clientsSelect).val(clientIds).trigger('change');
        } else {
            $(config.clientsSelect).val(null).trigger('change');
        }
        
        // Show delete button for edit mode
        document.getElementById('btnDeleteSession').classList.remove('d-none');
        
        // Show offcanvas
        showOffcanvas('Update Session');
    }

    // Handle event drop
    async function handleEventDrop(dropInfo) {
        try {
            const response = await fetch(`${config.baseUrl}/${dropInfo.event.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'X-CSRF-TOKEN': document.querySelector('input[name="__RequestVerificationToken"]').value
                },
                body: JSON.stringify({
                    id: dropInfo.event.id,
                    fechaHoraInicio: dropInfo.event.start.toISOString(),
                    fechaHoraFin: dropInfo.event.end ? dropInfo.event.end.toISOString() : dropInfo.event.start.toISOString()
                })
            });
            
            if (!response.ok) {
                dropInfo.revert();
                showToast('Error moving event', 'error');
            } else {
                showToast('Event moved successfully', 'success');
            }
        } catch (error) {
            dropInfo.revert();
            showToast('Error moving event', 'error');
        }
    }

    // Handle event resize
    async function handleEventResize(resizeInfo) {
        try {
            const response = await fetch(`${config.baseUrl}/${resizeInfo.event.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    'X-CSRF-TOKEN': document.querySelector('input[name="__RequestVerificationToken"]').value
                },
                body: JSON.stringify({
                    id: resizeInfo.event.id,
                    fechaHoraInicio: resizeInfo.event.start.toISOString(),
                    fechaHoraFin: resizeInfo.event.end.toISOString()
                })
            });
            
            if (!response.ok) {
                resizeInfo.revert();
                showToast('Error resizing event', 'error');
            } else {
                showToast('Event updated successfully', 'success');
            }
        } catch (error) {
            resizeInfo.revert();
            showToast('Error resizing event', 'error');
        }
    }

    // Initialize form components
    function initializeFormComponents() {
        // Initialize date pickers with Flatpickr
        const startInput = document.getElementById('fechaHoraInicio');
        const endInput = document.getElementById('fechaHoraFin');
        
        if (startInput) {
            config.startDatePicker = flatpickr(startInput, {
                enableTime: true,
                dateFormat: 'Y-m-d h:i K',
                time_24hr: false,
                locale: getCurrentLocale() === 'es' ? 'es' : 'en'
            });
        }
        
        if (endInput) {
            config.endDatePicker = flatpickr(endInput, {
                enableTime: true,
                dateFormat: 'Y-m-d h:i K',
                time_24hr: false,
                locale: getCurrentLocale() === 'es' ? 'es' : 'en'
            });
        }
        
        // Initialize Select2 for clients (will be configured when loading clients)
        config.clientsSelect = $('#clientIds');
        
        // Load initial data
        loadCoaches();
        loadClients();
    }

    // Load coaches (from Users table)
    async function loadCoaches() {
        try {
            const response = await fetch('/api/users/active');
            const users = await response.json();
            
            const select = document.getElementById('coachId');
            select.innerHTML = '<option value="">Select Coach</option>';
            
            users.forEach(user => {
                const option = document.createElement('option');
                option.value = user.id;
                option.textContent = user.fullName || user.userName;
                option.setAttribute('data-avatar', user.avatarUrl || '/img/default-avatar.png');
                select.appendChild(option);
            });
        } catch (error) {
            console.error('Error loading coaches:', error);
        }
    }

    // Load clients (from Customers table)
    async function loadClients() {
        try {
            const response = await fetch('/api/customers/active');
            const clients = await response.json();
            
            const select = document.getElementById('clientIds');
            select.innerHTML = '';
            
            // Destroy Select2 only if it exists
            if ($(select).hasClass('select2-hidden-accessible')) {
                $(select).select2('destroy');
            }
            
            clients.forEach(client => {
                const option = document.createElement('option');
                option.value = client.id;
                option.textContent = `${client.firstName} ${client.lastName}`;
                option.setAttribute('data-avatar', client.profileImageUrl || '/img/default-avatar.png');
                select.appendChild(option);
            });
            
            // Initialize Select2 with custom template
            $(select).select2({
                theme: 'bootstrap-5',
                placeholder: 'Select participants',
                allowClear: true,
                dropdownParent: $('#sessionOffcanvas'),
                templateResult: formatParticipant,
                templateSelection: formatParticipantSelection
            });
            
            // Store reference for later use
            config.clientsSelect = $(select);
        } catch (error) {
            console.error('Error loading clients:', error);
        }
    }

    // Event handlers
    const btnAddSession = document.getElementById('btnAddSession');
    if (btnAddSession) {
        btnAddSession.addEventListener('click', function() {
            resetForm();
            config.isEditMode = false;
            const deleteBtn = document.getElementById('btnDeleteSession');
            if (deleteBtn) deleteBtn.classList.add('d-none');
            showOffcanvas('Add Session');
        });
    }

    const btnSaveSession = document.getElementById('btnSaveSession');
    if (btnSaveSession) {
        btnSaveSession.addEventListener('click', saveSession);
    }
    
    const btnDeleteSession = document.getElementById('btnDeleteSession');
    if (btnDeleteSession) {
        btnDeleteSession.addEventListener('click', deleteSession);
    }

    // Status select will be handled by the form select element now

    // All day toggle
    document.getElementById('allDay').addEventListener('change', function() {
        if (this.checked) {
            config.startDatePicker.config.enableTime = false;
            config.endDatePicker.config.enableTime = false;
        } else {
            config.startDatePicker.config.enableTime = true;
            config.endDatePicker.config.enableTime = true;
        }
    });

    // Filter checkboxes
    document.querySelectorAll('.form-check-input[id^="filter"]').forEach(function(checkbox) {
        checkbox.addEventListener('change', function() {
            // Handle "View All" checkbox
            if (this.id === 'filterAll') {
                const isChecked = this.checked;
                document.querySelectorAll('.form-check-input[id^="filter"]:not(#filterAll)').forEach(cb => {
                    cb.checked = isChecked;
                });
            } else {
                // If unchecking a filter, uncheck "View All"
                if (!this.checked) {
                    document.getElementById('filterAll').checked = false;
                }
            }
            
            // Apply filters after a short delay to let DOM update
            setTimeout(applyFilters, 100);
        });
    });

    // Apply filters
    function applyFilters() {
        const filterAll = document.getElementById('filterAll');
        const filters = [];
        
        if (filterAll && filterAll.checked) {
            // Show all events
            document.querySelectorAll('.fc-event').forEach(event => {
                event.style.display = '';
            });
            return;
        }
        
        // Get active filters
        document.querySelectorAll('.form-check-input[id^="filter"]:checked').forEach(checkbox => {
            if (checkbox.value !== 'all') {
                filters.push(checkbox.value);
            }
        });
        
        // Apply filters to events
        document.querySelectorAll('.fc-event').forEach(event => {
            const status = event.getAttribute('data-status');
            if (filters.length === 0 || filters.includes(status)) {
                event.style.display = '';
            } else {
                event.style.display = 'none';
            }
        });
    }

    // Helper functions
    function showOffcanvas(title) {
        document.getElementById('offcanvasTitle').textContent = title;
        document.getElementById('btnSaveSession').textContent = config.isEditMode ? 'Update' : 'Add Event';
        
        const offcanvas = new bootstrap.Offcanvas(document.getElementById('sessionOffcanvas'));
        offcanvas.show();
    }

    function resetForm() {
        document.getElementById('sessionForm').reset();
        // Reset status select to default
        document.getElementById('estadoSesion').value = 'Programada';
        // Reset Select2
        if (config.clientsSelect && config.clientsSelect.length) {
            config.clientsSelect.val(null).trigger('change');
        }
    }

    // Status is now handled by select element, no need for updateSelectedStateLabel

    function getCurrentLocale() {
        return localStorage.getItem('selectedLanguage') || 'en';
    }

    function showToast(message, type = 'success') {
        // Implement toast notification (you can use your existing toast system)
        console.log(`${type}: ${message}`);
    }

    async function saveSession() {
        const form = document.getElementById('sessionForm');
        
        // Basic validation
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }
        
        const formData = new FormData(form);
        
        // Check required fields
        if (!formData.get('titulo')) {
            showToast('Por favor ingrese un título', 'error');
            return;
        }
        
        if (!config.startDatePicker.selectedDates.length || !config.endDatePicker.selectedDates.length) {
            showToast('Por favor seleccione las fechas', 'error');
            return;
        }
        
        const data = {
            titulo: formData.get('titulo'),
            estadoSesion: formData.get('estadoSesion'),
            fechaHoraInicio: config.startDatePicker.selectedDates[0].toISOString(),
            fechaHoraFin: config.endDatePicker.selectedDates[0].toISOString(),
            descripcion: formData.get('descripcion'),
            ubicacion: formData.get('ubicacion'),
            coachId: formData.get('coachId') ? parseInt(formData.get('coachId')) : null,
            clientIds: $(config.clientsSelect).val() ? $(config.clientsSelect).val().map(id => parseInt(id)) : [],
            tipoSesion: formData.get('tipoSesion')
        };
        
        console.log('Saving session with data:', data);
        
        try {
            const url = config.isEditMode ? `${config.baseUrl}/${config.currentEvent.id}` : config.baseUrl;
            const method = config.isEditMode ? 'PUT' : 'POST';
            
            if (config.isEditMode) {
                data.id = config.currentEvent.id;
            }
            
            const response = await fetch(url, {
                method: method,
                headers: {
                    'Content-Type': 'application/json',
                    'X-CSRF-TOKEN': document.querySelector('input[name="__RequestVerificationToken"]').value
                },
                body: JSON.stringify(data)
            });
            
            if (response.ok) {
                console.log('Session saved successfully');
                config.calendar.refetchEvents();
                const offcanvasEl = document.getElementById('sessionOffcanvas');
                const offcanvas = bootstrap.Offcanvas.getInstance(offcanvasEl);
                if (offcanvas) {
                    offcanvas.hide();
                }
                showToast(config.isEditMode ? 'Session updated successfully' : 'Session created successfully');
                // Reset form after save
                resetForm();
            } else {
                const errorText = await response.text();
                console.error('Error response:', errorText);
                showToast('Error saving session: ' + response.status, 'error');
            }
        } catch (error) {
            showToast('Error saving session', 'error');
        }
    }

    async function deleteSession() {
        if (!confirm('Are you sure you want to delete this session?')) return;
        
        try {
            const response = await fetch(`${config.baseUrl}/${config.currentEvent.id}`, {
                method: 'DELETE',
                headers: {
                    'X-CSRF-TOKEN': document.querySelector('input[name="__RequestVerificationToken"]').value
                }
            });
            
            if (response.ok) {
                config.calendar.refetchEvents();
                bootstrap.Offcanvas.getInstance(document.getElementById('sessionOffcanvas')).hide();
                showToast('Session deleted successfully');
            } else {
                showToast('Error deleting session', 'error');
            }
        } catch (error) {
            showToast('Error deleting session', 'error');
        }
    }

    // Initialize everything
    try {
        // Initialize form components first
        console.log('Initializing form components...');
        initializeFormComponents();
        console.log('Form components initialized');
        
        // Then initialize calendar
        console.log('Initializing calendar...');
        initializeCalendar();
        console.log('Calendar initialized');
        
        // Skip mini calendar since it's hidden
        // console.log('Initializing mini calendar...');
        // initializeMiniCalendar();
        // console.log('Mini calendar initialized');
    } catch (error) {
        console.error('Error during initialization:', error);
    }
});