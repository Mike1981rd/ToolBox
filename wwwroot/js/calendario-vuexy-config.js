// Vuexy Calendar Configuration for FullCalendar
const VuexyCalendarConfig = {
    
    // Main calendar configuration
    getCalendarOptions: function() {
        return {
            // Basic configuration
            initialView: 'dayGridMonth',
            locale: 'en',
            firstDay: 0, // Sunday
            height: 'auto',
            aspectRatio: 1.8,
            
            // Hide default header (we use custom)
            headerToolbar: false,
            
            // Interaction
            editable: true,
            selectable: true,
            selectMirror: true,
            dayMaxEvents: 3,
            eventStartEditable: true,
            eventDurationEditable: true,
            
            // Time configuration
            slotMinTime: '07:00:00',
            slotMaxTime: '21:00:00',
            slotDuration: '00:30:00',
            slotLabelInterval: '01:00',
            
            // Business hours (optional)
            businessHours: {
                daysOfWeek: [1, 2, 3, 4, 5],
                startTime: '09:00',
                endTime: '18:00'
            },
            
            // Event display
            eventDisplay: 'block',
            eventTimeFormat: {
                hour: 'numeric',
                minute: '2-digit',
                meridiem: 'short'
            },
            
            // Day cell customization
            dayCellClassNames: function(arg) {
                let classes = [];
                
                // Weekend styling
                if (arg.date.getDay() === 0 || arg.date.getDay() === 6) {
                    classes.push('fc-weekend');
                }
                
                return classes;
            },
            
            // Event rendering customization
            eventContent: function(arg) {
                let customHtml = '';
                let timeText = arg.timeText;
                let title = arg.event.title;
                
                if (arg.view.type === 'dayGridMonth') {
                    // Month view - compact display
                    customHtml = `
                        <div class="fc-event-main-frame">
                            ${timeText ? `<div class="fc-event-time">${timeText}</div>` : ''}
                            <div class="fc-event-title-wrap">
                                <div class="fc-event-title fc-sticky">${title}</div>
                            </div>
                        </div>
                    `;
                } else if (arg.view.type === 'timeGridWeek' || arg.view.type === 'timeGridDay') {
                    // Week/Day view - detailed display
                    customHtml = `
                        <div class="fc-event-main-frame">
                            <div class="fc-event-time">${timeText}</div>
                            <div class="fc-event-title">${title}</div>
                            ${arg.event.extendedProps.location ? 
                                `<div class="fc-event-location">
                                    <i class="fas fa-map-marker-alt"></i> ${arg.event.extendedProps.location}
                                </div>` : ''
                            }
                        </div>
                    `;
                }
                
                return { html: customHtml };
            },
            
            // Event class names based on label
            eventClassNames: function(arg) {
                const label = arg.event.extendedProps.label || 'primary';
                return [`fc-event-${label}`];
            },
            
            // More link text
            moreLinkText: function(num) {
                return `+${num} more`;
            },
            
            // View-specific options
            views: {
                dayGridMonth: {
                    dayMaxEventRows: 3,
                    moreLinkClick: 'popover'
                },
                timeGrid: {
                    dayMaxEventRows: false
                }
            },
            
            // Loading callback
            loading: function(isLoading) {
                // Handle loading state
                if (isLoading) {
                    // Show loading indicator
                } else {
                    // Hide loading indicator
                }
            }
        };
    },
    
    // Mini calendar configuration
    getMiniCalendarOptions: function() {
        return {
            initialView: 'dayGridMonth',
            height: 'auto',
            headerToolbar: {
                left: 'prev',
                center: 'title', 
                right: 'next'
            },
            titleFormat: { month: 'short', year: 'numeric' },
            dayHeaderFormat: { weekday: 'narrow' },
            eventDisplay: 'none', // Don't show events in mini calendar
            
            // Custom day cell content
            dayCellContent: function(arg) {
                return arg.dayNumberText.replace(/[^\d]/g, '');
            }
        };
    },
    
    // Event colors mapping
    eventColors: {
        primary: '#7367f0',
        success: '#28c76f',
        danger: '#ea5455',
        warning: '#ff9f43',
        info: '#00cfe8',
        secondary: '#82868b'
    },
    
    // Sample events for demo
    getSampleEvents: function() {
        const today = new Date();
        const y = today.getFullYear();
        const m = today.getMonth();
        
        return [
            {
                id: 1,
                title: 'Design Review',
                start: new Date(y, m, 1, 10, 30),
                end: new Date(y, m, 1, 11, 30),
                extendedProps: {
                    label: 'primary',
                    location: 'Conference Room A'
                }
            },
            {
                id: 2,
                title: 'Team Meeting',
                start: new Date(y, m, 5, 14, 0),
                end: new Date(y, m, 5, 15, 30),
                extendedProps: {
                    label: 'success',
                    location: 'Online - Zoom'
                }
            },
            {
                id: 3,
                title: 'Client Presentation',
                start: new Date(y, m, 10),
                allDay: true,
                extendedProps: {
                    label: 'danger'
                }
            },
            {
                id: 4,
                title: 'Project Deadline',
                start: new Date(y, m, 15),
                allDay: true,
                extendedProps: {
                    label: 'warning'
                }
            },
            {
                id: 5,
                title: 'Office Party',
                start: new Date(y, m, 20, 18, 0),
                end: new Date(y, m, 20, 21, 0),
                extendedProps: {
                    label: 'info',
                    location: 'Office Cafeteria'
                }
            }
        ];
    }
};

// Export for use
if (typeof module !== 'undefined' && module.exports) {
    module.exports = VuexyCalendarConfig;
}