using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToolBox.Interfaces
{
    public interface IPermissionService
    {
        /// <summary>
        /// Obtiene todos los permisos de un usuario basado en su rol
        /// </summary>
        /// <param name="userId">ID del usuario</param>
        /// <returns>Diccionario con módulo como clave y lista de acciones como valor</returns>
        Task<Dictionary<string, List<string>>> GetUserPermissionsAsync(int userId);

        /// <summary>
        /// Verifica si un usuario tiene un permiso específico
        /// </summary>
        /// <param name="userId">ID del usuario</param>
        /// <param name="module">Nombre del módulo</param>
        /// <param name="action">Nombre de la acción (Read, Write, Create)</param>
        /// <returns>True si tiene el permiso, false en caso contrario</returns>
        Task<bool> UserHasPermissionAsync(int userId, string module, string action);

        /// <summary>
        /// Obtiene los módulos que el usuario puede ver (tiene permiso Read)
        /// </summary>
        /// <param name="userId">ID del usuario</param>
        /// <returns>Lista de nombres de módulos</returns>
        Task<List<string>> GetUserReadableModulesAsync(int userId);
    }
}