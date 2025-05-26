-- Script para verificar permisos de roles
-- Ejecuta este script en tu base de datos PostgreSQL

-- 1. Ver todos los roles
SELECT * FROM "Roles";

-- 2. Ver todos los permisos
SELECT * FROM "Permissions" ORDER BY "ModuleName", "ActionName";

-- 3. Ver las relaciones entre roles y permisos
SELECT 
    r."Name" as "RoleName",
    p."ModuleName",
    p."ActionName",
    p."Description"
FROM "RolePermissions" rp
INNER JOIN "Roles" r ON rp."RoleId" = r."Id"
INNER JOIN "Permissions" p ON rp."PermissionId" = p."Id"
ORDER BY r."Name", p."ModuleName", p."ActionName";

-- 4. Contar permisos por rol
SELECT 
    r."Name" as "RoleName",
    COUNT(*) as "TotalPermissions"
FROM "RolePermissions" rp
INNER JOIN "Roles" r ON rp."RoleId" = r."Id"
GROUP BY r."Name";

-- 5. Si no hay relaciones, este query debería mostrar 0
SELECT COUNT(*) as "TotalRolePermissions" FROM "RolePermissions";