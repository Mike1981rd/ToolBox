// Notifications Page Functionality
(function() {
    'use strict';

    // Configuration
    const API_BASE_URL = '/api/notifications';
    const PAGE_SIZE = 10;
    
    // State
    let currentPage = 1;
    let totalNotifications = 0;
    let isLoading = false;

    // Initialize when DOM is ready
    document.addEventListener('DOMContentLoaded', function() {
        initializeNotificationsPage();
    });

    function initializeNotificationsPage() {
        // Load first page of notifications
        loadNotifications(1);

        // Add event listeners
        document.getElementById('markAllAsReadBtn')?.addEventListener('click', handleMarkAllAsRead);
        document.getElementById('prevPageBtn')?.addEventListener('click', handlePrevPage);
        document.getElementById('nextPageBtn')?.addEventListener('click', handleNextPage);
    }

    async function loadNotifications(page) {
        if (isLoading) return;
        
        isLoading = true;
        currentPage = page;
        
        const notificationsList = document.getElementById('notificationsList');
        const noNotificationsMessage = document.getElementById('noNotificationsMessage');
        const paginationContainer = document.getElementById('paginationContainer');
        
        // Show loading skeleton
        notificationsList.innerHTML = createLoadingSkeletons(5);
        noNotificationsMessage.classList.add('d-none');
        paginationContainer.classList.add('d-none');

        try {
            const response = await fetch(`${API_BASE_URL}?pageNumber=${page}&pageSize=${PAGE_SIZE}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json'
                }
            });

            if (response.ok) {
                const data = await response.json();
                const notifications = data.notifications || [];
                
                if (notifications.length === 0 && page === 1) {
                    // No notifications at all
                    notificationsList.innerHTML = '';
                    noNotificationsMessage.classList.remove('d-none');
                } else if (notifications.length === 0) {
                    // No more notifications on this page, go back to previous page
                    loadNotifications(currentPage - 1);
                } else {
                    // Display notifications
                    notificationsList.innerHTML = notifications.map(notification => 
                        createNotificationItem(notification)
                    ).join('');
                    
                    // Update pagination
                    updatePagination(notifications.length);
                    
                    // Add click events to notification items
                    notificationsList.querySelectorAll('.notification-item').forEach(item => {
                        item.addEventListener('click', function() {
                            handleNotificationClick(this.dataset.notificationId, this);
                        });
                    });
                }
            } else {
                showError('Error al cargar las notificaciones');
            }
        } catch (error) {
            console.error('Error loading notifications:', error);
            showError('Error al cargar las notificaciones');
        } finally {
            isLoading = false;
        }
    }

    function createLoadingSkeletons(count) {
        let skeletons = '';
        for (let i = 0; i < count; i++) {
            skeletons += `
                <div class="notification-skeleton">
                    <div class="d-flex align-items-center p-3">
                        <div class="loading-skeleton rounded-circle" style="width: 48px; height: 48px;"></div>
                        <div class="flex-grow-1 ms-3">
                            <div class="loading-skeleton rounded mb-2" style="height: 20px; width: 30%;"></div>
                            <div class="loading-skeleton rounded mb-2" style="height: 16px; width: 60%;"></div>
                            <div class="loading-skeleton rounded" style="height: 14px; width: 20%;"></div>
                        </div>
                    </div>
                </div>
            `;
        }
        return skeletons;
    }

    function createNotificationItem(notification) {
        const message = formatNotificationMessage(notification.type, notification.data);
        const timeAgo = formatTimeAgo(notification.createdAt);
        const icon = getNotificationIcon(notification.type);
        const isRead = notification.isRead;
        
        return `
            <div class="notification-item ${isRead ? 'read' : 'unread'}" data-notification-id="${notification.id}">
                <div class="d-flex align-items-start">
                    <div class="notification-icon ${icon.class}">
                        <i class="${icon.icon}"></i>
                    </div>
                    <div class="notification-content">
                        <div class="notification-title">${getNotificationTitle(notification.type)}</div>
                        <div class="notification-message">${message}</div>
                        <div class="notification-time">
                            <i class="far fa-clock me-1"></i>${timeAgo}
                            ${isRead ? `<span class="ms-2 text-success"><i class="fas fa-check-circle"></i> Leída</span>` : ''}
                        </div>
                    </div>
                </div>
            </div>
        `;
    }

    function getNotificationTitle(type) {
        const lang = getCurrentLanguage();
        
        switch(type) {
            case 'session_scheduled_by_coach':
                return lang === 'es' ? 'Nueva sesión programada' : 'New session scheduled';
            case 'calendar_event_scheduled_for_client':
                return lang === 'es' ? 'Nuevo evento en el calendario' : 'New calendar event';
            case 'calendar_event_invitation':
                return lang === 'es' ? 'Invitación a evento' : 'Event invitation';
            default:
                return lang === 'es' ? 'Notificación' : 'Notification';
        }
    }

    function formatNotificationMessage(type, dataJson) {
        try {
            const data = JSON.parse(dataJson);
            const lang = getCurrentLanguage();
            
            switch(type) {
                case 'session_scheduled_by_coach':
                    const sessionDate = new Date(data.SessionDateTime).toLocaleString();
                    if (lang === 'es') {
                        return `${data.CoachName || 'Tu coach'} ha programado una nueva sesión para el ${sessionDate}.`;
                    } else {
                        return `${data.CoachName || 'Your coach'} has scheduled a new session for ${sessionDate}.`;
                    }
                    
                case 'calendar_event_scheduled_for_client':
                    const eventDate = new Date(data.EventStartDate).toLocaleString();
                    if (lang === 'es') {
                        return `Evento "${data.EventTitle || 'Sin título'}" programado para el ${eventDate}${data.Location ? ` en ${data.Location}` : ''}.`;
                    } else {
                        return `Event "${data.EventTitle || 'Untitled'}" scheduled for ${eventDate}${data.Location ? ` at ${data.Location}` : ''}.`;
                    }
                    
                case 'calendar_event_invitation':
                    const inviteDate = new Date(data.EventStartDate).toLocaleString();
                    if (lang === 'es') {
                        return `${data.InvitedBy || 'Un coach'} te ha invitado al evento "${data.EventTitle || 'Sin título'}" el ${inviteDate}.`;
                    } else {
                        return `${data.InvitedBy || 'A coach'} has invited you to the event "${data.EventTitle || 'Untitled'}" on ${inviteDate}.`;
                    }
                    
                default:
                    return JSON.stringify(data);
            }
        } catch (error) {
            console.error('Error parsing notification data:', error);
            return 'Error al procesar la notificación';
        }
    }

    function getNotificationIcon(type) {
        switch(type) {
            case 'session_scheduled_by_coach':
                return { icon: 'fas fa-calendar-check', class: 'session' };
            case 'calendar_event_scheduled_for_client':
                return { icon: 'fas fa-calendar-alt', class: 'calendar' };
            case 'calendar_event_invitation':
                return { icon: 'fas fa-calendar-plus', class: 'calendar' };
            default:
                return { icon: 'fas fa-bell', class: 'default' };
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
        return localStorage.getItem('selectedLanguage') || 'es';
    }

    function updatePagination(notificationsCount) {
        const paginationContainer = document.getElementById('paginationContainer');
        const prevPageItem = document.getElementById('prevPageItem');
        const nextPageItem = document.getElementById('nextPageItem');
        const currentPageSpan = document.getElementById('currentPage');
        
        paginationContainer.classList.remove('d-none');
        currentPageSpan.textContent = currentPage;
        
        // Update prev button
        if (currentPage === 1) {
            prevPageItem.classList.add('disabled');
        } else {
            prevPageItem.classList.remove('disabled');
        }
        
        // Update next button
        if (notificationsCount < PAGE_SIZE) {
            nextPageItem.classList.add('disabled');
        } else {
            nextPageItem.classList.remove('disabled');
        }
    }

    function handlePrevPage(e) {
        e.preventDefault();
        if (currentPage > 1 && !isLoading) {
            loadNotifications(currentPage - 1);
        }
    }

    function handleNextPage(e) {
        e.preventDefault();
        if (!isLoading) {
            loadNotifications(currentPage + 1);
        }
    }

    async function handleNotificationClick(notificationId, element) {
        // If already read, do nothing
        if (element.classList.contains('read')) return;
        
        try {
            const response = await fetch(`${API_BASE_URL}/${notificationId}/mark-as-read`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'X-CSRF-TOKEN': getAntiForgeryToken()
                }
            });

            if (response.ok) {
                // Mark as read visually
                element.classList.remove('unread');
                element.classList.add('read');
                
                // Add read indicator
                const timeDiv = element.querySelector('.notification-time');
                if (timeDiv && !timeDiv.querySelector('.fa-check-circle')) {
                    timeDiv.innerHTML += ' <span class="ms-2 text-success"><i class="fas fa-check-circle"></i> Leída</span>';
                }
                
                // Update notification count in bell
                if (window.updateNotificationCount) {
                    window.updateNotificationCount();
                }
            }
        } catch (error) {
            console.error('Error marking notification as read:', error);
        }
    }

    async function handleMarkAllAsRead(e) {
        e.preventDefault();
        
        if (!confirm('¿Estás seguro de que deseas marcar todas las notificaciones como leídas?')) {
            return;
        }
        
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
                // Reload notifications
                loadNotifications(currentPage);
                
                // Update notification count in bell
                if (window.updateNotificationCount) {
                    window.updateNotificationCount();
                }
                
                // Show success message
                showSuccess('Todas las notificaciones han sido marcadas como leídas');
            }
        } catch (error) {
            console.error('Error marking all notifications as read:', error);
            showError('Error al marcar las notificaciones como leídas');
        }
    }

    function getAntiForgeryToken() {
        // Try multiple ways to get the token
        return document.querySelector('[name="__RequestVerificationToken"]')?.value ||
               document.querySelector('input[name="__RequestVerificationToken"]')?.value ||
               document.querySelector('meta[name="csrf-token"]')?.content ||
               '';
    }

    function showError(message) {
        const notificationsList = document.getElementById('notificationsList');
        notificationsList.innerHTML = `
            <div class="alert alert-danger m-3" role="alert">
                <i class="fas fa-exclamation-triangle me-2"></i>${message}
            </div>
        `;
    }

    function showSuccess(message) {
        // Create a toast notification
        const toast = document.createElement('div');
        toast.className = 'position-fixed top-0 end-0 p-3';
        toast.style.zIndex = '1050';
        toast.innerHTML = `
            <div class="toast show" role="alert">
                <div class="toast-header bg-success text-white">
                    <i class="fas fa-check-circle me-2"></i>
                    <strong class="me-auto">Éxito</strong>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="toast"></button>
                </div>
                <div class="toast-body">
                    ${message}
                </div>
            </div>
        `;
        
        document.body.appendChild(toast);
        
        // Remove after 3 seconds
        setTimeout(() => {
            toast.remove();
        }, 3000);
    }

    // Export function for bell to use
    window.updateNotificationCount = function() {
        // This will be called by the bell script to update count
        // The bell script handles this
    };
})();