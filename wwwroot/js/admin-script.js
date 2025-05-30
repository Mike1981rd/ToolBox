/**
 * Admin Dashboard JavaScript
 * Handles sidebar toggling, theme switching, and language selection
 */

/**
 * Translations for the application
 */
const translations = {
    en: {
        dashboard: "Dashboard",
        roles_menu: "Roles",
        users_menu: "Users",
        users_list_title: "Users List",
        // Tarjetas de estadísticas
        card_session_title: "Session",
        card_session_total_users: "Total Users",
        card_active_users_title: "Active Users",
        card_active_users_last_week: "Last week analytics",
        // Sección de Filtros
        filters_title: "Filters",
        filter_select_role: "Select Role",
        filter_select_plan: "Select Plan",
        filter_select_status: "Select Status",
        // Controles de Tabla
        search_user_placeholder: "Search User",
        export_button: "Export",
        export_to_excel: "Export to Excel",
        export_to_pdf: "Export to PDF",
        export_to_csv: "Export to CSV",
        add_new_record_button: "Add New Record",
        // Encabezados de Tabla
        th_user_list_user: "USER",
        th_user_list_role: "ROLE",
        th_user_list_plan: "PLAN",
        th_user_list_billing: "BILLING",
        th_user_list_status: "STATUS",
        th_user_list_actions: "ACTIONS",
        // Paginación
        showing_entries: "Showing {start} to {end} of {total} entries",
        // Add User Offcanvas
        add_user_title: "Add User",
        form_user_avatar: "User Avatar",
        form_upload_new: "Upload New",
        form_reset: "Reset",
        form_full_name: "Full Name",
        form_username: "Username",
        form_email: "Email",
        form_role: "Role",
        form_plan: "Plan",
        form_select_role: "Select Role",
        form_select_plan: "Select Plan",
        form_feedback_name: "Please enter a valid name",
        form_feedback_username: "Please enter a valid username",
        form_feedback_email: "Please enter a valid email",
        form_feedback_role: "Please select a role",
        form_feedback_plan: "Please select a plan",
        form_section_payment: "Payment Details",
        form_billing_method: "Billing Method",
        form_status: "Status",
        form_status_active: "Active",
        form_tax_id: "Tax ID",
        form_contact: "Contact",
        form_language: "Language",
        form_country: "Country",
        form_section_security: "Security",
        form_password: "Password",
        form_confirm_password: "Confirm Password",
        form_feedback_password: "Please provide a password",
        form_feedback_password_match: "Passwords must match",
        form_2fa: "Enable Two-Factor Authentication",
        form_button_cancel: "Cancel",
        form_button_submit: "Add User",
        mobile_filters_button: "Filters",
        mobile_filters_title: "Filter Options",
        apply_filters_button: "Apply Filters",
        payment_auto_debit: "Auto Debit",
        payment_manual_paypal: "Manual - Paypal",
        payment_manual_cc: "Manual - Credit Card",
        payment_manual_bank: "Manual - Bank Transfer",
        language_english: "English",
        language_spanish: "Spanish",
        country_usa: "United States",
        country_uk: "United Kingdom",
        country_spain: "Spain",
        country_france: "France",
        country_germany: "Germany",
        country_other: "Other",
        plan_basic: "Basic",
        plan_team: "Team",
        plan_company: "Company",
        plan_enterprise: "Enterprise",
        // Nuevas traducciones para el offcanvas mejorado
        offcanvas_user_avatar: "User Avatar",
        offcanvas_upload_new_photo: "Upload new photo",
        offcanvas_reset_avatar: "Reset",
        offcanvas_avatar_allowed_formats: "Allowed JPG, GIF or PNG. Max size of 800K",
        offcanvas_username: "Username",
        offcanvas_password: "Password",
        offcanvas_company: "Company",
        select_country_option: "Select Country",
        all_option: "All",
        roles_list_title: "Roles List",
        roles_list_description: "A role provided access to predefined menus and features so that depending on assigned role an administrator can have access to what user needs.",
        total_users_with_roles: "Total users with their roles",
        add_new_role_card_title: "Add New Role",
        add_new_role_card_text: "Add new role, if it doesn't exist.",
        total_users_role_prefix: "Total users",
        edit_role_link: "Edit Role",
        delete_role_title: "Delete Role",
        delete_role: "Delete Role",
        delete_role_confirmation_title: "Delete Role Confirmation",
        delete_role_warning: "Are you sure you want to delete this role? This action cannot be undone.",
        delete_role_info: "All permissions associated with this role will be removed. Users will need to be assigned to a different role.",
        delete_confirm_button: "Delete Role",
        permissions_count: "Permissions",
        permissions_assigned: "permissions assigned",
        no_permissions_assigned: "No permissions assigned",
        status: "Status",
        search_placeholder: "Search...",
        export_button: "Export",
        add_new_user_button: "Add New User",
        status_active: "Active",
        status_pending: "Pending",
        status_inactive: "Inactive",
        modal_add_new_role_title: "Add New Role",
        modal_set_role_permissions: "Set role permissions",
        modal_role_name_label: "Role Name",
        modal_enter_role_name_placeholder: "Enter a role name",
        modal_role_permissions_section_title: "Role Permissions",
        perm_admin_access: "Administrator Access",
        tooltip_admin_access: "Allows full access to the system",
        select_all: "Select All",
        perm_read: "Read",
        perm_write: "Write",
        perm_create: "Create",
        perm_cat_user_management: "User Management",
        perm_cat_content_management: "Content Management",
        perm_cat_disputes_management: "Disputes Management",
        perm_cat_database_management: "Database Management",
        perm_cat_financial_management: "Financial Management",
        perm_cat_reporting: "Reporting",
        perm_cat_api_control: "API Control",
        perm_cat_repository_management: "Repository Management",
        perm_cat_payroll: "Payroll",
        cancel_button: "Cancel",
        confirm_button: "Confirm",
        confirm_status_change: "Confirm Status Change",
        submit_button: "Submit",
        // Create Role Page translations
        create_new_role: "Create New Role",
        role_information: "Role Information",
        role_name: "Role Name",
        role_name_placeholder: "Enter role name",
        description: "Description",
        optional: "(Optional)",
        description_placeholder: "Enter optional description",
        assigned_dashboard: "Assigned Dashboard",
        select_dashboard: "Select Dashboard",
        admin_dashboard: "Admin Dashboard",
        coach_dashboard: "Coach Dashboard",
        client_dashboard: "Client Dashboard",
        role_permissions: "Role Permissions",
        select_permissions_message: "Select the permissions you want to assign to this role.",
        no_permissions_available: "No permissions available to assign.",
        create_role_button: "Create Role",
        edit_role: "Edit Role",
        edit_role_title: "Edit Role",
        update_role_button: "Update Role",
        role_active_status: "Active Status",
        information: "Information",
        about_roles: "About Roles",
        about_roles_description: "Roles define what actions users can perform in the system. Each role has a specific set of permissions that determine access to different modules and functionalities.",
        available_dashboards: "Available Dashboards",
        admin: "Admin",
        coach: "Coach",
        client: "Client",
        admin_dashboard_description: "Full panel with all functionalities",
        coach_dashboard_description: "Panel for trainers and managers",
        client_dashboard_description: "Panel for end clients",
        permission_types: "Permission Types",
        read: "Read",
        write: "Write",
        create: "Create",
        read_permission_description: "Allows viewing information",
        write_permission_description: "Allows editing existing information",
        create_permission_description: "Allows creating new information",
        th_user: "USER",
        th_role: "ROLE",
        th_plan: "PLAN",
        th_billing: "BILLING",
        th_status: "STATUS",
        th_actions: "ACTIONS",
        areas_of_life: "Areas of Life",
        xray_of_life: "X-Ray of life",
        default_welcome_message: "Default Welcome Message",
        "welcome-message-title": "Default Welcome Message",
        "welcome-message-subtitle": "Configure the welcome message that clients will see when accessing the platform",
        coaches: "Coaches",
        add_user: "Add USER",
        profile: "Profile",
        academy: "Academy",
        academy_submenu_1: "Courses",
        academy_submenu_2: "Materials",
        "sidebar.academia.topics": "Topics",
        "sidebar.academia.videoManagement": "Video Management",
        // Topics Module
        "breadcrumb_topics_list": "Topics List",
        "page_titles.topicsList": "Topics List",
        "buttons.addNewTopic": "Add New Topic",
        "offcanvas_titles.addNewTopic": "Add New Topic",
        "offcanvas_titles.editTopic": "Edit Topic",
        "labels.topicName": "Topic Name",
        "labels.description": "Description",
        "labels.status": "Status",
        "status_options.active": "Active",
        "status_options.inactive": "Inactive",
        "actions.viewEdit": "View/Edit",
        "actions.delete": "Delete",
        "search_topics_placeholder": "Search Topics...",
        "placeholders.topicName": "Enter topic name",
        "placeholders.topicDescription": "Write a brief description...",
        "confirm_messages.deleteTopic": "Are you sure you want to delete this topic?",
        "buttons.save": "Save",
        "buttons.cancel": "Cancel",
        "th_topic_name": "TOPIC NAME",
        "th_topic_status": "STATUS",
        "th_topic_actions": "ACTIONS",
        // Video Management Module
        "breadcrumb_video_management_list": "Video Management List",
        "breadcrumb_add_new_video": "Add New Video",
        "breadcrumb_edit_video": "Edit Video",
        "page_titles.videoManagementList": "Video Management List",
        "page_titles.addNewVideo": "Add New Video",
        "page_titles.editVideo": "Edit Video",
        "buttons.addNewVideo": "Add New Video",
        "buttons.goBack": "Go Back",
        "buttons.saveVideo": "Save Video",
        "buttons.cancel": "Cancel",
        "labels.videoSection": "Video Section",
        "labels.currentVideo": "Current Video",
        "labels.videoUrl": "Video URL",
        "labels.videoSource": "Video Source",
        "labels.uploadFile": "Upload File",
        "labels.videoFile": "Video File",
        "labels.videoPreview": "Video Preview",
        "labels.uploadVideoFile": "Upload Video File",
        "labels.basicInformation": "Basic Information",
        "labels.videoTitle": "Video Title",
        "labels.videoDescription": "Video Description",
        "labels.authorUser": "Author/User",
        "labels.user": "User",
        "labels.topic": "Topic",
        "labels.mediaType": "Media Type",
        "labels.duration": "Duration (mm:ss)",
        "labels.videoSettings": "Video Settings",
        "labels.isFeatured": "Mark as Featured?",
        "labels.featuredToggle": "Featured Video",
        "labels.featuredBadge": "Featured",
        "labels.status": "Status",
        "labels.uploadInfo": "Upload Info",
        "labels.uploadDate": "Upload Date",
        "labels.lastUpdated": "Last Updated",
        "labels.seoSettings": "SEO Settings",
        // Missing Video Management translations
        "featured_yes": "Yes",
        "featured_no": "No",
        "type_upload": "Uploaded",
        "filter_select_featured": "Select Featured",
        "status_published": "Published",
        "status_draft": "Draft",
        "status_archived": "Archived",
        "filter_select_type": "Select Type",
        "labels.metaTitle": "Meta Title",
        "labels.metaDescription": "Meta Description",
        "labels.keywords": "Keywords",
        "media_types.youtube": "YouTube URL",
        "media_types.vimeo": "Vimeo URL",
        "media_types.uploadedFile": "Uploaded File",
        "status_options.published": "Published",
        "status_options.draft": "Draft",
        "status_options.archived": "Archived",
        "search.placeholder.videos": "Search Videos...",
        "placeholders.videoUrl": "https://youtube.com/watch?v=...",
        "placeholders.videoTitle": "Enter video title",
        "placeholders.videoDescription": "Write a detailed description...",
        "placeholders.selectAuthor": "Select Author",
        "placeholders.selectUser": "Select User",
        "placeholders.selectTopic": "Select Topic",
        "placeholders.duration": "15:30",
        "placeholders.metaTitle": "SEO title for search engines",
        "placeholders.metaDescription": "SEO description for search engines",
        "placeholders.keywords": "keyword1, keyword2, keyword3",
        "help.videoUrl": "Enter YouTube or Vimeo URL",
        "help.videoFile": "Or upload a video file (MP4, AVI, MOV)",
        "helpers.videoUrlHelp": "Enter a valid YouTube or Vimeo URL",
        "helpers.videoFileHelp": "Max size: 500MB. Supported formats: MP4, AVI, MOV, WMV",
        "help.featured": "Featured videos appear prominently in listings",
        "th_video_title": "TITLE",
        "th_video_topic": "TOPIC",
        "th_video_author": "AUTHOR",
        "th_video_upload_date": "UPLOAD DATE",
        "th_video_status": "STATUS",
        "th_video_actions": "ACTIONS",
        "confirm_messages.deleteVideo": "Are you sure you want to delete this video?",
        // Website Settings Module
        "sidebar.websiteSettings": "Website Settings",
        "breadcrumb_website_settings": "Website Settings",
        "page_titles.websiteSettings": "Website Settings",
        "sections.generalInfo": "General Site Information",
        "sections.socialMediaLinks": "Social Media Links",
        "sections.siteBranding": "Site Branding", 
        "sections.additionalSettings": "Additional Settings",
        "labels.siteEmail": "Site Email",
        "labels.siteAddress": "Site Address",
        "labels.sitePhone": "Site Phone",
        "labels.footerMessage": "Footer Message",
        "labels.siteName": "Site Name",
        "labels.siteDescription": "Site Description",
        "labels.appLogo": "Application Logo",
        "labels.logoPreview": "Logo Preview",
        "labels.timeZone": "Default Time Zone",
        "labels.defaultLanguage": "Default Language",
        "labels.lastUpdated": "Last updated",
        "labels.by": "by",
        "labels.facebook": "Facebook",
        "labels.twitter": "Twitter",
        "labels.google": "Google",
        "labels.linkedin": "LinkedIn",
        "labels.youtube": "YouTube",
        "labels.instagram": "Instagram",
        "labels.telegram": "Telegram",
        "labels.tiktok": "TikTok",
        "labels.discord": "Discord",
        "labels.reddit": "Reddit",
        "buttons.saveSettings": "Save Settings",
        "buttons.resetLogo": "Reset to Default",
        "buttons.cancel": "Cancel",
        "placeholders.siteEmail": "info@company.com",
        "placeholders.sitePhone": "+1 (555) 123-4567",
        "placeholders.siteAddress": "Enter your business address",
        "placeholders.footerMessage": "Copyright message or footer text",
        "placeholders.siteName": "Your Site Name",
        "placeholders.siteDescription": "Brief description of your website",
        "placeholders.facebookUrl": "https://facebook.com/yourpage",
        "placeholders.twitterUrl": "https://twitter.com/yourpage",
        "placeholders.googleUrl": "https://google.com/yourpage",
        "placeholders.linkedinUrl": "https://linkedin.com/company/yourpage",
        "placeholders.youtubeUrl": "https://youtube.com/c/yourpage",
        "placeholders.instagramUrl": "https://instagram.com/yourpage",
        "placeholders.telegramUrl": "https://t.me/yourpage",
        "placeholders.tiktokUrl": "https://tiktok.com/@yourpage",
        "placeholders.discordUrl": "https://discord.gg/yourserver",
        "placeholders.redditUrl": "https://reddit.com/r/yoursubreddit",
        "help.socialMediaLinks": "Enter the complete URLs for your social media profiles. Leave empty if you don't have an account on that platform.",
        "help.logoUpload": "Upload a PNG logo for best quality and transparency. Recommended size: 200x60px. Max file size: 5MB.",
        "help.logoPreview": "This is how your logo will appear in the sidebar",
        "messages.settingsSaved": "Settings saved successfully",
        "messages.logoUploaded": "Logo uploaded successfully",
        "messages.logoReset": "Logo reset to default",
        "messages.invalidFileType": "Invalid file type. Please upload PNG, JPG, JPEG, SVG, or GIF files.",
        "messages.fileTooLarge": "File size too large. Maximum allowed size is 5MB.",
        toolbox_academy: "Toolbox academy",
        website_setting: "Website Setting",
        email_contents: "Email Contents",
        analytics: "Analytics",
        settings: "Settings",
        notifications: "Notifications",
        logout: "Logout",
        select_language: "Select Language",
        welcome_message_admin: 'Dear Carlos Checo <span data-translate-key="welcome">Welcome</span>. You are logged in as <span data-translate-key="admin_role">Admin</span>',
        welcome: "Welcome",
        admin_role: "Admin",
        total_coach: "Total Coaches",
        active_coaches: "Active Coaches",
        activity_rate: "Activity Rate",
        // Customers module
        customers_menu: "Customers",
        all_customers_title: "All Customers",
        search_order_placeholder: "Search Customer",
        add_customer_button: "Add Customer",
        add_new_customer_button: "Add New Customer",
        breadcrumb_customers_list: "Customers",
        customers_filters_title: "Customer Filters",
        notification_title: "Notification",
        status_all: "All",
        // Customer table headers
        th_customer_list_customer: "CUSTOMER",
        th_customer_list_customer_id: "CUSTOMER ID",
        th_customer_list_country: "COUNTRY",
        th_customer_list_order: "ORDER",
        th_customer_list_total_spent: "TOTAL SPENT",
        th_customer_list_id: "CUSTOMER ID",
        th_customer_list_phone: "PHONE",
        th_customer_list_status: "STATUS",
        th_customer_list_actions: "ACTIONS",
        // Customer details
        customer_details_title_prefix: "Customer ID #",
        customer_details_joined_on: "Joined on",
        button_edit_details: "Edit Details",
        button_delete_customer: "Delete Customer",
        button_deactivate_customer: "Deactivate Customer",
        card_upgrade_to_premium_title: "Upgrade to premium",
        card_upgrade_to_premium_text: "Upgrade customer to premium membership to access pro features.",
        button_upgrade_to_premium: "Upgrade to premium",
        // Tabs
        tab_overview: "Overview",
        tab_security: "Security",
        tab_address_billing: "Address & Billing",
        tab_notifications: "Notifications",
        // Overview tab content
        overview_orders_title: "Orders",
        overview_spent_title: "Spent",
        overview_details_section_title: "Details",
        overview_username_label: "Username:",
        overview_email_label: "Email:",
        overview_status_label: "Status:",
        overview_contact_label: "Contact:",
        overview_country_label: "Country:",
        overview_account_balance_title: "Account Balance",
        overview_credit_left: "Credit Left",
        overview_account_balance_next_purchase: "Account balance for next purchase",
        overview_loyalty_program_title: "Loyalty Program",
        overview_platinum_member: "Platinum member",
        overview_points_to_next_tier: "points to next tier",
        overview_wishlist_title: "Wishlist",
        overview_items_in_wishlist: "items in wishlist",
        overview_wishlist_receive_notification: "Receive notification when items go on sale",
        overview_coupons_title: "Coupons",
        overview_coupons_you_win: "Coupons you win",
        overview_coupons_use_on_next_purchase: "Use coupon on next purchase",
        overview_orders_placed_title: "Orders placed",
        overview_search_order_placeholder: "Search Order",
        // Customer offcanvas form
        offcanvas_add_customer_title: "Add New Customer",
        offcanvas_edit_customer_title: "Edit Customer",
        offcanvas_customer_avatar: "Customer Avatar",
        offcanvas_customer_type: "Customer Type",
        customer_type_individual: "Individual",
        customer_type_company: "Company",
        offcanvas_company_name_optional: "Company Name (Optional)",
        offcanvas_full_name: "Full Name",
        offcanvas_username: "Username",
        offcanvas_email: "Email",
        offcanvas_password: "Password",
        offcanvas_confirm_password: "Confirm Password",
        password_edit_note: "Leave blank to keep current password.",
        offcanvas_contact: "Contact",
        offcanvas_country: "Country",
        offcanvas_upload_photo: "Upload photo",
        offcanvas_reset_avatar: "Reset",
        offcanvas_allowed_formats: "Allowed JPG, GIF or PNG. Max size of 800K",
        submit_button: "Submit",
        cancel_button: "Cancel",
        // Orders table translations
        th_order_id: "ORDER",
        th_order_date: "DATE",
        th_order_status: "STATUS",
        th_order_spent: "SPENT",
        th_order_actions: "ACTIONS",
        showing_3_of_100_entries: "Showing 3 of 100 entries",
        customers_menu_breadcrumb: "Customers",
        // Breadcrumb translations
        breadcrumb_dashboard: "Dashboard",
        breadcrumb_roles_list: "Roles",
        breadcrumb_role_create: "Create Role",
        breadcrumb_role_edit: "Edit Role",
        breadcrumb_users_list: "Users",
        breadcrumb_user_create: "Create User",
        breadcrumb_user_edit: "Edit User",
        breadcrumb_customers_all: "All Customers",
        breadcrumb_customer_create: "Create Customer",
        breadcrumb_customer_edit: "Edit Customer",
        breadcrumb_customer_details: "Customer Details",
        close_button: "Close",
        // Page titles
        create_customer_page_title: "Create Customer",
        edit_customer_page_title: "Edit Customer",
        details_customer_page_title: "Customer Details",
        all_customers_title: "All Customers",
        // Tooltips
        tooltip_view_customer_details: "View Customer Details",
        tooltip_view_action: "View",
        tooltip_edit_action: "Edit",
        tooltip_deactivate_action: "Deactivate",
        // Countries
        select_country_option: "Select Country",
        country_ar: "Argentina",
        country_bo: "Bolivia",
        country_br: "Brazil",
        country_cl: "Chile",
        country_co: "Colombia",
        country_cr: "Costa Rica",
        country_cu: "Cuba",
        country_do: "Dominican Republic",
        country_ec: "Ecuador",
        country_sv: "El Salvador",
        country_gt: "Guatemala",
        country_hn: "Honduras",
        country_mx: "Mexico",
        country_ni: "Nicaragua",
        country_pa: "Panama",
        country_py: "Paraguay",
        country_pe: "Peru",
        country_pr: "Puerto Rico",
        country_uy: "Uruguay",
        country_ve: "Venezuela",
        // Language selector
        language_english: "English",
        language_spanish: "Spanish",
        // Instructors module
        coaches_menu: "Coaches",
        instructors_menu: "Coaches",
        instructor_profile_page_title: "Coach Profile",
        // Breadcrumbs
        breadcrumb_instructors_profile: "Coach Profile",
        // Pestañas
        tab_instructor_account: "Account",
        tab_instructor_security: "Security",
        tab_instructor_financial: "Financial",
        tab_instructor_notifications: "Notifications",
        tab_instructor_features: "Features",
        // Tab titles
        tab_instructor_account_title: "Account Details",
        tab_instructor_security_title: "Security Settings",
        tab_instructor_financial_title: "Payment Information & History",
        tab_instructor_notifications_title: "Notification Settings",
        tab_instructor_features_title: "Coach Features & Expertise",
        // Buttons
        save_changes_button: "Save changes",
        cancel_button: "Cancel",
        // Form labels
        form_avatar_label: "Avatar",
        form_upload_button: "Upload new photo",
        form_reset_button: "Reset",
        form_avatar_allowed_formats: "Allowed JPG, GIF or PNG. Max size of 800K",
        form_first_name_label: "First Name",
        form_last_name_label: "Last Name",
        form_email_label: "E-mail",
        form_organization_label: "Organization",
        form_phone_number_label: "Phone Number",
        form_address_label: "Address",
        form_state_label: "State",
        form_zip_code_label: "Zip Code",
        form_country_label: "Country",
        form_language_label: "Language",
        form_timezone_label: "Timezone",
        form_currency_label: "Currency",
        form_reset_button_alt: "Reset",
        // Select options
        select_timezone_option: "Select Timezone",
        select_currency_option: "Select Currency",
        // Countries
        country_usa_option: "USA",
        country_ar_option: "Argentina",
        country_bo_option: "Bolivia",
        country_br_option: "Brazil",
        country_cl_option: "Chile",
        country_co_option: "Colombia",
        country_cr_option: "Costa Rica",
        country_cu_option: "Cuba",
        country_do_option: "Dominican Republic",
        country_ec_option: "Ecuador",
        country_sv_option: "El Salvador",
        country_gt_option: "Guatemala",
        country_hn_option: "Honduras",
        country_mx_option: "Mexico",
        country_ni_option: "Nicaragua",
        country_pa_option: "Panama",
        country_py_option: "Paraguay",
        country_pe_option: "Peru",
        country_pr_option: "Puerto Rico",
        country_uy_option: "Uruguay",
        country_ve_option: "Venezuela",
        // Languages
        language_english_option: "English",
        language_spanish_option: "Spanish",
        language_french_option: "French",
        language_german_option: "German",
        language_portuguese_option: "Portuguese",
        language_italian_option: "Italian",
        // Currencies
        currency_usd_option: "USD",
        currency_eur_option: "EUR",
        currency_gbp_option: "GBP",
        currency_ars_option: "ARS - Argentine Peso",
        currency_brl_option: "BRL - Brazilian Real",
        currency_clp_option: "CLP - Chilean Peso",
        currency_cop_option: "COP - Colombian Peso",
        currency_mxn_option: "MXN - Mexican Peso",
        currency_pen_option: "PEN - Peruvian Sol",
        // Delete account section
        delete_account_section_title: "Delete Account",
        delete_account_warning: "Once you delete your account, there is no going back. Please be certain.",
        confirm_account_deactivation_label: "I confirm my account deactivation",
        deactivate_account_button: "Deactivate Account",
        // Security tab
        security_change_password_title: "Change Password",
        security_current_password_label: "Current Password",
        security_new_password_label: "New Password",
        security_confirm_new_password_label: "Confirm New Password",
        security_password_requirements_title: "Password Requirements:",
        security_req_min_chars: "Minimum 8 characters long - the more, the better",
        security_req_lowercase: "At least one lowercase character",
        security_req_number_symbol: "At least one number, symbol, or whitespace character",
        security_2fa_title: "Two-steps verification",
        security_2fa_description: "Two factor authentication adds an additional layer of security to your account by requiring more than just a password to log in.",
        learn_more_link: "Learn more.",
        security_2fa_not_enabled_title: "Two factor authentication is not enabled yet.",
        security_2fa_not_enabled_text: "Two-factor authentication adds an additional layer of security to your account by requiring more than just a password to log in.",
        security_enable_2fa_button: "Enable Two-Factor Authentication",
        security_create_api_key_title: "Create an API key",
        security_create_api_key_description: "API keys allow third-party applications to access your account's data without needing your password.",
        security_choose_key_type_label: "Choose the API key type you want to create",
        select_option_default: "Select Option",
        api_key_type_full: "Full Access",
        api_key_type_read: "Read Only",
        security_name_api_key_label: "Name the API key",
        security_create_key_button: "Create Key",
        security_api_key_list_title: "API Key List & Access",
        th_api_server_key: "Server Key",
        th_api_created_on: "Created On",
        th_api_actions: "Actions",
        api_key_full_access: "Full Access",
        api_key_read_only: "Read Only",
        security_recent_devices_title: "Recent Devices",
        th_device_browser: "BROWSER",
        th_device_device: "DEVICE",
        th_device_location: "LOCATION",
        th_device_recent_activity: "RECENT ACTIVITY",
        // Financial Tab
        financial_payment_method_title: "Preferred Payment Method",
        financial_payment_method_label: "Payment Method",
        financial_payment_method_select_option: "Select payment method",
        financial_payment_method_paypal: "PayPal",
        financial_payment_method_bank_transfer: "Bank Transfer",
        financial_paypal_email_label: "PayPal Email",
        financial_paypal_email_placeholder: "Enter your PayPal email",
        financial_bank_account_holder_label: "Account Holder Name",
        financial_bank_account_holder_placeholder: "Enter account holder name",
        financial_bank_account_number_label: "Account Number / IBAN",
        financial_bank_account_number_placeholder: "Enter account number",
        financial_bank_routing_number_label: "Routing Number",
        financial_bank_routing_number_placeholder: "Enter routing number",
        financial_bank_swift_code_label: "SWIFT Code / BIC",
        financial_bank_swift_code_placeholder: "Enter SWIFT/BIC code",
        financial_payment_frequency_title: "Payment Frequency",
        financial_payment_frequency_label: "Payment Frequency",
        financial_frequency_weekly: "Weekly",
        financial_frequency_biweekly: "Bi-weekly",
        financial_frequency_monthly: "Monthly",
        financial_minimum_threshold_label: "Minimum Payout Threshold (USD)",
        financial_minimum_threshold_placeholder: "Enter minimum amount",
        financial_save_payment_info_button: "Save Payment Information",
        financial_payment_history_title: "Payment History",
        th_payment_date: "DATE",
        th_payment_amount: "AMOUNT",
        th_payment_method: "METHOD",
        th_payment_status: "STATUS",
        th_payment_actions: "ACTIONS",
        payment_status_completed: "Completed",
        payment_status_pending: "Pending",
        payment_status_failed: "Failed",
        financial_view_invoice_tooltip: "View Invoice",
        financial_download_invoice_tooltip: "Download Invoice",
        // Additional Financial Tab keys from prompt
        financial_select_payment_method_label: "Select Method",
        select_option_default: "Select a method",
        payment_method_paypal: "PayPal",
        payment_method_bank_transfer: "Bank Transfer",
        financial_bank_holder_name_label: "Account Holder Name",
        financial_bank_name_label: "Bank Name",
        financial_bank_address_label: "Bank Address",
        frequency_monthly: "Monthly",
        frequency_biweekly: "Bi-Weekly",
        frequency_weekly: "Weekly",
        save_payment_settings_button: "Save Payment Settings",
        cancel_button: "Cancel",
        th_payment_id: "ID",
        th_payment_invoice: "INVOICE",
        showing_3_of_x_payments: "Showing 3 of X payments",
        // Payment status for table
        payment_status_paid: "Paid",
        payment_status_processing: "Processing",
        // Notifications Tab
        notifications_receive_for_items_label: "You will receive notification for the below selected items.",
        th_notifications_type: "TYPE",
        th_notifications_email: "EMAIL",
        th_notifications_browser: "BROWSER",
        th_notifications_app: "APP",
        // Notification Types
        notification_type_new_for_you: "New for you",
        notification_type_account_activity: "Account activity",
        notification_type_new_browser_login: "A new browser used to sign in",
        notification_type_new_device_linked: "A new device is linked",
        notifications_when_to_send_label: "When should we send you notifications?",
        notify_only_when_online: "Only when I'm online",
        notify_anytime: "Anytime",
        notify_daily_digest: "Daily Digest",
        discard_button: "Discard",
        // Features Tab
        features_title_profession_label: "Title / Profession",
        features_years_experience_label: "Years of Experience",
        features_specialties_label: "Specialties",
        features_specialties_note: "Select multiple. If a specialty is not listed, type it to add it (requires JS plugin).",
        manage_specialties_button: "Manage Specialties",
        manage_specialties_modal_title: "Manage Specialties",
        manage_specialties_modal_text: "Here you would list existing specialties with edit/delete options, and a form to add a new one.",
        close_button: "Close",
        // Specialty examples
        specialty_programming: "Programming",
        specialty_design: "Design",
        specialty_marketing: "Marketing",
        specialty_data_science: "Data Science",
        features_education_level_label: "Education Level",
        education_high_school: "High School / GED",
        education_technical_degree: "Technical Degree / Associate",
        education_bachelors: "Bachelor's Degree",
        education_masters: "Master's Degree",
        education_doctorate: "Doctorate (PhD, EdD, etc.)",
        education_professional: "Professional Degree (MD, JD, etc.)",
        education_other: "Other",
        features_institution_label: "Institution of Studies",
        features_certifications_label: "Certifications",
        features_portfolio_url_label: "Portfolio URL",
        features_website_url_label: "Personal Website URL",
        features_document_upload_label: "Upload CV / Diploma",
        features_document_upload_note: "Optional. PDF, DOC, DOCX. Max size 5MB.",
        // Coaches Index Page
        breadcrumb_instructors_list: "Coaches List",
        breadcrumb_instructor_create: "Create Coach",
        create_instructor_page_title: "Create New Coach",
        instructors_list_page_title: "Coaches List",
        add_new_instructor_button: "Add New Coach",
        search_instructor_placeholder: "Search Coach",
        th_instructor_name: "COACH",
        th_instructor_email: "EMAIL",
        th_instructor_specialties: "SPECIALTIES",
        th_instructor_status: "STATUS",
        th_instructor_actions: "ACTIONS",
        tooltip_view_profile: "View Profile",
        tooltip_edit_profile: "Edit Profile",
        tooltip_deactivate_instructor: "Deactivate Coach",
        // Life Areas Module
        life_areas_menu: "Areas of Life",
        breadcrumb_life_areas_list: "Areas of Life List",
        life_areas_page_title: "Areas of Life",
        search_category_placeholder: "Search Area of Life",
        add_category_button: "Add Area of Life",
        // Table headers
        th_life_area_categories: "AREA OF LIFE",
        th_life_area_total_products: "SUB-ITEMS",
        th_life_area_total_earning: "PROGRESS",
        th_life_area_actions: "ACTIONS",
        // Offcanvas
        offcanvas_add_life_area_title: "Add New Area of Life",
        offcanvas_edit_life_area_title: "Edit Area of Life",
        form_label_title: "Title",
        form_placeholder_title: "Enter area of life title",
        form_label_attachment: "Attachment",
        form_label_no_file_chosen: "No file chosen",
        form_attachment_note: "Optional. Small icon or representative image.",
        form_current_attachment: "Current Attachment:",
        form_label_description: "Description",
        form_placeholder_description: "Write a description...",
        form_label_status: "Select area of life status",
        status_active: "Active",
        status_inactive: "Inactive",
        status_scheduled: "Scheduled",
        add_button: "Add",
        // Life Area Names
        life_area_spirituality: "Spirituality",
        life_area_growth_learning: "Growth and learning",
        life_area_social_love: "Social and love",
        life_area_family_friends: "Family and friends",
        life_area_community: "Community",
        life_area_environment: "Environment",
        life_area_work_mission: "Work and mission of life",
        life_area_economy_finance: "Economy and finance",
        life_area_health_body: "Health and body",
        life_area_recreation_fun: "Recreation and fun",
        // Life Area Descriptions
        description_spirituality: "Connect with your inner self and values",
        description_growth_learning: "Personal development and education",
        description_social_love: "Romantic relationships and partnerships",
        description_family_friends: "Family bonds and friendships",
        description_community: "Social connections and civic engagement",
        description_environment: "Your physical surroundings and nature",
        description_work_mission: "Career and life purpose",
        description_economy_finance: "Financial health and wealth building",
        description_health_body: "Physical wellness and fitness",
        description_recreation_fun: "Leisure activities and entertainment",
        // X-Ray of Life Module
        xray_life_menu: "X-Ray of life",
        breadcrumb_xray_life_index: "X-Ray of life",
        xray_life_page_title: "X-Ray of life",
        xray_life_main_title: "X-Ray of Life",
        xray_life_main_subtitle: "Select an Area of Life or search for specific questions.",
        search_articles_placeholder: "Search questions...",
        all_areas_filter: "All Areas",
        // Offcanvas
        offcanvas_add_xray_question_title: "Add New Question",
        offcanvas_edit_xray_question_title: "Edit Question",
        form_label_life_area_category: "Area of Life (Category)",
        select_life_area_placeholder: "Select an Area of Life",
        form_label_xray_question: "Question",
        form_placeholder_xray_question: "Enter the question text...",
        add_button: "Add",
        save_changes_button: "Save Changes",
        add_new_question_button: "Add New Question",
        add_new_question_button_short: "Add",
        no_questions_found: "No questions found for this area or search.",
        no_questions_found_filter: "No questions match your current filter or search criteria.",
        edit_button: "Edit",
        deactivate_button: "Deactivate",
        // Sample Questions
        xray_q_spirit_1: "How do I connect with my spiritual side?",
        xray_q_spirit_2: "What practices help develop inner peace?",
        xray_q_growth_1: "How can I set effective learning goals?",
        xray_q_social_1: "How do I build meaningful relationships?",
        xray_q_family_1: "How to maintain strong family bonds?",
        // Sample Answers
        xray_a_spirit_1: "Consider meditation, prayer, or spending time in nature to connect with your spiritual self.",
        xray_a_spirit_2: "Regular meditation, mindfulness practices, and gratitude exercises can help develop inner peace.",
        xray_a_growth_1: "Set SMART goals, break them into smaller steps, and track your progress regularly.",
        xray_a_social_1: "Be authentic, listen actively, show empathy, and invest time in nurturing relationships.",
        xray_a_family_1: "Schedule regular family time, communicate openly, and create shared traditions and memories.",
        // Email Contents Module
        "email.emailContents": "Email Contents",
        "email.compose": "Compose",
        "email.folders": "Folders",
        "email.labels": "Labels",
        "email.inbox": "Inbox",
        "email.sent": "Sent",
        "email.draft": "Draft",
        "email.starred": "Starred",
        "email.spam": "Spam",
        "email.trash": "Trash",
        "email.labels.personal": "Personal",
        "email.labels.company": "Company",
        "email.labels.important": "Important",
        "email.labels.private": "Private",
        "email.searchMail": "Search mail...",
        "email.refresh": "Refresh",
        "email.markUnread": "Mark as unread",
        "email.delete": "Delete",
        "email.moveToFolder": "Move to folder",
        "email.applyLabel": "Apply label",
        "email.newMessage": "New Message",
        "email.to": "To",
        "email.cc": "CC",
        "email.bcc": "BCC",
        "email.subject": "Subject",
        "email.message": "Message",
        "email.attachments": "Attachments",
        "email.attachFile": "Attach file",
        "email.maxFileSize": "Maximum 10MB per file",
        "email.send": "Send",
        "email.saveDraft": "Save Draft",
        "email.discard": "Discard",
        "email.reply": "Reply",
        "email.replyAll": "Reply All",
        "email.forward": "Forward",
        "email.toPlaceholder": "recipient@example.com",
        "email.ccPlaceholder": "cc@example.com",
        "email.bccPlaceholder": "bcc@example.com",
        "email.subjectPlaceholder": "Enter subject here...",
        "email.messagePlaceholder": "Write your message here...",
        "email.errorLoadingEmails": "Error loading emails",
        "email.errorLoadingDetail": "Error loading email detail",
        "email.errorSending": "Error sending email",
        "email.errorSavingDraft": "Error saving draft",
        "email.errorDeleting": "Error deleting emails",
        "email.errorMoving": "Error moving emails",
        "email.errorApplyingLabel": "Error applying label",
        "email.noEmailsSelected": "No emails selected",
        "email.confirmDelete": "Are you sure you want to delete the selected emails?",
        "email.refreshed": "List refreshed",
        "email.markedAsUnread": "Marked as unread",
        "validation.required": "This field is required",
        // Toolbox Academy Module
        "page_titles.toolboxAcademy": "Toolbox Academy",
        "academy.hero.title": "Toolbox Academy",
        "academy.hero.subtitle": "Learn best practices and techniques with our educational videos",
        "academy.hero.description": "From web development to business strategy, we have quality content for your professional growth.",
        "academy.stats.videosTotal": "Total Videos",
        "academy.stats.categoriesTotal": "Categories",
        "academy.stats.viewsTotal": "Views",
        "academy.stats.videos": "Videos",
        "academy.stats.featured": "Featured",
        "academy.stats.authors": "Authors",
        "academy.stats.views": "Views",
        "academy.stats.title": "Statistics",
        "academy.featured.title": "Featured Videos",
        "academy.featured.description": "Recommended content for you",
        "academy.search.placeholder": "Search videos, categories or topics...",
        "academy.filtering.category": "Showing videos from:",
        "academy.filtering.search": "Search:",
        "academy.filtering.clearAll": "Clear filters",
        "academy.videoUnavailable": "Video unavailable",
        "academy.noVideos.title": "No videos found",
        "academy.noVideos.description": "No videos available for current search criteria.",
        "academy.noVideos.showAll": "Show all videos",
        "academy.loading": "Loading videos...",
        "academy.views": "views",
        "academy.categories.all": "All Categories",
        "academy.categories.webDevelopment": "Web Development",
        "academy.categories.projectManagement": "Project Management",
        "academy.categories.designUiUx": "Design & UI/UX",
        "academy.categories.marketingGrowth": "Marketing & Growth",
        "academy.categories.businessStrategy": "Business Strategy",
        "academy.categories.other": "Other",
        "academy.share.title": "Share Video",
        "academy.relatedVideos.title": "Related Videos",
        "academy.relatedVideos.viewAll": "View All in",
        "academy.backToAcademy.title": "Ready for More?",
        "academy.backToAcademy.description": "Explore more videos in our academy and continue learning.",
        "buttons.backToAcademy": "Back to Academy",
        "buttons.watchVideo": "Watch Video",
        "buttons.search": "Search",
        "labels.categories": "Categories",
        "labels.temas": "Topics",
        "labels.featured": "Featured",
        "labels.author": "Author",
        "labels.publishDate": "Publish Date",
        "labels.tags": "Tags",
        "breadcrumbs.dashboard": "Dashboard",
        "breadcrumbs.academy": "Toolbox Academy",
        // Life Assessment Questionnaires Module
        "sidebar.lifeAssessmentQuestionnaires": "Life Assessment Questionnaires",
        "sidebar.questionnaireTemplates": "Questionnaires",
        
        // Questionnaire Templates Module
        "questionnaire_templates.page_title": "Questionnaire Templates",
        "questionnaire_templates.create_template": "Create Template",
        "questionnaire_templates.edit_template": "Edit Template",
        "questionnaire_templates.list_title": "My Templates",
        "questionnaire_templates.template_details": "Template Details",
        "questionnaire_templates.table.title": "Title",
        "questionnaire_templates.table.description": "Description",
        "questionnaire_templates.table.questions_count": "Questions",
        "questionnaire_templates.table.status": "Status",
        "questionnaire_templates.table.created_at": "Created",
        "questionnaire_templates.table.actions": "Actions",
        "questionnaire_templates.form.title": "Template Title",
        "questionnaire_templates.form.title_placeholder": "Ex: Customer Satisfaction Survey",
        "questionnaire_templates.form.description": "Description (Optional)",
        "questionnaire_templates.form.description_placeholder": "Describe the purpose and context of this questionnaire...",
        "questionnaire_templates.form.description_help": "This description will be shown to users when they respond to the questionnaire.",
        "questionnaire_templates.form.create_and_add_questions": "Create and Add Questions",
        "questionnaire_templates.help.title": "What can you do next?",
        "questionnaire_templates.help.step1": "After creating the template, you can add different types of questions",
        "questionnaire_templates.help.step2": "Organize question order by dragging and dropping",
        "questionnaire_templates.help.step3": "Assign the complete template to specific users to respond",
        "questionnaire_templates.stats.title": "Statistics",
        "questionnaire_templates.stats.questions": "Questions",
        "questionnaire_templates.stats.required": "Required",
        "questionnaire_templates.questions_manager.title": "Question Builder",
        "questionnaire_templates.questions_manager.add_question": "Add Question",
        "questionnaire_templates.questions_manager.add_first_question": "Add First Question",
        "questionnaire_templates.questions_manager.no_questions": "No questions yet",
        "questionnaire_templates.questions_manager.no_questions_help": "Start by adding your first question to build the questionnaire.",
        "questionnaire_templates.question_modal.title": "Add Question",
        "questionnaire_templates.question_modal.question_text": "Question Text",
        "questionnaire_templates.question_modal.question_text_placeholder": "Ex: How satisfied are you with the service?",
        "questionnaire_templates.question_modal.question_type": "Question Type",
        "questionnaire_templates.question_modal.options": "Answer Options",
        "questionnaire_templates.question_modal.add_option": "Add Option",
        "questionnaire_templates.question_modal.likert_options": "Likert Scale",
        "questionnaire_templates.question_modal.add_likert_option": "Add Option",
        "questionnaire_templates.question_modal.is_required": "This question is required",
        "questionnaire_templates.question_modal.save_question": "Save Question",
        "questionnaire_templates.question_types.single_choice": "Multiple Choice (Single Answer)",
        "questionnaire_templates.question_types.multiple_choice": "Multiple Choice (Multiple Answers)",
        "questionnaire_templates.question_types.short_text": "Short Answer",
        "questionnaire_templates.question_types.long_text": "Long Answer",
        "questionnaire_templates.question_types.likert_scale": "Likert Scale",
        "questionnaire_templates.question_types.true_false": "True/False",
        // Assignment translations
        "assign_questionnaire": "Assign Questionnaire",
        "questionnaire_details": "Questionnaire Details",
        "questionnaire_title": "Questionnaire Title",
        "select_clients": "Select Clients",
        "select_clients_help": "Select one or more clients to assign this questionnaire. Use Ctrl+Click for multiple selection.",
        "already_assigned": "Already Assigned",
        "assign": "Assign",
        "assignment_info": "Assignment Information",
        "assignment_info_text": "When assigning this questionnaire:",
        "assignment_benefit_1": "Clients will receive a notification",
        "assignment_benefit_2": "They can complete the questionnaire in their panel",
        "assignment_benefit_3": "You can view responses once completed",
        "assigned_questionnaires": "Assigned Questionnaires",
        "assign_more": "Assign to more clients",
        "back": "Back",
        "client": "Client",
        "assigned_date": "Assignment Date",
        "started_date": "Start Date",
        "completed_date": "Completion Date",
        "progress": "Progress",
        "view_responses": "View Responses",
        "export_pdf": "Export PDF",
        "cancel_assignment": "Cancel Assignment",
        "no_assignments": "No assignments for this questionnaire.",
        "assign_first": "Assign to clients",
        "page_titles.lifeAssessment": "Life Assessment Questionnaires",
        "breadcrumbs.lifeAssessment": "Life Assessment Questionnaires",
        "labels.filterByCategory": "Filter By Category",
        "labels.question": "Question",
        "labels.questions": "Assessment Questions",
        "labels.selectAnswer": "Select Answer",
        "placeholders.selectArea": "Select an Area...",
        "buttons.submitAnswers": "Submit Answers",
        "messages.noQuestionsFound": "No questions found for this area.",
        "messages.answersSubmittedSuccess": "Answers submitted successfully!",
        "messages.selectAreaToStart": "Select an area to start your assessment",
        "messages.selectAreaDescription": "Choose a life area from the dropdown above to see related questions and begin your evaluation.",
        // Wheel of Life Module
        "sidebar.wheelOfLife": "Wheel of Life",
        "page_titles.wheelOfLife": "Wheel of Life",
        "breadcrumbs.wheelOfLife": "Wheel of Life",
        "labels.selectYourScore": "Select your score below:",
        "labels.totalScore": "Total Score:",
        "labels.average": "Average",
        "labels.areas": "Areas",
        "labels.score": "Score",
        "buttons.saveScores": "Save Scores",
        // Wheel of Progress Module
        "sidebar.wheelOfProgress": "Wheel of Progress",
        "page_titles.wheelOfProgress": "Wheel of Progress",
        "breadcrumbs.wheelOfProgress": "Wheel of Progress",
        "wheelOfProgress.subtitle": "Set goals, track your progress and plan your next actions",
        "wheelOfProgress.chartTitle": "Your Progress Wheel",
        "wheelOfProgress.progressTableTitle": "Goals and Progress Management",
        "wheelOfProgress.globalProgress": "Global Progress",
        "wheelOfProgress.totalCategories": "Total Categories",
        "wheelOfProgress.categoriesWithGoals": "With Goals",
        "wheelOfProgress.lastUpdated": "Last Updated",
        "wheelOfProgress.globalLabel": "Global",
        "wheelOfProgress.autoSaveNote": "Changes are saved automatically",
        "wheelOfProgress.saveNote": "Click the button to save changes",
        "table_headers.area": "Area",
        "table_headers.categories": "Categories",
        "table_headers.goal": "Goal",
        "table_headers.percentage": "%",
        "table_headers.nextAction": "Next Action",
        "table_headers.deadline": "Deadline",
        "buttons.saveProgress": "Save Progress",
        // Categories Names
        "categories.exercise-fitness": "Exercise & Fitness",
        "categories.nutrition-diet": "Nutrition & Diet",
        "categories.stress-management": "Stress Management",
        "categories.work-life-balance": "Work-Life Balance",
        "categories.professional-development": "Professional Development",
        "categories.networking": "Networking",
        "categories.emergency-fund": "Emergency Fund",
        "categories.investment-portfolio": "Investment Portfolio",
        "categories.family-time": "Family Time",
        "categories.friendships": "Friendships",
        "categories.learning-education": "Learning & Education",
        "categories.skill-development": "Skill Development",
        // Tasks Module
        "sidebar.tasks": "Tasks",
        "page_titles.tasks": "Tasks Management",
        "breadcrumbs.tasks": "Tasks",
        "tasks.subtitle": "Organize and prioritize your tasks using the Eisenhower Matrix",
        "tasks.eisenhowerMatrix": "Eisenhower Matrix",
        "tasks.taskManagement": "Task Management",
        "tasks.totalTasks": "Total Tasks",
        "tasks.pendingTasks": "Pending",
        "tasks.completedTasks": "Completed",
        "tasks.completionRate": "Completion Rate",
        "tasks.allTasks": "All Tasks",
        "quadrants.urgentImportant": "Urgent & Important",
        "quadrants.notUrgentImportant": "Not Urgent & Important",
        "quadrants.urgentNotImportant": "Urgent & Not Important",
        "quadrants.notUrgentNotImportant": "Not Urgent & Not Important",
        "quadrants.doAction": "Do",
        "quadrants.scheduleAction": "Schedule",
        "quadrants.delegateAction": "Delegate",
        "quadrants.eliminateAction": "Eliminate",
        "labels.taskDescription": "Task Description",
        "labels.urgent": "Urgent",
        "labels.important": "Important",
        "buttons.addTask": "Add Task",
        "buttons.editTask": "Edit Task",
        "buttons.delete": "Delete",
        "buttons.saveChanges": "Save Changes",
        "buttons.cancel": "Cancel",
        "messages.noTasks": "No tasks yet. Add one above!",
        // Life Areas Names
        "areas.physical-health": "Physical Health",
        "areas.mental-health": "Mental Health",
        "areas.career-work": "Career & Work",
        "areas.financial-wellness": "Financial Wellness",
        "areas.relationships": "Relationships",
        "areas.personal-growth": "Personal Growth",
        "areas.spiritual-life": "Spiritual Life",
        "areas.recreation-fun": "Recreation & Fun",
        // Life Areas Descriptions
        "areas.physical-health.description": "Your physical well-being, fitness, and health habits",
        "areas.mental-health.description": "Your mental well-being, stress management, and emotional health",
        "areas.career-work.description": "Your professional life, career satisfaction, and work-life balance",
        "areas.financial-wellness.description": "Your financial health, planning, and money management",
        "areas.relationships.description": "Your relationships with family, friends, and romantic partners",
        "areas.personal-growth.description": "Your personal development, learning, and self-improvement",
        "areas.spiritual-life.description": "Your spiritual well-being, purpose, and inner peace",
        "areas.recreation-fun.description": "Your hobbies, leisure activities, and work-life balance",
        // Habit Tracker Module
        "sidebar.habitTracker": "Habit Tracker",
        "habitTracker.title": "Habit Tracker",
        "habitTracker.addHabit": "Add Habit",
        "habitTracker.tabs.habitLog": "Habit Log",
        "habitTracker.tabs.progressChart": "Progress Chart",
        "habitTracker.habitLog": "Habit Log",
        "headings.progressSummary": "Progress Summary",
        "habitTracker.currentWeek": "Current Week",
        "habitTracker.table.habit": "Habit",
        "habitTracker.table.daysMet": "Days Met",
        "habitTracker.table.percentMet": "% Met",
        "habitTracker.table.actions": "Actions",
        "habitTracker.overallSuccessRate": "Overall Success Rate:",
        "habitTracker.saveLog": "Save Log",
        "breadcrumbs.habitTracker": "Habit Tracker",
        "habitTracker.noHabits.title": "No habits yet",
        "habitTracker.noHabits.description": "Start building better habits by adding your first one!",
        "habitTracker.addFirstHabit": "Add Your First Habit",
        "habitTracker.progressOverview": "Progress Overview",
        "habitTracker.periods.last7Days": "Last 7 Days",
        "habitTracker.periods.last30Days": "Last 30 Days",
        "habitTracker.periods.thisMonth": "This Month",
        "habitTracker.periods.allTime": "All Time",
        "habitTracker.stats.title": "Statistics",
        "habitTracker.stats.totalHabits": "TOTAL HABITS",
        "habitTracker.stats.avgCompletion": "AVG COMPLETION",
        "habitTracker.stats.bestStreak": "BEST STREAK", 
        "habitTracker.stats.activeStreaks": "ACTIVE STREAKS",
        "habitTracker.noData.title": "No data to display",
        "habitTracker.noData.description": "Add some habits and start tracking to see your progress charts!",
        "habitTracker.modal.title": "Add New Habit",
        "habitTracker.modal.name": "Habit Name",
        "habitTracker.modal.nameHelp": "What habit do you want to track?",
        "habitTracker.modal.description": "Description",
        "habitTracker.modal.descriptionHelp": "Optional: Add more details about your habit",
        "habitTracker.modal.color": "Color",
        "habitTracker.modal.colorHelp": "Choose a color to represent this habit",
        "habitTracker.modal.category": "Category",
        "habitTracker.modal.frequency": "Frequency",
        "habitTracker.modal.customDays": "Select Days",
        "habitTracker.modal.reminder": "Enable daily reminders",
        "habitTracker.modal.reminderHelp": "Get notified to maintain your habit streak",
        "habitTracker.modal.addHabit": "Add Habit",
        "habitTracker.categories.health": "Health",
        "habitTracker.categories.fitness": "Fitness",
        "habitTracker.categories.productivity": "Productivity",
        "habitTracker.categories.learning": "Learning",
        "habitTracker.categories.social": "Social",
        "habitTracker.categories.creative": "Creative",
        "habitTracker.categories.mindfulness": "Mindfulness",
        "habitTracker.categories.other": "Other",
        "habitTracker.frequency.daily": "Daily",
        "habitTracker.frequency.weekdays": "Weekdays",
        "habitTracker.frequency.weekends": "Weekends",
        "habitTracker.frequency.custom": "Custom",
        // Calendar Module
        "sidebar.calendar": "Calendar",
        "page_titles.calendar": "Calendar",
        "breadcrumbs.calendar": "Calendar",
        "calendar.subtitle": "Schedule and manage your coaching sessions",
        "days.monday": "Monday",
        "days.tuesday": "Tuesday",
        "days.wednesday": "Wednesday",
        "days.thursday": "Thursday",
        "days.friday": "Friday",
        "days.saturday": "Saturday",
        "days.sunday": "Sunday",
        "days.mon": "Mon",
        "days.tue": "Tue", 
        "days.wed": "Wed",
        "days.thu": "Thu",
        "days.fri": "Fri",
        "days.sat": "Sat",
        "days.sun": "Sun",
        "common.cancel": "Cancel",
        "common.search_placeholder": "Search...",
        "common.filter_active": "Active",
        "common.filter_inactive": "Inactive",
        "common.filter_all": "All",
        "common.page": "Page",
        "common.back": "Back",
        "common.save_changes": "Save Changes",
        // Calendar specific translations
        "breadcrumb_calendar": "Calendar",
        "calendar.title": "Calendar",
        "calendar.addSession": "Add Event",
        "calendar.filters": "Event Filters",
        "calendar.viewAll": "View All",
        "calendar.month": "Month",
        "calendar.week": "Week",
        "calendar.day": "Day",
        "calendar.list": "List",
        "calendar.sessionTitle": "Title",
        "calendar.status": "Status",
        "calendar.startDateTime": "Start Date & Time",
        "calendar.endDateTime": "End Date & Time",
        "calendar.allDay": "All Day",
        "calendar.coach": "Coach",
        "calendar.participants": "Participants",
        "calendar.inviteUsers": "Invite Users",
        "calendar.location": "Location",
        "calendar.description": "Description",
        "calendar.sessionType": "Session Type",
        // Dashboard Permissions
        "dashboard_permissions": "Dashboard Permissions",
        "dashboard_permissions_message": "Control which dashboard components this role can see.",
        "dashboard_visibility": "Dashboard Visibility",
        "can_view_welcome_message": "Can View Welcome Message",
        "can_view_welcome_video": "Can View Welcome Video",
        "can_view_total_clients_card": "Can View Total Clients Card",
        "can_view_active_clients_card": "Can View Active Clients Card",
        // Customer form missing translations
        "form_customer_avatar": "Customer Avatar",
        "form_phone": "Phone",
        "optional_fields_title": "Optional Information",
        "form_company": "Company",
        "form_status_detail": "Status",
        "btn_cancel": "Cancel",
        "btn_create_customer": "Create Customer",
        "help_title": "Help",
        "required_fields_title": "Required Information",
        "form_first_name": "First Name",
        "form_last_name": "Last Name",
        "form_select_country": "Select Country",
        "customer_information_title": "Customer Information",
        "avatar_help_text": "Allowed JPG, GIF or PNG. Max size of 800K",
        // Welcome Message module translations
        "video-configuration": "Video Configuration",
        "video-type": "Video Type",
        "no-video": "No Video",
        "upload-video": "Upload Video",
        "youtube-video": "YouTube Video",
        "vimeo-video": "Vimeo Video",
        "select-video": "Select Video",
        "upload-info": "Upload MP4 file (max 100MB)",
        "youtube-url": "YouTube URL",
        "vimeo-url": "Vimeo URL",
        "save-changes": "Save Changes",
        "update-preview": "Update Preview",
        "preview": "Preview",
        "content-configuration": "Content Configuration",
        "title": "Title",
        // Topics module translations
        "topics_list_title": "Topics List",
        "filter_status": "Filter by Status",
        "filter_all": "All",
        "filter_active": "Active",
        "filter_inactive": "Inactive",
        "add_new_topic_button": "Add New Topic",
        // Website Settings module translations
        "labels.defaultLanguage": "Default Language",
        "help.defaultLanguage": "This will be your preferred language for the interface. You can change it temporarily using the language selector.",
        // User form translations
        "form_default_language": "Default Language",
        "form_default_language_help": "This will be the user's preferred language for the interface.",
        "mobile_filters_button": "Filters",
        "mobile_filters_title": "Filter Options",
        "apply_filters_button": "Apply Filters",
        // Sessions Module
        "sidebar.sessions": "Sessions",
        "breadcrumb_sessions_list": "Sessions",
        "breadcrumb_new_session": "New Session",
        "breadcrumb_edit_session": "Edit Session",
        "sessions_title": "Sessions",
        "search_session_placeholder": "Search session...",
        "add_new_session_button": "Add New Session",
        "table_client": "Client",
        "table_session_date": "Session Date",
        "table_next_session_date": "Next Session",
        "table_ways_to_develop": "Ways to Develop",
        "table_assignments": "Assignments",
        "table_challenges": "Challenges",
        "table_key_points": "Key Points",
        "table_status": "Status",
        "table_actions": "Actions",
        "status_active": "Active",
        "status_inactive": "Inactive",
        "status_filter_label": "Filter by Status",
        "action_edit": "Edit",
        "action_deactivate": "Deactivate",
        "action_activate": "Activate",
        "no_sessions_found": "No sessions found",
        "no_permissions": "No permissions",
        // Sessions Form
        "main_information_title": "Main Information",
        "field_client": "Client",
        "select_client_placeholder": "Select a client",
        "field_session_datetime": "Session Date and Time",
        "field_next_session_datetime": "Next Session Date and Time",
        "session_content_title": "Session Content",
        "field_key_points": "Key Points",
        "field_ways_to_develop": "Ways to Develop",
        "field_assignments": "Assignments",
        "field_challenges": "Challenges",
        "feedback_others_title": "Feedback and Others",
        "field_feedback": "Feedback",
        "field_twitter": "Twitter",
        "attachments_title": "Attachments / Materials",
        "existing_attachments_title": "Existing Attachments",
        "new_attachments_title": "Add New Files",
        "drop_files_here": "Drop files here or click to select",
        "file_upload_hint": "JPG, PNG, PDF, DOC, DOCX, MP4, MOV (Max: 10MB)",
        "selected_files": "Selected files:",
        "no_existing_attachments": "No attachments",
        "save_button": "Save",
        "update_button": "Update",
        "cancel_button": "Cancel",
        "notification_title": "Notification",
        "validation_errors_title": "Validation errors:",
        "confirm_delete_file": "Are you sure you want to delete this file?",
        "required_fields_note": "Required fields",
        // Notifications Module
        "sidebar.notifications": "Notifications",
        "sidebar.notificationPreferences": "Notification Preferences",
        "sidebar.clientQuizzes": "My Questionnaires",
        "sidebar.lifeEvents": "Life Line",
        "sidebar.communicationWheelTemplates": "Communication Wheel Templates",
        "sidebar.coachClientWheels": "Client Communication Wheels",
        "sidebar.clientCommunicationWheels": "My Communication Wheels",
        // Communication Wheel Module
        "communication_wheel_templates_title": "Communication Wheel Templates",
        "communication_wheel_edit_template": "Edit Template",
        "communication_wheel_template_details": "Template Details",
        "communication_wheel_template_name": "Template Name",
        "communication_wheel_template_description": "Description",
        "communication_wheel_dimensions_title": "Communication Dimensions",
        "communication_wheel_add_dimension": "Add Dimension",
        "communication_wheel_dimensions_info": "Drag dimensions to reorder them. Order affects how they appear in the chart.",
        "communication_wheel_no_dimensions": "This template doesn't have dimensions defined yet.",
        "communication_wheel_add_first_dimension": "Add First Dimension",
        "communication_wheel_dimension_modal_title": "Communication Dimension",
        "communication_wheel_dimension_name": "Dimension Name",
        "communication_wheel_dimension_name_placeholder": "e.g., Active Listening",
        "communication_wheel_dimension_name_required": "Name is required",
        "communication_wheel_guiding_question": "Guiding Question (Optional)",
        "communication_wheel_guiding_question_placeholder": "e.g., Do I pay full attention and understand what others say?",
        "communication_wheel_guiding_question_help": "This question will help the client reflect on this dimension",
        "button_back": "Back",
        "button_save": "Save",
        "button_cancel": "Cancel",
        "notifications.title": "Notifications",
        "notifications.markAllAsRead": "Mark all as read",
        "notifications.viewAll": "View all notifications",
        "notifications.noUnread": "No unread notifications",
        "notifications.noNotifications": "No notifications",
        "pagination.previous": "Previous",
        "pagination.next": "Next",
        "pagination.page": "Page",
        "notification_types.session_scheduled_by_coach": "New session scheduled",
        "notification_types.calendar_event_scheduled_for_client": "New calendar event",
        "notification_types.calendar_event_invitation": "Event invitation",
        "notification_types.questionnaire_assigned": "New questionnaire assigned",
        "notification_types.questionnaire_completed_by_client": "Questionnaire completed",
        "notification_types.communication_wheel_assigned": "New communication wheel assigned",
        "notification_types.communication_wheel_completed": "Communication wheel completed",
        "notifications.newCount": "New",
        "notifications.markAllAsReadTooltip": "Mark all as read",
        "common.loading": "Loading...",
        // Life Events
        "life_events.page_title": "Life Line",
        "life_events.add_event": "Add Event",
        "life_events.create_title": "Add Event",
        "life_events.edit_title": "Edit Event",
        "life_events.delete_title": "Delete Event",
        "life_events.event_details": "Event Details",
        "life_events.chart_title": "Life Line Chart",
        "life_events.events_list": "Events List",
        "life_events.no_events": "You have no events recorded",
        "life_events.no_events_description": "Start creating your first life line by adding significant events from your life.",
        "life_events.add_first_event": "Add First Event",
        "life_events.event_to_delete": "Event to delete:",
        "life_events.confirm_delete": "Confirm Deletion",
        "life_events.delete_warning_title": "Are you sure you want to delete this event?",
        "life_events.delete_warning_message": "This action cannot be undone. The event will be permanently deleted from your life line.",
        "life_events.confirm_delete_button": "Yes, Delete Event",
        "life_events.no_description": "No description",
        "life_events.stats.total": "Total Events",
        "life_events.stats.positive": "Positive Events",
        "life_events.stats.negative": "Negative Events",
        "life_events.stats.neutral": "Neutral Events",
        "life_events.table.year": "Year",
        "life_events.table.title": "Title",
        "life_events.table.impact": "Impact",
        "life_events.table.description": "Description",
        "life_events.table.actions": "Actions",
        "life_events.form.title": "Event Title",
        "life_events.form.title_placeholder": "Ex: Graduation, Marriage, New job...",
        "life_events.form.year": "Event Year",
        "life_events.form.description": "Description",
        "life_events.form.description_placeholder": "Describe this event in more detail (optional)...",
        "life_events.form.impact": "Emotional Impact",
        "life_events.form.impact_instruction": "Select the emotional impact this event had on your life:",
        "life_events.form.impact_help": "The scale goes from -3 (very negative) to +3 (very positive). Think about how this event affected your life overall.",
        "life_events.impact.very_positive": "Very Positive",
        "life_events.impact.positive": "Positive",
        "life_events.impact.slightly_positive": "Slightly Positive",
        "life_events.impact.neutral": "Neutral",
        "life_events.impact.slightly_negative": "Slightly Negative",
        "life_events.impact.negative": "Negative",
        "life_events.impact.very_negative": "Very Negative",
        // Client Quizzes
        "client_quizzes.page_title": "My Questionnaires",
        "client_quizzes.list_title": "Assigned Questionnaires",
        "client_quizzes.table.title": "Questionnaire",
        "client_quizzes.table.assigned_date": "Assigned Date",
        "client_quizzes.table.status": "Status",
        "client_quizzes.table.progress": "Progress",
        "client_quizzes.table.actions": "Actions",
        "client_quizzes.status.pending": "Pending",
        "client_quizzes.status.in_progress": "In Progress",
        "client_quizzes.status.completed": "Completed",
        "client_quizzes.no_quizzes": "You have no assigned questionnaires",
        "client_quizzes.no_quizzes_description": "When your coach assigns questionnaires, they will appear here.",
        "client_quizzes.filter_by_status": "Filter by status:",
        "client_quizzes.filter.all": "All",
        "client_quizzes.filter.pending": "Pending",
        "client_quizzes.filter.in_progress": "In Progress",
        "client_quizzes.filter.completed": "Completed",
        "client_quizzes.respond_title": "Respond to Questionnaire",
        "client_quizzes.instructions": "Please answer all required questions",
        "client_quizzes.save_draft": "Save Draft",
        "client_quizzes.submit_answers": "Submit Answers",
        "client_quizzes.progress_title": "Progress",
        "client_quizzes.auto_save": "Auto-save enabled",
        "client_quizzes.required_fields": "Required fields are marked with *"
    },
    es: {
        dashboard: "Tablero",
        roles_menu: "Roles",
        users_menu: "Usuarios",
        users_list_title: "Lista de Usuarios",
        // Roles
        role_editor: "Editor",
        role_maintainer: "Mantenedor",
        role_admin: "Administrador",
        role_author: "Autor",
        role_subscriber: "Suscriptor",
        // Acciones
        action_view: "Ver",
        action_suspend: "Suspender",
        // Tarjetas de estadísticas
        card_session_title: "Sesión",
        card_session_total_users: "Usuarios Totales",
        card_active_users_title: "Usuarios Activos",
        card_active_users_last_week: "Análisis de la última semana",
        // Sección de Filtros
        filters_title: "Filtros",
        filter_select_role: "Seleccionar Rol",
        filter_select_plan: "Seleccionar Plan",
        filter_select_status: "Seleccionar Estado",
        // Controles de Tabla
        search_user_placeholder: "Buscar Usuario",
        export_button: "Exportar",
        export_to_excel: "Exportar a Excel",
        export_to_pdf: "Exportar a PDF",
        export_to_csv: "Exportar a CSV",
        add_new_record_button: "Añadir Nuevo Registro",
        // Encabezados de Tabla
        th_user_list_user: "USUARIO",
        th_user_list_role: "ROL",
        th_user_list_plan: "PLAN",
        th_user_list_billing: "FACTURACIÓN",
        th_user_list_status: "ESTADO",
        th_user_list_actions: "ACCIONES",
        // Paginación
        showing_entries: "Mostrando {start} a {end} de {total} entradas",
        // Add User Offcanvas
        add_user_title: "Añadir Usuario",
        form_user_avatar: "Avatar de Usuario",
        form_upload_new: "Subir Nuevo",
        form_reset: "Restablecer",
        form_full_name: "Nombre Completo",
        form_username: "Nombre de Usuario",
        form_email: "Correo Electrónico",
        form_role: "Rol",
        form_plan: "Plan",
        form_select_role: "Seleccionar Rol",
        form_select_plan: "Seleccionar Plan",
        form_feedback_name: "Por favor, introduce un nombre válido",
        form_feedback_username: "Por favor, introduce un nombre de usuario válido",
        form_feedback_email: "Por favor, introduce un correo electrónico válido",
        form_feedback_role: "Por favor, selecciona un rol",
        form_feedback_plan: "Por favor, selecciona un plan",
        form_section_payment: "Detalles de Pago",
        form_billing_method: "Método de Facturación",
        form_status: "Estado",
        form_status_active: "Activo",
        form_tax_id: "ID Fiscal",
        form_contact: "Contacto",
        form_language: "Idioma",
        form_country: "País",
        form_section_security: "Seguridad",
        form_password: "Contraseña",
        form_confirm_password: "Confirmar Contraseña",
        form_feedback_password: "Por favor, proporciona una contraseña",
        form_feedback_password_match: "Las contraseñas deben coincidir",
        form_2fa: "Habilitar Autenticación de Dos Factores",
        form_button_cancel: "Cancelar",
        form_button_submit: "Añadir Usuario",
        mobile_filters_button: "Filtros",
        mobile_filters_title: "Opciones de Filtro",
        apply_filters_button: "Aplicar Filtros",
        payment_auto_debit: "Débito Automático",
        payment_manual_paypal: "Manual - Paypal",
        payment_manual_cc: "Manual - Tarjeta de Crédito",
        payment_manual_bank: "Manual - Transferencia Bancaria",
        language_english: "Inglés",
        language_spanish: "Español",
        country_usa: "Estados Unidos",
        country_uk: "Reino Unido",
        country_spain: "España",
        country_france: "Francia",
        country_germany: "Alemania",
        country_other: "Otro",
        plan_basic: "Básico",
        plan_team: "Equipo",
        plan_company: "Empresa",
        plan_enterprise: "Empresa Grande",
        // Nuevas traducciones para el offcanvas mejorado
        offcanvas_user_avatar: "Avatar de Usuario",
        offcanvas_upload_new_photo: "Subir nueva foto",
        offcanvas_reset_avatar: "Restablecer",
        offcanvas_avatar_allowed_formats: "Formatos permitidos JPG, GIF o PNG. Tamaño máx. de 800K",
        offcanvas_username: "Nombre de usuario",
        offcanvas_password: "Contraseña",
        offcanvas_company: "Empresa",
        select_country_option: "Seleccionar País",
        all_option: "Todos",
        roles_list_title: "Lista de Roles",
        roles_list_description: "Un rol proporciona acceso a menús y funciones predefinidos para que, dependiendo del rol asignado, un administrador pueda tener acceso a lo que el usuario necesita.",
        total_users_with_roles: "Total de usuarios con sus roles",
        add_new_role_card_title: "Añadir Nuevo Rol",
        add_new_role_card_text: "Añade un nuevo rol, si no existe.",
        total_users_role_prefix: "Total usuarios",
        edit_role_link: "Editar Rol",
        delete_role_title: "Eliminar Rol",
        delete_role: "Eliminar Rol",
        delete_role_confirmation_title: "Confirmación de Eliminación de Rol",
        delete_role_warning: "¿Está seguro de que desea eliminar este rol? Esta acción no se puede deshacer.",
        delete_role_info: "Todos los permisos asociados con este rol serán eliminados. Los usuarios deberán ser asignados a un rol diferente.",
        delete_confirm_button: "Eliminar Rol",
        permissions_count: "Permisos",
        permissions_assigned: "permisos asignados",
        no_permissions_assigned: "Sin permisos asignados",
        status: "Estado",
        search_placeholder: "Buscar...",
        export_button: "Exportar",
        add_new_user_button: "Añadir Nuevo Usuario",
        status_active: "Activo",
        status_pending: "Pendiente",
        status_inactive: "Inactivo",
        modal_add_new_role_title: "Añadir Nuevo Rol",
        modal_set_role_permissions: "Establecer permisos del rol",
        modal_role_name_label: "Nombre del Rol",
        modal_enter_role_name_placeholder: "Introduce un nombre de rol",
        modal_role_permissions_section_title: "Permisos del Rol",
        perm_admin_access: "Acceso de Administrador",
        tooltip_admin_access: "Permite acceso completo al sistema",
        select_all: "Seleccionar Todo",
        perm_read: "Leer",
        perm_write: "Escribir",
        perm_create: "Crear",
        perm_cat_user_management: "Gestión de Usuarios",
        perm_cat_content_management: "Gestión de Contenido",
        perm_cat_disputes_management: "Gestión de Disputas",
        perm_cat_database_management: "Gestión de Base de Datos",
        perm_cat_financial_management: "Gestión Financiera",
        perm_cat_reporting: "Reportes",
        perm_cat_api_control: "Control de API",
        perm_cat_repository_management: "Gestión de Repositorio",
        perm_cat_payroll: "Nómina",
        cancel_button: "Cancelar",
        confirm_button: "Confirmar",
        confirm_status_change: "Confirmar Cambio de Estado",
        submit_button: "Enviar",
        // Create Role Page translations
        create_new_role: "Crear Nuevo Rol",
        role_information: "Información del Rol",
        role_name: "Nombre del Rol",
        role_name_placeholder: "Ingrese el nombre del rol",
        description: "Descripción",
        optional: "(Opcional)",
        description_placeholder: "Ingrese una descripción opcional",
        assigned_dashboard: "Dashboard Asignado",
        select_dashboard: "Seleccionar Dashboard",
        admin_dashboard: "Admin Dashboard",
        coach_dashboard: "Coach Dashboard",
        client_dashboard: "Client Dashboard",
        role_permissions: "Permisos del Rol",
        select_permissions_message: "Seleccione los permisos que desea asignar a este rol.",
        no_permissions_available: "No hay permisos disponibles para asignar.",
        create_role_button: "Crear Rol",
        edit_role: "Editar Rol",
        edit_role_title: "Editar Rol",
        update_role_button: "Actualizar Rol",
        role_active_status: "Estado Activo",
        information: "Información",
        about_roles: "Acerca de los Roles",
        about_roles_description: "Los roles definen qué acciones pueden realizar los usuarios en el sistema. Cada rol tiene un conjunto específico de permisos que determinan el acceso a diferentes módulos y funcionalidades.",
        available_dashboards: "Dashboards Disponibles",
        admin: "Admin",
        coach: "Coach",
        client: "Client",
        admin_dashboard_description: "Panel completo con todas las funcionalidades",
        coach_dashboard_description: "Panel para entrenadores y gestores",
        client_dashboard_description: "Panel para clientes finales",
        permission_types: "Tipos de Permisos",
        read: "Leer",
        write: "Escribir",
        create: "Crear",
        read_permission_description: "Permite ver información",
        write_permission_description: "Permite editar información existente",
        create_permission_description: "Permite crear nueva información",
        th_user: "USUARIO",
        th_role: "ROL",
        th_plan: "PLAN",
        th_billing: "FACTURACIÓN",
        th_status: "ESTADO",
        th_actions: "ACCIONES",
        areas_of_life: "Áreas de Vida",
        xray_of_life: "Radiografía de Vida",
        default_welcome_message: "Mensaje de Bienvenida",
        "welcome-message-title": "Mensaje de Bienvenida Predeterminado",
        "welcome-message-subtitle": "Configura el mensaje de bienvenida que verán los clientes al acceder a la plataforma",
        "content-configuration": "Configuración de Contenido",
        "video-configuration": "Configuración de Video",
        "title": "Título",
        "description": "Descripción",
        "video-type": "Tipo de Video",
        "no-video": "Sin Video",
        "upload-video": "Subir Video",
        "youtube-video": "YouTube",
        "vimeo-video": "Vimeo",
        "select-video": "Seleccionar Video",
        "upload-info": "Formatos soportados: MP4, AVI, MOV, WMV, WEBM",
        "youtube-url": "URL de YouTube",
        "vimeo-url": "URL de Vimeo",
        "save-changes": "Guardar Cambios",
        "update-preview": "Actualizar Vista Previa",
        "preview": "Vista Previa",
        coaches: "Coaches",
        add_user: "Añadir Usuario",
        profile: "Perfil",
        academy: "Academia",
        academy_submenu_1: "Cursos",
        academy_submenu_2: "Materiales",
        "sidebar.academia.topics": "Temas",
        "sidebar.academia.videoManagement": "Gestión de Videos",
        // Topics Module
        "breadcrumb_topics_list": "Lista de Temas",
        "page_titles.topicsList": "Lista de Temas",
        "buttons.addNewTopic": "Añadir Nuevo Tema",
        "offcanvas_titles.addNewTopic": "Añadir Nuevo Tema",
        "offcanvas_titles.editTopic": "Editar Tema",
        "labels.topicName": "Nombre del Tema",
        "labels.description": "Descripción",
        "labels.status": "Estado",
        "status_options.active": "Activo",
        "status_options.inactive": "Inactivo",
        "actions.viewEdit": "Ver/Editar",
        "actions.delete": "Eliminar",
        "search_topics_placeholder": "Buscar Temas...",
        "placeholders.topicName": "Ingresa el nombre del tema",
        "placeholders.topicDescription": "Escribe una breve descripción...",
        "confirm_messages.deleteTopic": "¿Estás seguro de que quieres eliminar este tema?",
        "buttons.save": "Guardar",
        "buttons.cancel": "Cancelar",
        "th_topic_name": "NOMBRE DEL TEMA",
        "th_topic_status": "ESTADO",
        "th_topic_actions": "ACCIONES",
        // Video Management Module
        "breadcrumb_video_management_list": "Lista de Gestión de Videos",
        "breadcrumb_add_new_video": "Añadir Nuevo Video",
        "breadcrumb_edit_video": "Editar Video",
        "page_titles.videoManagementList": "Lista de Gestión de Videos",
        "page_titles.addNewVideo": "Añadir Nuevo Video",
        "page_titles.editVideo": "Editar Video",
        "buttons.addNewVideo": "Añadir Nuevo Video",
        "buttons.goBack": "Volver",
        "buttons.saveVideo": "Guardar Video",
        "buttons.cancel": "Cancelar",
        "labels.videoSection": "Sección de Video",
        "labels.currentVideo": "Video Actual",
        "labels.videoUrl": "URL del Video",
        "labels.videoSource": "Fuente del Video",
        "labels.uploadFile": "Subir Archivo",
        "labels.videoFile": "Archivo de Video",
        "labels.videoPreview": "Vista Previa del Video",
        "labels.uploadVideoFile": "Subir Archivo de Video",
        "labels.basicInformation": "Información Básica",
        "labels.videoTitle": "Título del Video",
        "labels.videoDescription": "Descripción del Video",
        "labels.authorUser": "Autor/Usuario",
        "labels.user": "Usuario",
        "labels.topic": "Tema",
        "labels.mediaType": "Tipo de Medio",
        "labels.duration": "Duración (mm:ss)",
        "labels.videoSettings": "Configuración del Video",
        "labels.isFeatured": "¿Marcar como Destacado?",
        "labels.featuredToggle": "Video Destacado",
        "labels.featuredBadge": "Destacado",
        "labels.status": "Estado",
        "labels.uploadInfo": "Información de Subida",
        "labels.uploadDate": "Fecha de Subida",
        "labels.lastUpdated": "Última Actualización",
        "labels.seoSettings": "Configuración SEO",
        "labels.metaTitle": "Meta Título",
        "labels.metaDescription": "Meta Descripción",
        "labels.keywords": "Palabras Clave",
        "media_types.youtube": "URL de YouTube",
        "media_types.vimeo": "URL de Vimeo",
        "media_types.uploadedFile": "Archivo Subido",
        "status_options.published": "Publicado",
        "status_options.draft": "Borrador",
        "status_options.archived": "Archivado",
        "search.placeholder.videos": "Buscar Videos...",
        // Missing Video Management translations
        "featured_yes": "Sí",
        "featured_no": "No",
        "type_upload": "Subido",
        "filter_select_featured": "Seleccionar Destacados",
        "status_published": "Publicado",
        "status_draft": "Borrador",
        "status_archived": "Archivado",
        "filter_select_type": "Seleccionar Tipo",
        "placeholders.videoUrl": "https://youtube.com/watch?v=...",
        "placeholders.videoTitle": "Ingresa el título del video",
        "placeholders.videoDescription": "Escribe una descripción detallada...",
        "placeholders.selectAuthor": "Seleccionar Autor",
        "placeholders.selectUser": "Seleccionar Usuario",
        "placeholders.selectTopic": "Seleccionar Tema",
        "placeholders.duration": "15:30",
        "placeholders.metaTitle": "Título SEO para motores de búsqueda",
        "placeholders.metaDescription": "Descripción SEO para motores de búsqueda",
        "placeholders.keywords": "palabra1, palabra2, palabra3",
        "help.videoUrl": "Ingresa URL de YouTube o Vimeo",
        "help.videoFile": "O sube un archivo de video (MP4, AVI, MOV)",
        "helpers.videoUrlHelp": "Ingrese una URL válida de YouTube o Vimeo",
        "helpers.videoFileHelp": "Tamaño máximo: 500MB. Formatos soportados: MP4, AVI, MOV, WMV",
        "help.featured": "Los videos destacados aparecen prominentemente en las listas",
        "th_video_title": "TÍTULO",
        "th_video_topic": "TEMA",
        "th_video_author": "AUTOR",
        "th_video_upload_date": "FECHA DE SUBIDA",
        "th_video_status": "ESTADO",
        "th_video_actions": "ACCIONES",
        "confirm_messages.deleteVideo": "¿Estás seguro de que quieres eliminar este video?",
        // Website Settings Module
        "sidebar.websiteSettings": "Configuración del Sitio Web",
        "breadcrumb_website_settings": "Configuración del Sitio Web",
        "page_titles.websiteSettings": "Configuración del Sitio Web",
        "sections.generalInfo": "Información General del Sitio",
        "sections.socialMediaLinks": "Enlaces de Redes Sociales",
        "sections.siteBranding": "Marca del Sitio",
        "sections.additionalSettings": "Configuraciones Adicionales",
        "labels.siteEmail": "Email del Sitio",
        "labels.siteAddress": "Dirección del Sitio",
        "labels.sitePhone": "Teléfono del Sitio",
        "labels.footerMessage": "Mensaje del Pie de Página",
        "labels.siteName": "Nombre del Sitio",
        "labels.siteDescription": "Descripción del Sitio",
        "labels.appLogo": "Logo de la Aplicación",
        "labels.logoPreview": "Vista Previa del Logo",
        "labels.timeZone": "Zona Horaria Predeterminada",
        "labels.defaultLanguage": "Idioma Predeterminado",
        "labels.lastUpdated": "Última actualización",
        "labels.by": "por",
        "labels.facebook": "Facebook",
        "labels.twitter": "Twitter",
        "labels.google": "Google",
        "labels.linkedin": "LinkedIn",
        "labels.youtube": "YouTube",
        "labels.instagram": "Instagram",
        "labels.telegram": "Telegram",
        "labels.tiktok": "TikTok",
        "labels.discord": "Discord",
        "labels.reddit": "Reddit",
        "buttons.saveSettings": "Guardar Configuración",
        "buttons.resetLogo": "Restablecer por Defecto",
        "buttons.cancel": "Cancelar",
        "placeholders.siteEmail": "info@empresa.com",
        "placeholders.sitePhone": "+1 (555) 123-4567",
        "placeholders.siteAddress": "Ingresa la dirección de tu negocio",
        "placeholders.footerMessage": "Mensaje de copyright o texto del pie de página",
        "placeholders.siteName": "Nombre de tu Sitio",
        "placeholders.siteDescription": "Breve descripción de tu sitio web",
        "placeholders.facebookUrl": "https://facebook.com/tupagina",
        "placeholders.twitterUrl": "https://twitter.com/tupagina",
        "placeholders.googleUrl": "https://google.com/tupagina",
        "placeholders.linkedinUrl": "https://linkedin.com/company/tupagina",
        "placeholders.youtubeUrl": "https://youtube.com/c/tupagina",
        "placeholders.instagramUrl": "https://instagram.com/tupagina",
        "placeholders.telegramUrl": "https://t.me/tupagina",
        "placeholders.tiktokUrl": "https://tiktok.com/@tupagina",
        "placeholders.discordUrl": "https://discord.gg/tuservidor",
        "placeholders.redditUrl": "https://reddit.com/r/tusubreddit",
        "help.socialMediaLinks": "Ingresa las URLs completas de tus perfiles de redes sociales. Deja vacío si no tienes cuenta en esa plataforma.",
        "help.logoUpload": "Sube un logo en formato PNG para mejor calidad y transparencia. Tamaño recomendado: 200x60px. Tamaño máximo: 5MB.",
        "help.logoPreview": "Así es como aparecerá tu logo en la barra lateral",
        "messages.settingsSaved": "Configuración guardada exitosamente",
        "messages.logoUploaded": "Logo subido exitosamente",
        "messages.logoReset": "Logo restablecido por defecto",
        "messages.invalidFileType": "Tipo de archivo inválido. Por favor sube archivos PNG, JPG, JPEG, SVG, o GIF.",
        "messages.fileTooLarge": "Archivo demasiado grande. El tamaño máximo permitido es 5MB.",
        toolbox_academy: "Academia ToolBox",
        website_setting: "Configuración Web",
        email_contents: "Contenidos de Email",
        analytics: "Analítica",
        settings: "Configuración",
        notifications: "Notificaciones",
        logout: "Cerrar Sesión",
        select_language: "Seleccionar Idioma",
        welcome_message_admin: 'Estimado Carlos Checo <span data-translate-key="welcome">Bienvenido</span>. Has iniciado sesión como <span data-translate-key="admin_role">Administrador</span>',
        welcome: "Bienvenido",
        admin_role: "Administrador",
        total_coach: "Total de Coaches",
        active_coaches: "Coaches Activos",
        activity_rate: "Tasa de Actividad",
        // Customers module
        customers_menu: "Clientes",
        all_customers_title: "Todos los Clientes",
        search_order_placeholder: "Buscar Cliente",
        add_customer_button: "Añadir Cliente",
        add_new_customer_button: "Agregar Cliente",
        breadcrumb_customers_list: "Clientes",
        customers_filters_title: "Filtros de Clientes",
        notification_title: "Notificación",
        status_all: "Todos",
        // Customer table headers
        th_customer_list_customer: "CLIENTE",
        th_customer_list_customer_id: "ID CLIENTE",
        th_customer_list_country: "PAÍS",
        th_customer_list_order: "PEDIDOS",
        th_customer_list_total_spent: "GASTO TOTAL",
        th_customer_list_id: "ID CLIENTE",
        th_customer_list_phone: "TELÉFONO",
        th_customer_list_status: "ESTADO",
        th_customer_list_actions: "ACCIONES",
        // Customer details
        customer_details_title_prefix: "Cliente ID #",
        customer_details_joined_on: "Se unió el",
        button_edit_details: "Editar Detalles",
        button_delete_customer: "Desactivar Cliente",
        button_deactivate_customer: "Desactivar Cliente",
        card_upgrade_to_premium_title: "Actualizar a premium",
        card_upgrade_to_premium_text: "Actualiza el cliente a membresía premium para acceder a funciones pro.",
        button_upgrade_to_premium: "Actualizar a premium",
        // Tabs
        tab_overview: "Resumen",
        tab_security: "Seguridad",
        tab_address_billing: "Dirección y Facturación",
        tab_notifications: "Notificaciones",
        // Overview tab content
        overview_orders_title: "Pedidos",
        overview_spent_title: "Gastado",
        overview_details_section_title: "Detalles",
        overview_username_label: "Usuario:",
        overview_email_label: "Correo:",
        overview_status_label: "Estado:",
        overview_contact_label: "Contacto:",
        overview_country_label: "País:",
        overview_account_balance_title: "Saldo de Cuenta",
        overview_credit_left: "Crédito Restante",
        overview_account_balance_next_purchase: "Saldo para la próxima compra",
        overview_loyalty_program_title: "Programa de Lealtad",
        overview_platinum_member: "Miembro Platino",
        overview_points_to_next_tier: "puntos para el siguiente nivel",
        overview_wishlist_title: "Lista de Deseos",
        overview_items_in_wishlist: "artículos en lista de deseos",
        overview_wishlist_receive_notification: "Recibir notificación cuando los artículos estén en oferta",
        overview_coupons_title: "Cupones",
        overview_coupons_you_win: "Cupones ganados",
        overview_coupons_use_on_next_purchase: "Usar cupón en la próxima compra",
        overview_orders_placed_title: "Pedidos Realizados",
        overview_search_order_placeholder: "Buscar Pedido",
        // Customer offcanvas form translations
        offcanvas_add_customer_title: "Añadir Nuevo Cliente",
        offcanvas_edit_customer_title: "Editar Cliente",
        offcanvas_customer_avatar: "Avatar del Cliente",
        offcanvas_avatar_upload: "Subir",
        offcanvas_avatar_reset: "Restablecer",
        offcanvas_avatar_allowed: "Formatos permitidos JPG, GIF o PNG. Tamaño máx. de 800K",
        offcanvas_customer_first_name: "Nombre",
        offcanvas_customer_last_name: "Apellido",
        offcanvas_customer_email: "Correo Electrónico",
        offcanvas_customer_password: "Contraseña",
        offcanvas_customer_confirm_password: "Confirmar Contraseña",
        password_edit_note: "Dejar en blanco para mantener la contraseña actual.",
        offcanvas_customer_phone: "Número de Teléfono",
        offcanvas_customer_company: "Empresa",
        offcanvas_customer_country: "País",
        offcanvas_customer_address: "Dirección",
        offcanvas_customer_state: "Estado/Provincia",
        offcanvas_customer_zip: "Código Postal",
        offcanvas_customer_tax_id: "ID Fiscal",
        offcanvas_customer_language: "Idioma",
        offcanvas_customer_status: "Estado",
        offcanvas_customer_submit: "Enviar",
        offcanvas_customer_cancel: "Cancelar",
        // Placeholders
        offcanvas_customer_first_name_placeholder: "Juan",
        offcanvas_customer_last_name_placeholder: "Pérez",
        offcanvas_customer_email_placeholder: "juan.perez@ejemplo.com",
        offcanvas_customer_password_placeholder: "············",
        offcanvas_customer_confirm_password_placeholder: "············",
        offcanvas_customer_phone_placeholder: "+1 (555) 123-4567",
        offcanvas_customer_company_placeholder: "Mi Empresa",
        offcanvas_customer_country_select: "Seleccionar País",
        offcanvas_customer_address_placeholder: "Calle Principal 123",
        offcanvas_customer_state_placeholder: "Madrid",
        offcanvas_customer_zip_placeholder: "28001",
        offcanvas_customer_tax_id_placeholder: "TAX-12345",
        offcanvas_customer_language_select: "Seleccionar Idioma",
        // Create/Edit page titles
        create_customer_page_title: "Crear Cliente",
        edit_customer_page_title: "Editar Cliente",
        customers_menu_breadcrumb: "Clientes",
        // Orders table translations
        th_order_id: "PEDIDO",
        th_order_date: "FECHA",
        th_order_status: "ESTADO",
        th_order_spent: "GASTADO",
        th_order_actions: "ACCIONES",
        showing_3_of_100_entries: "Mostrando 3 de 100 entradas",
        // Breadcrumb translations
        breadcrumb_dashboard: "Tablero",
        breadcrumb_roles_list: "Roles",
        breadcrumb_role_create: "Crear Rol",
        breadcrumb_role_edit: "Editar Rol",
        breadcrumb_users_list: "Usuarios",
        breadcrumb_user_create: "Crear Usuario",
        breadcrumb_user_edit: "Editar Usuario",
        breadcrumb_customers_all: "Todos los Clientes",
        breadcrumb_customer_create: "Crear Cliente",
        breadcrumb_customer_edit: "Editar Cliente",
        breadcrumb_customer_details: "Detalles del Cliente",
        close_button: "Cerrar",
        // Latin American Countries
        country_ar: "Argentina",
        country_bo: "Bolivia",
        country_br: "Brasil",
        country_cl: "Chile",
        country_co: "Colombia",
        country_cr: "Costa Rica",
        country_cu: "Cuba",
        country_do: "República Dominicana",
        country_ec: "Ecuador",
        country_sv: "El Salvador",
        country_gt: "Guatemala",
        country_hn: "Honduras",
        country_mx: "México",
        country_ni: "Nicaragua",
        country_pa: "Panamá",
        country_py: "Paraguay",
        country_pe: "Perú",
        country_pr: "Puerto Rico",
        country_uy: "Uruguay",
        country_ve: "Venezuela",
        // Page titles
        create_customer_page_title: "Crear Cliente",
        edit_customer_page_title: "Editar Cliente",
        details_customer_page_title: "Detalles del Cliente",
        all_customers_title: "Todos los Clientes",
        // Tooltips
        tooltip_view_customer_details: "Ver Detalles del Cliente",
        tooltip_view_action: "Ver",
        tooltip_edit_action: "Editar",
        tooltip_deactivate_action: "Desactivar",
        // Countries Spanish
        select_country_option: "Seleccionar País",
        // Language selector
        language_english: "Inglés",
        language_spanish: "Español",
        // Instructors module
        coaches_menu: "Instructores",
        instructors_menu: "Instructores",
        instructor_profile_page_title: "Perfil del Instructor",
        // Breadcrumbs
        breadcrumb_instructors_profile: "Perfil del Instructor",
        // Pestañas
        tab_instructor_account: "Cuenta",
        tab_instructor_security: "Seguridad",
        tab_instructor_financial: "Financiero",
        tab_instructor_notifications: "Notificaciones",
        tab_instructor_features: "Características",
        // Tab titles
        tab_instructor_account_title: "Detalles de la Cuenta",
        tab_instructor_security_title: "Configuraciones de Seguridad",
        tab_instructor_financial_title: "Información de Pagos e Historial",
        tab_instructor_notifications_title: "Configuraciones de Notificación",
        tab_instructor_features_title: "Características y Experiencia del Instructor",
        // Buttons
        save_changes_button: "Guardar cambios",
        cancel_button: "Cancelar",
        // Form labels
        form_avatar_label: "Avatar",
        form_upload_button: "Subir nueva foto",
        form_reset_button: "Restablecer",
        form_avatar_allowed_formats: "Formatos permitidos JPG, GIF o PNG. Tamaño máx. de 800K",
        form_first_name_label: "Nombre",
        form_last_name_label: "Apellido",
        form_email_label: "Correo electrónico",
        form_organization_label: "Organización",
        form_phone_number_label: "Número de teléfono",
        form_address_label: "Dirección",
        form_state_label: "Estado/Provincia",
        form_zip_code_label: "Código postal",
        form_country_label: "País",
        form_language_label: "Idioma",
        form_timezone_label: "Zona horaria",
        form_currency_label: "Moneda",
        form_reset_button_alt: "Restablecer",
        // Select options
        select_timezone_option: "Seleccionar zona horaria",
        select_currency_option: "Seleccionar moneda",
        // Countries
        country_usa_option: "EE. UU.",
        country_ar_option: "Argentina",
        country_bo_option: "Bolivia",
        country_br_option: "Brasil",
        country_cl_option: "Chile",
        country_co_option: "Colombia",
        country_cr_option: "Costa Rica",
        country_cu_option: "Cuba",
        country_do_option: "República Dominicana",
        country_ec_option: "Ecuador",
        country_sv_option: "El Salvador",
        country_gt_option: "Guatemala",
        country_hn_option: "Honduras",
        country_mx_option: "México",
        country_ni_option: "Nicaragua",
        country_pa_option: "Panamá",
        country_py_option: "Paraguay",
        country_pe_option: "Perú",
        country_pr_option: "Puerto Rico",
        country_uy_option: "Uruguay",
        country_ve_option: "Venezuela",
        // Languages
        language_english_option: "Inglés",
        language_spanish_option: "Español",
        language_french_option: "Francés",
        language_german_option: "Alemán",
        language_portuguese_option: "Portugués",
        language_italian_option: "Italiano",
        // Currencies
        currency_usd_option: "USD",
        currency_eur_option: "EUR",
        currency_gbp_option: "GBP",
        currency_ars_option: "ARS - Peso argentino",
        currency_brl_option: "BRL - Real brasileño",
        currency_clp_option: "CLP - Peso chileno",
        currency_cop_option: "COP - Peso colombiano",
        currency_mxn_option: "MXN - Peso mexicano",
        currency_pen_option: "PEN - Sol peruano",
        // Delete account section
        delete_account_section_title: "Eliminar cuenta",
        delete_account_warning: "Una vez que elimines tu cuenta, no hay vuelta atrás. Por favor ten certeza.",
        confirm_account_deactivation_label: "Confirmo la desactivación de mi cuenta",
        deactivate_account_button: "Desactivar cuenta",
        // Security tab
        security_change_password_title: "Cambiar contraseña",
        security_current_password_label: "Contraseña actual",
        security_new_password_label: "Nueva contraseña",
        security_confirm_new_password_label: "Confirmar nueva contraseña",
        security_password_requirements_title: "Requisitos de la contraseña:",
        security_req_min_chars: "Mínimo 8 caracteres - cuantos más, mejor",
        security_req_lowercase: "Al menos una letra minúscula",
        security_req_number_symbol: "Al menos un número, símbolo o espacio en blanco",
        security_2fa_title: "Verificación en dos pasos",
        security_2fa_description: "La autenticación de dos factores añade una capa adicional de seguridad a tu cuenta requiriendo más que solo una contraseña para iniciar sesión.",
        learn_more_link: "Saber más.",
        security_2fa_not_enabled_title: "La autenticación de dos factores aún no está habilitada.",
        security_2fa_not_enabled_text: "La autenticación de dos factores añade una capa adicional de seguridad a tu cuenta requiriendo más que solo una contraseña para iniciar sesión.",
        security_enable_2fa_button: "Habilitar Autenticación de Dos Factores",
        security_create_api_key_title: "Crear una clave API",
        security_create_api_key_description: "Las claves API permiten a aplicaciones de terceros acceder a los datos de tu cuenta sin necesidad de tu contraseña.",
        security_choose_key_type_label: "Elige el tipo de clave API que quieres crear",
        select_option_default: "Seleccionar Opción",
        api_key_type_full: "Acceso Completo",
        api_key_type_read: "Solo Lectura",
        security_name_api_key_label: "Nombra la clave API",
        security_create_key_button: "Crear Clave",
        security_api_key_list_title: "Lista de Claves API y Acceso",
        th_api_server_key: "Clave del Servidor",
        th_api_created_on: "Creada el",
        th_api_actions: "Acciones",
        api_key_full_access: "Acceso Completo",
        api_key_read_only: "Solo Lectura",
        security_recent_devices_title: "Dispositivos Recientes",
        th_device_browser: "NAVEGADOR",
        th_device_device: "DISPOSITIVO",
        th_device_location: "UBICACIÓN",
        th_device_recent_activity: "ACTIVIDAD RECIENTE",
        // Financial Tab
        tab_instructor_financial_title: "Información e Historial de Pagos",
        financial_payment_method_title: "Método de Pago Preferido",
        financial_payment_method_label: "Método de Pago",
        financial_payment_method_select_option: "Seleccionar método de pago",
        financial_payment_method_paypal: "PayPal",
        financial_payment_method_bank_transfer: "Transferencia Bancaria",
        financial_paypal_email_label: "Correo de PayPal",
        financial_paypal_email_placeholder: "Ingresa tu email de PayPal",
        financial_bank_account_holder_label: "Nombre del Titular de la Cuenta",
        financial_bank_account_holder_placeholder: "Ingresa el nombre del titular",
        financial_bank_account_number_label: "Número de Cuenta / IBAN",
        financial_bank_account_number_placeholder: "Ingresa el número de cuenta",
        financial_bank_routing_number_label: "Número de Ruta",
        financial_bank_routing_number_placeholder: "Ingresa el número de ruta",
        financial_bank_swift_code_label: "Código SWIFT / BIC",
        financial_bank_swift_code_placeholder: "Ingresa el código SWIFT/BIC",
        financial_payment_frequency_title: "Frecuencia de Pago",
        financial_payment_frequency_label: "Frecuencia de Pago",
        financial_frequency_weekly: "Semanal",
        financial_frequency_biweekly: "Quincenal",
        financial_frequency_monthly: "Mensual",
        financial_minimum_threshold_label: "Umbral Mínimo de Pago (USD)",
        financial_minimum_threshold_placeholder: "Ingresa el monto mínimo",
        financial_save_payment_info_button: "Guardar Información de Pago",
        financial_payment_history_title: "Historial de Pagos",
        th_payment_date: "FECHA",
        th_payment_amount: "MONTO",
        th_payment_method: "MÉTODO",
        th_payment_status: "ESTADO",
        th_payment_actions: "ACCIONES",
        payment_status_completed: "Completado",
        payment_status_pending: "Pendiente",
        payment_status_failed: "Fallido",
        financial_view_invoice_tooltip: "Ver Factura",
        financial_download_invoice_tooltip: "Descargar Factura",
        // Additional Financial Tab keys from prompt
        financial_select_payment_method_label: "Seleccionar Método",
        select_option_default: "Seleccionar un método",
        payment_method_paypal: "PayPal",
        payment_method_bank_transfer: "Transferencia Bancaria",
        financial_bank_holder_name_label: "Nombre del Titular de la Cuenta",
        financial_bank_name_label: "Nombre del Banco",
        financial_bank_address_label: "Dirección del Banco",
        frequency_monthly: "Mensual",
        frequency_biweekly: "Quincenal",
        frequency_weekly: "Semanal",
        save_payment_settings_button: "Guardar Configuración de Pago",
        cancel_button: "Cancelar",
        th_payment_id: "ID",
        th_payment_invoice: "FACTURA",
        showing_3_of_x_payments: "Mostrando 3 de X pagos",
        // Payment status for table
        payment_status_paid: "Pagado",
        payment_status_processing: "Procesando",
        // Notifications Tab
        tab_instructor_notifications_title: "Configuración de Notificaciones",
        notifications_receive_for_items_label: "Recibirás notificaciones para los siguientes elementos seleccionados.",
        th_notifications_type: "TIPO",
        th_notifications_email: "CORREO",
        th_notifications_browser: "NAVEGADOR",
        th_notifications_app: "APLICACIÓN",
        // Notification Types
        notification_type_new_for_you: "Novedades para ti",
        notification_type_account_activity: "Actividad de la cuenta",
        notification_type_new_browser_login: "Nuevo navegador utilizado para iniciar sesión",
        notification_type_new_device_linked: "Nuevo dispositivo vinculado",
        notifications_when_to_send_label: "¿Cuándo debemos enviarte notificaciones?",
        notify_only_when_online: "Solo cuando estoy en línea",
        notify_anytime: "En cualquier momento",
        notify_daily_digest: "Resumen Diario",
        discard_button: "Descartar",
        // Features Tab
        tab_instructor_features_title: "Características y Experiencia del Instructor",
        features_title_profession_label: "Título / Profesión",
        features_years_experience_label: "Años de Experiencia",
        features_specialties_label: "Especialidades",
        features_specialties_note: "Selecciona múltiples. Si una especialidad no está en la lista, escríbela para añadirla (requiere plugin JS).",
        manage_specialties_button: "Gestionar Especialidades",
        manage_specialties_modal_title: "Gestionar Especialidades",
        manage_specialties_modal_text: "Aquí se listarían las especialidades existentes con opciones de edición/eliminación, y un formulario para añadir una nueva.",
        close_button: "Cerrar",
        // Specialty examples
        specialty_programming: "Programación",
        specialty_design: "Diseño",
        specialty_marketing: "Marketing",
        specialty_data_science: "Ciencia de Datos",
        features_education_level_label: "Nivel de Educación",
        education_high_school: "Bachillerato / Equivalente",
        education_technical_degree: "Título Técnico / Asociado",
        education_bachelors: "Licenciatura",
        education_masters: "Maestría",
        education_doctorate: "Doctorado (PhD, EdD, etc.)",
        education_professional: "Título Profesional (MD, JD, etc.)",
        education_other: "Otro",
        features_institution_label: "Institución de Estudios",
        features_certifications_label: "Certificaciones",
        features_portfolio_url_label: "URL del Portafolio",
        features_website_url_label: "URL del Sitio Web Personal",
        features_document_upload_label: "Subir CV / Diploma",
        features_document_upload_note: "Opcional. PDF, DOC, DOCX. Tamaño máx. 5MB.",
        // Instructors Index Page
        breadcrumb_instructors_list: "Lista de Instructores",
        breadcrumb_instructor_create: "Crear Instructor",
        create_instructor_page_title: "Crear Nuevo Instructor",
        instructors_list_page_title: "Lista de Instructores",
        add_new_instructor_button: "Añadir Nuevo Instructor",
        search_instructor_placeholder: "Buscar Instructor",
        th_instructor_name: "INSTRUCTOR",
        th_instructor_email: "CORREO",
        th_instructor_specialties: "ESPECIALIDADES",
        th_instructor_status: "ESTADO",
        th_instructor_actions: "ACCIONES",
        tooltip_view_profile: "Ver Perfil",
        tooltip_edit_profile: "Editar Perfil",
        tooltip_deactivate_instructor: "Desactivar Instructor",
        // Life Areas Module
        life_areas_menu: "Áreas de Vida",
        breadcrumb_life_areas_list: "Lista de Áreas de Vida",
        life_areas_page_title: "Áreas de Vida",
        search_category_placeholder: "Buscar Área de Vida",
        add_category_button: "Añadir Área de Vida",
        // Table headers
        th_life_area_categories: "ÁREA DE VIDA",
        th_life_area_total_products: "SUB-ELEMENTOS",
        th_life_area_total_earning: "PROGRESO",
        th_life_area_actions: "ACCIONES",
        // Offcanvas
        offcanvas_add_life_area_title: "Añadir Nueva Área de Vida",
        offcanvas_edit_life_area_title: "Editar Área de Vida",
        form_label_title: "Título",
        form_placeholder_title: "Ingresa el título del área de vida",
        form_label_attachment: "Adjunto",
        form_label_no_file_chosen: "Ningún archivo seleccionado",
        form_attachment_note: "Opcional. Icono pequeño o imagen representativa.",
        form_current_attachment: "Adjunto Actual:",
        form_label_description: "Descripción",
        form_placeholder_description: "Escribe una descripción...",
        form_label_status: "Seleccionar estado del área de vida",
        status_active: "Activo",
        status_inactive: "Inactivo",
        status_scheduled: "Programado",
        add_button: "Añadir",
        // Life Area Names
        life_area_spirituality: "Espiritualidad",
        life_area_growth_learning: "Crecimiento y aprendizaje",
        life_area_social_love: "Socios y amor",
        life_area_family_friends: "Familia y amigos",
        life_area_community: "Comunidad",
        life_area_environment: "Ambiente",
        life_area_work_mission: "Trabajo y misión de vida",
        life_area_economy_finance: "Economía y finanzas",
        life_area_health_body: "Salud y cuerpo",
        life_area_recreation_fun: "Recreación y diversión",
        // Life Area Descriptions
        description_spirituality: "Conecta con tu ser interior y valores",
        description_growth_learning: "Desarrollo personal y educación",
        description_social_love: "Relaciones románticas y parejas",
        description_family_friends: "Vínculos familiares y amistades",
        description_community: "Conexiones sociales y participación cívica",
        description_environment: "Tu entorno físico y la naturaleza",
        description_work_mission: "Carrera y propósito de vida",
        description_economy_finance: "Salud financiera y construcción de riqueza",
        description_health_body: "Bienestar físico y estado físico",
        description_recreation_fun: "Actividades de ocio y entretenimiento",
        // X-Ray of Life Module
        xray_life_menu: "Radiografía de Vida",
        breadcrumb_xray_life_index: "Radiografía de Vida",
        xray_life_page_title: "Radiografía de Vida",
        xray_life_main_title: "Radiografía de Vida",
        xray_life_main_subtitle: "Selecciona un Área de Vida o busca preguntas específicas.",
        search_articles_placeholder: "Buscar preguntas...",
        all_areas_filter: "Todas las Áreas",
        // Offcanvas
        offcanvas_add_xray_question_title: "Añadir Nueva Pregunta",
        offcanvas_edit_xray_question_title: "Editar Pregunta",
        form_label_life_area_category: "Área de Vida (Categoría)",
        select_life_area_placeholder: "Selecciona un Área de Vida",
        form_label_xray_question: "Pregunta",
        form_placeholder_xray_question: "Introduce el texto de la pregunta...",
        add_button: "Añadir",
        save_changes_button: "Guardar Cambios",
        add_new_question_button: "Añadir Nueva Pregunta",
        add_new_question_button_short: "Añadir",
        no_questions_found: "No se encontraron preguntas para esta área o búsqueda.",
        no_questions_found_filter: "Ninguna pregunta coincide con tu filtro o criterio de búsqueda actual.",
        edit_button: "Editar",
        deactivate_button: "Desactivar",
        // Sample Questions
        xray_q_spirit_1: "¿Cómo me conecto con mi lado espiritual?",
        xray_q_spirit_2: "¿Qué prácticas ayudan a desarrollar la paz interior?",
        xray_q_growth_1: "¿Cómo puedo establecer metas de aprendizaje efectivas?",
        xray_q_social_1: "¿Cómo construyo relaciones significativas?",
        xray_q_family_1: "¿Cómo mantener vínculos familiares fuertes?",
        // Sample Answers
        xray_a_spirit_1: "Considera la meditación, oración, o pasar tiempo en la naturaleza para conectar con tu ser espiritual.",
        xray_a_spirit_2: "La meditación regular, prácticas de atención plena y ejercicios de gratitud pueden ayudar a desarrollar la paz interior.",
        xray_a_growth_1: "Establece metas SMART, divídelas en pasos más pequeños y haz seguimiento de tu progreso regularmente.",
        xray_a_social_1: "Sé auténtico, escucha activamente, muestra empatía e invierte tiempo en nutrir las relaciones.",
        xray_a_family_1: "Programa tiempo familiar regular, comunícate abiertamente y crea tradiciones y recuerdos compartidos.",
        // Email Contents Module
        "email.emailContents": "Contenido de Correo",
        "email.compose": "Redactar",
        "email.folders": "Carpetas",
        "email.labels": "Etiquetas",
        "email.inbox": "Bandeja de entrada",
        "email.sent": "Enviados",
        "email.draft": "Borradores",
        "email.starred": "Destacados",
        "email.spam": "Correo no deseado",
        "email.trash": "Papelera",
        "email.labels.personal": "Personal",
        "email.labels.company": "Empresa",
        "email.labels.important": "Importante",
        "email.labels.private": "Privado",
        "email.searchMail": "Buscar correo...",
        "email.refresh": "Actualizar",
        "email.markUnread": "Marcar como no leído",
        "email.delete": "Eliminar",
        "email.moveToFolder": "Mover a carpeta",
        "email.applyLabel": "Aplicar etiqueta",
        "email.newMessage": "Nuevo Mensaje",
        "email.to": "Para",
        "email.cc": "CC",
        "email.bcc": "BCC",
        "email.subject": "Asunto",
        "email.message": "Mensaje",
        "email.attachments": "Adjuntos",
        "email.attachFile": "Adjuntar archivo",
        "email.maxFileSize": "Máximo 10MB por archivo",
        "email.send": "Enviar",
        "email.saveDraft": "Guardar borrador",
        "email.discard": "Descartar",
        "email.reply": "Responder",
        "email.replyAll": "Responder a todos",
        "email.forward": "Reenviar",
        "email.toPlaceholder": "destinatario@ejemplo.com",
        "email.ccPlaceholder": "cc@ejemplo.com",
        "email.bccPlaceholder": "bcc@ejemplo.com",
        "email.subjectPlaceholder": "Escriba el asunto aquí...",
        "email.messagePlaceholder": "Escriba su mensaje aquí...",
        "email.errorLoadingEmails": "Error al cargar los correos",
        "email.errorLoadingDetail": "Error al cargar el detalle del correo",
        "email.errorSending": "Error al enviar correo",
        "email.errorSavingDraft": "Error al guardar borrador",
        "email.errorDeleting": "Error al eliminar correos",
        "email.errorMoving": "Error al mover correos",
        "email.errorApplyingLabel": "Error al aplicar etiqueta",
        "email.noEmailsSelected": "No hay correos seleccionados",
        "email.confirmDelete": "¿Está seguro de que desea eliminar los correos seleccionados?",
        "email.refreshed": "Lista actualizada",
        "email.markedAsUnread": "Marcados como no leídos",
        "validation.required": "Este campo es requerido",
        // Toolbox Academy Module
        "page_titles.toolboxAcademy": "Academia Toolbox",
        "academy.hero.title": "Academia Toolbox",
        "academy.hero.subtitle": "Aprende las mejores prácticas y técnicas con nuestros videos educativos",
        "academy.hero.description": "Desde desarrollo web hasta estrategia de negocio, tenemos contenido de calidad para tu crecimiento profesional.",
        "academy.stats.videosTotal": "Videos Totales",
        "academy.stats.categoriesTotal": "Categorías",
        "academy.stats.viewsTotal": "Visualizaciones",
        "academy.stats.videos": "Videos",
        "academy.stats.featured": "Destacados",
        "academy.stats.authors": "Autores",
        "academy.stats.views": "Vistas",
        "academy.stats.title": "Estadísticas",
        "academy.featured.title": "Videos Destacados",
        "academy.featured.description": "Contenido recomendado para ti",
        "academy.search.placeholder": "Buscar videos, categorías o temas...",
        "academy.filtering.category": "Mostrando videos de:",
        "academy.filtering.search": "Búsqueda:",
        "academy.filtering.clearAll": "Limpiar filtros",
        "academy.videoUnavailable": "Video no disponible",
        "academy.noVideos.title": "No se encontraron videos",
        "academy.noVideos.description": "No hay videos disponibles para los criterios de búsqueda actuales.",
        "academy.noVideos.showAll": "Ver todos los videos",
        "academy.loading": "Cargando videos...",
        "academy.views": "visualizaciones",
        "academy.categories.all": "Todas las Categorías",
        "academy.categories.webDevelopment": "Desarrollo Web",
        "academy.categories.projectManagement": "Gestión de Proyectos",
        "academy.categories.designUiUx": "Diseño & UI/UX",
        "academy.categories.marketingGrowth": "Marketing & Crecimiento",
        "academy.categories.businessStrategy": "Estrategia de Negocio",
        "academy.categories.other": "Otros",
        "academy.share.title": "Compartir Video",
        "academy.relatedVideos.title": "Videos Relacionados",
        "academy.relatedVideos.viewAll": "Ver Todos en",
        "academy.backToAcademy.title": "¿Listo para Más?",
        "academy.backToAcademy.description": "Explora más videos en nuestra academia y continúa aprendiendo.",
        "buttons.backToAcademy": "Volver a Academia",
        "buttons.watchVideo": "Ver Video",
        "buttons.search": "Buscar",
        "labels.categories": "Categorías",
        "labels.temas": "Temas",
        "labels.featured": "Destacado",
        "labels.author": "Autor",
        "labels.publishDate": "Fecha de Publicación",
        "labels.tags": "Etiquetas",
        "breadcrumbs.dashboard": "Tablero",
        "breadcrumbs.academy": "Academia Toolbox",
        // Life Assessment Questionnaires Module
        "sidebar.lifeAssessmentQuestionnaires": "Cuestionarios de Evaluación de Vida",
        "sidebar.questionnaireTemplates": "Cuestionarios",
        
        // Módulo de Plantillas de Cuestionarios
        "questionnaire_templates.page_title": "Plantillas de Cuestionarios",
        "questionnaire_templates.create_template": "Crear Plantilla",
        "questionnaire_templates.edit_template": "Editar Plantilla",
        "questionnaire_templates.list_title": "Mis Plantillas",
        "questionnaire_templates.template_details": "Detalles de la Plantilla",
        "questionnaire_templates.table.title": "Título",
        "questionnaire_templates.table.description": "Descripción",
        "questionnaire_templates.table.questions_count": "Preguntas",
        "questionnaire_templates.table.status": "Estado",
        "questionnaire_templates.table.created_at": "Creada",
        "questionnaire_templates.table.actions": "Acciones",
        "questionnaire_templates.form.title": "Título de la Plantilla",
        "questionnaire_templates.form.title_placeholder": "Ej: Evaluación de Satisfacción del Cliente",
        "questionnaire_templates.form.description": "Descripción (Opcional)",
        "questionnaire_templates.form.description_placeholder": "Describe el propósito y contexto de este cuestionario...",
        "questionnaire_templates.form.description_help": "Esta descripción se mostrará a los usuarios cuando respondan el cuestionario.",
        "questionnaire_templates.form.create_and_add_questions": "Crear y Agregar Preguntas",
        "questionnaire_templates.help.title": "¿Qué puedes hacer después?",
        "questionnaire_templates.help.step1": "Después de crear la plantilla, podrás agregar diferentes tipos de preguntas",
        "questionnaire_templates.help.step2": "Organizar el orden de las preguntas arrastrando y soltando",
        "questionnaire_templates.help.step3": "Asignar la plantilla completa a usuarios específicos para que respondan",
        "questionnaire_templates.stats.title": "Estadísticas",
        "questionnaire_templates.stats.questions": "Preguntas",
        "questionnaire_templates.stats.required": "Obligatorias",
        "questionnaire_templates.questions_manager.title": "Constructor de Preguntas",
        "questionnaire_templates.questions_manager.add_question": "Agregar Pregunta",
        "questionnaire_templates.questions_manager.add_first_question": "Agregar Primera Pregunta",
        "questionnaire_templates.questions_manager.no_questions": "No hay preguntas todavía",
        "questionnaire_templates.questions_manager.no_questions_help": "Comienza agregando tu primera pregunta para construir el cuestionario.",
        "questionnaire_templates.question_modal.title": "Agregar Pregunta",
        "questionnaire_templates.question_modal.question_text": "Texto de la Pregunta",
        "questionnaire_templates.question_modal.question_text_placeholder": "Ej: ¿Qué tan satisfecho estás con el servicio?",
        "questionnaire_templates.question_modal.question_type": "Tipo de Pregunta",
        "questionnaire_templates.question_modal.options": "Opciones de Respuesta",
        "questionnaire_templates.question_modal.add_option": "Agregar Opción",
        "questionnaire_templates.question_modal.likert_options": "Escala Likert",
        "questionnaire_templates.question_modal.add_likert_option": "Agregar Opción",
        "questionnaire_templates.question_modal.is_required": "Esta pregunta es obligatoria",
        "questionnaire_templates.question_modal.save_question": "Guardar Pregunta",
        "questionnaire_templates.question_types.single_choice": "Opción Múltiple (Una respuesta)",
        "questionnaire_templates.question_types.multiple_choice": "Opción Múltiple (Múltiples respuestas)",
        "questionnaire_templates.question_types.short_text": "Respuesta Corta",
        "questionnaire_templates.question_types.long_text": "Respuesta Larga",
        "questionnaire_templates.question_types.likert_scale": "Escala Likert",
        "questionnaire_templates.question_types.true_false": "Verdadero/Falso",
        // Assignment translations
        "assign_questionnaire": "Asignar Cuestionario",
        "questionnaire_details": "Detalles del Cuestionario",
        "questionnaire_title": "Título del Cuestionario",
        "select_clients": "Seleccionar Clientes",
        "select_clients_help": "Seleccione uno o más clientes para asignar este cuestionario. Use Ctrl+Click para selección múltiple.",
        "already_assigned": "Ya Asignados",
        "assign": "Asignar",
        "assignment_info": "Información de Asignación",
        "assignment_info_text": "Al asignar este cuestionario:",
        "assignment_benefit_1": "Los clientes recibirán una notificación",
        "assignment_benefit_2": "Podrán completar el cuestionario en su panel",
        "assignment_benefit_3": "Podrás ver las respuestas una vez completadas",
        "assigned_questionnaires": "Cuestionarios Asignados",
        "assign_more": "Asignar a más clientes",
        "back": "Volver",
        "client": "Cliente",
        "assigned_date": "Fecha de Asignación",
        "started_date": "Fecha de Inicio",
        "completed_date": "Fecha de Completado",
        "progress": "Progreso",
        "view_responses": "Ver Respuestas",
        "export_pdf": "Exportar PDF",
        "cancel_assignment": "Cancelar Asignación",
        "no_assignments": "No hay asignaciones para este cuestionario.",
        "assign_first": "Asignar a clientes",
        "page_titles.lifeAssessment": "Cuestionarios de Evaluación de Vida",
        "breadcrumbs.lifeAssessment": "Cuestionarios de Evaluación de Vida",
        "labels.filterByCategory": "Filtrar por Categoría",
        "labels.question": "Pregunta",
        "labels.questions": "Preguntas de Evaluación",
        "labels.selectAnswer": "Seleccionar Respuesta",
        "placeholders.selectArea": "Seleccione un Área...",
        "buttons.submitAnswers": "Enviar Respuestas",
        "messages.noQuestionsFound": "No se encontraron preguntas para esta área.",
        "messages.answersSubmittedSuccess": "¡Respuestas enviadas con éxito!",
        "messages.selectAreaToStart": "Seleccione un área para comenzar su evaluación",
        "messages.selectAreaDescription": "Elija un área de vida del menú desplegable arriba para ver las preguntas relacionadas y comenzar su evaluación.",
        // Wheel of Life Module
        "sidebar.wheelOfLife": "Rueda de la Vida",
        "page_titles.wheelOfLife": "Rueda de la Vida",
        "breadcrumbs.wheelOfLife": "Rueda de la Vida",
        "labels.selectYourScore": "Selecciona tu puntuación abajo:",
        "labels.totalScore": "Puntuación Total:",
        "labels.average": "Promedio",
        "labels.areas": "Áreas",
        "labels.score": "Puntuación",
        "buttons.saveScores": "Guardar Puntuaciones",
        "wheelOfLife.chartTitle": "Tu Rueda de Equilibrio de Vida",
        "wheelOfLife.subtitle": "Visualiza y equilibra las diferentes áreas de tu vida",
        // Wheel of Progress Module
        "sidebar.wheelOfProgress": "Rueda del Progreso",
        "page_titles.wheelOfProgress": "Rueda del Progreso",
        "breadcrumbs.wheelOfProgress": "Rueda del Progreso",
        "wheelOfProgress.subtitle": "Establece metas, registra tu progreso y planifica tus siguientes acciones",
        "wheelOfProgress.chartTitle": "Tu Rueda del Progreso",
        "wheelOfProgress.progressTableTitle": "Gestión de Metas y Progreso",
        "wheelOfProgress.globalProgress": "Progreso Global",
        "wheelOfProgress.totalCategories": "Total Categorías",
        "wheelOfProgress.categoriesWithGoals": "Con Metas",
        "wheelOfProgress.lastUpdated": "Última Actualización",
        "wheelOfProgress.globalLabel": "Global",
        "wheelOfProgress.autoSaveNote": "Los cambios se guardan automáticamente",
        "wheelOfProgress.saveNote": "Haz clic en el botón para guardar los cambios",
        "table_headers.area": "Área",
        "table_headers.categories": "Categorías",
        "table_headers.goal": "Meta",
        "table_headers.percentage": "%",
        "table_headers.nextAction": "Siguiente Acción",
        "table_headers.deadline": "Fecha Límite",
        "buttons.saveProgress": "Guardar Progreso",
        // Categories Names in Spanish
        "categories.exercise-fitness": "Ejercicio y Fitness",
        "categories.nutrition-diet": "Nutrición y Dieta",
        "categories.stress-management": "Manejo del Estrés",
        "categories.work-life-balance": "Equilibrio Trabajo-Vida",
        "categories.professional-development": "Desarrollo Profesional",
        "categories.networking": "Networking",
        "categories.emergency-fund": "Fondo de Emergencia",
        "categories.investment-portfolio": "Portafolio de Inversión",
        "categories.family-time": "Tiempo en Familia",
        "categories.friendships": "Amistades",
        "categories.learning-education": "Aprendizaje y Educación",
        "categories.skill-development": "Desarrollo de Habilidades",
        // Tasks Module
        "sidebar.tasks": "Tareas",
        "page_titles.tasks": "Gestión de Tareas",
        "breadcrumbs.tasks": "Tareas",
        "tasks.subtitle": "Organiza y prioriza tus tareas usando la Matriz de Eisenhower",
        "tasks.eisenhowerMatrix": "Matriz de Eisenhower",
        "tasks.taskManagement": "Gestión de Tareas",
        "tasks.totalTasks": "Total de Tareas",
        "tasks.pendingTasks": "Pendientes",
        "tasks.completedTasks": "Completadas",
        "tasks.completionRate": "Tasa de Finalización",
        "tasks.allTasks": "Todas las Tareas",
        "quadrants.urgentImportant": "Urgente e Importante",
        "quadrants.notUrgentImportant": "No Urgente e Importante",
        "quadrants.urgentNotImportant": "Urgente y No Importante",
        "quadrants.notUrgentNotImportant": "No Urgente y No Importante",
        "quadrants.doAction": "Hacer",
        "quadrants.scheduleAction": "Programar",
        "quadrants.delegateAction": "Delegar",
        "quadrants.eliminateAction": "Eliminar",
        "labels.taskDescription": "Descripción de la Tarea",
        "labels.urgent": "Urgente",
        "labels.important": "Importante",
        "buttons.addTask": "Añadir Tarea",
        "buttons.editTask": "Editar Tarea",
        "buttons.delete": "Borrar",
        "buttons.saveChanges": "Guardar Cambios",
        "buttons.cancel": "Cancelar",
        "messages.noTasks": "¡Aún no hay tareas. Añade una arriba!",
        // Life Areas Names in Spanish
        "areas.physical-health": "Salud Física",
        "areas.mental-health": "Salud Mental",
        "areas.career-work": "Carrera y Trabajo",
        "areas.financial-wellness": "Bienestar Financiero",
        "areas.relationships": "Relaciones",
        "areas.personal-growth": "Crecimiento Personal",
        "areas.spiritual-life": "Vida Espiritual",
        "areas.recreation-fun": "Recreación y Diversión",
        // Life Areas Descriptions in Spanish
        "areas.physical-health.description": "Tu bienestar físico, estado físico y hábitos de salud",
        "areas.mental-health.description": "Tu bienestar mental, manejo del estrés y salud emocional",
        "areas.career-work.description": "Tu vida profesional, satisfacción laboral y equilibrio trabajo-vida",
        "areas.financial-wellness.description": "Tu salud financiera, planificación y gestión del dinero",
        "areas.relationships.description": "Tus relaciones con familia, amigos y parejas románticas",
        "areas.personal-growth.description": "Tu desarrollo personal, aprendizaje y automejora",
        "areas.spiritual-life.description": "Tu bienestar espiritual, propósito y paz interior",
        "areas.recreation-fun.description": "Tus pasatiempos, actividades de ocio y equilibrio trabajo-vida",
        // Habit Tracker Module
        "sidebar.habitTracker": "Rastreador de Hábitos",
        "habitTracker.title": "Rastreador de Hábitos",
        "habitTracker.addHabit": "Añadir Hábito",
        "habitTracker.tabs.habitLog": "Registro de Hábitos",
        "habitTracker.tabs.progressChart": "Gráfico de Progreso",
        "habitTracker.habitLog": "Registro de Hábitos",
        "headings.progressSummary": "Resumen de Progreso",
        "habitTracker.currentWeek": "Semana Actual",
        "habitTracker.table.habit": "Hábito",
        "habitTracker.table.daysMet": "Días Cumplidos",
        "habitTracker.table.percentMet": "% Cumplido",
        "habitTracker.table.actions": "Acciones",
        "habitTracker.overallSuccessRate": "Tasa de Éxito General:",
        "habitTracker.saveLog": "Guardar Registro",
        "breadcrumbs.habitTracker": "Rastreador de Hábitos",
        "habitTracker.noHabits.title": "Aún no hay hábitos",
        "habitTracker.noHabits.description": "¡Comienza a construir mejores hábitos añadiendo tu primero!",
        "habitTracker.addFirstHabit": "Añadir Tu Primer Hábito",
        "habitTracker.progressOverview": "Resumen de Progreso",
        "habitTracker.periods.last7Days": "Últimos 7 Días",
        "habitTracker.periods.last30Days": "Últimos 30 Días",
        "habitTracker.periods.thisMonth": "Este Mes",
        "habitTracker.periods.allTime": "Todo el Tiempo",
        "habitTracker.stats.title": "Estadísticas",
        "habitTracker.stats.totalHabits": "Hábitos Totales",
        "habitTracker.stats.avgCompletion": "Compleción Promedio",
        "habitTracker.stats.bestStreak": "Mejor Racha",
        "habitTracker.stats.activeStreaks": "Rachas Activas",
        "habitTracker.noData.title": "No hay datos para mostrar",
        "habitTracker.noData.description": "¡Añade algunos hábitos y comienza a rastrear para ver tus gráficos de progreso!",
        "habitTracker.modal.title": "Añadir Nuevo Hábito",
        "habitTracker.modal.name": "Nombre del Hábito",
        "habitTracker.modal.nameHelp": "¿Qué hábito quieres rastrear?",
        "habitTracker.modal.description": "Descripción",
        "habitTracker.modal.descriptionHelp": "Opcional: Añade más detalles sobre tu hábito",
        "habitTracker.modal.color": "Color",
        "habitTracker.modal.colorHelp": "Elige un color para representar este hábito",
        "habitTracker.modal.category": "Categoría",
        "habitTracker.modal.frequency": "Frecuencia",
        "habitTracker.modal.customDays": "Seleccionar Días",
        "habitTracker.modal.reminder": "Habilitar recordatorios diarios",
        "habitTracker.modal.reminderHelp": "Recibe notificaciones para mantener tu racha de hábitos",
        "habitTracker.modal.addHabit": "Añadir Hábito",
        "habitTracker.categories.health": "Salud",
        "habitTracker.categories.fitness": "Ejercicio",
        "habitTracker.categories.productivity": "Productividad",
        "habitTracker.categories.learning": "Aprendizaje",
        "habitTracker.categories.social": "Social",
        "habitTracker.categories.creative": "Creativo",
        "habitTracker.categories.mindfulness": "Atención Plena",
        "habitTracker.categories.other": "Otro",
        "habitTracker.frequency.daily": "Diario",
        "habitTracker.frequency.weekdays": "Días de Semana",
        "habitTracker.frequency.weekends": "Fines de Semana",
        "habitTracker.frequency.custom": "Personalizado",
        // Calendar Module
        "sidebar.calendar": "Calendario",
        "page_titles.calendar": "Calendario",
        "breadcrumbs.calendar": "Calendario", 
        "calendar.subtitle": "Programa y gestiona tus sesiones de coaching",
        "days.monday": "Lunes",
        "days.tuesday": "Martes",
        "days.wednesday": "Miércoles",
        "days.thursday": "Jueves",
        "days.friday": "Viernes",
        "days.saturday": "Sábado",
        "days.sunday": "Domingo",
        "days.mon": "Lun",
        "days.tue": "Mar", 
        "days.wed": "Mié",
        "days.thu": "Jue",
        "days.fri": "Vie",
        "days.sat": "Sáb",
        "days.sun": "Dom",
        "common.cancel": "Cancelar",
        "common.search_placeholder": "Buscar...",
        "common.filter_active": "Activos",
        "common.filter_inactive": "Inactivos",
        "common.filter_all": "Todos",
        "common.page": "Página",
        "common.back": "Volver",
        "common.save_changes": "Guardar Cambios",
        // Calendar specific translations
        "breadcrumb_calendar": "Calendario",
        "calendar.title": "Calendario",
        "calendar.addSession": "Agregar Evento",
        "calendar.filters": "Filtros de Eventos",
        "calendar.viewAll": "Ver Todos",
        "calendar.month": "Mes",
        "calendar.week": "Semana",
        "calendar.day": "Día",
        "calendar.list": "Lista",
        "calendar.sessionTitle": "Título",
        "calendar.status": "Estado",
        "calendar.startDateTime": "Fecha y Hora de Inicio",
        "calendar.endDateTime": "Fecha y Hora de Fin",
        "calendar.allDay": "Todo el Día",
        "calendar.coach": "Coach",
        "calendar.participants": "Participantes",
        "calendar.inviteUsers": "Invitar Usuarios",
        "calendar.location": "Ubicación",
        "calendar.description": "Descripción",
        "calendar.sessionType": "Tipo de Sesión",
        // Dashboard Permissions
        "dashboard_permissions": "Permisos del Dashboard",
        "dashboard_permissions_message": "Controla qué componentes del dashboard puede ver este rol.",
        "dashboard_visibility": "Visibilidad del Dashboard",
        "can_view_welcome_message": "Puede Ver Mensaje de Bienvenida",
        "can_view_welcome_video": "Puede Ver Video de Bienvenida",
        "can_view_total_clients_card": "Puede Ver Tarjeta Total de Clientes",
        "can_view_active_clients_card": "Puede Ver Tarjeta de Clientes Activos",
        // Customer form missing translations
        "form_customer_avatar": "Avatar del Cliente",
        "form_phone": "Teléfono",
        "optional_fields_title": "Información Opcional",
        "form_company": "Empresa",
        "form_status_detail": "Estado",
        "btn_cancel": "Cancelar",
        "btn_create_customer": "Crear Cliente",
        "help_title": "Ayuda",
        "required_fields_title": "Información Requerida",
        "form_first_name": "Nombre",
        "form_last_name": "Apellido",
        "form_select_country": "Seleccionar País",
        "customer_information_title": "Información del Cliente",
        "avatar_help_text": "JPG, GIF o PNG permitidos. Tamaño máximo de 800K",
        // Welcome Message module translations
        "video-configuration": "Configuración de Video",
        "video-type": "Tipo de Video",
        "no-video": "Sin Video",
        "upload-video": "Subir Video",
        "youtube-video": "Video de YouTube",
        "vimeo-video": "Video de Vimeo",
        "select-video": "Seleccionar Video",
        "upload-info": "Subir archivo MP4 (máx 100MB)",
        "youtube-url": "URL de YouTube",
        "vimeo-url": "URL de Vimeo",
        "save-changes": "Guardar Cambios",
        "update-preview": "Actualizar Vista Previa",
        "preview": "Vista Previa",
        "content-configuration": "Configuración de Contenido",
        "title": "Título",
        // Topics module translations
        "topics_list_title": "Lista de Temas",
        "filter_status": "Filtrar por Estado",
        "filter_all": "Todos",
        "filter_active": "Activos",
        "filter_inactive": "Inactivos",
        "add_new_topic_button": "Agregar Nuevo Tema",
        // Website Settings module translations
        "labels.defaultLanguage": "Idioma Predeterminado",
        "help.defaultLanguage": "Este será tu idioma preferido para la interfaz. Puedes cambiarlo temporalmente usando el selector de idioma.",
        // User form translations
        "form_default_language": "Idioma Predeterminado",
        "form_default_language_help": "Este será el idioma preferido del usuario para la interfaz.",
        // Sessions Module
        "sidebar.sessions": "Sesiones",
        "breadcrumb_sessions_list": "Sesiones",
        "breadcrumb_new_session": "Nueva Sesión",
        "breadcrumb_edit_session": "Editar Sesión",
        "sessions_title": "Sesiones",
        "search_session_placeholder": "Buscar sesión...",
        "add_new_session_button": "Añadir Nueva Sesión",
        "table_client": "Cliente",
        "table_session_date": "Fecha Sesión",
        "table_next_session_date": "Próxima Sesión",
        "table_ways_to_develop": "Formas de Desarrollar",
        "table_assignments": "Tareas",
        "table_challenges": "Desafíos",
        "table_key_points": "Puntos Clave",
        "table_status": "Estado",
        "table_actions": "Acciones",
        "status_active": "Activo",
        "status_inactive": "Inactivo",
        "status_filter_label": "Filtrar por Estado",
        "action_edit": "Editar",
        "action_deactivate": "Desactivar",
        "action_activate": "Activar",
        "no_sessions_found": "No se encontraron sesiones",
        "no_permissions": "Sin permisos",
        // Sessions Form
        "main_information_title": "Información Principal",
        "field_client": "Cliente",
        "select_client_placeholder": "Seleccione un cliente",
        "field_session_datetime": "Fecha y hora de la sesión",
        "field_next_session_datetime": "Fecha y hora de la próxima sesión",
        "session_content_title": "Contenido de la Sesión",
        "field_key_points": "Puntos clave",
        "field_ways_to_develop": "Formas de desarrollar",
        "field_assignments": "Asignaciones",
        "field_challenges": "Desafíos",
        "feedback_others_title": "Retroalimentación y Otros",
        "field_feedback": "Retroalimentación",
        "field_twitter": "Twitter",
        "attachments_title": "Archivos Adjuntos / Materiales",
        "existing_attachments_title": "Archivos Adjuntos Existentes",
        "new_attachments_title": "Agregar Nuevos Archivos",
        "drop_files_here": "Arrastra archivos aquí o haz clic para seleccionar",
        "file_upload_hint": "JPG, PNG, PDF, DOC, DOCX, MP4, MOV (Max: 10MB)",
        "selected_files": "Archivos seleccionados:",
        "no_existing_attachments": "No hay archivos adjuntos",
        "save_button": "Guardar",
        "update_button": "Actualizar",
        "cancel_button": "Cancelar",
        "notification_title": "Notificación",
        "validation_errors_title": "Errores de validación:",
        "confirm_delete_file": "¿Estás seguro de que deseas eliminar este archivo?",
        "required_fields_note": "Campos obligatorios",
        // Módulo de Notificaciones
        "sidebar.notifications": "Notificaciones",
        "sidebar.notificationPreferences": "Preferencias de Notificaciones",
        "sidebar.clientQuizzes": "Mis Cuestionarios",
        "sidebar.lifeEvents": "Línea de la Vida",
        "sidebar.communicationWheelTemplates": "Plantillas de Rueda de Comunicación",
        "sidebar.coachClientWheels": "Ruedas de Comunicación de Clientes",
        "sidebar.clientCommunicationWheels": "Mis Ruedas de Comunicación",
        // Módulo de Rueda de Comunicación
        "communication_wheel_templates_title": "Plantillas de Rueda de Comunicación",
        "communication_wheel_edit_template": "Editar Plantilla",
        "communication_wheel_template_details": "Detalles de la Plantilla",
        "communication_wheel_template_name": "Nombre de la Plantilla",
        "communication_wheel_template_description": "Descripción",
        "communication_wheel_dimensions_title": "Dimensiones de Comunicación",
        "communication_wheel_add_dimension": "Agregar Dimensión",
        "communication_wheel_dimensions_info": "Arrastra las dimensiones para reordenarlas. El orden afecta cómo se muestran en el gráfico.",
        "communication_wheel_no_dimensions": "Esta plantilla aún no tiene dimensiones definidas.",
        "communication_wheel_add_first_dimension": "Agregar Primera Dimensión",
        "communication_wheel_dimension_modal_title": "Dimensión de Comunicación",
        "communication_wheel_dimension_name": "Nombre de la Dimensión",
        "communication_wheel_dimension_name_placeholder": "Ej: Escucha Activa",
        "communication_wheel_dimension_name_required": "El nombre es requerido",
        "communication_wheel_guiding_question": "Pregunta Guía (Opcional)",
        "communication_wheel_guiding_question_placeholder": "Ej: ¿Presto atención completa y comprendo lo que otros dicen?",
        "communication_wheel_guiding_question_help": "Esta pregunta ayudará al cliente a reflexionar sobre esta dimensión",
        "button_back": "Volver",
        "button_save": "Guardar",
        "button_cancel": "Cancelar",
        "notifications.title": "Notificaciones",
        "notifications.markAllAsRead": "Marcar todas como leídas",
        "notifications.viewAll": "Ver todas las notificaciones",
        "notifications.noUnread": "No tienes notificaciones sin leer",
        "notifications.noNotifications": "No tienes notificaciones",
        "pagination.previous": "Anterior",
        "pagination.next": "Siguiente",
        "pagination.page": "Página",
        "notification_types.session_scheduled_by_coach": "Nueva sesión programada",
        "notification_types.calendar_event_scheduled_for_client": "Nuevo evento en el calendario",
        "notification_types.calendar_event_invitation": "Invitación a evento",
        "notification_types.questionnaire_assigned": "Nuevo cuestionario asignado",
        "notification_types.questionnaire_completed_by_client": "Cuestionario completado",
        "notification_types.communication_wheel_assigned": "Nueva rueda de comunicación asignada",
        "notification_types.communication_wheel_completed": "Rueda de comunicación completada",
        "notifications.newCount": "Nuevas",
        "notifications.markAllAsReadTooltip": "Marcar todas como leídas",
        "common.loading": "Cargando...",
        // Life Events
        "life_events.page_title": "Línea de la Vida",
        "life_events.add_event": "Agregar Evento",
        "life_events.create_title": "Agregar Evento",
        "life_events.edit_title": "Editar Evento",
        "life_events.delete_title": "Eliminar Evento",
        "life_events.event_details": "Detalles del Evento",
        "life_events.chart_title": "Gráfico de Línea de la Vida",
        "life_events.events_list": "Lista de Eventos",
        "life_events.no_events": "No tienes eventos registrados",
        "life_events.no_events_description": "Comienza creando tu primera línea de la vida agregando eventos significativos de tu vida.",
        "life_events.add_first_event": "Agregar Primer Evento",
        "life_events.event_to_delete": "Evento a eliminar:",
        "life_events.confirm_delete": "Confirmar Eliminación",
        "life_events.delete_warning_title": "¿Estás seguro de que deseas eliminar este evento?",
        "life_events.delete_warning_message": "Esta acción no se puede deshacer. El evento será eliminado permanentemente de tu línea de la vida.",
        "life_events.confirm_delete_button": "Sí, Eliminar Evento",
        "life_events.no_description": "Sin descripción",
        "life_events.stats.total": "Total de Eventos",
        "life_events.stats.positive": "Eventos Positivos",
        "life_events.stats.negative": "Eventos Negativos",
        "life_events.stats.neutral": "Eventos Neutrales",
        "life_events.table.year": "Año",
        "life_events.table.title": "Título",
        "life_events.table.impact": "Impacto",
        "life_events.table.description": "Descripción",
        "life_events.table.actions": "Acciones",
        "life_events.form.title": "Título del Evento",
        "life_events.form.title_placeholder": "Ej: Graduación, Matrimonio, Nuevo trabajo...",
        "life_events.form.year": "Año del Evento",
        "life_events.form.description": "Descripción",
        "life_events.form.description_placeholder": "Describe este evento con más detalle (opcional)...",
        "life_events.form.impact": "Impacto Emocional",
        "life_events.form.impact_instruction": "Selecciona el impacto emocional que este evento tuvo en tu vida:",
        "life_events.form.impact_help": "La escala va desde -3 (muy negativo) hasta +3 (muy positivo). Piensa en cómo este evento afectó tu vida en general.",
        "life_events.impact.very_positive": "Muy Positivo",
        "life_events.impact.positive": "Positivo",
        "life_events.impact.slightly_positive": "Ligeramente Positivo",
        "life_events.impact.neutral": "Neutral",
        "life_events.impact.slightly_negative": "Ligeramente Negativo",
        "life_events.impact.negative": "Negativo",
        "life_events.impact.very_negative": "Muy Negativo",
        // Módulo Client Quizzes
        "common.dashboard": "Panel de Control",
        "common.notification": "Notificación",
        "client_quizzes.page_title": "Mis Cuestionarios",
        "client_quizzes.list_title": "Cuestionarios Asignados",
        "client_quizzes.table.title": "Cuestionario",
        "client_quizzes.table.assigned_date": "Fecha Asignación",
        "client_quizzes.table.status": "Estado",
        "client_quizzes.table.progress": "Progreso",
        "client_quizzes.table.actions": "Acciones",
        "client_quizzes.status.pending": "Pendiente",
        "client_quizzes.status.in_progress": "En Progreso",
        "client_quizzes.status.completed": "Completado",
        "client_quizzes.no_quizzes": "No tienes cuestionarios asignados",
        "client_quizzes.no_quizzes_description": "Cuando tu coach te asigne cuestionarios aparecerán aquí.",
        "client_quizzes.filter_by_status": "Filtrar por estado:",
        "client_quizzes.filter.all": "Todos",
        "client_quizzes.filter.pending": "Pendientes",
        "client_quizzes.filter.in_progress": "En Progreso",
        "client_quizzes.filter.completed": "Completados",
        "client_quizzes.respond_title": "Responder Cuestionario",
        "client_quizzes.instructions": "Por favor responde todas las preguntas requeridas",
        "client_quizzes.save_draft": "Guardar Borrador",
        "client_quizzes.submit_answers": "Enviar Respuestas",
        "client_quizzes.progress_title": "Progreso",
        "client_quizzes.auto_save": "Guardado automático activado",
        "client_quizzes.required_fields": "Los campos requeridos están marcados con *",
        // Client Communication Wheels
        "client_communication_wheels_title": "Mis Ruedas de Comunicación",
        "client_wheel_view_result": "Ver Resultado",
        "button_print": "Imprimir",
        "client_wheel_your_result": "Tu Resultado",
        "client_wheel_completed_on": "Completado el",
        "client_wheel_legend_title": "Leyenda de Puntajes",
        "client_wheel_summary": "Resumen",
        "client_wheel_avg_score": "Puntaje Promedio",
        "client_wheel_max_score": "Puntaje Máximo",
        "client_wheel_min_score": "Puntaje Mínimo",
        "client_wheel_dimensions_count": "Número de Dimensiones",
        "client_wheel_assignment_info": "Información de Asignación",
        "client_wheel_assigned_by": "Asignado por",
        "client_wheel_assigned_date": "Fecha de Asignación",
        "button_back_to_list": "Volver a la Lista"
    }
};

