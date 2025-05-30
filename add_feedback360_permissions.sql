-- Script para agregar permisos del módulo Feedback 360
-- Último ID de permiso existente: 77

DO $$
DECLARE
    next_id INTEGER;
BEGIN
    -- Permiso Read para Feedback360
    SELECT COALESCE(MAX("Id"), 77) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'Feedback360' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'Feedback360', 'Read', 'Ver y listar procesos de Feedback 360°', 'Herramientas de Evaluación');
    END IF;

    -- Permiso Write para Feedback360
    SELECT COALESCE(MAX("Id"), 77) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'Feedback360' AND "ActionName" = 'Write'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'Feedback360', 'Write', 'Editar y gestionar evaluadores en procesos de Feedback 360°', 'Herramientas de Evaluación');
    END IF;

    -- Permiso Create para Feedback360
    SELECT COALESCE(MAX("Id"), 77) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'Feedback360' AND "ActionName" = 'Create'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'Feedback360', 'Create', 'Crear nuevos procesos de Feedback 360°', 'Herramientas de Evaluación');
    END IF;
END $$;

-- Verificar permisos creados
SELECT * FROM "Permissions" 
WHERE "ModuleName" = 'Feedback360'
ORDER BY "Id";

-- Mostrar el último ID usado
SELECT MAX("Id") as "Último ID usado" FROM "Permissions";