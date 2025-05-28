-- Script para agregar TODOS los permisos existentes al rol Administrador (RoleId = 2)
-- Solo inserta los permisos que NO tenga ya asignados

-- Insertar permisos faltantes para el rol Administrador
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 2, p."Id"
FROM "Permissions" p
WHERE NOT EXISTS (
    SELECT 1 
    FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 2 
    AND rp."PermissionId" = p."Id"
);

-- Verificar total de permisos asignados al administrador después de la inserción
SELECT 
    'Total de permisos en el sistema' as "Descripcion",
    COUNT(*) as "Cantidad"
FROM "Permissions"
UNION ALL
SELECT 
    'Permisos asignados al Administrador' as "Descripcion",
    COUNT(*) as "Cantidad"
FROM "RolePermissions" 
WHERE "RoleId" = 2;

-- Listar todos los módulos y permisos del administrador
SELECT 
    p."Module",
    COUNT(*) as "CantidadPermisos"
FROM "Permissions" p
INNER JOIN "RolePermissions" rp ON p."Id" = rp."PermissionId"
WHERE rp."RoleId" = 2
GROUP BY p."Module"
ORDER BY p."Module";