// Welcome Message JavaScript Module
document.addEventListener('DOMContentLoaded', function() {
    console.log('Welcome Message module initialized');
    
    // Global variables
    let welcomeMessageEditor = null;
    let currentSettings = null;
    
    // Initialize the module
    initializeWelcomeMessage();
    
    function initializeWelcomeMessage() {
        // Initialize TinyMCE editor
        initializeTinyMCEEditor();
        
        // Load current settings from server
        loadCurrentSettings();
        
        // Initialize event listeners
        initializeEventListeners();
        
        // Show/hide video sections based on current selection
        updateVideoSectionVisibility();
    }
    
    // Initialize TinyMCE Editor
    function initializeTinyMCEEditor() {
        if (typeof tinymce !== 'undefined') {
            tinymce.init({
                selector: '#description',
                height: 300,
                menubar: true,
                plugins: [
                    'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
                    'anchor', 'searchreplace', 'visualblocks', 'code', 'fullscreen',
                    'insertdatetime', 'media', 'table', 'help', 'wordcount'
                ],
                toolbar: 'undo redo | blocks | ' +
                         'bold italic forecolor backcolor | alignleft aligncenter ' +
                         'alignright alignjustify | bullist numlist outdent indent | ' +
                         'removeformat | image media link | code fullscreen preview | help',
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
                language: 'es',
                setup: function(editor) {
                    welcomeMessageEditor = editor;
                    
                    // Update preview when content changes
                    editor.on('change', function() {
                        updateDescriptionPreview();
                    });
                    
                    editor.on('init', function() {
                        console.log('TinyMCE initialized successfully');
                        // Update preview after initialization
                        updatePreview();
                    });
                }
            });
        } else {
            console.warn('TinyMCE not loaded, retrying...');
            setTimeout(initializeTinyMCEEditor, 500);
        }
    }
    
    // Load current settings from API
    async function loadCurrentSettings() {
        try {
            const response = await fetch('/api/welcomemessage/settings', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            
            const result = await response.json();
            
            if (result.success && result.data) {
                currentSettings = result.data;
                populateForm(result.data);
                updatePreview();
            }
        } catch (error) {
            console.error('Error loading settings:', error);
            showNotification('Error al cargar la configuración', 'error');
        }
    }
    
    // Populate form with current settings
    function populateForm(settings) {
        // Set title
        const titleInput = document.getElementById('title');
        if (titleInput) {
            titleInput.value = settings.title || '';
        }
        
        // Set description in TinyMCE (wait for editor to be ready)
        if (welcomeMessageEditor) {
            welcomeMessageEditor.setContent(settings.descriptionHTML || '');
        } else {
            // If editor not ready, wait and retry
            setTimeout(() => {
                if (welcomeMessageEditor) {
                    welcomeMessageEditor.setContent(settings.descriptionHTML || '');
                }
            }, 1000);
        }
        
        // Set video type
        const videoType = settings.videoType || 'None';
        const videoTypeRadio = document.querySelector(`input[name="videoType"][value="${videoType}"]`);
        if (videoTypeRadio) {
            videoTypeRadio.checked = true;
        }
        
        // Set video URLs based on type
        if (videoType === 'YouTube' && settings.videoUrl) {
            const youtubeInput = document.getElementById('youtubeUrl');
            if (youtubeInput) {
                youtubeInput.value = settings.videoUrl;
            }
        } else if (videoType === 'Vimeo' && settings.videoUrl) {
            const vimeoInput = document.getElementById('vimeoUrl');
            if (vimeoInput) {
                vimeoInput.value = settings.videoUrl;
            }
        } else if (videoType === 'Uploaded' && settings.uploadedVideoPath) {
            // Show current uploaded video info
            showUploadedVideoInfo(settings.uploadedVideoFileName, settings.uploadedVideoPath);
        }
        
        // Update video section visibility
        updateVideoSectionVisibility();
    }
    
    // Initialize event listeners
    function initializeEventListeners() {
        // Form submission
        const form = document.getElementById('welcomeMessageForm');
        if (form) {
            form.addEventListener('submit', handleFormSubmit);
        }
        
        // Preview button
        const previewBtn = document.querySelector('.btn-preview');
        if (previewBtn) {
            previewBtn.addEventListener('click', updatePreview);
        }
        
        // Title input change
        const titleInput = document.getElementById('title');
        if (titleInput) {
            titleInput.addEventListener('input', updateTitlePreview);
        }
        
        // Video type radio buttons
        const videoTypeRadios = document.querySelectorAll('input[name="videoType"]');
        videoTypeRadios.forEach(radio => {
            radio.addEventListener('change', function() {
                updateVideoSectionVisibility();
                updateVideoPreview();
            });
        });
        
        // Video URL inputs
        const youtubeInput = document.getElementById('youtubeUrl');
        const vimeoInput = document.getElementById('vimeoUrl');
        
        if (youtubeInput) {
            youtubeInput.addEventListener('input', debounce(updateVideoPreview, 500));
        }
        
        if (vimeoInput) {
            vimeoInput.addEventListener('input', debounce(updateVideoPreview, 500));
        }
        
        // Video file input
        const videoFileInput = document.getElementById('videoFile');
        if (videoFileInput) {
            videoFileInput.addEventListener('change', handleVideoFileChange);
        }
    }
    
    // Handle form submission
    async function handleFormSubmit(e) {
        e.preventDefault();
        
        const submitBtn = document.querySelector('.btn-save');
        if (!submitBtn) return;
        
        // Show loading state
        submitBtn.disabled = true;
        submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Guardando...';
        
        try {
            // Get form data
            const formData = new FormData();
            
            // Get title
            const title = document.getElementById('title').value;
            formData.append('Title', title);
            
            // Get description from TinyMCE
            if (welcomeMessageEditor) {
                const descriptionHTML = welcomeMessageEditor.getContent();
                formData.append('DescriptionHTML', descriptionHTML);
            }
            
            // Get video type
            const videoType = document.querySelector('input[name="videoType"]:checked').value;
            formData.append('VideoType', videoType);
            
            // Handle video data based on type
            switch (videoType) {
                case 'YouTube':
                    const youtubeUrl = document.getElementById('youtubeUrl').value;
                    formData.append('VideoUrl', youtubeUrl);
                    break;
                    
                case 'Vimeo':
                    const vimeoUrl = document.getElementById('vimeoUrl').value;
                    formData.append('VideoUrl', vimeoUrl);
                    break;
                    
                case 'Uploaded':
                    const videoFile = document.getElementById('videoFile').files[0];
                    if (videoFile) {
                        formData.append('VideoFile', videoFile);
                    }
                    break;
            }
            
            // Get CSRF token
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            
            // Send to API
            const response = await fetch('/api/welcomemessage/settings', {
                method: 'PUT',
                headers: {
                    'RequestVerificationToken': token
                },
                body: formData
            });
            
            const result = await response.json();
            
            if (result.success) {
                showNotification('Mensaje de bienvenida guardado exitosamente', 'success');
                // Reload settings to ensure we have the latest data
                await loadCurrentSettings();
            } else {
                showNotification(result.message || 'Error al guardar', 'error');
            }
            
        } catch (error) {
            console.error('Error saving settings:', error);
            showNotification('Error al guardar la configuración', 'error');
        } finally {
            // Restore button state
            submitBtn.disabled = false;
            submitBtn.innerHTML = '<i class="fas fa-save me-2"></i>Guardar Cambios';
        }
    }
    
    // Update entire preview
    function updatePreview() {
        updateTitlePreview();
        updateDescriptionPreview();
        updateVideoPreview();
    }
    
    // Update title preview
    function updateTitlePreview() {
        const title = document.getElementById('title').value;
        const previewTitle = document.getElementById('previewTitle');
        
        if (previewTitle) {
            previewTitle.textContent = title || 'Título del mensaje';
        }
    }
    
    // Update description preview
    function updateDescriptionPreview() {
        if (welcomeMessageEditor) {
            const content = welcomeMessageEditor.getContent();
            const previewDescription = document.getElementById('previewDescription');
            
            if (previewDescription) {
                previewDescription.innerHTML = content || '<p>Descripción del mensaje...</p>';
            }
        }
    }
    
    // Update video preview
    function updateVideoPreview() {
        const videoType = document.querySelector('input[name="videoType"]:checked')?.value || 'None';
        const previewVideo = document.getElementById('previewVideo');
        
        if (!previewVideo) return;
        
        switch(videoType) {
            case 'None':
                previewVideo.innerHTML = '';
                break;
                
            case 'Uploaded':
                const videoFile = document.getElementById('videoFile').files[0];
                if (videoFile) {
                    previewUploadedVideo(videoFile);
                } else if (currentSettings && currentSettings.uploadedVideoPath) {
                    // Show existing uploaded video
                    previewVideo.innerHTML = `
                        <video controls class="w-100">
                            <source src="${currentSettings.uploadedVideoPath}" type="video/mp4">
                            Tu navegador no soporta el elemento video.
                        </video>`;
                } else {
                    previewVideo.innerHTML = '<p class="text-muted">Selecciona un archivo de video</p>';
                }
                break;
                
            case 'YouTube':
                const youtubeUrl = document.getElementById('youtubeUrl').value;
                if (youtubeUrl) {
                    previewYouTubeVideo(youtubeUrl);
                } else {
                    previewVideo.innerHTML = '<p class="text-muted">Ingresa una URL de YouTube</p>';
                }
                break;
                
            case 'Vimeo':
                const vimeoUrl = document.getElementById('vimeoUrl').value;
                if (vimeoUrl) {
                    previewVimeoVideo(vimeoUrl);
                } else {
                    previewVideo.innerHTML = '<p class="text-muted">Ingresa una URL de Vimeo</p>';
                }
                break;
        }
    }
    
    // Preview uploaded video
    function previewUploadedVideo(file) {
        const previewVideo = document.getElementById('previewVideo');
        const url = URL.createObjectURL(file);
        
        previewVideo.innerHTML = `
            <video controls class="w-100">
                <source src="${url}" type="${file.type}">
                Tu navegador no soporta el elemento video.
            </video>`;
    }
    
    // Preview YouTube video
    function previewYouTubeVideo(url) {
        const previewVideo = document.getElementById('previewVideo');
        const videoId = extractYouTubeVideoId(url);
        
        if (videoId) {
            previewVideo.innerHTML = `
                <div class="ratio ratio-16x9">
                    <iframe 
                        src="https://www.youtube.com/embed/${videoId}" 
                        allowfullscreen>
                    </iframe>
                </div>`;
        } else {
            previewVideo.innerHTML = '<p class="text-muted">URL de YouTube no válida</p>';
        }
    }
    
    // Preview Vimeo video
    function previewVimeoVideo(url) {
        const previewVideo = document.getElementById('previewVideo');
        const videoId = extractVimeoVideoId(url);
        
        if (videoId) {
            previewVideo.innerHTML = `
                <div class="ratio ratio-16x9">
                    <iframe 
                        src="https://player.vimeo.com/video/${videoId}" 
                        allowfullscreen>
                    </iframe>
                </div>`;
        } else {
            previewVideo.innerHTML = '<p class="text-muted">URL de Vimeo no válida</p>';
        }
    }
    
    // Extract YouTube video ID from URL
    function extractYouTubeVideoId(url) {
        const regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
        const match = url.match(regExp);
        return (match && match[2].length === 11) ? match[2] : null;
    }
    
    // Extract Vimeo video ID from URL
    function extractVimeoVideoId(url) {
        const regExp = /(?:vimeo)\.com.*(?:videos|video|channels|)\/([\d]+)/i;
        const match = url.match(regExp);
        return match ? match[1] : null;
    }
    
    // Update video section visibility
    function updateVideoSectionVisibility() {
        const videoType = document.querySelector('input[name="videoType"]:checked')?.value || 'None';
        
        // Hide all sections first
        document.querySelectorAll('.video-section').forEach(section => {
            section.style.display = 'none';
        });
        
        // Show the appropriate section
        switch(videoType) {
            case 'Uploaded':
                document.getElementById('uploadVideoSection').style.display = 'block';
                break;
            case 'YouTube':
                document.getElementById('youtubeVideoSection').style.display = 'block';
                break;
            case 'Vimeo':
                document.getElementById('vimeoVideoSection').style.display = 'block';
                break;
        }
    }
    
    // Handle video file change
    function handleVideoFileChange(e) {
        const file = e.target.files[0];
        if (file) {
            // Sin validaciones según requisitos
            // Update preview
            updateVideoPreview();
        }
    }
    
    // Show uploaded video info
    function showUploadedVideoInfo(fileName, filePath) {
        const uploadSection = document.getElementById('uploadVideoSection');
        if (uploadSection && fileName) {
            // Add info about current video
            const infoDiv = uploadSection.querySelector('.current-video-info') || 
                           document.createElement('div');
            infoDiv.className = 'current-video-info alert alert-info mt-2';
            infoDiv.innerHTML = `
                <i class="fas fa-info-circle me-2"></i>
                Video actual: <strong>${fileName}</strong>
            `;
            
            if (!uploadSection.querySelector('.current-video-info')) {
                uploadSection.appendChild(infoDiv);
            }
        }
    }
    
    // Show notification
    function showNotification(message, type = 'info') {
        // Create notification container if it doesn't exist
        let container = document.getElementById('notification-container');
        if (!container) {
            container = document.createElement('div');
            container.id = 'notification-container';
            container.className = 'position-fixed top-0 end-0 p-3';
            container.style.zIndex = '9999';
            document.body.appendChild(container);
        }
        
        // Create notification
        const notification = document.createElement('div');
        notification.className = `alert alert-${type === 'error' ? 'danger' : type} alert-dismissible fade show`;
        notification.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;
        
        container.appendChild(notification);
        
        // Auto remove after 5 seconds
        setTimeout(() => {
            notification.remove();
        }, 5000);
    }
    
    // Debounce function for input events
    function debounce(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }
});