using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    /// <summary>
    /// Класс описывающий номера.
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// Номер телефона.
        /// </summary>
        private long _number;

        /// <summary>
        /// Свойство номера телефона.
        /// </summary>
        public long Number
        {
            get => _number;
            set
            {
                if (value.ToString().Length != 11 || value.ToString()[0] != '7')
                {
                    throw new ArgumentException(message: "Phone number must start with 7 and be 11 digits long");
                }
                _number = value;
            }
        }
    }
}