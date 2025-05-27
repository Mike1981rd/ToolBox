// Calendar Module JavaScript
document.addEventListener('DOMContentLoaded', function() {
    
    // Configuration
    const config = {
        calendar: null,
        miniCalendar: null,
        startDatePicker: null,
        endDatePicker: null,
        clientsSelect: null,
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
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
            },
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
                fechaInicio: fetchInfo.start.toISOString(),
                fechaFin: fetchInfo.end.toISOString()
            });

            const response = await fetch(`${config.baseUrl}/sesiones?${params}`, {
                headers: {
                    'RequestVerificationToken': token
                }
            });

            if (!response.ok) throw new Error('Error fetching events');

            const result = await response.json();
            
            if (result.success && result.data) {
                const events = result.data.sesiones.map(sesion => ({
                    id: sesion.id,
                    title: sesion.titulo,
                    start: sesion.fechaHoraInicio,
                    end: sesion.fechaHoraFin,
                    backgroundColor: stateColors[sesion.estadoSesion] || '#7367f0',
                    borderColor: stateColors[sesion.estadoSesion] || '#7367f0',
                    extendedProps: {
                        descripcion: sesion.descripcion,
                        ubicacionOEnlace: sesion.ubicacionOEnlace,
                        estadoSesion: sesion.estadoSesion,
                        clientes: sesion.clientes,
                        coachId: sesion.coachId,
                        coachNombre: sesion.coachNombre
                    }
                }));
                
                successCallback(events);
                
                // Update filters
                updateEventFilters();
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
                clienteIds: dropInfo.event.extendedProps.clientes.map(c => c.clienteId)
            };

            const success = await updateSession(sessionId, updateData);
            
            if (!success) {
                dropInfo.revert();
            }
        } catch (error) {
            console.error('Error updating event:', error);
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
                clienteIds: resizeInfo.event.extendedProps.clientes.map(c => c.clienteId)
            };

            const success = await updateSession(sessionId, updateData);
            
            if (!success) {
                resizeInfo.revert();
            }
        } catch (error) {
            console.error('Error resizing event:', error);
            resizeInfo.revert();
            showToast('Error al actualizar la duración', 'error');
        }
    }

    // Open session modal
    function openSessionModal(event = null, startDate = null, endDate = null) {
        const offcanvasEl = document.getElementById('sessionOffcanvas');
        const offcanvas = new bootstrap.Offcanvas(offcanvasEl);
        
        config.isEditMode = !!event;
        config.currentEvent = event;
        
        // Update title
        document.getElementById('offcanvasTitle').textContent = config.isEditMode ? 
            'Editar Sesión' : 'Añadir Sesión';
        
        // Show/hide delete button
        document.getElementById('btnDeleteSession').classList.toggle('d-none', !config.isEditMode);
        
        // Reset forms
        document.getElementById('sessionForm').reset();
        document.getElementById('notesForm').reset();
        
        // Reset to first tab
        document.querySelector('#details-tab').click();
        
        if (config.isEditMode && event) {
            // Populate form with event data
            document.getElementById('sessionId').value = event.id;
            document.getElementById('sessionTitle').value = event.title;
            document.getElementById('sessionDescription').value = event.extendedProps.descripcion || '';
            document.getElementById('sessionLocation').value = event.extendedProps.ubicacionOEnlace || '';
            document.getElementById('sessionStatus').value = event.extendedProps.estadoSesion;
            
            // Set dates
            config.startDatePicker.setDate(event.start);
            config.endDatePicker.setDate(event.end);
            
            // Set all day checkbox
            const isAllDay = !event.start.getHours() && !event.start.getMinutes() && 
                            !event.end.getHours() && !event.end.getMinutes();
            document.getElementById('allDayCheck').checked = isAllDay;
            
            // Set clients
            const clientIds = event.extendedProps.clientes.map(c => c.clienteId.toString());
            $(config.clientsSelect).val(clientIds).trigger('change');
            
            // Load notes if session is completed
            if (event.extendedProps.estadoSesion === 'Completada') {
                loadSessionNotes(event.id);
            }
        } else {
            // New session - set default dates
            if (startDate) {
                config.startDatePicker.setDate(startDate);
            }
            if (endDate) {
                config.endDatePicker.setDate(endDate);
            }
            
            // Default status
            document.getElementById('sessionStatus').value = 'Programada';
        }
        
        offcanvas.show();
    }

    // Initialize date pickers
    function initializeDatePickers() {
        const allDayCheck = document.getElementById('allDayCheck');
        
        config.startDatePicker = flatpickr('#sessionStart', {
            enableTime: true,
            dateFormat: 'Y-m-d H:i',
            time_24hr: true,
            locale: localStorage.getItem('selectedLanguage') || 'es',
            onChange: function(selectedDates) {
                if (selectedDates.length > 0) {
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
        
        config.endDatePicker = flatpickr('#sessionEnd', {
            enableTime: true,
            dateFormat: 'Y-m-d H:i',
            time_24hr: true,
            locale: localStorage.getItem('selectedLanguage') || 'es'
        });
        
        // Handle all day checkbox
        allDayCheck.addEventListener('change', function() {
            const enableTime = !this.checked;
            config.startDatePicker.set('enableTime', enableTime);
            config.endDatePicker.set('enableTime', enableTime);
            
            if (this.checked) {
                // Set times to 00:00
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
        });
    }

    // Initialize clients select
    function initializeClientsSelect() {
        config.clientsSelect = $('#sessionClients').select2({
            theme: 'bootstrap-5',
            placeholder: 'Seleccionar clientes...',
            allowClear: true,
            dropdownParent: $('#sessionOffcanvas'),
            language: localStorage.getItem('selectedLanguage') || 'es'
        });
        
        loadClients();
    }

    // Load clients from API
    async function loadClients() {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            const response = await fetch(`${config.baseUrl}/mis-clientes`, {
                headers: {
                    'RequestVerificationToken': token
                }
            });

            if (!response.ok) throw new Error('Error loading clients');

            const result = await response.json();
            
            if (result.success && result.data) {
                const select = document.getElementById('sessionClients');
                select.innerHTML = '';
                
                result.data.forEach(client => {
                    const option = new Option(
                        `${client.nombreCompleto} (${client.email})`,
                        client.id,
                        false,
                        false
                    );
                    select.add(option);
                });
                
                $(select).trigger('change');
            }
        } catch (error) {
            console.error('Error loading clients:', error);
            showToast('Error al cargar clientes', 'error');
        }
    }

    // Save session
    async function saveSession() {
        const form = document.getElementById('sessionForm');
        
        if (!form.checkValidity()) {
            form.reportValidity();
            return;
        }
        
        const selectedClients = $(config.clientsSelect).val();
        if (!selectedClients || selectedClients.length === 0) {
            showToast('Debe seleccionar al menos un cliente', 'warning');
            return;
        }
        
        const sessionData = {
            titulo: document.getElementById('sessionTitle').value,
            descripcion: document.getElementById('sessionDescription').value,
            fechaHoraInicio: config.startDatePicker.selectedDates[0].toISOString(),
            fechaHoraFin: config.endDatePicker.selectedDates[0].toISOString(),
            ubicacionOEnlace: document.getElementById('sessionLocation').value,
            clienteIds: selectedClients.map(id => parseInt(id))
        };
        
        try {
            let success = false;
            
            if (config.isEditMode) {
                const sessionId = document.getElementById('sessionId').value;
                success = await updateSession(sessionId, sessionData);
                
                // Update status if changed
                const newStatus = document.getElementById('sessionStatus').value;
                if (config.currentEvent.extendedProps.estadoSesion !== newStatus) {
                    await updateSessionStatus(sessionId, newStatus);
                }
                
                // Save notes if on notes tab
                if (document.getElementById('notes-tab').classList.contains('active')) {
                    await saveSessionNotes(sessionId);
                }
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

    // Create session
    async function createSession(sessionData) {
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        
        const response = await fetch(`${config.baseUrl}/sesiones`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': token
            },
            body: JSON.stringify(sessionData)
        });

        const result = await response.json();
        return result.success;
    }

    // Update session
    async function updateSession(sessionId, sessionData) {
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        
        const response = await fetch(`${config.baseUrl}/sesiones/${sessionId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': token
            },
            body: JSON.stringify(sessionData)
        });

        const result = await response.json();
        return result.success;
    }

    // Update session status
    async function updateSessionStatus(sessionId, newStatus) {
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        
        const response = await fetch(`${config.baseUrl}/sesiones/${sessionId}/estado`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': token
            },
            body: JSON.stringify({ estadoSesion: newStatus })
        });

        const result = await response.json();
        return result.success;
    }

    // Delete session
    async function deleteSession() {
        if (!confirm('¿Está seguro de que desea eliminar esta sesión?')) {
            return;
        }
        
        try {
            const sessionId = document.getElementById('sessionId').value;
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            
            const response = await fetch(`${config.baseUrl}/sesiones/${sessionId}`, {
                method: 'DELETE',
                headers: {
                    'RequestVerificationToken': token
                }
            });

            const result = await response.json();
            
            if (result.success) {
                bootstrap.Offcanvas.getInstance(document.getElementById('sessionOffcanvas')).hide();
                config.calendar.refetchEvents();
                showToast('Sesión eliminada exitosamente', 'success');
            } else {
                showToast(result.message || 'Error al eliminar la sesión', 'error');
            }
        } catch (error) {
            console.error('Error deleting session:', error);
            showToast('Error al eliminar la sesión', 'error');
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

    // Event listeners
    document.getElementById('btnAddSession').addEventListener('click', () => openSessionModal());
    document.getElementById('btnSaveSession').addEventListener('click', saveSession);
    document.getElementById('btnDeleteSession').addEventListener('click', deleteSession);

    // Initialize everything
    initializeCalendar();
    initializeMiniCalendar();
    initializeDatePickers();
    initializeClientsSelect();
    
    // Apply translations
    if (typeof applyTranslations === 'function') {
        applyTranslations();
    }
});