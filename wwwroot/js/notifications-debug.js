// Debug script to force Vuexy styles
document.addEventListener('DOMContentLoaded', function() {
    console.log('DEBUG: Force Vuexy styles loaded');
    
    setTimeout(() => {
        // Force styles
        const style = document.createElement('style');
        style.textContent = `
            .notification-dropdown-vuexy {
                min-width: 380px !important;
                max-width: 400px !important;
                border: 1px solid #e9ecef !important;
                border-radius: 0.375rem !important;
                box-shadow: 0 0.25rem 1rem rgba(165, 163, 174, 0.3) !important;
                background: white !important;
            }
            
            .notification-header-vuexy {
                background: white !important;
                border-bottom: 1px solid #f1f1f1 !important;
                border-radius: 0.375rem 0.375rem 0 0 !important;
            }
            
            .notification-title-vuexy {
                font-size: 15px !important;
                font-weight: 500 !important;
                color: #5e5873 !important;
                margin: 0 !important;
                margin-right: 12px !important;
            }
            
            .notification-count-badge {
                background: #7367f0 !important;
                color: white !important;
                font-size: 11px !important;
                font-weight: 500 !important;
                padding: 2px 6px !important;
                border-radius: 10px !important;
                line-height: 1 !important;
            }
            
            .notification-item-vuexy {
                border-bottom: 1px solid #f1f1f1 !important;
                background: white !important;
                cursor: pointer !important;
            }
            
            .notification-item-vuexy:hover {
                background-color: #f8f8f8 !important;
            }
            
            .notification-item-content-vuexy {
                padding: 12px 16px !important;
            }
            
            .notification-icon-vuexy {
                width: 32px !important;
                height: 32px !important;
                border-radius: 50% !important;
                display: flex !important;
                align-items: center !important;
                justify-content: center !important;
                font-size: 14px !important;
                color: white !important;
            }
            
            .notification-icon-vuexy.session {
                background: #ff9f43 !important;
            }
            
            .notification-icon-vuexy.message {
                background: #7367f0 !important;
            }
            
            .notification-icon-vuexy.order {
                background: #28c76f !important;
            }
            
            .notification-icon-vuexy.approved {
                background: #00cfe8 !important;
            }
            
            .notification-title-text {
                font-size: 14px !important;
                font-weight: 500 !important;
                color: #5e5873 !important;
                margin-bottom: 2px !important;
                line-height: 1.2 !important;
            }
            
            .notification-subtitle-text {
                font-size: 12px !important;
                color: #b4b7bd !important;
                margin-bottom: 4px !important;
                line-height: 1.3 !important;
            }
            
            .notification-time-vuexy {
                font-size: 11px !important;
                color: #b4b7bd !important;
                font-weight: 400 !important;
            }
            
            .notification-dot-vuexy {
                width: 8px !important;
                height: 8px !important;
                background: #7367f0 !important;
                border: none !important;
                border-radius: 50% !important;
                flex-shrink: 0 !important;
                margin-bottom: 4px !important;
            }
            
            .notification-actions-vuexy {
                display: flex !important;
                flex-direction: column !important;
                align-items: center !important;
                gap: 8px !important;
                opacity: 0.7 !important;
                transition: opacity 0.2s ease !important;
            }
            
            .notification-item-vuexy:hover .notification-actions-vuexy {
                opacity: 1 !important;
            }
            
            .notification-dismiss {
                background: none !important;
                border: none !important;
                color: #b4b7bd !important;
                font-size: 12px !important;
                padding: 4px !important;
                border-radius: 50% !important;
                width: 20px !important;
                height: 20px !important;
                display: flex !important;
                align-items: center !important;
                justify-content: center !important;
                cursor: pointer !important;
                transition: all 0.2s ease !important;
            }
            
            .notification-dismiss:hover {
                background: #f1f1f1 !important;
                color: #dc3545 !important;
                transform: scale(1.1) !important;
            }
            
            .notification-view-all-vuexy {
                background: #7367f0 !important;
                color: white !important;
                font-size: 13px !important;
                font-weight: 500 !important;
                text-decoration: none !important;
                padding: 10px 16px !important;
                border-radius: 0.25rem !important;
                margin: 0 !important;
            }
            
            .notification-view-all-vuexy:hover {
                background: #5e5ce6 !important;
                color: white !important;
                text-decoration: none !important;
            }
        `;
        document.head.appendChild(style);
        console.log('DEBUG: Forced styles applied');
        
        // Don't force title text - let translations work
        console.log('DEBUG: Leaving title text for translations');
        
        // Don't force count - let it be dynamic
        console.log('DEBUG: Leaving count dynamic');
        
        // Don't force view all text - let translations work
        console.log('DEBUG: Leaving view all text for translations');
        
    }, 1000);
});