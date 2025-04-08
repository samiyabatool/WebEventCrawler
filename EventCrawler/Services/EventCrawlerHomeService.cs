using EventCrawler.Models;
using OpenQA.Selenium;

namespace EventCrawler.Services
{
    public class EventCrawlerHomeService : BaseEventCrawlerService
    {
        public EventCrawlerHomeService(string url, IWebDriver driver) : base(url, driver) { }

        // Extract event data from the homepage
        public async Task<List<Event>> ExtractEventDataAsync()
        {
            var events = new List<Event>();

            try
            {
                Driver.Navigate().GoToUrl(Url);
                Driver.Manage().Window.Maximize();

                // Accept cookies if the prompt is present
                SafeFindElement(By.ClassName("cc-nb-reject"))?.Click();

                // Find all event links on the page
                var eventLinks = Driver.FindElements(By.XPath("//div[@class='d-sm-none d-md-block']//div[@class='event']//h4/a"));

                if (!eventLinks.Any())
                {
                    Console.WriteLine("No events found on the page.");
                    return events;
                }

                // Loop through each event link and parse details
                foreach (var link in eventLinks)
                {
                    try
                    {
                        var eventData = await ParseEventNodeAsync(link);
                        if (eventData != null) events.Add(eventData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing event node: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data from URL: {ex.Message}");
            }
            finally
            {
                Driver?.Quit();
            }

            return events;
        }

        // Parse individual event details
        private async Task<Event> ParseEventNodeAsync(IWebElement element)
        {
            try
            {
                var descriptionUrl = element.GetAttribute("href") ?? "N/A";
                Driver.Navigate().GoToUrl(descriptionUrl);
                var eventDetails = new Event
                {
                    Title = SafeFindElement(By.XPath("//div[@class='col-md-12']//h2"))?.Text ?? "N/A",
                    Date = SafeFindElement(By.XPath("(//div[@class='col-md-3 kurzinfos']//p)[1]"))?.Text.Trim() ?? "N/A",
                    Time = element.FindElement(By.XPath("//p[i[contains(@class, 'fa-clock-o')]]"))?.Text.Trim() ?? "N/A",
                    DescriptionUrl = descriptionUrl,
                    Description = SafeFindElement(By.XPath("//div[@class='container details']//div[@class='col-md-12']//p"))?.Text ?? "N/A",
                    Location = SafeFindElement(By.XPath("//div[@class='infos']//p[span[contains(text(),'Veranstaltungsort:')]]"))?.Text ?? "N/A",
                    ImageUrl = CombineUrl(SafeFindElement(By.XPath("//img[@class='img-fix img-contain details img-block']"))?.GetDomAttribute("src"))
                };

                Driver.Navigate().Back();
                return eventDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting event details: {ex.Message}");
                return null;
            }
        }
    }

}
