using HtmlAgilityPack;
using Minerva.DataScraper;

namespace FileScraper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
#if DEBUG
            args = new string[] { "--destination=\"C:\\Data\\JFKFiles2025\"" };
#endif
            // Default-Zielordner
            string downloadFolder = "JFK_Files";

            // Zielordner aus Argument extrahieren (z. B. --destination=C:\Downloads\JFK)
            var destinationArg = args.FirstOrDefault(a => a.StartsWith("--destination="));
            if (destinationArg == null)
            {
                Console.WriteLine("Es wurde kein Zielordner ('--destination=') angegeben");
                return;
            }

            downloadFolder = destinationArg.Split('=')[1].Trim('"');
            if (string.IsNullOrWhiteSpace(downloadFolder))
            {
                Console.WriteLine("❌ Ungültiger Zielordner. Beende.");
                return;
            }

            if (!Directory.Exists(downloadFolder))
            {
                Directory.CreateDirectory(downloadFolder);
            }
            Console.WriteLine($"📁 Zielordner: {Path.GetFullPath(downloadFolder)}");

            var scraper = new JFKFileScraper(downloadFolder, LogMessage);
            await scraper.StartScrapingAsync();

            Console.WriteLine("\n✅ Scraping abgeschlossen.");
        }

        static async void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

    }
}
