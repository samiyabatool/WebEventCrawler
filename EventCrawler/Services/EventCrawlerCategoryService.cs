using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using EventCrawler.Services;

internal class EventCrawlerCategoryService : BaseEventCrawlerService
{
    public EventCrawlerCategoryService(string url, IWebDriver driver) : base(url, driver) { }
    /// <summary>
    /// Extracts event data by navigating through categories and rows.
    /// </summary>
    /// <returns>A list of event records represented as dictionaries.</returns>
    public async Task<List<Dictionary<string, string>>> ExtractEventData()
    {
        var records = new List<Dictionary<string, string>>();

        try
        {
            Driver.Navigate().GoToUrl(Url);
            Driver.Manage().Window.Maximize();

            // Accept cookies if present
            SafeFindElement(By.ClassName("cc-nb-reject"))?.Click();

            var dropdown = SafeFindElement(By.XPath("//select[@id='bereich']"));
            if (dropdown == null)
            {
                Console.WriteLine("Dropdown not found.");
                return records;
            }

            var options = dropdown.FindElements(By.TagName("option"));
            if (!options.Any())
            {
                Console.WriteLine("No options found in the dropdown.");
                return records;
            }

            for (int i = 0; i < options.Count; i++)
            {
                // Refetch dropdown and options to avoid stale element exception
                dropdown = SafeFindElement(By.TagName("select"));
                options = dropdown?.FindElements(By.TagName("option"));
                if (options == null || i >= options.Count) break;

                var option = options[i];
                string category = option.Text;
                string value = option.GetDomAttribute("value");

                if (value == "Alle") continue; // Skip "Alle" option

                option.Click();
                SafeFindElement(By.XPath("//button[@type='submit']"))?.Click();

                var showAllButton = SafeFindElement(By.XPath("//a[@class='kat-liste' and contains(text(), 'alle anzeigen')]"));
                if (showAllButton != null)
                {
                    var showAllHref = showAllButton.GetAttribute("href");
                    Driver.Navigate().GoToUrl(showAllHref);
                }

                var rows = Driver.FindElements(By.XPath("//table[@class='table']//tbody/tr"));
                if (!rows.Any())
                {
                    Console.WriteLine($"No rows found for category: {category}");
                    continue;
                }

                foreach (var row in rows.Take(3))
                {
                    var record = new Dictionary<string, string>();
                    string descriptionUrl = "";

                    try
                    {
                        record["Category"] = category;

                        var columns = row.FindElements(By.TagName("td"));
                        if (columns.Count >= 3)
                        {
                            var linkElement = columns[2].FindElement(By.TagName("a"));
                            descriptionUrl = linkElement.GetAttribute("href");
                            record["DescriptionUrl"] = descriptionUrl ?? "N/A";
                        }

                        if (!string.IsNullOrEmpty(descriptionUrl))
                        {
                            Driver.Navigate().GoToUrl(descriptionUrl);

                            record["Location"] = SafeFindElement(By.XPath("//div[@class='infos']//p[span[contains(text(),'Veranstaltungsort:')]]"))?.Text ?? "N/A";
                            record["Title"] = SafeFindElement(By.XPath("//div[@class='col-md-12']//h2"))?.Text ?? "N/A";
                            record["Date"] = SafeFindElement(By.XPath("(//div[@class='col-md-3 kurzinfos']//p)[1]"))?.Text.Trim() ?? "N/A";
                            record["Image"] = CombineUrl(SafeFindElement(By.XPath("//img[@class='img-fix img-contain details img-block']"))?.GetDomAttribute("src"));
                            record["Description"] = SafeFindElement(By.XPath("//div[@class='container details']//div[@class='col-md-12']//p"))?.Text.Trim() ?? "N/A";

                            Driver.Navigate().Back();
                        }
                        else
                        {
                            record["Title"] = row.FindElement(By.XPath("//td[3]/a")).Text.Trim();
                            record["DateTime"] = row.FindElement(By.XPath("//td[1]")).Text.Trim();
                            record["Location"] = row.FindElement(By.XPath("//td[@class='last']/small")).Text.Trim();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing row: {ex.Message}");
                    }

                    records.Add(record);
                }

                Driver.Navigate().GoToUrl(Url); // Return to base URL
                dropdown = Driver.FindElement(By.TagName("select"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting event data: {ex.Message}");
        }

        return records;
    }
}