/**
 * Image URL Fixer Script
 * 
 * Este script corrige URLs malformadas de imágenes que pueden causar errores 
 * 'net::ERR_NAME_NOT_RESOLVED' en la consola.
 */

document.addEventListener('DOMContentLoaded', function() {
    // Verificar todas las imágenes en el documento
    checkAndFixImageUrls();
    
    // Observar cambios en el DOM para corregir nuevas imágenes
    observeDOMChanges();
});

/**
 * Verifica y corrige todas las URLs de imágenes en el documento
 */
function checkAndFixImageUrls() {
    // Obtener todas las imágenes del documento
    const images = document.querySelectorAll('img');
    
    images.forEach(function(img) {
        fixImageUrl(img);
        
        // También escuchar por errores en la carga de imágenes
        img.addEventListener('error', function(e) {
            console.warn('Error al cargar imagen:', img.src);
            handleImageError(img);
        });
    });
}

/**
 * Corrige la URL de una imagen si está malformada
 * @param {HTMLImageElement} img - El elemento imagen a verificar
 */
function fixImageUrl(img) {
    const src = img.getAttribute('src');
    
    // Si ya es una imagen base64 o no tiene src, no hacemos nada
    if (!src || src.startsWith('data:image')) return;
    
    // Si es una URL de placeholder válida, no hacemos nada
    if (src.startsWith('https://via.placeholder.com/')) return;
    
    // Comprobar si hay errores en via.placeholder.com
    if (src.includes('via.placeholder.com') && src.includes('FFFFFFF')) {
        // Corregir error de 7 F a 6 F
        const fixedUrl = src.replace('FFFFFFF', 'FFFFFF');
        console.log('URL corregida de 7F a 6F:', fixedUrl);
        img.src = fixedUrl;
        return;
    }
    
    // Si es un servicio de placeholder sin https, añadir https
    if (src.startsWith('//via.placeholder.com') || src.startsWith('via.placeholder.com')) {
        img.src = 'https:' + (src.startsWith('//') ? src : '//' + src);
        return;
    }
    
    // Si detectamos un patrón de color sin URL completa, usar fallback
    if (/^[0-9A-F]{6}\/?/.test(src) || 
        src.match(/^FFFFFF/) ||
        src.match(/^8C98A4/)) {
        
        // No intentar reconstruir URLs - usar directamente la imagen fallback
        console.warn('URL malformada sustituida por fallback:', src);
        handleImageError(img);
    }
}

/**
 * Intenta reconstruir una URL válida para un servicio de placeholder
 * @param {string} src - La URL malformada
 * @returns {string} - URL corregida
 */
function getFixedPlaceholderUrl(src) {
    // Intentar extraer tamaño y colores
    let size = '100x100';
    let bgColor = 'DFE3E7';
    let textColor = '8C98A4';
    let text = 'Avatar';
    
    // Buscar tamaño y colores en la URL malformada
    const sizeMatch = src.match(/(\d+)x(\d+)/);
    if (sizeMatch) {
        size = sizeMatch[0];
    }
    
    // Buscar colores hexadecimales
    const hexColors = src.match(/[0-9A-F]{6}/g);
    if (hexColors && hexColors.length >= 1) {
        bgColor = hexColors[0];
        if (hexColors.length >= 2) {
            textColor = hexColors[1];
        }
    }
    
    // Buscar texto
    const textMatch = src.match(/text=([^&]+)/);
    if (textMatch) {
        text = textMatch[1];
    }
    
    // Construir URL correcta
    return `https://via.placeholder.com/${size}/${bgColor}/${textColor}?text=${text}`;
}

/**
 * Maneja el error de carga de una imagen
 * @param {HTMLImageElement} img - El elemento imagen con error
 */
