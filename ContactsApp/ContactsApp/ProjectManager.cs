using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ContactsApp
{
    /// <summary>
    /// Класс описывающий сохранение данных в файл и загрузки из него.
    /// </summary>
    public class ProjectManager
    {
        /// <summary>
        /// Путь по умолчанию по которому сохраняется файл.
        /// </summary>
        public static string FilePath()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\ContactsApp\Contacts.json";
        }

        /// <summary>
        /// Путь по умолчанию по которому создается папка для файла.
        /// </summary>
        public static string DirectoryPath()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path + @"\ContactsApp\";
        }

        /// <summary>
        /// Метод сохранения данных в файл.
        /// </summary>
        /// <param name="data">Данные для сериализации.</param>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="directoryPath">Путь до папки.</param>
        public static void SaveToFile(Project data, string filePath, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var serializer = new JsonSerializer();
            using (var streamwriter = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(streamwriter))
            {
                serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// Метод загрузки данных из файла.
        /// </summary>
        /// /// <param name="filepath">Путь до файла</param>
        public static Project LoadFromFile(string filepath)
        {
            Project project;
            if (!File.Exists(filepath))
            {
                return new Project();
            }

            var serializer = new JsonSerializer();
            try
            {
                using (var sr = new StreamReader(filepath))
                using (JsonReader reader = new JsonTextReader(sr))
                    project = serializer.Deserialize<Project>(reader);
            }
            catch
            {
                return new Project();
            }

            return project;
        }
    }
}

