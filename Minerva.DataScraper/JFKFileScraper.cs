using HtmlAgilityPack;

namespace Minerva.DataScraper
{
    public class JFKFileScraper
    {
        private readonly string _destinationFolder;
        private readonly string _baseUrl = "https://www.archives.gov/research/jfk/release-2025";
        private readonly HttpClient _httpClient;
        private readonly Action<string> _log;

        private int _totalDownloaded = 0;

        public JFKFileScraper(string destinationFolder, Action<string> logCallback)
        {
            _destinationFolder = destinationFolder;
            _httpClient = new HttpClient();
            _log = logCallback ?? (_ => { }); // Falls null: leere Methode
        }

        public async Task StartScrapingAsync()
        {

            try
            {
                _log("🔍 Starte WebScraping ...");

                var html = await _httpClient.GetStringAsync(_baseUrl);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var links = doc.DocumentNode.SelectNodes("//a[@href]");
                if (links == null) return;

                int pageDownloadCount = 0;

                foreach (var link in links)
                {
                    string href = link.GetAttributeValue("href", "");

                    if (href.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        string fileUrl = href.StartsWith("http") ? href : new Uri(new Uri(_baseUrl), href).ToString();
                        string fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);
                        string filePath = Path.Combine(_destinationFolder, fileName);

                        if (File.Exists(filePath))
                        {
                            continue;
                        }

                        try
                        {
                            var bytes = await _httpClient.GetByteArrayAsync(fileUrl);
                            await File.WriteAllBytesAsync(filePath, bytes);
                            _totalDownloaded++;
                            pageDownloadCount++;

                            _log($"  ⬇️  {_totalDownloaded} | {fileName}");
                        }
                        catch (Exception ex)
                        {
                            _log($"  ⚠️  Fehler bei {fileUrl}: {ex.Message}");
                        }

                        await Task.Delay(200); // Pause zum Schutz des Servers
                    }
                }

                _log($"📄 Dateien auf dieser Seite heruntergeladen: {pageDownloadCount}");

            }
            catch (Exception ex)
            {
                _log($"❗ Fehler : {ex.Message}");
            }
        }
    }
}
