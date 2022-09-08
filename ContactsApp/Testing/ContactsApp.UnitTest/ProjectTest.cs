using System;
using NUnit.Framework;

namespace ContactsApp.UnitTest
{
    public class ProjectTest
    {
        [Test]
        public void Test_Project_CreateProject_ReturnRegulatedProject()
        {
            //Setup
            var sourceNumber = 71234567891;
            var phoneNumber = new PhoneNumber
            {
                Number = sourceNumber
            };

            var contact = new Contact
            {
                Name = "Casper",
                Surname = "Parker",
                DateOfBirth = new DateTime(2000, 2, 1),
                Email = "Casper@gmail.com",
                IdVk = "22666",
                PhoneNumber = phoneNumber
            };

            var sourceProject = new Project();

            //Act
            sourceProject.Contacts.Add(contact);

            //Assert
            Assert.IsNotNull(sourceProject);
        }
    }
}
