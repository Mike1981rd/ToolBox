/**
 * Admin Dashboard JavaScript
 * Handles sidebar toggling, theme switching, and language selection
 */

/**
 * Translations for the application
 */
const translations = {
    en: {
        dashboard: "Dashboard",
        roles_menu: "Roles",
        users_menu: "Users",
        users_list_title: "Users List",
        // Tarjetas de estadísticas
        card_session_title: "Session",
        card_session_total_users: "Total Users",
        card_active_users_title: "Active Users",
        card_active_users_last_week: "Last week analytics",
        // Sección de Filtros
        filters_title: "Filters",
        filter_select_role: "Select Role",
        filter_select_plan: "Select Plan",
        filter_select_status: "Select Status",
        // Controles de Tabla
        search_user_placeholder: "Search User",
        export_button: "Export",
        export_to_excel: "Export to Excel",
        export_to_pdf: "Export to PDF",
        export_to_csv: "Export to CSV",
        add_new_record_button: "Add New Record",
        // Encabezados de Tabla
        th_user_list_user: "USER",
        th_user_list_role: "ROLE",
        th_user_list_plan: "PLAN",
        th_user_list_billing: "BILLING",
        th_user_list_status: "STATUS",
        th_user_list_actions: "ACTIONS",
        // Paginación
        showing_entries: "Showing {start} to {end} of {total} entries",
        // Add User Offcanvas
        add_user_title: "Add User",
        form_user_avatar: "User Avatar",
        form_upload_new: "Upload New",
        form_reset: "Reset",
        form_full_name: "Full Name",
        form_username: "Username",
        form_email: "Email",
        form_role: "Role",
        form_plan: "Plan",
        form_select_role: "Select Role",
        form_select_plan: "Select Plan",
        form_feedback_name: "Please enter a valid name",
        form_feedback_username: "Please enter a valid username",
        form_feedback_email: "Please enter a valid email",
        form_feedback_role: "Please select a role",
        form_feedback_plan: "Please select a plan",
        form_section_payment: "Payment Details",
        form_billing_method: "Billing Method",
        form_status: "Status",
        form_status_active: "Active",
        form_tax_id: "Tax ID",
        form_contact: "Contact",
        form_language: "Language",
        form_country: "Country",
        form_section_security: "Security",
        form_password: "Password",
        form_confirm_password: "Confirm Password",
        form_feedback_password: "Please provide a password",
        form_feedback_password_match: "Passwords must match",
        form_2fa: "Enable Two-Factor Authentication",
        form_button_cancel: "Cancel",
        form_button_submit: "Add User",
        payment_auto_debit: "Auto Debit",
        payment_manual_paypal: "Manual - Paypal",
        payment_manual_cc: "Manual - Credit Card",
        payment_manual_bank: "Manual - Bank Transfer",
        language_english: "English",
        language_spanish: "Spanish",
        country_usa: "United States",
        country_uk: "United Kingdom",
        country_spain: "Spain",
        country_france: "France",
        country_germany: "Germany",
        country_other: "Other",
        plan_basic: "Basic",
        plan_team: "Team",
        plan_company: "Company",
        plan_enterprise: "Enterprise",
        // Nuevas traducciones para el offcanvas mejorado
        offcanvas_user_avatar: "User Avatar",
        offcanvas_upload_new_photo: "Upload new photo",
        offcanvas_reset_avatar: "Reset",
        offcanvas_avatar_allowed_formats: "Allowed JPG, GIF or PNG. Max size of 800K",
        offcanvas_username: "Username",
        offcanvas_password: "Password",
        offcanvas_company: "Company",
        select_country_option: "Select Country",
        all_option: "All",
        roles_list_title: "Roles List",
        roles_list_description: "A role provided access to predefined menus and features so that depending on assigned role an administrator can have access to what user needs.",
        total_users_with_roles: "Total users with their roles",
        add_new_role_card_title: "Add New Role",
        add_new_role_card_text: "Add new role, if it doesn't exist.",
        total_users_role_prefix: "Total users",
        edit_role_link: "Edit Role",
        search_placeholder: "Search...",
        export_button: "Export",
        add_new_user_button: "Add New User",
        status_active: "Active",
        status_pending: "Pending",
        status_inactive: "Inactive",
        modal_add_new_role_title: "Add New Role",
        modal_set_role_permissions: "Set role permissions",
        modal_role_name_label: "Role Name",
        modal_enter_role_name_placeholder: "Enter a role name",
        modal_role_permissions_section_title: "Role Permissions",
        perm_admin_access: "Administrator Access",
        tooltip_admin_access: "Allows full access to the system",
        select_all: "Select All",
        perm_read: "Read",
        perm_write: "Write",
        perm_create: "Create",
        perm_cat_user_management: "User Management",
        perm_cat_content_management: "Content Management",
        perm_cat_disputes_management: "Disputes Management",
        perm_cat_database_management: "Database Management",
        perm_cat_financial_management: "Financial Management",
        perm_cat_reporting: "Reporting",
        perm_cat_api_control: "API Control",
        perm_cat_repository_management: "Repository Management",
        perm_cat_payroll: "Payroll",
        cancel_button: "Cancel",
        submit_button: "Submit",
        th_user: "USER",
        th_role: "ROLE",
        th_plan: "PLAN",
        th_billing: "BILLING",
        th_status: "STATUS",
        th_actions: "ACTIONS",
        areas_of_life: "Areas of Life",
        xray_of_life: "X-Ray of life",
        default_welcome_message: "Default Welcome Message",
        coaches: "Coaches",
        add_user: "Add USER",
        profile: "Profile",
        academy: "Academy",
        academy_submenu_1: "Courses",
        academy_submenu_2: "Materials",
        toolbox_academy: "Toolbox academy",
        website_setting: "Website Setting",
        email_contents: "Email Contents",
        analytics: "Analytics",
        settings: "Settings",
        notifications: "Notifications",
        logout: "Logout",
        select_language: "Select Language",
        welcome_message_admin: 'Dear Carlos Checo <span data-translate-key="welcome">Welcome</span>. You are logged in as <span data-translate-key="admin_role">Admin</span>',
        welcome: "Welcome",
        admin_role: "Admin",
        total_coach: "Total Coaches",
        active_coaches: "Active Coaches",
        activity_rate: "Activity Rate"
    },
    es: {
        dashboard: "Tablero",
        roles_menu: "Roles",
        users_menu: "Usuarios",
        users_list_title: "Lista de Usuarios",
        // Tarjetas de estadísticas
        card_session_title: "Sesión",
        card_session_total_users: "Usuarios Totales",
        card_active_users_title: "Usuarios Activos",
        card_active_users_last_week: "Análisis de la última semana",
        // Sección de Filtros
        filters_title: "Filtros",
        filter_select_role: "Seleccionar Rol",
        filter_select_plan: "Seleccionar Plan",
        filter_select_status: "Seleccionar Estado",
        // Controles de Tabla
        search_user_placeholder: "Buscar Usuario",
        export_button: "Exportar",
        export_to_excel: "Exportar a Excel",
        export_to_pdf: "Exportar a PDF",
        export_to_csv: "Exportar a CSV",
        add_new_record_button: "Añadir Nuevo Registro",
        // Encabezados de Tabla
        th_user_list_user: "USUARIO",
        th_user_list_role: "ROL",
        th_user_list_plan: "PLAN",
        th_user_list_billing: "FACTURACIÓN",
        th_user_list_status: "ESTADO",
        th_user_list_actions: "ACCIONES",
        // Paginación
        showing_entries: "Mostrando {start} a {end} de {total} entradas",
        // Add User Offcanvas
        add_user_title: "Añadir Usuario",
        form_user_avatar: "Avatar de Usuario",
        form_upload_new: "Subir Nuevo",
        form_reset: "Restablecer",
        form_full_name: "Nombre Completo",
        form_username: "Nombre de Usuario",
        form_email: "Correo Electrónico",
        form_role: "Rol",
        form_plan: "Plan",
        form_select_role: "Seleccionar Rol",
        form_select_plan: "Seleccionar Plan",
        form_feedback_name: "Por favor, introduce un nombre válido",
        form_feedback_username: "Por favor, introduce un nombre de usuario válido",
        form_feedback_email: "Por favor, introduce un correo electrónico válido",
        form_feedback_role: "Por favor, selecciona un rol",
        form_feedback_plan: "Por favor, selecciona un plan",
        form_section_payment: "Detalles de Pago",
        form_billing_method: "Método de Facturación",
        form_status: "Estado",
        form_status_active: "Activo",
        form_tax_id: "ID Fiscal",
        form_contact: "Contacto",
        form_language: "Idioma",
        form_country: "País",
        form_section_security: "Seguridad",
        form_password: "Contraseña",
        form_confirm_password: "Confirmar Contraseña",
        form_feedback_password: "Por favor, proporciona una contraseña",
        form_feedback_password_match: "Las contraseñas deben coincidir",
        form_2fa: "Habilitar Autenticación de Dos Factores",
        form_button_cancel: "Cancelar",
        form_button_submit: "Añadir Usuario",
        payment_auto_debit: "Débito Automático",
        payment_manual_paypal: "Manual - Paypal",
        payment_manual_cc: "Manual - Tarjeta de Crédito",
        payment_manual_bank: "Manual - Transferencia Bancaria",
        language_english: "Inglés",
        language_spanish: "Español",
        country_usa: "Estados Unidos",
        country_uk: "Reino Unido",
        country_spain: "España",
        country_france: "Francia",
        country_germany: "Alemania",
        country_other: "Otro",
        plan_basic: "Básico",
        plan_team: "Equipo",
        plan_company: "Empresa",
        plan_enterprise: "Empresa Grande",
        // Nuevas traducciones para el offcanvas mejorado
        offcanvas_user_avatar: "Avatar de Usuario",
        offcanvas_upload_new_photo: "Subir nueva foto",
        offcanvas_reset_avatar: "Restablecer",
        offcanvas_avatar_allowed_formats: "Formatos permitidos JPG, GIF o PNG. Tamaño máx. de 800K",
        offcanvas_username: "Nombre de usuario",
        offcanvas_password: "Contraseña",
        offcanvas_company: "Empresa",
        select_country_option: "Seleccionar País",
        all_option: "Todos",
        roles_list_title: "Lista de Roles",
        roles_list_description: "Un rol proporciona acceso a menús y funciones predefinidos para que, dependiendo del rol asignado, un administrador pueda tener acceso a lo que el usuario necesita.",
        total_users_with_roles: "Total de usuarios con sus roles",
        add_new_role_card_title: "Añadir Nuevo Rol",
        add_new_role_card_text: "Añade un nuevo rol, si no existe.",
        total_users_role_prefix: "Total usuarios",
        edit_role_link: "Editar Rol",
        search_placeholder: "Buscar...",
        export_button: "Exportar",
        add_new_user_button: "Añadir Nuevo Usuario",
        status_active: "Activo",
        status_pending: "Pendiente",
        status_inactive: "Inactivo",
        modal_add_new_role_title: "Añadir Nuevo Rol",
        modal_set_role_permissions: "Establecer permisos del rol",
        modal_role_name_label: "Nombre del Rol",
        modal_enter_role_name_placeholder: "Introduce un nombre de rol",
        modal_role_permissions_section_title: "Permisos del Rol",
        perm_admin_access: "Acceso de Administrador",
        tooltip_admin_access: "Permite acceso completo al sistema",
        select_all: "Seleccionar Todo",
        perm_read: "Leer",
        perm_write: "Escribir",
        perm_create: "Crear",
        perm_cat_user_management: "Gestión de Usuarios",
        perm_cat_content_management: "Gestión de Contenido",
        perm_cat_disputes_management: "Gestión de Disputas",
        perm_cat_database_management: "Gestión de Base de Datos",
        perm_cat_financial_management: "Gestión Financiera",
        perm_cat_reporting: "Reportes",
        perm_cat_api_control: "Control de API",
        perm_cat_repository_management: "Gestión de Repositorio",
        perm_cat_payroll: "Nómina",
        cancel_button: "Cancelar",
        submit_button: "Enviar",
        th_user: "USUARIO",
        th_role: "ROL",
        th_plan: "PLAN",
        th_billing: "FACTURACIÓN",
        th_status: "ESTADO",
        th_actions: "ACCIONES",
        areas_of_life: "Áreas de Vida",
        xray_of_life: "Radiografía de Vida",
        default_welcome_message: "Mensaje de Bienvenida",
        coaches: "Coaches",
        add_user: "Añadir Usuario",
        profile: "Perfil",
        academy: "Academia",
        academy_submenu_1: "Cursos",
        academy_submenu_2: "Materiales",
        toolbox_academy: "Academia ToolBox",
        website_setting: "Configuración Web",
        email_contents: "Contenidos de Email",
        analytics: "Analítica",
        settings: "Configuración",
        notifications: "Notificaciones",
        logout: "Cerrar Sesión",
        select_language: "Seleccionar Idioma",
        welcome_message_admin: 'Estimado Carlos Checo <span data-translate-key="welcome">Bienvenido</span>. Has iniciado sesión como <span data-translate-key="admin_role">Administrador</span>',
        welcome: "Bienvenido",
        admin_role: "Administrador",
        total_coach: "Total de Coaches",
        active_coaches: "Coaches Activos",
        activity_rate: "Tasa de Actividad"
    }
};

// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {
    // Elements
    const sidebarToggle = document.getElementById('sidebarToggle');
    const menuToggle = document.getElementById('menu-toggle');
    const sidebar = document.getElementById('sidebar-wrapper');
    const pageContent = document.getElementById('page-content-wrapper');
    const themeToggle = document.getElementById('theme-toggle');
    const themeIcon = document.getElementById('theme-icon');
    const languageButtons = document.querySelectorAll('.lang-select');
    const selectedLanguage = document.getElementById('selectedLanguage');
    
    // Create sidebar overlay for mobile
    const overlay = document.createElement('div');
    overlay.classList.add('sidebar-overlay');
    document.body.appendChild(overlay);
    
    // Initialize all components from localStorage
    initTheme();
    initLanguage();
    initSidebar();

    // Sidebar Toggle functionality
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function(e) {
            e.preventDefault();
            toggleSidebar();
        });
    }
    
    // Mobile menu close button
    if (menuToggle) {
        menuToggle.addEventListener('click', function(e) {
            e.preventDefault();
            toggleSidebar();
        });
    }
    
    // Overlay click handler (for mobile)
    overlay.addEventListener('click', function() {
        toggleSidebar();
    });
    
    // Theme Toggle functionality
    if (themeToggle) {
        themeToggle.addEventListener('click', function(e) {
            e.preventDefault();
            toggleTheme();
        });
    }
    
    // Language Selection functionality
    languageButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            const language = this.getAttribute('data-lang');
            setLanguage(language);
            console.log("Changing language to: " + language);
        });
    });
    
    /**
     * Toggles the sidebar visibility
     */
    function toggleSidebar() {
        sidebar.classList.toggle('toggled');
        pageContent.classList.toggle('toggled');
        overlay.classList.toggle('show');
        
        // Update aria-expanded attribute
        const isExpanded = !sidebar.classList.contains('toggled');
        if (sidebarToggle) {
            sidebarToggle.setAttribute('aria-expanded', isExpanded);
        }
        if (menuToggle) {
            menuToggle.setAttribute('aria-expanded', isExpanded);
        }
        
        // Save sidebar state to localStorage
        localStorage.setItem('sidebarCollapsed', sidebar.classList.contains('toggled'));
        
        // Prevent body scroll when sidebar is open on mobile
        if (sidebar.classList.contains('toggled') && window.innerWidth < 992) {
            document.body.style.overflow = 'hidden';
        } else {
            document.body.style.overflow = '';
        }
    }
    
    /**
     * Toggles between light and dark theme
     */
    function toggleTheme() {
        // Get the current theme from the document element
        const currentTheme = document.documentElement.getAttribute('data-theme');
        const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        
        // Update both HTML and body attributes
        document.documentElement.setAttribute('data-theme', newTheme);
        document.body.setAttribute('data-bs-theme', newTheme);
        
        // Update the theme icon
        if (newTheme === 'dark') {
            themeIcon.classList.remove('fa-moon');
            themeIcon.classList.add('fa-sun');
        } else {
            themeIcon.classList.remove('fa-sun');
            themeIcon.classList.add('fa-moon');
        }
        
        // Save the theme preference to localStorage
        localStorage.setItem('theme', newTheme);
        
        // Update the charts if they exist (optional)
        if (typeof salesChart !== 'undefined' && salesChart) {
            updateChartColors(newTheme);
        }
    }
    
    /**
     * Updates chart colors based on the current theme
     * @param {string} theme - The current theme ('light' or 'dark')
     */
    function updateChartColors(theme) {
        // Check if we're on a page with a chart
        const chartCanvas = document.getElementById('salesChart');
        if (!chartCanvas) {
            console.log('No chart canvas found on this page, skipping chart color update');
            return;
        }
        
        // Check if salesChart is defined and accessible in the global scope
        if (typeof window.salesChart === 'undefined' || !window.salesChart) {
            console.log('salesChart is not defined, skipping chart color update');
            
            // If we have a canvas but no chart yet, try to initialize it based on the theme
            if (chartCanvas && typeof Chart !== 'undefined') {
                try {
                    initializeChart(theme);
                } catch (error) {
                    console.log('Failed to initialize chart:', error);
                }
            }
            return;
        }
        
        try {
            // Make sure the chart has the necessary properties before accessing them
            if (!window.salesChart.data || !window.salesChart.data.datasets || window.salesChart.data.datasets.length === 0) {
                console.log('Chart exists but has no datasets, skipping color update');
                return;
            }
            
            if (theme === 'dark') {
                // Dark mode colors - slightly brighter for dark background
                window.salesChart.data.datasets[0].borderColor = 'rgba(50, 145, 255, 1)';
                window.salesChart.data.datasets[0].backgroundColor = 'rgba(50, 145, 255, 0.2)';
                if (window.salesChart.data.datasets.length > 1) {
                    window.salesChart.data.datasets[1].borderColor = 'rgba(50, 200, 100, 1)';
                    window.salesChart.data.datasets[1].backgroundColor = 'rgba(50, 200, 100, 0.2)';
                }
            } else {
                // Light mode colors
                window.salesChart.data.datasets[0].borderColor = 'rgba(13, 110, 253, 1)';
                window.salesChart.data.datasets[0].backgroundColor = 'rgba(13, 110, 253, 0.1)';
                if (window.salesChart.data.datasets.length > 1) {
                    window.salesChart.data.datasets[1].borderColor = 'rgba(25, 135, 84, 1)';
                    window.salesChart.data.datasets[1].backgroundColor = 'rgba(25, 135, 84, 0.1)';
                }
            }
            
            window.salesChart.update();
            console.log('Chart colors updated for theme:', theme);
        } catch (error) {
            console.log('Error updating chart colors:', error);
        }
    }
    
    /**
     * Initializes the sales chart
     * @param {string} theme - The current theme ('light' or 'dark')
     */
    function initializeChart(theme) {
        const chartCanvas = document.getElementById('salesChart');
        if (!chartCanvas) {
            console.log('No chart canvas found on this page');
            return;
        }
        
        // Exit if Chart.js is not loaded or if chart is already initialized
        if (typeof Chart === 'undefined' || window.salesChart) {
            return;
        }
        
        try {
            const ctx = chartCanvas.getContext('2d');
            
            // Set colors based on theme
            const borderColor = theme === 'dark' ? 'rgba(50, 145, 255, 1)' : 'rgba(13, 110, 253, 1)';
            const backgroundColor = theme === 'dark' ? 'rgba(50, 145, 255, 0.2)' : 'rgba(13, 110, 253, 0.1)';
            const borderColor2 = theme === 'dark' ? 'rgba(50, 200, 100, 1)' : 'rgba(25, 135, 84, 1)';
            const backgroundColor2 = theme === 'dark' ? 'rgba(50, 200, 100, 0.2)' : 'rgba(25, 135, 84, 0.1)';
            
            window.salesChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                    datasets: [{
                        label: 'Sales',
                        data: [12, 19, 15, 17, 23, 25, 22, 28, 24, 29, 32, 35],
                        borderColor: borderColor,
                        backgroundColor: backgroundColor,
                        borderWidth: 2,
                        tension: 0.3,
                        fill: true
                    }, {
                        label: 'Revenue',
                        data: [8, 12, 10, 14, 16, 19, 18, 23, 20, 25, 27, 30],
                        borderColor: borderColor2,
                        backgroundColor: backgroundColor2,
                        borderWidth: 2,
                        tension: 0.3,
                        fill: true
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        }
                    },
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    },
                    interaction: {
                        intersect: false,
                        mode: 'index'
                    }
                }
            });
            
            console.log('Chart initialized successfully with theme:', theme);
        } catch (error) {
            console.error('Error initializing chart:', error);
        }
    }
    
    /**
     * Sets the application language and updates all UI elements
     * @param {string} lang - The language code (e.g., 'en', 'es')
     */
    function setLanguage(lang) {
        // Update the language display text
        let displayText = 'English';
        
        if (lang === 'es') {
            displayText = 'Español';
        }
        
        // Update the selected language text
        if (selectedLanguage) {
            selectedLanguage.textContent = displayText;
        }
        
        // Save the language preference
        localStorage.setItem('language', lang);
        
        // Get all elements with translation keys
        const elements = document.querySelectorAll('[data-translate-key]');
        console.log(`Found ${elements.length} elements with translation keys`);
        
        // Update each element with the corresponding translation
        elements.forEach(element => {
            const key = element.getAttribute('data-translate-key');
            
            if (!translations[lang] || !translations[lang][key]) {
                console.warn(`Missing translation for key: ${key} in language: ${lang}`);
                return;
            }
            
            const translation = translations[lang][key];
            
            if (translation) {
                // Find the span tag for text content (sidebar items)
                const textSpan = element.querySelector('.sidebar-item-text');
                if (textSpan) {
                    textSpan.textContent = translation;
                    console.log(`Updated sidebar item: ${key} to ${translation}`);
                }
                
                // For elements containing HTML (like welcome message)
                else if (key === 'welcome_message_admin') {
                    element.innerHTML = translation;
                    console.log(`Updated welcome message to ${translation}`);
                }
                
                // For elements without span (like dropdown items)
                else if (!textSpan && !element.querySelector('div')) {
                    // Skip elements that might have child elements
                    element.textContent = translation;
                    console.log(`Updated element: ${key} to ${translation}`);
                }
            }
        });
        
        console.log(`Language changed to: ${lang}`);
    }
    
    /**
     * Legacy function for backward compatibility, calls setLanguage()
     * @param {string} language - The language code (e.g., 'en', 'es')
     */
    function changeLanguage(language) {
        setLanguage(language);
    }
    
    /**
     * Initializes the theme based on localStorage or defaults to light
     */
    function initTheme() {
        const savedTheme = localStorage.getItem('theme') || 'light';
        
        // Set theme on both HTML and body elements
        document.documentElement.setAttribute('data-theme', savedTheme);
        document.body.setAttribute('data-bs-theme', savedTheme);
        
        // Check if theme icon exists
        if (themeIcon) {
            // Set the correct icon for the current theme
            if (savedTheme === 'dark') {
                themeIcon.classList.remove('fa-moon');
                themeIcon.classList.add('fa-sun');
            } else {
                themeIcon.classList.remove('fa-sun');
                themeIcon.classList.add('fa-moon');
            }
        } else {
            console.log('Theme icon element not found');
        }
        
        // Initialize or update chart if needed
        try {
            // First check if we need to initialize the chart
            const chartCanvas = document.getElementById('salesChart');
            if (chartCanvas) {
                if (typeof window.salesChart === 'undefined' || !window.salesChart) {
                    // Chart is not initialized yet, initialize it
                    if (typeof initializeChart === 'function') {
                        initializeChart(savedTheme);
                    }
                } else {
                    // Chart exists, just update colors
                    updateChartColors(savedTheme);
                }
            }
        } catch (error) {
            console.log('Error handling chart during theme initialization:', error);
        }
    }
    
    /**
     * Initializes the language based on localStorage or defaults to English
     */
    function initLanguage() {
        const savedLanguage = localStorage.getItem('language') || 'en';
        
        // Apply translations based on the saved language
        setLanguage(savedLanguage);
    }
    
    /**
     * Initializes the sidebar state based on localStorage
     */
    function initSidebar() {
        const sidebarCollapsed = localStorage.getItem('sidebarCollapsed') === 'true';
        
        if (sidebarCollapsed) {
            sidebar.classList.add('toggled');
            pageContent.classList.add('toggled');
        }
        
        // Handle submenu hover behavior when sidebar is collapsed
        const submenuItems = document.querySelectorAll('.sidebar a[data-bs-toggle="collapse"]');
        
        submenuItems.forEach(item => {
            const submenuId = item.getAttribute('href');
            const submenu = document.querySelector(submenuId);
            
            item.addEventListener('mouseenter', function() {
                if (sidebar.classList.contains('toggled')) {
                    const bsCollapse = new bootstrap.Collapse(submenu, {
                        toggle: false
                    });
                    bsCollapse.show();
                }
            });
            
            item.parentElement.addEventListener('mouseleave', function() {
                if (sidebar.classList.contains('toggled')) {
                    const bsCollapse = new bootstrap.Collapse(submenu, {
                        toggle: false
                    });
                    bsCollapse.hide();
                }
            });
        });
    }
    
    /**
     * Legacy function for backward compatibility, calls setLanguage()
     * @param {string} language - The language code to translate to
     */
    function translatePage(language) {
        setLanguage(language);
    }
    
    /**
     * Window resize event handler
     * Closes the sidebar when resizing from mobile to desktop
     */
    window.addEventListener('resize', function() {
        if (window.innerWidth >= 992 && overlay.classList.contains('show')) {
            toggleSidebar();
        }
    });
});