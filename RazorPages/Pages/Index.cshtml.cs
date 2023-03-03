using HistoryBrowser.Cli;
using HistoryBrowser.Cli.Entities;
using HistoryBrowser.Cli.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private IAuthorService _service;

        public IndexModel(IAuthorService service)
        {
            _service = service;
        }

        public List<Author> Authors { get; set; }
        public Dictionary<string, List<Film>> ChannelsNumberOfViews { get; set; } = new();
        public Dictionary<string, int> VideosNumberOfViews { get; set; } = new();

        public IActionResult OnGet()
        {
            var authors = _service.GetData();
            var ordered = authors.OrderByDescending(x => x.Films.Count).ToList();
            Authors = ordered;

            //TODO
            JsonReader reader = new();
            AuthorsCounter authorsCounter = new();
            var videoHistory =
                reader.ReadVideosHistory();
            ChannelsNumberOfViews = authorsCounter.GetAuthorsAndTheirFilms(videoHistory);
            VideosNumberOfViews = authorsCounter.GetMostViewedVideo();
            return Page();
        }
    }
}