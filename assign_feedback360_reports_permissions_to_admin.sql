-- Script para asignar permisos de Feedback 360 Reports al rol Admin
-- Este script debe ejecutarse DESPUÉS de add_feedback360_reports_permissions.sql

-- Verificar los IDs de los roles (para confirmar que Admin es ID 2)
SELECT "Id", "Name" FROM "Roles" ORDER BY "Id";

-- Asignar permisos de Feedback360Reports al rol Admin (ID = 2)
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 2, p."Id"
FROM "Permissions" p
WHERE p."ModuleName" IN ('Feedback360Reports', 'ClientFeedback360Reports')
AND NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 2 AND rp."PermissionId" = p."Id"
);

-- Opcional: Si también quieres asignar permisos de cliente a un rol específico (como Cliente o Usuario)
-- Descomenta las siguientes líneas si es necesario
/*
-- Asignar solo permisos de ClientFeedback360Reports al rol Cliente (ajusta el ID según tu configuración)
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 3, p."Id"  -- Ajusta el ID según tu configuración de roles
FROM "Permissions" p
WHERE p."ModuleName" = 'ClientFeedback360Reports'
AND NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 3 AND rp."PermissionId" = p."Id"
);
*/

-- Verificar permisos asignados al rol Admin para los módulos de reportes
SELECT 
    r."Name" as "Rol",
    p."ModuleName" as "Módulo",
    p."ActionName" as "Acción",
    p."Description" as "Descripción"
FROM "RolePermissions" rp
INNER JOIN "Roles" r ON rp."RoleId" = r."Id"
INNER JOIN "Permissions" p ON rp."PermissionId" = p."Id"
WHERE r."Id" = 2 AND p."ModuleName" IN ('Feedback360Reports', 'ClientFeedback360Reports')
ORDER BY p."ModuleName", p."Id";

-- Verificar cuántos permisos tiene el rol Admin en total
SELECT 
    r."Name" as "Rol",
    COUNT(*) as "Total de Permisos"
FROM "RolePermissions" rp
INNER JOIN "Roles" r ON rp."RoleId" = r."Id"
WHERE r."Id" = 2
GROUP BY r."Name";