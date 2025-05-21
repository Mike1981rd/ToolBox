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
    
    // Default avatar URL
    const defaultAvatarUrl = 'https://via.placeholder.com/100x100/DFE3E7/8C98A4?text=Avatar';
    
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
                
                const reader = new FileReader();
                reader.onload = function(e) {
                    userAvatarPreview.src = e.target.result;
                    userAvatarPreview.classList.add('avatar-highlight'); // Opcional: efecto visual
                    setTimeout(() => userAvatarPreview.classList.remove('avatar-highlight'), 500);
                };
                reader.readAsDataURL(file);
            }
        });
    }
    
    // 2. Reset Avatar to Default
    if (resetAvatarButton && userAvatarPreview) {
        resetAvatarButton.addEventListener('click', function() {
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
                // Aquí se enviaría el formulario en una implementación real
                console.log('Form submitted successfully');
                
                // Creamos un objeto para simular los datos del formulario
                const formData = {
                    fullName: document.getElementById('add-user-fullname').value,
                    username: document.getElementById('add-user-username').value,
                    email: document.getElementById('add-user-email').value,
                    role: document.getElementById('user-role').value,
                    plan: document.getElementById('user-plan').value,
                    status: document.getElementById('activeStatus').checked ? 'Active' : 'Inactive'
                };
                console.log('User data:', formData);
                
                // Cerrar el offcanvas
                const offcanvasElement = document.getElementById('addUserOffcanvas');
                const offcanvasInstance = bootstrap.Offcanvas.getInstance(offcanvasElement);
                if (offcanvasInstance) {
                    offcanvasInstance.hide();
                }
                
                // Reset the form
                this.reset();
                this.classList.remove('was-validated');
                if (userAvatarPreview) {
                    userAvatarPreview.src = defaultAvatarUrl;
                }
                
                // Mostrar notificación de éxito (conectaría con un sistema de toasts en implementación real)
                alert('User added successfully!');
                
                // En una implementación real, aquí se recargaría la tabla o se añadiría el usuario a la tabla existente
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