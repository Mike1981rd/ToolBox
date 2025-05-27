-- Script para verificar y agregar permisos de Sessions si no existen

-- Verificar si los permisos ya existen
DO $$
BEGIN
    -- Verificar y agregar permisos si no existen
    IF NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "ModuleName" = 'Sessions' AND "ActionName" = 'Read') THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Category", "DisplayName", "Description", "IsSystem", "CreatedAt")
        VALUES (115, 'Sessions', 'Read', 'Gestión de Clientes', 'Sessions - Read', 'Ver y listar sesiones', false, NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "ModuleName" = 'Sessions' AND "ActionName" = 'Write') THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Category", "DisplayName", "Description", "IsSystem", "CreatedAt")
        VALUES (116, 'Sessions', 'Write', 'Gestión de Clientes', 'Sessions - Write', 'Editar y actualizar sesiones', false, NOW());
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "Permissions" WHERE "ModuleName" = 'Sessions' AND "ActionName" = 'Create') THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Category", "DisplayName", "Description", "IsSystem", "CreatedAt")
        VALUES (117, 'Sessions', 'Create', 'Gestión de Clientes', 'Sessions - Create', 'Crear nuevas sesiones', false, NOW());
    END IF;

    -- Asignar permisos al rol Admin (ID = 1) si no están asignados
    IF NOT EXISTS (SELECT 1 FROM "RolePermissions" WHERE "RoleId" = 1 AND "PermissionId" = 115) THEN
        INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
        VALUES (1, 115);
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "RolePermissions" WHERE "RoleId" = 1 AND "PermissionId" = 116) THEN
        INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
        VALUES (1, 116);
    END IF;

    IF NOT EXISTS (SELECT 1 FROM "RolePermissions" WHERE "RoleId" = 1 AND "PermissionId" = 117) THEN
        INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
        VALUES (1, 117);
    END IF;

    -- Opcional: Asignar permisos de lectura a otros roles
    -- Por ejemplo, para el rol User (ID = 2)
    /*
    IF NOT EXISTS (SELECT 1 FROM "RolePermissions" WHERE "RoleId" = 2 AND "PermissionId" = 115) THEN
        INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
        VALUES (2, 115);
    END IF;
    */
END $$;

-- Verificar que los permisos se crearon correctamente
SELECT * FROM "Permissions" WHERE "ModuleName" = 'Sessions';

-- Verificar que los permisos están asignados a los roles
SELECT r."Name" as "RoleName", p."ModuleName", p."ActionName", p."DisplayName"
FROM "RolePermissions" rp
JOIN "Roles" r ON rp."RoleId" = r."Id"
JOIN "Permissions" p ON rp."PermissionId" = p."Id"
WHERE p."ModuleName" = 'Sessions'
ORDER BY r."Name", p."ActionName";