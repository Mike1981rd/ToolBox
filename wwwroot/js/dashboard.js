// Dashboard Dynamic Loading Script
document.addEventListener('DOMContentLoaded', function() {
    console.log('Dashboard.js loaded and DOMContentLoaded fired');
    
    // Load welcome message and video configuration
    loadWelcomeConfiguration();
    
    // Load statistics
    loadStatistics();
});

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
            videoContainer.innerHTML = '<div class="d-flex justify-content-center align-items-center" style="height: 300px;"><p class="text-muted">No hay video configurado</p></div>';
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
        container.innerHTML = '<div class="d-flex justify-content-center align-items-center" style="height: 300px;"><p class="text-muted">URL de video no configurada</p></div>';
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
                videoHtml = '<div class="d-flex justify-content-center align-items-center" style="height: 300px;"><p class="text-danger">URL de YouTube inválida</p></div>';
            }
            break;
            
        case 'Vimeo':
            // Extract Vimeo video ID
            const vimeoId = extractVimeoId(videoUrl);
            if (vimeoId) {
                videoHtml = `<iframe src="https://player.vimeo.com/video/${vimeoId}" frameborder="0" allowfullscreen></iframe>`;
            } else {
                videoHtml = '<div class="d-flex justify-content-center align-items-center" style="height: 300px;"><p class="text-danger">URL de Vimeo inválida</p></div>';
            }
            break;
            
        case 'Uploaded':
            // Use HTML5 video player for uploaded videos
            videoHtml = `
                <video controls style="width: 100%; height: 100%;">
                    <source src="${videoUrl}" type="video/mp4">
                    Tu navegador no soporta la reproducción de videos.
                </video>
            `;
            break;
            
        default:
            videoHtml = '<div class="d-flex justify-content-center align-items-center" style="height: 300px;"><p class="text-muted">Tipo de video no soportado</p></div>';
    }
    
    container.innerHTML = videoHtml;
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