using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class IncidentesModel : PageModel
    {
        private IIncidentesNegocio? iIncidentesNegocio;

        [BindProperty] public List<Incidentes>? Lista { get; set; }
        [BindProperty] public Incidentes? Incidente { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public IncidentesModel()
        {
            iIncidentesNegocio = new IncidentesNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iIncidentesNegocio?.Consultar();
                Incidente = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Incidente = new Incidentes();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Incidente == null) return;
                if (Incidente.Id == 0)
                    Incidente = iIncidentesNegocio!.Guardar(Incidente);
                else
                    Incidente = iIncidentesNegocio!.Actualizar(Incidente);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Incidente = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Incidente = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Incidente == null) return;
                iIncidentesNegocio!.Eliminar(Incidente.Id);
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
