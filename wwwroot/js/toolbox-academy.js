// Toolbox Academy JavaScript
// Author: Claude
// Description: Handles video filtering, search, and academy interface functionality

const ToolboxAcademy = {
    // Configuration
    config: {
        baseUrl: '/ToolboxAcademy',
        currentTemaId: null,
        currentSearch: '',
        currentPage: 1,
        isLoading: false,
        translations: window.translations || {},
        apiEndpoints: {
            getVideos: '/ToolboxAcademy/GetPortalVideos',
            getTemas: '/ToolboxAcademy/GetTemasConConteo',
            getStats: '/ToolboxAcademy/GetEstadisticasPortal'
        }
    },

    // Initialize the module
    init: function() {
        console.log('Initializing Toolbox Academy module...');
        
        // Get current filters from URL or page data
        this.getCurrentFilters();
        
        // Bind all events
        this.bindEvents();
        
        // Initialize any additional components
        this.initializeComponents();
        
        console.log('Toolbox Academy module initialized successfully');
    },

    // Get current filters from page data
    getCurrentFilters: function() {
        // Try to get current tema from active category item
        const activeCategoryEl = document.querySelector('.category-item-enhanced.active');
        if (activeCategoryEl) {
            const temaId = activeCategoryEl.getAttribute('data-category');
            this.config.currentTemaId = temaId === '0' ? null : parseInt(temaId) || null;
        }
        
        // Get search query from search input
        const searchInput = document.getElementById('videoSearchInput');
        if (searchInput) {
            this.config.currentSearch = searchInput.value || '';
        }
        
        console.log(`Current filters - TemaId: ${this.config.currentTemaId}, Search: "${this.config.currentSearch}"`);
    },

    // Bind all event handlers
    bindEvents: function() {
        // Category filter clicks
        this.bindCategoryFilters();
        
        // Search functionality
        this.bindSearchEvents();
        
        // Video card interactions
        this.bindVideoCardEvents();
        
        // Pagination if exists
        this.bindPaginationEvents();
        
        console.log('All event handlers bound successfully');
    },

    // Bind category filter events
    bindCategoryFilters: function() {
        $(document).on('click', '.category-item-enhanced', (e) => {
            e.preventDefault();
            
            const temaId = $(e.currentTarget).data('category');
            const temaName = $(e.currentTarget).find('.category-name').text().trim();
            
            console.log(`Tema filter clicked: ${temaId} (${temaName})`);
            
            // Update active state
            $('.category-item-enhanced').removeClass('active');
            $(e.currentTarget).addClass('active');
            
            // Update current tema
            this.config.currentTemaId = temaId === '0' ? null : parseInt(temaId) || null;
            
            // Filter videos
            this.filterVideos(this.config.currentTemaId, this.config.currentSearch, 1);
        });
    },

    // Bind search events
    bindSearchEvents: function() {
        // Search button click
        $('#searchBtn').on('click', (e) => {
            e.preventDefault();
            this.performSearch();
        });
        
        // Search input enter key
        $('#videoSearchInput').on('keypress', (e) => {
            if (e.which === 13) { // Enter key
                e.preventDefault();
                this.performSearch();
            }
        });
        
        // Clear search when input is empty (with debounce)
        $('#videoSearchInput').on('input', this.debounce(() => {
            const searchValue = $('#videoSearchInput').val().trim();
            if (searchValue === '' && this.config.currentSearch !== '') {
                this.config.currentSearch = '';
                this.filterVideos(this.config.currentTemaId, '', 1);
            }
        }, 500));
    },

    // Perform search
    performSearch: function() {
        const searchQuery = $('#videoSearchInput').val().trim();
        console.log(`Performing search: "${searchQuery}"`);
        
        this.config.currentSearch = searchQuery;
        this.filterVideos(this.config.currentTemaId, searchQuery, 1);
    },

    // Bind video card events
    bindVideoCardEvents: function() {
        // Video card hover effects (already handled by CSS, but we can add more interactions here)
        
        // Track video clicks for analytics (placeholder)
        $(document).on('click', '.video-card-enhanced a, .watch-video-btn', (e) => {
            const videoId = this.extractVideoIdFromUrl(e.currentTarget.href);
            if (videoId) {
                console.log(`Video clicked: ID ${videoId}`);
                // Here you could send analytics data
                this.trackVideoClick(videoId);
            }
        });
        
        // Category badge clicks
        $(document).on('click', '.category-badge-enhanced', (e) => {
            e.preventDefault();
            const href = e.currentTarget.href;
            if (href) {
                window.location.href = href;
            }
        });
        
        // Video thumbnail hover effects
        $(document).on('mouseenter', '.video-card-enhanced', function() {
            $(this).find('.video-play-overlay').addClass('hovered');
        });
        
        $(document).on('mouseleave', '.video-card-enhanced', function() {
            $(this).find('.video-play-overlay').removeClass('hovered');
        });
    },

    // Bind pagination events
    bindPaginationEvents: function() {
        // Pagination is handled by server-side links, but we can add AJAX here if needed
        $(document).on('click', '.pagination .page-link', (e) => {
            // For now, let the default behavior handle pagination
            // In the future, we could implement AJAX pagination here
        });
    },

    // Filter videos (page redirect for now, AJAX ready)
    filterVideos: function(temaId, searchQuery = '', page = 1) {
        if (this.config.isLoading) {
            console.log('Already loading, ignoring filter request');
            return;
        }
        
        console.log(`Filtering videos - TemaId: ${temaId}, Search: "${searchQuery}", Page: ${page}`);
        
        this.config.isLoading = true;
        this.showLoadingState();
        
        // Build URL parameters
        const params = new URLSearchParams();
        if (temaId && temaId !== null) {
            params.append('temaId', temaId);
        }
        if (searchQuery) {
            params.append('search', searchQuery);
        }
        if (page > 1) {
            params.append('page', page);
        }
        
        // For now, redirect to filtered page (server-side filtering)
        const url = `${this.config.baseUrl}${params.toString() ? '?' + params.toString() : ''}`;
        window.location.href = url;
        
        // Alternative: AJAX approach ready to use
        /*
        $.get(this.config.apiEndpoints.getVideos, {
            temaId: temaId,
            search: searchQuery,
            page: page,
            pageSize: 6
        })
        .done((response) => {
            if (response.success) {
                this.renderVideos(response.data);
                this.updatePagination(response);
                this.updateUrl(temaId, searchQuery, page);
                this.updateStats();
            } else {
                this.showError(response.message);
            }
        })
        .fail(() => {
            this.showError('Error al cargar los videos');
        })
        .always(() => {
            this.config.isLoading = false;
            this.hideLoadingState();
        });
        */
    },

    // Show loading state
    showLoadingState: function() {
        $('#videosContainer').append(`
            <div id="loadingOverlay" class="text-center p-4">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
                <div class="mt-2">${this.translate('academy.loading') || 'Cargando videos...'}</div>
            </div>
        `);
    },

    // Hide loading state
    hideLoadingState: function() {
        $('#loadingOverlay').remove();
        this.config.isLoading = false;
    },

    // Initialize additional components
    initializeComponents: function() {
        // Initialize tooltips
        this.initializeTooltips();
        
        // Initialize lazy loading for video thumbnails if needed
        this.initializeLazyLoading();
        
        // Initialize any other components
        this.initializeVideoPlayers();
        
        // Initialize hero animations
        this.initializeHeroAnimations();
        
        // Initialize enhanced interactions
        this.initializeEnhancedInteractions();
        
        // Load dynamic data if using AJAX mode
        // this.loadDynamicData();
    },
    
    // Load dynamic data (for AJAX mode)
    loadDynamicData: function() {
        // Load temas with counts
        this.loadTemasWithCounts();
        
        // Load statistics
        this.updateStats();
    },
    
    // Load temas with video counts
    loadTemasWithCounts: function() {
        $.get(this.config.apiEndpoints.getTemas)
            .done((response) => {
                if (response.success) {
                    this.renderTemasList(response.data);
                }
            })
            .fail(() => {
                console.error('Error loading temas');
            });
    },
    
    // Update statistics
    updateStats: function() {
        $.get(this.config.apiEndpoints.getStats)
            .done((response) => {
                if (response.success) {
                    this.renderStats(response.data);
                }
            })
            .fail(() => {
                console.error('Error loading statistics');
            });
    },
    
    // Render temas list
    renderTemasList: function(temas) {
        const temasHtml = temas.map(tema => `
            <a href="#" 
               class="category-item-enhanced ${tema.id === this.config.currentTemaId ? 'active' : ''}"
               data-category="${tema.id}">
                <div class="category-item-left">
                    <span class="category-emoji">📁</span>
                    <div class="category-info">
                        <div class="category-name">${tema.nombre}</div>
                        <div class="category-description">${tema.descripcion}</div>
                    </div>
                </div>
                <span class="category-count-badge">${tema.conteoVideos}</span>
            </a>
        `).join('');
        
        $('.categories-list').html(temasHtml);
    },
    
    // Render statistics
    renderStats: function(stats) {
        $('.stat-number').each(function() {
            const statType = $(this).data('stat-type');
            if (statType && stats[statType] !== undefined) {
                $(this).text(stats[statType].toLocaleString());
            }
        });
    },

    // Initialize Bootstrap tooltips
    initializeTooltips: function() {
        if (typeof bootstrap !== 'undefined') {
            const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
            tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl);
            });
        }
    },

    // Initialize lazy loading for better performance
    initializeLazyLoading: function() {
        // Simple intersection observer for lazy loading video embeds
        if ('IntersectionObserver' in window) {
            const lazyVideoObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        const iframe = entry.target;
                        if (iframe.dataset.src) {
                            iframe.src = iframe.dataset.src;
                            iframe.removeAttribute('data-src');
                            observer.unobserve(iframe);
                        }
                    }
                });
            });

            // Observe all iframes with data-src attribute
            document.querySelectorAll('iframe[data-src]').forEach(iframe => {
                lazyVideoObserver.observe(iframe);
            });
        }
    },

    // Initialize video players with additional controls
    initializeVideoPlayers: function() {
        // Add any custom video player initialization here
        // For example, tracking when videos are played, adding custom controls, etc.
        
        $('iframe[src*="youtube.com"], iframe[src*="vimeo.com"]').each(function() {
            $(this).on('load', function() {
                console.log('Video iframe loaded:', this.title);
            });
        });
    },

    // Initialize hero section animations
    initializeHeroAnimations: function() {
        // Animate hero stats on scroll
        if ('IntersectionObserver' in window) {
            const heroStatsObserver = new IntersectionObserver((entries) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        this.animateHeroStats();
                        heroStatsObserver.unobserve(entry.target);
                    }
                });
            });

            const heroStats = document.querySelector('.hero-stats');
            if (heroStats) {
                heroStatsObserver.observe(heroStats);
            }
        }

        // Floating card hover effects
        $('.featured-card-floating').hover(
            function() {
                $(this).css('transform', 'translateY(-5px) scale(1.02)');
            },
            function() {
                $(this).css('transform', 'translateY(0) scale(1)');
            }
        );
    },

    // Animate hero statistics with counting effect
    animateHeroStats: function() {
        $('.hero-stat-number').each(function() {
            const $this = $(this);
            const finalValue = $this.text();
            
            // Extract numeric value for animation
            const numericValue = parseInt(finalValue.replace(/[^\d]/g, ''));
            if (!isNaN(numericValue) && numericValue > 0) {
                $this.text('0');
                
                $({ count: 0 }).animate(
                    { count: numericValue },
                    {
                        duration: 2000,
                        easing: 'swing',
                        step: function() {
                            $this.text(Math.floor(this.count).toLocaleString());
                        },
                        complete: function() {
                            $this.text(finalValue);
                        }
                    }
                );
            }
        });
    },

    // Initialize enhanced interactions
    initializeEnhancedInteractions: function() {
        // Search input focus effects
        $('#videoSearchInput').on('focus', function() {
            $(this).closest('.search-section').addClass('focused');
        }).on('blur', function() {
            $(this).closest('.search-section').removeClass('focused');
        });

        // Category item hover effects with description tooltip
        $('.category-item-enhanced').hover(
            function() {
                const description = $(this).find('.category-description').text();
                if (description && $(this).find('.category-tooltip').length === 0) {
                    $(this).append(`<div class="category-tooltip">${description}</div>`);
                }
            },
            function() {
                $(this).find('.category-tooltip').remove();
            }
        );

        // Smooth scroll for internal links
        $('a[href^="#"]').on('click', function(e) {
            e.preventDefault();
            const target = $(this.getAttribute('href'));
            if (target.length) {
                $('html, body').animate({
                    scrollTop: target.offset().top - 80
                }, 500);
            }
        });

        // Add loading states to buttons
        $('.watch-video-btn, .search-btn').on('click', function() {
            const $btn = $(this);
            const originalText = $btn.html();
            
            $btn.html('<i class="fas fa-spinner fa-spin me-2"></i>Cargando...')
                .prop('disabled', true);
            
            // Re-enable after navigation (fallback)
            setTimeout(() => {
                $btn.html(originalText).prop('disabled', false);
            }, 3000);
        });
    },

    // Track video clicks for analytics
    trackVideoClick: function(videoId) {
        // Placeholder for analytics tracking
        console.log(`Analytics: Video ${videoId} clicked`);
        
        // Here you could send data to your analytics service
        // Example: gtag('event', 'video_click', { video_id: videoId });
    },

    // Extract video ID from URL
    extractVideoIdFromUrl: function(url) {
        const match = url.match(/\/Detail\/(\d+)/);
        return match ? parseInt(match[1]) : null;
    },

    // Update browser URL without page reload
    updateUrl: function(categorySlug, searchQuery, page) {
        const params = new URLSearchParams();
        if (categorySlug && categorySlug !== 'all') {
            params.append('categorySlug', categorySlug);
        }
        if (searchQuery) {
            params.append('search', searchQuery);
        }
        if (page > 1) {
            params.append('page', page);
        }
        
        const newUrl = `${this.config.baseUrl}${params.toString() ? '?' + params.toString() : ''}`;
        window.history.pushState({}, '', newUrl);
    },

    // Render videos (for AJAX approach)
    renderVideos: function(videos) {
        const videosHtml = videos.map(video => this.renderVideoCard(video)).join('');
        $('#videosList').html(videosHtml);
        
        // Re-initialize components for new content
        this.initializeTooltips();
        this.initializeLazyLoading();
    },

    // Render single video card
    renderVideoCard: function(video) {
        const categoryColor = this.getCategoryColor(video.categorySlug);
        const featuredBadge = video.isFeatured ? `<div class="featured-badge">${this.translate('labels.featured')}</div>` : '';
        
        return `
            <div class="col-md-6 mb-4">
                <div class="card video-card">
                    <div class="video-embed-container">
                        ${featuredBadge}
                        <iframe src="${video.embedUrl}" 
                                title="${video.title}" 
                                frameborder="0" 
                                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" 
                                allowfullscreen>
                        </iframe>
                        <div class="video-duration">${video.durationFormatted}</div>
                    </div>
                    <div class="card-body">
                        <div class="d-flex justify-content-between align-items-start mb-2">
                            <a href="${this.config.baseUrl}?categorySlug=${video.categorySlug}" 
                               class="category-badge text-white text-decoration-none"
                               style="background-color: ${categoryColor}">
                                ${video.categoryName}
                            </a>
                            <small class="video-meta">
                                <i class="fas fa-eye me-1"></i>${video.viewCount.toLocaleString()}
                            </small>
                        </div>
                        
                        <h5 class="card-title">
                            <a href="${this.config.baseUrl}/Detail/${video.id}" class="video-title">${video.title}</a>
                        </h5>
                        
                        <p class="card-text text-muted">${video.shortDescription}</p>
                        
                        <div class="d-flex justify-content-between align-items-center">
                            <small class="video-meta">
                                <i class="fas fa-calendar me-1"></i>${video.publishDateFormatted}
                            </small>
                            <small class="video-meta">
                                <i class="fas fa-user me-1"></i>${video.authorName}
                            </small>
                        </div>
                        
                        <div class="mt-3">
                            <a href="${this.config.baseUrl}/Detail/${video.id}" class="btn btn-outline-primary btn-sm">
                                <i class="fas fa-play me-1"></i>${this.translate('buttons.readMore')}
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        `;
    },

    // Get category color
    getCategoryColor: function(categorySlug) {
        const colors = {
            'web-development': '#007bff',
            'project-management': '#28a745',
            'design-ui-ux': '#e83e8c',
            'marketing-growth': '#fd7e14',
            'business-strategy': '#6f42c1'
        };
        return colors[categorySlug] || '#6c757d';
    },

    // Show error message
    showError: function(message) {
        const alert = $(`
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                <i class="fas fa-exclamation-triangle me-2"></i>
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `);
        
        $('#videosContainer').prepend(alert);
        
        // Auto remove after 5 seconds
        setTimeout(() => {
            alert.alert('close');
        }, 5000);
    },

    // Utility functions
    translate: function(key) {
        return this.config.translations[key] || key;
    },

    debounce: function(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func.apply(this, args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    },

    // Format numbers for display
    formatNumber: function(num) {
        if (num >= 1000000) {
            return (num / 1000000).toFixed(1) + 'M';
        } else if (num >= 1000) {
            return (num / 1000).toFixed(1) + 'K';
        }
        return num.toString();
    },

    // Format duration for display
    formatDuration: function(minutes) {
        const hours = Math.floor(minutes / 60);
        const mins = minutes % 60;
        
        if (hours > 0) {
            return `${hours}:${mins.toString().padStart(2, '0')}:00`;
        }
        return `${mins}:00`;
    }
};

// Initialize when document is ready
$(document).ready(function() {
    ToolboxAcademy.init();
});

// Export for global access if needed
window.ToolboxAcademy = ToolboxAcademy;