// Dashboard Dynamic Loading Script

// Function to ensure dashboard initializes properly
function ensureDashboardInit() {
    console.log('Dashboard.js loaded');
    
    // Force a layout recalculation on mobile devices
    if (window.innerWidth <= 768) {
        // Use double requestAnimationFrame for better mobile timing
        requestAnimationFrame(() => {
            requestAnimationFrame(() => {
                initializeDashboard();
            });
        });
    } else {
        initializeDashboard();
    }
}

// Handle different loading scenarios
if (document.readyState === 'loading') {
    // DOM is still loading
    document.addEventListener('DOMContentLoaded', ensureDashboardInit);
} else {
    // DOM is already loaded (script loaded async or deferred)
    ensureDashboardInit();
}

// Also listen for window load as a fallback
window.addEventListener('load', function() {
    // Ensure layout is correct after all resources loaded
    if (window.innerWidth <= 768) {
        setTimeout(() => {
            forceLayoutRecalculation();
        }, 100);
    }
});

// Initialize dashboard components
function initializeDashboard() {
    // Force layout recalculation
    forceLayoutRecalculation();
    
    // Load welcome message and video configuration
    loadWelcomeConfiguration();
    
    // Load statistics
    loadStatistics();
    
    // Setup resize handlers
    setupResizeHandlers();
    
    // Mark dashboard as loaded after a brief delay
    setTimeout(() => {
        const dashboardContainer = document.querySelector('.dashboard-container');
        if (dashboardContainer) {
            dashboardContainer.classList.add('loaded');
        }
    }, 100);
}

// Force layout recalculation to fix initial mobile rendering
function forceLayoutRecalculation() {
    // Get all cards and containers
    const cards = document.querySelectorAll('.card');
    const containers = document.querySelectorAll('.dashboard-container, .welcome-section, .statistics-section');
    
    // Force browser to recalculate styles
    containers.forEach(container => {
        container.style.display = 'none';
        container.offsetHeight; // Force reflow
        container.style.display = '';
    });
    
    // Recalculate card dimensions
    cards.forEach(card => {
        card.style.width = 'auto';
        card.offsetWidth; // Force reflow
        card.style.width = '';
    });
    
    // Fix video container aspect ratio
    fixVideoContainers();
}

// Fix video container dimensions
function fixVideoContainers() {
    const videoContainers = document.querySelectorAll('.video-container');
    videoContainers.forEach(container => {
        if (container.querySelector('iframe, video')) {
            // Force recalculation of padding-bottom for aspect ratio
            container.style.paddingBottom = '0';
            container.offsetHeight;
            container.style.paddingBottom = '56.25%';
        }
    });
}

// Setup resize handlers with debouncing
function setupResizeHandlers() {
    let resizeTimeout;
    
    window.addEventListener('resize', function() {
        clearTimeout(resizeTimeout);
        resizeTimeout = setTimeout(() => {
            // Recalculate layouts on resize
            forceLayoutRecalculation();
            
            // Fix any video containers
            fixVideoContainers();
        }, 250); // Debounce for 250ms
    });
}

// Load Welcome Message Configuration
async function loadWelcomeConfiguration() {
    console.log('Loading welcome configuration...');
    try {
        const response = await fetch('/api/dashboard/welcome-message-config');
        console.log('API Response status:', response.status);
        
        if (!response.ok) {
            throw new Error('Error al cargar configuración: ' + response.status);
        }

        const data = await response.json();
        
        // Update welcome title
        const titleElement = document.getElementById('welcomeTitle');
        if (titleElement) {
            titleElement.textContent = data.title || 'Bienvenido';
        }

        // Update welcome content
        const contentElement = document.getElementById('welcomeContent');
        if (contentElement) {
            contentElement.innerHTML = data.htmlContent || '<p>Bienvenido al dashboard</p>';
        }

        // Handle video
        const videoContainer = document.getElementById('videoContainer');
        if (videoContainer && data.videoType && data.videoType !== 'None') {
            renderVideo(videoContainer, data.videoType, data.videoUrl);
        } else if (videoContainer) {
            // No video configured
            videoContainer.innerHTML = '<div class="d-flex justify-content-center align-items-center" style="min-height: 200px; height: 100%;"><p class="text-muted">No hay video configurado</p></div>';
        }

    } catch (error) {
        console.error('Error loading welcome configuration:', error);
        
        // Show error messages
        const titleElement = document.getElementById('welcomeTitle');
        if (titleElement) {
            titleElement.textContent = 'Error al cargar';
        }

        const contentElement = document.getElementById('welcomeContent');
        if (contentElement) {
            contentElement.innerHTML = '<p class="text-danger">Error al cargar el mensaje de bienvenida</p>';
        }
    }
}

