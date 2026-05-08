using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VehiculosController : ControllerBase
    {
        private IVehiculosNegocio? IVehiculosNegocio;

        public VehiculosController()
        {
            IVehiculosNegocio = new VehiculosNegocio();
        }

        [HttpGet]
        public List<Vehiculos> Consultar()
        {
            if (IVehiculosNegocio == null)
                throw new Exception("No implementado");
            return IVehiculosNegocio!.Consultar();
        }

        [HttpPost]
        public Vehiculos Guardar([FromBody] Vehiculos entidad)
        {
            if (IVehiculosNegocio == null)
                throw new Exception("No implementado");
            return IVehiculosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Vehiculos Actualizar([FromBody] Vehiculos entidad)
        {
            if (IVehiculosNegocio == null)
                throw new Exception("No implementado");
            return IVehiculosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IVehiculosNegocio == null)
                throw new Exception("No implementado");
            return IVehiculosNegocio!.Eliminar(id);
        }
    }
}
