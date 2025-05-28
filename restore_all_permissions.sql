-- Script para restaurar TODOS los permisos del sistema
-- ADVERTENCIA: Esto eliminará todos los permisos existentes y los recreará

-- Primero, eliminar todos los registros de RolePermissions
DELETE FROM "RolePermissions";

-- Luego, eliminar todos los permisos existentes
DELETE FROM "Permissions";

-- Reiniciar la secuencia si existe
-- ALTER SEQUENCE permissions_id_seq RESTART WITH 1;

-- Insertar todos los permisos del sistema
INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category") VALUES
-- Dashboard (1-3)
(1, 'Dashboard', 'Read', 'Ver y listar en Dashboard', 'General'),
(2, 'Dashboard', 'Write', 'Editar y actualizar en Dashboard', 'General'),
(3, 'Dashboard', 'Create', 'Crear nuevos registros en Dashboard', 'General'),

-- Topics (4-6)
(4, 'Topics', 'Read', 'Ver y listar en Temas', 'Gestión de Contenido'),
(5, 'Topics', 'Write', 'Editar y actualizar en Temas', 'Gestión de Contenido'),
(6, 'Topics', 'Create', 'Crear nuevos registros en Temas', 'Gestión de Contenido'),

-- VideoManagement (7-9)
(7, 'VideoManagement', 'Read', 'Ver y listar en Gestión de Videos', 'Gestión de Contenido'),
(8, 'VideoManagement', 'Write', 'Editar y actualizar en Gestión de Videos', 'Gestión de Contenido'),
(9, 'VideoManagement', 'Create', 'Crear nuevos registros en Gestión de Videos', 'Gestión de Contenido'),

-- Customers (10-12)
(10, 'Customers', 'Read', 'Ver y listar en Clientes', 'Gestión de Usuarios'),
(11, 'Customers', 'Write', 'Editar y actualizar en Clientes', 'Gestión de Usuarios'),
(12, 'Customers', 'Create', 'Crear nuevos registros en Clientes', 'Gestión de Usuarios'),

-- Users (13-15)
(13, 'Users', 'Read', 'Ver y listar en Usuarios', 'Gestión de Usuarios'),
(14, 'Users', 'Write', 'Editar y actualizar en Usuarios', 'Gestión de Usuarios'),
(15, 'Users', 'Create', 'Crear nuevos registros en Usuarios', 'Gestión de Usuarios'),

-- Roles (16-18)
(16, 'Roles', 'Read', 'Ver y listar en Roles', 'Gestión de Usuarios'),
(17, 'Roles', 'Write', 'Editar y actualizar en Roles', 'Gestión de Usuarios'),
(18, 'Roles', 'Create', 'Crear nuevos registros en Roles', 'Gestión de Usuarios'),

-- Instructors (19-21)
(19, 'Instructors', 'Read', 'Ver y listar en Instructores', 'Gestión de Usuarios'),
(20, 'Instructors', 'Write', 'Editar y actualizar en Instructores', 'Gestión de Usuarios'),
(21, 'Instructors', 'Create', 'Crear nuevos registros en Instructores', 'Gestión de Usuarios'),

-- ToolboxAcademy (22-24)
(22, 'ToolboxAcademy', 'Read', 'Ver y listar en Toolbox Academy', 'Gestión de Contenido'),
(23, 'ToolboxAcademy', 'Write', 'Editar y actualizar en Toolbox Academy', 'Gestión de Contenido'),
(24, 'ToolboxAcademy', 'Create', 'Crear nuevos registros en Toolbox Academy', 'Gestión de Contenido'),

-- WheelOfLife (25-27)
(25, 'WheelOfLife', 'Read', 'Ver y listar en Rueda de la Vida', 'Herramientas de Vida'),
(26, 'WheelOfLife', 'Write', 'Editar y actualizar en Rueda de la Vida', 'Herramientas de Vida'),
(27, 'WheelOfLife', 'Create', 'Crear nuevos registros en Rueda de la Vida', 'Herramientas de Vida'),

-- WheelOfProgress (28-30)
(28, 'WheelOfProgress', 'Read', 'Ver y listar en Rueda del Progreso', 'Herramientas de Vida'),
(29, 'WheelOfProgress', 'Write', 'Editar y actualizar en Rueda del Progreso', 'Herramientas de Vida'),
(30, 'WheelOfProgress', 'Create', 'Crear nuevos registros en Rueda del Progreso', 'Herramientas de Vida'),

