-- Verificar qué usuarios tienen cuestionarios asignados
SELECT 
    qi."ClientId",
    u."FullName" as "ClientName",
    u."Email" as "ClientEmail",
    qt."Title" as "QuestionnaireTitle",
    qi."Status",
    qi."AssignedAt",
    qi."AssignedByCoachId"
FROM "QuestionnaireInstances" qi
INNER JOIN "Users" u ON qi."ClientId" = u."Id"
INNER JOIN "QuestionnaireTemplates" qt ON qi."QuestionnaireTemplateId" = qt."Id"
ORDER BY qi."AssignedAt" DESC;

-- Contar cuántos cuestionarios tiene cada usuario
SELECT 
    u."Id",
    u."FullName",
    u."Email",
    COUNT(qi."Id") as "TotalQuestionnaires"
FROM "Users" u
LEFT JOIN "QuestionnaireInstances" qi ON u."Id" = qi."ClientId"
GROUP BY u."Id", u."FullName", u."Email"
HAVING COUNT(qi."Id") > 0
ORDER BY COUNT(qi."Id") DESC;

-- Ver específicamente qué tiene el usuario con ID=1
SELECT 
    qi.*,
    qt."Title"
FROM "QuestionnaireInstances" qi
INNER JOIN "QuestionnaireTemplates" qt ON qi."QuestionnaireTemplateId" = qt."Id"
WHERE qi."ClientId" = 1;