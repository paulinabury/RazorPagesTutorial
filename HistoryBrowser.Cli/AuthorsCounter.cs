using HistoryBrowser.Cli.Entities;

namespace HistoryBrowser.Cli
{
    public class AuthorsCounter
    {
        private Dictionary<string, List<Film>> AuthorList { get; } = new();
        private Dictionary<string, int> VideoCount { get; } = new();

        public Dictionary<string, List<Film>> GetAuthorsAndTheirFilms(List<Entry>? entries)
        {
            foreach (var e in entries)
            {
                if (e.Subtitles is null) continue;

                var name = e.Subtitles[0].Name;
                var title = e.Title.Remove(0, 10).Normalize();
                var url = e.TitleUrl;
                var time = e.Time;

                var film = new Film() { Title = title, Url = url, Time = time };

                if (AuthorList.ContainsKey(name))
                {
                    AuthorList[name].Add(film);
                    AuthorList[name].OrderByDescending(x => x.Time);
                }
                else
                {
                    AuthorList.Add(name, new List<Film> { film });
                }
            }

            return AuthorList.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, int> GetMostViewedVideo()
        {
            foreach (var f in AuthorList.SelectMany(a => a.Value))
            {
                if (VideoCount.ContainsKey(f.Title)) VideoCount[f.Title]++;
                else VideoCount.Add(f.Title, 1);
            }

            return VideoCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public List<Author> GetAuthors(List<Entry> entries)
        {
            var authors = new List<Author>();

            foreach (var entry in entries)
            {
                if (entry.Subtitles is null) continue;

                var author = new Author { Name = entry.Subtitles[0].Name, Url = entry.Subtitles[0].Url };

                var film = new Film { Title = entry.Title.Remove(0, 10), Time = entry.Time, Url = entry.TitleUrl };

                var index = authors.FindIndex(a => a.Name == author.Name);

                if (index < 0)
                {
                    author.Films.Add(film);
                    authors.Add(author);
                }
                else authors[index].Films.Add(film);
            }
            return authors;
        }
    }
}