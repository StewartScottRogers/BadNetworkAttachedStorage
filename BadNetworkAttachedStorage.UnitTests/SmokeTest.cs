using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;
using System.Reflection;

namespace BadNetworkAttachedStorage.UnitTests {
    [TestClass]
    public class SmokeTest {
        private const string FileExtentions = ".UnitTestData.Test";
        private const string FileTestText = "Test File Content...";
        public static readonly DirectoryInfo DirectoryInfo = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        [TestMethod]
        public void Create_A_SubDirectory_And_Then_Create_A_File_And_Write_Text_To_It_And_Then_Check_If_It_Exists() {
            var badNasDirectoryInfo = new BadNasDirectoryInfo(DirectoryInfo);
            badNasDirectoryInfo = badNasDirectoryInfo.CreateSubdirectory("TestSubDirectory");
            var badNasFileInfo = badNasDirectoryInfo.CreateFile($"{Guid.NewGuid().ToString()}{FileExtentions}");
            badNasFileInfo.WriteAllText(FileTestText);
            if (badNasFileInfo.Exists())
                return;
            throw new Exception("File Does not Exist.");
        }

        [TestMethod]
        public void Create_A_SubDirectory_And_Then_Create_A_File_And_Write_Text_To_It_And_Then_Read_The_Text_Back() {
            var badNasDirectoryInfo = new BadNasDirectoryInfo(DirectoryInfo);
            badNasDirectoryInfo = badNasDirectoryInfo.CreateSubdirectory("TestSubDirectory");
            var badNasFileInfo = badNasDirectoryInfo.CreateFile($"{Guid.NewGuid().ToString()}{FileExtentions}");
            badNasFileInfo.WriteAllText(FileTestText);
            var text = badNasFileInfo.ReadAllText();

            FileTestText.Should().Be(text);
            Console.WriteLine(text);
        }
    }
}
