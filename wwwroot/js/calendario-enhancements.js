// Calendar UI Enhancements
document.addEventListener('DOMContentLoaded', function() {
    
    // Smooth scroll when navigating dates
    let isTransitioning = false;
    
    // Override FullCalendar navigation with smooth transitions
    function enhanceCalendarTransitions() {
        const calendar = document.getElementById('calendar');
        if (!calendar) return;
        
        // Add transition class on view change
        const observer = new MutationObserver((mutations) => {
            if (!isTransitioning) {
                isTransitioning = true;
                calendar.style.opacity = '0.7';
                calendar.style.transform = 'scale(0.98)';
                
                setTimeout(() => {
                    calendar.style.opacity = '1';
                    calendar.style.transform = 'scale(1)';
                    isTransitioning = false;
                }, 150);
            }
        });
        
        observer.observe(calendar, { 
            childList: true, 
            subtree: true,
            attributes: true,
            attributeFilter: ['class']
        });
    }
    
    // Enhance filter checkboxes with ripple effect
    function addRippleEffect() {
        const checkboxes = document.querySelectorAll('.filter-checkboxes .form-check');
        
        checkboxes.forEach(checkbox => {
            checkbox.addEventListener('click', function(e) {
                const ripple = document.createElement('div');
                ripple.className = 'ripple';
                
                const rect = this.getBoundingClientRect();
                const size = Math.max(rect.width, rect.height);
                const x = e.clientX - rect.left - size / 2;
                const y = e.clientY - rect.top - size / 2;
                
                ripple.style.width = ripple.style.height = size + 'px';
                ripple.style.left = x + 'px';
                ripple.style.top = y + 'px';
                
                this.appendChild(ripple);
                
                setTimeout(() => ripple.remove(), 600);
            });
        });
    }
    
    // Enhance mini calendar hover effects
    function enhanceMiniCalendar() {
        const miniCalendar = document.getElementById('calendarMini');
        if (!miniCalendar) return;
        
        // Add hover effects to day cells
        const observer = new MutationObserver(() => {
            const dayCells = miniCalendar.querySelectorAll('.fc-daygrid-day');
            dayCells.forEach(cell => {
                if (!cell.hasAttribute('data-enhanced')) {
                    cell.setAttribute('data-enhanced', 'true');
                    
                    cell.addEventListener('mouseenter', function() {
                        this.style.transform = 'scale(1.05)';
                        this.style.zIndex = '10';
                    });
                    
                    cell.addEventListener('mouseleave', function() {
                        this.style.transform = 'scale(1)';
                        this.style.zIndex = '1';
                    });
                }
            });
        });
        
        observer.observe(miniCalendar, { childList: true, subtree: true });
    }
    
    // Add pulse animation to today button
    function animateTodayButton() {
        const todayBtn = document.querySelector('.fc-today-button');
        if (!todayBtn) return;
        
        // Check if we're not on today's date
        setInterval(() => {
            const calendar = window.calendar || window.config?.calendar;
            if (calendar) {
                const currentDate = calendar.getDate();
                const today = new Date();
                
                // If not viewing today, add pulse
                if (currentDate.toDateString() !== today.toDateString()) {
                    todayBtn.classList.add('pulse');
                } else {
                    todayBtn.classList.remove('pulse');
                }
            }
        }, 1000);
    }
    
    // Initialize all enhancements
    setTimeout(() => {
        enhanceCalendarTransitions();
        addRippleEffect();
        enhanceMiniCalendar();
        animateTodayButton();
    }, 500);
});

// CSS for ripple effect (add to calendario-style-refined.css)
const rippleStyles = `
<style>
.form-check {
    position: relative;
    overflow: hidden;
}

.ripple {
    position: absolute;
    border-radius: 50%;
    background: rgba(115, 103, 240, 0.3);
    transform: scale(0);
    animation: ripple-animation 0.6s ease-out;
    pointer-events: none;
}

@keyframes ripple-animation {
    to {
        transform: scale(4);
        opacity: 0;
    }
}

.fc-today-button.pulse {
    animation: pulse 2s infinite;
}

@keyframes pulse {
    0% {
        box-shadow: 0 0 0 0 rgba(115, 103, 240, 0.7);
    }
    70% {
        box-shadow: 0 0 0 10px rgba(115, 103, 240, 0);
    }
    100% {
        box-shadow: 0 0 0 0 rgba(115, 103, 240, 0);
    }
}

#calendar {
    transition: opacity 0.15s ease, transform 0.15s ease;
}

#calendarMini .fc-daygrid-day {
    transition: transform 0.2s ease, background-color 0.2s ease;
}
</style>
`;

document.head.insertAdjacentHTML('beforeend', rippleStyles);