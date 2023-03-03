using HistoryBrowser.Cli;
using HistoryBrowser.Cli.Entities;
using HistoryBrowser.Cli.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Models;

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
        public List<Author> TopThree { get; set; } = new();
        public Dictionary<string, List<Film>> ChannelsNumberOfViews { get; set; } = new();
        public Dictionary<string, int> VideosNumberOfViews { get; set; } = new();

        public Pager Pager { get; set; }

        public IActionResult OnGet(int pg = 1)
        {
            var authors = _service.GetData();

            const int pageSize = 10;
            if (pg < 1) pg = 1;

            var authorsCount = authors.Count();
            Pager = new Pager(authorsCount, pg, pageSize);
            var recSkip = (pg - 1) * pageSize;

            var ordered = authors.OrderByDescending(x => x.Films.Count).ToList();
            var top = new List<Author>();
            for (var i = 0; i < ordered.Count; i++)
            {
                if (i > 2) break;
                top.Add(ordered[i]);
            }

            TopThree = top;

            Authors = ordered.Skip(recSkip).Take(Pager.PageSize).ToList();

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