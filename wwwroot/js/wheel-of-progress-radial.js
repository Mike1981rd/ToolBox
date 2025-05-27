// Modern Wheel of Progress with ApexCharts Radial Bar Implementation
console.log('Loading wheel-of-progress-radial.js...');

const WheelOfProgress = {
    config: {
        chart: null,
        baseUrl: '/WheelOfProgress',
        currentData: [],
        isLoading: false,
        datepickers: [],
        currentProgress: [] // Store current progress values for legend
    },

    // Initialize the module
    init: function() {
        console.log('Initializing Modern Wheel of Progress with Radial Bars...');
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
                this.initializeRadialChart();
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

    // Initialize modern ApexCharts radial bar visualization
    initializeRadialChart: function() {
        const areas = this.config.currentData.areas || [];
        
        // Calculate average progress for each area
        const chartData = [];
        const labels = [];
        const colors = [];
        
        areas.forEach(area => {
            const categorias = area.categorias || [];
            if (categorias.length === 0) {
                chartData.push(0);
            } else {
                const totalProgress = categorias.reduce((sum, cat) => sum + (cat.porcentajeProgreso || 0), 0);
                chartData.push(Math.round(totalProgress / categorias.length));
            }
            labels.push(area.nombre);
            colors.push(area.iconColor);
        });

        // Store current progress for legend updates
        this.config.currentProgress = chartData;

        // ApexCharts options for modern radial bar chart
        const options = {
            series: chartData,
            chart: {
                height: 450,
                type: 'radialBar',
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
                }
            },
            plotOptions: {
                radialBar: {
                    size: undefined,
                    inverseOrder: false,
                    startAngle: 0,
                    endAngle: 360,
                    offsetX: 0,
                    offsetY: 0,
                    hollow: {
                        margin: 5,
                        size: '25%',
                        background: 'transparent',
                        image: undefined,
                        imageWidth: 150,
                        imageHeight: 150,
                        imageOffsetX: 0,
                        imageOffsetY: 0,
                        imageClipped: true,
                        position: 'front',
                        dropShadow: {
                            enabled: false,
                            top: 0,
                            left: 0,
                            blur: 3,
                            opacity: 0.5
                        }
                    },
                    track: {
                        show: true,
                        startAngle: undefined,
                        endAngle: undefined,
                        background: '#f2f2f2',
                        strokeWidth: '97%',
                        opacity: 1,
                        margin: 5,
                        dropShadow: {
                            enabled: false,
                            top: 0,
                            left: 0,
                            blur: 3,
                            opacity: 0.5
                        }
                    },
                    dataLabels: {
                        show: true,
                        name: {
                            show: true,
                            fontSize: '16px',
                            fontFamily: undefined,
                            fontWeight: 600,
                            color: undefined,
                            offsetY: -10
                        },
                        value: {
                            show: true,
                            fontSize: '20px',
                            fontFamily: undefined,
                            fontWeight: 700,
                            color: undefined,
                            offsetY: 5,
                            formatter: function (val) {
                                return parseInt(val) + '%'
                            }
                        },
                        total: {
                            show: true,
                            showAlways: false,
                            label: 'Progreso Global',
                            fontSize: '22px',
                            fontWeight: 600,
                            color: '#373d3f',
                            formatter: function (w) {
                                const totalProgress = w.globals.seriesTotals.reduce((a, b) => a + b, 0);
                                const avgProgress = Math.round(totalProgress / w.globals.series.length);
                                return avgProgress + '%';
                            }
                        }
                    }
                }
            },
            fill: {
                type: 'gradient',
                gradient: {
                    shade: 'dark',
                    type: 'horizontal',
                    shadeIntensity: 0.5,
                    gradientToColors: colors,
                    inverseColors: true,
                    opacityFrom: 1,
                    opacityTo: 1,
                    stops: [0, 100]
                }
            },
            colors: colors,
            labels: labels,
            legend: {
                show: true,
                floating: false,
                fontSize: '14px',
                position: 'bottom',
                offsetX: 0,
                offsetY: 10,
                labels: {
                    useSeriesColors: false
                },
                markers: {
                    size: 12,
                    strokeWidth: 0,
                    fillColors: colors,
                    strokeColor: '#fff',
                    radius: 12,
                    customHTML: undefined,
                    onClick: undefined,
                    offsetX: 0,
                    offsetY: 0
                },
                formatter: function(seriesName, opts) {
                    const value = opts.w.globals.series[opts.seriesIndex];
                    return seriesName + ': ' + value + '%';
                },
                itemMargin: {
                    horizontal: 15,
                    vertical: 8
                },
                onItemClick: {
                    toggleDataSeries: true
                },
                onItemHover: {
                    highlightDataSeries: true
                }
            },
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        height: 350
                    },
                    legend: {
                        fontSize: '12px',
                        itemMargin: {
                            horizontal: 8,
                            vertical: 5
                        }
                    }
                }
            }]
        };

        // Destroy existing chart if any
        if (this.config.chart) {
            this.config.chart.destroy();
        }

        // Create new chart
        this.config.chart = new ApexCharts(document.querySelector("#progressWheelChart"), options);
        this.config.chart.render();
    },

    // Render progress table
    renderProgressTable: function(areas) {
        const tbody = document.getElementById('progressTableBody');
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
        $('#globalProgressStat').text(Math.round(stats.progresoGlobal) + '%');
        $('#totalMetasStat').text(stats.totalMetas);
        $('#metasCompletadasStat').text(stats.metasCompletadas);
        
        if (stats.ultimaActualizacion) {
            const fecha = new Date(stats.ultimaActualizacion);
            $('#ultimaActualizacionStat').text(fecha.toLocaleDateString());
        } else {
            $('#ultimaActualizacionStat').text('-');
        }
    },

    // Update chart with new data
    updateChartData: function() {
        if (!this.config.chart) return;

        const areas = this.config.currentData.areas || [];
        const newData = [];

        // Calculate progress for each area based on current input values
        areas.forEach(area => {
            const categorias = area.categorias || [];
            let totalProgress = 0;
            let categoryCount = 0;

            categorias.forEach(categoria => {
                const input = document.querySelector(`.progress-input[data-category-id="${categoria.id}"]`);
                const value = input ? parseInt(input.value) || 0 : categoria.porcentajeProgreso || 0;
                totalProgress += value;
                categoryCount++;
            });

            const avgProgress = categoryCount > 0 ? Math.round(totalProgress / categoryCount) : 0;
            newData.push(avgProgress);
        });

        // Store current progress for legend updates
        this.config.currentProgress = newData;

        // Update ApexCharts
        this.config.chart.updateSeries(newData);
        
        // Update global progress stat
        const globalProgress = newData.length > 0 ? 
            Math.round(newData.reduce((a, b) => a + b, 0) / newData.length) : 0;
        $('#globalProgressStat').text(globalProgress + '%');
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
                        this.updateChartData();
                    }
                });
                
                this.config.datepickers.push(flatpickrInstance);
            }
        });
    },

    // Bind all event handlers
    bindEvents: function() {
        // Progress percentage input change events
        $(document).on('input change', '.progress-input', (e) => {
            const value = parseInt($(e.target).val()) || 0;
            if (value < 0) $(e.target).val(0);
            if (value > 100) $(e.target).val(100);
            
            this.updateChartData();
        });

        // Save progress button
        $('#saveProgressBtn').on('click', (e) => {
            e.preventDefault();
            this.saveProgress();
        });

        // Window resize handler for chart responsiveness
        $(window).on('resize', this.debounce(() => {
            if (this.config.chart) {
                this.config.chart.updateOptions({}, true, true);
            }
        }, 300));
    },

    // Save progress to server
    saveProgress: function(silent = false) {
        if (this.config.isLoading) return;

        this.config.isLoading = true;
        const $btn = $('#saveProgressBtn');
        const originalText = $btn.html();
        
        if (!silent) {
            $btn.prop('disabled', true).html('<i class="fas fa-spinner fa-spin me-2"></i>Guardando...');
        }

        // Collect all progress data from the table
        const categories = [];
        
        $('#progressTableBody tr').each(function() {
            const $row = $(this);
            const categoryId = parseInt($row.data('category-id'));
            
            const meta = $row.find('.goal-input').val() || '';
            const porcentajeProgreso = parseInt($row.find('.progress-input').val()) || 0;
            const siguienteAccion = $row.find('.action-input').val() || '';
            const fechaLimiteValue = $row.find('.deadline-input').val();
            
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
        const token = $('input[name="__RequestVerificationToken"]').val();
        
        $.ajax({
            url: `${this.config.baseUrl}/SaveProgress`,
            method: 'POST',
            contentType: 'application/json',
            headers: {
                'RequestVerificationToken': token
            },
            data: JSON.stringify(progressData),
            success: (response) => {
                if (response.success) {
                    if (!silent) {
                        this.showMessage('success', response.message, response);
                    }
                    // Update statistics if provided
                    if (response.updatedStats) {
                        this.updateStatistics(response.updatedStats);
                    }
                } else {
                    this.showMessage('error', response.message || 'Error saving progress');
                }
            },
            error: (xhr) => {
                if (!silent && xhr.status >= 400) {
                    this.showMessage('error', 'Error al guardar el progreso. Por favor intente nuevamente.');
                }
            },
            complete: () => {
                this.config.isLoading = false;
                if (!silent) {
                    $btn.prop('disabled', false).html(originalText);
                }
            }
        });
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
        
        $('#saveMessage').html(messageHtml);
        
        // Auto remove after 4 seconds
        setTimeout(() => {
            $('#saveMessage').fadeOut(300, function() {
                $(this).empty().show();
            });
        }, 4000);
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
        if (show) {
            $('#chartLoading').removeClass('d-none');
        } else {
            $('#chartLoading').addClass('d-none');
        }
    },

    // Show error message
    showError: function(message) {
        console.error('Wheel of Progress Error:', message);
        const tbody = $('#progressTableBody');
        tbody.html(`
            <tr>
                <td colspan="6" class="text-center text-danger py-4">
                    <i class="fas fa-exclamation-triangle fa-2x"></i>
                    <p class="mt-2">${message}</p>
                </td>
            </tr>
        `);
    }
};

// Initialize when document is ready
$(document).ready(function() {
    WheelOfProgress.init();
});