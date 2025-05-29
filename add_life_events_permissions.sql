-- Script para agregar permisos del módulo LifeEvents
-- Último ID verificado: 68
-- IDs a usar: 69, 70, 71

DO $$
DECLARE
    next_id INTEGER;
BEGIN
    -- Permiso Read para LifeEvents (ID: 69)
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'LifeEvents' AND "ActionName" = 'Read'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (69, 'LifeEvents', 'Read', 'Ver y listar eventos de línea de vida', 'Personal Tools');
        RAISE NOTICE 'Permiso LifeEvents.Read creado con ID 69';
    ELSE
        RAISE NOTICE 'Permiso LifeEvents.Read ya existe';
    END IF;

    -- Permiso Write para LifeEvents (ID: 70)
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'LifeEvents' AND "ActionName" = 'Write'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (70, 'LifeEvents', 'Write', 'Editar eventos de línea de vida', 'Personal Tools');
        RAISE NOTICE 'Permiso LifeEvents.Write creado con ID 70';
    ELSE
        RAISE NOTICE 'Permiso LifeEvents.Write ya existe';
    END IF;

    -- Permiso Create para LifeEvents (ID: 71)
    IF NOT EXISTS (
        SELECT 1 FROM "Permissions" 
        WHERE "ModuleName" = 'LifeEvents' AND "ActionName" = 'Create'
    ) THEN
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES (71, 'LifeEvents', 'Create', 'Crear nuevos eventos de línea de vida', 'Personal Tools');
        RAISE NOTICE 'Permiso LifeEvents.Create creado con ID 71';
    ELSE
        RAISE NOTICE 'Permiso LifeEvents.Create ya existe';
    END IF;

    RAISE NOTICE 'Permisos de LifeEvents procesados correctamente';
END $$;