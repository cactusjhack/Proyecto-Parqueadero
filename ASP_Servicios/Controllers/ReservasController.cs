using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservasController : ControllerBase
    {
        private IReservasNegocio? IReservasNegocio;

        public ReservasController()
        {
            IReservasNegocio = new ReservasNegocio();
        }

        [HttpGet]
        public List<Reservas> Consultar()
        {
            if (IReservasNegocio == null)
                throw new Exception("No implementado");
            return IReservasNegocio!.Consultar();
        }

        [HttpPost]
        public Reservas Guardar([FromBody] Reservas entidad)
        {
            if (IReservasNegocio == null)
                throw new Exception("No implementado");
            return IReservasNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Reservas Actualizar([FromBody] Reservas entidad)
        {
            if (IReservasNegocio == null)
                throw new Exception("No implementado");
            return IReservasNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IReservasNegocio == null)
                throw new Exception("No implementado");
            return IReservasNegocio!.Eliminar(id);
        }
    }
}
