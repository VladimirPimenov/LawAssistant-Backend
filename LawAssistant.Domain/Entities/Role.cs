namespace LawAssistant.Domain.Entities
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Название роли (для авторизации)
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// Название роли на русском
        /// </summary>
        public string RoleNameRus { get; set; } 
    }
}