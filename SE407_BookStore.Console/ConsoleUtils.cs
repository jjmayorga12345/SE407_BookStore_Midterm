using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using SE407_BookStore.Models;

namespace SE407_BookStore.Console
{
    public static class ConsoleUtils
    {
        public static void WriteBooksToCsv(List<Book> books)
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(projectDirectory, "Books.csv");

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(books);
            }

            System.Console.WriteLine($"Books exported successfully to: {filePath}");

        }
    }
}
