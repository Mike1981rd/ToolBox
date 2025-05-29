-- Script para agregar SOLO el permiso Create de LifeEvents
-- ID fijo: 70

INSERT INTO "Permissions" ("Id", "ModuleName", "ActionName", "Description", "Category")
VALUES (70, 'LifeEvents', 'Create', 'Crear nuevos eventos de línea de vida', 'Personal Tools');