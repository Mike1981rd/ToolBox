// Modern Wheel of Life with ApexCharts
const WheelOfLife = {
    config: {
        chart: null,
        baseUrl: '/WheelOfLife',
        currentData: [],
        isLoading: false
    },

    // Initialize the module
    init: function() {
        console.log('Initializing Modern Wheel of Life module...');
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

    // Initialize ApexCharts RadialBar visualization
    initializeChart: function(areas) {
        const chartContainer = document.getElementById('wheelOfLifeChart');
        if (!chartContainer) return;

        // Destroy existing chart if any
        if (this.config.chart) {
            this.config.chart.destroy();
        }

        // Prepare data
        const series = areas.map(area => ((area.currentScore || 5) / 10) * 100); // Convert to percentage
        const labels = areas.map(area => area.areaName);
        const colors = areas.map(area => area.iconColor);

        // Chart configuration
        const options = {
            series: series,
            chart: {
                type: 'radialBar',
                height: 580,
                animations: {
                    enabled: true,
                    easing: 'easeinout',
                    speed: 800,
                    animateGradually: {
                        enabled: true,
                        delay: 150
                    },
                    dynamicAnimation: {
                        enabled: true,
                        speed: 350
                    }
                },
                background: 'transparent',
                fontFamily: 'Inter, system-ui, -apple-system, sans-serif',
                // Ultra-crisp ApexCharts configuration
                pixelRatio: window.devicePixelRatio || 2,
                redrawOnParentResize: true,
                redrawOnWindowResize: true
            },
            plotOptions: {
                radialBar: {
                    offsetY: 0,
                    startAngle: 0,
                    endAngle: 360,
                    hollow: {
                        margin: 5,
                        size: '15%',
                        background: 'transparent',
                        image: undefined,
                        imageOffsetX: 0,
                        imageOffsetY: 0,
                        position: 'front',
                        dropShadow: {
                            enabled: true,
                            top: 3,
                            left: 0,
                            blur: 4,
                            opacity: 0.24
                        }
                    },
                    track: {
                        background: '#f2f2f2',
                        strokeWidth: '67%',
                        margin: 0,
                        dropShadow: {
                            enabled: true,
                            top: -3,
                            left: 0,
                            blur: 4,
                            opacity: 0.35
                        }
                    },
                    dataLabels: {
                        show: true,
                        name: {
                            offsetY: -10,
                            show: true,
                            color: '#2d3748',
                            fontSize: '11px',
                            fontWeight: '600',
                            fontFamily: 'Inter, system-ui, -apple-system, sans-serif'
                        },
                        value: {
                            formatter: function(val, opts) {
                                return Math.round((val / 100) * 10) + '/10';
                            },
                            color: '#667eea',
                            fontSize: '13px',
                            fontWeight: 'bold',
                            show: true,
                            fontFamily: 'Inter, system-ui, -apple-system, sans-serif'
                        }
                    }
                }
            },
            colors: colors,
            labels: labels,
            legend: {
                show: true,
                floating: true,
                fontSize: '12px',
                position: 'bottom',
                offsetX: 0,
                offsetY: 80, // Much more spacing from chart
                labels: {
                    useSeriesColors: true,
                },
                markers: {
                    size: 6,
                    strokeWidth: 0,
                    strokeColor: '#fff',
                    fillColors: colors,
                    radius: 12
                },
                formatter: function(seriesName, opts) {
                    return seriesName + ': ' + Math.round((opts.w.globals.series[opts.seriesIndex] / 100) * 10) + '/10';
                },
                itemMargin: {
                    vertical: 8, // More vertical spacing between legend items
                    horizontal: 20 // More horizontal spacing between legend items
                }
            },
            tooltip: {
                enabled: true,
                theme: 'dark',
                style: {
                    fontSize: '12px',
                    fontFamily: 'Inter, system-ui, -apple-system, sans-serif'
                },
                y: {
                    formatter: function(val, opts) {
                        const score = Math.round((val / 100) * 10);
                        return `Puntuación: ${score}/10`;
                    }
                }
            },
            responsive: [{
                breakpoint: 768,
                options: {
                    chart: {
                        height: 480
                    },
                    legend: {
                        position: 'bottom',
                        offsetY: 60 // More spacing on mobile too
                    }
                }
            }]
        };

        // Create chart
        this.config.chart = new ApexCharts(chartContainer, options);
        this.config.chart.render();
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

        // Convert scores to percentages for radial chart
        const newSeries = this.config.currentData.map(area => ((area.currentScore || 5) / 10) * 100);
        
        // Update chart with animation
        this.config.chart.updateSeries(newSeries);
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
    }
};

// Initialize when document is ready
$(document).ready(function() {
    // Add CSRF token if not present
    if (!$('input[name="__RequestVerificationToken"]').length) {
        console.warn('CSRF token not found in the page');
    }
    
    WheelOfLife.init();
});