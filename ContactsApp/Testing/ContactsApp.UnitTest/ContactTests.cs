using System;
using NUnit.Framework;

namespace ContactsApp.UnitTest
{
    [TestFixture]
    public class ContactTests
    {
        private Contact PrepareContact()
        {
            var sourceNumber = 79996196969;
            var phoneNumber = new PhoneNumber
            {
                Number = sourceNumber
            };
            var contact = new Contact
            {
                Name = "Ivan",
                Surname = "Ivanov",
                DateOfBirth = new DateTime(2000,1,1),
                PhoneNumber = phoneNumber,
                Email = "ivanov@bk.com",
                IdVk = "226665"
            };
            return contact;
        }

        [Test]
        public void Test_Name_CorrectName_ReturnsSameName()
        {
            //Setup
            var contact = new Contact();
            var sourceName = "Farrukh";
            var expectedName = sourceName;

            //Act
            contact.Name = sourceName;
            var actualName = contact.Name;

            //Assert
            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void Test_Name_WrongName_ReturnsUpperCaseName()
        {
            //Setup
            var contact = new Contact();
            const string sourceName = "farrukh";
            const string expectedName = "Farrukh";

            //Act
            contact.Name = sourceName;
            var actualName = contact.Name;

            //Assert
            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void Test_Name_LongName_ThrowsException()
        {
            //Setup
            var contact = new Contact();
            var sourceName = "123456789 123456789 123456789 123456789 123456789 1";
  
            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>

                {
                    //Act
                    contact.Name = sourceName;
                }
            );
        }

        [Test]
        public void Test_Name_EmptyName_ThrowsException()
        {
            //Setup
            var contact = new Contact();
            var sourceName = "";

            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>

                {
                    //Act
                    contact.Name = sourceName;
                }
            );
        }

        [Test]
        public void Test_Surname_CorrectSurname_ReturnsSameSurname()
        {
            //Setup
            var contact = new Contact();
            var sourceSurname = "Rakhimov";
            var expectedSurname = sourceSurname;

            //Act
            contact.Surname = sourceSurname;
            var actualSurname = contact.Surname;

            //Assert
            Assert.AreEqual(expectedSurname, actualSurname);
        }

        [Test]
        public void Test_Surname_WrongSurname_ReturnsUpperCaseName()
        {
            //Setup
            var contact = new Contact();
            const string sourceSurname = "rakhimov";
            const string expectedSurname = "Rakhimov";

            //Act
            contact.Surname = sourceSurname;
            var actualSurname = contact.Surname;

            //Assert
            Assert.AreEqual(expectedSurname, actualSurname);
        }

        [Test]
        public void Test_Surname_LongSurname_ThrowsException()
        {
            //Setup
            var contact = new Contact();
            var sourceSurname = "123456789 123456789 123456789 123456789 123456789 1";
           
            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>

                {
                    //Act
                    contact.Surname = sourceSurname;
                }
            );
        }

        [Test]
        public void Test_Surname_EmptySurname_ThrowsException()
        {
            //Setup
            var contact = new Contact();
            var sourceSurname = "";

            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>

                {
                    //Act
                    contact.Surname = sourceSurname;
                }
            );
        }

        [Test]
        public void Test_Email_CorrectEmail_ReturnSameEmail()
        {
            //Setup
            var contact = new Contact();
            var sourceEmail = "Rakhimov@gmail.com";
            var expectedEmail = sourceEmail;

            //Act
            contact.Email = sourceEmail;
            var actualEmail = contact.Email;

            //Assert
            Assert.AreEqual(expectedEmail, actualEmail);
        }

        [Test]
        public void Test_Email_TooLongEmail_ThrowsException()
        {
            //Setup
            var contact = new Contact();
            var sourceEmail = "RakhimovRakhimovRakhimovRakhimovRakhimov1@gmail.com";

            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>

                {
                    //Act
                    contact.Email = sourceEmail;
                }
            );
        }

        [Test]
        public void Test_IdVk_CorrectIdVk_ReturnSameIdVk()
        {
            //Setup
            var contact = new Contact();
            var sourceIdVk = "2266677";
            var expectedIdVk = sourceIdVk;

            //Act
            contact.IdVk = sourceIdVk;
            var actualIdVk = contact.IdVk;

            //Assert
            Assert.AreEqual(expectedIdVk, actualIdVk);
        }

        [Test]
        public void Test_IdVk_TooLongIdVk_ThrowsException()
        {
            //Setup
            var contact = new Contact();
            var sourceIdVk = "123456789 123456789 123456789 1";

            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>

                {
                    //Act
                    contact.IdVk = sourceIdVk;
                }
            );
        }

        [Test]
        public void Test_DateOfBirth_CorrectDateOfBirth_ReturnsSameDateOfBirth()
        {
            //Setup
            var contact = new Contact();
            var sourceDateOfBirth = DateTime.Today;
            var expectedDateOfBirth = sourceDateOfBirth;

            //Act
            contact.DateOfBirth = sourceDateOfBirth;
            var actualDateOfBirth = contact.DateOfBirth;

            //Assert
            Assert.AreEqual(expectedDateOfBirth, actualDateOfBirth);
        }

        [Test]
        public void Test_DateOfBirth_OutOfRangeDateOfBirth_ThrowsException()
        {
            //Setup
            var contact = new Contact();
            var sourceDateOfBirth = new DateTime(2222, 1, 1);

            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    contact.DateOfBirth = sourceDateOfBirth;
                }
            );
        }

        [Test]
        public void Test_DateOfBirth_TooSmallDateOfBirth_ThrowsException()
        {
            //Setup
            var contact = new Contact();
            var sourceDateOfBirth = new DateTime(1800, 1, 1);

            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>

                {
                    //Act
                    contact.DateOfBirth = sourceDateOfBirth;
                }
            );
        }

        [Test]
        public void Test_Clone_CorrectClone_ReturnsSameObject()
        {
            //Setup
            var expectedContact = PrepareContact();

            //Act
            var actualContact = expectedContact.Clone() as Contact;
           
            //Assert
            Assert.AreEqual(expectedContact, actualContact);
        }
    }
}
