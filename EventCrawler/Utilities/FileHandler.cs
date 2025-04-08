using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;

namespace EventCrawler.Utilities
{
    /// <summary>
    /// Handles saving event data to files in various formats.
    /// </summary>
    public static class FileHandler
    {
        public static void SaveToJson<T>(IEnumerable<T> data, string fileName)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(fileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving JSON file: {ex.Message}");
            }
        }

        public static void SaveToCsv(List<Dictionary<string, string>> records, string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (records.Count > 0)
                {
                    foreach (var header in records[0].Keys)
                    {
                        csv.WriteField(header);
                    }
                    csv.NextRecord();

                    foreach (var record in records)
                    {
                        foreach (var value in record.Values)
                        {
                            csv.WriteField(value);
                        }
                        csv.NextRecord();
                    }
                }
            }
        }
    }

}

