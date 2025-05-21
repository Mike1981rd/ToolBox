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