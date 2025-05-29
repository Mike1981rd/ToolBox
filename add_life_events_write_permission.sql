-- Script para agregar SOLO el permiso Write de LifeEvents
-- ID fijo: 71

INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
VALUES (71, 'LifeEvents', 'Write', 'Editar eventos de línea de vida', 'Personal Tools');