using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NotificacionesController : ControllerBase
    {
        private INotificacionesNegocio? INotificacionesNegocio;

        public NotificacionesController()
        {
            INotificacionesNegocio = new NotificacionesNegocio();
        }

        [HttpGet]
        public List<Notificaciones> Consultar()
        {
            if (INotificacionesNegocio == null)
                throw new Exception("No implementado");
            return INotificacionesNegocio!.Consultar();
        }

        [HttpPost]
        public Notificaciones Guardar([FromBody] Notificaciones entidad)
        {
            if (INotificacionesNegocio == null)
                throw new Exception("No implementado");
            return INotificacionesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Notificaciones Actualizar([FromBody] Notificaciones entidad)
        {
            if (INotificacionesNegocio == null)
                throw new Exception("No implementado");
            return INotificacionesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (INotificacionesNegocio == null)
                throw new Exception("No implementado");
            return INotificacionesNegocio!.Eliminar(id);
        }
    }
}
