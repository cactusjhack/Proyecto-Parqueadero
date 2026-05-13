using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class CobrosModel : PageModel
    {
        private ICobrosNegocio? iCobrosNegocio;

        [BindProperty] public List<Cobros>? Lista { get; set; }
        [BindProperty] public Cobros? Cobro { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public CobrosModel()
        {
            iCobrosNegocio = new CobrosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iCobrosNegocio?.Consultar();
                Cobro = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Cobro = new Cobros();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Cobro == null) return;
                if (Cobro.Id == 0)
                    Cobro = iCobrosNegocio!.Guardar(Cobro);
                else
                    Cobro = iCobrosNegocio!.Actualizar(Cobro);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Cobro = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Cobro = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Cobro == null) return;
                iCobrosNegocio!.Eliminar(Cobro.Id);
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