// Function to load user avatar
function loadUserAvatar() {
    const avatarContainer = document.getElementById('userAvatar');
    if (!avatarContainer) return;
    
    fetch('/Users/GetCurrentUserInfo')
        .then(response => response.json())
        .then(data => {
            if (data.success && data.user) {
                const user = data.user;
                
                // Update user name in the dropdown
                const userNameElement = document.querySelector('.user-name');
                if (userNameElement) {
                    userNameElement.textContent = user.fullName;
                }
                
                // Update avatar
                if (user.avatarUrl && user.avatarUrl !== '/img/default-avatar.png') {
                    // User has a custom avatar
                    avatarContainer.innerHTML = `
                        <img src="${user.avatarUrl}" alt="${user.fullName}" 
                             class="rounded-circle user-avatar" 
                             style="width: 38px; height: 38px; object-fit: cover;"
                             onerror="this.onerror=null; this.parentElement.innerHTML=createInitialsAvatar('${user.fullName}');">
                    `;
                } else {
                    // Use initials avatar
                    avatarContainer.innerHTML = createInitialsAvatar(user.fullName);
                }
            }
        })
        .catch(error => {
            console.error('Error loading user info:', error);
        });
}

// Function to create initials avatar
function createInitialsAvatar(fullName) {
    const initials = fullName
        .split(' ')
        .map(name => name.charAt(0).toUpperCase())
        .slice(0, 2)
        .join('');
    
    return `
        <svg width="38" height="38" xmlns="http://www.w3.org/2000/svg" 
             class="rounded-circle user-avatar-placeholder" 
             style="background-color: #7367f0;">
            <text x="50%" y="50%" dominant-baseline="middle" text-anchor="middle" 
                  font-family="Arial" font-size="14" fill="#fff">${initials}</text>
        </svg>
    `;
}

// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {
    // Load user avatar on page load
    loadUserAvatar();
    
    // Elements
    const sidebarToggle = document.getElementById('sidebarToggle');
    const menuToggle = document.getElementById('menu-toggle');
    const sidebar = document.getElementById('sidebar-wrapper');
    const pageContent = document.getElementById('page-content-wrapper');
    const themeToggle = document.getElementById('theme-toggle');
    const themeIcon = document.getElementById('theme-icon');
    const languageButtons = document.querySelectorAll('.lang-select');
    const selectedLanguage = document.getElementById('selectedLanguage');
    
    // Create sidebar overlay for mobile
    const overlay = document.createElement('div');
    overlay.classList.add('sidebar-overlay');
    document.body.appendChild(overlay);
    
    // Initialize all components from localStorage
    initTheme();
    initLanguage();
    initSidebar();

    // Sidebar Toggle functionality
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function(e) {
            e.preventDefault();
            toggleSidebar();
        });
    }
    
    // Mobile menu close button
    if (menuToggle) {
        menuToggle.addEventListener('click', function(e) {
            e.preventDefault();
            toggleSidebar();
        });
    }
    
    // Overlay click handler (for mobile)
    overlay.addEventListener('click', function() {
        toggleSidebar();
    });
    
    // Theme Toggle functionality
    if (themeToggle) {
        themeToggle.addEventListener('click', function(e) {
            e.preventDefault();
            toggleTheme();
        });
    }
    
    // Language Selection functionality
    languageButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            const language = this.getAttribute('data-lang');
            setLanguage(language);
        });
    });
    
    /**
     * Toggles the sidebar visibility
     */
    function toggleSidebar() {
        sidebar.classList.toggle('toggled');
        pageContent.classList.toggle('toggled');
        overlay.classList.toggle('show');
        
        // Update aria-expanded attribute
        const isExpanded = !sidebar.classList.contains('toggled');
        if (sidebarToggle) {
            sidebarToggle.setAttribute('aria-expanded', isExpanded);
        }
        if (menuToggle) {
            menuToggle.setAttribute('aria-expanded', isExpanded);
        }
        
        // Save sidebar state to localStorage
        localStorage.setItem('sidebarCollapsed', sidebar.classList.contains('toggled'));
        
        // Prevent body scroll when sidebar is open on mobile
        if (sidebar.classList.contains('toggled') && window.innerWidth < 992) {
            document.body.style.overflow = 'hidden';
        } else {
            document.body.style.overflow = '';
        }
    }
    
    /**
     * Toggles between light and dark theme
     */
    function toggleTheme() {
        // Get the current theme from the document element
        const currentTheme = document.documentElement.getAttribute('data-theme');
        const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        
        // Update both HTML and body attributes
        document.documentElement.setAttribute('data-theme', newTheme);
        document.body.setAttribute('data-bs-theme', newTheme);
        
        // Update the theme icon
        if (newTheme === 'dark') {
            themeIcon.classList.remove('fa-moon');
            themeIcon.classList.add('fa-sun');
        } else {
            themeIcon.classList.remove('fa-sun');
            themeIcon.classList.add('fa-moon');
        }
        
        // Save the theme preference to localStorage
        localStorage.setItem('theme', newTheme);
        
        // Update the charts if they exist (optional)
        if (typeof salesChart !== 'undefined' && salesChart) {
            updateChartColors(newTheme);
        }
    }
    
    /**
     * Updates chart colors based on the current theme
     * @param {string} theme - The current theme ('light' or 'dark')
     */
    function updateChartColors(theme) {
        // Check if we're on a page with a chart
        const chartCanvas = document.getElementById('salesChart');
        if (!chartCanvas) {
            console.log('No chart canvas found on this page, skipping chart color update');
            return;
        }
        
        // Check if salesChart is defined and accessible in the global scope
        if (typeof window.salesChart === 'undefined' || !window.salesChart) {
            console.log('salesChart is not defined, skipping chart color update');
            
            // If we have a canvas but no chart yet, try to initialize it based on the theme
            if (chartCanvas && typeof Chart !== 'undefined') {
                try {
                    initializeChart(theme);
                } catch (error) {
                    console.log('Failed to initialize chart:', error);
                }
            }
            return;
        }
        
        try {
            // Make sure the chart has the necessary properties before accessing them
            if (!window.salesChart.data || !window.salesChart.data.datasets || window.salesChart.data.datasets.length === 0) {
                console.log('Chart exists but has no datasets, skipping color update');
                return;
            }
            
            if (theme === 'dark') {
                // Dark mode colors - slightly brighter for dark background
                window.salesChart.data.datasets[0].borderColor = 'rgba(50, 145, 255, 1)';
                window.salesChart.data.datasets[0].backgroundColor = 'rgba(50, 145, 255, 0.2)';
                if (window.salesChart.data.datasets.length > 1) {
                    window.salesChart.data.datasets[1].borderColor = 'rgba(50, 200, 100, 1)';
                    window.salesChart.data.datasets[1].backgroundColor = 'rgba(50, 200, 100, 0.2)';
                }
            } else {
                // Light mode colors
                window.salesChart.data.datasets[0].borderColor = 'rgba(13, 110, 253, 1)';
                window.salesChart.data.datasets[0].backgroundColor = 'rgba(13, 110, 253, 0.1)';
                if (window.salesChart.data.datasets.length > 1) {
                    window.salesChart.data.datasets[1].borderColor = 'rgba(25, 135, 84, 1)';
                    window.salesChart.data.datasets[1].backgroundColor = 'rgba(25, 135, 84, 0.1)';
                }
            }
            
            window.salesChart.update();
            console.log('Chart colors updated for theme:', theme);
        } catch (error) {
            console.log('Error updating chart colors:', error);
        }
    }
    
    /**
     * Initializes the sales chart
     * @param {string} theme - The current theme ('light' or 'dark')
     */
    function initializeChart(theme) {
        const chartCanvas = document.getElementById('salesChart');
        if (!chartCanvas) {
            console.log('No chart canvas found on this page');
            return;
        }
        
        // Exit if Chart.js is not loaded or if chart is already initialized
        if (typeof Chart === 'undefined' || window.salesChart) {
            return;
        }
        
        try {
            const ctx = chartCanvas.getContext('2d');
            
            // Set colors based on theme
            const borderColor = theme === 'dark' ? 'rgba(50, 145, 255, 1)' : 'rgba(13, 110, 253, 1)';
            const backgroundColor = theme === 'dark' ? 'rgba(50, 145, 255, 0.2)' : 'rgba(13, 110, 253, 0.1)';
            const borderColor2 = theme === 'dark' ? 'rgba(50, 200, 100, 1)' : 'rgba(25, 135, 84, 1)';
            const backgroundColor2 = theme === 'dark' ? 'rgba(50, 200, 100, 0.2)' : 'rgba(25, 135, 84, 0.1)';
            
            window.salesChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                    datasets: [{
                        label: 'Sales',
                        data: [12, 19, 15, 17, 23, 25, 22, 28, 24, 29, 32, 35],
                        borderColor: borderColor,
                        backgroundColor: backgroundColor,
                        borderWidth: 2,
                        tension: 0.3,
                        fill: true
                    }, {
                        label: 'Revenue',
                        data: [8, 12, 10, 14, 16, 19, 18, 23, 20, 25, 27, 30],
                        borderColor: borderColor2,
                        backgroundColor: backgroundColor2,
                        borderWidth: 2,
                        tension: 0.3,
                        fill: true
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        }
                    },
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    },
                    interaction: {
                        intersect: false,
                        mode: 'index'
                    }
                }
            });
            
            console.log('Chart initialized successfully with theme:', theme);
        } catch (error) {
            console.error('Error initializing chart:', error);
        }
    }
    
    /**
     * Sets the application language and updates all UI elements
     * @param {string} lang - The language code (e.g., 'en', 'es')
     */
    function setLanguage(lang) {
        // Update the language display text
        let displayText = 'English';
        
        if (lang === 'es') {
            displayText = 'Español';
        }
        
        // Update the selected language text
        if (selectedLanguage) {
            selectedLanguage.textContent = displayText;
        }
        
        // Save the language preference
        localStorage.setItem('language', lang);
        
        // Get all elements with translation keys
        const elements = document.querySelectorAll('[data-translate-key]');
        console.log(`Found ${elements.length} elements with translation keys`);
        
        // Update each element with the corresponding translation
        elements.forEach(element => {
            const key = element.getAttribute('data-translate-key');
            
            // SOLUCIÓN A: Excluir el textarea específico de CKEditor
            if (element.tagName === 'TEXTAREA' && element.id === 'description') {
                console.log('Saltando traducción directa del textarea #description para CKEditor.');
                return;
            }
            
            if (!translations[lang] || !translations[lang][key]) {
                console.warn(`Missing translation for key: ${key} in language: ${lang}`);
                return;
            }
            
            const translation = translations[lang][key];
            
            if (translation) {
                // Find the span tag for text content (sidebar items)
                const textSpan = element.querySelector('.sidebar-item-text');
                if (textSpan) {
                    textSpan.textContent = translation;
                    console.log(`Updated sidebar item: ${key} to ${translation}`);
                }
                
                // For elements containing HTML (like welcome message)
                else if (key === 'welcome_message_admin') {
                    element.innerHTML = translation;
                    console.log(`Updated welcome message to ${translation}`);
                }
                
                // For elements without span (like dropdown items)
                else if (!textSpan && !element.querySelector('div')) {
                    // Skip elements that might have child elements
                    element.textContent = translation;
                    console.log(`Updated element: ${key} to ${translation}`);
                }
            }
        });
        
        // Translate placeholders
        const placeholderElements = document.querySelectorAll('[data-translate-placeholder-key]');
        placeholderElements.forEach(element => {
            const key = element.getAttribute('data-translate-placeholder-key');
            if (translations[lang] && translations[lang][key]) {
                element.placeholder = translations[lang][key];
            }
        });

        // Translate tooltips
        const tooltipElements = document.querySelectorAll('[data-translate-title-key]');
        tooltipElements.forEach(element => {
            const key = element.getAttribute('data-translate-title-key');
            if (translations[lang] && translations[lang][key]) {
                element.title = translations[lang][key];
                // Update Bootstrap tooltip instance if it exists
                const tooltipInstance = bootstrap.Tooltip.getInstance(element);
                if (tooltipInstance) {
                    tooltipInstance.setContent({
                        '.tooltip-inner': translations[lang][key]
                    });
                }
            }
        });
        
        console.log(`Language changed to: ${lang}`);
    }
    
    /**
     * Legacy function for backward compatibility, calls setLanguage()
     * @param {string} language - The language code (e.g., 'en', 'es')
     */
    function changeLanguage(language) {
        setLanguage(language);
    }
    
    /**
     * Initializes the theme based on localStorage or defaults to light
     */
    function initTheme() {
        const savedTheme = localStorage.getItem('theme') || 'light';
        
        // Set theme on both HTML and body elements
        document.documentElement.setAttribute('data-theme', savedTheme);
        document.body.setAttribute('data-bs-theme', savedTheme);
        
        // Check if theme icon exists
        if (themeIcon) {
            // Set the correct icon for the current theme
            if (savedTheme === 'dark') {
                themeIcon.classList.remove('fa-moon');
                themeIcon.classList.add('fa-sun');
            } else {
                themeIcon.classList.remove('fa-sun');
                themeIcon.classList.add('fa-moon');
            }
        } else {
            console.log('Theme icon element not found');
        }
        
        // Initialize or update chart if needed
        try {
            // First check if we need to initialize the chart
            const chartCanvas = document.getElementById('salesChart');
            if (chartCanvas) {
                if (typeof window.salesChart === 'undefined' || !window.salesChart) {
                    // Chart is not initialized yet, initialize it
                    if (typeof initializeChart === 'function') {
                        initializeChart(savedTheme);
                    }
                } else {
                    // Chart exists, just update colors
                    updateChartColors(savedTheme);
                }
            }
        } catch (error) {
            console.log('Error handling chart during theme initialization:', error);
        }
    }
    
    /**
     * Initializes the language based on user preference or defaults to Spanish
     */
    async function initLanguage() {
        try {
            // First try to get user's default language from server
            const response = await fetch('/WebsiteSettings/GetDefaultLanguage');
            const result = await response.json();
            
            let languageToUse;
            if (result.success && result.language) {
                languageToUse = result.language;
            } else {
                // Fallback to localStorage or default
                languageToUse = localStorage.getItem('language') || 'es';
            }
            
            // Apply translations based on the determined language
            setLanguage(languageToUse);
            
            // Update the language selector in header
            updateLanguageSelector(languageToUse);
            
        } catch (error) {
            console.error('Error loading user language preference:', error);
            // Fallback to localStorage or default
            const fallbackLanguage = localStorage.getItem('language') || 'es';
            setLanguage(fallbackLanguage);
            updateLanguageSelector(fallbackLanguage);
        }
    }

    /**
     * Updates the language selector in the header
     */
    function updateLanguageSelector(language) {
        const selectedLanguageSpan = document.getElementById('selectedLanguage');
        if (selectedLanguageSpan) {
            const languageText = language === 'en' ? 'English' : 'Español';
            selectedLanguageSpan.textContent = languageText;
        }
        
        // Mark the active language option
        const languageLinks = document.querySelectorAll('.lang-select');
        languageLinks.forEach(link => {
            link.classList.remove('active');
            if (link.getAttribute('data-lang') === language) {
                link.classList.add('active');
            }
        });
    }
    
    /**
     * Initializes the sidebar state based on localStorage
     */
    function initSidebar() {
        const sidebarCollapsed = localStorage.getItem('sidebarCollapsed') === 'true';
        
        if (sidebarCollapsed) {
            sidebar.classList.add('toggled');
            pageContent.classList.add('toggled');
        }
        
        // Handle submenu hover behavior when sidebar is collapsed
        const submenuItems = document.querySelectorAll('.sidebar a[data-bs-toggle="collapse"]');
        
        submenuItems.forEach(item => {
            const submenuId = item.getAttribute('href');
            const submenu = document.querySelector(submenuId);
            
            item.addEventListener('mouseenter', function() {
                if (sidebar.classList.contains('toggled')) {
                    const bsCollapse = new bootstrap.Collapse(submenu, {
                        toggle: false
                    });
                    bsCollapse.show();
                }
            });
            
            item.parentElement.addEventListener('mouseleave', function() {
                if (sidebar.classList.contains('toggled')) {
                    const bsCollapse = new bootstrap.Collapse(submenu, {
                        toggle: false
                    });
                    bsCollapse.hide();
                }
            });
        });
    }
    
    /**
     * Legacy function for backward compatibility, calls setLanguage()
     * @param {string} language - The language code to translate to
     */
    function translatePage(language) {
        setLanguage(language);
    }
    
    /**
     * Window resize event handler
     * Closes the sidebar when resizing from mobile to desktop
     */
    window.addEventListener('resize', function() {
        if (window.innerWidth >= 992 && overlay.classList.contains('show')) {
            toggleSidebar();
        }
    });
});

