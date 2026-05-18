using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class DetallesModel : PageModel
    {
        private IDetallesNegocio? iDetallesNegocio;

        [BindProperty] public List<Detalles>? Lista { get; set; }
        [BindProperty] public Detalles? Detalle { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public DetallesModel()
        {
            iDetallesNegocio = new DetallesNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iDetallesNegocio?.Consultar();
                Detalle = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Detalle = new Detalles();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Detalle == null) return;
                if (Detalle.Id == 0)
                    Detalle = iDetallesNegocio!.Guardar(Detalle);
                else
                    Detalle = iDetallesNegocio!.Actualizar(Detalle);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Detalle = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Detalle = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Detalle == null) return;
                iDetallesNegocio!.Eliminar(Detalle.Id);
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
