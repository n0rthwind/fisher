using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FISHER
{
    /// <summary>
    /// Отслеживает права пользователей на управления объектами
    /// </summary>
    public static class Authorization
    {
        /// <summary>
        /// Список пользователей с полными правами для доступа 
        /// </summary>
        public static List<string> RootNames = new List<string>();

        /// <summary>
        /// Возвращает уровень доступа для клента 
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns>Уровень доступа (0 - полный доступ, 1 - только просмотр)</returns>
        public static int RulesId(string name)
        {
            if (RootNames.IndexOf(name) > -1) return 0;
            return 1;
        }
    }
}
