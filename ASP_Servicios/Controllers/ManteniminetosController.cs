using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MantenimientosController : ControllerBase
    {
        private IMantenimientosNegocio? IMantenimientosNegocio;

        public MantenimientosController()
        {
            IMantenimientosNegocio = new MantenimientosNegocio();
        }

        [HttpGet]
        public List<Mantenimientos> Consultar()
        {
            if (IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return IMantenimientosNegocio!.Consultar();
        }

        [HttpPost]
        public Mantenimientos Guardar([FromBody] Mantenimientos entidad)
        {
            if (IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return IMantenimientosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Mantenimientos Actualizar([FromBody] Mantenimientos entidad)
        {
            if (IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return IMantenimientosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return IMantenimientosNegocio!.Eliminar(id);
        }
    }
}
