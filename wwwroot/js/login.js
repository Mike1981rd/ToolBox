/**
 * Login Page JavaScript
 * Handles login functionality, dynamic logo loading, and form interactions
 */

document.addEventListener('DOMContentLoaded', function() {
    // Elements
    const loginForm = document.getElementById('loginForm');
    const loginButton = document.getElementById('loginButton');
    const loginSpinner = document.getElementById('loginSpinner');
    const togglePassword = document.getElementById('togglePassword');
    const passwordInput = document.querySelector('input[name="Password"]');
    const passwordIcon = document.getElementById('passwordIcon');
    const errorMessage = document.getElementById('errorMessage');
    const errorText = document.getElementById('errorText');
    const loginLogo = document.getElementById('loginLogo');
    const siteTitle = document.getElementById('siteTitle');
    const welcomeMessage = document.getElementById('welcomeMessage');

    // Initialize page
    initializePage();
    
    // Hide empty validation summary
    hideEmptyValidationSummary();
    
    // Remove email validation to allow username
    removeEmailValidation();

    // Event listeners
    if (togglePassword && passwordInput) {
        togglePassword.addEventListener('click', handlePasswordToggle);
    }

    if (loginForm) {
        loginForm.addEventListener('submit', handleLoginSubmit);
    }

    // Functions
    
    /**
     * Remove email validation to allow username
     */
    function removeEmailValidation() {
        // Ya no es necesario porque el campo se llama EmailOrUsername
        // Esta función se mantiene para compatibilidad
    }

    /**
     * Hide empty validation summary
     */
    function hideEmptyValidationSummary() {
        const validationSummary = document.querySelector('.validation-summary');
        if (validationSummary) {
            // Check if it's empty or only contains an empty list
            const ul = validationSummary.querySelector('ul');
            if (!ul || ul.children.length === 0) {
                validationSummary.style.display = 'none';
            }
            
            // Also set up a MutationObserver to watch for changes
            const observer = new MutationObserver(function(mutations) {
                const ul = validationSummary.querySelector('ul');
                if (!ul || ul.children.length === 0) {
                    validationSummary.style.display = 'none';
                } else {
                    validationSummary.style.display = 'block';
                }
            });
            
            observer.observe(validationSummary, { childList: true, subtree: true });
        }
    }

    /**
     * Initialize the login page by loading site configuration
     */
    async function initializePage() {
        try {
            await loadSiteConfiguration();
            addFadeInAnimation();
        } catch (error) {
            console.error('Error initializing login page:', error);
            // Continue with default values if configuration fails
        }
    }

    /**
     * Load site configuration (logo, title, welcome message)
     */
    async function loadSiteConfiguration() {
        try {
            const response = await fetch('/api/site-configuration', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if (response.ok) {
                const config = await response.json();
                updateSiteConfiguration(config);
            } else {
                console.warn('Could not load site configuration, using defaults');
            }
        } catch (error) {
            console.error('Error loading site configuration:', error);
            // Fallback to defaults
        }
    }

    /**
     * Update the page with site configuration data
     */
    function updateSiteConfiguration(config) {
        // DISABLED: Logo update on login page to maintain consistent branding
        // The login page should always show the static logo at /img/logo.png
        // If you need to re-enable dynamic logo, uncomment the following lines:
        /*
        if (config.logoUrl && loginLogo) {
            loginLogo.src = config.logoUrl;
            loginLogo.alt = config.siteName || 'Site Logo';
        }
        */

        // Update site title
        if (config.siteName && siteTitle) {
            siteTitle.textContent = config.siteName;
        }

        // Update welcome message
        if (config.welcomeMessage && welcomeMessage) {
            welcomeMessage.textContent = config.welcomeMessage;
        } else if (config.siteName && welcomeMessage) {
            welcomeMessage.textContent = `Bienvenido a ${config.siteName}! Por favor inicia sesión en tu cuenta.`;
        }

        // Update page title
        if (config.siteName) {
            document.title = `Login - ${config.siteName}`;
        }
    }

    /**
     * Handle password visibility toggle
     */
    function handlePasswordToggle() {
        const isPassword = passwordInput.type === 'password';
        
        passwordInput.type = isPassword ? 'text' : 'password';
        passwordIcon.className = isPassword ? 'fas fa-eye-slash' : 'fas fa-eye';
        
        // Brief focus back to input
        passwordInput.focus();
    }

    /**
     * Handle login form submission
     */
    async function handleLoginSubmit(event) {
        event.preventDefault();
        
        // Clear previous errors
        hideError();
        
        // Validate form
        if (!validateForm()) {
            return;
        }

        // Show loading state
        showLoading(true);

        try {
            const formData = new FormData(loginForm);
            
            const response = await fetch('/Auth/Login', {
                method: 'POST',
                body: formData
            });

            if (response.ok) {
                // Check if it's a redirect (successful login)
                if (response.redirected || response.url.includes('/Admin/Dashboard')) {
                    // Successful login
                    showSuccess('¡Login exitoso! Redirigiendo...');
                    
                    // Small delay for UX, then redirect
                    setTimeout(() => {
                        window.location.href = '/Admin/Dashboard';
                    }, 1000);
                } else {
                    // Handle case where server returns 200 but with validation errors
                    const text = await response.text();
                    if (text.includes('validation-summary') || text.includes('text-danger')) {
                        showError('Credenciales inválidas. Por favor verifica tu email y contraseña.');
                    } else {
                        showError('Error inesperado. Por favor intenta de nuevo.');
                    }
                }
            } else if (response.status === 400) {
                // Bad request - likely validation errors
                showError('Por favor verifica que todos los campos estén completos y sean válidos.');
            } else if (response.status === 401) {
                // Unauthorized
                showError('Credenciales inválidas. Por favor verifica tu email y contraseña.');
            } else if (response.status === 403) {
                // Forbidden - account might be inactive
                showError('Tu cuenta está inactiva. Contacta al administrador.');
            } else {
                // Other server errors
                showError('Error del servidor. Por favor intenta de nuevo más tarde.');
            }
        } catch (error) {
            console.error('Login error:', error);
            showError('Error de conexión. Por favor verifica tu conexión a internet e intenta de nuevo.');
        } finally {
            showLoading(false);
        }
    }

    /**
     * Validate form before submission
     */
    function validateForm() {
        const emailOrUsername = loginForm.querySelector('input[name="EmailOrUsername"]');
        const password = loginForm.querySelector('input[name="Password"]');
        
        if (!emailOrUsername || !emailOrUsername.value.trim()) {
            showError('Por favor ingresa tu email o username.');
            if (emailOrUsername) emailOrUsername.focus();
            return false;
        }

        if (!password || !password.value) {
            showError('Por favor ingresa tu contraseña.');
            if (password) password.focus();
            return false;
        }

        // No validar formato de email ya que puede ser username
        return true;
    }

    /**
     * Show loading state
     */
    function showLoading(show) {
        if (loginButton && loginSpinner) {
            if (show) {
                loginButton.disabled = true;
                loginButton.classList.add('loading');
                loginSpinner.classList.remove('d-none');
            } else {
                loginButton.disabled = false;
                loginButton.classList.remove('loading');
                loginSpinner.classList.add('d-none');
            }
        }
    }

    /**
     * Show error message
     */
    function showError(message) {
        if (errorMessage && errorText) {
            errorText.textContent = message;
            errorMessage.classList.remove('d-none');
            errorMessage.classList.add('shake');
            
            // Remove shake animation after it completes
            setTimeout(() => {
                errorMessage.classList.remove('shake');
            }, 500);
        }
    }

    /**
     * Hide error message
     */
    function hideError() {
        if (errorMessage) {
            errorMessage.classList.add('d-none');
        }
    }

    /**
     * Show success message
     */
    function showSuccess(message) {
        if (errorMessage && errorText) {
            errorText.textContent = message;
            errorMessage.className = 'alert alert-success';
            errorMessage.innerHTML = `<i class="fas fa-check-circle me-2"></i><span>${message}</span>`;
            errorMessage.classList.remove('d-none');
        }
    }

    /**
     * Add fade-in animation to the form
     */
    function addFadeInAnimation() {
        const formContainer = document.querySelector('.login-form-container');
        const illustrationContent = document.querySelector('.illustration-content');
        
        if (formContainer) {
            formContainer.classList.add('fade-in');
        }
        
        if (illustrationContent) {
            setTimeout(() => {
                illustrationContent.classList.add('fade-in');
            }, 200);
        }
    }

    /**
     * Handle input focus effects
     */
    const inputs = document.querySelectorAll('.form-control');
    inputs.forEach(input => {
        input.addEventListener('focus', function() {
            this.parentElement.classList.add('focused');
        });

        input.addEventListener('blur', function() {
            this.parentElement.classList.remove('focused');
        });

        // Clear errors when user starts typing
        input.addEventListener('input', function() {
            if (!errorMessage.classList.contains('d-none')) {
                hideError();
            }
        });
    });

    /**
     * Handle Enter key press for better UX
     */
    document.addEventListener('keypress', function(event) {
        if (event.key === 'Enter' && event.target.tagName === 'INPUT') {
            event.preventDefault();
            
            if (event.target.name === 'EmailOrUsername') {
                // Move to password field
                passwordInput.focus();
            } else if (event.target.name === 'Password') {
                // Submit form
                loginForm.dispatchEvent(new Event('submit'));
            }
        }
    });

    /**
     * Add subtle animations and interactions
     */
    const brandLogo = document.querySelector('.brand-logo');
    if (brandLogo) {
        brandLogo.addEventListener('load', function() {
            this.style.opacity = '0';
            this.style.transform = 'scale(0.8)';
            
            setTimeout(() => {
                this.style.transition = 'all 0.5s ease';
                this.style.opacity = '1';
                this.style.transform = 'scale(1)';
            }, 100);
        });
    }

    // Auto-focus on email/username field when page loads
    const emailInput = document.querySelector('input[name="EmailOrUsername"]');
    if (emailInput) {
        setTimeout(() => {
            emailInput.focus();
        }, 500);
    }
});

/**
 * Utility function to handle API errors
 */
function handleApiError(error, defaultMessage = 'Error inesperado') {
    console.error('API Error:', error);
    
    if (error.name === 'TypeError' && error.message.includes('fetch')) {
        return 'Error de conexión. Por favor verifica tu conexión a internet.';
    }
    
    return defaultMessage;
}