/**
 * Habit Tracker Module
 * Handles habit tracking functionality, chart visualization, and data persistence
 */

// Global variables for habit tracker
let habitProgressChart = null;
let currentChartPeriod = 'Last7Days';

/**
 * Initialize habit tracker functionality
 */
window.initializeHabitTracker = function() {
    console.log('Initializing Habit Tracker...');
    
    // Initialize event listeners
    initializeHabitCheckboxes();
    initializeDeleteButtons();
    initializeChartPeriodFilters();
    initializeLogPeriodFilters();
    initializeSaveLogButton();
    initializeTabSwitching();
    
    // Don't initialize chart immediately, wait for tab switch
    console.log('Habit Tracker initialization complete');
};

/**
 * Initialize tab switching logic
 */
function initializeTabSwitching() {
    const chartTab = document.getElementById('progress-chart-tab');
    if (chartTab) {
        chartTab.addEventListener('shown.bs.tab', function() {
            console.log('Chart tab shown, initializing chart...');
            // Delay to ensure the tab content is visible
            setTimeout(() => {
                initializeChart();
                updateChart();
            }, 100);
        });
    }
}

/**
 * Initialize habit completion checkboxes
 */
function initializeHabitCheckboxes() {
    const checkboxes = document.querySelectorAll('.habit-checkbox');
    
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            const habitId = this.getAttribute('data-habit-id');
            const date = this.getAttribute('data-date');
            const isCompleted = this.checked;
            
            // Save habit log entry
            saveHabitLog(habitId, date, isCompleted);
        });
    });
}

