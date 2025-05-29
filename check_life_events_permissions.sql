-- Script para verificar permisos de LifeEvents

-- 1. Verificar que los permisos fueron creados
SELECT 
    "Id",
    "ModuleName",
    "ActionName", 
    "Description",
    "Category"
FROM "Permissions" 
WHERE "ModuleName" = 'LifeEvents'
ORDER BY "Id";

-- 2. Verificar asignaciones de roles
SELECT 
    r."Name" as "RoleName",
    p."ModuleName",
    p."ActionName",
    p."Description"
FROM "RolePermissions" rp
INNER JOIN "Roles" r ON rp."RoleId" = r."Id"
INNER JOIN "Permissions" p ON rp."PermissionId" = p."Id"
WHERE p."ModuleName" = 'LifeEvents'
ORDER BY r."Name", p."ActionName";

-- 3. Contar permisos por módulo para verificar consistencia
SELECT 
    "ModuleName",
    COUNT(*) as "TotalPermisos"
FROM "Permissions"
WHERE "ModuleName" IN ('LifeEvents', 'Users', 'Dashboard', 'LifeAreas')
GROUP BY "ModuleName"
ORDER BY "ModuleName";

-- 4. Verificar rango de IDs usado
SELECT 
    MIN("Id") as "MinId",
    MAX("Id") as "MaxId",
    COUNT(*) as "TotalPermissions"
FROM "Permissions";