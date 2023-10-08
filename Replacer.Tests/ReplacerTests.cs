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
            //act
            Task<string> Task = Program.GetJsonString(apiUrl);
            string actual = await Task;
            //assert
            string expected = "\"[\\n  \\\"Two roads diverged in a yellow d12324344rgg6f5g6gdf2ddjf,\\\",\\n  \\\"Robert Frost poetAnd sorry I cou1d not trave1 both\\\",\\n  \\\"An other text\\\",\\n  \\\"And be one trave1er, long I stood\\\",\\n  \\\"And 1ooked down one as far as I cou1d\\\",\\n  \\\"Bla-bla-bla-blaaa, just some RANDOM tExT\\\",\\n  \\\"To where it bent in the undergrowth;\\\",\\n  \\\"Then Random text, yeeep the other, as just as fair,\\\",\\n  \\\"And having perhaps parentheses - that is a smart word,\\\",\\n  \\\"Bla-bla-bla-blaaa, just some RANDOM tExT\\\",\\n  \\\"Because it was grassy and wanted wear;\\\",\\n  \\\"An other text\\\",\\n  \\\"An other text\\\",\\n  \\\"Though as for that the passing there\\\",\\n  \\\"Emptry... or NOT! them rea11y about the same,\\\",\\n  \\\"And both that morning equally lay\\\",\\n  \\\"In 1eaves no step had trodden b1ack.\\\",\\n  \\\"Oh, I kept the first for another day!\\\",\\n  \\\"Yet Skooby-dooby-doooo 1eads on to way,\\\",\\n  \\\"Ha-haaa, hacked you.\\\",\\n  \\\"An other text\\\",\\n  \\\"I shall be te11ing this with a sigh\\\",\\n  \\\"sdshdjdskfm sfjsdif jfjfidjf\\\",\\n  \\\"Two roads diverged in a d12324344rgg6f5g6gdf2ddjf, and I\\\",\\n  \\\"I Random text, yeeep the one less traveled by,\\\",\\n  \\\"And that has made a11 the difference.\\\",\\n  \\\"Bla-bla-bla-blaaa, just some RANDOM tExT\\\"\\n]\\n\"";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetReplacements_Test()//получение замен
        {
            //arrange

            //act
            List<Replacement> actual = Program.GetReplacements("replacement.json");
            //assert
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
            string helpString = "[\n  \"Two roads diverged in a yellow d12324344rgg6f5g6gdf2ddjf,\",\n  \"Robert Frost poetAnd sorry I cou1d not trave1 both\",\n  \"An other text\",\n  \"And be one trave1er, long I stood\",\n  \"And 1ooked down one as far as I cou1d\",\n  \"Bla-bla-bla-blaaa, just some RANDOM tExT\",\n  \"To where it bent in the undergrowth;\",\n  \"Then Random text, yeeep the other, as just as fair,\",\n  \"And having perhaps parentheses - that is a smart word,\",\n  \"Bla-bla-bla-blaaa, just some RANDOM tExT\",\n  \"Because it was grassy and wanted wear;\",\n  \"An other text\",\n  \"An other text\",\n  \"Though as for that the passing there\",\n  \"Emptry... or NOT! them rea11y about the same,\",\n  \"And both that morning equally lay\",\n  \"In 1eaves no step had trodden b1ack.\",\n  \"Oh, I kept the first for another day!\",\n  \"Yet Skooby-dooby-doooo 1eads on to way,\",\n  \"Ha-haaa, hacked you.\",\n  \"An other text\",\n  \"I shall be te11ing this with a sigh\",\n  \"sdshdjdskfm sfjsdif jfjfidjf\",\n  \"Two roads diverged in a d12324344rgg6f5g6gdf2ddjf, and I\",\n  \"I Random text, yeeep the one less traveled by,\",\n  \"And that has made a11 the difference.\",\n  \"Bla-bla-bla-blaaa, just some RANDOM tExT\"\n]\n";
            //act
            string actual = Program.ReplaceInJson(repList, helpString);
            //assert
            string expected = "[\n  \"Two roads diverged in a yellow wood,\",\n  \"Robert Frost poetAnd sorry I could not travel both\",\n  \"\",\n  \"And be one traveler, long I stood\",\n  \"And looked down one as far as I could\",\n  \"\",\n  \"To where it bent in the undergrowth;\",\n  \"Then took the other, as just as fair,\",\n  \"And having perhaps the better claim,\",\n  \"\",\n  \"Because it was grassy and wanted wear;\",\n  \"\",\n  \"\",\n  \"Though as for that the passing there\",\n  \"Had worn them really about the same,\",\n  \"And both that morning equally lay\",\n  \"In leaves no step had trodden black.\",\n  \"Oh, I kept the first for another day!\",\n  \"Yet knowing how way leads on to way,\",\n  \"I doubted if I should ever come back.\",\n  \"\",\n  \"I shall be telling this with a sigh\",\n  \"Somewhere ages and ages hence:\",\n  \"Two roads diverged in a wood, and I\",\n  \"I took the one less traveled by,\",\n  \"And that has made all the difference.\",\n  \"\"\n]\n";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void RemoveEscapedQuotes_Test()//удаление кавычек
        {
            //arrange
            string helpString = "[\n  \"Two roads diverged in a yellow wood,\",\n  \"Robert Frost poetAnd sorry I could not travel both\",\n  \"\",\n  \"And be one traveler, long I stood\",\n  \"And looked down one as far as I could\",\n  \"\",\n  \"To where it bent in the undergrowth;\",\n  \"Then took the other, as just as fair,\",\n  \"And having perhaps the better claim,\",\n  \"\",\n  \"Because it was grassy and wanted wear;\",\n  \"\",\n  \"\",\n  \"Though as for that the passing there\",\n  \"Had worn them really about the same,\",\n  \"And both that morning equally lay\",\n  \"In leaves no step had trodden black.\",\n  \"Oh, I kept the first for another day!\",\n  \"Yet knowing how way leads on to way,\",\n  \"I doubted if I should ever come back.\",\n  \"\",\n  \"I shall be telling this with a sigh\",\n  \"Somewhere ages and ages hence:\",\n  \"Two roads diverged in a wood, and I\",\n  \"I took the one less traveled by,\",\n  \"And that has made all the difference.\",\n  \"\"\n]\n";
            //act
            string actual = Program.RemoveEscapedQuotes(helpString);
            //assert
            string expected = "Two roads diverged in a yellow wood,\",\n\"Robert Frost poetAnd sorry I could not travel both\",\n\"And be one traveler, long I stood\",\n\"And looked down one as far as I could\",\n\"To where it bent in the undergrowth;\",\n\"Then took the other, as just as fair,\",\n\"And having perhaps the better claim,\",\n\"Because it was grassy and wanted wear;\",\n\"Though as for that the passing there\",\n\"Had worn them really about the same,\",\n\"And both that morning equally lay\",\n\"In leaves no step had trodden black.\",\n\"Oh, I kept the first for another day!\",\n\"Yet knowing how way leads on to way,\",\n\"I doubted if I should ever come back.\",\n\"I shall be telling this with a sigh\",\n\"Somewhere ages and ages hence:\",\n\"Two roads diverged in a wood, and I\",\n\"I took the one less traveled by,\",\n\"And that has made all the difference.";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void WriteJsonToFile_Test()//удаление кавычек
        {
            //arrange
            string helpString = "Two roads diverged in a yellow wood,\",\n\"Robert Frost poetAnd sorry I could not travel both\",\n\"And be one traveler, long I stood\",\n\"And looked down one as far as I could\",\n\"To where it bent in the undergrowth;\",\n\"Then took the other, as just as fair,\",\n\"And having perhaps the better claim,\",\n\"Because it was grassy and wanted wear;\",\n\"Though as for that the passing there\",\n\"Had worn them really about the same,\",\n\"And both that morning equally lay\",\n\"In leaves no step had trodden black.\",\n\"Oh, I kept the first for another day!\",\n\"Yet knowing how way leads on to way,\",\n\"I doubted if I should ever come back.\",\n\"I shall be telling this with a sigh\",\n\"Somewhere ages and ages hence:\",\n\"Two roads diverged in a wood, and I\",\n\"I took the one less traveled by,\",\n\"And that has made all the difference.";
            
            //act
            Program.WriteJsonToFile(helpString);
            //assert
            Assert.IsTrue(File.Exists("result.json"));
        }


    }
}
