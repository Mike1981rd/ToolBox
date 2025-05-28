-- Script para agregar permisos de NotificationPreferences
-- IMPORTANTE: Este script verifica el último ID y agrega los permisos con IDs automáticos

DO $$
DECLARE
    next_id INTEGER;
BEGIN
    -- Permiso Read
    SELECT COALESCE(MAX("Id"), 0) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'NotificationPreferences' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'NotificationPreferences', 'Read', 'Ver preferencias de notificaciones', 'General');
    END IF;

    -- Permiso Write (para actualizar preferencias)
    SELECT COALESCE(MAX("Id"), 0) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'NotificationPreferences' AND "ActionName" = 'Write'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'NotificationPreferences', 'Write', 'Actualizar preferencias de notificaciones', 'General');
    END IF;
END $$;

-- Asignar permisos a todos los roles (todos los usuarios deben poder gestionar sus propias preferencias)
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT r."Id", p."Id"
FROM "Roles" r
CROSS JOIN "Permissions" p
WHERE p."ModuleName" = 'NotificationPreferences'
AND NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."RoleId" = r."Id" AND rp."PermissionId" = p."Id"
);

-- Verificar que se crearon correctamente
SELECT * FROM "Permissions" WHERE "ModuleName" = 'NotificationPreferences';