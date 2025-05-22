// Email Contents Module JavaScript
// Author: Claude
// Description: Handles all email interface functionality including compose, list management, and email actions

const EmailContents = {
    // Configuration
    config: {
        baseUrl: '/EmailContents',
        currentFolder: 'inbox',
        selectedEmails: [],
        quillEditor: null,
        translations: window.translations || {}
    },

    // Initialize the module
    init: function() {
        console.log('Initializing Email Contents module...');
        
        // Check if emails are already rendered
        const emailCount = $('.email-item').length;
        console.log(`Found ${emailCount} emails already rendered in the DOM`);
        
        this.bindEvents();
        this.initializeQuillEditor();
        this.loadEmailList();
        this.setupTooltips();
        console.log('Email Contents module initialized successfully');
    },

    // Bind all event handlers
    bindEvents: function() {
        // Folder navigation
        $(document).on('click', '.folder-item', this.handleFolderClick.bind(this));
        
        // Label filtering
        $(document).on('click', '.label-item', this.handleLabelClick.bind(this));
        
        // Email selection
        $(document).on('change', '#selectAllEmails', this.handleSelectAll.bind(this));
        $(document).on('change', '.email-checkbox', this.handleEmailSelection.bind(this));
        
        // Email actions
        $(document).on('click', '.email-item', this.handleEmailClick.bind(this));
        $(document).on('click', '.star-btn', this.handleStarClick.bind(this));
        
        // Toolbar actions
        $('#refreshBtn').on('click', this.refreshEmailList.bind(this));
        $('#deleteBtn').on('click', this.deleteSelectedEmails.bind(this));
        $('#markUnreadBtn').on('click', this.markSelectedAsUnread.bind(this));
        
        // Move to folder
        $(document).on('click', '.move-to-folder', this.handleMoveToFolder.bind(this));
        
        // Apply label
        $(document).on('click', '.apply-label', this.handleApplyLabel.bind(this));
        
        // Compose modal events
        $('#composeModal').on('show.bs.modal', this.initializeComposeModal.bind(this));
        $('#composeModal').on('hidden.bs.modal', this.resetComposeModal.bind(this));
        
        // Compose form events
        $('#sendEmailBtn').on('click', this.sendEmail.bind(this));
        $('#saveDraftBtn').on('click', this.saveDraft.bind(this));
        $('#attachFileBtn').on('click', this.handleAttachFile.bind(this));
        $('#composeAttachments').on('change', this.handleFileSelection.bind(this));
        
        // CC/BCC toggle
        $('#showCcBtn').on('click', function() { $('#ccField').toggle(); });
        $('#showBccBtn').on('click', function() { $('#bccField').toggle(); });
        
        // Search
        $('#emailSearchInput').on('input', this.debounce(this.handleSearch.bind(this), 300));
    },

    // Initialize Quill rich text editor
    initializeQuillEditor: function() {
        if (typeof Quill !== 'undefined') {
            this.config.quillEditor = new Quill('#composeBodyEditor', {
                theme: 'snow',
                placeholder: this.translate('email.messagePlaceholder') || 'Escriba su mensaje aquí...',
                modules: {
                    toolbar: [
                        [{ 'header': [1, 2, false] }],
                        ['bold', 'italic', 'underline', 'strike'],
                        [{ 'color': [] }, { 'background': [] }],
                        [{ 'list': 'ordered'}, { 'list': 'bullet' }],
                        [{ 'align': [] }],
                        ['link', 'image'],
                        ['clean']
                    ]
                }
            });
        } else {
            // Fallback to simple textarea if Quill is not available
            $('#composeBodyEditor').html('<textarea class="form-control" rows="10" placeholder="' + 
                (this.translate('email.messagePlaceholder') || 'Escriba su mensaje aquí...') + '"></textarea>');
        }
    },

    // Handle folder navigation
    handleFolderClick: function(e) {
        e.preventDefault();
        const folder = $(e.currentTarget).data('folder');
        
        // Update active folder
        $('.folder-item').removeClass('active');
        $(e.currentTarget).addClass('active');
        
        this.config.currentFolder = folder;
        this.loadEmailList();
    },

    // Handle label filtering
    handleLabelClick: function(e) {
        e.preventDefault();
        const label = $(e.currentTarget).data('label');
        this.filterByLabel(label);
    },

    // Handle select all checkbox
    handleSelectAll: function(e) {
        const isChecked = $(e.target).is(':checked');
        $('.email-checkbox').prop('checked', isChecked);
        this.updateSelectedEmails();
    },

    // Handle individual email selection
    handleEmailSelection: function(e) {
        this.updateSelectedEmails();
        
        // Update select all checkbox state
        const totalEmails = $('.email-checkbox').length;
        const selectedEmails = $('.email-checkbox:checked').length;
        
        if (selectedEmails === 0) {
            $('#selectAllEmails').prop('indeterminate', false).prop('checked', false);
        } else if (selectedEmails === totalEmails) {
            $('#selectAllEmails').prop('indeterminate', false).prop('checked', true);
        } else {
            $('#selectAllEmails').prop('indeterminate', true);
        }
    },

    // Update selected emails array
    updateSelectedEmails: function() {
        this.config.selectedEmails = [];
        $('.email-checkbox:checked').each((index, element) => {
            this.config.selectedEmails.push(parseInt($(element).val()));
        });
        
        // Enable/disable toolbar buttons based on selection
        const hasSelection = this.config.selectedEmails.length > 0;
        $('#deleteBtn, #markUnreadBtn').prop('disabled', !hasSelection);
    },

    // Handle email click to view details
    handleEmailClick: function(e) {
        // Don't trigger if clicking on checkbox, star, or other interactive elements
        if ($(e.target).is('input, button, .star-btn, .star-btn *')) {
            return;
        }
        
        const emailId = $(e.currentTarget).data('email-id');
        this.loadEmailDetail(emailId);
        
        // Mark email as read
        $(e.currentTarget).removeClass('unread');
        this.markAsRead(emailId);
    },

    // Handle star/unstar email
    handleStarClick: function(e) {
        e.stopPropagation();
        const $btn = $(e.currentTarget);
        const emailId = $btn.data('email-id');
        const isStarred = $btn.hasClass('starred');
        
        this.toggleStar(emailId, !isStarred);
        $btn.toggleClass('starred');
    },

    // Load email list for current folder
    loadEmailList: function() {
        // Since emails are already rendered on the server, we just need to simulate a successful load
        console.log(`Loaded emails for folder: ${this.config.currentFolder}`);
        
        // Update UI elements that depend on the current folder
        this.updateFolderSelection();
        this.updateEmailCounts();
    },

    // Update folder selection in UI
    updateFolderSelection: function() {
        $('.folder-item').removeClass('active');
        $(`.folder-item[data-folder="${this.config.currentFolder}"]`).addClass('active');
    },

    // Update email counts and other UI elements
    updateEmailCounts: function() {
        const totalEmails = $('.email-item').length;
        const unreadEmails = $('.email-item.unread').length;
        
        // Update any counters if they exist
        console.log(`Total emails: ${totalEmails}, Unread: ${unreadEmails}`);
    },

    // Load email detail view
    loadEmailDetail: function(emailId) {
        this.showLoading('#emailDetailContent');
        
        $.get(`${this.config.baseUrl}/GetEmailDetail?emailId=${emailId}`)
            .done((response) => {
                if (response.success) {
                    this.renderEmailDetail(response.data);
                    $('#emailDetailView').show();
                } else {
                    this.showNotification(response.message || this.translate('email.errorLoadingDetail'), 'error');
                }
            })
            .fail((xhr) => {
                this.hideLoading('#emailDetailContent');
                this.showNotification(this.translate('email.errorLoadingDetail') || 'Error al cargar el detalle del correo', 'error');
            });
    },

    // Render email detail in the detail view
    renderEmailDetail: function(email) {
        const attachmentsHtml = email.attachments && email.attachments.length > 0 
            ? email.attachments.map(att => `
                <div class="attachment-item d-flex align-items-center justify-content-between mb-2">
                    <div class="attachment-info">
                        <i class="fas fa-paperclip me-2"></i>
                        <span>${att.fileName}</span>
                        <small class="text-muted ms-2">(${att.fileSize})</small>
                    </div>
                    <button class="btn btn-outline-primary btn-sm">
                        <i class="fas fa-download"></i>
                    </button>
                </div>
            `).join('')
            : '';

        const detailHtml = `
            <div class="email-detail">
                <div class="email-header mb-4">
                    <div class="d-flex justify-content-between align-items-start mb-3">
                        <h4 class="mb-0">${email.subject}</h4>
                        <button class="btn btn-outline-secondary btn-sm" id="closeDetailBtn">
                            <i class="fas fa-times"></i>
                        </button>
                    </div>
                    
                    <div class="d-flex align-items-center mb-3">
                        <img src="${email.senderAvatar}" alt="${email.senderName}" class="rounded-circle me-3" width="48" height="48">
                        <div class="flex-grow-1">
                            <div class="fw-bold">${email.senderName}</div>
                            <div class="text-muted small">${email.senderEmail}</div>
                            <div class="text-muted small">${new Date(email.date).toLocaleString()}</div>
                        </div>
                        <div class="email-actions">
                            <button class="btn btn-outline-secondary btn-sm me-1" title="${this.translate('email.reply')}">
                                <i class="fas fa-reply"></i>
                            </button>
                            <button class="btn btn-outline-secondary btn-sm me-1" title="${this.translate('email.replyAll')}">
                                <i class="fas fa-reply-all"></i>
                            </button>
                            <button class="btn btn-outline-secondary btn-sm me-1" title="${this.translate('email.forward')}">
                                <i class="fas fa-share"></i>
                            </button>
                            <button class="btn btn-outline-secondary btn-sm" title="${this.translate('email.delete')}">
                                <i class="fas fa-trash"></i>
                            </button>
                        </div>
                    </div>
                </div>
                
                <div class="email-body mb-4">
                    ${email.fullBody}
                </div>
                
                ${attachmentsHtml ? `
                    <div class="email-attachments">
                        <h6 class="mb-3">${this.translate('email.attachments') || 'Adjuntos'}</h6>
                        ${attachmentsHtml}
                    </div>
                ` : ''}
            </div>
        `;

        $('#emailDetailContent').html(detailHtml);
        this.hideLoading('#emailDetailContent');
        
        // Bind close button
        $('#closeDetailBtn').on('click', () => {
            $('#emailDetailView').hide();
        });
    },

    // Refresh email list
    refreshEmailList: function() {
        // Simulate refresh by updating the folder selection and counts
        this.loadEmailList();
        this.showNotification(this.translate('email.refreshed') || 'Lista actualizada', 'success');
    },

    // Delete selected emails
    deleteSelectedEmails: function() {
        if (this.config.selectedEmails.length === 0) {
            this.showNotification(this.translate('email.noEmailsSelected') || 'No hay correos seleccionados', 'warning');
            return;
        }

        if (!confirm(this.translate('email.confirmDelete') || '¿Está seguro de que desea eliminar los correos seleccionados?')) {
            return;
        }

        $.post(`${this.config.baseUrl}/DeleteEmails`, JSON.stringify(this.config.selectedEmails), 'application/json')
            .done((response) => {
                if (response.success) {
                    // Remove deleted emails from UI
                    this.config.selectedEmails.forEach(emailId => {
                        $(`.email-item[data-email-id="${emailId}"]`).fadeOut(300, function() {
                            $(this).remove();
                        });
                    });
                    
                    this.config.selectedEmails = [];
                    $('#selectAllEmails').prop('checked', false);
                    this.showNotification(response.message, 'success');
                } else {
                    this.showNotification(response.message, 'error');
                }
            })
            .fail(() => {
                this.showNotification(this.translate('email.errorDeleting') || 'Error al eliminar correos', 'error');
            });
    },

    // Mark selected emails as unread
    markSelectedAsUnread: function() {
        if (this.config.selectedEmails.length === 0) {
            this.showNotification(this.translate('email.noEmailsSelected') || 'No hay correos seleccionados', 'warning');
            return;
        }

        this.config.selectedEmails.forEach(emailId => {
            $.post(`${this.config.baseUrl}/MarkAsRead`, { emailId: emailId, isRead: false })
                .done((response) => {
                    if (response.success) {
                        $(`.email-item[data-email-id="${emailId}"]`).addClass('unread');
                    }
                });
        });

        this.showNotification(this.translate('email.markedAsUnread') || 'Marcados como no leídos', 'success');
    },

    // Move emails to folder
    handleMoveToFolder: function(e) {
        e.preventDefault();
        const targetFolder = $(e.currentTarget).data('folder');
        
        if (this.config.selectedEmails.length === 0) {
            this.showNotification(this.translate('email.noEmailsSelected') || 'No hay correos seleccionados', 'warning');
            return;
        }

        const data = {
            emailIds: this.config.selectedEmails,
            targetFolder: targetFolder
        };

        $.post(`${this.config.baseUrl}/MoveToFolder`, JSON.stringify(data), 'application/json')
            .done((response) => {
                if (response.success) {
                    // Remove moved emails from current view
                    this.config.selectedEmails.forEach(emailId => {
                        $(`.email-item[data-email-id="${emailId}"]`).fadeOut(300, function() {
                            $(this).remove();
                        });
                    });
                    
                    this.config.selectedEmails = [];
                    this.showNotification(response.message, 'success');
                } else {
                    this.showNotification(response.message, 'error');
                }
            })
            .fail(() => {
                this.showNotification(this.translate('email.errorMoving') || 'Error al mover correos', 'error');
            });
    },

    // Apply label to emails
    handleApplyLabel: function(e) {
        e.preventDefault();
        const label = $(e.currentTarget).data('label');
        
        if (this.config.selectedEmails.length === 0) {
            this.showNotification(this.translate('email.noEmailsSelected') || 'No hay correos seleccionados', 'warning');
            return;
        }

        const data = {
            emailIds: this.config.selectedEmails,
            label: label
        };

        $.post(`${this.config.baseUrl}/ApplyLabel`, JSON.stringify(data), 'application/json')
            .done((response) => {
                if (response.success) {
                    this.showNotification(response.message, 'success');
                    // In a real implementation, update the UI to show the new labels
                } else {
                    this.showNotification(response.message, 'error');
                }
            })
            .fail(() => {
                this.showNotification(this.translate('email.errorApplyingLabel') || 'Error al aplicar etiqueta', 'error');
            });
    },

    // Initialize compose modal
    initializeComposeModal: function() {
        // Reset form
        $('#composeEmailForm')[0].reset();
        $('#ccField, #bccField').hide();
        $('#attachmentsList').empty();
        
        // Clear editor content
        if (this.config.quillEditor) {
            this.config.quillEditor.setContents([]);
        }
        
        // Remove validation classes
        $('#composeEmailForm .is-invalid').removeClass('is-invalid');
    },

    // Reset compose modal
    resetComposeModal: function() {
        this.initializeComposeModal();
    },

    // Send email
    sendEmail: function() {
        if (!this.validateComposeForm()) {
            return;
        }

        const formData = this.getComposeFormData();
        
        $.post(`${this.config.baseUrl}/SendEmail`, JSON.stringify(formData), 'application/json')
            .done((response) => {
                if (response.success) {
                    $('#composeModal').modal('hide');
                    this.showNotification(response.message, 'success');
                    
                    if (this.config.currentFolder === 'sent') {
                        this.loadEmailList();
                    }
                } else {
                    this.showNotification(response.message, 'error');
                }
            })
            .fail(() => {
                this.showNotification(this.translate('email.errorSending') || 'Error al enviar correo', 'error');
            });
    },

    // Save as draft
    saveDraft: function() {
        if (!this.validateComposeForm(false)) {
            return;
        }

        const formData = this.getComposeFormData();
        formData.saveAsDraft = true;
        
        $.post(`${this.config.baseUrl}/SendEmail`, JSON.stringify(formData), 'application/json')
            .done((response) => {
                if (response.success) {
                    this.showNotification(response.message, 'success');
                } else {
                    this.showNotification(response.message, 'error');
                }
            })
            .fail(() => {
                this.showNotification(this.translate('email.errorSavingDraft') || 'Error al guardar borrador', 'error');
            });
    },

    // Validate compose form
    validateComposeForm: function(requireAll = true) {
        let isValid = true;
        
        // Clear previous validation
        $('#composeEmailForm .is-invalid').removeClass('is-invalid');
        
        // Validate required fields
        const to = $('#composeTo').val().trim();
        const subject = $('#composeSubject').val().trim();
        
        if (requireAll && !to) {
            $('#composeTo').addClass('is-invalid');
            isValid = false;
        }
        
        if (requireAll && !subject) {
            $('#composeSubject').addClass('is-invalid');
            isValid = false;
        }
        
        // Validate email format
        if (to && !this.isValidEmail(to)) {
            $('#composeTo').addClass('is-invalid');
            isValid = false;
        }
        
        return isValid;
    },

    // Get compose form data
    getComposeFormData: function() {
        const labels = [];
        $('#composeEmailForm input[name="Labels"]:checked').each(function() {
            labels.push($(this).val());
        });

        return {
            to: $('#composeTo').val().trim(),
            cc: $('#composeCc').val().trim(),
            bcc: $('#composeBcc').val().trim(),
            subject: $('#composeSubject').val().trim(),
            body: this.config.quillEditor ? this.config.quillEditor.root.innerHTML : $('#composeBody').val(),
            labels: labels
        };
    },

    // Handle file attachment
    handleAttachFile: function() {
        $('#composeAttachments').click();
    },

    // Handle file selection
    handleFileSelection: function(e) {
        const files = e.target.files;
        for (let i = 0; i < files.length; i++) {
            this.addAttachment(files[i]);
        }
    },

    // Add attachment to list
    addAttachment: function(file) {
        const attachmentHtml = `
            <div class="attachment-item" data-filename="${file.name}">
                <div class="attachment-info">
                    <i class="fas fa-paperclip"></i>
                    <span>${file.name}</span>
                    <small class="text-muted ms-2">(${this.formatFileSize(file.size)})</small>
                </div>
                <button type="button" class="attachment-remove">
                    <i class="fas fa-times"></i>
                </button>
            </div>
        `;
        
        $('#attachmentsList').append(attachmentHtml);
        
        // Bind remove button
        $('#attachmentsList').find('.attachment-remove').last().on('click', function() {
            $(this).closest('.attachment-item').remove();
        });
    },

    // Handle search
    handleSearch: function() {
        const query = $('#emailSearchInput').val().toLowerCase();
        
        $('.email-item').each(function() {
            const emailText = $(this).text().toLowerCase();
            const isMatch = emailText.includes(query);
            $(this).toggle(isMatch);
        });
    },

    // Filter by label
    filterByLabel: function(label) {
        $('.email-item').each(function() {
            const emailLabels = $(this).find('.email-label').map(function() {
                return $(this).text().toLowerCase();
            }).get();
            
            const hasLabel = emailLabels.includes(label.toLowerCase());
            $(this).toggle(hasLabel);
        });
    },

    // Mark email as read
    markAsRead: function(emailId) {
        $.post(`${this.config.baseUrl}/MarkAsRead`, { emailId: emailId, isRead: true });
    },

    // Toggle star status
    toggleStar: function(emailId, isStarred) {
        $.post(`${this.config.baseUrl}/StarEmail`, { emailId: emailId, isStarred: isStarred });
    },

    // Utility functions
    translate: function(key) {
        return this.config.translations[key] || key;
    },

    isValidEmail: function(email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    },

    formatFileSize: function(bytes) {
        if (bytes === 0) return '0 Bytes';
        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(k));
        return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
    },

    debounce: function(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func.apply(this, args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    },

    showLoading: function(selector) {
        // Only show loading for email detail content, not the main email list
        if (selector === '#emailDetailContent') {
            $(selector).html('<div class="text-center p-4"><i class="fas fa-spinner fa-spin"></i> Cargando...</div>');
        }
    },

    hideLoading: function(selector) {
        // This would be handled by the actual content replacement
        // For email list, we don't need to do anything since emails are server-rendered
    },

    showNotification: function(message, type = 'info') {
        // Create notification element
        const alertClass = `alert-${type === 'error' ? 'danger' : type}`;
        const notification = $(`
            <div class="alert ${alertClass} alert-dismissible fade show position-fixed" style="top: 20px; right: 20px; z-index: 9999;">
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `);
        
        $('body').append(notification);
        
        // Auto remove after 5 seconds
        setTimeout(() => {
            notification.alert('close');
        }, 5000);
    },

    setupTooltips: function() {
        // Initialize Bootstrap tooltips
        if (typeof bootstrap !== 'undefined') {
            const tooltipTriggerList = [].slice.call(document.querySelectorAll('[title]'));
            tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl);
            });
        }
    }
};

// Initialize when document is ready
$(document).ready(function() {
    EmailContents.init();
});