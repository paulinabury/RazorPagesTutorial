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

        public Subtitle GetMostPopularAuthor(List<Entry> entries)
        {
            var dict = new Dictionary<Subtitle, int>();

            foreach (var entry in entries)
            {
                if (entry.Subtitles is null) continue;

                var name = entry.Subtitles[0].Name;
                var url = entry.Subtitles[0].Url;

                var author = new Subtitle
                {
                    Name = name,
                    Url = url
                };

                if (dict.ContainsKey(author)) dict[author]++;
                else dict.Add(author, 1);
            }

            return dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value).FirstOrDefault().Key;
        }
    }
}