/**
 * Initialize delete habit buttons
 */
function initializeDeleteButtons() {
    const deleteButtons = document.querySelectorAll('.habit-delete-btn');
    
    deleteButtons.forEach(button => {
        button.addEventListener('click', function() {
            const habitId = this.getAttribute('data-habit-id');
            const habitName = this.getAttribute('data-habit-name');
            
            // Show confirmation dialog
            if (confirm(`Are you sure you want to delete the habit "${habitName}"? This action cannot be undone.`)) {
                deleteHabit(habitId);
            }
        });
    });
}

/**
 * Initialize chart period filter buttons (new design)
 */
function initializeChartPeriodFilters() {
    const periodButtons = document.querySelectorAll('#progress-chart .period-filter-btn');
    
    periodButtons.forEach(button => {
        button.addEventListener('click', function() {
            // Remove active class from all buttons
            periodButtons.forEach(btn => btn.classList.remove('active'));
            
            // Add active class to clicked button
            this.classList.add('active');
            
            // Update current period and chart
            currentChartPeriod = this.getAttribute('data-period');
            updateChart();
        });
    });
}

/**
 * Initialize log period filter buttons (new design)
 */
function initializeLogPeriodFilters() {
    const periodButtons = document.querySelectorAll('#habit-log .period-filter-btn');
    
    periodButtons.forEach(button => {
        button.addEventListener('click', function() {
            // Remove active class from all buttons
            periodButtons.forEach(btn => btn.classList.remove('active'));
            
            // Add active class to clicked button
            this.classList.add('active');
            
            // Update log period and sync with chart
            const period = this.getAttribute('data-period');
            updateLogPeriod(period);
            currentChartPeriod = period;
            updateChart();
        });
    });
}

