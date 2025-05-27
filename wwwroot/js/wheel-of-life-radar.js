// Wheel of Life Radar Chart Implementation
const WheelOfLifeRadar = {
    config: {
        chart: null,
        baseUrl: '/WheelOfLife',
        currentData: [],
        isLoading: false,
        // Color palette for sectors - vibrant colors like the reference
        sectorColors: [
            'rgba(255, 99, 132, 0.7)',   // Red
            'rgba(54, 162, 235, 0.7)',   // Blue
            'rgba(255, 206, 86, 0.7)',   // Yellow
            'rgba(75, 192, 192, 0.7)',   // Teal
            'rgba(153, 102, 255, 0.7)',  // Purple
            'rgba(255, 159, 64, 0.7)',   // Orange
            'rgba(231, 233, 237, 0.7)',  // Gray
            'rgba(121, 224, 117, 0.7)'   // Green
        ]
    },

    // Initialize the module
    init: function() {
        console.log('Initializing Wheel of Life Radar Chart...');
        this.bindEvents();
        this.loadWheelData();
    },

    // Load initial data from API
    loadWheelData: async function() {
        this.showLoading(true);
        
        try {
            const response = await fetch(`${this.config.baseUrl}/GetWheelData`);
            const result = await response.json();
            
            if (result.success && result.data) {
                console.log('Wheel data loaded:', result.data);
                this.renderLifeAreas(result.data.areas);
                this.updateStatistics(result.data);
                this.initializeRadarChart(result.data.areas);
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

    // Initialize Chart.js Radar Chart
    initializeRadarChart: function(areas) {
        const canvas = document.getElementById('wheelOfLifeChart');
        if (!canvas) return;

        // Destroy existing chart if any
        if (this.config.chart) {
            this.config.chart.destroy();
        }

        // Prepare data
        const labels = areas.map(area => area.areaName);
        const data = areas.map(area => area.currentScore || 5);
        
        // Create datasets for colored sectors
        const datasets = this.createSectorDatasets(areas);

        // Chart configuration matching the reference image
        const config = {
            type: 'radar',
            data: {
                labels: labels,
                datasets: [
                    // Colored sectors (draw first so they're in background)
                    ...datasets,
                    // Main data line (draw on top)
                    {
                        label: 'Mi Evaluación',
                        data: data,
                        borderColor: 'rgba(34, 84, 168, 1)',
                        backgroundColor: 'rgba(34, 84, 168, 0.2)',
                        borderWidth: 3,
                        pointBackgroundColor: 'rgba(34, 84, 168, 1)',
                        pointBorderColor: '#fff',
                        pointBorderWidth: 2,
                        pointRadius: 8,
                        pointHoverRadius: 10,
                        tension: 0
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: false // No legend as per reference
                    },
                    tooltip: {
                        callbacks: {
                            label: function(context) {
                                if (context.datasetIndex === 0) {
                                    return context.label + ': ' + context.raw + '/10';
                                }
                                return '';
                            }
                        },
                        filter: function(tooltipItem) {
                            return tooltipItem.datasetIndex === 0;
                        }
                    }
                },
                scales: {
                    r: {
                        min: 0,
                        max: 10,
                        stepSize: 1,
                        startAngle: 0,
                        grid: {
                            circular: true,
                            color: 'rgba(0, 0, 0, 0.1)',
                            lineWidth: 1
                        },
                        angleLines: {
                            color: 'rgba(0, 0, 0, 0.1)',
                            lineWidth: 1
                        },
                        pointLabels: {
                            font: {
                                size: 14,
                                weight: '600',
                                family: '"Segoe UI", -apple-system, BlinkMacSystemFont, Roboto, sans-serif'
                            },
                            color: '#2d3748',
                            padding: 15,
                            centerPointLabels: false,
                            callback: function(value, index) {
                                // Truncate long labels
                                return value.length > 15 ? value.substr(0, 15) + '...' : value;
                            }
                        },
                        ticks: {
                            stepSize: 1,
                            min: 0,
                            max: 10,
                            display: true,
                            font: {
                                size: 12,
                                weight: '600'
                            },
                            color: '#444',
                            backdropColor: 'rgba(255, 255, 255, 0.8)',
                            backdropPadding: 4,
                            callback: function(value) {
                                // Show all integer values from 0 to 10
                                return value;
                            }
                        }
                    }
                },
                elements: {
                    line: {
                        borderWidth: 3
                    },
                    point: {
                        radius: 6,
                        hoverRadius: 8
                    }
                }
            }
        };

        // Create chart
        this.config.chart = new Chart(canvas, config);
    },

    // Create colored sector datasets
    createSectorDatasets: function(areas) {
        const datasets = [];
        
        // Create background sectors for visual effect like reference image
        // Each area gets its own background color at full scale
        areas.forEach((area, index) => {
            const color = area.iconColor || this.config.sectorColors[index % this.config.sectorColors.length];
            
            // Create data array with only this axis at max value
            const backgroundData = new Array(areas.length).fill(0);
            backgroundData[index] = 10;
            
            datasets.push({
                label: '',
                data: backgroundData,
                backgroundColor: this.hexToRgba(color, 0.25), // Semi-transparent background
                borderColor: this.hexToRgba(color, 0.4),
                borderWidth: 1,
                pointRadius: 0,
                pointHoverRadius: 0,
                fill: true,
                showLine: true
            });
        });
        
        return datasets;
    },

    // Convert hex color to rgba
    hexToRgba: function(hex, alpha) {
        // Handle both hex formats
        if (hex.startsWith('rgba')) return hex;
        
        const r = parseInt(hex.slice(1, 3), 16);
        const g = parseInt(hex.slice(3, 5), 16);
        const b = parseInt(hex.slice(5, 7), 16);
        
        return `rgba(${r}, ${g}, ${b}, ${alpha})`;
    },

    // Render life areas in the panel
    renderLifeAreas: function(areas) {
        const container = document.getElementById('scoresContainer');
        if (!container) return;
        
        container.innerHTML = '';
        
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
            container.innerHTML += areaHtml;
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
        const totalScore = document.getElementById('totalScoreNumber');
        const averageScore = document.getElementById('averageScore');
        const areasCount = document.getElementById('areasCount');
        const totalScoreLabel = document.querySelector('.total-score-label');
        
        if (totalScore) totalScore.textContent = data.totalScore;
        if (averageScore) averageScore.textContent = data.averageScore.toFixed(1);
        if (areasCount) areasCount.textContent = data.areasCount;
        if (totalScoreLabel) totalScoreLabel.innerHTML = `Total Score / ${data.areasCount * 10}`;
    },

    // Bind all event handlers
    bindEvents: function() {
        // Score select change events
        document.addEventListener('change', (e) => {
            if (e.target.classList.contains('score-input')) {
                const areaId = parseInt(e.target.dataset.areaId);
                const newScore = parseInt(e.target.value);
                const areaName = e.target.dataset.areaName;
                
                // Update the visual score display
                const scoreDisplay = document.getElementById(`currentScore_${areaId}`);
                if (scoreDisplay) scoreDisplay.textContent = newScore;
                
                // Update in memory data
                this.updateAreaScore(areaId, newScore);
                
                // Update chart
                this.updateChart();
                
                // Update statistics
                this.updateLocalStatistics();
                
                console.log(`Updated ${areaName}: ${newScore}`);
            }
        });

        // Save scores button
        const saveBtn = document.getElementById('saveScoresBtn');
        if (saveBtn) {
            saveBtn.addEventListener('click', (e) => {
                e.preventDefault();
                this.saveScores();
            });
        }
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

        // Update main dataset with new scores
        const newScores = this.config.currentData.map(area => area.currentScore || 5);
        this.config.chart.data.datasets[0].data = newScores;
        
        // Update chart with animation
        this.config.chart.update('active');
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
        
        const totalScoreEl = document.getElementById('totalScoreNumber');
        const averageScoreEl = document.getElementById('averageScore');
        
        if (totalScoreEl) totalScoreEl.textContent = totalScore;
        if (averageScoreEl) averageScoreEl.textContent = averageScore;
    },

    // Save scores to server
    saveScores: async function() {
        if (this.config.isLoading) return;

        this.config.isLoading = true;
        const btn = document.getElementById('saveScoresBtn');
        const originalText = btn.innerHTML;
        
        btn.disabled = true;
        btn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Guardando...';

        // Get CSRF token
        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

        // Prepare scores data
        const scores = this.config.currentData.map(area => ({
            LifeAreaId: area.lifeAreaId,
            Score: area.currentScore || 5
        }));

        const requestData = {
            Scores: scores
        };

        console.log('Saving scores:', requestData);

        try {
            const response = await fetch(`${this.config.baseUrl}/SaveScores`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify(requestData)
            });

            const result = await response.json();
            console.log('Save response:', result);

            if (result.success) {
                this.showMessage('success', result.message || 'Puntuaciones guardadas exitosamente');
            } else {
                this.showMessage('error', result.message || 'Error al guardar las puntuaciones');
            }
        } catch (error) {
            console.error('Error saving scores:', error);
            this.showMessage('error', 'Error de conexión. Por favor intente nuevamente.');
        } finally {
            this.config.isLoading = false;
            btn.disabled = false;
            btn.innerHTML = originalText;
        }
    },

    // Show loading state
    showLoading: function(show) {
        const loading = document.getElementById('chartLoading');
        const container = document.getElementById('scoresContainer');
        
        if (loading) {
            if (show) {
                loading.classList.remove('d-none');
            } else {
                loading.classList.add('d-none');
            }
        }
        
        if (container) {
            container.style.opacity = show ? '0.5' : '1';
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
        
        const saveMessage = document.getElementById('saveMessage');
        if (saveMessage) {
            saveMessage.innerHTML = messageHtml;
            
            // Auto remove after 4 seconds
            setTimeout(() => {
                saveMessage.style.opacity = '0';
                setTimeout(() => {
                    saveMessage.innerHTML = '';
                    saveMessage.style.opacity = '1';
                }, 300);
            }, 4000);
        }
    },

    // Show error message
    showError: function(message) {
        this.showMessage('error', message);
    }
};

// Initialize when document is ready
document.addEventListener('DOMContentLoaded', function() {
    // Add CSRF token if not present
    if (!document.querySelector('input[name="__RequestVerificationToken"]')) {
        console.warn('CSRF token not found in the page');
    }
    
    WheelOfLifeRadar.init();
});