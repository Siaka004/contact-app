using System;
using NUnit.Framework;

namespace ContactsApp.UnitTest
{
    public class PhoneNumberTests
    {
        [Test]
        public void Test_PhoneNumber_CorrectPhoneNumber_ReturnsSamePhoneNumber()
        {
            //Setup
            var sourcePhoneNumber = 79293749789;
            var phoneNumber = new PhoneNumber();
            var expectedPhoneNumber = sourcePhoneNumber;

            //Act
            phoneNumber.Number = sourcePhoneNumber;
            var actualPhoneNumber = phoneNumber.Number;

            //Assert
            Assert.AreEqual(expectedPhoneNumber, actualPhoneNumber);
        }

        [Test]
        public void Test_PhoneNumber_InitialPhoneNumberNot7_ThrowsException()
        {
            //Setup
            var phoneNumber = new PhoneNumber();
            var sourceNumber = 998998887788;

            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    phoneNumber.Number = sourceNumber;
                }
            );
        }

        [Test]
        public void Test_PhoneNumber_TooLongPhoneNumber_ThrowsException()
        {
            //Setup
            var phoneNumber = new PhoneNumber();
            var sourceNumber = 877766655477777777;

            //Assert
            Assert.Throws<ArgumentException>
            (
                () =>
                {
                    //Act
                    phoneNumber.Number = sourceNumber;
                }
            );
        }
    }
}