/**
 * Initialize save log button
 */
function initializeSaveLogButton() {
    const saveLogBtn = document.getElementById('saveLogBtn');
    if (saveLogBtn) {
        saveLogBtn.addEventListener('click', function() {
            saveAllHabitLogs();
        });
    }
}

/**
 * Initialize the habit progress chart with enhanced clarity and design
 */
function initializeChart() {
    // Check if Chart.js is loaded
    if (typeof Chart === 'undefined') {
        console.log('Chart.js not loaded yet, retrying in 100ms...');
        setTimeout(initializeChart, 100);
        return;
    }
    
    const canvas = document.getElementById('habitProgressChart');
    if (!canvas) {
        console.log('Chart canvas not found');
        return;
    }
    
    // Destroy existing chart if it exists
    if (habitProgressChart) {
        habitProgressChart.destroy();
    }
    
    // Set high resolution for crisp rendering
    const ctx = canvas.getContext('2d');
    const devicePixelRatio = window.devicePixelRatio || 1;
    
    // Set canvas size for high-DPI displays
    const rect = canvas.parentNode.getBoundingClientRect();
    canvas.width = rect.width * devicePixelRatio;
    canvas.height = rect.height * devicePixelRatio;
    canvas.style.width = rect.width + 'px';
    canvas.style.height = rect.height + 'px';
    ctx.scale(devicePixelRatio, devicePixelRatio);
    
    // Enhanced color scheme - elegant purple
    const primaryPurple = '#6366f1';
    const lightPurple = 'rgba(163, 180, 252, 0.3)';
    const borderPurple = '#4338ca';
    
    // Create gradient for bars
    const gradient = ctx.createLinearGradient(0, 0, canvas.width, 0);
    gradient.addColorStop(0, 'rgba(99, 102, 241, 0.3)');
    gradient.addColorStop(1, 'rgba(99, 102, 241, 0.8)');
    
    // Register the plugin
    Chart.register(ChartDataLabels);
    
    habitProgressChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [],
            datasets: [{
                label: 'Progreso del Hábito',
                data: [],
                backgroundColor: gradient,
                borderColor: primaryPurple,
                borderWidth: 0,
                borderRadius: 8,
                barThickness: 30,
                maxBarThickness: 40,
                borderSkipped: false
            }]
        },
        options: {
            indexAxis: 'y',
            responsive: true,
            maintainAspectRatio: false,
            devicePixelRatio: devicePixelRatio,
            animation: {
                duration: 1000,
                easing: 'easeInOutCubic',
                delay: (context) => {
                    let delay = 0;
                    if (context.type === 'data' && context.mode === 'default') {
                        delay = context.dataIndex * 100;
                    }
                    return delay;
                }
            },
            layout: {
                padding: {
                    top: 20,
                    right: 20,
                    bottom: 20,
                    left: 20
                }
            },
            scales: {
                x: {
                    beginAtZero: true,
                    max: 100,
                    grid: {
                        display: true,
                        drawBorder: false,
                        color: 'rgba(226, 232, 240, 0.5)',
                        lineWidth: 1
                    },
                    ticks: {
                        stepSize: 25,
                        color: '#64748b',
                        font: {
                            size: 12,
                            weight: '500',
                            family: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto'
                        },
                        callback: function(value) {
                            return value + '%';
                        }
                    }
                },
                y: {
                    grid: {
                        display: false,
                        drawBorder: false
                    },
                    ticks: {
                        color: '#1e293b',
                        font: {
                            size: 14,
                            weight: '600',
                            family: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto'
                        },
                        padding: 10
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    backgroundColor: 'rgba(15, 23, 42, 0.95)',
                    titleColor: '#ffffff',
                    bodyColor: '#e2e8f0',
                    borderColor: primaryPurple,
                    borderWidth: 1,
                    cornerRadius: 12,
                    padding: 16,
                    displayColors: false,
                    titleFont: {
                        size: 15,
                        weight: '600',
                        family: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto'
                    },
                    bodyFont: {
                        size: 14,
                        weight: '400',
                        family: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto'
                    },
                    callbacks: {
                        title: function(context) {
                            return context[0].label;
                        },
                        label: function(context) {
                            const value = context.parsed.x;
                            const progressBar = '█'.repeat(Math.floor(value / 10)) + '░'.repeat(10 - Math.floor(value / 10));
                            return [`Progreso: ${value}%`, progressBar];
                        },
                        afterLabel: function(context) {
                            const dataIndex = context.dataIndex;
                            const totalDays = 7; // You can make this dynamic
                            const daysCompleted = Math.round((context.parsed.x / 100) * totalDays);
                            return `${daysCompleted} de ${totalDays} días completados`;
                        }
                    }
                },
                datalabels: {
                    display: true,
                    anchor: 'end',
                    align: 'end',
                    color: '#1e293b',
                    font: {
                        size: 14,
                        weight: '700',
                        family: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto'
                    },
                    formatter: function(value) {
                        return value + '%';
                    },
                    padding: {
                        right: 10
                    }
                }
            }
        },
        plugins: [{
            id: 'customCanvasBackgroundColor',
            beforeDraw: (chart) => {
                const ctx = chart.canvas.getContext('2d');
                ctx.save();
                ctx.globalCompositeOperation = 'destination-over';
                ctx.fillStyle = '#ffffff';
                ctx.fillRect(0, 0, chart.width, chart.height);
                ctx.restore();
            }
        }]
    });
    
    console.log('Habit tracker chart initialized successfully with enhanced clarity');
}

