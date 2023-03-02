using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace RazorPages.Pages.Youtube
{
    public class UploadModel : PageModel
    {
        private IHostingEnvironment _environment;

        public UploadModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var ticks = DateTime.Now.Ticks;
            var file = Path.Combine(_environment.ContentRootPath, "uploads", ticks.ToString());
            await using var fileStream = new FileStream(file, FileMode.Create);
            try
            {
                await Upload.CopyToAsync(fileStream);
                TempData["success"] = "Saved successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error has occurred";
            }
            return RedirectToPage("DisplayAllAuthors");
        }
    }
}