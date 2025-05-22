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
        submit_button: "Submit",
        th_user: "USER",
        th_role: "ROLE",
        th_plan: "PLAN",
        th_billing: "BILLING",
        th_status: "STATUS",
        th_actions: "ACTIONS",
        areas_of_life: "Areas of Life",
        xray_of_life: "X-Ray of life",
        default_welcome_message: "Default Welcome Message",
        coaches: "Coaches",
        add_user: "Add USER",
        profile: "Profile",
        academy: "Academy",
        academy_submenu_1: "Courses",
        academy_submenu_2: "Materials",
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
        // Customer table headers
        th_customer_list_customer: "CUSTOMER",
        th_customer_list_customer_id: "CUSTOMER ID",
        th_customer_list_country: "COUNTRY",
        th_customer_list_order: "ORDER",
        th_customer_list_total_spent: "TOTAL SPENT",
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
        xray_a_family_1: "Schedule regular family time, communicate openly, and create shared traditions and memories."
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
        submit_button: "Enviar",
        th_user: "USUARIO",
        th_role: "ROL",
        th_plan: "PLAN",
        th_billing: "FACTURACIÓN",
        th_status: "ESTADO",
        th_actions: "ACCIONES",
        areas_of_life: "Áreas de Vida",
        xray_of_life: "Radiografía de Vida",
        default_welcome_message: "Mensaje de Bienvenida",
        coaches: "Coaches",
        add_user: "Añadir Usuario",
        profile: "Perfil",
        academy: "Academia",
        academy_submenu_1: "Cursos",
        academy_submenu_2: "Materiales",
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
        // Customer table headers
        th_customer_list_customer: "CLIENTE",
        th_customer_list_customer_id: "ID CLIENTE",
        th_customer_list_country: "PAÍS",
        th_customer_list_order: "PEDIDOS",
        th_customer_list_total_spent: "GASTO TOTAL",
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
        xray_a_family_1: "Programa tiempo familiar regular, comunícate abiertamente y crea tradiciones y recuerdos compartidos."
    }
};

// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {
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
            console.log("Changing language to: " + language);
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
     * Initializes the language based on localStorage or defaults to English
     */
    function initLanguage() {
        const savedLanguage = localStorage.getItem('language') || 'en';
        
        // Apply translations based on the saved language
        setLanguage(savedLanguage);
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