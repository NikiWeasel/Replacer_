using Replacer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace Replacer.Tests
{

    [TestClass()]
    public class ReplacerTests
    {
        [TestMethod()]
        public async void GetJsonString_Test()//ссылка
        {
            //arrange
            const string apiUrl = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/data.json";
            string expected = Constance.GetJsonString_EXPECTED;
            //act
            Task<string> Task = Program.GetJsonString(apiUrl);
            string actual = await Task;
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetReplacements_Test()//получение замен
        {
            //arrange
            List<Replacement> expected = new List<Replacement>();
            expected.Add(new Replacement() { replacement = "Ha-haaa, hacked you", source = "I doubted if I should ever come back" });
            expected.Add(new Replacement() { replacement = "sdshdjdskfm sfjsdif jfjfidjf", source = "Somewhere ages and ages hence:" });
            expected.Add(new Replacement() { replacement = "1", source = "l" });
            expected.Add(new Replacement() { replacement = "Emptry... or NOT!", source = "" });
            expected.Add(new Replacement() { replacement = "d12324344rgg6f5g6gdf2ddjf", source = "wood" });
            expected.Add(new Replacement() { replacement = "Random text, yeeep", source = "took" });
            expected.Add(new Replacement() { replacement = "Bla-bla-bla-blaaa, just some RANDOM tExT", source = "" });
            expected.Add(new Replacement() { replacement = "parentheses - that is a smart word", source = "the better claim" });
            expected.Add(new Replacement() { replacement = "sdshdjdskfm sfjsdif jfjfidjf", source = "Somewhere ages and ages hence:" });
            expected.Add(new Replacement() { replacement = "Emptry... or NOT!", source = "Had worn" });
            expected.Add(new Replacement() { replacement = "Skooby-dooby-doooo", source = "knowing how way leads on" });
            expected.Add(new Replacement() { replacement = "sdshdjdskfm sfjsdif jfjfidjf", source = "Somewhere ages and ages hence:" });
            expected.Add(new Replacement() { replacement = "An other text", source = "" });
            expected.Add(new Replacement() { replacement = "Skooby-dooby-doooo", source = "knowing how way" });
            //act
            List<Replacement> actual = Program.GetReplacements("replacement.json");
            //assert
            CollectionAssert.Equals(expected, actual);
        }


        [TestMethod()]
        public void ReplaceInJson_Test()//перестановка
        {
            //arrange
            List<Replacement> repList = new List<Replacement>();
            repList.Add(new Replacement() { replacement = "Ha-haaa, hacked you", source = "I doubted if I should ever come back" });
            repList.Add(new Replacement() { replacement = "sdshdjdskfm sfjsdif jfjfidjf", source = "Somewhere ages and ages hence:" });
            repList.Add(new Replacement() { replacement = "1", source = "l" });
            repList.Add(new Replacement() { replacement = "Emptry... or NOT!", source = "" });
            repList.Add(new Replacement() { replacement = "d12324344rgg6f5g6gdf2ddjf", source = "wood" });
            repList.Add(new Replacement() { replacement = "Random text, yeeep", source = "took" });
            repList.Add(new Replacement() { replacement = "Bla-bla-bla-blaaa, just some RANDOM tExT", source = "" });
            repList.Add(new Replacement() { replacement = "parentheses - that is a smart word", source = "the better claim" });
            repList.Add(new Replacement() { replacement = "sdshdjdskfm sfjsdif jfjfidjf", source = "Somewhere ages and ages hence:" });
            repList.Add(new Replacement() { replacement = "Emptry... or NOT!", source = "Had worn" });
            repList.Add(new Replacement() { replacement = "Skooby-dooby-doooo", source = "knowing how way leads on" });
            repList.Add(new Replacement() { replacement = "sdshdjdskfm sfjsdif jfjfidjf", source = "Somewhere ages and ages hence:" });
            repList.Add(new Replacement() { replacement = "An other text", source = "" });
            repList.Add(new Replacement() { replacement = "Skooby-dooby-doooo", source = "knowing how way" });
            string helpString = Constance.ReplaceInJson_HS;
            string expected = Constance.ReplaceInJson_EXPECTED;
            //act
            string actual = Program.ReplaceInJson(repList, helpString);
            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void RemoveEscapedQuotes_Test()//удаление кавычек
        {
            //arrange
            string helpString = Constance.RemoveEscapedQuotes_HS;
            string expected = Constance.RemoveEscapedQuotes_EXPECTED;
            //act
            string actual = Program.RemoveEscapedQuotes(helpString);
            //assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void WriteJsonToFile_Test()//удаление кавычек
        {
            //arrange
            string helpString = Constance.WriteJsonToFile_HS;            
            //act
            Program.WriteJsonToFile(helpString);
            //assert
            Assert.IsTrue(File.Exists("result.json"));
        }
    }
}
