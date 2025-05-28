-- Verificar permisos actuales del rol administrador (RoleId = 2)
SELECT 
    p."Module",
    p."Name",
    p."Description",
    CASE WHEN rp."RoleId" IS NOT NULL THEN 'SI' ELSE 'NO' END as "TienePermiso"
FROM "Permissions" p
LEFT JOIN "RolePermissions" rp ON p."Id" = rp."PermissionId" AND rp."RoleId" = 2
ORDER BY p."Module", p."Name";

-- Contar permisos por módulo
SELECT 
    p."Module",
    COUNT(p."Id") as "TotalPermisos",
    COUNT(rp."RoleId") as "PermisosAsignados"
FROM "Permissions" p
LEFT JOIN "RolePermissions" rp ON p."Id" = rp."PermissionId" AND rp."RoleId" = 2
GROUP BY p."Module"
ORDER BY p."Module";

-- Ver qué permisos NO tiene el administrador
SELECT 
    p."Id",
    p."Module",
    p."Name",
    p."Description"
FROM "Permissions" p
WHERE NOT EXISTS (
    SELECT 1 FROM "RolePermissions" rp 
    WHERE rp."PermissionId" = p."Id" AND rp."RoleId" = 2
)
ORDER BY p."Module", p."Name";