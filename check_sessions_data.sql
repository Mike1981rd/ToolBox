-- Verificar qué sesiones existen y sus ClientIds
SELECT s."Id", s."ClientId", c."FirstName", c."LastName", c."Email"
FROM "Sessions" s
LEFT JOIN "Customers" c ON s."ClientId" = c."Id"
ORDER BY s."Id";

-- Verificar qué usuarios existen
SELECT "Id", "FullName", "Email" 
FROM "Users" 
ORDER BY "Id";

-- Ver si hay correspondencia entre Customers y Users por email
SELECT 
    c."Id" as customer_id, 
    c."FirstName" || ' ' || c."LastName" as customer_name,
    c."Email" as customer_email,
    u."Id" as user_id,
    u."FullName" as user_name,
    u."Email" as user_email
FROM "Customers" c
LEFT JOIN "Users" u ON c."Email" = u."Email"
WHERE c."Id" IN (SELECT DISTINCT "ClientId" FROM "Sessions");