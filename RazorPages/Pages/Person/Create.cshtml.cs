using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Data;

namespace RazorPages.Pages.Person
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _dbContext;

        public CreateModel(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Data.Person Person { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                if (Person == null) return NotFound();
                _dbContext.Person.Add(Person);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Saved successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error has occurred";
            }
            return RedirectToPage();
        }
    }
}