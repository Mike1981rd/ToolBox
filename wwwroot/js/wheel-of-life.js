// Wheel of Life JavaScript Module
const WheelOfLife = {
    config: {
        chart: null,
        baseUrl: '/WheelOfLife',
        currentData: [],
        isLoading: false
    },

    // Initialize the module
    init: function() {
        console.log('Initializing Wheel of Life module...');
        this.bindEvents();
        this.loadWheelData();
    },

    // Load initial data from API
    loadWheelData: async function() {
        this.showLoading(true);
        
        try {
            const response = await $.get(`${this.config.baseUrl}/GetWheelData`);
            
            if (response.success && response.data) {
                console.log('Wheel data loaded:', response.data);
                this.renderLifeAreas(response.data.areas);
                this.updateStatistics(response.data);
                this.initializeChart(response.data.areas);
            } else {
                this.showError('Error al cargar los datos de la rueda de la vida');
            }
        } catch (error) {
            console.error('Error loading wheel data:', error);
            this.showError('Error de conexión al cargar los datos');
        } finally {
            this.showLoading(false);
        }
    },

    // Render life areas in the panel
    renderLifeAreas: function(areas) {
        const container = $('#scoresContainer');
        container.empty();
        
        areas.forEach(area => {
            const areaHtml = `
                <div class="area-card" data-area-id="${area.lifeAreaId}">
                    <div class="area-card-content">
                        <div class="area-icon" style="background-color: ${area.iconColor};">
                            <i class="${area.iconClass}"></i>
                        </div>
                        <div class="area-info">
                            <div class="area-name">${area.areaName}</div>
                            <p class="area-description">Evalúa esta área de tu vida</p>
                        </div>
                        <div class="score-selector">
                            <div class="current-score" id="currentScore_${area.lifeAreaId}">
                                ${area.currentScore || 5}
                            </div>
                            <select class="form-select score-input" 
                                    data-area-id="${area.lifeAreaId}" 
                                    data-area-name="${area.areaName}"
                                    data-area-color="${area.iconColor}">
                                ${this.generateScoreOptions(area.currentScore || 5)}
                            </select>
                            <span class="score-label">Puntuación</span>
                        </div>
                    </div>
                </div>
            `;
            container.append(areaHtml);
        });
        
        // Store current data
        this.config.currentData = areas;
    },

    // Generate score options 1-10
    generateScoreOptions: function(currentScore) {
        let options = '';
        for (let i = 1; i <= 10; i++) {
            const selected = i === currentScore ? 'selected' : '';
            options += `<option value="${i}" ${selected}>${i}</option>`;
        }
        return options;
    },

    // Update statistics display
    updateStatistics: function(data) {
        $('#totalScoreNumber').text(data.totalScore);
        $('#averageScore').text(data.averageScore.toFixed(1));
        $('#areasCount').text(data.areasCount);
        
        // Update max possible score in label
        $('.total-score-label').html(`Total Score / ${data.areasCount * 10}`);
    },

    // Initialize Chart.js radar chart
    initializeChart: function(areas) {
        const canvas = document.getElementById('wheelOfLifeChart');
        if (!canvas) return;
        
        const ctx = canvas.getContext('2d');
        
        // Destroy existing chart if any
        if (this.config.chart) {
            this.config.chart.destroy();
        }
        
        // Prepare chart data
        const labels = areas.map(area => area.areaName);
        const data = areas.map(area => area.currentScore || 5);
        const backgroundColors = areas.map(area => this.hexToRgba(area.iconColor, 0.2));
        const borderColors = areas.map(area => area.iconColor);

        this.config.chart = new Chart(ctx, {
            type: 'radar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Puntuación Actual',
                    data: data,
                    backgroundColor: this.hexToRgba('#667eea', 0.2),
                    borderColor: '#667eea',
                    borderWidth: 3,
                    pointBackgroundColor: borderColors,
                    pointBorderColor: '#fff',
                    pointBorderWidth: 2,
                    pointRadius: 6,
                    pointHoverRadius: 8,
                    pointHoverBackgroundColor: borderColors,
                    pointHoverBorderColor: '#fff',
                    pointHoverBorderWidth: 3
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: false
                    },
                    tooltip: {
                        backgroundColor: 'rgba(0, 0, 0, 0.8)',
                        titleColor: '#fff',
                        bodyColor: '#fff',
                        borderColor: '#667eea',
                        borderWidth: 1,
                        cornerRadius: 8,
                        displayColors: false,
                        callbacks: {
                            label: function(context) {
                                return `${context.label}: ${context.parsed.r}/10`;
                            }
                        }
                    }
                },
                scales: {
                    r: {
                        beginAtZero: true,
                        min: 0,
                        max: 10,
                        stepSize: 1,
                        ticks: {
                            font: {
                                size: 12,
                                weight: '600'
                            },
                            color: '#718096',
                            backdropColor: 'rgba(255, 255, 255, 0.9)',
                            backdropPadding: 3,
                            showLabelBackdrop: true
                        },
                        grid: {
                            color: '#e2e8f0',
                            lineWidth: 1
                        },
                        angleLines: {
                            color: '#e2e8f0',
                            lineWidth: 1
                        },
                        pointLabels: {
                            font: {
                                size: 13,
                                weight: '600'
                            },
                            color: '#2d3748',
                            padding: 15
                        }
                    }
                },
                animation: {
                    duration: 1000,
                    easing: 'easeInOutQuart'
                },
                interaction: {
                    intersect: false,
                    mode: 'nearest'
                }
            }
        });
    },

    // Bind all event handlers
    bindEvents: function() {
        // Score select change events
        $(document).on('change', '.score-input', (e) => {
            const areaId = parseInt($(e.target).data('area-id'));
            const newScore = parseInt($(e.target).val());
            const areaName = $(e.target).data('area-name');
            
            // Update the visual score display
            $(`#currentScore_${areaId}`).text(newScore);
            
            // Update in memory data
            this.updateAreaScore(areaId, newScore);
            
            // Update chart
            this.updateChart();
            
            // Update statistics
            this.updateLocalStatistics();
            
            console.log(`Updated ${areaName}: ${newScore}`);
        });

        // Save scores button
        $('#saveScoresBtn').on('click', (e) => {
            e.preventDefault();
            this.saveScores();
        });

        // Remove any existing event handlers to prevent duplicates
        $('#saveScoresBtn').off('click').on('click', (e) => {
            e.preventDefault();
            this.saveScores();
        });
    },

    // Update area score in current data
    updateAreaScore: function(areaId, newScore) {
        const area = this.config.currentData.find(a => a.lifeAreaId === areaId);
        if (area) {
            area.currentScore = newScore;
        }
    },

    // Update the chart with new data
    updateChart: function() {
        if (!this.config.chart) return;

        const newData = this.config.currentData.map(area => area.currentScore || 5);
        
        // Update chart data
        this.config.chart.data.datasets[0].data = newData;
        this.config.chart.update('none');
    },

    // Update statistics locally
    updateLocalStatistics: function() {
        let totalScore = 0;
        let scoredAreas = 0;
        
        this.config.currentData.forEach(area => {
            if (area.currentScore) {
                totalScore += area.currentScore;
                scoredAreas++;
            }
        });
        
        const averageScore = scoredAreas > 0 ? (totalScore / scoredAreas).toFixed(1) : '0.0';
        
        $('#totalScoreNumber').text(totalScore);
        $('#averageScore').text(averageScore);
    },

    // Save scores to server
    saveScores: async function() {
        if (this.config.isLoading) return;

        this.config.isLoading = true;
        const $btn = $('#saveScoresBtn');
        const originalText = $btn.html();
        
        $btn.prop('disabled', true).html('<i class="fas fa-spinner fa-spin me-2"></i>Guardando...');

        // Get CSRF token
        const token = $('input[name="__RequestVerificationToken"]').val();

        // Prepare scores data in PascalCase for C#
        const scores = this.config.currentData.map(area => ({
            LifeAreaId: area.lifeAreaId,
            Score: area.currentScore || 5
        }));

        const requestData = {
            Scores: scores
        };

        console.log('Saving scores:', requestData);

        try {
            const response = await $.ajax({
                url: `${this.config.baseUrl}/SaveScores`,
                method: 'POST',
                headers: {
                    'RequestVerificationToken': token
                },
                contentType: 'application/json',
                data: JSON.stringify(requestData)
            });

            console.log('Save response:', response);

            if (response.success) {
                this.showMessage('success', response.message || 'Puntuaciones guardadas exitosamente');
            } else {
                this.showMessage('error', response.message || 'Error al guardar las puntuaciones');
            }
        } catch (error) {
            console.error('Error saving scores:', error);
            let errorMessage = 'Error de conexión. Por favor verifique su conexión e intente nuevamente.';
            
            if (error.responseJSON && error.responseJSON.message) {
                errorMessage = error.responseJSON.message;
            }
            
            this.showMessage('error', errorMessage);
        } finally {
            this.config.isLoading = false;
            $btn.prop('disabled', false).html(originalText);
        }
    },

    // Show loading state
    showLoading: function(show) {
        if (show) {
            $('#chartLoading').removeClass('d-none');
            $('#scoresContainer').css('opacity', '0.5');
        } else {
            $('#chartLoading').addClass('d-none');
            $('#scoresContainer').css('opacity', '1');
        }
    },

    // Show success/error message
    showMessage: function(type, message) {
        const className = type === 'success' ? 'success-message' : 'error-message';
        const icon = type === 'success' ? 'fas fa-check-circle' : 'fas fa-exclamation-triangle';
        
        const messageHtml = `
            <div class="${className}">
                <i class="${icon} me-2"></i>${message}
            </div>
        `;
        
        $('#saveMessage').html(messageHtml);
        
        // Auto remove after 4 seconds
        setTimeout(() => {
            $('#saveMessage').fadeOut(300, function() {
                $(this).empty().show();
            });
        }, 4000);
    },

    // Show error message
    showError: function(message) {
        this.showMessage('error', message);
    },

    // Utility functions
    hexToRgba: function(hex, opacity) {
        if (!hex || !hex.startsWith('#')) return `rgba(102, 126, 234, ${opacity})`;
        
        const r = parseInt(hex.slice(1, 3), 16);
        const g = parseInt(hex.slice(3, 5), 16);
        const b = parseInt(hex.slice(5, 7), 16);
        
        return `rgba(${r}, ${g}, ${b}, ${opacity})`;
    }
};

// Initialize when document is ready
$(document).ready(function() {
    // Add CSRF token if not present
    if (!$('input[name="__RequestVerificationToken"]').length) {
        // This should be added by the view, but just in case
        console.warn('CSRF token not found in the page');
    }
    
    WheelOfLife.init();
});