/**
 * Customers Management JavaScript
 * Handles functionality for the customers module
 */

document.addEventListener('DOMContentLoaded', function() {
    // Initialize customer table functionality
    initCustomerTable();
    
    // Initialize customer action buttons
    initCustomerActions();

    // Initialize customer offcanvas functionality
    initCustomerOffcanvas();

    // Initialize translation after DOM is ready
    if (typeof translatePage === 'function') {
        translatePage();
    }
});

/**
 * Initialize customer table functionality
 */
function initCustomerTable() {
    // Customer table search functionality
    const searchInput = document.querySelector('#DataTables_Table_Customers_filter input');
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            // TODO: Implement search functionality
            console.log('Searching customers:', this.value);
        });
    }
    
    // Table pagination
    const paginationLinks = document.querySelectorAll('#DataTables_Table_Customers_paginate .page-link');
    paginationLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            // TODO: Implement pagination functionality
            console.log('Pagination clicked');
        });
    });
    
    // Select all checkbox
    const selectAllCheckbox = document.querySelector('.dt-checkboxes-select-all');
    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener('change', function() {
            const checkboxes = document.querySelectorAll('.dt-checkboxes');
            checkboxes.forEach(checkbox => {
                checkbox.checked = this.checked;
            });
        });
    }
}

/**
 * Initialize customer action buttons
 */
function initCustomerActions() {
    // Edit customer buttons
    document.querySelectorAll('.edit-customer-btn').forEach(button => {
        button.addEventListener('click', function() {
            const customerId = this.getAttribute('data-customer-id');
            console.log('Edit customer:', customerId);
            
            // Change offcanvas title to Edit mode
            const titleElement = document.getElementById('addCustomerOffcanvasLabel');
            if (titleElement) {
                titleElement.setAttribute('data-translate-key', 'offcanvas_edit_customer_title');
                titleElement.textContent = 'Edit Customer';
            }
            
            // Hide password section for edit mode
            const passwordSection = document.getElementById('passwordSection');
            if (passwordSection) {
                passwordSection.style.display = 'none';
            }
            
            // Show the offcanvas
            const offcanvasElement = document.getElementById('addCustomerOffcanvas');
            if (offcanvasElement && typeof bootstrap !== 'undefined') {
                const offcanvas = new bootstrap.Offcanvas(offcanvasElement);
                offcanvas.show();
            }
            
            // Re-translate the page to update the changed title
            if (typeof translatePage === 'function') {
                translatePage();
            }
        });
    });
    
    // Deactivate customer buttons
    document.querySelectorAll('.deactivate-customer-btn').forEach(button => {
        button.addEventListener('click', function() {
            const customerId = this.getAttribute('data-customer-id');
            if (confirm('Are you sure you want to deactivate this customer?')) {
                console.log('Deactivate customer:', customerId);
                // TODO: Implement deactivate functionality
            }
        });
    });
    
    // Add customer button
    const addCustomerBtn = document.querySelector('[data-bs-target="#addCustomerOffcanvas"]');
    if (addCustomerBtn) {
        addCustomerBtn.addEventListener('click', function() {
            console.log('Add new customer');
            
            // Reset offcanvas title to Add mode
            const titleElement = document.getElementById('addCustomerOffcanvasLabel');
            if (titleElement) {
                titleElement.setAttribute('data-translate-key', 'offcanvas_add_customer_title');
                titleElement.textContent = 'Add New Customer';
            }
            
            // Show password section for add mode
            const passwordSection = document.getElementById('passwordSection');
            if (passwordSection) {
                passwordSection.style.display = 'block';
            }
            
            // Clear form
            const form = document.getElementById('addNewCustomerForm');
            if (form) {
                form.reset();
            }
            
            // Re-translate the page to update the changed title
            if (typeof translatePage === 'function') {
                translatePage();
            }
        });
    }
}

/**
 * Initialize customer offcanvas functionality
 */
function initCustomerOffcanvas() {
    // Password toggle functionality
    document.querySelectorAll('.form-password-toggle .input-group-text').forEach(toggleBtn => {
        toggleBtn.addEventListener('click', function() {
            const input = this.parentElement.querySelector('input');
            const icon = this.querySelector('i');

            if (input.type === 'password') {
                input.type = 'text';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            } else {
                input.type = 'password';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            }
        });
    });

    // Avatar upload functionality
    const uploadInput = document.getElementById('upload');
    if (uploadInput) {
        uploadInput.addEventListener('change', function() {
            const file = this.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function(e) {
                    const avatarImg = document.getElementById('uploadedAvatar');
                    if (avatarImg) {
                        avatarImg.src = e.target.result;
                    }
                };
                reader.readAsDataURL(file);
            }
        });
    }

    // Avatar reset functionality
    const resetBtn = document.querySelector('.account-image-reset');
    if (resetBtn) {
        resetBtn.addEventListener('click', function() {
            const avatarImg = document.getElementById('uploadedAvatar');
            const uploadInput = document.getElementById('upload');
            
            if (avatarImg) {
                avatarImg.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTAwIiBoZWlnaHQ9IjEwMCIgdmlld0JveD0iMCAwIDEwMCAxMDAiIGZpbGw9Im5vbmUiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyI+CjxyZWN0IHdpZHRoPSIxMDAiIGhlaWdodD0iMTAwIiBmaWxsPSIjRkZGRkZGIi8+CjxwYXRoIGQ9Ik01MCA1MEMzNS44NTc5IDUwIDI1IDM5LjE0MjEgMjUgMjVDMjUgMTAuODU3OSAzNS44NTc5IDAgNTAgMEM2NC4xNDIxIDAgNzUgMTAuODU3OSA3NSAyNUM3NSAzOS4xNDIxIDY0LjE0MjEgNTAgNTAgNTBaIiBmaWxsPSIjOUI5Qjk5Ii8+CjxwYXRoIGQ9Ik01MCA1NUMzNS44NTc5IDU1IDI1IDQ0LjE0MjEgMjUgMzBDMjUgMTUuODU3OSAzNS44NTc5IDUgNTAgNUM2NC4xNDIxIDUgNzUgMTUuODU3OSA3NSAzMEM3NSA0NC4xNDIxIDY0LjE0MjEgNTUgNTAgNTVaIiBmaWxsPSIjOUI5Qjk5Ii8+CjxwYXRoIGQ9Ik01MCA2MEM0MS43MTU5IDYwIDM1IDY2LjcxNTkgMzUgNzVDMzUgODMuMjg0MSA0MS43MTU5IDkwIDUwIDkwQzU4LjI4NDEgOTAgNjUgODMuMjg0MSA2NSA3NUM2NSA2Ni43MTU5IDU4LjI4NDEgNjAgNTAgNjBaIiBmaWxsPSIjOUI5Qjk5Ii8+Cjwvc3ZnPg==';
            }
            if (uploadInput) {
                uploadInput.value = '';
            }
        });
    }

    // Form submission
    const form = document.getElementById('addNewCustomerForm');
    if (form) {
        form.addEventListener('submit', function(e) {
            e.preventDefault();
            
            // Here you would typically send the data to your server
            console.log('Customer form submitted');
            
            // Close the offcanvas
            const offcanvasElement = document.getElementById('addCustomerOffcanvas');
            if (offcanvasElement && typeof bootstrap !== 'undefined') {
                const offcanvas = bootstrap.Offcanvas.getInstance(offcanvasElement);
                if (offcanvas) {
                    offcanvas.hide();
                }
            }
            
            // Show success message (you can customize this)
            // alert('Customer saved successfully!');
        });
    }
}