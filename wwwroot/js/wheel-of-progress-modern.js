// Modern Wheel of Progress with Chart.js Implementation
console.log('Loading wheel-of-progress-modern.js...');

const WheelOfProgress = {
    config: {
        chart: null,
        baseUrl: '/WheelOfProgress',
        currentData: [],
        isLoading: false,
        datepickers: [],
        // Modern color palette for progress visualization
        colors: [
            '#667eea', // Primary purple-blue
            '#48bb78', // Green
            '#f6ad55', // Orange
            '#fc8181', // Red
            '#63b3ed', // Light blue
            '#9f7aea', // Purple
            '#38b2ac', // Teal
            '#ed8936'  // Dark orange
        ]
    },

    // Initialize the module
    init: function() {
        console.log('Initializing Modern Wheel of Progress...');
        console.log('Template literal test:', `${1 + 1}`); // Should log "2"
        this.loadWheelData();
        this.bindEvents();
    },

    // Load data from backend
    loadWheelData: async function() {
        this.showLoading(true);
        
        try {
            const result = await $.get(`${this.config.baseUrl}/GetWheelData`);
            
            if (result.success && result.data) {
                console.log('Wheel data loaded:', result.data);
                this.config.currentData = result.data;
                this.renderProgressTable(result.data.areas);
                this.updateStatistics(result.data.stats);
                this.initializeChart();
                this.initializeDatepickers();
            } else {
                this.showError('Error al cargar los datos');
            }
        } catch (error) {
            console.error('Error loading wheel data:', error);
            this.showError('Error de conexión al cargar los datos');
        } finally {
            this.showLoading(false);
        }
    },

    // Initialize modern Chart.js visualization
    initializeChart: function() {
        const canvas = document.getElementById('progressWheelChart');
        if (!canvas) return;

        // Destroy existing chart if any
        if (this.config.chart) {
            this.config.chart.destroy();
        }

        // Prepare data for modern radial progress chart
        const areas = this.config.currentData.areas || [];
        
        // Calculate average progress for each area
        const chartData = areas.map(area => {
            const categorias = area.categorias || [];
            if (categorias.length === 0) return 0;
            
            const totalProgress = categorias.reduce((sum, cat) => sum + (cat.porcentajeProgreso || 0), 0);
            return Math.round(totalProgress / categorias.length);
        });

        const labels = areas.map(area => area.nombre);
        const backgroundColors = areas.map((area, index) => 
            area.iconColor || this.config.colors[index % this.config.colors.length]
        );

        // Create gradient colors for modern look
        const gradientColors = backgroundColors.map(color => {
            return this.createGradientColor(canvas, color);
        });

        // Calculate global progress for center display
        const globalProgress = chartData.length > 0 ? 
            Math.round(chartData.reduce((a, b) => a + b, 0) / chartData.length) : 0;

        // Chart configuration
        const config = {
            type: 'doughnut',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Progreso',
                    data: chartData,
                    backgroundColor: gradientColors,
                    borderColor: backgroundColors,
                    borderWidth: 3,
                    cutout: '65%', // Large center hole for global progress
                    borderRadius: 8,
                    borderJoinStyle: 'round',
                    hoverBorderWidth: 4,
                    hoverBorderColor: '#fff',
                    hoverBackgroundColor: backgroundColors.map(color => this.lightenColor(color, 0.1))
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: true,
                aspectRatio: 1.5,
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            usePointStyle: true,
                            pointStyle: 'circle',
                            padding: 20,
                            font: {
                                size: 12,
                                weight: '600'
                            },
                            color: '#4a5568',
                            generateLabels: function(chart) {
                                const data = chart.data;
                                return data.labels.map((label, index) => {
                                    return {
                                        text: `${label}: ${data.datasets[0].data[index]}%`,
                                        fillStyle: data.datasets[0].backgroundColor[index],
                                        strokeStyle: data.datasets[0].borderColor[index],
                                        lineWidth: 2,
                                        index: index
                                    };
                                });
                            }
                        },
                        onClick: function(e, legendItem, legend) {
                            // Custom legend click behavior
                            const index = legendItem.index;
                            const chart = legend.chart;
                            const meta = chart.getDatasetMeta(0);
                            meta.data[index].hidden = !meta.data[index].hidden;
                            chart.update();
                        }
                    },
                    tooltip: {
                        backgroundColor: 'rgba(0, 0, 0, 0.8)',
                        titleColor: '#fff',
                        bodyColor: '#fff',
                        borderColor: '#667eea',
                        borderWidth: 1,
                        cornerRadius: 8,
                        displayColors: true,
                        callbacks: {
                            label: function(context) {
                                const label = context.label || '';
                                const value = context.parsed || 0;
                                return `${label}: ${value}% completado`;
                            },
                            afterLabel: function(context) {
                                const areas = WheelOfProgress.config.currentData.areas || [];
                                const area = areas[context.dataIndex];
                                if (area && area.categorias) {
                                    return `${area.categorias.length} categorías`;
                                }
                                return '';
                            }
                        }
                    }
                },
                elements: {
                    arc: {
                        borderWidth: 3,
                        borderColor: '#fff'
                    }
                },
                animation: {
                    animateRotate: true,
                    animateScale: true,
                    duration: 1200,
                    easing: 'easeOutQuart'
                },
                onHover: (event, elements) => {
                    if (elements.length > 0) {
                        canvas.style.cursor = 'pointer';
                    } else {
                        canvas.style.cursor = 'default';
                    }
                }
            },
            plugins: [{
                id: 'centerText',
                beforeDraw: function(chart) {
                    // Draw global progress in center
                    const ctx = chart.ctx;
                    const centerX = chart.chartArea.left + (chart.chartArea.right - chart.chartArea.left) / 2;
                    const centerY = chart.chartArea.top + (chart.chartArea.bottom - chart.chartArea.top) / 2;

                    ctx.save();
                    
                    // Global progress number
                    ctx.font = 'bold 28px "Segoe UI", sans-serif';
                    ctx.fillStyle = '#2d3748';
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'middle';
                    ctx.fillText(`${globalProgress}%`, centerX, centerY - 10);
                    
                    // Label
                    ctx.font = '14px "Segoe UI", sans-serif';
                    ctx.fillStyle = '#718096';
                    ctx.fillText('Progreso Global', centerX, centerY + 20);
                    
                    ctx.restore();
                }
            }]
        };

        // Create chart
        this.config.chart = new Chart(canvas, config);
    },

    // Create gradient color for modern appearance
    createGradientColor: function(canvas, baseColor) {
        const ctx = canvas.getContext('2d');
        const gradient = ctx.createLinearGradient(0, 0, 0, 400);
        gradient.addColorStop(0, baseColor);
        gradient.addColorStop(1, this.darkenColor(baseColor, 0.2));
        return gradient;
    },

    // Utility function to lighten color
    lightenColor: function(color, percent) {
        const num = parseInt(color.replace("#", ""), 16);
        const amt = Math.round(2.55 * percent * 100);
        const R = (num >> 16) + amt;
        const G = (num >> 8 & 0x00FF) + amt;
        const B = (num & 0x0000FF) + amt;
        return "#" + (0x1000000 + (R < 255 ? R < 1 ? 0 : R : 255) * 0x10000 +
            (G < 255 ? G < 1 ? 0 : G : 255) * 0x100 +
            (B < 255 ? B < 1 ? 0 : B : 255)).toString(16).slice(1);
    },

    // Utility function to darken color
    darkenColor: function(color, percent) {
        const num = parseInt(color.replace("#", ""), 16);
        const amt = Math.round(2.55 * percent * 100);
        const R = (num >> 16) - amt;
        const G = (num >> 8 & 0x00FF) - amt;
        const B = (num & 0x0000FF) - amt;
        return "#" + (0x1000000 + (R > 255 ? 255 : R < 0 ? 0 : R) * 0x10000 +
            (G > 255 ? 255 : G < 0 ? 0 : G) * 0x100 +
            (B > 255 ? 255 : B < 0 ? 0 : B)).toString(16).slice(1);
    },

    // Render progress table
    renderProgressTable: function(areas) {
        const tbody = document.getElementById('progressTableBody');
        if (!tbody) return;
        
        tbody.innerHTML = '';
        
        areas.forEach(area => {
            const categorias = area.categorias || [];
            categorias.forEach((categoria, index) => {
                const isFirstCategory = index === 0;
                const rowspan = categorias.length;
                
                // Ensure we have valid values
                const metaValue = categoria.meta || '';
                const progressValue = categoria.porcentajeProgreso || 0;
                const actionValue = categoria.siguienteAccion || '';
                
                console.log('Values for categoria:', {
                    id: categoria.id,
                    metaValue,
                    progressValue,
                    actionValue
                });
                
                let fechaLimite = '';
                if (categoria.fechaLimite) {
                    try {
                        const date = new Date(categoria.fechaLimite);
                        if (!isNaN(date.getTime())) {
                            fechaLimite = date.toISOString().split('T')[0];
                        }
                    } catch (e) {
                        console.warn('Invalid date for categoria:', categoria.fechaLimite);
                        fechaLimite = '';
                    }
                }
                
                let row = document.createElement('tr');
                row.setAttribute('data-area-id', area.id);
                row.setAttribute('data-category-id', categoria.id);
                
                if (isFirstCategory) {
                    const textColor = area.iconColor === '#2c3e50' ? 'white' : 'white';
                    
                    // Build HTML using string concatenation to avoid template literal issues
                    let html = '';
                    html += '<td class="area-cell" rowspan="' + rowspan + '" style="background-color: ' + area.iconColor + '; color: ' + textColor + ';">';
                    html += '<div>';
                    html += '<i class="' + area.iconClass + '"></i><br>';
                    html += '<small>' + area.nombre + '</small>';
                    html += '</div>';
                    html += '</td>';
                    html += '<td class="category-cell">';
                    html += '<span>' + categoria.nombre + '</span>';
                    html += '</td>';
                    html += '<td>';
                    html += '<input type="text" class="form-control input-progress goal-input" ';
                    html += 'data-category-id="' + categoria.id + '" ';
                    html += 'value="' + metaValue + '" ';
                    html += 'placeholder="Define tu meta..." maxlength="1000">';
                    html += '</td>';
                    html += '<td>';
                    html += '<input type="number" class="form-control input-progress progress-input" ';
                    html += 'data-category-id="' + categoria.id + '" ';
                    html += 'value="' + progressValue + '" ';
                    html += 'min="0" max="100" placeholder="0">';
                    html += '</td>';
                    html += '<td>';
                    html += '<input type="text" class="form-control input-progress action-input" ';
                    html += 'data-category-id="' + categoria.id + '" ';
                    html += 'value="' + actionValue + '" ';
                    html += 'placeholder="Próxima acción..." maxlength="1000">';
                    html += '</td>';
                    html += '<td>';
                    html += '<input type="text" class="form-control input-progress deadline-input flatpickr-input" ';
                    html += 'data-category-id="' + categoria.id + '" ';
                    html += 'value="' + fechaLimite + '" ';
                    html += 'placeholder="Seleccionar fecha..." readonly>';
                    html += '</td>';
                    
                    row.innerHTML = html;
                } else {
                    // Build HTML using string concatenation to avoid template literal issues
                    let html = '';
                    html += '<td class="category-cell">';
                    html += '<span>' + categoria.nombre + '</span>';
                    html += '</td>';
                    html += '<td>';
                    html += '<input type="text" class="form-control input-progress goal-input" ';
                    html += 'data-category-id="' + categoria.id + '" ';
                    html += 'value="' + metaValue + '" ';
                    html += 'placeholder="Define tu meta..." maxlength="1000">';
                    html += '</td>';
                    html += '<td>';
                    html += '<input type="number" class="form-control input-progress progress-input" ';
                    html += 'data-category-id="' + categoria.id + '" ';
                    html += 'value="' + progressValue + '" ';
                    html += 'min="0" max="100" placeholder="0">';
                    html += '</td>';
                    html += '<td>';
                    html += '<input type="text" class="form-control input-progress action-input" ';
                    html += 'data-category-id="' + categoria.id + '" ';
                    html += 'value="' + actionValue + '" ';
                    html += 'placeholder="Próxima acción..." maxlength="1000">';
                    html += '</td>';
                    html += '<td>';
                    html += '<input type="text" class="form-control input-progress deadline-input flatpickr-input" ';
                    html += 'data-category-id="' + categoria.id + '" ';
                    html += 'value="' + fechaLimite + '" ';
                    html += 'placeholder="Seleccionar fecha..." readonly>';
                    html += '</td>';
                    
                    row.innerHTML = html;
                }
                
                tbody.appendChild(row);
            });
        });
    },

    // Update statistics display
    updateStatistics: function(stats) {
        const elements = {
            globalProgress: document.getElementById('globalProgressStat'),
            totalMetas: document.getElementById('totalMetasStat'),
            metasCompletadas: document.getElementById('metasCompletadasStat'),
            ultimaActualizacion: document.getElementById('ultimaActualizacionStat')
        };

        if (elements.globalProgress) {
            elements.globalProgress.textContent = Math.round(stats.progresoGlobal) + '%';
        }
        
        if (elements.totalMetas) {
            elements.totalMetas.textContent = stats.totalMetas;
        }
        
        if (elements.metasCompletadas) {
            elements.metasCompletadas.textContent = stats.metasCompletadas;
        }
        
        if (elements.ultimaActualizacion) {
            if (stats.ultimaActualizacion) {
                const fecha = new Date(stats.ultimaActualizacion);
                elements.ultimaActualizacion.textContent = fecha.toLocaleDateString();
            } else {
                elements.ultimaActualizacion.textContent = '-';
            }
        }
    },

    // Update chart with new data
    updateChart: function() {
        if (!this.config.chart) return;

        const areas = this.config.currentData.areas || [];
        
        // Recalculate progress for each area
        const newData = areas.map(area => {
            const categorias = area.categorias || [];
            if (categorias.length === 0) return 0;
            
            // Get current values from inputs
            let totalProgress = 0;
            categorias.forEach(categoria => {
                const input = document.querySelector(`.progress-input[data-category-id="${categoria.id}"]`);
                const value = input ? parseInt(input.value) || 0 : categoria.porcentajeProgreso || 0;
                totalProgress += value;
            });
            
            return Math.round(totalProgress / categorias.length);
        });

        // Update chart data
        this.config.chart.data.datasets[0].data = newData;
        this.config.chart.update('active');
    },

    // Initialize Flatpickr date pickers
    initializeDatepickers: function() {
        const dateInputs = document.querySelectorAll('.deadline-input');
        
        dateInputs.forEach(input => {
            if (typeof flatpickr !== 'undefined') {
                // Clear any invalid initial value
                const currentValue = input.value;
                if (currentValue && currentValue.trim() !== '') {
                    try {
                        const testDate = new Date(currentValue);
                        if (isNaN(testDate.getTime())) {
                            input.value = ''; // Clear invalid date
                        }
                    } catch (e) {
                        input.value = ''; // Clear invalid date
                    }
                }
                
                const flatpickrInstance = flatpickr(input, {
                    dateFormat: "Y-m-d",
                    allowInput: true,
                    locale: localStorage.getItem('selectedLanguage') === 'es' ? 'es' : 'en',
                    minDate: "today",
                    onChange: (selectedDates, dateStr) => {
                        this.updateGlobalProgress();
                    }
                });
                
                this.config.datepickers.push(flatpickrInstance);
            }
        });
    },

    // Bind all event handlers
    bindEvents: function() {
        // Progress percentage input change events
        document.addEventListener('input', (e) => {
            if (e.target.classList.contains('progress-input')) {
                let value = parseInt(e.target.value) || 0;
                if (value < 0) {
                    value = 0;
                    e.target.value = 0;
                }
                if (value > 100) {
                    value = 100;
                    e.target.value = 100;
                }
                
                this.updateGlobalProgress();
                this.updateChart();
            }
        });

        // Save progress button
        const saveBtn = document.getElementById('saveProgressBtn');
        if (saveBtn) {
            saveBtn.addEventListener('click', (e) => {
                e.preventDefault();
                this.saveProgress();
            });
        }

        // Window resize handler for chart responsiveness
        window.addEventListener('resize', this.debounce(() => {
            if (this.config.chart) {
                this.config.chart.resize();
            }
        }, 300));
    },

    // Update global progress percentage
    updateGlobalProgress: function() {
        const progressInputs = document.querySelectorAll('.progress-input');
        let total = 0;
        let count = 0;

        progressInputs.forEach(input => {
            const value = parseInt(input.value) || 0;
            total += value;
            count++;
        });

        const globalProgress = count > 0 ? Math.round(total / count) : 0;
        
        // Update stats card
        const globalProgressStat = document.getElementById('globalProgressStat');
        if (globalProgressStat) {
            globalProgressStat.textContent = globalProgress + '%';
        }
    },

    // Save progress to server
    saveProgress: async function(silent = false) {
        if (this.config.isLoading) return;

        this.config.isLoading = true;
        const btn = document.getElementById('saveProgressBtn');
        const originalText = btn.innerHTML;
        
        if (!silent) {
            btn.disabled = true;
            btn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Guardando...';
        }

        // Collect all progress data from the table
        const categories = [];
        
        document.querySelectorAll('#progressTableBody tr').forEach(row => {
            const categoryId = parseInt(row.dataset.categoryId);
            
            const meta = row.querySelector('.goal-input')?.value || '';
            const porcentajeProgreso = parseInt(row.querySelector('.progress-input')?.value) || 0;
            const siguienteAccion = row.querySelector('.action-input')?.value || '';
            const fechaLimiteValue = row.querySelector('.deadline-input')?.value;
            
            // Convert date to ISO format for PostgreSQL
            let fechaLimite = null;
            if (fechaLimiteValue && fechaLimiteValue.trim() !== '') {
                const dateObj = new Date(fechaLimiteValue + 'T00:00:00');
                fechaLimite = dateObj.toISOString();
            }
            
            categories.push({
                CategoriaProgresoId: categoryId,
                Meta: meta,
                PorcentajeProgreso: porcentajeProgreso,
                SiguienteAccion: siguienteAccion,
                FechaLimite: fechaLimite
            });
        });

        const progressData = {
            Categories: categories
        };

        // Get CSRF token
        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        
        try {
            const response = await fetch(`${this.config.baseUrl}/SaveProgress`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': token
                },
                body: JSON.stringify(progressData)
            });

            const result = await response.json();

            if (result.success) {
                if (!silent) {
                    this.showMessage('success', result.message, result);
                }
                // Update statistics if provided
                if (result.updatedStats) {
                    this.updateStatistics(result.updatedStats);
                }
            } else {
                this.showMessage('error', result.message || 'Error saving progress');
            }
        } catch (error) {
            if (!silent) {
                this.showMessage('error', 'Error al guardar el progreso. Por favor intente nuevamente.');
            }
        } finally {
            this.config.isLoading = false;
            if (!silent) {
                btn.disabled = false;
                btn.innerHTML = originalText;
            }
        }
    },

    // Show success/error message
    showMessage: function(type, message, data = null) {
        const className = type === 'success' ? 'success-message' : 'error-message';
        const icon = type === 'success' ? 'fas fa-check-circle' : 'fas fa-exclamation-triangle';
        
        let extraInfo = '';
        if (type === 'success' && data && data.updatedStats) {
            extraInfo = `
                <div style="margin-top: 0.5rem; font-size: 0.9rem;">
                    Progreso global: ${Math.round(data.updatedStats.progresoGlobal)}% | 
                    Metas completadas: ${data.updatedStats.metasCompletadas}
                </div>
            `;
        }

        const messageHtml = `
            <div class="${className}">
                <i class="${icon} me-2"></i>${message}${extraInfo}
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

    // Utility functions
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

    // Show loading state
    showLoading: function(show) {
        const loading = document.getElementById('chartLoading');
        if (loading) {
            if (show) {
                loading.classList.remove('d-none');
            } else {
                loading.classList.add('d-none');
            }
        }
    },

    // Show error message
    showError: function(message) {
        console.error('Modern Wheel of Progress Error:', message);
        const tbody = document.getElementById('progressTableBody');
        if (tbody) {
            tbody.innerHTML = `
                <tr>
                    <td colspan="6" class="text-center text-danger py-4">
                        <i class="fas fa-exclamation-triangle fa-2x"></i>
                        <p class="mt-2">${message}</p>
                    </td>
                </tr>
            `;
        }
    }
};

// Initialize when document is ready
$(document).ready(function() {
    WheelOfProgress.init();
});