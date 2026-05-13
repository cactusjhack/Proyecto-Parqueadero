using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class PagosModel : PageModel
    {
        private IPagosNegocio? iPagosNegocio;

        [BindProperty] public List<Pagos>? Lista { get; set; }
        [BindProperty] public Pagos? Pago { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public PagosModel()
        {
            iPagosNegocio = new PagosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iPagosNegocio?.Consultar();
                Pago = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Pago = new Pagos();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Pago == null) return;
                if (Pago.Id == 0)
                    Pago = iPagosNegocio!.Guardar(Pago);
                else
                    Pago = iPagosNegocio!.Actualizar(Pago);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Pago = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Pago = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Pago == null) return;
                iPagosNegocio!.Eliminar(Pago.Id);
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
