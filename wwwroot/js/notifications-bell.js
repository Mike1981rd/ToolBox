// Notifications Bell Functionality
(function() {
    'use strict';

    // Configuration
    const API_BASE_URL = '/api/notifications';
    const POLL_INTERVAL = 60000; // 60 seconds
    
    // State
    let notificationDropdown = null;
    let notificationBadge = null;
    let notificationBell = null;
    let isDropdownOpen = false;
    let pollingInterval = null;

    // Initialize when DOM is ready
    document.addEventListener('DOMContentLoaded', function() {
        initializeNotificationBell();
    });

    function initializeNotificationBell() {
        // Find the notification bell elements
        const bellContainer = document.querySelector('.fa-bell')?.closest('.position-relative');
        if (!bellContainer) return;

        notificationBell = bellContainer.querySelector('.fa-bell');
        notificationBadge = bellContainer.querySelector('.position-absolute');
        
        // Update the HTML structure for better functionality
        bellContainer.innerHTML = `
            <a href="#" class="btn position-relative" id="notificationBellBtn">
                <i class="fas fa-bell"></i>
                <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger d-none" id="notificationBadge">
                    <span id="notificationCount">0</span>
                    <span class="visually-hidden">unread notifications</span>
                </span>
            </a>
            <div class="dropdown-menu dropdown-menu-end notification-dropdown d-none" id="notificationDropdown">
                <div class="dropdown-header d-flex justify-content-between align-items-center">
                    <h6 class="mb-0" data-translate-key="notifications.title">Notificaciones</h6>
                    <button class="btn btn-link btn-sm text-decoration-none" id="markAllReadBtn" data-translate-key="notifications.markAllAsRead">
                        Marcar todas como leídas
                    </button>
                </div>
                <div class="dropdown-divider"></div>
                <div id="notificationDropdownContent" class="notification-list">
                    <div class="text-center py-3">
                        <div class="spinner-border spinner-border-sm text-primary" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                    </div>
                </div>
                <div class="dropdown-divider"></div>
                <a href="/NotificationsMvc" class="dropdown-item text-center text-primary" data-translate-key="notifications.viewAll">
                    Ver todas las notificaciones
                </a>
            </div>
        `;

        // Re-get elements after updating HTML
        notificationBell = document.getElementById('notificationBellBtn');
        notificationBadge = document.getElementById('notificationBadge');
        notificationDropdown = document.getElementById('notificationDropdown');

        // Add click event to bell
        notificationBell.addEventListener('click', handleBellClick);
        
        // Add click event to mark all as read button
        const markAllBtn = document.getElementById('markAllReadBtn');
        if (markAllBtn) {
            markAllBtn.addEventListener('click', handleMarkAllAsRead);
        }

        // Close dropdown when clicking outside
        document.addEventListener('click', function(e) {
            if (!bellContainer.contains(e.target) && isDropdownOpen) {
                closeNotificationDropdown();
            }
        });

        // Load initial notification count
        updateNotificationCount();

        // Start polling for new notifications
        startPolling();
    }

    function handleBellClick(e) {
        e.preventDefault();
        e.stopPropagation();

        if (isDropdownOpen) {
            closeNotificationDropdown();
        } else {
            openNotificationDropdown();
        }
    }

    function openNotificationDropdown() {
        notificationDropdown.classList.remove('d-none');
        notificationDropdown.classList.add('show');
        isDropdownOpen = true;
        
        // Load latest notifications
        loadLatestNotifications();
    }

    function closeNotificationDropdown() {
        notificationDropdown.classList.add('d-none');
        notificationDropdown.classList.remove('show');
        isDropdownOpen = false;
    }

    async function updateNotificationCount() {
        try {
            const response = await fetch(`${API_BASE_URL}/unread-count`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json'
                }
            });

            if (response.ok) {
                const data = await response.json();
                const count = data.count || 0;
                
                const countElement = document.getElementById('notificationCount');
                if (countElement) {
                    countElement.textContent = count;
                }

                if (count > 0) {
                    notificationBadge.classList.remove('d-none');
                } else {
                    notificationBadge.classList.add('d-none');
                }
            }
        } catch (error) {
            console.error('Error updating notification count:', error);
        }
    }

    async function loadLatestNotifications() {
        const contentContainer = document.getElementById('notificationDropdownContent');
        
        try {
            const response = await fetch(`${API_BASE_URL}/latest-unread?count=5`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json'
                }
            });

            if (response.ok) {
                const data = await response.json();
                const notifications = data.notifications || [];
                
                if (notifications.length === 0) {
                    contentContainer.innerHTML = `
                        <div class="text-center py-4">
                            <i class="fas fa-bell-slash fa-2x text-muted mb-2"></i>
                            <p class="text-muted mb-0" data-translate-key="notifications.noUnread">No tienes notificaciones sin leer</p>
                        </div>
                    `;
                } else {
                    contentContainer.innerHTML = notifications.map(notification => 
                        createNotificationItem(notification)
                    ).join('');
                    
                    // Add click events to notification items
                    contentContainer.querySelectorAll('.notification-item').forEach(item => {
                        item.addEventListener('click', function() {
                            handleNotificationClick(this.dataset.notificationId);
                        });
                    });
                }
            } else {
                contentContainer.innerHTML = `
                    <div class="text-center py-3 text-danger">
                        <i class="fas fa-exclamation-triangle"></i>
                        <p class="mb-0">Error al cargar notificaciones</p>
                    </div>
                `;
            }
        } catch (error) {
            console.error('Error loading notifications:', error);
            contentContainer.innerHTML = `
                <div class="text-center py-3 text-danger">
                    <i class="fas fa-exclamation-triangle"></i>
                    <p class="mb-0">Error al cargar notificaciones</p>
                </div>
            `;
        }
    }

    function createNotificationItem(notification) {
        const message = formatNotificationMessage(notification.type, notification.data);
        const timeAgo = formatTimeAgo(notification.createdAt);
        const icon = getNotificationIcon(notification.type);
        
        return `
            <div class="notification-item dropdown-item px-3 py-2" data-notification-id="${notification.id}">
                <div class="d-flex align-items-start">
                    <div class="notification-icon ${icon.class} me-3">
                        <i class="${icon.icon}"></i>
                    </div>
                    <div class="flex-grow-1">
                        <p class="mb-1">${message}</p>
                        <small class="text-muted">${timeAgo}</small>
                    </div>
                </div>
            </div>
        `;
    }

    function formatNotificationMessage(type, dataJson) {
        try {
            const data = JSON.parse(dataJson);
            const lang = getCurrentLanguage();
            
            switch(type) {
                case 'session_scheduled_by_coach':
                    if (lang === 'es') {
                        return `${data.CoachName || 'Tu coach'} ha programado una nueva sesión para ti.`;
                    } else {
                        return `${data.CoachName || 'Your coach'} has scheduled a new session for you.`;
                    }
                    
                case 'calendar_event_scheduled_for_client':
                    if (lang === 'es') {
                        return `Nuevo evento del calendario: ${data.EventTitle || 'Sin título'}`;
                    } else {
                        return `New calendar event: ${data.EventTitle || 'Untitled'}`;
                    }
                    
                default:
                    if (lang === 'es') {
                        return 'Nueva notificación';
                    } else {
                        return 'New notification';
                    }
            }
        } catch (error) {
            console.error('Error parsing notification data:', error);
            return 'Nueva notificación';
        }
    }

    function getNotificationIcon(type) {
        switch(type) {
            case 'session_scheduled_by_coach':
                return { icon: 'fas fa-calendar-check', class: 'bg-primary text-white' };
            case 'calendar_event_scheduled_for_client':
                return { icon: 'fas fa-calendar-alt', class: 'bg-info text-white' };
            default:
                return { icon: 'fas fa-bell', class: 'bg-secondary text-white' };
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
            return lang === 'es' ? 'Justo ahora' : 'Just now';
        } else if (diffMins < 60) {
            return lang === 'es' ? `Hace ${diffMins} minuto${diffMins > 1 ? 's' : ''}` : `${diffMins} minute${diffMins > 1 ? 's' : ''} ago`;
        } else if (diffHours < 24) {
            return lang === 'es' ? `Hace ${diffHours} hora${diffHours > 1 ? 's' : ''}` : `${diffHours} hour${diffHours > 1 ? 's' : ''} ago`;
        } else if (diffDays < 7) {
            return lang === 'es' ? `Hace ${diffDays} día${diffDays > 1 ? 's' : ''}` : `${diffDays} day${diffDays > 1 ? 's' : ''} ago`;
        } else {
            return date.toLocaleDateString();
        }
    }

    function getCurrentLanguage() {
        // Get language from localStorage or default to 'es'
        return localStorage.getItem('selectedLanguage') || 'es';
    }

    async function handleNotificationClick(notificationId) {
        try {
            const response = await fetch(`${API_BASE_URL}/${notificationId}/mark-as-read`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'X-CSRF-TOKEN': document.querySelector('[name="__RequestVerificationToken"]')?.value
                }
            });

            if (response.ok) {
                // Update notification count
                updateNotificationCount();
                
                // TODO: Handle redirect if notification has a URL
                // For now, just close the dropdown
                closeNotificationDropdown();
            }
        } catch (error) {
            console.error('Error marking notification as read:', error);
        }
    }

    async function handleMarkAllAsRead(e) {
        e.preventDefault();
        e.stopPropagation();
        
        try {
            const response = await fetch(`${API_BASE_URL}/mark-all-as-read`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'X-CSRF-TOKEN': document.querySelector('[name="__RequestVerificationToken"]')?.value
                }
            });

            if (response.ok) {
                // Update notification count
                updateNotificationCount();
                
                // Reload notifications
                loadLatestNotifications();
            }
        } catch (error) {
            console.error('Error marking all notifications as read:', error);
        }
    }

    function startPolling() {
        // Poll for new notifications every minute
        pollingInterval = setInterval(() => {
            updateNotificationCount();
        }, POLL_INTERVAL);
    }

    function stopPolling() {
        if (pollingInterval) {
            clearInterval(pollingInterval);
            pollingInterval = null;
        }
    }

    // Clean up on page unload
    window.addEventListener('beforeunload', function() {
        stopPolling();
    });

    // Add custom CSS for notification dropdown
    const style = document.createElement('style');
    style.textContent = `
        .notification-dropdown {
            width: 350px;
            max-height: 400px;
            overflow-y: auto;
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
            border: 1px solid rgba(0, 0, 0, 0.15);
            border-radius: 0.375rem;
            position: absolute;
            top: 100%;
            right: 0;
            z-index: 1050;
            background: white;
            margin-top: 0.5rem;
        }
        
        .notification-dropdown .dropdown-header {
            padding: 0.75rem 1rem;
            background-color: #f8f9fa;
            border-bottom: 1px solid #dee2e6;
        }
        
        .notification-dropdown .notification-item {
            border-bottom: 1px solid #f1f1f1;
            cursor: pointer;
            transition: background-color 0.2s;
        }
        
        .notification-dropdown .notification-item:hover {
            background-color: #f8f9fa;
        }
        
        .notification-dropdown .notification-item:last-child {
            border-bottom: none;
        }
        
        .notification-icon {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-shrink: 0;
        }
        
        .notification-list {
            max-height: 300px;
            overflow-y: auto;
        }
        
        #notificationBadge {
            font-size: 0.75rem;
            padding: 0.25rem 0.5rem;
            min-width: 1.25rem;
        }
    `;
    document.head.appendChild(style);
})();