-- Script para eliminar las sesiones existentes antes de aplicar la migración

-- Primero eliminar los archivos de sesión relacionados
DELETE FROM "SessionFiles";

-- Luego eliminar todas las sesiones
DELETE FROM "Sessions";

-- Verificar que no queden registros
SELECT COUNT(*) as total_sessions FROM "Sessions";
SELECT COUNT(*) as total_files FROM "SessionFiles";