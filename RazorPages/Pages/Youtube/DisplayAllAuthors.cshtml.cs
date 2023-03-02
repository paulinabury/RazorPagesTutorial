using HistoryBrowser.Cli;
using HistoryBrowser.Cli.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Youtube
{
    public class DisplayAllAuthorsModel : PageModel
    {
        public Dictionary<string, List<Film>> Authors { get; set; } = new();
        public Dictionary<string, int> Videos { get; set; } = new();

        public IActionResult OnGet()
        {
            JsonReader reader = new();
            AuthorsCounter authorsCounter = new();
            var videoHistory =
                reader.ReadVideosHistory();
            Authors = authorsCounter.GetAuthorsAndTheirFilms(videoHistory);
            Videos = authorsCounter.GetMostViewedVideo();
            return Page();
        }
    }
}