namespace HistoryBrowser.Cli.Entities
{
    public class Entry
    {
        public string? Header { get; set; }
        public string? Title { get; set; } //video name
        public string? TitleUrl { get; set; } //video url
        public List<Subtitle>? Subtitles { get; set; }
        public DateTime? Time { get; set; }
        public List<string>? Products { get; set; }
        public List<string>? ActivityControls { get; set; }
    }
}