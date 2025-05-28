-- Script para agregar permisos del módulo Notifications
-- Este script verifica si los permisos existen antes de insertarlos

DO $$
BEGIN
    -- Verificar y agregar permiso Read para Notifications
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'Notifications' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("ModuleName", "ActionName", "Description", "Category")
        VALUES ('Notifications', 'Read', 'Ver y listar en Notificaciones', 'General');
    END IF;

    -- Verificar y agregar permiso Write para Notifications
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'Notifications' AND "ActionName" = 'Write'
    ) THEN
        INSERT INTO "Permissions" ("ModuleName", "ActionName", "Description", "Category")
        VALUES ('Notifications', 'Write', 'Editar y actualizar en Notificaciones', 'General');
    END IF;

    -- Verificar y agregar permiso Create para Notifications
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'Notifications' AND "ActionName" = 'Create'
    ) THEN
        INSERT INTO "Permissions" ("ModuleName", "ActionName", "Description", "Category")
        VALUES ('Notifications', 'Create', 'Crear nuevos registros en Notificaciones', 'General');
    END IF;

    -- Asignar permisos de Notifications al rol Admin (ID = 1)
    -- Primero obtenemos los IDs de los permisos recién creados
    INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
    SELECT 1, p."Id"
    FROM "Permissions" p
    WHERE p."ModuleName" = 'Notifications'
    AND NOT EXISTS (
        SELECT 1 FROM "RolePermissions" rp 
        WHERE rp."RoleId" = 1 AND rp."PermissionId" = p."Id"
    );
    
    -- También asignar al rol Coach (ID = 2) si existe
    INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
    SELECT 2, p."Id"
    FROM "Permissions" p
    WHERE p."ModuleName" = 'Notifications'
    AND EXISTS (SELECT 1 FROM "Roles" WHERE "Id" = 2)
    AND NOT EXISTS (
        SELECT 1 FROM "RolePermissions" rp 
        WHERE rp."RoleId" = 2 AND rp."PermissionId" = p."Id"
    );
END $$;

-- Verificar que los permisos se crearon correctamente
SELECT * FROM "Permissions" WHERE "ModuleName" = 'Notifications';