-- Script para agregar permisos del módulo Quiz/QuestionnaireTemplates
-- Este script es seguro porque:
-- 1. Verifica el último ID antes de insertar
-- 2. Solo inserta si no existen los permisos
-- 3. Usa el ID correcto del rol Admin (2)

DO $$
DECLARE
    next_id INTEGER;
BEGIN
    -- Obtener el siguiente ID disponible
    SELECT COALESCE(MAX("Id"), 0) + 1 INTO next_id FROM "Permissions";
    
    -- Mostrar el ID que se usará (para verificación)
    RAISE NOTICE 'Siguiente ID disponible para permisos: %', next_id;
    
    -- Verificar si ya existen permisos para QuestionnaireTemplates
    IF EXISTS (SELECT 1 FROM "Permissions" WHERE "ModuleName" = 'QuestionnaireTemplates') THEN
        RAISE NOTICE 'Los permisos para QuestionnaireTemplates ya existen. No se insertarán duplicados.';
    ELSE
        -- Insertar los tres permisos básicos
        INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
        VALUES 
            (next_id, 'QuestionnaireTemplates', 'Read', 'Ver y listar en Cuestionarios', 'Herramientas de Evaluación'),
            (next_id + 1, 'QuestionnaireTemplates', 'Write', 'Editar y actualizar en Cuestionarios', 'Herramientas de Evaluación'),
            (next_id + 2, 'QuestionnaireTemplates', 'Create', 'Crear nuevos registros en Cuestionarios', 'Herramientas de Evaluación');
            
        RAISE NOTICE 'Permisos creados exitosamente con IDs: %, %, %', next_id, next_id + 1, next_id + 2;
    END IF;
END $$;

-- Asignar permisos al rol Admin (ID=2)
DO $$
DECLARE
    permission_record RECORD;
    inserted_count INTEGER := 0;
BEGIN
    -- Para cada permiso de QuestionnaireTemplates
    FOR permission_record IN 
        SELECT "Id" FROM "Permissions" WHERE "ModuleName" = 'QuestionnaireTemplates'
    LOOP
        -- Verificar si ya existe la asignación
        IF NOT EXISTS (
            SELECT 1 FROM "RolePermissions" 
            WHERE "RoleId" = 2 AND "PermissionId" = permission_record."Id"
        ) THEN
            -- Insertar la asignación
            INSERT INTO "RolePermissions" ("RoleId", "PermissionId")
            VALUES (2, permission_record."Id");
            
            inserted_count := inserted_count + 1;
        END IF;
    END LOOP;
    
    IF inserted_count > 0 THEN
        RAISE NOTICE 'Se asignaron % permisos al rol Admin', inserted_count;
    ELSE
        RAISE NOTICE 'Los permisos ya estaban asignados al rol Admin';
    END IF;
END $$;

-- Verificar que todo se creó correctamente
SELECT 
    p."Id",
    p."ModuleName",
    p."ActionName",
    p."Description",
    p."Category",
    CASE WHEN rp."RoleId" IS NOT NULL THEN 'Sí' ELSE 'No' END AS "Asignado a Admin"
FROM "Permissions" p
LEFT JOIN "RolePermissions" rp ON p."Id" = rp."PermissionId" AND rp."RoleId" = 2
WHERE p."ModuleName" = 'QuestionnaireTemplates'
ORDER BY p."Id";