using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ContactsApp;

namespace ContactsAppUI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Локальное хранилище контактов.
        /// </summary>
        private Project _project = new Project();

        /// <summary>
        /// Проект для поиска.
        /// </summary>
        private List<Contact> _viewedContacts = new List<Contact>();

        /// <summary>
        /// Путь к файлу.
        /// </summary>
        private readonly string _filePath = ProjectManager.FilePath();

        /// <summary>
        /// Путь к папке.
        /// </summary>
        private readonly string _directoryPath = ProjectManager.DirectoryPath();

        /// <summary>
        /// Загрузка данных.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Изменение текста в поле поиска.
        /// </summary>
        private void FindTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex >= 0)
            {
                var selectedContact = _viewedContacts[ContactsListBox.SelectedIndex];
                UpdateContactsList(selectedContact);
            }

            else
            {
                UpdateContactsList(null);
            }
        }

        /// <summary>
        /// Загрузка данных.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            _project = ProjectManager.LoadFromFile(_filePath);
            if (_project.Contacts.Count == 0)
            {
                return;
            }

            _viewedContacts = _project.Contacts;
            UpdateContactsList(null);
            BirthDaysContacts();
            ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
        }

        /// <summary>
        /// Вывод данных контакта на главную форму.
        /// </summary>
        private void ViewContacts(IReadOnlyList<Contact> contacts)
        {
            var index = ContactsListBox.SelectedIndex;
            if (index == -1)
            {
                ClearContactsView();
                return;
            }

            var contact = contacts[index];
            surnameTextBox.Text = contact.Surname;
            nameTextBox.Text = contact.Name;
            phoneTextBox.Text = $@"+{contact.PhoneNumber.Number}";
            emailTextBox.Text = contact.Email;
            idVkTextBox.Text = contact.IdVk;
            birthDateBox.Text = contact.DateOfBirth.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Вывод контактов у которых день рождения.
        /// </summary>
        private void BirthDaysContacts()
        {
            var surnames = _project.Contacts.Where(contact =>
                contact.DateOfBirth == DateTime.Today).Select(contact => contact.Surname);
            birthContactsLabel.Text = string.Join(", ", surnames);
        }

        /// <summary>
        /// Сортировка контактов.
        /// </summary>
        private void UpdateContactsList(Contact contact)
        {
            _viewedContacts = _project.SortContacts(findTextBox.Text);
            var index = _viewedContacts.FindIndex(x => x == contact);
            ContactsListBox.Items.Clear();
            foreach (var t in _viewedContacts)
            {
                ContactsListBox.Items.Add(t.Surname);
            }

            if (index == -1 && ContactsListBox.Items.Count != 0)
            {
                index = 0;
            }

            ContactsListBox.SelectedIndex = index;
            if (index == -1)
            {
                ClearContactsView();
            }
        }

        /// <summary>
        /// Очищения полей для вывода контакта.
        /// </summary>
        private void ClearContactsView()
        {
            surnameTextBox.Text = "";
            nameTextBox.Text = "";
            phoneTextBox.Text = "";
            emailTextBox.Text = "";
            idVkTextBox.Text = "";
            birthDateBox.Text = "";
        }

        /// <summary>
        /// Добавление контакта.
        /// </summary>
        private void AddContact()
        {
            var newContact = new Contact { PhoneNumber = new PhoneNumber() };
            var contactForm = new ContactForm { Contact = newContact };
            var dialogResult = contactForm.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            _project.Contacts.Add(contactForm.Contact);
            _project.Contacts = _project.Sort(_project.Contacts);
            UpdateContactsList(contactForm.Contact);
            ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
        }

        /// <summary>
        /// Редактирование контакта.
        /// </summary>
        private void EditContact()
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact.", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                var selectedContact = _viewedContacts[ContactsListBox.SelectedIndex];
                var contactForm = new ContactForm { Contact = selectedContact };
                var dialogResult = contactForm.ShowDialog();
                if (dialogResult != DialogResult.OK)
                {
                    return;
                }

                var index = _project.Contacts.FindIndex(x => Equals(x, contactForm.Contact));
                _project.Contacts.RemoveAt(index);
                _project.Contacts.Insert(index, contactForm.Contact);
                _project.Contacts = _project.Sort(_project.Contacts);
                UpdateContactsList(contactForm.Contact);
                ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
            }
        }

        /// <summary>
        /// Удаление контакта
        /// </summary>
        private void DeleteContact()
        {
            if (ContactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Select the contact.", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                var selectedIndex = ContactsListBox.SelectedIndex;
                var result = MessageBox.Show($@"Do you really want to delete this contact: 
                    {_project.Contacts[selectedIndex].Surname}?", @"Confirmation",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result != DialogResult.OK)
                {
                    return;
                }

                _project.Contacts.RemoveAt(selectedIndex);
                ContactsListBox.Items.RemoveAt(selectedIndex);
                ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
                if (ContactsListBox.Items.Count > 0)
                {
                    ContactsListBox.SelectedIndex = 0;
                }

                if (ContactsListBox.Items.Count == 0)
                {
                   ClearContactsView();
                }
            }
        }

        /// <summary>
        /// Вызов окна About.
        /// </summary>
        private static void ShowAboutForm()
        {
            var about = new AboutForm();
            about.ShowDialog();
        }

        private void ContactlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewContacts(_project.SortContacts(findTextBox.Text));
        }

        /// <summary>
        /// Добавление контакта через меню.
        /// </summary>
        private void addContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Редактирование контакта через меню.
        /// </summary>
        private void editContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Удаление контакта через меню.
        /// </summary>
        private void deleteContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteContact();
        }

        /// <summary>
        /// Вызов окна About.
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutForm();
        }

        /// <summary>
        /// Выход через меню.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Сохранение при выходе из программы.
        /// </summary>
        private void MainForm_Closed(object sender, FormClosedEventArgs e)
        {
            ProjectManager.SaveToFile(_project, _filePath, _directoryPath);
        }

        /// <summary>
        /// Добавление контакта по нажатию кнопки add.
        /// </summary>
        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Редактирование контакта по нажатию кнопки edit.
        /// </summary>
        private void EditNoteButton_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Удаление контакта по нажатию кнопки delete.
        /// </summary>
        private void DeleteNoteButton_Click(object sender, EventArgs e)
        {
            DeleteContact();
        }

        private void ContactsListBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) 
            {
                DeleteContact();
            }
        }
    }
}
