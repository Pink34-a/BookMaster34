using BookMaster34.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookMaster34.AppData
{
    public static class CredentialService
    {
        #region Хранение пользователя (в оперативной памяти) 
        private const string KeyCurrentAdministrator = "Administrator";
        /// <summary>
        /// Текущий авторизованный администратор. Хранит данные пока приложение работает
        /// </summary>
        public static Administrator? Administrator
        {
            get => Application.Current.Properties["KeyCurrentAdministrator"] as Administrator;
            set => Application.Current.Properties["KeyCurrentAdministrator"] = value;
        }
        /// <summary>
        /// Проверяет хранится ли текущий администратор в памяти
        /// </summary>
        public static bool HasAdministrator => Administrator != null;
        /// <summary>
        /// Очищает список свойств в текущем приложении
        /// </summary>

        public static void ClearAdministrator() => Application.Current.Properties.Remove(KeyCurrentAdministrator);
        #endregion

        #region Постоянное хранение данных пользователя (в настройках проекта)
        /// <summary>
        /// Сохраненный логин администратора
        /// </summary>
        public static string? SavedLogin
        {
            get => Properties.Settings.Default.SavedLogin;
            set => Properties.Settings.Default.SavedLogin = value;
        }
        /// <summary>
        /// Сохраненный пароль администратора
        /// </summary>
        public static string? SavedPassword
        {
            get => Properties.Settings.Default.SavedPassword;
            set => Properties.Settings.Default.SavedPassword = value;

        }
        /// <summary>
        /// Флаг включения автозаполнения
        /// </summary>
        public static bool AutoLogin
        {
            get => Properties.Settings.Default.AutoLogin;
            set => Properties.Settings.Default.AutoLogin = value;

        }

        /// <summary>
        /// сохранение учетных данных на диск.
        /// </summary>
        public static void SaveCredentials(string login, string password)
        {
            SavedLogin = login;
            SavedPassword = password;
            AutoLogin = true;
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// очистка учетных данных на диске.
        /// </summary>
        public static void ClearCredentials()
        {
            SavedLogin = string.Empty;
            SavedPassword = string.Empty;
            AutoLogin = false;
            Properties.Settings.Default.Save();
            #endregion 
        }
    }
}
