﻿using System;
using NUnit.Framework;
using System.IO;
using System.Reflection;


namespace ContactsApp.UnitTest
{
   public class ProjectManagerTests
    {
        public Project PrepareProject()
        {
            var sourceProject = new Project();

            sourceProject.Contacts.Add(new Contact()
            {
                Surname = "Ivanov",
                Name = "Ivan",
                DateOfBirth = new DateTime(2021, 12, 7),
                PhoneNumber = new PhoneNumber()
                {
                    Number = 78901234567
                },
                IdVk = "qwerty",
                Email = "123@gmail.com"
            }
            );

            sourceProject.Contacts.Add(new Contact()
            {
                Surname = "Sergachev",
                Name = "Mikhail",
                DateOfBirth = new DateTime(2021, 12, 7),
                PhoneNumber = new PhoneNumber()
                {
                    Number = 78891234321
                },
                IdVk = "qwerty",
                Email = "123@gmail.com"
            }
            );
            return sourceProject;
        }
       
        [Test]
        public void Test_LoadFromFile_UnCorrectFile_ReturnEmptyProject()
        {
            //Setup
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\WrongFile.json";

            //Act
            var actualProject = ProjectManager.LoadFromFile(testDataFolder);

            //Assert
            Assert.IsEmpty(actualProject.Contacts);
        }
        [Test]
        public void Test_SaveToFile_CorrectProject_FileSavedCorrectly()
        {
            //Setup
            var sourceProject = PrepareProject();
            var testDataFolder = Common.DataFolderForTest();
            var actualFileName = testDataFolder + @"\actualProject.json";
            var expectedFileName = testDataFolder + @"\expectedProject.json";
            if (File.Exists(actualFileName))
            {
                File.Delete(actualFileName);
            }

            //Act

            ProjectManager.SaveToFile(sourceProject, actualFileName, testDataFolder);

            var isFileExist = File.Exists(actualFileName);
            Assert.AreEqual(true, isFileExist);

            //Assert
            var actualFileContent = File.ReadAllText(actualFileName);
            var expectedFileContent = File.ReadAllText(expectedFileName);
            Assert.AreEqual(expectedFileContent, actualFileContent);
        }

        [Test]
        public void Test_SaveToFile_CreateFolder_FolderIsExist()
        {
            //Setup
            var project = PrepareProject();

            var testDataFolder = Common.DataFolderForTest() + "CreateTest";
            var testFileName = testDataFolder + @"CreateFolderTest";
            if (Directory.Exists(testDataFolder))
            {
                Directory.Delete(testDataFolder);
            }

            //Act
            ProjectManager.SaveToFile(project, testFileName, testDataFolder);

            //Assert
            Assert.IsTrue(Directory.Exists(testDataFolder));
        }

        [Test]
        public void Test_LoadFromFile_CorrectProject_FileLoadedCorrectly()
        {
            //Setup
            var expectedProject = PrepareProject();
            var testDataFolder = Common.DataFolderForTest();
            var testFileName = testDataFolder + @"\expectedProject.json";

            //Act
            var actualProject = ProjectManager.LoadFromFile(testFileName);

            //Assert
            Assert.AreEqual(expectedProject.Contacts, actualProject.Contacts);
        }

        [Test]
        public void Test_LoadFromFile_UnCorrectPath_ReturnEmptyProject()
        {
            //Setup
            var testFileName = Common.DataFolderForTest() +  "wrong";

            //Act
            var actualProject = ProjectManager.LoadFromFile(testFileName);

            //Assert
            Assert.IsEmpty(actualProject.Contacts);
        }

        [Test]
        public void Test_FilePath_CorrectFilePath_ReturnSamePath()
        {
            //Setup
            var expectedPath = Common.FilePath();

            //Act
            var actualPath = ProjectManager.FilePath();

            //Assert
            Assert.AreEqual(expectedPath, actualPath);
        }

        [Test]
        public void Test_DirectoryPath_CorrectDirectoryPath_ReturnSameDirectory()
        {
            //Setup
            var expectedPath = Common.DirectoryPath();

            //Act
            var actualPath = ProjectManager.DirectoryPath();

            //Assert
            Assert.AreEqual(expectedPath, actualPath);
        }
    }
}
