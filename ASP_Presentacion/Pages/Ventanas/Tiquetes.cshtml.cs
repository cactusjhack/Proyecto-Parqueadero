using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class TiquetesModel : PageModel
    {
        private ITiquetesNegocio? iTiquetesNegocio;

        [BindProperty] public List<Tiquetes>? Lista { get; set; }
        [BindProperty] public Tiquetes? Tiquete { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public TiquetesModel()
        {
            iTiquetesNegocio = new TiquetesNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iTiquetesNegocio?.Consultar();
                Tiquete = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Tiquete = new Tiquetes();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Tiquete == null) return;
                if (Tiquete.Id == 0)
                    Tiquete = iTiquetesNegocio!.Guardar(Tiquete);
                else
                    Tiquete = iTiquetesNegocio!.Actualizar(Tiquete);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Tiquete = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Tiquete = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Tiquete == null) return;
                iTiquetesNegocio!.Eliminar(Tiquete.Id);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}
