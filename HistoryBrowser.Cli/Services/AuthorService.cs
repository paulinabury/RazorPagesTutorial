using HistoryBrowser.Cli.Entities;

namespace HistoryBrowser.Cli.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetPaginationResult(int currentPage, int pageSize = 10);

        Task<int> GetCount();

        List<Author> GetData();
    }

    public class AuthorService : IAuthorService
    {
        public List<Author> GetData()
        {
            var reader = new JsonReader();
            var data = reader.ReadVideosHistory();
            var authorCounter = new AuthorsCounter();

            var authors = authorCounter.GetAuthors(data);

            return authors;
        }

        public async Task<List<Author>> GetPaginationResult(int currentPage, int pageSize = 10)
        {
            var data = GetData();
            return data.OrderByDescending(x => x.Films.Count).Skip((currentPage - 1) * pageSize).Take(pageSize)
                .ToList();
        }

        public async Task<int> GetCount()
        {
            var data = GetData();
            return data.Count;
        }
    }
}