/**
 * Update the chart with current period data
 */
function updateChart() {
    console.log('updateChart called, habitProgressChart exists:', !!habitProgressChart);
    
    if (!habitProgressChart) {
        console.log('Chart not initialized, initializing now...');
        initializeChart();
        // Retry after initialization
        setTimeout(updateChart, 100);
        return;
    }
    
    console.log('Fetching chart data for period:', currentChartPeriod);
    
    // Fetch chart data from server
    fetch(`/HabitTracker/GetHabitChartData?period=${currentChartPeriod}`)
        .then(response => {
            console.log('Response status:', response.status);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log('Chart data received:', data);
            
            if (data.success === false) {
                throw new Error(data.message || 'Failed to get chart data');
            }
            
            // Update chart data
            const labels = data.labels || [];
            const completionRates = data.completionRates || [];
            const colors = data.colors || [];
            
            console.log('Updating chart with:', { labels, completionRates, colors });
            
            habitProgressChart.data.labels = labels;
            habitProgressChart.data.datasets[0].data = completionRates;
            
            // Create individual gradients for each bar based on habit colors
            const ctx = habitProgressChart.ctx;
            const chartArea = habitProgressChart.chartArea;
            
            if (colors.length > 0 && chartArea) {
                const gradients = colors.map(color => {
                    const gradient = ctx.createLinearGradient(chartArea.left, 0, chartArea.right, 0);
                    gradient.addColorStop(0, color + '40'); // 25% opacity
                    gradient.addColorStop(0.5, color + '80'); // 50% opacity
                    gradient.addColorStop(1, color + 'CC'); // 80% opacity
                    return gradient;
                });
                
                habitProgressChart.data.datasets[0].backgroundColor = gradients;
                habitProgressChart.data.datasets[0].borderColor = colors.map(color => color + 'FF');
                habitProgressChart.data.datasets[0].borderWidth = 2;
            } else {
                // Default gradient when no data
                const defaultGradient = ctx.createLinearGradient(chartArea.left, 0, chartArea.right, 0);
                defaultGradient.addColorStop(0, 'rgba(99, 102, 241, 0.3)');
                defaultGradient.addColorStop(1, 'rgba(99, 102, 241, 0.8)');
                habitProgressChart.data.datasets[0].backgroundColor = defaultGradient;
                habitProgressChart.data.datasets[0].borderColor = '#6366f1';
                habitProgressChart.data.datasets[0].borderWidth = 0;
            }
            
            // Smooth update with animation
            habitProgressChart.update('active');
            console.log('Chart updated successfully');
            
            // Update statistics
            updateStatistics(data);
        })
        .catch(error => {
            console.error('Error fetching chart data:', error);
            showToast('Error loading chart data: ' + error.message, 'error');
        });
}

