using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Data;

namespace RazorPages.Pages.Person
{
    public class EditModel : PageModel
    {
        private readonly DatabaseContext _dbContext;

        public EditModel(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Data.Person Person { get; set; }

        public IActionResult OnGet(int id)
        {
            var person = _dbContext.Person.Find(id);
            if (person == null)
                return NotFound();
            Person = person;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                if (Person == null) return NotFound();
                _dbContext.Person.Update(Person);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "Saved successfully";
                return RedirectToPage("DisplayAll");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error has occurred";
            }
            return Page();
        }
    }
}