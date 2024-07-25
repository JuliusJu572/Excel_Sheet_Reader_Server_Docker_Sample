using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using OfficeOpenXml;

class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        // 设置EPPlus许可证上下文
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        string currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine($"Current directory: {currentDirectory}");

        string filePath = "files/file.xlsx";

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        List<string> sheetNames = new List<string>();

        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
        {
            foreach (var sheet in package.Workbook.Worksheets)
            {
                sheetNames.Add(sheet.Name);
            }
        }

        string json = JsonConvert.SerializeObject(sheetNames, Formatting.Indented);

        using (HttpClient client = new HttpClient())
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://host.docker.internal:5000", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data sent successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to send data. Status code: {response.StatusCode}");
            }
        }
    }
}
