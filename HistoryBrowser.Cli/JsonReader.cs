using HistoryBrowser.Cli.Entities;
using Newtonsoft.Json;

namespace HistoryBrowser.Cli
{
    public class JsonReader
    {
        private IEnumerable<Entry>? Entries { get; set; } = new List<Entry>();

        public List<Entry> ReadVideosHistory()
        {
            var directory = new DirectoryInfo(@"C:\Users\burab\source\repos\RazorPages\RazorPages\uploads\");
            var fileName = directory.GetFiles().Select(fi => fi.Name).FirstOrDefault();
            var path = $"C:\\Users\\burab\\source\\repos\\RazorPages\\RazorPages\\uploads\\{fileName}";
            if (fileName != null)
            {
                using StreamReader r = new(path);
                var json = r.ReadToEnd();
                Entries = JsonConvert.DeserializeObject<List<Entry>>(json);
            }

            return (Entries ?? throw new InvalidOperationException("Empty youtube history was found. ")).ToList();
        }
    }
}