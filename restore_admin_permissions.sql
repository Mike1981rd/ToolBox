-- Script para restaurar TODOS los permisos al rol administrador (RoleId = 2)

-- Primero, eliminar permisos existentes del administrador para evitar duplicados
DELETE FROM "RolePermissions" WHERE "RoleId" = 2;

-- Luego, insertar TODOS los permisos para el administrador
INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
SELECT 2, "Id" FROM "Permissions";

-- Verificar resultado
SELECT COUNT(*) as "PermisosAsignados" FROM "RolePermissions" WHERE "RoleId" = 2;
SELECT COUNT(*) as "TotalPermisos" FROM "Permissions";