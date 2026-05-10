using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class SedesModel : PageModel
    {
        private ISedesNegocio? iSedesNegocio;

        [BindProperty] public List<Sedes>? Lista { get; set; }
        [BindProperty] public Sedes? Sede { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public SedesModel()
        {
            iSedesNegocio = new SedesNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iSedesNegocio?.Consultar();
                Sede = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Sede = new Sedes();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Sede == null) return;
                if (Sede.Id == 0)
                    Sede = iSedesNegocio!.Guardar(Sede);
                else
                    Sede = iSedesNegocio!.Actualizar(Sede);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Sede = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Sede = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Sede == null) return;
                iSedesNegocio!.Eliminar(Sede.Id);
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
    