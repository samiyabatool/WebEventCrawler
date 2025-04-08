using EventCrawler.Services;
using EventCrawler.Utilities;
using OpenQA.Selenium.Chrome;

class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            string url = "https://www.kultur.bamberg.de/_plaza/kuba.cfm";
            using var driver = new ChromeDriver();

            var categoryCrawler = new EventCrawlerCategoryService(url, driver);
            var categoryEvents = await categoryCrawler.ExtractEventData();

            // Output events
            Console.WriteLine($"Extracted {categoryEvents.Count} events.");

            // Save data to files
            FileHandler.SaveToJson(categoryEvents, "categoryEvents.json");
            FileHandler.SaveToCsv(categoryEvents, "categoryEvents.csv");

            var homeCrawler = new EventCrawlerHomeService(url, driver);
            var homeEvents = await homeCrawler.ExtractEventDataAsync();

            // Output events
            Console.WriteLine($"Extracted {homeEvents.Count} events.");

            // Save data to files
            FileHandler.SaveToJson(homeEvents, "homeEvents.json");
           // FileHandler.SaveToCsv(homeEvents, "homeEvents.csv");

            Console.WriteLine("Data extraction and saving completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}