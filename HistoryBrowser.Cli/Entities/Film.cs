namespace HistoryBrowser.Cli.Entities
{
    public class Film
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime? Time { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType()) return false;
            var f = (Film)obj;
            return (Title == f.Title) && (Url == f.Url);
        }
    }
}