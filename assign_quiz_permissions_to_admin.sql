-- Script para asignar permisos de QuestionnaireTemplates al rol Admin (ID=2)

-- Primero, verificar qué permisos existen para QuestionnaireTemplates
SELECT 
    "Id", 
    "ModuleName", 
    "ActionName", 
    "Description" 
FROM "Permissions" 
WHERE "ModuleName" = 'QuestionnaireTemplates'
ORDER BY "Id";

-- Asignar todos los permisos de QuestionnaireTemplates al rol Admin
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 
    2 as "RoleId", -- Rol Admin
    p."Id" as "PermissionId"
FROM "Permissions" p
WHERE p."ModuleName" = 'QuestionnaireTemplates'
AND NOT EXISTS (
    -- Solo insertar si no existe ya la asignación
    SELECT 1 
    FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 2 
    AND rp."PermissionId" = p."Id"
)
ORDER BY p."Id";

-- Verificar cuántas asignaciones se crearon
DO $$
DECLARE
    assigned_count INTEGER;
BEGIN
    SELECT COUNT(*) INTO assigned_count
    FROM "RolePermissions" rp
    INNER JOIN "Permissions" p ON rp."PermissionId" = p."Id"
    WHERE rp."RoleId" = 2 
    AND p."ModuleName" = 'QuestionnaireTemplates';
    
    RAISE NOTICE 'Total de permisos de QuestionnaireTemplates asignados al rol Admin: %', assigned_count;
END $$;

-- Mostrar resultado final: todos los permisos de QuestionnaireTemplates y su estado de asignación
SELECT 
    p."Id" as "Permission ID",
    p."ActionName" as "Acción",
    p."Description" as "Descripción",
    CASE 
        WHEN rp."RoleId" IS NOT NULL THEN '✓ Asignado'
        ELSE '✗ No asignado'
    END as "Estado para Admin"
FROM "Permissions" p
LEFT JOIN "RolePermissions" rp ON p."Id" = rp."PermissionId" AND rp."RoleId" = 2
WHERE p."ModuleName" = 'QuestionnaireTemplates'
ORDER BY p."Id";