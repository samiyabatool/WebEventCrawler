# WebEventCrawler 🕷️🎫

A custom web crawler built using **C#** and **Selenium WebDriver** to extract structured event data from the official [Kultur Bamberg Events](https://www.kultur.bamberg.de/_plaza/kuba.cfm) website. The tool collects information like event titles, dates, descriptions, and images, and stores them in both **JSON** and **CSV** formats for flexible use in applications, reports, or data analysis.

---

## 📌 About the Project

This project was created to automate the process of gathering event listings from a local cultural platform. The crawler navigates through the webpage, scrapes event data, and organizes it for further use. It was developed as a standalone project and demonstrates skills in automation, web scraping, and structured data handling.

---

## ✨ Features

- 🔍 Scrapes event title, date, description, location, and image (if available)
- 💾 Exports data in both **JSON** and **CSV** formats
- 🔧 Easy to extend for other websites with similar HTML structures
- ⚙️ Simple and clean code structure using object-oriented principles
- ✅ Basic validation to ensure data completeness

---

## 🛠 Tech Stack

- **C# (.NET Core)**
- **Selenium WebDriver**
- **Newtonsoft.Json** – for JSON serialization
- **CsvHelper** – for CSV export
- **Visual Studio** – for development

---

## 🧠 Project Structure

- `WebEventCrawler/`
  - `Models/`
    - `Event.cs` – Defines the structure for event data
  - `Services/`
    - `BaseEventCrawlerService.cs` – Abstract base class for shared crawler logic
    - `EventCrawlerCategoryService.cs` – Crawler logic for category/event list pages
    - `EventCrawlerHomeService.cs` – Crawler logic for homepage event listings
  - `Utilities/`
    - `FileHandler.cs` – Handles writing output to JSON and CSV formats
  - `Program.cs` – Application entry point
  - `README.md` – Project documentation

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Visual Studio](https://visualstudio.microsoft.com/)
- Google Chrome + ChromeDriver (or modify the driver setup accordingly)

### Installation & Run

1. **Clone the Repository**
   ```bash
   git clone https://github.com/samiyabatool/WebEventCrawler.git
   cd WebEventCrawler

## 🚀 Running the Application

1. **Open the project in Visual Studio**

2. **Restore NuGet Packages**  
   Visual Studio will usually prompt this automatically when you open the solution.

3. **Run the Application**
   - The console application will launch a browser window and scrap data from the target site.
   - Once complete, you'll find the following output files:
     - `events.json` – JSON-formatted data
     - `events.csv` – CSV-formatted data
   - Both files will be saved in the **root directory** of the project.


## 🔒 Legal Disclaimer
This project is intended for educational purposes only. Before scraping content, always check and respect the website’s robots.txt and terms of service.

---

## ⚖️ License

All rights reserved.  
This repository and its contents are the intellectual property of the author.  
Unauthorized copying, redistribution, or commercial use is prohibited without explicit permission.

### © 2025 Samiya Batool. All rights reserved.

## 📬 Contact
For questions or suggestions, feel free to reach out:

📧 Email: samiyabatool125@gmail.com

🧑‍💻 GitHub: github.com/samiyabatool
