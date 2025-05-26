/**
 * Instructors Module JavaScript
 * Handles instructor profile functionality including avatar upload and account deactivation
 */

document.addEventListener('DOMContentLoaded', function() {
    // Avatar Preview Logic para Instructor
    const uploadInstructorAvatar = document.getElementById('uploadInstructorAvatar');
    const instructorAvatarPreview = document.getElementById('instructorAvatarPreview');
    const resetInstructorAvatar = document.getElementById('resetInstructorAvatar');
    const defaultInstructorAvatarSrc = '/img/default-avatar.png';

    if (uploadInstructorAvatar && instructorAvatarPreview) {
        uploadInstructorAvatar.addEventListener('change', function(event) {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function(e) {
                    instructorAvatarPreview.src = e.target.result;
                }
                reader.readAsDataURL(file);
            }
        });
    }
    
    if (resetInstructorAvatar && instructorAvatarPreview) {
        resetInstructorAvatar.addEventListener('click', function() {
            instructorAvatarPreview.src = defaultInstructorAvatarSrc;
            if (uploadInstructorAvatar) uploadInstructorAvatar.value = '';
        });
    }

    // Lógica para Habilitar/Deshabilitar Botón "Deactivate Account"
    const confirmDeactivationCheckbox = document.getElementById('confirmAccountDeactivation');
    const deactivateAccountButton = document.getElementById('deactivateAccountButton');

    if (confirmDeactivationCheckbox && deactivateAccountButton) {
        confirmDeactivationCheckbox.addEventListener('change', function() {
            deactivateAccountButton.disabled = !this.checked;
        });
    }

    // Password toggle functionality for Security tab
    // El script para los toggles de contraseña en la sección "Change Password" 
    // usa una clase específica para no interferir con otros toggles
    document.querySelectorAll('.toggle-password-instructor-security').forEach(toggle => {
        toggle.addEventListener('click', function () {
            const passwordInput = this.previousElementSibling; // Asume que el input está justo antes del span
            if (passwordInput) {
                const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
                passwordInput.setAttribute('type', type);
                this.querySelector('i').classList.toggle('fa-eye');
                this.querySelector('i').classList.toggle('fa-eye-slash');
            }
        });
    });

    // API Key creation functionality
    const createKeyButton = document.getElementById('securityCreateKeyButton');
    if (createKeyButton) {
        createKeyButton.addEventListener('click', function() {
            const keyType = document.getElementById('apiKeyType').value;
            const keyName = document.getElementById('apiKeyName').value;
            
            if (!keyType || !keyName) {
                alert('Please select a key type and enter a key name.');
                return;
            }
            
            // Here would go the actual API key creation logic
            console.log('Creating API key:', { type: keyType, name: keyName });
            alert('API Key creation functionality would be implemented here.');
        });
    }

    // API Key copy functionality
    document.querySelectorAll('[title="Copy API Key"]').forEach(function(button) {
        button.addEventListener('click', function() {
            // Get the API key from the row
            const row = this.closest('tr');
            const apiKeyText = row.querySelector('small').textContent;
            
            // Copy to clipboard
            navigator.clipboard.writeText(apiKeyText).then(function() {
                // Show success feedback
                const originalIcon = button.querySelector('i');
                originalIcon.classList.remove('fa-copy');
                originalIcon.classList.add('fa-check');
                button.classList.add('btn-success');
                button.classList.remove('btn-outline-secondary');
                
                setTimeout(function() {
                    originalIcon.classList.remove('fa-check');
                    originalIcon.classList.add('fa-copy');
                    button.classList.remove('btn-success');
                    button.classList.add('btn-outline-secondary');
                }, 2000);
            }).catch(function() {
                alert('Failed to copy API key to clipboard.');
            });
        });
    });

    // API Key delete functionality
    document.querySelectorAll('[title="Delete API Key"]').forEach(function(button) {
        button.addEventListener('click', function() {
            if (confirm('Are you sure you want to delete this API key? This action cannot be undone.')) {
                // Here would go the actual API key deletion logic
                const row = this.closest('tr');
                console.log('Deleting API key from row:', row);
                // row.remove(); // Uncomment to actually remove the row
                alert('API Key deletion functionality would be implemented here.');
            }
        });
    });

    // Enable 2FA button functionality
    const enable2FAButton = document.querySelector('[data-translate-key="security_enable_2fa_button"]');
    if (enable2FAButton) {
        enable2FAButton.addEventListener('click', function() {
            // Here would go the 2FA setup process
            alert('Two-Factor Authentication setup would be implemented here.');
        });
    }

    // Financial Tab - Payment Method Conditional Fields
    const paymentMethodSelect = document.getElementById('paymentMethod');
    const paypalFieldsDiv = document.getElementById('paypalFields');
    const bankTransferFieldsDiv = document.getElementById('bankTransferFields');

    if (paymentMethodSelect) {
        paymentMethodSelect.addEventListener('change', function() {
            // Ocultar todos los divs de campos específicos primero
            document.querySelectorAll('.payment-method-fields').forEach(div => {
                div.style.display = 'none';
            });

            // Mostrar el div correspondiente al método seleccionado
            if (this.value === 'paypal' && paypalFieldsDiv) {
                paypalFieldsDiv.style.display = 'flex'; // o 'block' o lo que corresponda a la estructura row
            } else if (this.value === 'bank_transfer' && bankTransferFieldsDiv) {
                bankTransferFieldsDiv.style.display = 'flex';
            }
        });
        // Disparar el evento change al cargar para mostrar los campos si ya hay un método seleccionado (en modo edición)
        // paymentMethodSelect.dispatchEvent(new Event('change'));
    }

    // Financial Tab - Form submission
    const financialForm = document.getElementById('financialForm');
    if (financialForm) {
        financialForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            // Here would go the form validation and submission logic
            console.log('Financial form submitted');
            alert('Payment information would be saved here.');
        });
    }

    // Financial Tab - Payment history actions
    document.querySelectorAll('.btn-view-invoice').forEach(function(button) {
        button.addEventListener('click', function() {
            const paymentId = this.getAttribute('data-payment-id');
            console.log('Viewing invoice for payment:', paymentId);
            alert('Invoice viewer would open here for payment ID: ' + paymentId);
        });
    });

    document.querySelectorAll('.btn-download-invoice').forEach(function(button) {
        button.addEventListener('click', function() {
            const paymentId = this.getAttribute('data-payment-id');
            console.log('Downloading invoice for payment:', paymentId);
            alert('Invoice download would start here for payment ID: ' + paymentId);
        });
    });

    // Features Tab - Form submission
    const featuresForm = document.getElementById('instructorFeaturesForm');
    if (featuresForm) {
        featuresForm.addEventListener('submit', function(e) {
            e.preventDefault();
            
            // Here would go the form validation and submission logic
            console.log('Features form submitted');
            alert('Instructor features would be saved here.');
        });
    }

    // Features Tab - File upload handling (optional preview)
    const documentUpload = document.getElementById('instructorDocumentUpload');
    if (documentUpload) {
        documentUpload.addEventListener('change', function(e) {
            const file = e.target.files[0];
            if (file) {
                console.log('Document uploaded:', file.name, file.size);
                // Could add file size validation, type validation, etc.
            }
        });
    }

    // Placeholder for Tom Select initialization (if needed in the future)
    // const specialtiesSelect = document.getElementById('instructorSpecialties');
    // if (specialtiesSelect) {
    //     new TomSelect(specialtiesSelect, {
    //         plugins: ['remove_button', 'input_autogrow'],
    //         create: true,
    //         persist: false,
    //         onOptionAdd: function(value, data) { 
    //             console.log('New specialty added:', value);
    //         }
    //     });
    // }

    // Inicializar Selects con Tom Select/Select2 si se usan (especialmente para 'Language' si es multiple)
    // Ejemplo básico si tienes la clase 'tom-select' en los selects:
    // document.querySelectorAll('.tom-select').forEach((el)=>{
    //  new TomSelect(el, {plugins: ['dropdown_input']}); // Ejemplo simple
    // });
});