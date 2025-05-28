-- Script to add Calendar module permissions
-- This script calculates the next available ID to avoid conflicts

DO $$
DECLARE
    next_id INTEGER;
BEGIN
    -- Get the next available permission ID
    SELECT COALESCE(MAX("Id"), 0) + 1 INTO next_id FROM "Permissions";
    
    -- Insert Calendar permissions
    INSERT INTO "Permissions" ("Id", "Name", "Description", "Module", "CreatedAt", "UpdatedAt") VALUES
    (next_id, 'calendar.view', 'View calendar and events', 'Calendar', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (next_id + 1, 'calendar.create', 'Create calendar events', 'Calendar', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (next_id + 2, 'calendar.edit', 'Edit calendar events', 'Calendar', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (next_id + 3, 'calendar.delete', 'Delete calendar events', 'Calendar', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
    
    -- Grant all calendar permissions to Admin role (RoleId = 1)
    INSERT INTO "RolePermissions" ("RoleId", "PermissionId") VALUES
    (1, next_id),
    (1, next_id + 1),
    (1, next_id + 2),
    (1, next_id + 3);
    
    -- Grant view and create permissions to User role (RoleId = 2)
    INSERT INTO "RolePermissions" ("RoleId", "PermissionId") VALUES
    (2, next_id),
    (2, next_id + 1);
    
    RAISE NOTICE 'Calendar permissions added successfully with IDs % to %', next_id, next_id + 3;
END $$;