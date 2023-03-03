namespace HistoryBrowser.Cli.Entities
{
    public class Author
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Film> Films { get; set; } = new();
    }
}