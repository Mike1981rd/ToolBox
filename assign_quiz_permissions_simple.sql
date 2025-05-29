-- Script simple para asignar permisos de QuestionnaireTemplates al rol Admin (ID=2)

-- Asignar los permisos al rol Admin
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 
    2, -- Rol Admin
    p."Id"
FROM "Permissions" p
WHERE p."ModuleName" = 'QuestionnaireTemplates'
AND NOT EXISTS (
    SELECT 1 
    FROM "RolePermissions" rp 
    WHERE rp."RoleId" = 2 
    AND rp."PermissionId" = p."Id"
);