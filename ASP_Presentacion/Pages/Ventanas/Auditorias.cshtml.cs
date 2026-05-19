using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasNegocio? iAuditoriasNegocio;

        [BindProperty] public List<Auditorias>? Lista { get; set; }
        [BindProperty] public Auditorias? Auditoria { get; set; }

        public AuditoriasModel()
        {
            iAuditoriasNegocio = new AuditoriasNegocio();
        }

        public IActionResult OnGet()
        {
            var usuario = HttpContext.Session.GetString("Usuario");

            if (usuario != "andrea.gomez")
            {
                return RedirectToPage("/AccesoDenegado");
            }

            OnPostBtRefrescar();
            return Page();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iAuditoriasNegocio?.Consultar();
                Auditoria = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
    }
}
