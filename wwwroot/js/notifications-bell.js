// Enhanced Notifications Bell Functionality - Vuexy Style
(function() {
    'use strict';

    // Configuration
    const API_BASE_URL = '/api/notifications';
    const POLL_INTERVAL = 60000; // 60 seconds
    const MAX_DROPDOWN_NOTIFICATIONS = 5; // Show max 5 in dropdown
    
    // State
    let notificationDropdown = null;
    let notificationBadge = null;
    let notificationBell = null;
    let isDropdownOpen = false;
    let pollingInterval = null;
    let currentLanguage = 'es';
    let dismissedNotifications = new Set(); // Track dismissed notifications

    // Initialize when DOM is ready
    document.addEventListener('DOMContentLoaded', function() {
        initializeNotificationBell();
    });

    function initializeNotificationBell() {
        // Get current language
        currentLanguage = getCurrentLanguage();
        
        // Load dismissed notifications from localStorage
        loadDismissedNotifications();
        
        // Find the notification elements
        notificationBell = document.getElementById('notificationBellBtn');
        notificationBadge = document.getElementById('notificationBadge');
        notificationDropdown = document.getElementById('notificationDropdown');
        
        if (!notificationBell || !notificationDropdown) {
            console.warn('Notification elements not found');
            return;
        }

        // Add event listeners
        setupEventListeners();
        
        // Listen for language changes
        document.addEventListener('languageChanged', function(e) {
            currentLanguage = e.detail.language;
            console.log('Language changed to:', currentLanguage);
            
            // Force re-translate notification dropdown elements
            setTimeout(() => {
                applyNotificationTranslations();
            }, 100);
            
            // Refresh notifications with new language
            if (isDropdownOpen) {
                loadNotificationsForDropdown();
            }
            updateNotificationCount();
        });

        // Load initial notification count and data
        updateNotificationCount();
        
        // Apply initial translations
        setTimeout(() => {
            applyNotificationTranslations();
        }, 500);
        
        // Start polling for new notifications
        startPolling();
    }

    function setupEventListeners() {
        // Bell click handler (Bootstrap handles dropdown toggle)
        notificationBell.addEventListener('shown.bs.dropdown', function() {
            isDropdownOpen = true;
            loadNotificationsForDropdown();
        });

        notificationBell.addEventListener('hidden.bs.dropdown', function() {
            isDropdownOpen = false;
        });

        // Mark all as read button
        const markAllBtn = document.getElementById('markAllReadBtn');
        if (markAllBtn) {
            markAllBtn.addEventListener('click', handleMarkAllAsRead);
        }
    }

    async function updateNotificationCount() {
        try {
            // Get all notifications to filter dismissed ones
            const response = await fetch(`${API_BASE_URL}?pageNumber=1&pageSize=50`, {
                method: 'GET',
                headers: { 'Accept': 'application/json' }
            });

            if (response.ok) {
                const data = await response.json();
                const allNotifications = data.notifications || [];
                
                // Filter out dismissed notifications and count unread
                const activeUnreadNotifications = allNotifications.filter(notification => 
                    !notification.readAt && !dismissedNotifications.has(notification.id.toString())
                );
                
                const count = activeUnreadNotifications.length;
                
                updateBadge(count);
                updateDropdownHeader(count);
            }
        } catch (error) {
            console.error('Error fetching notification count:', error);
        }
    }

    function updateBadge(count) {
        const badge = document.getElementById('notificationBadge');
        const countElement = document.getElementById('notificationCount');
        
        if (badge && countElement) {
            if (count > 0) {
                badge.classList.remove('d-none');
                countElement.textContent = count > 99 ? '99+' : count.toString();
            } else {
                badge.classList.add('d-none');
            }
        }
    }

    function updateDropdownHeader(count) {
        const headerCount = document.getElementById('notificationHeaderCount');
        if (headerCount) {
            const lang = getCurrentLanguage();
            const text = lang === 'es' ? `${count} Nuevas` : `${count} New`;
            headerCount.textContent = text;
        }
    }

    async function loadNotificationsForDropdown() {
        const container = document.getElementById('notificationListContainer');
        if (!container) return;

        // Show loading
        container.innerHTML = createLoadingHTML();

        try {
            // Get latest notifications (both read and unread) for the dropdown
            const response = await fetch(`${API_BASE_URL}?pageNumber=1&pageSize=${MAX_DROPDOWN_NOTIFICATIONS}`, {
                method: 'GET',
                headers: { 'Accept': 'application/json' }
            });

            if (response.ok) {
                const data = await response.json();
                const allNotifications = data.notifications || [];
                
                // Filter out dismissed notifications
                const notifications = allNotifications.filter(notification => 
                    !dismissedNotifications.has(notification.id.toString())
                );
                
                if (notifications.length === 0) {
                    container.innerHTML = createEmptyNotificationsHTML();
                } else {
                    container.innerHTML = notifications.map(notification => 
                        createNotificationItemHTML(notification)
                    ).join('');
                    
                    // Add event listeners to notification items
                    addNotificationItemListeners(container);
                }
            } else {
                container.innerHTML = createErrorHTML();
            }
        } catch (error) {
            console.error('Error loading notifications:', error);
            container.innerHTML = createErrorHTML();
        }
    }

    function createNotificationItemHTML(notification) {
        const notificationData = parseNotificationData(notification.data);
        const { avatar, icon } = getNotificationAvatarAndIcon(notification.type, notificationData);
        const { title, subtitle } = formatNotificationMessage(notification.type, notificationData);
        const timeAgo = formatTimeAgo(notification.createdAt);
        const isUnread = !notification.readAt;

        return `
            <div class="notification-item-vuexy ${isUnread ? 'unread' : ''}" data-notification-id="${notification.id}">
                <div class="d-flex align-items-start px-3 py-3 notification-item-content-vuexy position-relative">
                    <!-- Avatar/Icon -->
                    <div class="notification-avatar-vuexy me-3">
                        ${avatar ? 
                            `<img src="${avatar}" alt="Avatar" class="rounded-circle" width="32" height="32">` :
                            `<div class="notification-icon-vuexy ${icon.class}">
                                <i class="${icon.icon}"></i>
                            </div>`
                        }
                    </div>
                    
                    <!-- Content -->
                    <div class="notification-content-vuexy flex-grow-1 me-2">
                        <div class="notification-title-text">${title}</div>
                        <div class="notification-subtitle-text">${subtitle}</div>
                        <div class="notification-time-vuexy">${timeAgo}</div>
                    </div>
                    
                    <!-- Actions -->
                    <div class="notification-actions-vuexy">
                        <!-- Unread indicator -->
                        ${isUnread ? '<div class="notification-dot-vuexy"></div>' : ''}
                        <!-- Dismiss button -->
                        <button class="notification-dismiss" data-notification-id="${notification.id}" title="${currentLanguage === 'es' ? 'Eliminar notificación' : 'Remove notification'}">
                            <i class="fas fa-times"></i>
                        </button>
                    </div>
                </div>
            </div>
        `;
    }

    function addNotificationItemListeners(container) {
        // Add click listeners to notification items
        container.querySelectorAll('.notification-item-vuexy').forEach(item => {
            const contentArea = item.querySelector('.notification-item-content-vuexy');
            contentArea.addEventListener('click', function(e) {
                e.preventDefault();
                e.stopPropagation(); // Prevent dropdown from closing
                
                // Don't handle click if it's on the dismiss button
                if (e.target.closest('.notification-dismiss')) {
                    return;
                }
                
                const notificationId = item.dataset.notificationId;
                handleNotificationItemClick(notificationId, item);
            });
        });
        
        // Add click listeners to dismiss buttons
        container.querySelectorAll('.notification-dismiss').forEach(button => {
            button.addEventListener('click', function(e) {
                e.preventDefault();
                e.stopPropagation(); // Prevent dropdown from closing
                
                const notificationId = this.dataset.notificationId;
                const itemElement = this.closest('.notification-item-vuexy');
                handleNotificationDismiss(notificationId, itemElement);
            });
        });
    }

    async function handleNotificationDismiss(notificationId, itemElement) {
        try {
            // Mark as read on server
            await markNotificationAsRead(notificationId);
            
            // Add to dismissed notifications (locally)
            dismissedNotifications.add(notificationId.toString());
            saveDismissedNotifications();
            
            // Remove the item with animation
            itemElement.style.opacity = '0';
            itemElement.style.transform = 'translateX(100%)';
            
            setTimeout(() => {
                itemElement.remove();
                
                // Check if no more notifications
                const container = document.getElementById('notificationListContainer');
                const remainingItems = container.querySelectorAll('.notification-item-vuexy');
                
                if (remainingItems.length === 0) {
                    container.innerHTML = createEmptyNotificationsHTML();
                }
            }, 300);
            
            // Update counters
            updateNotificationCount();
            
        } catch (error) {
            console.error('Error dismissing notification:', error);
            const lang = getCurrentLanguage();
            showToast(lang === 'es' ? 'Error al descartar notificación' : 'Error dismissing notification', 'error');
        }
    }

    async function handleNotificationItemClick(notificationId, itemElement) {
        // Mark as read if unread
        if (itemElement.classList.contains('unread')) {
            try {
                await markNotificationAsRead(notificationId);
                
                // Update UI - remove unread class and dot, but keep item in card
                itemElement.classList.remove('unread');
                const dot = itemElement.querySelector('.notification-dot-vuexy');
                if (dot) dot.remove();
                
                // Update counters
                updateNotificationCount();
                
                console.log('Notification marked as read, but kept in card');
            } catch (error) {
                console.error('Error marking notification as read:', error);
            }
        }
        
        // Optionally redirect or perform action based on notification type
        // This can be enhanced based on specific notification types
    }


    async function handleMarkAllAsRead() {
        try {
            const response = await fetch(`${API_BASE_URL}/mark-all-as-read`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'X-CSRF-TOKEN': getAntiForgeryToken()
                }
            });

            if (response.ok) {
                // Update UI
                const container = document.getElementById('notificationListContainer');
                container.innerHTML = createEmptyNotificationsHTML();
                
                // Update counters
                updateBadge(0);
                updateDropdownHeader(0);
                
                showToast(currentLanguage === 'en' ? 'All notifications marked as read' : 'Todas las notificaciones marcadas como leídas', 'success');
            } else {
                throw new Error('Failed to mark all as read');
            }
        } catch (error) {
            console.error('Error marking all as read:', error);
            showToast(currentLanguage === 'en' ? 'Error marking notifications as read' : 'Error al marcar notificaciones como leídas', 'error');
        }
    }

    async function markNotificationAsRead(notificationId) {
        const response = await fetch(`${API_BASE_URL}/${notificationId}/mark-as-read`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'X-CSRF-TOKEN': getAntiForgeryToken()
            }
        });

        if (!response.ok) {
            throw new Error('Failed to mark notification as read');
        }
        
        return response.json();
    }

    function getNotificationAvatarAndIcon(type, data) {
        // Try to get avatar from data first
        let avatar = null;
        if (data && (data.senderAvatarUrl || data.coachAvatarUrl)) {
            avatar = data.senderAvatarUrl || data.coachAvatarUrl;
        }

        // Get icon based on notification type - using colorful icons like Vuexy
        let icon = { icon: 'fas fa-bell', class: 'default' };
        
        switch(type) {
            case 'session_scheduled_by_coach':
                icon = { icon: 'fas fa-calendar-check', class: 'session' }; // Calendar icon for sessions
                break;
            case 'calendar_event_invitation':
                icon = { icon: 'fas fa-envelope', class: 'message' }; // Envelope icon for invitations
                break;
            case 'calendar_event_scheduled_for_client':
                icon = { icon: 'fas fa-calendar-alt', class: 'order' }; // Calendar icon for events
                break;
            case 'questionnaire_assigned':
                icon = { icon: 'fas fa-clipboard-list', class: 'approved' }; // Clipboard icon for questionnaires
                break;
            case 'questionnaire_completed_by_client':
                icon = { icon: 'fas fa-check-square', class: 'approved' }; // Check icon for completed questionnaires
                break;
            default:
                icon = { icon: 'fas fa-bell', class: 'approved' }; // Bell icon for general notifications
        }

        return { avatar, icon };
    }

    function formatNotificationMessage(type, data) {
        const lang = getCurrentLanguage();
        
        // Debug log to see what data we're receiving
        console.log('formatNotificationMessage - type:', type, 'data:', data);
        
        try {
            switch(type) {
                case 'session_scheduled_by_coach':
                    if (lang === 'es') {
                        return {
                            title: 'Nueva Sesión Programada 📅',
                            subtitle: `${data.CoachName || 'Tu coach'} ha programado una nueva sesión para ti`
                        };
                    } else {
                        return {
                            title: 'New Session Scheduled 📅',
                            subtitle: `${data.CoachName || 'Your coach'} has scheduled a new session for you`
                        };
                    }
                    
                case 'calendar_event_invitation':
                    // Format the event date
                    let eventDateStr = '';
                    if (data.eventStartDate) {
                        const eventDate = new Date(data.eventStartDate);
                        if (!isNaN(eventDate.getTime())) {
                            if (lang === 'es') {
                                eventDateStr = ` el ${eventDate.toLocaleDateString('es-ES', { 
                                    weekday: 'long', 
                                    year: 'numeric', 
                                    month: 'long', 
                                    day: 'numeric',
                                    hour: '2-digit',
                                    minute: '2-digit'
                                })}`;
                            } else {
                                eventDateStr = ` on ${eventDate.toLocaleDateString('en-US', { 
                                    weekday: 'long', 
                                    year: 'numeric', 
                                    month: 'long', 
                                    day: 'numeric',
                                    hour: '2-digit',
                                    minute: '2-digit'
                                })}`;
                            }
                        }
                    }
                    
                    if (lang === 'es') {
                        return {
                            title: 'Invitación a Evento 💬',
                            subtitle: `${data.invitedBy || 'Un coach'} te ha invitado al evento "${data.eventTitle || 'Sin título'}"${eventDateStr}`
                        };
                    } else {
                        return {
                            title: 'Event Invitation 💬',
                            subtitle: `${data.invitedBy || 'A coach'} invited you to "${data.eventTitle || 'Untitled event'}"${eventDateStr}`
                        };
                    }
                    
                case 'calendar_event_scheduled_for_client':
                    if (lang === 'es') {
                        return {
                            title: 'Nuevo Evento Programado 📋',
                            subtitle: `El evento "${data.eventTitle || 'Sin título'}" ha sido programado para ti`
                        };
                    } else {
                        return {
                            title: 'New Event Scheduled 📋',
                            subtitle: `Event "${data.eventTitle || 'Untitled'}" has been scheduled for you`
                        };
                    }
                    
                case 'questionnaire_assigned':
                    if (lang === 'es') {
                        return {
                            title: 'Nuevo Cuestionario 📝',
                            subtitle: `${data.coachName || 'Tu coach'} te ha enviado un nuevo cuestionario: "${data.questionnaireTitle || 'Sin título'}"`
                        };
                    } else {
                        return {
                            title: 'New Questionnaire 📝',
                            subtitle: `${data.coachName || 'Your coach'} has sent you a new questionnaire: "${data.questionnaireTitle || 'Untitled'}"`
                        };
                    }
                    
                case 'questionnaire_completed_by_client':
                    if (lang === 'es') {
                        return {
                            title: 'Cuestionario Completado ✅',
                            subtitle: `${data.clientName || 'Un cliente'} ha completado el cuestionario: "${data.questionnaireTitle || 'Sin título'}"`
                        };
                    } else {
                        return {
                            title: 'Questionnaire Completed ✅',
                            subtitle: `${data.clientName || 'A client'} has completed the questionnaire: "${data.questionnaireTitle || 'Untitled'}"`
                        };
                    }
                    
                default:
                    if (lang === 'es') {
                        return {
                            title: 'Nueva Notificación 🔔',
                            subtitle: 'Has recibido una nueva notificación'
                        };
                    } else {
                        return {
                            title: 'New Notification 🔔',
                            subtitle: 'You have received a new notification'
                        };
                    }
            }
        } catch (error) {
            console.error('Error formatting notification message:', error);
            return lang === 'es' 
                ? { title: 'Notificación', subtitle: 'Error al cargar' }
                : { title: 'Notification', subtitle: 'Error loading' };
        }
    }

    function formatTimeAgo(dateString) {
        const date = new Date(dateString);
        const now = new Date();
        const diffMs = now - date;
        const diffMins = Math.floor(diffMs / 60000);
        const diffHours = Math.floor(diffMins / 60);
        const diffDays = Math.floor(diffHours / 24);
        
        const lang = getCurrentLanguage();
        
        if (diffMins < 1) {
            return lang === 'es' ? 'Ahora' : 'Now';
        } else if (diffMins < 60) {
            return lang === 'es' ? `hace ${diffMins}m` : `${diffMins}m ago`;
        } else if (diffHours < 24) {
            return lang === 'es' ? `hace ${diffHours}h` : `${diffHours}h ago`;
        } else if (diffDays === 1) {
            return lang === 'es' ? 'hace 1 día' : '1 day ago';
        } else if (diffDays === 2) {
            return lang === 'es' ? 'hace 2 días' : '2 days ago';
        } else if (diffDays < 7) {
            return lang === 'es' ? `hace ${diffDays} días` : `${diffDays} days ago`;
        } else {
            return date.toLocaleDateString();
        }
    }

    function parseNotificationData(dataJson) {
        try {
            return typeof dataJson === 'string' ? JSON.parse(dataJson) : dataJson;
        } catch (error) {
            console.error('Error parsing notification data:', error);
            return {};
        }
    }


    function createLoadingHTML() {
        const lang = getCurrentLanguage();
        return `
            <div class="notification-loading text-center py-4">
                <div class="spinner-border spinner-border-sm text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
                <div class="mt-2 text-muted small">${lang === 'es' ? 'Cargando...' : 'Loading...'}</div>
            </div>
        `;
    }

    function createEmptyNotificationsHTML() {
        const lang = getCurrentLanguage();
        return `
            <div class="notification-empty text-center py-4">
                <i class="fas fa-bell-slash text-muted mb-2" style="font-size: 2rem;"></i>
                <div class="text-muted">${lang === 'es' ? 'Sin notificaciones' : 'No notifications'}</div>
                <small class="text-muted">${lang === 'es' ? '¡Estás al día!' : 'You\'re all caught up!'}</small>
            </div>
        `;
    }

    function createErrorHTML() {
        const lang = getCurrentLanguage();
        return `
            <div class="notification-error text-center py-4">
                <i class="fas fa-exclamation-triangle text-warning mb-2"></i>
                <div class="text-muted small">${lang === 'es' ? 'Error al cargar notificaciones' : 'Error loading notifications'}</div>
            </div>
        `;
    }

    function startPolling() {
        if (pollingInterval) {
            clearInterval(pollingInterval);
        }
        
        pollingInterval = setInterval(() => {
            updateNotificationCount();
            
            // If dropdown is open, refresh its content
            if (isDropdownOpen) {
                loadNotificationsForDropdown();
            }
        }, POLL_INTERVAL);
    }

    function getCurrentLanguage() {
        return localStorage.getItem('selectedLanguage') || 'es';
    }

    function getAntiForgeryToken() {
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        return token ? token.value : '';
    }

    function showToast(message, type = 'info') {
        // Simple toast implementation - can be enhanced
        console.log(`Toast (${type}): ${message}`);
        
        // If you have a toast system, use it here
        // For now, just log to console
    }

    // Dismissed notifications management
    function loadDismissedNotifications() {
        try {
            const stored = localStorage.getItem('dismissedNotifications');
            if (stored) {
                dismissedNotifications = new Set(JSON.parse(stored));
            }
        } catch (error) {
            console.error('Error loading dismissed notifications:', error);
            dismissedNotifications = new Set();
        }
    }

    function saveDismissedNotifications() {
        try {
            localStorage.setItem('dismissedNotifications', JSON.stringify([...dismissedNotifications]));
        } catch (error) {
            console.error('Error saving dismissed notifications:', error);
        }
    }

    // Force apply translations to notification elements
    function applyNotificationTranslations() {
        const lang = getCurrentLanguage();
        
        // Try to get translations from the global admin script
        if (typeof window.translations !== 'undefined' && window.translations[lang]) {
            const translations = window.translations[lang];
            
            // Update notification title
            const titleElement = document.querySelector('.notification-title-vuexy[data-translate-key="notifications.title"]');
            if (titleElement && translations['notifications.title']) {
                titleElement.textContent = translations['notifications.title'];
            }
            
            // Update view all button
            const viewAllElement = document.querySelector('.notification-view-all-vuexy[data-translate-key="notifications.viewAll"]');
            if (viewAllElement && translations['notifications.viewAll']) {
                viewAllElement.textContent = translations['notifications.viewAll'];
            }
            
            // Update count badge
            updateDropdownHeader(getCurrentUnreadCount());
            
            console.log('Applied notification translations for language:', lang);
        }
    }
    
    function getCurrentUnreadCount() {
        const badge = document.getElementById('notificationCount');
        return badge ? parseInt(badge.textContent) || 0 : 0;
    }

    // Cleanup on page unload
    window.addEventListener('beforeunload', function() {
        if (pollingInterval) {
            clearInterval(pollingInterval);
        }
    });

})();