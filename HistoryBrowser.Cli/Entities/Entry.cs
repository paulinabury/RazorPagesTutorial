namespace HistoryBrowser.Cli.Entities
{
    public class Entry
    {
        public string? Header { get; set; }
        public string? Title { get; set; }
        public string? TitleUrl { get; set; }
        public List<Subtitle>? Subtitles { get; set; }
        public DateTime? Time { get; set; }
        public List<string>? Products { get; set; }
        public List<string>? ActivityControls { get; set; }
    }
}