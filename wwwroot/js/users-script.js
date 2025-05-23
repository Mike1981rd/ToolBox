/**
 * Users Management JavaScript
 * Handles form validation, avatar upload preview, and password visibility toggle
 */

document.addEventListener('DOMContentLoaded', function() {
    // Get references to all interactive elements - Nueva estructura
    const addUserForm = document.getElementById('addNewUserForm');
    const uploadAvatarInput = document.getElementById('uploadAvatarInput');
    const resetAvatarButton = document.getElementById('resetAvatarButton');
    const userAvatarPreview = document.getElementById('userAvatarPreview');
    const togglePasswordVisibility = document.getElementById('togglePasswordVisibility');
    const toggleConfirmPasswordVisibility = document.getElementById('toggleConfirmPasswordVisibility');
    const passwordInput = document.getElementById('add-user-password');
    const confirmPasswordInput = document.getElementById('add-user-confirm-password');
    
    // Avatar por defecto local
    const defaultAvatarUrl = '/img/default-avatar.png';
    
    // 1. Avatar Upload Preview - Mejorado
    if (uploadAvatarInput && userAvatarPreview) {
        uploadAvatarInput.addEventListener('change', function(event) {
            const file = event.target.files[0];
            if (file) {
                // Validación de tamaño (800KB max)
                if (file.size > 800 * 1024) {
                    alert('El archivo es demasiado grande. El tamaño máximo permitido es 800KB.');
                    this.value = ''; // Limpiar input
                    return;
                }
                
                // Validación de tipo (solo imágenes)
                if (!file.type.match('image.*')) {
                    alert('Por favor, selecciona una imagen válida (JPG, PNG o GIF).');
                    this.value = ''; // Limpiar input
                    return;
                }
                
                // Usar URL.createObjectURL para mejor rendimiento
                const objectUrl = URL.createObjectURL(file);
                userAvatarPreview.src = objectUrl;
                userAvatarPreview.classList.add('avatar-highlight'); // Opcional: efecto visual
                setTimeout(() => userAvatarPreview.classList.remove('avatar-highlight'), 500);
                
                // Limpiar el objeto URL cuando se cambie la imagen
                userAvatarPreview.onload = function() {
                    URL.revokeObjectURL(objectUrl);
                };
            }
        });
    }
    
    // 2. Reset Avatar to Default
    if (resetAvatarButton && userAvatarPreview) {
        resetAvatarButton.addEventListener('click', function() {
            // Usar la imagen por defecto local
            userAvatarPreview.src = defaultAvatarUrl;
            
            if (uploadAvatarInput) {
                uploadAvatarInput.value = ''; // Limpiar input file
            }
        });
    }
    
    // 3. Toggle Password Visibility - Mejorado con elementos específicos
    if (togglePasswordVisibility && passwordInput) {
        togglePasswordVisibility.addEventListener('click', function() {
            const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
            passwordInput.setAttribute('type', type);
            // Cambiar icono
            this.querySelector('i').classList.toggle('fa-eye');
            this.querySelector('i').classList.toggle('fa-eye-slash');
        });
    }
    
    if (toggleConfirmPasswordVisibility && confirmPasswordInput) {
        toggleConfirmPasswordVisibility.addEventListener('click', function() {
            const type = confirmPasswordInput.getAttribute('type') === 'password' ? 'text' : 'password';
            confirmPasswordInput.setAttribute('type', type);
            // Cambiar icono
            this.querySelector('i').classList.toggle('fa-eye');
            this.querySelector('i').classList.toggle('fa-eye-slash');
        });
    }
    
    // 4. Form Validation - Mejorado
    if (addUserForm) {
        // Plan field removed - no longer needed
        
        // Check passwords match
        addUserForm.addEventListener('submit', function(event) {
            event.preventDefault();
            
            // Add was-validated class to show validation feedback
            this.classList.add('was-validated');
            
            // Additional validation for password match
            if (passwordInput && confirmPasswordInput) {
                if (passwordInput.value !== confirmPasswordInput.value) {
                    confirmPasswordInput.setCustomValidity('Passwords must match');
                } else {
                    confirmPasswordInput.setCustomValidity('');
                }
            }
            
            // If form is valid
            if (this.checkValidity()) {
                // Preparar los datos del formulario
                const formData = new FormData();
                formData.append('FullName', document.getElementById('add-user-fullname').value);
                formData.append('UserName', document.getElementById('add-user-username').value);
                formData.append('Email', document.getElementById('add-user-email').value);
                formData.append('Password', document.getElementById('add-user-password').value);
                formData.append('ConfirmPassword', document.getElementById('add-user-confirm-password').value);
                formData.append('RoleId', document.getElementById('user-role').value || '');
                formData.append('PhoneNumber', document.getElementById('add-user-contact').value || '');
                formData.append('CompanyName', document.getElementById('add-user-company').value || '');
                formData.append('Country', document.getElementById('country').value || '');
                formData.append('TaxId', document.getElementById('taxId').value || '');
                formData.append('BillingMethod', document.getElementById('billingMethod').value || '');
                formData.append('Language', document.getElementById('language').value || '');
                formData.append('IsActive', document.getElementById('activeStatus').checked);
                formData.append('IsTwoFactorEnabled', document.getElementById('twoFactorAuth').checked);
                
                // Agregar el archivo de avatar si existe
                const avatarInput = document.getElementById('uploadAvatarInput');
                if (avatarInput && avatarInput.files && avatarInput.files[0]) {
                    formData.append('AvatarFile', avatarInput.files[0]);
                }
                
                // Obtener el token antiforgery
                const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
                formData.append('__RequestVerificationToken', token);
                
                // Enviar la petición al servidor
                fetch('/Users/Create', {
                    method: 'POST',
                    body: formData
                })
                .then(response => {
                    if (response.redirected) {
                        // Si hay redirección, probablemente el usuario se creó exitosamente
                        window.location.href = response.url;
                    } else {
                        return response.text();
                    }
                })
                .then(html => {
                    if (html) {
                        // Si hay HTML, probablemente hay errores de validación
                        // Por ahora, mostrar alerta simple
                        alert('Error al crear el usuario. Por favor revise los datos.');
                        console.error('Validation errors occurred');
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                    alert('Ocurrió un error al crear el usuario.');
                });
            }
        });
        
        // Validación en tiempo real para confirmación de contraseña
        if (confirmPasswordInput && passwordInput) {
            confirmPasswordInput.addEventListener('input', function() {
                if (this.value !== passwordInput.value) {
                    this.setCustomValidity('Passwords must match');
                } else {
                    this.setCustomValidity('');
                }
            });
            
            passwordInput.addEventListener('input', function() {
                if (confirmPasswordInput.value && this.value !== confirmPasswordInput.value) {
                    confirmPasswordInput.setCustomValidity('Passwords must match');
                } else {
                    confirmPasswordInput.setCustomValidity('');
                }
            });
        }
    }
    
    // 5. Table Row Actions - Mejorado para funcionar con cualquier selector
    const editButtons = document.querySelectorAll('.btn-icon i.fa-edit, .btn-icon i.fas.fa-edit').forEach(icon => {
        const button = icon.closest('button');
        if (button) {
            button.addEventListener('click', function() {
                const row = this.closest('tr');
                const userId = row ? (row.dataset.userId || 'placeholder-id') : 'unknown';
                console.log(`Edit user with ID: ${userId}`);
                
                // En una implementación real, aquí se cargarían los datos del usuario en el formulario
                const offcanvasElement = document.getElementById('addUserOffcanvas');
                if (offcanvasElement) {
                    const offcanvasInstance = new bootstrap.Offcanvas(offcanvasElement);
                    offcanvasInstance.show();
                }
            });
        }
    });
    
    // Delete button functionality - Mejorado
    document.querySelectorAll('.delete-record').forEach(button => {
        button.addEventListener('click', function() {
            if (confirm('Are you sure you want to delete this user?')) {
                const row = this.closest('tr');
                if (!row) return;
                
                const userId = row.dataset.userId || 'placeholder-id';
                console.log(`Delete user with ID: ${userId}`);
                
                // Animación mejorada para eliminar fila
                row.style.backgroundColor = '#ffe5e5';
                setTimeout(() => {
                    row.style.transition = 'all 0.3s ease';
                    row.style.opacity = '0';
                    row.style.maxHeight = '0';
                    row.style.transform = 'translateX(20px)';
                    setTimeout(() => {
                        row.remove();
                    }, 300);
                }, 300);
            }
        });
    });
    
    // 6. Initialize tooltips y popovers
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function(tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl, {
            boundary: document.body
        });
    });
});

// Función auxiliar para manejar errores graciosamente
function handleError(element, message) {
    console.error(message);
    if (element) {
        // Añadir clase de error o hacer algo visual si es apropiado
    }
    return false;
}