using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    /// <summary>
    /// Класс добавления и редактирования контакта
    /// </summary>
    public partial class ContactForm : Form
    {
        private Contact _contact;

        /// <summary>
        /// Временный проект для добавления или редактирования контакта.
        /// </summary>
        public Contact Contact
        {
            get => _contact;
            set
            {
                _contact = value;
                surnameTextBox.Text = value.Surname;
                nameTextBox.Text = value.Name;
                idVkTextBox.Text = value.IdVk;
                emailTextBox.Text = value.Email;
                if (value.PhoneNumber.Number != 0)
                    phoneTextBox.Text = value.PhoneNumber.Number.ToString();
                DateOfBirthPicker.Value = value.DateOfBirth;
            }
        }

        public ContactForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Действие при нажатии кнопки Ok.
        /// </summary>
        private void okbutton_Click(object sender, EventArgs e)
        {
            NewContact();
        }

        /// <summary>
        /// Создание контакта для добавления или редактирования.
        /// </summary>
        private void NewContact()
        {
            try
            {
                Contact.Surname = surnameTextBox.Text;
                Contact.Name = nameTextBox.Text;
                Contact.IdVk = idVkTextBox.Text;
                Contact.Email = emailTextBox.Text;
                Contact.DateOfBirth = DateOfBirthPicker.Value;
                var phoneNumber = new PhoneNumber
                {
                    Number = phoneTextBox.Text != "" ? Convert.ToInt64(phoneTextBox.Text) : 0
                };

                Contact.PhoneNumber = phoneNumber;
                DialogResult = DialogResult.OK;
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Действие при нажатии кнопки Cancel.
        /// </summary>
        private void cancelbutton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Действие при закрытии формы.
        /// </summary>
        private void ContactOperationsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Cancel) return;
            if (surnameTextBox.Text == "" && surnameTextBox.Text == "" && phoneTextBox.Text == "" &&
                emailTextBox.Text == "" && idVkTextBox.Text == "")
            {
                return;
            }

            var dialogAnswer = MessageBox.Show(@"The entered data will not be saved.",
                @"Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogAnswer == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Изменение цвета при попытке неправильного ввода данных Фамилия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void surnameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var surname = new Contact();
                surname.Surname = surnameTextBox.Text;
                surnameTextBox.BackColor = Color.White;
            }

            catch (ArgumentException exception)
            {
                surnameTextBox.BackColor = Color.LightSalmon;
            }         
        }

        /// <summary>
        /// Изменение цвета при попытке неправильного ввода данных Имя
        /// </summary>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var name = new Contact();
                name.Name = nameTextBox.Text;
                nameTextBox.BackColor = Color.White;
            }

            catch (ArgumentException exception)
            {
                nameTextBox.BackColor = Color.LightSalmon;
            }
        }

        /// <summary>
        /// Изменение цвета при попытке неправильного ввода данных Email
        /// </summary>
        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var email = new Contact();
                email.Email = emailTextBox.Text;
                emailTextBox.BackColor = Color.White;
            }

            catch (ArgumentException exception)
            {
                emailTextBox.BackColor = Color.LightSalmon;
            }
        }

        private void phoneTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long number;
                long.TryParse(phoneTextBox.Text, out number);
                _contact.PhoneNumber.Number = number;
                phoneTextBox.BackColor = Color.White;
            }

            catch (ArgumentException exception)
            {
                phoneTextBox.BackColor = Color.LightSalmon;
            }
        }

        private void idVkTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var IdVk = new Contact();
                IdVk.IdVk = idVkTextBox.Text;
                idVkTextBox.BackColor = Color.White;
            }

            catch (ArgumentException exception)
            {
                idVkTextBox.BackColor = Color.LightSalmon;
            }
        }
    }
}
