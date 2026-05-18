using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP_Presentacion.Pages.Ventanas
{
    public class NotificacionesModel : PageModel
    {
        private INotificacionesNegocio? iNotificacionesNegocio;

        [BindProperty] public List<Notificaciones>? Lista { get; set; }
        [BindProperty] public Notificaciones? Notificacion { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public NotificacionesModel()
        {
            iNotificacionesNegocio = new NotificacionesNegocio();
        }

        public void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = iNotificacionesNegocio?.Consultar();
                Notificacion = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtNuevo()
        {
            Notificacion = new Notificaciones();
            Borrando = false;
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Notificacion == null) return;
                if (Notificacion.Id == 0)
                    Notificacion = iNotificacionesNegocio!.Guardar(Notificacion);
                else
                    Notificacion = iNotificacionesNegocio!.Actualizar(Notificacion);
                OnPostBtRefrescar();
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Notificacion = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }
        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Notificacion = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Notificacion == null) return;
                iNotificacionesNegocio!.Eliminar(Notificacion.Id);
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
