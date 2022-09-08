using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    /// <summary>
    /// Класс содержит список всех контактов.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Лист, который хранит в себе список контактов
        /// </summary>>
        public List<Contact> Contacts = new List<Contact>();

        /// <summary>
        /// Сортировка листа.
        /// </summary>
        /// <param name="contacts"> Лист для сортировки.</param>
        /// <returns></returns>
        public List<Contact> Sort(List<Contact> contacts)
        {
            var sortedContacts = from u in contacts orderby u.Surname select u;
            return sortedContacts.ToList();
        }

        /// <summary>
        /// Поиск контакта по строке.
        /// </summary>
        /// <param name="substringForSearch"> Строка по которой ведется поиск.</param>
        /// <param name="contacts"> Список контактов для поиска.</param>
        /// <returns></returns>
        public List<Contact> SortContacts(string substringForSearch)
        {
            var findProject = Contacts;
            if (substringForSearch == "")
            {
                return findProject;
            }

            findProject = Contacts.Where(contact =>
                contact.Surname.StartsWith(substringForSearch, StringComparison.OrdinalIgnoreCase) ||
                contact.Name.StartsWith(substringForSearch, StringComparison.OrdinalIgnoreCase)).ToList();
            if (findProject.Count == 0)
            {
                return findProject;
            }

            findProject = Sort(findProject);
            return findProject;
        }
    }
}
