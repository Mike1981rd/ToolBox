-- Script para agregar SOLO el permiso Read de LifeEvents
-- ID fijo: 69

INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
VALUES (69, 'LifeEvents', 'Read', 'Ver y listar eventos de línea de vida', 'Personal Tools');