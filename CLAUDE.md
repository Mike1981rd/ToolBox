# ToolBox Admin Dashboard Project

## Project Structure

- **Views/Shared/_AdminLayout.cshtml** - Main layout for admin pages with sidebar and navbar
- **Views/Admin/** - Admin views:
  - Dashboard.cshtml - Main dashboard with stats and charts
  - Users.cshtml - User management placeholder
  - Analytics.cshtml - Analytics placeholder
  - Settings.cshtml - Settings placeholder
  
- **Controllers/AdminController.cs** - Controller for admin views
- **wwwroot/css/admin-style.css** - Custom styles for the admin dashboard
- **wwwroot/js/admin-script.js** - JavaScript functions for the admin dashboard
- **wwwroot/img/logo.png** - Logo for the dashboard

## Notes

- Uses dark sidebar (#010813) and responsive layout
- Sidebar toggles on mobile and small screens
- Theme switching between light/dark modes
- Language selection (English/Spanish)
- Uses Bootstrap 5.3 and Font Awesome icons

## How to use

1. Run the application
2. Navigate to the root URL (/) which will redirect to the admin dashboard
3. Test responsiveness by resizing browser window
4. Test theme switching with the moon/sun icon in the navbar
5. Test language switching with the globe icon in the navbar
6. Test sidebar collapsing with the hamburger menu icon