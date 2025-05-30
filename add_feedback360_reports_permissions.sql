-- Script para agregar permisos del módulo Feedback 360 Reports
-- Último ID de permiso existente: 80 (de Feedback360)

DO $$
DECLARE
    next_id INTEGER;
BEGIN
    -- Permiso Read para Feedback360Reports (Coach view)
    SELECT COALESCE(MAX("Id"), 80) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'Feedback360Reports' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'Feedback360Reports', 'Read', 'Ver reportes completos de Feedback 360° (vista de coach)', 'Herramientas de Evaluación');
    END IF;

    -- Permiso Write para Feedback360Reports (Manage/share reports)
    SELECT COALESCE(MAX("Id"), 80) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'Feedback360Reports' AND "ActionName" = 'Write'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'Feedback360Reports', 'Write', 'Gestionar y compartir reportes de Feedback 360°', 'Herramientas de Evaluación');
    END IF;

    -- Permiso Read para ClientFeedback360Reports (Client view)
    SELECT COALESCE(MAX("Id"), 80) + 1 INTO next_id FROM "Permissions";
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'ClientFeedback360Reports' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (next_id, 'ClientFeedback360Reports', 'Read', 'Ver mis propios reportes de Feedback 360° (vista de cliente)', 'Auto-gestión');
    END IF;
END $$;

-- Verificar permisos creados
SELECT * FROM "Permissions" 
WHERE "ModuleName" IN ('Feedback360Reports', 'ClientFeedback360Reports')
ORDER BY "Id";

-- Mostrar el último ID usado
SELECT MAX("Id") as "Último ID usado" FROM "Permissions";