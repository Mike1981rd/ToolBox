/**
 * Website Settings JavaScript
 * Handles logo preview, form submission, and dynamic updates
 */

const WebsiteSettings = {
    // Initialize the module
    init: function() {
        this.setupEventListeners();
        this.initializeFormValidation();
    },

    // Setup all event listeners
    setupEventListeners: function() {
        // Logo file input change
        const logoInput = document.getElementById('appLogo');
        if (logoInput) {
            logoInput.addEventListener('change', (e) => {
                this.handleLogoChange(e.target.files[0]);
            });
        }

        // Reset logo button
        const resetLogoBtn = document.getElementById('resetLogoBtn');
        if (resetLogoBtn) {
            resetLogoBtn.addEventListener('click', () => {
                this.resetLogo();
            });
        }

        // Form submission
        const settingsForm = document.getElementById('websiteSettingsForm');
        if (settingsForm) {
            settingsForm.addEventListener('submit', (e) => {
                e.preventDefault();
                this.saveSettings();
            });
        }

        // Social media URL validation on blur
        this.setupSocialMediaValidation();
    },

    // Handle logo file change
    handleLogoChange: function(file) {
        if (!file) return;

        // Validate file type
        const allowedTypes = ['image/png', 'image/jpg', 'image/jpeg', 'image/svg+xml', 'image/gif'];
        if (!allowedTypes.includes(file.type)) {
            this.showError(this.getTranslation('messages.invalidFileType'));
            this.clearLogoInput();
            return;
        }

        // Validate file size (5MB max)
        const maxSize = 5 * 1024 * 1024; // 5MB
        if (file.size > maxSize) {
            this.showError(this.getTranslation('messages.fileTooLarge'));
            this.clearLogoInput();
            return;
        }

        // Read file and update preview
        const reader = new FileReader();
        reader.onload = (e) => {
            const imageDataUrl = e.target.result;
            this.updateLogoPreview(imageDataUrl);
            this.updateSidebarLogo(imageDataUrl);
        };
        reader.readAsDataURL(file);
    },

    // Update logo preview in the settings page
    updateLogoPreview: function(imageSrc) {
        const logoPreview = document.getElementById('logoPreview');
        if (logoPreview) {
            logoPreview.src = imageSrc;
            logoPreview.style.display = 'block';
        }
    },

    // Update logo in the sidebar
    updateSidebarLogo: function(imageSrc) {
        const sidebarLogo = document.getElementById('sidebarLogo');
        if (sidebarLogo) {
            sidebarLogo.src = imageSrc;
            // Add a subtle animation to indicate the change
            sidebarLogo.style.transition = 'opacity 0.3s ease-in-out';
            sidebarLogo.style.opacity = '0.7';
            setTimeout(() => {
                sidebarLogo.style.opacity = '1';
            }, 150);
        }
    },

    // Reset logo to default
    resetLogo: function() {
        const defaultLogoPath = '/img/toolbox-logo.svg';
        
        // Update preview
        this.updateLogoPreview(defaultLogoPath);
        
        // Update sidebar
        this.updateSidebarLogo(defaultLogoPath);
        
        // Clear file input
        this.clearLogoInput();
        
        // Show success message
        this.showSuccess(this.getTranslation('messages.logoReset'));
    },

    // Clear logo file input
    clearLogoInput: function() {
        const logoInput = document.getElementById('appLogo');
        if (logoInput) {
            logoInput.value = '';
        }
    },

    // Setup social media URL validation
    setupSocialMediaValidation: function() {
        const socialInputs = [
            'facebookurl', 'twitterurl', 'googleurl', 'linkedinurl', 
            'youtubeurl', 'instagramurl', 'telegramurl', 'tiktokurl', 
            'discordurl', 'redditurl'
        ];

        socialInputs.forEach(inputId => {
            const input = document.getElementById(inputId);
            if (input) {
                input.addEventListener('blur', (e) => {
                    this.validateSocialMediaUrl(e.target);
                });
            }
        });
    },

    // Validate social media URL format
    validateSocialMediaUrl: function(input) {
        const url = input.value.trim();
        if (!url) return; // Empty is valid

        // Basic URL validation
        try {
            new URL(url);
            input.classList.remove('is-invalid');
            input.classList.add('is-valid');
        } catch {
            input.classList.remove('is-valid');
            input.classList.add('is-invalid');
        }
    },

    // Initialize form validation
    initializeFormValidation: function() {
        // Email validation
        const emailInput = document.getElementById('siteEmail');
        if (emailInput) {
            emailInput.addEventListener('blur', (e) => {
                this.validateEmail(e.target);
            });
        }

        // Phone validation
        const phoneInput = document.getElementById('sitePhone');
        if (phoneInput) {
            phoneInput.addEventListener('blur', (e) => {
                this.validatePhone(e.target);
            });
        }
    },

    // Validate email format
    validateEmail: function(input) {
        const email = input.value.trim();
        if (!email) return;

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (emailRegex.test(email)) {
            input.classList.remove('is-invalid');
            input.classList.add('is-valid');
        } else {
            input.classList.remove('is-valid');
            input.classList.add('is-invalid');
        }
    },

    // Validate phone format
    validatePhone: function(input) {
        const phone = input.value.trim();
        if (!phone) return;

        // Basic phone validation (allows various formats)
        const phoneRegex = /^[\+]?[1-9][\d]{0,15}$/;
        const cleanPhone = phone.replace(/[\s\-\(\)]/g, '');
        
        if (phoneRegex.test(cleanPhone)) {
            input.classList.remove('is-invalid');
            input.classList.add('is-valid');
        } else {
            input.classList.remove('is-valid');
            input.classList.add('is-invalid');
        }
    },

    // Save settings
    saveSettings: function() {
        const form = document.getElementById('websiteSettingsForm');
        if (!form) return;

        // Show loading state
        const submitBtn = document.getElementById('saveSettingsBtn');
        const originalHtml = submitBtn.innerHTML;
        submitBtn.disabled = true;
        submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-1"></i> Saving...';

        // Prepare form data
        const formData = new FormData(form);
        
        // Add logo file if selected
        const logoInput = document.getElementById('appLogo');
        if (logoInput && logoInput.files.length > 0) {
            formData.append('appLogo', logoInput.files[0]);
        }

        // Make AJAX request
        fetch('/WebsiteSettings/SaveSettings', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                this.showSuccess(this.getTranslation('messages.settingsSaved'));
                
                // Update logo path if provided
                if (data.logoPath) {
                    this.updateLogoPreview(data.logoPath);
                    this.updateSidebarLogo(data.logoPath);
                }
                
                // Clear file input after successful upload
                this.clearLogoInput();
            } else {
                this.showError(data.message || 'Error saving settings');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            this.showError('Error saving settings');
        })
        .finally(() => {
            // Reset button state
            submitBtn.disabled = false;
            submitBtn.innerHTML = originalHtml;
        });
    },

    // Utility functions
    getTranslation: function(key) {
        if (typeof translations !== 'undefined') {
            const currentLang = localStorage.getItem('language') || 'en';
            return translations[currentLang] && translations[currentLang][key];
        }
        return null;
    },

    showSuccess: function(message) {
        // Simple alert for now - in a real app, you might use toast notifications
        alert('Success: ' + message);
    },

    showError: function(message) {
        // Simple alert for now - in a real app, you might use toast notifications
        alert('Error: ' + message);
    },

    // Additional utility: Format file size
    formatFileSize: function(bytes) {
        if (bytes === 0) return '0 Bytes';
        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(k));
        return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
    },

    // Validate all form fields
    validateForm: function() {
        let isValid = true;
        
        // Validate email
        const emailInput = document.getElementById('siteEmail');
        if (emailInput && emailInput.value.trim()) {
            this.validateEmail(emailInput);
            if (emailInput.classList.contains('is-invalid')) {
                isValid = false;
            }
        }

        // Validate phone
        const phoneInput = document.getElementById('sitePhone');
        if (phoneInput && phoneInput.value.trim()) {
            this.validatePhone(phoneInput);
            if (phoneInput.classList.contains('is-invalid')) {
                isValid = false;
            }
        }

        // Validate social media URLs
        const socialInputs = document.querySelectorAll('input[type="url"]');
        socialInputs.forEach(input => {
            if (input.value.trim()) {
                this.validateSocialMediaUrl(input);
                if (input.classList.contains('is-invalid')) {
                    isValid = false;
                }
            }
        });

        return isValid;
    }
};

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    WebsiteSettings.init();
});

// Export for global access
window.WebsiteSettings = WebsiteSettings;