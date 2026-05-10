using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string? NombreUsuario { get; set; }

        [BindProperty]
        public string? Contrasena { get; set; }

        // Propiedad para mostrar mensajes de error en la vista
        public string? ErrorMessage { get; set; }
        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = new HttpClient();
            var body = new { NombreUsuario, Contrasena };
            var response = await client.PostAsJsonAsync("https://localhost:7160/Usuarios/Loging/Loging", body);

            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("Usuario", NombreUsuario!);
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Nombre de usuario o contraseña incorrectos.";
                return Page();
            }
        }
    }
}