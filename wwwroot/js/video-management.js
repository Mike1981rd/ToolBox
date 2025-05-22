/**
 * Video Management JavaScript
 * Handles CRUD operations for the Video Management module
 */

const VideoManagement = {
    videosData: [],
    currentPage: 1,
    pageSize: 10,
    totalPages: 1,
    currentSearchTerm: '',
    richTextEditor: null,

    // Initialize the module
    init: function() {
        if (document.getElementById('videosTable')) {
            // We're on the Index page
            this.initializeIndex();
        }
    },

    // Initialize Index page
    initializeIndex: function() {
        this.loadVideos();
        this.setupIndexEventListeners();
    },

    // Initialize Form page
    initializeForm: function(isEditMode) {
        this.setupFormEventListeners();
        this.loadDropdownData();
        this.initializeRichTextEditor();
        
        if (isEditMode) {
            this.populateFormData();
        }
    },

    // Setup event listeners for Index page
    setupIndexEventListeners: function() {
        // Search functionality
        const searchInput = document.getElementById('videosSearchInput');
        if (searchInput) {
            let searchTimeout;
            searchInput.addEventListener('input', (e) => {
                clearTimeout(searchTimeout);
                searchTimeout = setTimeout(() => {
                    this.currentSearchTerm = e.target.value;
                    this.currentPage = 1;
                    this.loadVideos();
                }, 300);
            });
        }

        // Page size change
        const pageSizeSelect = document.getElementById('videosPageSize');
        if (pageSizeSelect) {
            pageSizeSelect.addEventListener('change', (e) => {
                this.pageSize = parseInt(e.target.value);
                this.currentPage = 1;
                this.loadVideos();
            });
        }
    },

    // Setup event listeners for Form page
    setupFormEventListeners: function() {
        // Form submission
        const videoForm = document.getElementById('videoForm');
        if (videoForm) {
            videoForm.addEventListener('submit', (e) => {
                e.preventDefault();
                this.saveVideo();
            });
        }

        // File upload handling
        const videoFileInput = document.getElementById('videoFile');
        if (videoFileInput) {
            videoFileInput.addEventListener('change', (e) => {
                this.handleFileUpload(e.target.files[0]);
            });
        }

        // Media type change handling
        const mediaTypeSelect = document.getElementById('mediaType');
        if (mediaTypeSelect) {
            mediaTypeSelect.addEventListener('change', (e) => {
                this.handleMediaTypeChange(e.target.value);
            });
        }

        // Video URL validation
        const videoUrlInput = document.getElementById('videoUrl');
        if (videoUrlInput) {
            videoUrlInput.addEventListener('blur', (e) => {
                this.validateVideoUrl(e.target.value);
            });
        }
    },

    // Load videos from server
    loadVideos: function() {
        this.showLoadingState();

        const params = new URLSearchParams({
            searchTerm: this.currentSearchTerm,
            page: this.currentPage,
            pageSize: this.pageSize
        });

        fetch(`/VideoManagement/GetVideos?${params}`)
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    this.videosData = data.data;
                    this.totalPages = data.totalPages;
                    this.renderVideosTable();
                    this.renderPagination();
                    this.updateTableInfo(data.totalRecords);
                } else {
                    this.showError('Error loading videos: ' + data.message);
                }
            })
            .catch(error => {
                console.error('Error:', error);
                this.showError('Error loading videos');
            });
    },

    // Render videos table
    renderVideosTable: function() {
        const tbody = document.getElementById('videosTableBody');
        if (!tbody) return;

        if (this.videosData.length === 0) {
            tbody.innerHTML = `
                <tr>
                    <td colspan="6" class="text-center text-muted py-4">
                        <i class="fas fa-video fa-2x mb-2 d-block"></i>
                        No videos found
                    </td>
                </tr>
            `;
            return;
        }

        tbody.innerHTML = this.videosData.map(video => `
            <tr>
                <td class="dt-checkboxes-cell">
                    <input type="checkbox" class="form-check-input dt-checkboxes" value="${video.id}">
                </td>
                <td>
                    <div class="d-flex align-items-center">
                        <div class="me-3">
                            <div class="avatar avatar-sm bg-label-primary bg-opacity-25">
                                <i class="fas fa-${this.getMediaTypeIcon(video.mediaType)}"></i>
                            </div>
                        </div>
                        <div>
                            <div class="d-flex align-items-center">
                                <span class="fw-semibold">${this.escapeHtml(video.title)}</span>
                                ${video.isFeatured ? '<span class="badge bg-label-primary ms-2" data-translate-key="labels.featuredBadge">Featured</span>' : ''}
                            </div>
                            <small class="text-muted">${this.escapeHtml(video.description ? video.description.substring(0, 50) + '...' : 'No description')}</small>
                        </div>
                    </div>
                </td>
                <td>
                    <span class="badge topic-badge bg-label-info">${this.escapeHtml(video.topicName)}</span>
                </td>
                <td>
                    <div class="d-flex align-items-center">
                        <div class="avatar avatar-xs me-2">
                            <div class="avatar-initial bg-label-secondary rounded-circle">
                                <span class="fw-medium">${video.authorName.charAt(0)}</span>
                            </div>
                        </div>
                        <span>${this.escapeHtml(video.authorName)}</span>
                    </div>
                </td>
                <td>
                    <span class="text-muted">${this.formatDate(video.uploadDate)}</span>
                    ${video.duration ? `<br><small class="text-muted"><i class="fas fa-clock me-1"></i>${video.duration}</small>` : ''}
                </td>
                <td>
                    <div class="dropdown">
                        <button type="button" class="btn btn-sm btn-icon dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                            <i class="fas fa-ellipsis-v"></i>
                        </button>
                        <div class="dropdown-menu">
                            <a class="dropdown-item" href="/VideoManagement/VideoForm/${video.id}">
                                <i class="fas fa-edit me-1"></i> Edit
                            </a>
                            <a class="dropdown-item text-danger" href="javascript:void(0);" onclick="VideoManagement.deleteVideo(${video.id})">
                                <i class="fas fa-trash-alt me-1"></i> Delete
                            </a>
                        </div>
                    </div>
                </td>
            </tr>
        `).join('');
    },

    // Render pagination
    renderPagination: function() {
        const pagination = document.getElementById('videosPagination');
        if (!pagination) return;

        let paginationHtml = '';

        // Previous button
        paginationHtml += `
            <li class="paginate_button page-item ${this.currentPage === 1 ? 'disabled' : ''}">
                <a href="#" class="page-link" onclick="VideoManagement.goToPage(${this.currentPage - 1})">«</a>
            </li>
        `;

        // Page numbers
        const startPage = Math.max(1, this.currentPage - 2);
        const endPage = Math.min(this.totalPages, this.currentPage + 2);

        for (let i = startPage; i <= endPage; i++) {
            paginationHtml += `
                <li class="paginate_button page-item ${i === this.currentPage ? 'active' : ''}">
                    <a href="#" class="page-link" onclick="VideoManagement.goToPage(${i})">${i}</a>
                </li>
            `;
        }

        // Next button
        paginationHtml += `
            <li class="paginate_button page-item ${this.currentPage === this.totalPages ? 'disabled' : ''}">
                <a href="#" class="page-link" onclick="VideoManagement.goToPage(${this.currentPage + 1})">»</a>
            </li>
        `;

        pagination.innerHTML = paginationHtml;
    },

    // Go to specific page
    goToPage: function(page) {
        if (page < 1 || page > this.totalPages || page === this.currentPage) return;
        this.currentPage = page;
        this.loadVideos();
    },

    // Delete video
    deleteVideo: function(videoId) {
        const confirmMessage = this.getTranslation('confirm_messages.deleteVideo') || 'Are you sure you want to delete this video?';
        
        if (!confirm(confirmMessage)) return;

        fetch('/VideoManagement/DeleteVideo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ videoId: videoId })
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                this.showSuccess('Video deleted successfully');
                this.loadVideos();
            } else {
                this.showError(data.message || 'Error deleting video');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            this.showError('Error deleting video');
        });
    },

    // Load dropdown data for form
    loadDropdownData: function() {
        // Load topics
        fetch('/VideoManagement/GetTopicsForDropdown')
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    this.populateDropdown('topicId', data.data, 'id', 'name');
                }
            })
            .catch(error => console.error('Error loading topics:', error));

        // Load users
        fetch('/VideoManagement/GetUsersForDropdown')
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    this.populateDropdown('authorId', data.data, 'id', 'name');
                }
            })
            .catch(error => console.error('Error loading users:', error));
    },

    // Populate dropdown with data
    populateDropdown: function(selectId, data, valueField, textField) {
        const select = document.getElementById(selectId);
        if (!select) return;

        // Keep the first option (placeholder)
        const firstOption = select.options[0];
        select.innerHTML = '';
        select.appendChild(firstOption);

        // Add data options
        data.forEach(item => {
            const option = document.createElement('option');
            option.value = item[valueField];
            option.textContent = item[textField];
            select.appendChild(option);
        });
    },

    // Initialize rich text editor
    initializeRichTextEditor: function() {
        const descriptionTextarea = document.getElementById('description');
        if (descriptionTextarea && typeof ClassicEditor !== 'undefined') {
            ClassicEditor
                .create(descriptionTextarea, {
                    toolbar: ['heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', '|', 'outdent', 'indent', '|', 'blockQuote', 'undo', 'redo']
                })
                .then(editor => {
                    this.richTextEditor = editor;
                })
                .catch(error => {
                    console.error('Error initializing rich text editor:', error);
                });
        }
    },

    // Save video
    saveVideo: function() {
        const form = document.getElementById('videoForm');
        if (!form) return;

        // Get form data
        const formData = new FormData(form);
        
        // Get rich text editor content
        if (this.richTextEditor) {
            formData.set('Description', this.richTextEditor.getData());
        }

        // Convert to JSON object
        const videoData = {};
        formData.forEach((value, key) => {
            if (key === 'IsFeatured') {
                videoData[key] = document.getElementById('isFeatured').checked;
            } else {
                videoData[key] = value;
            }
        });

        // Basic validation
        if (!videoData.Title || !videoData.AuthorId || !videoData.TopicId) {
            this.showError('Please fill in all required fields');
            return;
        }

        // Show loading state
        const submitBtn = document.getElementById('saveVideoBtn');
        const originalHtml = submitBtn.innerHTML;
        submitBtn.disabled = true;
        submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-1"></i> Saving...';

        // Make AJAX call
        fetch('/VideoManagement/SaveVideo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(videoData)
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                this.showSuccess('Video saved successfully');
                // Redirect to index after short delay
                setTimeout(() => {
                    window.location.href = '/VideoManagement/Index';
                }, 1500);
            } else {
                this.showError(data.message || 'Error saving video');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            this.showError('Error saving video');
        })
        .finally(() => {
            // Reset button state
            submitBtn.disabled = false;
            submitBtn.innerHTML = originalHtml;
        });
    },

    // Handle file upload
    handleFileUpload: function(file) {
        if (!file) return;

        // Validate file type
        const allowedTypes = ['video/mp4', 'video/avi', 'video/mov', 'video/wmv'];
        if (!allowedTypes.includes(file.type)) {
            this.showError('Please select a valid video file (MP4, AVI, MOV, WMV)');
            return;
        }

        // Validate file size (max 100MB)
        const maxSize = 100 * 1024 * 1024; // 100MB
        if (file.size > maxSize) {
            this.showError('File size must be less than 100MB');
            return;
        }

        // Simulate file upload
        console.log('File selected:', file.name, 'Size:', this.formatFileSize(file.size));
        
        // Update media type to uploaded file
        const mediaTypeSelect = document.getElementById('mediaType');
        if (mediaTypeSelect) {
            mediaTypeSelect.value = 'uploadedfile';
        }
    },

    // Handle media type change
    handleMediaTypeChange: function(mediaType) {
        const videoUrlInput = document.getElementById('videoUrl');
        const videoFileInput = document.getElementById('videoFile');

        if (mediaType === 'uploadedfile') {
            if (videoUrlInput) videoUrlInput.value = '';
        } else {
            if (videoFileInput) videoFileInput.value = '';
        }
    },

    // Validate video URL
    validateVideoUrl: function(url) {
        if (!url) return true;

        const youtubeRegex = /^(https?:\/\/)?(www\.)?(youtube\.com\/watch\?v=|youtu\.be\/)[\w-]+/;
        const vimeoRegex = /^(https?:\/\/)?(www\.)?vimeo\.com\/\d+/;

        if (!youtubeRegex.test(url) && !vimeoRegex.test(url)) {
            this.showError('Please enter a valid YouTube or Vimeo URL');
            return false;
        }

        return true;
    },

    // Utility functions
    showLoadingState: function() {
        const tbody = document.getElementById('videosTableBody');
        if (tbody) {
            tbody.innerHTML = `
                <tr>
                    <td colspan="6" class="text-center py-4">
                        <i class="fas fa-spinner fa-spin fa-2x mb-2 d-block"></i>
                        Loading videos...
                    </td>
                </tr>
            `;
        }
    },

    updateTableInfo: function(totalRecords) {
        const infoElement = document.getElementById('videosTableInfo');
        if (infoElement) {
            const start = (this.currentPage - 1) * this.pageSize + 1;
            const end = Math.min(this.currentPage * this.pageSize, totalRecords);
            infoElement.textContent = `Showing ${start} to ${end} of ${totalRecords} entries`;
        }
    },

    getMediaTypeIcon: function(mediaType) {
        switch (mediaType) {
            case 'youtube': return 'youtube';
            case 'vimeo': return 'vimeo';
            case 'uploadedfile': return 'file-video';
            default: return 'video';
        }
    },

    formatDate: function(dateString) {
        const date = new Date(dateString);
        return date.toLocaleDateString('en-US', { 
            year: 'numeric', 
            month: 'short', 
            day: 'numeric' 
        });
    },

    formatFileSize: function(bytes) {
        if (bytes === 0) return '0 Bytes';
        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(k));
        return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
    },

    escapeHtml: function(text) {
        const div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    },

    getTranslation: function(key) {
        if (typeof translations !== 'undefined') {
            const currentLang = localStorage.getItem('language') || 'en';
            return translations[currentLang] && translations[currentLang][key];
        }
        return null;
    },

    showSuccess: function(message) {
        alert('Success: ' + message);
    },

    showError: function(message) {
        alert('Error: ' + message);
    }
};

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    VideoManagement.init();
});

// Export for global access
window.VideoManagement = VideoManagement;