-- Script para asignar permisos de Feedback 360 al rol Admin
-- Este script debe ejecutarse DESPUÉS de add_feedback360_permissions.sql

-- Verificar los IDs de los roles (para confirmar que Admin es ID 2)
SELECT "Id", "Name" FROM "Roles" ORDER BY "Id";

-- Asignar permisos de Feedback360 al rol Admin (ID = 2)
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 2, p."Id"
FROM "Permissions" p
WHERE p."ModuleName" = 'Feedback360'
AND NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 2 AND rp."PermissionId" = p."Id"
);

-- Opcional: Si también quieres asignar al rol Coach (si es diferente de Admin)
-- Descomenta las siguientes líneas si es necesario
/*
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 3, p."Id"  -- Ajusta el ID según tu configuración
FROM "Permissions" p
WHERE p."ModuleName" = 'Feedback360'
AND NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 3 AND rp."PermissionId" = p."Id"
);
*/

-- Verificar permisos asignados al rol Admin para el módulo Feedback360
SELECT 
    r."Name" as "Rol",
    p."ModuleName" as "Módulo",
    p."ActionName" as "Acción",
    p."Description" as "Descripción"
FROM "RolePermissions" rp
INNER JOIN "Roles" r ON rp."RoleId" = r."Id"
INNER JOIN "Permissions" p ON rp."PermissionId" = p."Id"
WHERE r."Id" = 2 AND p."ModuleName" = 'Feedback360'
ORDER BY p."Id";

-- Verificar cuántos permisos tiene el rol Admin en total
SELECT 
    r."Name" as "Rol",
    COUNT(*) as "Total de Permisos"
FROM "RolePermissions" rp
INNER JOIN "Roles" r ON rp."RoleId" = r."Id"
WHERE r."Id" = 2
GROUP BY r."Name";