using HistoryBrowser.Cli;
using HistoryBrowser.Cli.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        public Subtitle MostPopularAuthor { get; set; }

        public IActionResult OnGet()
        {
            JsonReader reader = new();
            AuthorsCounter authorsCounter = new();
            var videoHistory =
                    reader.ReadVideosHistory();
            var authorsInfo = authorsCounter.GetMostPopularAuthor(videoHistory);
            MostPopularAuthor = new Subtitle()
            {
                Name = authorsInfo.Name,
                Url = authorsInfo.Url
            };
            return Page();
        }
    }
}