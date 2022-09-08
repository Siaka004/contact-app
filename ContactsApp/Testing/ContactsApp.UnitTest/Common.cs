using System;
using System.IO;
using System.Reflection;


namespace ContactsApp.UnitTest
{
    /// <summary>
    /// Класс генерации данных
    /// </summary>
    class Common
    {
        /// <summary>
        /// Обращение к пути
        /// </summary>
        public static string DataFolderForTest()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(location) + @"\TestData";
        }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public static string FilePath()
        {
            var path = DirectoryPath();
            return path + @"Contacts.json";
        }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public static string DirectoryPath()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\ContactsApp\";
        }
    }
}
