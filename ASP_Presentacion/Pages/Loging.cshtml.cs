using Microsoft.AspNetCore.Components.Forms;
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
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = 
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var client = new HttpClient(handler);
            var body = new { NombreUsuario, Contrasena };
            var response = await client.PostAsJsonAsync("https://localhost:7160/Usuarios/Loging/Loging", body);

            if (response.IsSuccessStatusCode)
            {
                var usuario = await response.Content
                    .ReadFromJsonAsync<Dictionary<string, object>>();

                HttpContext.Session.SetString("Usuario", NombreUsuario!);

                if (usuario != null && usuario.ContainsKey("rol"))
                {
                    var rolId = usuario["rol"].ToString();
                    var rolResponse = await client.GetAsync
                        ($"https://localhost:7160/Roles/Consultar");

                    if (rolResponse.IsSuccessStatusCode)
                    {
                        var roles = await rolResponse.Content
                            .ReadFromJsonAsync<List<Dictionary<string, object>>>();

                        var rol = roles?.FirstOrDefault(r =>
                        r["id"].ToString() == rolId);
                        if (rol != null)
                            HttpContext.Session.SetString("Rol", rol["nombre"].ToString()!);
                    }
                }

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