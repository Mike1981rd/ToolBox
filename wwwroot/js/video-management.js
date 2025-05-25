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
    currentStatusFilter: '',
    currentTypeFilter: '',
    currentFeaturedFilter: '',
    richTextEditor: null,
    currentVideoId: 0,
    topicsData: [],
    authorsData: [],

    // Initialize the module
    init: function() {
        if (document.getElementById('videosTable')) {
            // We're on the Index page
            this.initializeIndex();
        } else if (document.getElementById('videoForm')) {
            // We're on the Form page - will be initialized separately
            console.log('Video form page detected');
        }
    },

    // Initialize Index page
    initializeIndex: function() {
        this.loadVideos();
        this.setupIndexEventListeners();
    },

    // Initialize Form page
    initializeForm: function(videoId = 0) {
        this.currentVideoId = videoId;
        this.setupFormEventListeners();
        this.loadDropdownData();
        this.initializeRichTextEditor();
        this.initializeVideoPreview();
        
        if (videoId > 0) {
            this.loadVideoData(videoId);
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

        // Status filter
        const statusFilter = document.getElementById('selectVideoStatus');
        if (statusFilter) {
            statusFilter.addEventListener('change', (e) => {
                this.currentStatusFilter = e.target.value;
                this.currentPage = 1;
                this.loadVideos();
            });
        }

        // Video type filter
        const typeFilter = document.getElementById('selectVideoType');
        if (typeFilter) {
            typeFilter.addEventListener('change', (e) => {
                this.currentTypeFilter = e.target.value;
                this.currentPage = 1;
                this.loadVideos();
            });
        }

        // Featured filter
        const featuredFilter = document.getElementById('selectFeatured');
        if (featuredFilter) {
            featuredFilter.addEventListener('change', (e) => {
                this.currentFeaturedFilter = e.target.value;
                this.currentPage = 1;
                this.loadVideos();
            });
        }

        // Event delegation for action buttons
        document.addEventListener('click', (e) => {
            // Toggle featured button
            if (e.target.closest('.toggle-featured-btn')) {
                const btn = e.target.closest('.toggle-featured-btn');
                const videoId = btn.dataset.videoId;
                const videoTitle = btn.dataset.videoTitle;
                this.toggleFeatured(videoId, videoTitle);
            }
            
            // Delete button
            if (e.target.closest('.delete-video-btn')) {
                const btn = e.target.closest('.delete-video-btn');
                const videoId = btn.dataset.videoId;
                const videoTitle = btn.dataset.videoTitle;
                this.confirmDelete(videoId, videoTitle);
            }
        });
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

        // Media type radio buttons
        const videoSourceRadios = document.querySelectorAll('input[name="TipoFuenteVideo"]');
        videoSourceRadios.forEach(radio => {
            radio.addEventListener('change', (e) => {
                this.handleVideoSourceChange(e.target.value);
            });
        });

        // Video URL input with debounce for preview
        const videoUrlInput = document.getElementById('videoUrl');
        if (videoUrlInput) {
            let urlTimeout;
            videoUrlInput.addEventListener('input', (e) => {
                clearTimeout(urlTimeout);
                urlTimeout = setTimeout(() => {
                    this.updateVideoPreview(e.target.value);
                }, 500);
            });
        }
    },

    // Initialize video preview functionality
    initializeVideoPreview: function() {
        // Check initial state
        const checkedRadio = document.querySelector('input[name="TipoFuenteVideo"]:checked');
        if (checkedRadio) {
            this.handleVideoSourceChange(checkedRadio.value);
        }
    },

    // Handle video source type change
    handleVideoSourceChange: function(sourceType) {
        const urlSection = document.getElementById('urlVideoSection');
        const uploadSection = document.getElementById('uploadVideoSection');
        const previewSection = document.getElementById('videoPreviewSection');

        if (sourceType === 'Upload') {
            urlSection.style.display = 'none';
            uploadSection.style.display = 'block';
            previewSection.style.display = 'none';
        } else {
            urlSection.style.display = 'block';
            uploadSection.style.display = 'none';
            previewSection.style.display = 'block';
            
            // Update preview if URL exists
            const videoUrl = document.getElementById('videoUrl').value;
            if (videoUrl) {
                this.updateVideoPreview(videoUrl);
            }
        }
    },

    // Update video preview
    updateVideoPreview: function(url) {
        const previewContainer = document.getElementById('videoPreviewContainer');
        if (!previewContainer) return;

        if (!url) {
            previewContainer.innerHTML = '<p class="text-muted text-center">Vista previa del video aparecerá aquí</p>';
            return;
        }

        // Extract video ID and create embed URL
        const youtubeId = this.extractYouTubeVideoId(url);
        const vimeoId = this.extractVimeoVideoId(url);

        if (youtubeId) {
            previewContainer.innerHTML = `
                <div class="ratio ratio-16x9">
                    <iframe 
                        src="https://www.youtube.com/embed/${youtubeId}" 
                        frameborder="0" 
                        allowfullscreen
                        style="border-radius: 8px;">
                    </iframe>
                </div>
            `;
        } else if (vimeoId) {
            previewContainer.innerHTML = `
                <div class="ratio ratio-16x9">
                    <iframe 
                        src="https://player.vimeo.com/video/${vimeoId}" 
                        frameborder="0" 
                        allowfullscreen
                        style="border-radius: 8px;">
                    </iframe>
                </div>
            `;
        } else {
            previewContainer.innerHTML = '<p class="text-muted text-center">URL de video no válida. Ingrese una URL de YouTube o Vimeo válida.</p>';
        }
    },

    // Extract YouTube video ID
    extractYouTubeVideoId: function(url) {
        const regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
        const match = url.match(regExp);
        return (match && match[2].length === 11) ? match[2] : null;
    },

    // Extract Vimeo video ID
    extractVimeoVideoId: function(url) {
        const regExp = /(?:vimeo)\.com.*(?:videos|video|channels|)\/([\d]+)/i;
        const match = url.match(regExp);
        return match ? match[1] : null;
    },

    // Load video data for editing
    loadVideoData: function(videoId) {
        fetch(`/VideoManagement/GetVideo?id=${videoId}`)
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    this.populateForm(data.data);
                } else {
                    this.showError('Error loading video data');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                this.showError('Error loading video data');
            });
    },

    // Populate form with video data
    populateForm: function(video) {
        // Basic fields
        document.getElementById('videoId').value = video.id;
        document.getElementById('titulo').value = video.titulo;
        document.getElementById('topicId').value = video.temaId || '';
        document.getElementById('authorId').value = video.autorId || '';
        document.getElementById('duration').value = video.duracion || '';
        
        // SEO fields
        document.getElementById('metaTitle').value = video.metaTituloSEO || '';
        document.getElementById('metaDescription').value = video.metaDescripcionSEO || '';
        document.getElementById('metaKeywords').value = video.palabrasClaveSEO || '';
        
        // Status fields
        document.getElementById('isFeatured').checked = video.esDestacado;
        document.getElementById('videoStatus').value = video.estadoVideo;
        
        // Video source
        const sourceRadio = document.querySelector(`input[name="TipoFuenteVideo"][value="${video.tipoFuenteVideo}"]`);
        if (sourceRadio) {
            sourceRadio.checked = true;
            this.handleVideoSourceChange(video.tipoFuenteVideo);
        }
        
        // Video URL or file info
        if (video.tipoFuenteVideo !== 'Upload' && video.urlVideoExterno) {
            document.getElementById('videoUrl').value = video.urlVideoExterno;
            this.updateVideoPreview(video.urlVideoExterno);
        } else if (video.tipoFuenteVideo === 'Upload' && video.nombreArchivoVideoSubido) {
            const fileInfo = document.getElementById('uploadedFileInfo');
            if (fileInfo) {
                fileInfo.innerHTML = `
                    <div class="alert alert-info">
                        <i class="fas fa-file-video me-2"></i>
                        Archivo actual: <strong>${video.nombreArchivoVideoSubido}</strong>
                    </div>
                `;
                fileInfo.style.display = 'block';
            }
        }
        
        // Rich text editor content
        if (video.descripcionHTML) {
            if (this.richTextEditor && this.richTextEditor.setContent) {
                this.richTextEditor.setContent(video.descripcionHTML);
            } else {
                // If editor isn't ready yet, set the textarea value directly
                const descriptionTextarea = document.getElementById('description');
                if (descriptionTextarea) {
                    descriptionTextarea.value = video.descripcionHTML;
                    // Try to set content again after a delay
                    setTimeout(() => {
                        if (this.richTextEditor && this.richTextEditor.setContent) {
                            this.richTextEditor.setContent(video.descripcionHTML);
                        }
                    }, 500);
                }
            }
        }
    },

    // Load videos from server
    loadVideos: function() {
        this.showLoadingState();

        const params = new URLSearchParams({
            searchTerm: this.currentSearchTerm,
            page: this.currentPage,
            pageSize: this.pageSize,
            statusFilter: this.currentStatusFilter || 'all'
        });

        // Add additional filters if they have values
        if (this.currentTypeFilter) {
            params.append('typeFilter', this.currentTypeFilter);
        }
        if (this.currentFeaturedFilter) {
            params.append('featuredFilter', this.currentFeaturedFilter);
        }

        fetch(`/VideoManagement/GetVideos?${params}`)
            .then(response => {
                console.log('GetVideos response status:', response.status);
                if (!response.ok) {
                    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
                }
                return response.json();
            })
            .then(data => {
                console.log('GetVideos data received:', data);
                if (data && data.success) {
                    this.videosData = data.data || [];
                    this.totalPages = data.totalPages || 1;
                    this.renderVideosTable();
                    this.renderPagination();
                    this.updateTableInfo(data.totalRecords || 0);
                } else {
                    this.showError('Error loading videos: ' + (data ? data.message : 'Unknown error'));
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
                    <td colspan="7" class="text-center text-muted py-4">
                        <i class="fas fa-video fa-2x mb-2 d-block"></i>
                        No se encontraron videos
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
                    <div class="d-flex justify-content-start align-items-center">
                        <div class="avatar-wrapper me-3">
                            <div class="avatar avatar-md">
                                <span class="avatar-initial rounded-circle bg-label-${this.getMediaTypeColor(video.tipoFuenteVideo)}">
                                    <i class="fas fa-${this.getMediaTypeIcon(video.tipoFuenteVideo)}"></i>
                                </span>
                            </div>
                        </div>
                        <div class="d-flex flex-column">
                            <a href="/VideoManagement/VideoForm?id=${video.id}" class="text-body text-truncate">
                                <span class="fw-semibold">${this.escapeHtml(video.titulo)}</span>
                            </a>
                            <small class="text-muted">
                                ${video.duracion || 'Sin duración'}
                            </small>
                        </div>
                    </div>
                </td>
                <td>
                    ${video.temaNombre ? 
                        `<span class="badge bg-info text-white">
                            ${this.escapeHtml(video.temaNombre)}
                        </span>` : 
                        `<span class="text-muted">Sin tema</span>`
                    }
                </td>
                <td>
                    ${video.autorNombre ?
                        `<div class="d-flex align-items-center">
                            <div class="avatar avatar-sm me-2">
                                <span class="avatar-initial rounded-circle bg-label-secondary">
                                    ${video.autorNombre.charAt(0).toUpperCase()}
                                </span>
                            </div>
                            <span>${this.escapeHtml(video.autorNombre)}</span>
                        </div>` :
                        `<span class="text-muted">
                            <i class="fas fa-question-circle me-1"></i>
                            Sin autor
                        </span>`
                    }
                </td>
                <td>
                    <span class="text-nowrap">${this.formatDate(video.fechaSubida)}</span>
                </td>
                <td>
                    <div class="d-flex align-items-center">
                        ${this.getVideoStatusBadge(video.estadoVideo)}
                        ${video.esDestacado ? 
                            '<i class="fas fa-star text-warning ms-2" title="Destacado"></i>' : 
                            ''
                        }
                    </div>
                </td>
                <td>
                    <div class="d-inline-block text-nowrap">
                        <a href="/VideoManagement/VideoForm?id=${video.id}" class="btn btn-sm btn-icon" title="Editar Video">
                            <i class="fas fa-edit"></i>
                        </a>
                        ${video.esDestacado ?
                            `<button type="button" class="btn btn-sm btn-icon text-warning toggle-featured-btn" 
                                    title="Quitar Destacado"
                                    data-video-id="${video.id}"
                                    data-video-title="${this.escapeHtml(video.titulo)}">
                                <i class="fas fa-star"></i>
                            </button>` :
                            `<button type="button" class="btn btn-sm btn-icon text-muted toggle-featured-btn" 
                                    title="Marcar como Destacado"
                                    data-video-id="${video.id}"
                                    data-video-title="${this.escapeHtml(video.titulo)}">
                                <i class="far fa-star"></i>
                            </button>`
                        }
                        <button type="button" class="btn btn-sm btn-icon text-danger delete-video-btn" 
                                title="Eliminar Video"
                                data-video-id="${video.id}"
                                data-video-title="${this.escapeHtml(video.titulo)}">
                            <i class="fas fa-trash-alt"></i>
                        </button>
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
        const confirmMessage = '¿Está seguro de que desea eliminar este video?';
        
        if (!confirm(confirmMessage)) return;

        fetch('/VideoManagement/DeleteVideo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
            },
            body: JSON.stringify({ videoId: videoId })
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                this.showSuccess('Video eliminado exitosamente');
                this.loadVideos();
            } else {
                this.showError(data.message || 'Error al eliminar el video');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            this.showError('Error al eliminar el video');
        });
    },

    // Load dropdown data for form
    loadDropdownData: function() {
        console.log('Loading dropdown data...');
        
        // Load topics
        fetch('/VideoManagement/GetTopicsForDropdown')
            .then(response => {
                console.log('Topics response status:', response.status);
                if (!response.ok) {
                    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
                }
                return response.json();
            })
            .then(data => {
                console.log('Topics data received:', data);
                if (data.success) {
                    this.topicsData = data.data;
                    this.populateDropdown('topicId', data.data, 'id', 'name');
                    console.log('Topics populated:', data.data.length, 'items');
                } else {
                    console.error('Topics loading failed:', data.message);
                }
            })
            .catch(error => console.error('Error loading topics:', error));

        // Load users
        fetch('/VideoManagement/GetUsersForDropdown')
            .then(response => {
                console.log('Users response status:', response.status);
                if (!response.ok) {
                    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
                }
                return response.json();
            })
            .then(data => {
                console.log('Users data received:', data);
                if (data.success) {
                    this.authorsData = data.data;
                    this.populateDropdown('authorId', data.data, 'id', 'name');
                    console.log('Users populated:', data.data.length, 'items');
                } else {
                    console.error('Users loading failed:', data.message);
                }
            })
            .catch(error => console.error('Error loading users:', error));
    },

    // Populate dropdown with data
    populateDropdown: function(selectId, data, valueField, textField) {
        console.log(`Populating dropdown ${selectId} with data:`, data);
        const select = document.getElementById(selectId);
        if (!select) {
            console.error(`Select element with ID ${selectId} not found`);
            return;
        }

        // Keep the first option (placeholder)
        const firstOption = select.options[0];
        select.innerHTML = '';
        if (firstOption) {
            select.appendChild(firstOption);
        }

        // Add data options
        data.forEach((item, index) => {
            console.log(`Adding option ${index}:`, item);
            const option = document.createElement('option');
            option.value = item[valueField];
            option.textContent = item[textField];
            select.appendChild(option);
        });
        
        console.log(`Dropdown ${selectId} populated with ${data.length} items`);
    },

    // Initialize rich text editor
    initializeRichTextEditor: function() {
        // Wait for TinyMCE to load
        const self = this;
        const checkTinyMCE = () => {
            if (typeof tinymce !== 'undefined') {
                const descriptionTextarea = document.getElementById('description');
                if (descriptionTextarea) {
                    tinymce.init({
                        selector: '#description',
                        height: 300,
                        menubar: false,
                        plugins: 'advlist autolink lists link image charmap preview anchor searchreplace visualblocks code fullscreen insertdatetime media table help wordcount',
                        toolbar: 'undo redo | formatselect | bold italic backcolor | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | removeformat | help',
                        content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
                        promotion: false,
                        branding: false,
                        setup: (editor) => {
                            self.richTextEditor = editor;
                            console.log('TinyMCE initialized successfully');
                        },
                        init_instance_callback: (editor) => {
                            console.log('TinyMCE editor ready:', editor.id);
                            self.richTextEditor = editor;
                        }
                    });
                }
            } else {
                // Retry after 100ms if TinyMCE isn't loaded yet
                setTimeout(checkTinyMCE, 100);
            }
        };
        checkTinyMCE();
    },

    // Save video
    saveVideo: function() {
        const form = document.getElementById('videoForm');
        if (!form) return;

        // Create FormData for file upload support
        const formData = new FormData();
        
        // Get basic form values
        formData.append('Id', this.currentVideoId);
        formData.append('Titulo', document.getElementById('titulo').value);
        formData.append('TemaId', document.getElementById('topicId').value || '');
        formData.append('AutorId', document.getElementById('authorId').value || '');
        formData.append('Duracion', document.getElementById('duration').value || '');
        
        // Get rich text editor content
        if (this.richTextEditor && this.richTextEditor.getContent) {
            formData.append('DescripcionHTML', this.richTextEditor.getContent());
        } else {
            // Fallback to textarea value if editor isn't ready
            const descriptionTextarea = document.getElementById('description');
            formData.append('DescripcionHTML', descriptionTextarea ? descriptionTextarea.value : '');
        }
        
        // Video source type
        const tipoFuenteVideo = document.querySelector('input[name="TipoFuenteVideo"]:checked').value;
        formData.append('TipoFuenteVideo', tipoFuenteVideo);
        
        // Video URL or file
        if (tipoFuenteVideo === 'Upload') {
            const fileInput = document.getElementById('videoFile');
            if (fileInput.files.length > 0) {
                formData.append('videoFile', fileInput.files[0]);
            }
        } else {
            formData.append('UrlVideoExterno', document.getElementById('videoUrl').value || '');
        }
        
        // SEO fields
        formData.append('MetaTituloSEO', document.getElementById('metaTitle').value || '');
        formData.append('MetaDescripcionSEO', document.getElementById('metaDescription').value || '');
        formData.append('PalabrasClaveSEO', document.getElementById('metaKeywords').value || '');
        
        // Status fields
        formData.append('EsDestacado', document.getElementById('isFeatured').checked);
        formData.append('EstadoVideo', document.getElementById('videoStatus').value);

        // Basic validation
        const titulo = document.getElementById('titulo').value;
        if (!titulo) {
            this.showError('Por favor ingrese el título del video');
            return;
        }

        // Show loading state
        const submitBtn = document.getElementById('saveVideoBtn');
        const originalHtml = submitBtn.innerHTML;
        submitBtn.disabled = true;
        submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-1"></i> Guardando...';

        // Make AJAX call
        fetch('/VideoManagement/SaveVideo', {
            method: 'POST',
            headers: {
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
            },
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                this.showSuccess('Video guardado exitosamente');
                // Redirect to index after short delay
                setTimeout(() => {
                    // Use the redirect URL from server response if available
                    if (data.redirectUrl) {
                        console.log('Redirecting to server-provided URL:', data.redirectUrl);
                        window.location.href = data.redirectUrl;
                    } else {
                        // Fallback to manual construction
                        const redirectUrl = '/VideoManagement/Index';
                        console.log('Redirecting to fallback URL:', redirectUrl);
                        window.location.href = redirectUrl;
                    }
                }, 1500);
            } else {
                this.showError(data.message || 'Error al guardar el video');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            this.showError('Error al guardar el video');
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
        const allowedTypes = ['video/mp4', 'video/avi', 'video/mov', 'video/wmv', 'video/webm'];
        if (!allowedTypes.includes(file.type)) {
            this.showError('Por favor seleccione un archivo de video válido (MP4, AVI, MOV, WMV, WebM)');
            document.getElementById('videoFile').value = '';
            return;
        }

        // Validate file size (max 500MB)
        const maxSize = 500 * 1024 * 1024;
        if (file.size > maxSize) {
            this.showError('El tamaño del archivo debe ser menor a 500MB');
            document.getElementById('videoFile').value = '';
            return;
        }

        // Show file info
        const fileInfo = document.getElementById('uploadedFileInfo');
        if (fileInfo) {
            fileInfo.innerHTML = `
                <div class="alert alert-info">
                    <i class="fas fa-file-video me-2"></i>
                    Archivo seleccionado: <strong>${file.name}</strong>
                    <br>
                    Tamaño: ${this.formatFileSize(file.size)}
                </div>
            `;
            fileInfo.style.display = 'block';
        }
    },

    // Helper functions
    getMediaTypeIcon: function(mediaType) {
        const icons = {
            'YouTube': 'youtube',
            'Vimeo': 'vimeo',
            'Upload': 'file-video'
        };
        return icons[mediaType] || 'video';
    },

    getVideoStatusBadge: function(status) {
        const badges = {
            'Publicado': '<span class="badge bg-success">Publicado</span>',
            'Borrador': '<span class="badge bg-warning">Borrador</span>',
            'Archivado': '<span class="badge bg-secondary">Archivado</span>'
        };
        return badges[status] || '<span class="badge bg-secondary">' + status + '</span>';
    },

    formatDate: function(dateString) {
        const date = new Date(dateString);
        return date.toLocaleDateString('es-ES', { 
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
        div.textContent = text || '';
        return div.innerHTML;
    },

    // Get media type icon
    getMediaTypeIcon: function(type) {
        switch(type) {
            case 'YouTube': return 'youtube';
            case 'Vimeo': return 'vimeo';
            case 'Upload': return 'file-video';
            default: return 'video';
        }
    },

    // Get media type color
    getMediaTypeColor: function(type) {
        switch(type) {
            case 'YouTube': return 'danger';
            case 'Vimeo': return 'info';
            case 'Upload': return 'success';
            default: return 'secondary';
        }
    },

    // Get video status badge
    getVideoStatusBadge: function(status) {
        switch(status) {
            case 'Publicado':
                return `<span class="badge bg-success text-white">
                            Publicado
                        </span>`;
            case 'Borrador':
                return `<span class="badge bg-warning text-dark">
                            Borrador
                        </span>`;
            case 'Archivado':
                return `<span class="badge bg-secondary text-white">
                            Archivado
                        </span>`;
            default:
                return `<span class="badge bg-light text-muted">
                            ${status}
                        </span>`;
        }
    },

    showLoadingState: function() {
        const tbody = document.getElementById('videosTableBody');
        if (tbody) {
            tbody.innerHTML = `
                <tr>
                    <td colspan="7" class="text-center py-4">
                        <i class="fas fa-spinner fa-spin fa-2x mb-2 d-block"></i>
                        Cargando videos...
                    </td>
                </tr>
            `;
        }
    },

    updateTableInfo: function(totalRecords) {
        const infoElement = document.getElementById('videosTableInfo');
        if (infoElement) {
            const start = totalRecords > 0 ? ((this.currentPage - 1) * this.pageSize) + 1 : 0;
            const end = Math.min(this.currentPage * this.pageSize, totalRecords);
            infoElement.textContent = `Mostrando ${start} a ${end} de ${totalRecords} entradas`;
        }
    },

    showSuccess: function(message) {
        if (typeof showToast === 'function') {
            showToast(message, 'success');
        } else {
            alert(message);
        }
    },

    showError: function(message) {
        if (typeof showToast === 'function') {
            showToast(message, 'error');
        } else {
            alert('Error: ' + message);
        }
    },

    getTranslation: function(key) {
        // Placeholder for translation system
        return null;
    },

    // Toggle featured status
    toggleFeatured: function(videoId, videoTitle) {
        fetch(`/VideoManagement/ToggleFeatured`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
            },
            body: JSON.stringify({ VideoId: parseInt(videoId) })
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                this.showSuccess(data.message);
                this.loadVideos();
            } else {
                this.showError(data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            this.showError('Error al cambiar el estado destacado');
        });
    },

    // Confirm delete
    confirmDelete: function(videoId, videoTitle) {
        if (confirm(`¿Está seguro de que desea eliminar el video "${videoTitle}"?`)) {
            this.deleteVideo(videoId);
        }
    },

    // Delete video
    deleteVideo: function(videoId) {
        fetch(`/VideoManagement/DeleteVideo`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
            },
            body: JSON.stringify({ VideoId: parseInt(videoId) })
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                this.showSuccess(data.message);
                this.loadVideos();
            } else {
                this.showError(data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            this.showError('Error al eliminar el video');
        });
    }
};

// Initialize on DOM ready
document.addEventListener('DOMContentLoaded', function() {
    VideoManagement.init();
});