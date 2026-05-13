using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class PromocionesModel : PageModel
    {
        private IPromocionesNegocio? iPromocionesNegocio;

        [BindProperty] public List<Promociones>? Lista { get; set; }
        [BindProperty] public Promociones? Promocion { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public PromocionesModel()
        {
            iPromocionesNegocio = new PromocionesNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iPromocionesNegocio?.Consultar();
                Promocion = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Promocion = new Promociones();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Promocion == null) return;
                if (Promocion.Id == 0)
                    Promocion = iPromocionesNegocio!.Guardar(Promocion);
                else
                    Promocion = iPromocionesNegocio!.Actualizar(Promocion);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Promocion = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Promocion = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Promocion == null) return;
                iPromocionesNegocio!.Eliminar(Promocion.Id);
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
