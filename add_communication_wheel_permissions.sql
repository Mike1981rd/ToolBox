-- Script para agregar permisos del módulo Communication Wheel
-- Último ID de permiso existente: 71

DO $$
DECLARE
    next_id INTEGER;
BEGIN
    -- Permisos para el Coach (gestión de plantillas)
    
    -- Permiso Read para CommunicationWheelTemplates
    SELECT COALESCE(MAX("Id"), 71) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'CommunicationWheelTemplates' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'CommunicationWheelTemplates', 'Read', 'Ver y listar plantillas de rueda de comunicación', 'Herramientas de Comunicación');
    END IF;

    -- Permiso Write para CommunicationWheelTemplates
    SELECT COALESCE(MAX("Id"), 71) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'CommunicationWheelTemplates' AND "ActionName" = 'Write'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'CommunicationWheelTemplates', 'Write', 'Editar y actualizar plantillas de rueda de comunicación', 'Herramientas de Comunicación');
    END IF;

    -- Permiso Create para CommunicationWheelTemplates
    SELECT COALESCE(MAX("Id"), 71) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'CommunicationWheelTemplates' AND "ActionName" = 'Create'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'CommunicationWheelTemplates', 'Create', 'Crear nuevas plantillas de rueda de comunicación', 'Herramientas de Comunicación');
    END IF;

    -- Permisos para el Coach (visualización de ruedas asignadas)
    
    -- Permiso Read para CoachClientWheels
    SELECT COALESCE(MAX("Id"), 71) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'CoachClientWheels' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'CoachClientWheels', 'Read', 'Ver ruedas de comunicación asignadas y completadas por clientes', 'Herramientas de Comunicación');
    END IF;

    -- Permisos para el Cliente
    
    -- Permiso Read para ClientCommunicationWheels
    SELECT COALESCE(MAX("Id"), 71) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'ClientCommunicationWheels' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'ClientCommunicationWheels', 'Read', 'Ver ruedas de comunicación asignadas', 'Herramientas de Comunicación');
    END IF;

    -- Permiso Write para ClientCommunicationWheels (completar ruedas)
    SELECT COALESCE(MAX("Id"), 71) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'ClientCommunicationWheels' AND "ActionName" = 'Write'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'ClientCommunicationWheels', 'Write', 'Completar evaluaciones de rueda de comunicación', 'Herramientas de Comunicación');
    END IF;
END $$;

-- Asignar permisos al rol Admin (ID = 2)
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 2, p."Id"
FROM "Permissions" p
WHERE p."ModuleName" IN ('CommunicationWheelTemplates', 'CoachClientWheels', 'ClientCommunicationWheels')
AND NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 2 AND rp."PermissionId" = p."Id"
);

-- Opcional: Asignar permisos de cliente al rol Cliente (si existe)
-- Descomenta y ajusta el RoleId según tu configuración
/*
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 2, p."Id"  -- Asumiendo que 2 es el ID del rol Cliente
FROM "Permissions" p
WHERE p."ModuleName" = 'ClientCommunicationWheels'
AND NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 2 AND rp."PermissionId" = p."Id"
);
*/

-- Verificar permisos creados
SELECT * FROM "Permissions" 
WHERE "ModuleName" IN ('CommunicationWheelTemplates', 'CoachClientWheels', 'ClientCommunicationWheels')
ORDER BY "Id";