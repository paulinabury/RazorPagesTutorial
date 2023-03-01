using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Data;

namespace RazorPages.Pages.Person
{
    public class DisplayAll : PageModel
    {
        private readonly DatabaseContext _dbContext;

        public DisplayAll(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Data.Person> People { get; set; }

        public IActionResult OnGet()
        {
            People = _dbContext.Person.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var person = _dbContext.Person.Find(id);
            if (person == null) return NotFound();

            try
            {
                _dbContext.Person.Remove(person);
                _dbContext.SaveChanges();
                TempData["success"] = "Saved successfully";
            }
            catch (Exception e)
            {
                TempData["error"] = "Error has occurred";
            }

            return RedirectToPage();
        }
    }
}