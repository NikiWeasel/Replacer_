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


namespace ConsoleApp2
{
    public class Replacement
    {
        public string replacement { get; set; }
        public string source { get; set; }
    }


    internal class Program
    {

        private static bool RemoteFileExists(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }


        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            string apiUrl = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/data.json";

            if (RemoteFileExists(apiUrl))
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    if (File.Exists(@"replacement.json"))
                    {

                        string text = File.ReadAllText(@"replacement.json");//

                        List<Replacement> repList = null;
                        repList = JsonConvert.DeserializeObject<List<Replacement>>(text);

                        string helpString = jsonString;

                        for (int i = repList.Count - 1; i >= 0; i--)
                        {
                            if (repList[i].source == null)
                            {
                                repList[i].source = String.Empty;
                            }

                            Regex reg = new Regex(@repList[i].replacement);
                            helpString = reg.Replace(helpString, repList[i].source);
                        }

                        Regex reg2 = new Regex(@"\""\""");

                        if (reg2.IsMatch(helpString))
                        {
                            Regex reg1 = new Regex(@"\""\""\,");
                            helpString = reg1.Replace(helpString, String.Empty);
                            helpString = reg2.Replace(helpString, String.Empty);
                        }

                        File.WriteAllText(@"result.json", helpString);

                        Console.WriteLine("Запись прошла успешно.");
                    }
                    else
                    {
                        Console.WriteLine("Файл не найден.");
                    }
                }
                else
                {
                    Console.WriteLine($"Запрос не выполнен с кодом состояния: {response.StatusCode}");
                }
            }
            else
            {
                Console.WriteLine("APIurl не работает.");
            }


            client.Dispose();
            Console.ReadKey();
        }
    }
}
