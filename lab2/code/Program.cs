using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Завантаження даних з веб-ресурсу
            string url = "https://jsonplaceholder.typicode.com/posts";
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);

            // Обробка даних та запис у файл
            List<string> lines = response.Split('\n').ToList();
            lines = lines.Where(l => !string.IsNullOrEmpty(l)).ToList();
            lines = lines.Select(l => l.Replace("{", string.Empty).Replace("}", string.Empty).Trim()).ToList();
            string path = "output.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (string line in lines)
                {
                    await writer.WriteLineAsync(line);
                }
            }

            Console.WriteLine($"Успішно записано у файл {path}.");
        }
    }
}
