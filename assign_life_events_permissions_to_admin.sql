-- Script para asignar permisos de LifeEvents al rol Admin
-- Ejecutar DESPUÉS de add_life_events_permissions.sql

-- Asignar todos los permisos de LifeEvents al rol Admin (RoleId = 1)
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 1, p."Id"
FROM "Permissions" p
WHERE p."ModuleName" = 'LifeEvents'
AND NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 1 AND rp."PermissionId" = p."Id"
);

-- Verificar que se asignaron correctamente
SELECT 
    p."Id",
    p."ModuleName",
    p."ActionName",
    p."Description",
    CASE WHEN rp."PermissionId" IS NOT NULL THEN 'Asignado' ELSE 'No Asignado' END as "Estado"
FROM "Permissions" p
LEFT JOIN "RolePermissions" rp ON p."Id" = rp."PermissionId" AND rp."RoleId" = 1
WHERE p."ModuleName" = 'LifeEvents'
ORDER BY p."Id";