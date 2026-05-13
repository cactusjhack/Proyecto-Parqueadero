using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class PisosModel : PageModel
    {
        private IPisosNegocio? iPisosNegocio;

        [BindProperty] public List<Pisos>? Lista { get; set; }
        [BindProperty] public Pisos? Piso { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public PisosModel()
        {
            iPisosNegocio = new PisosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iPisosNegocio?.Consultar();
                Piso = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Piso = new Pisos();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Piso == null) return;
                if (Piso.Id == 0)
                    Piso = iPisosNegocio!.Guardar(Piso);
                else
                    Piso = iPisosNegocio!.Actualizar(Piso);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Piso = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Piso = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Piso == null) return;
                iPisosNegocio!.Eliminar(Piso.Id);
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
