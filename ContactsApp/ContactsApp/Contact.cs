using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    /// <summary>
    /// Класс описывающий контакт.
    /// </summary>
    public class Contact :ICloneable
    {
        /// <summary>
        /// Возвращает и задаёт фамилию.
        /// </summary>
        private string _surname;

        /// <summary>
        /// Возвращает и задаёт имя.
        /// </summary>
        private string _name;

        /// <summary>
        /// Возвращает и задаёт дату рождения.
        /// </summary>
        private DateTime _dateOfBirth = DateTime.Today;

        /// <summary>
        /// Возвращает и задаёт e-mail.
        /// </summary>
        private string _email;

        /// <summary>
        /// Возвращает и задаёт ID личной страницы Вконтакте.
        /// </summary>
        private string _idVk;

        /// <summary>
        /// Свойство даты рождения.
        /// </summary>
        public DateTime DateOfBirth
        {
            get 
            { 
                return _dateOfBirth; 
            }
            set
            {
                if (value >= DateTime.Now || value.Year <= 1900)
                {
                    throw new ArgumentException(@"Date of birth cannot be more than 
                    the current date and cannot be less than 1900");
                }

                _dateOfBirth = value;
            }
        }

        /// <summary>
        /// Свойство ID Вконтакте.
        /// </summary>
        public string IdVk
        {
            get
            {
                return _idVk;
            }
            set
            {
                if (value.Length > 30)
                {
                    throw new ArgumentException("ID_vk must not exceed 30 characters");
                }

                _idVk = value;
            }
        }

        /// <summary>
        /// Метод для дублированного кода
        /// Первая буква - заглавная  
        /// Ограничение 50 символами по длине строки.
        /// </summary>
        /// <param name="initials"></param>
        /// <returns></returns>
        public static string WordInput(string initials, string propertyName)
        {
            if (initials.Length > 50)
            {
                throw new ArgumentException($"{propertyName} must not exceed 50 characters");
            }

            if (initials.Length == 0)
            {
                throw new ArgumentException($"{propertyName} is not entered");
            }

            initials = char.ToUpper(initials[0]) + initials.Substring(1);
            return initials;
        }

        /// <summary>
        /// Свойство фамилии.
        /// </summary>
        public string Surname
        {
            get 
            { 
                return _surname;
            } 
            set
            {
                _surname = WordInput(value, nameof(Surname));
            }
        }

        /// <summary>
        /// Свойство имени.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = WordInput(value, nameof(Name));
            }
        }

        /// <summary>
        /// Свойство е-мейла.
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value.Length > 50)
                {
                    throw new ArgumentException("e-mail must not exceed 50 characters");
                }

                _email = value;
            }
        }
        
        /// <summary>
        /// Возвращает и задаёт номер телефона.
        /// </summary>
        public PhoneNumber PhoneNumber { get; set; }

        /// <summary>
        /// Реализация клонирования
        /// </summary>
        /// <returns>Возвращает объект - клон контакта, с полями: номер телефона, имя, фамилия, емейл, дата рождения, айди вк.</returns>
        public object Clone()
        {
            var phoneNumber = new PhoneNumber { Number = PhoneNumber.Number };
            return new Contact
            {
                Surname = Surname,
                Name = Name,
                DateOfBirth = DateOfBirth,
                Email = Email,
                IdVk = IdVk,
                PhoneNumber = phoneNumber
            };
        }

        public override bool Equals(object obj)
        {
            var toCompareWith = obj as Contact;
            if (toCompareWith == null)
            {
                return false;
            }

            return Name == toCompareWith.Name &&
                   Surname == toCompareWith.Surname &&
                   PhoneNumber.Number == toCompareWith.PhoneNumber.Number &&
                   IdVk == toCompareWith.IdVk &&
                   Email == toCompareWith.Email &&
                   DateOfBirth == toCompareWith.DateOfBirth;
        }
    }
}
