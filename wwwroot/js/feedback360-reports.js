// Feedback 360 Reports functionality and translations
document.addEventListener('DOMContentLoaded', function() {
    // Initialize tooltips for better UX
    initializeTooltips();
    
    // Auto-refresh for reports in progress
    initializeAutoRefresh();
    
    // Handle dynamic content translations
    applyTranslations();
});

function initializeTooltips() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}

function initializeAutoRefresh() {
    // Auto-refresh for reports that are still collecting feedback
    const hasInProgress = document.querySelector('[data-status="CollectingFeedback"]');
    if (hasInProgress) {
        setTimeout(function() {
            location.reload();
        }, 300000); // 5 minutes
    }
}

function applyTranslations() {
    // Apply translations to dynamic content
    translateDynamicContent();
}

function translateDynamicContent() {
    // Translate report counts
    translateReportCounts();
    
    // Translate status badges
    translateStatusBadges();
    
    // Translate competency names if needed
    translateCompetencyNames();
}

function translateReportCounts() {
    const reportCountElements = document.querySelectorAll('[data-report-count]');
    reportCountElements.forEach(element => {
        const count = parseInt(element.dataset.reportCount);
        const isClient = element.dataset.isClient === 'true';
        
        if (isClient) {
            const key = count === 1 ? 'client_feedback360_reports.reports_available' : 'client_feedback360_reports.reports_available_plural';
            const translation = getTranslation(key, count === 1 ? 'Report' : 'Reports');
            element.textContent = `${count} ${translation} Available`;
        }
    });
}

function translateStatusBadges() {
    const statusElements = document.querySelectorAll('[data-status-translate]');
    statusElements.forEach(element => {
        const status = element.dataset.statusTranslate;
        let translationKey = '';
        
        switch(status) {
            case 'PendingSetup':
                translationKey = 'client_feedback360_reports.setting_up';
                break;
            case 'CollectingFeedback':
                translationKey = 'client_feedback360_reports.collecting_feedback';
                break;
            case 'Completed':
                translationKey = 'client_feedback360_reports.completed';
                break;
        }
        
        if (translationKey) {
            const translation = getTranslation(translationKey, element.textContent);
            element.textContent = translation;
        }
    });
}

function translateCompetencyNames() {
    // Translate standard competency names to user's language
    const competencyElements = document.querySelectorAll('[data-competency]');
    competencyElements.forEach(element => {
        const competencyCode = element.dataset.competency;
        const translation = getCompetencyTranslation(competencyCode);
        if (translation) {
            element.textContent = translation;
        }
    });
}

function getCompetencyTranslation(competencyCode) {
    // Standard competency translations
    const competencyTranslations = {
        'en': {
            'LEADERSHIP': 'Leadership',
            'COMMUNICATION': 'Communication',
            'TEAMWORK': 'Teamwork',
            'PROBLEM_SOLVING': 'Problem Solving',
            'ADAPTABILITY': 'Adaptability'
        },
        'es': {
            'LEADERSHIP': 'Liderazgo',
            'COMMUNICATION': 'Comunicación',
            'TEAMWORK': 'Trabajo en Equipo',
            'PROBLEM_SOLVING': 'Resolución de Problemas',
            'ADAPTABILITY': 'Adaptabilidad'
        }
    };
    
    const currentLang = getCurrentLanguage();
    return competencyTranslations[currentLang]?.[competencyCode];
}

function getCurrentLanguage() {
    // Get current language from localStorage or default to 'en'
    return localStorage.getItem('selectedLanguage') || 'en';
}

function getTranslation(key, defaultText) {
    // Get translation using the global translation system
    if (typeof window.getTranslation === 'function') {
        return window.getTranslation(key, defaultText);
    }
    return defaultText;
}

// Export functions for use in other scripts
window.Feedback360Reports = {
    initializeTooltips,
    applyTranslations,
    getCompetencyTranslation,
    translateDynamicContent
};