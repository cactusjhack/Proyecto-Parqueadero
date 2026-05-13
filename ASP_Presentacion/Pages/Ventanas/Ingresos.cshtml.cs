using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class IngresosModel : PageModel
    {
        private IIngresosNegocio? iIngresosNegocio;

        [BindProperty] public List<Ingresos>? Lista { get; set; }
        [BindProperty] public Ingresos? Ingreso { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public IngresosModel()
        {
            iIngresosNegocio = new IngresosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iIngresosNegocio?.Consultar();
                Ingreso = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Ingreso = new Ingresos();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Ingreso == null) return;
                if (Ingreso.Id == 0)
                    Ingreso = iIngresosNegocio!.Guardar(Ingreso);
                else
                    Ingreso = iIngresosNegocio!.Actualizar(Ingreso);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Ingreso = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Ingreso = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Ingreso == null) return;
                iIngresosNegocio!.Eliminar(Ingreso.Id);
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
