using OpenQA.Selenium;

namespace EventCrawler.Services
{
    public abstract class BaseEventCrawlerService
    {
        protected readonly string Url;
        protected readonly IWebDriver Driver;

        protected BaseEventCrawlerService(string url, IWebDriver driver)
        {
            Url = string.IsNullOrWhiteSpace(url) ? throw new ArgumentException("URL cannot be null or empty.", nameof(url)) : url;
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        // Safely find an element, returning null if not found
        protected IWebElement SafeFindElement(By by)
        {
            try
            {
                return Driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        // Combine relative URL with base URL
        protected string CombineUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl)) return "N/A";

            try
            {
                return "https://www.kultur.bamberg.de" + relativeUrl.TrimStart('.');
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error combining URL: {ex.Message}");
                return "N/A";
            }
        }
    }

}
