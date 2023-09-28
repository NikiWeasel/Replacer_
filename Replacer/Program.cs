using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Security.Policy;
using Newtonsoft.Json.Serialization;

namespace ConsoleApp2
{
    public class Replacement
    {
        public string replacement { get; set; }
        public string source { get; set; }
    }


    internal class Program
    {
        private static async Task<string> GetJsonString(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                client.Dispose();
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                client.Dispose();
                throw new Exception($"Запрос не выполнен. Код состояния: {response.StatusCode}");
            }
        }

        private static List<Replacement> GetReplacements()//получение замен
        {
            if (!File.Exists(@"replacement.json")) throw new Exception("Ошибка чтения файла.");

            string text = File.ReadAllText(@"replacement.json");
            return JsonConvert.DeserializeObject<List<Replacement>>(text);
        }

        private static string ReplaceInJson(List<Replacement> repList, string helpString)//перестановка
        {
            for (int i = repList.Count - 1; i >= 0; i--)
            {
                if (repList[i].source == null)
                {
                    repList[i].source = String.Empty;
                }
                helpString = helpString.Replace(repList[i].replacement, repList[i].source);
            }
            return helpString;

        }

        private static string RemoveEscapedQuotes(string helpString)
        {
            List<string> resultList = new List<string>();
            resultList = JsonConvert.DeserializeObject<List<String>>(helpString);
            resultList = resultList.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            return string.Join("\",\n\"", resultList);
        }


        private static void WriteJsonToFile(string helpString)//запись в файл
        {
            File.WriteAllText(@"result.json", "[\n\"" + helpString + "\"\n]");
            Console.WriteLine("Запись прошла успешно.");
        }

        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            const string apiUrl = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/data.json";

            try
            {
                Task<string> Task = GetJsonString(apiUrl);
                string jsonString = await Task;

                WriteJsonToFile(RemoveEscapedQuotes(ReplaceInJson(GetReplacements(), jsonString)));


                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();

            }

        }
    }
}