function handleImageError(img) {
    // IMPORTANTE: Detenemos la recursión si ya estamos usando URL base64
    if (img.src.startsWith('data:image')) {
        return; // Ya estamos usando un fallback en base64
    }
    
    // Usamos URLs en base64 para evitar peticiones adicionales al servidor
    // Avatar por defecto en base64 (pequeño icono de usuario)
    const defaultAvatarBase64 = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABmJLR0QA/wD/AP+gvaeTAAACMklEQVRYhe2WTUhUURTHf+e+8dNJrXRRRrRoEbRzEVGbItwUrYIgaBG0CLIgiGpR0CJo0apNQauINlFhUYuIFrWLEtwVgRItRJI+dCa90Zm5ty08Z3yOOuObGYK8P9zFfff8z/+ce+65T5Q1V9vXhBAOe7Q5LLKzOBj9nY5PpQISjn8duHWlf2IiNjwCDYUpVVmYU2zffO6WTUv3hjODlSNTb18ecCLRypICRKt7yjsW7LntYnJFYPQxTZ1X4umhlz1T/eBpRxHQ2hFJZeLHVLWmIN9UtLJPRA5mBnvvOXZxswR0b+lcunfO3t6cWy0gZ6P5Js+S5k7V8L6ApIFJkBj/pVlkXQi6NQ+5KTRNgPsrAWlnCTCRTb6xvH1BrfP48czcf2QgfwE2hAQHw+RaARWQdKxhLKBLYIzJxRJgBZQAK6AEFI05XVvXl6B7mL0CYnX3BtS8AFAAb3T/yHh8eEB1ZQGqvTGKvlFdAisnQIHIqt5lM2pXbI+mhlM9QXRsgLuQWDN8OgYSQs2WQG6uYJPTxbFntxsBM+4CtH0iHD/7JxEoQ/sDUNQoAAvbRXSr63n9PzAvQKHOGXPQDxhZ9/tHAP2EWgFlDwUyEN4r5LHlsdyT/YlQLAMVVbW72+f2X3PFXRGKiJ80ZafDamwKVsQP2gsjD1cU7epI9y6tDoKKFy9Fl8f3nxeR2ryFVHtF3FPp/sdnQg/jwZrOeDBhyKaE4ZjjyqHGwB1u/h9/AMuLr0CnGFazAAAAAElFTkSuQmCC';
    
    // Imagen por defecto en base64 (pequeño icono de imagen)
    const defaultImageBase64 = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABmJLR0QA/wD/AP+gvaeTAAAA+UlEQVRYhe2XvQ3CMBCFP5dUTMAGsAElLRNQMgQwAh0DsAUbZALYgIb8NIgCmwgcQxAipPekK3z2vbOdfziitcgAa2AEJFjzBnKgSLRfAVODUFfmwMI10IPRPhGJ+zpY4z9yWDEF1sBzaLHvDkhp9iYHZvWg/L39DVgdOI59nM6A5uI2xOmACuieOQ1RBxSw9CXEWUC/LyEXAcDElxB3AZ/Y8Cek0ZGy1xKitXjvvUGlrLUQnf+1hOg8x0JC9BWHhrRdGxqI2VBD/C1uaiGmRl6Eh1Sh9zXx7S4hXE3Ed1TSBfQ4fdA4C1gBj2uI0C5o6+YhXdAd+AJkpFt9X/vkJwAAAABJRU5ErkJggg==';
    
    // Verificar si la imagen tiene clases específicas
    if (img.classList.contains('avatar') || img.classList.contains('rounded-circle')) {
        img.src = defaultAvatarBase64;
    } else {
        img.src = defaultImageBase64;
    }
    
    // Añadir un tooltip para indicar que es una imagen de fallback
    img.title = 'Imagen no disponible';
    img.alt = 'Imagen no disponible';
}

/**
 * Observa cambios en el DOM para corregir nuevas imágenes
 */
function observeDOMChanges() {
    // Configurar un MutationObserver para detectar nuevas imágenes
    const observer = new MutationObserver(function(mutations) {
        mutations.forEach(function(mutation) {
            // Verificar si se han añadido nuevos nodos
            if (mutation.addedNodes && mutation.addedNodes.length > 0) {
                // Procesar cada nodo añadido
                mutation.addedNodes.forEach(function(node) {
                    // Verificar si el nodo es un elemento
                    if (node.nodeType === Node.ELEMENT_NODE) {
                        // Si es una imagen, corregir su URL
                        if (node.tagName === 'IMG') {
                            fixImageUrl(node);
                            
                            node.addEventListener('error', function(e) {
                                handleImageError(node);
                            });
                        }
                        
                        // Si contiene imágenes, corregir esas URLs también
                        const images = node.querySelectorAll('img');
                        if (images.length > 0) {
                            images.forEach(fixImageUrl);
                            
                            images.forEach(function(img) {
                                img.addEventListener('error', function(e) {
                                    handleImageError(img);
                                });
                            });
                        }
                    }
                });
            }
        });
    });
    
    // Iniciar la observación del documento
    observer.observe(document.body, {
        childList: true,
        subtree: true
    });
}