// Render video based on type
function renderVideo(container, videoType, videoUrl) {
    if (!videoUrl) {
        container.innerHTML = '<div class="d-flex justify-content-center align-items-center" style="min-height: 200px; height: 100%;"><p class="text-muted">URL de video no configurada</p></div>';
        return;
    }

    let videoHtml = '';
    
    switch(videoType) {
        case 'YouTube':
            // Extract YouTube video ID
            const youtubeId = extractYouTubeId(videoUrl);
            if (youtubeId) {
                videoHtml = `<iframe src="https://www.youtube.com/embed/${youtubeId}" frameborder="0" allowfullscreen></iframe>`;
            } else {
                videoHtml = '<div class="d-flex justify-content-center align-items-center" style="min-height: 200px; height: 100%;"><p class="text-danger">URL de YouTube inválida</p></div>';
            }
            break;
            
        case 'Vimeo':
            // Extract Vimeo video ID
            const vimeoId = extractVimeoId(videoUrl);
            if (vimeoId) {
                videoHtml = `<iframe src="https://player.vimeo.com/video/${vimeoId}" frameborder="0" allowfullscreen></iframe>`;
            } else {
                videoHtml = '<div class="d-flex justify-content-center align-items-center" style="min-height: 200px; height: 100%;"><p class="text-danger">URL de Vimeo inválida</p></div>';
            }
            break;
            
        case 'Uploaded':
            // Use HTML5 video player for uploaded videos
            videoHtml = `
                <video controls style="width: 100%; height: 100%; object-fit: contain;">
                    <source src="${videoUrl}" type="video/mp4">
                    Tu navegador no soporta la reproducción de videos.
                </video>
            `;
            break;
            
        default:
            videoHtml = '<div class="d-flex justify-content-center align-items-center" style="height: 300px;"><p class="text-muted">Tipo de video no soportado</p></div>';
    }
    
    container.innerHTML = videoHtml;
    
    // Force layout recalculation after video is inserted
    requestAnimationFrame(() => {
        fixVideoContainers();
    });
}

// Extract YouTube video ID from URL
function extractYouTubeId(url) {
    const regex = /(?:youtube\.com\/watch\?v=|youtu\.be\/|youtube\.com\/embed\/)([^&\n?#]+)/;
    const match = url.match(regex);
    return match ? match[1] : null;
}

// Extract Vimeo video ID from URL
function extractVimeoId(url) {
    const regex = /(?:vimeo\.com\/)(\d+)/;
    const match = url.match(regex);
    return match ? match[1] : null;
}

// Load Statistics
async function loadStatistics() {
    // Load total clients
    const totalClientesElement = document.getElementById('totalClientes');
    if (totalClientesElement) {
        try {
            const response = await fetch('/api/dashboard/stats/total-clientes');
            const data = await response.json();
            
            if (response.ok) {
                totalClientesElement.textContent = data.total || '0';
            } else {
                totalClientesElement.innerHTML = '<span class="text-danger">Error</span>';
            }
        } catch (error) {
            console.error('Error loading total clients:', error);
            totalClientesElement.innerHTML = '<span class="text-danger">Error</span>';
        }
    }

    // Load active clients
    const clientesActivosElement = document.getElementById('clientesActivos');
    if (clientesActivosElement) {
        try {
            const response = await fetch('/api/dashboard/stats/clientes-activos');
            const data = await response.json();
            
            if (response.ok) {
                clientesActivosElement.textContent = data.total || '0';
            } else {
                clientesActivosElement.innerHTML = '<span class="text-danger">Error</span>';
            }
        } catch (error) {
            console.error('Error loading active clients:', error);
            clientesActivosElement.innerHTML = '<span class="text-danger">Error</span>';
        }
    }
}

// Refresh statistics every 30 seconds
setInterval(loadStatistics, 30000);