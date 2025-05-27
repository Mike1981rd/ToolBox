// Sessions File Upload Functionality (shared between Create and Edit views)
document.addEventListener('DOMContentLoaded', function() {
    // File upload and preview functionality
    let selectedFiles = [];
    const fileInput = document.getElementById('attachments');
    const filePreviewContainer = document.getElementById('filePreviewContainer');
    const filePreviewList = document.getElementById('filePreviewList');
    const maxFileSize = 10 * 1024 * 1024; // 10MB
    const allowedExtensions = ['jpg', 'jpeg', 'png', 'pdf', 'doc', 'docx', 'mp4', 'mov'];

    // File type icons
    const fileIcons = {
        pdf: 'fas fa-file-pdf text-danger',
        doc: 'fas fa-file-word text-primary',
        docx: 'fas fa-file-word text-primary',
        mp4: 'fas fa-file-video text-info',
        mov: 'fas fa-file-video text-info',
        default: 'fas fa-file text-secondary'
    };

    if (fileInput) {
        fileInput.addEventListener('change', function(e) {
            handleFileSelect(e.target.files);
        });
    }

    // Drag and drop functionality
    const uploadLabel = document.querySelector('.file-upload-label');
    
    if (uploadLabel) {
        ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
            uploadLabel.addEventListener(eventName, preventDefaults, false);
        });

        ['dragenter', 'dragover'].forEach(eventName => {
            uploadLabel.addEventListener(eventName, highlight, false);
        });

        ['dragleave', 'drop'].forEach(eventName => {
            uploadLabel.addEventListener(eventName, unhighlight, false);
        });

        uploadLabel.addEventListener('drop', handleDrop, false);
    }

    function preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }

    function highlight(e) {
        uploadLabel.classList.add('file-upload-highlight');
    }

    function unhighlight(e) {
        uploadLabel.classList.remove('file-upload-highlight');
    }

    function handleDrop(e) {
        const dt = e.dataTransfer;
        const files = dt.files;
        handleFileSelect(files);
    }

    function handleFileSelect(files) {
        const newFiles = Array.from(files);
        
        newFiles.forEach(file => {
            const extension = file.name.split('.').pop().toLowerCase();
            
            // Validate file
            if (!allowedExtensions.includes(extension)) {
                showError(`Formato no permitido: ${file.name}`);
                return;
            }
            
            if (file.size > maxFileSize) {
                showError(`Archivo muy grande: ${file.name} (Max: 10MB)`);
                return;
            }
            
            // Add to selected files if not already present
            if (!selectedFiles.some(f => f.name === file.name && f.size === file.size)) {
                selectedFiles.push(file);
            }
        });

        updateFileInput();
        updatePreview();
    }

    function updateFileInput() {
        // Create a new DataTransfer object
        const dataTransfer = new DataTransfer();
        
        // Add selected files to the DataTransfer object
        selectedFiles.forEach(file => {
            dataTransfer.items.add(file);
        });
        
        // Update the file input's files
        fileInput.files = dataTransfer.files;
    }

    function updatePreview() {
        filePreviewList.innerHTML = '';
        
        if (selectedFiles.length === 0) {
            filePreviewContainer.style.display = 'none';
            return;
        }
        
        filePreviewContainer.style.display = 'block';
        
        selectedFiles.forEach((file, index) => {
            const fileItem = createFilePreviewItem(file, index);
            filePreviewList.appendChild(fileItem);
        });
    }

    function createFilePreviewItem(file, index) {
        const div = document.createElement('div');
        div.className = 'file-preview-item';
        
        const extension = file.name.split('.').pop().toLowerCase();
        const isImage = ['jpg', 'jpeg', 'png'].includes(extension);
        const isVideo = ['mp4', 'mov'].includes(extension);
        
        let previewContent = '';
        
        if (isImage) {
            // Create image preview
            const reader = new FileReader();
            reader.onload = function(e) {
                div.querySelector('.file-preview-image').innerHTML = 
                    `<img src="${e.target.result}" alt="${file.name}">`;
            };
            reader.readAsDataURL(file);
            previewContent = '<div class="file-preview-image"><div class="spinner-border spinner-border-sm" role="status"></div></div>';
        } else if (isVideo) {
            // Create video preview with thumbnail
            const reader = new FileReader();
            reader.onload = function(e) {
                const video = document.createElement('video');
                video.src = e.target.result;
                video.style.width = '100%';
                video.style.height = '100%';
                video.style.objectFit = 'cover';
                video.preload = 'metadata';
                video.muted = true;
                
                video.addEventListener('loadedmetadata', function() {
                    video.currentTime = 1; // Seek to 1 second for thumbnail
                });
                
                video.addEventListener('seeked', function() {
                    const canvas = document.createElement('canvas');
                    canvas.width = 48;
                    canvas.height = 48;
                    const ctx = canvas.getContext('2d');
                    ctx.drawImage(video, 0, 0, 48, 48);
                    
                    const thumbnail = document.createElement('img');
                    thumbnail.src = canvas.toDataURL();
                    thumbnail.style.width = '100%';
                    thumbnail.style.height = '100%';
                    thumbnail.style.objectFit = 'cover';
                    thumbnail.title = 'Video: ' + file.name;
                    
                    div.querySelector('.file-preview-video').innerHTML = '';
                    div.querySelector('.file-preview-video').appendChild(thumbnail);
                    
                    // Add play icon overlay
                    const playIcon = document.createElement('div');
                    playIcon.innerHTML = '<i class="fas fa-play-circle" style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); color: white; font-size: 16px; text-shadow: 0 0 4px rgba(0,0,0,0.5);"></i>';
                    playIcon.style.position = 'absolute';
                    playIcon.style.top = '0';
                    playIcon.style.left = '0';
                    playIcon.style.width = '100%';
                    playIcon.style.height = '100%';
                    playIcon.style.pointerEvents = 'none';
                    div.querySelector('.file-preview-video').style.position = 'relative';
                    div.querySelector('.file-preview-video').appendChild(playIcon);
                });
            };
            reader.readAsDataURL(file);
            previewContent = '<div class="file-preview-video" style="width: 48px; height: 48px; margin-right: 1rem; overflow: hidden; border-radius: 0.25rem; display: flex; align-items: center; justify-content: center; background-color: #f8f9fa;"><div class="spinner-border spinner-border-sm" role="status"></div></div>';
        } else {
            // Use icon for other file types
            const iconClass = fileIcons[extension] || fileIcons.default;
            previewContent = `<div class="file-preview-icon"><i class="${iconClass} fa-2x"></i></div>`;
        }
        
        div.innerHTML = `
            ${previewContent}
            <div class="file-preview-info">
                <div class="file-name">${file.name}</div>
                <div class="file-size">${formatFileSize(file.size)}</div>
            </div>
            <button type="button" class="btn btn-sm btn-outline-danger file-remove-btn" data-index="${index}">
                <i class="fas fa-times"></i>
            </button>
        `;
        
        // Add remove functionality
        div.querySelector('.file-remove-btn').addEventListener('click', function() {
            removeFile(index);
        });
        
        return div;
    }

    function removeFile(index) {
        selectedFiles.splice(index, 1);
        updateFileInput();
        updatePreview();
    }

    function formatFileSize(bytes) {
        if (bytes === 0) return '0 Bytes';
        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(k));
        return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
    }

    function showError(message) {
        const errorElement = document.getElementById('attachmentErrors');
        if (errorElement) {
            errorElement.textContent = message;
            setTimeout(() => {
                errorElement.textContent = '';
            }, 5000);
        }
    }

    // Make functions available globally if needed
    window.sessionsFileUpload = {
        formatFileSize: formatFileSize,
        showError: showError
    };
});