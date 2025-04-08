
namespace EventCrawler.Models
{
    /// <summary>
    /// Represents the structure of an event.
    /// </summary>
    public class Event
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Category{ get; set; }
        public string SubCategory { get; set; }
        public string DescriptionUrl { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
    }
}
