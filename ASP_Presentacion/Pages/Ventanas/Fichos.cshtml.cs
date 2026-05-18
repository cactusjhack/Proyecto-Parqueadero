using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class FichosModel : PageModel
    {
        private IFichosNegocio? iFichosNegocio;

        [BindProperty] public List<Fichos>? Lista { get; set; }
        [BindProperty] public Fichos? Ficho { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public FichosModel()
        {
            iFichosNegocio = new FichosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iFichosNegocio?.Consultar();
                Ficho = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Ficho = new Fichos();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Ficho == null) return;
                if (Ficho.Id == 0)
                    Ficho = iFichosNegocio!.Guardar(Ficho);
                else
                    Ficho = iFichosNegocio!.Actualizar(Ficho);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Ficho = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Ficho = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Ficho == null) return;
                iFichosNegocio!.Eliminar(Ficho.Id);
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
