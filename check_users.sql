-- Script para verificar usuarios en la base de datos
-- Ejecuta este script en tu base de datos PostgreSQL para ver los usuarios

-- Ver todos los usuarios con sus campos importantes
SELECT 
    "Id",
    "FullName",
    "UserName",
    "Email",
    "IsActive",
    LEFT("PasswordHash", 10) as "PasswordStart",
    "RoleId"
FROM "Users"
ORDER BY "Id";

-- Verificar si hay usuarios sin UserName
SELECT COUNT(*) as "UsersWithoutUserName"
FROM "Users"
WHERE "UserName" IS NULL OR "UserName" = '';

-- Ver ejemplo de un usuario activo
SELECT *
FROM "Users"
WHERE "IsActive" = true
LIMIT 1;

-- Para actualizar un usuario específico con username (ejemplo)
-- UPDATE "Users" 
-- SET "UserName" = 'admin' 
-- WHERE "Email" = 'admin@example.com';

-- Para crear un usuario de prueba con contraseña simple
-- INSERT INTO "Users" ("FullName", "UserName", "Email", "PasswordHash", "IsActive", "RoleId", "CreatedAt", "UpdatedAt")
-- VALUES ('Test Admin', 'admin', 'admin@test.com', 'password123', true, 1, NOW(), NOW());