/**
 * Update statistics display
 */
function updateStatistics(data) {
    const totalHabitsElement = document.getElementById('totalHabits');
    const avgCompletionElement = document.getElementById('avgCompletion');
    const bestStreakElement = document.getElementById('bestStreak');
    const activeStreaksElement = document.getElementById('activeStreaks');
    
    if (totalHabitsElement) totalHabitsElement.textContent = data.totalHabits;
    if (avgCompletionElement) avgCompletionElement.textContent = `${data.averageCompletion}%`;
    if (bestStreakElement) bestStreakElement.textContent = data.bestStreak;
    if (activeStreaksElement) activeStreaksElement.textContent = data.activeStreaks;
}

/**
 * Save habit log entry
 */
function saveHabitLog(habitId, date, isCompleted) {
    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    const token = tokenElement ? tokenElement.value : '';
    
    // Ensure date is in correct format (YYYY-MM-DD)
    const formattedDate = new Date(date).toISOString().split('T')[0];
    
    const logData = {
        Registros: [
            {
                HabitoId: parseInt(habitId),
                Fecha: formattedDate + 'T00:00:00.000Z', // ISO format for compatibility
                Cumplido: isCompleted
            }
        ]
    };
    
    fetch('/HabitTracker/SaveHabitLog', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': token
        },
        body: JSON.stringify(logData)
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Calculate completion percentage locally
            const habitRow = document.querySelector(`tr:has(.habit-checkbox[data-habit-id="${habitId}"])`);
            if (habitRow) {
                const checkboxes = habitRow.querySelectorAll(`.habit-checkbox[data-habit-id="${habitId}"]`);
                const checkedCount = habitRow.querySelectorAll(`.habit-checkbox[data-habit-id="${habitId}"]:checked`).length;
                const totalCount = checkboxes.length;
                const completionPercentage = totalCount > 0 ? Math.round((checkedCount / totalCount) * 100) : 0;
                
                // Update progress display for this habit
                updateHabitProgress(habitId, completionPercentage, 0); // currentStreak not provided by server
            }
            
            // Update overall success rate if provided
            if (data.data && data.data.OverallSuccessRate !== undefined) {
                document.getElementById('overallSuccessRate').textContent = `${Math.round(data.data.OverallSuccessRate)}%`;
            }
            
            // Update chart if we're on the chart tab
            if (document.getElementById('progress-chart-tab').classList.contains('active')) {
                updateChart();
            }
            
            showToast('Habit updated successfully', 'success');
        } else {
            showToast(data.message || 'Error updating habit', 'error');
        }
    })
    .catch(error => {
        console.error('Error saving habit log:', error);
        showToast('Error updating habit', 'error');
    });
}

/**
 * Update habit progress display
 */
function updateHabitProgress(habitId, completionPercentage, currentStreak) {
    const habitRow = document.querySelector(`tr:has(.habit-checkbox[data-habit-id="${habitId}"])`);
    if (!habitRow) return;
    
    // Update days met badge
    const daysMet = habitRow.querySelectorAll(`.habit-checkbox[data-habit-id="${habitId}"]:checked`).length;
    const daysMetBadge = habitRow.querySelector('.habit-days-met');
    if (daysMetBadge) {
        daysMetBadge.textContent = daysMet;
    }
    
    // Update progress bar
    const progressBar = habitRow.querySelector('.habit-progress-bar');
    if (progressBar) {
        progressBar.style.width = `${completionPercentage}%`;
        progressBar.setAttribute('aria-valuenow', completionPercentage);
    }
    
    // Update percentage text
    const percentageText = habitRow.querySelector('.habit-percentage');
    if (percentageText) {
        percentageText.textContent = `${completionPercentage}%`;
    }
    
    // Update streak display
    const streakElement = habitRow.querySelector('.text-success');
    if (streakElement) {
        if (currentStreak > 0) {
            streakElement.innerHTML = `<i class="fas fa-fire me-1"></i>${currentStreak}`;
            streakElement.style.display = 'block';
        } else {
            streakElement.style.display = 'none';
        }
    }
    
    // Update overall success rate
    updateOverallSuccessRate();
}

/**
 * Update log period display and data
 */
function updateLogPeriod(period) {
    // This would typically fetch new data from server
    // For now, we'll just update the period text
    const periodText = document.getElementById('currentPeriodText');
    if (periodText) {
        let dateText = '';
        const today = new Date();
        
        switch(period) {
            case 'Last7Days':
                const sevenDaysAgo = new Date(today);
                sevenDaysAgo.setDate(today.getDate() - 7);
                dateText = `${sevenDaysAgo.toLocaleDateString('en-US', {month: 'short', day: 'numeric'})} - ${today.toLocaleDateString('en-US', {month: 'short', day: 'numeric', year: 'numeric'})}`;
                break;
            case 'Last30Days':
                const thirtyDaysAgo = new Date(today);
                thirtyDaysAgo.setDate(today.getDate() - 30);
                dateText = `${thirtyDaysAgo.toLocaleDateString('en-US', {month: 'short', day: 'numeric'})} - ${today.toLocaleDateString('en-US', {month: 'short', day: 'numeric', year: 'numeric'})}`;
                break;
            case 'ThisMonth':
                const firstDayOfMonth = new Date(today.getFullYear(), today.getMonth(), 1);
                dateText = `${firstDayOfMonth.toLocaleDateString('en-US', {month: 'short', day: 'numeric'})} - ${today.toLocaleDateString('en-US', {month: 'short', day: 'numeric', year: 'numeric'})}`;
                break;
            case 'AllTime':
                dateText = 'All Time';
                break;
        }
        
        periodText.textContent = dateText;
    }
}

/**
 * Update overall success rate
 */
function updateOverallSuccessRate() {
    const habitRows = document.querySelectorAll('tbody tr');
    let totalDaysMet = 0;
    let totalPercentage = 0;
    let habitCount = 0;
    
    habitRows.forEach(row => {
        const daysMetBadge = row.querySelector('.habit-days-met');
        const percentageText = row.querySelector('.habit-percentage');
        
        if (daysMetBadge && percentageText) {
            totalDaysMet += parseInt(daysMetBadge.textContent) || 0;
            totalPercentage += parseInt(percentageText.textContent.replace('%', '')) || 0;
            habitCount++;
        }
    });
    
    // Update overall days met
    const overallDaysMetElement = document.getElementById('overallDaysMet');
    if (overallDaysMetElement) {
        overallDaysMetElement.textContent = totalDaysMet;
    }
    
    // Update overall success rate
    const overallSuccessRateElement = document.getElementById('overallSuccessRate');
    if (overallSuccessRateElement && habitCount > 0) {
        const avgPercentage = Math.round(totalPercentage / habitCount * 10) / 10;
        overallSuccessRateElement.textContent = `${avgPercentage}%`;
    }
}

/**
 * Save all habit logs
 */
function saveAllHabitLogs() {
    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    const token = tokenElement ? tokenElement.value : '';
    
    const checkboxes = document.querySelectorAll('.habit-checkbox');
    const logEntries = [];
    
    checkboxes.forEach(checkbox => {
        const date = checkbox.getAttribute('data-date');
        const formattedDate = new Date(date).toISOString().split('T')[0] + 'T00:00:00.000Z';
        
        logEntries.push({
            HabitoId: parseInt(checkbox.getAttribute('data-habit-id')),
            Fecha: formattedDate,
            Cumplido: checkbox.checked
        });
    });
    
    const logData = {
        Registros: logEntries
    };
    
    // Show loading state
    const saveLogBtn = document.getElementById('saveLogBtn');
    const originalText = saveLogBtn.innerHTML;
    saveLogBtn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Saving...';
    saveLogBtn.disabled = true;
    
    fetch('/HabitTracker/SaveHabitLog', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': token
        },
        body: JSON.stringify(logData)
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            showToast('All habit logs saved successfully', 'success');
            
            // Update chart if we're on the chart tab
            if (document.getElementById('progress-chart-tab').classList.contains('active')) {
                updateChart();
            }
        } else {
            showToast('Error saving habit logs', 'error');
        }
    })
    .catch(error => {
        console.error('Error saving habit logs:', error);
        showToast('Error saving habit logs', 'error');
    })
    .finally(() => {
        // Restore button state
        saveLogBtn.innerHTML = originalText;
        saveLogBtn.disabled = false;
    });
}

/**
 * Add new habit
 */
window.addNewHabit = function(habitData) {
    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    const token = tokenElement ? tokenElement.value : '';
    
    fetch('/HabitTracker/AddHabit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': token
        },
        body: JSON.stringify({
            Nombre: habitData.nombre || "",
            Descripcion: habitData.descripcion || "",
            Color: habitData.color || "#3498db",
            CategoriaHabitoId: parseInt(habitData.categoriaHabitoId || 1),
            FrecuenciaHabitoId: parseInt(habitData.frecuenciaHabitoId || 1),
            HabilitarRecordatorios: habitData.habilitarRecordatorios || false
        })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Close offcanvas
            const offcanvas = bootstrap.Offcanvas.getInstance(document.getElementById('addHabitModal'));
            offcanvas.hide();
            
            // Reset form
            document.getElementById('addHabitForm').reset();
            
            // Reload page to show new habit
            window.location.reload();
            
            showToast('Habit added successfully', 'success');
        } else {
            showToast(data.message || 'Error adding habit', 'error');
        }
    })
    .catch(error => {
        console.error('Error adding habit:', error);
        showToast('Error adding habit', 'error');
    });
};

/**
 * Delete habit
 */
function deleteHabit(habitId) {
    const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    const token = tokenElement ? tokenElement.value : '';
    
    const formData = new FormData();
    formData.append('habitId', habitId);
    
    fetch('/HabitTracker/DeleteHabit', {
        method: 'POST',
        headers: {
            'RequestVerificationToken': token
        },
        body: formData
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Remove habit row from table  
            const habitRow = document.querySelector(`[data-habit-id="${habitId}"]`).closest('tr');
            if (habitRow) {
                habitRow.remove();
            }
            
            // Update overall success rate after deletion
            updateOverallSuccessRate();
            
            // Update chart if on chart tab
            if (document.getElementById('progress-chart-tab').classList.contains('active')) {
                updateChart();
            }
            
            showToast('Habit deleted successfully', 'success');
        } else {
            showToast(data.message || 'Error deleting habit', 'error');
        }
    })
    .catch(error => {
        console.error('Error deleting habit:', error);
        showToast('Error deleting habit', 'error');
    });
}

/**
 * Show toast notification
 */
function showToast(message, type = 'info') {
    // Create toast container if it doesn't exist
    let toastContainer = document.getElementById('toast-container');
    if (!toastContainer) {
        toastContainer = document.createElement('div');
        toastContainer.id = 'toast-container';
        toastContainer.className = 'toast-container position-fixed bottom-0 end-0 p-3';
        toastContainer.style.zIndex = '9999';
        document.body.appendChild(toastContainer);
    }
    
    // Create toast element
    const toastId = 'toast-' + Date.now();
    const toastHtml = `
        <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">
                <i class="fas fa-${type === 'success' ? 'check-circle text-success' : type === 'error' ? 'exclamation-circle text-danger' : 'info-circle text-info'} me-2"></i>
                <strong class="me-auto">Notification</strong>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                ${message}
            </div>
        </div>
    `;
    
    toastContainer.insertAdjacentHTML('beforeend', toastHtml);
    
    // Initialize and show toast
    const toastElement = document.getElementById(toastId);
    const toast = new bootstrap.Toast(toastElement);
    toast.show();
    
    // Remove toast element after it's hidden
    toastElement.addEventListener('hidden.bs.toast', function() {
        this.remove();
    });
}

/**
 * Welcome Message Module Functions
 */
let welcomeMessageEditor;

/* Función initializeWelcomeMessageEditor comentada temporalmente para depuración
function initializeWelcomeMessageEditor() {
    // Destroy existing editor instance if it exists
    if (CKEDITOR.instances.description) {
        CKEDITOR.instances.description.destroy(true);
    }
    
    // Inicio del código de depuración
    console.log('Intentando inicializar CKEditor para el ID: description');
    const textareaElement = document.getElementById('description');
    console.log('Resultado de document.getElementById("description"):', textareaElement);

    if (textareaElement) {
        console.log('Textarea #description ENCONTRADO. Atributos:', {
            id: textareaElement.id,
            name: textareaElement.name,
            className: textareaElement.className,
            valueLength: textareaElement.value ? textareaElement.value.length : 0,
            isVisible: !!(textareaElement.offsetWidth || textareaElement.offsetHeight || textareaElement.getClientRects().length) // Comprueba si es visible
        });
        
        // Línea original de inicialización (después del código de depuración)
        CKEDITOR.replace('description', {
            height: 300,
            width: '100%',
            language: 'es',
            uiColor: '#FAFAFA'
            // Sin ninguna configuración de toolbar o extraPlugins por ahora
        });
        
        console.log('CKEditor.replace() ejecutado');
        
    } else {
        console.error('¡ERROR CRÍTICO! Textarea con ID "description" NO FUE ENCONTRADO EN EL DOM al momento de la inicialización de CKEditor.');
    }

    welcomeMessageEditor = CKEDITOR.instances.description;

    // Initialize video type handlers
    initializeVideoTypeHandlers();
    
    // Initialize form handlers
    initializeWelcomeMessageForm();
    
    // Initialize preview handlers
    initializePreviewHandlers();
    
    // Show initial video section based on current type
    showVideoSection();
    
    // Initialize real-time preview updates
    initializeRealTimePreview();
}
*/

function initializeVideoTypeHandlers() {
    const videoTypeRadios = document.querySelectorAll("input[name=\"videoType\"]");
    
    videoTypeRadios.forEach(radio => {
        radio.addEventListener("change", function() {
            showVideoSection();
            updateVideoPreview();
        });
    });
}

function showVideoSection() {
    const videoType = document.querySelector("input[name=\"videoType\"]:checked")?.value || "None";
    
    // Hide all sections
    document.getElementById("uploadVideoSection").style.display = "none";
    document.getElementById("youtubeVideoSection").style.display = "none";
    document.getElementById("vimeoVideoSection").style.display = "none";
    
    // Show relevant section
    switch(videoType) {
        case "Uploaded":
            document.getElementById("uploadVideoSection").style.display = "block";
            break;
        case "YouTube":
            document.getElementById("youtubeVideoSection").style.display = "block";
            break;
        case "Vimeo":
            document.getElementById("vimeoVideoSection").style.display = "block";
            break;
    }
}

function initializeWelcomeMessageForm() {
    const form = document.getElementById("welcomeMessageForm");
    
    if (form) {
        form.addEventListener("submit", function(e) {
            e.preventDefault();
            saveWelcomeMessage();
        });
    }
    
    // Handle video file upload
    const videoFileInput = document.getElementById("videoFile");
    if (videoFileInput) {
        videoFileInput.addEventListener("change", function() {
            if (this.files && this.files[0]) {
                previewUploadedVideo(this.files[0]);
            }
        });
    }
}

function initializePreviewHandlers() {
    const previewButton = document.querySelector(".btn-preview");
    
    if (previewButton) {
        previewButton.addEventListener("click", function() {
            updatePreview();
        });
    }
}

function initializeRealTimePreview() {
    // Title input
    const titleInput = document.getElementById("title");
    if (titleInput) {
        titleInput.addEventListener("input", function() {
            updateTitlePreview();
        });
    }
    
    // CKEditor content changes
    if (welcomeMessageEditor) {
        welcomeMessageEditor.on("change", function() {
            updateDescriptionPreview();
        });
    }
    
    // URL inputs
    const youtubeInput = document.getElementById("youtubeUrl");
    const vimeoInput = document.getElementById("vimeoUrl");
    
    if (youtubeInput) {
        youtubeInput.addEventListener("input", debounce(updateVideoPreview, 500));
    }
    
    if (vimeoInput) {
        vimeoInput.addEventListener("input", debounce(updateVideoPreview, 500));
    }
}

function updateTitlePreview() {
    const title = document.getElementById("title").value;
    const previewTitle = document.getElementById("previewTitle");
    
    if (previewTitle) {
        previewTitle.textContent = title || "Título del mensaje";
    }
}

function updateDescriptionPreview() {
    if (welcomeMessageEditor) {
        const content = welcomeMessageEditor.getData();
        const previewDescription = document.getElementById("previewDescription");
        
        if (previewDescription) {
            previewDescription.innerHTML = content || "<p>Descripción del mensaje...</p>";
        }
    }
}

function updateVideoPreview() {
    const videoType = document.querySelector("input[name=\"videoType\"]:checked")?.value || "None";
    const previewVideo = document.getElementById("previewVideo");
    
    if (!previewVideo) return;
    
    switch(videoType) {
        case "None":
            previewVideo.innerHTML = "<p>Sin video seleccionado</p>";
            break;
            
        case "Uploaded":
            const videoFile = document.getElementById("videoFile").files[0];
            if (videoFile) {
                previewUploadedVideo(videoFile);
            } else {
                previewVideo.innerHTML = "<p>Selecciona un archivo de video</p>";
            }
            break;
            
        case "YouTube":
            const youtubeUrl = document.getElementById("youtubeUrl").value;
            if (youtubeUrl) {
                previewYouTubeVideo(youtubeUrl);
            } else {
                previewVideo.innerHTML = "<p>Ingresa una URL de YouTube</p>";
            }
            break;
            
        case "Vimeo":
            const vimeoUrl = document.getElementById("vimeoUrl").value;
            if (vimeoUrl) {
                previewVimeoVideo(vimeoUrl);
            } else {
                previewVideo.innerHTML = "<p>Ingresa una URL de Vimeo</p>";
            }
            break;
    }
}

function previewUploadedVideo(file) {
    const previewVideo = document.getElementById("previewVideo");
    const url = URL.createObjectURL(file);
    
    previewVideo.innerHTML = `
        <video controls>
            <source src="${url}" type="${file.type}">
            Tu navegador no soporta el elemento video.
        </video>
    `;
}

function previewYouTubeVideo(url) {
    const previewVideo = document.getElementById("previewVideo");
    const videoId = extractYouTubeVideoId(url);
    
    if (videoId) {
        previewVideo.innerHTML = `
            <iframe 
                src="https://www.youtube.com/embed/${videoId}" 
                frameborder="0" 
                allowfullscreen>
            </iframe>
        `;
    } else {
        previewVideo.innerHTML = "<p>URL de YouTube no válida</p>";
    }
}

function previewVimeoVideo(url) {
    const previewVideo = document.getElementById("previewVideo");
    const videoId = extractVimeoVideoId(url);
    
    if (videoId) {
        previewVideo.innerHTML = `
            <iframe 
                src="https://player.vimeo.com/video/${videoId}" 
                frameborder="0" 
                allowfullscreen>
            </iframe>
        `;
    } else {
        previewVideo.innerHTML = "<p>URL de Vimeo no válida</p>";
    }
}

function extractYouTubeVideoId(url) {
    const regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
    const match = url.match(regExp);
    return (match && match[2].length === 11) ? match[2] : null;
}

function extractVimeoVideoId(url) {
    const regExp = /(?:vimeo)\.com.*(?:videos|video|channels|)\/([\d]+)/i;
    const match = url.match(regExp);
    return match ? match[1] : null;
}

function updatePreview() {
    updateTitlePreview();
    updateDescriptionPreview();
    updateVideoPreview();
}

function saveWelcomeMessage() {
    const form = document.getElementById("welcomeMessageForm");
    const submitBtn = form.querySelector(".btn-save");
    
    // Show loading state
    submitBtn.classList.add("loading");
    submitBtn.disabled = true;
    
    // Update CKEditor content
    if (welcomeMessageEditor) {
        welcomeMessageEditor.updateElement();
    }
    
    const formData = new FormData(form);
    
    fetch("/WelcomeMessage/Save", {
        method: "POST",
        body: formData,
        headers: {
            "RequestVerificationToken": document.querySelector("input[name=\"__RequestVerificationToken\"]").value
        }
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            showMessage(data.message, "success");
        } else {
            showMessage(data.message, "error");
        }
    })
    .catch(error => {
        console.error("Error:", error);
        showMessage("Error al guardar el mensaje de bienvenida", "error");
    })
    .finally(() => {
        // Remove loading state
        submitBtn.classList.remove("loading");
        submitBtn.disabled = false;
    });
}

function showMessage(message, type) {
    const container = document.querySelector(".welcome-message-content");
    const existingMessage = container.querySelector(".message-success, .message-error");
    
    if (existingMessage) {
        existingMessage.remove();
    }
    
    const messageDiv = document.createElement("div");
    messageDiv.className = `message-${type}`;
    messageDiv.innerHTML = `
        <i class="fas fa-${type === "success" ? "check-circle" : "exclamation-circle"}"></i>
        <span>${message}</span>
    `;
    
    container.insertBefore(messageDiv, container.firstChild);
    
    // Auto remove after 5 seconds
    setTimeout(() => {
        messageDiv.remove();
    }, 5000);
}

function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}
