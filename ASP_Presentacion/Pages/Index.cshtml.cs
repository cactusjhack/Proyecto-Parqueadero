using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
                return RedirectToPage("/Loging");

            return Page();
        }

        public IActionResult OnPostBtSalir()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}
