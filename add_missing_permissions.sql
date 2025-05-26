-- Script para agregar permisos faltantes
-- Ejecuta este script en tu base de datos PostgreSQL

-- Agregar permisos para Analytics
INSERT INTO "Permissions" ("ModuleName", "ActionName", "Description", "Category")
VALUES 
    ('Analytics', 'Read', 'Ver y listar en Analytics', 'Configuración')
ON CONFLICT ("ModuleName", "ActionName") DO NOTHING;

-- Verificar que todos los permisos existan
SELECT "ModuleName", "ActionName", "Description" 
FROM "Permissions" 
ORDER BY "ModuleName", "ActionName";