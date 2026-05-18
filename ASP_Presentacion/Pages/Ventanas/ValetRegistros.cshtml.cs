using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class ValetRegistrosModel : PageModel
    {
        private IValetRegistrosNegocio? iValetRegistrosNegocio;

        [BindProperty] public List<ValetRegistros>? Lista { get; set; }
        [BindProperty] public ValetRegistros? ValetRegistro { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ValetRegistrosModel()
        {
            iValetRegistrosNegocio = new ValetRegistrosNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iValetRegistrosNegocio?.Consultar();
                ValetRegistro = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            ValetRegistro = new ValetRegistros();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (ValetRegistro == null) return;
                if (ValetRegistro.Id == 0)
                    ValetRegistro = iValetRegistrosNegocio!.Guardar(ValetRegistro);
                else
                    ValetRegistro = iValetRegistrosNegocio!.Actualizar(ValetRegistro);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                ValetRegistro = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                ValetRegistro = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (ValetRegistro == null) return;
                iValetRegistrosNegocio!.Eliminar(ValetRegistro.Id);
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