-- XRayLife (31-33)
(31, 'XRayLife', 'Read', 'Ver y listar en XRay Life', 'Herramientas de Vida'),
(32, 'XRayLife', 'Write', 'Editar y actualizar en XRay Life', 'Herramientas de Vida'),
(33, 'XRayLife', 'Create', 'Crear nuevos registros en XRay Life', 'Herramientas de Vida'),

-- LifeAssessment (34-36)
(34, 'LifeAssessment', 'Read', 'Ver y listar en Evaluación de Vida', 'Herramientas de Vida'),
(35, 'LifeAssessment', 'Write', 'Editar y actualizar en Evaluación de Vida', 'Herramientas de Vida'),
(36, 'LifeAssessment', 'Create', 'Crear nuevos registros en Evaluación de Vida', 'Herramientas de Vida'),

-- LifeAreas (37-39)
(37, 'LifeAreas', 'Read', 'Ver y listar en Áreas de Vida', 'Herramientas de Vida'),
(38, 'LifeAreas', 'Write', 'Editar y actualizar en Áreas de Vida', 'Herramientas de Vida'),
(39, 'LifeAreas', 'Create', 'Crear nuevos registros en Áreas de Vida', 'Herramientas de Vida'),

-- Tasks (40-42)
(40, 'Tasks', 'Read', 'Ver y listar en Tareas', 'Productividad'),
(41, 'Tasks', 'Write', 'Editar y actualizar en Tareas', 'Productividad'),
(42, 'Tasks', 'Create', 'Crear nuevos registros en Tareas', 'Productividad'),

-- HabitTracker (43-45)
(43, 'HabitTracker', 'Read', 'Ver y listar en Rastreador de Hábitos', 'Productividad'),
(44, 'HabitTracker', 'Write', 'Editar y actualizar en Rastreador de Hábitos', 'Productividad'),
(45, 'HabitTracker', 'Create', 'Crear nuevos registros en Rastreador de Hábitos', 'Productividad'),

-- EmailContents (46-48)
(46, 'EmailContents', 'Read', 'Ver y listar en Contenidos de Email', 'Configuración'),
(47, 'EmailContents', 'Write', 'Editar y actualizar en Contenidos de Email', 'Configuración'),
(48, 'EmailContents', 'Create', 'Crear nuevos registros en Contenidos de Email', 'Configuración'),

-- WebsiteSettings (49-51)
(49, 'WebsiteSettings', 'Read', 'Ver y listar en Configuración del Sitio', 'Configuración'),
(50, 'WebsiteSettings', 'Write', 'Editar y actualizar en Configuración del Sitio', 'Configuración'),
(51, 'WebsiteSettings', 'Create', 'Crear nuevos registros en Configuración del Sitio', 'Configuración'),

-- WelcomeMessage (52-54)
(52, 'WelcomeMessage', 'Read', 'Ver y listar en Mensaje de Bienvenida', 'Configuración'),
(53, 'WelcomeMessage', 'Write', 'Editar y actualizar en Mensaje de Bienvenida', 'Configuración'),
(54, 'WelcomeMessage', 'Create', 'Crear nuevos registros en Mensaje de Bienvenida', 'Configuración'),

-- Calendario (55-57)
(55, 'Calendario', 'Read', 'Ver y listar en Calendario', 'Productividad'),
(56, 'Calendario', 'Write', 'Editar y actualizar en Calendario', 'Productividad'),
(57, 'Calendario', 'Create', 'Crear nuevos registros en Calendario', 'Productividad'),

-- Sessions (58-60)
(58, 'Sessions', 'Read', 'Ver y listar en Sesiones', 'Productividad'),
(59, 'Sessions', 'Write', 'Editar y actualizar en Sesiones', 'Productividad'),
(60, 'Sessions', 'Create', 'Crear nuevos registros en Sesiones', 'Productividad');

-- Ahora asignar TODOS los permisos al rol Administrador (RoleId = 2)
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 2, "Id" FROM "Permissions";

-- Verificar resultados
SELECT 'Permisos creados:' as info, COUNT(*) as total FROM "Permissions";
SELECT 'Permisos asignados al Admin:' as info, COUNT(*) as total FROM "RolePermissions" WHERE "RoleId" = 2;