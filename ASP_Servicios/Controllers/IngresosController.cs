using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IngresosController : ControllerBase
    {
        private IIngresosNegocio? IIngresosNegocio;

        public IngresosController()
        {
            IIngresosNegocio = new IngresosNegocio();
        }

        [HttpGet]
        public List<Ingresos> Consultar()
        {
            if (IIngresosNegocio == null)
                throw new Exception("No implementado");
            return IIngresosNegocio!.Consultar();
        }

        [HttpPost]
        public Ingresos Guardar([FromBody] Ingresos entidad)
        {
            if (IIngresosNegocio == null)
                throw new Exception("No implementado");
            return IIngresosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Ingresos Actualizar([FromBody] Ingresos entidad)
        {
            if (IIngresosNegocio == null)
                throw new Exception("No implementado");
            return IIngresosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IIngresosNegocio == null)
                throw new Exception("No implementado");
            return IIngresosNegocio!.Eliminar(id);
        }
    }
}
