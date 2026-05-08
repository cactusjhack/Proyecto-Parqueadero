using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DetallesController : ControllerBase
    {
        private IDetallesNegocio? IDetallesNegocio;

        public DetallesController()
        {
            IDetallesNegocio = new DetallesNegocio();
        }

        [HttpGet]
        public List<Detalles> Consultar()
        {
            if (IDetallesNegocio == null)
                throw new Exception("No implementado");
            return IDetallesNegocio!.Consultar();
        }

        [HttpPost]
        public Detalles Guardar([FromBody] Detalles entidad)
        {
            if (IDetallesNegocio == null)
                throw new Exception("No implementado");
            return IDetallesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Detalles Actualizar([FromBody] Detalles entidad)
        {
            if (IDetallesNegocio == null)
                throw new Exception("No implementado");
            return IDetallesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IDetallesNegocio == null)
                throw new Exception("No implementado");
            return IDetallesNegocio!.Eliminar(id);
        }
    }
}
