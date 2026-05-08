using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EspaciosController : ControllerBase
    {
        private IEspaciosNegocio? IEspaciosNegocio;

        public EspaciosController()
        {
            IEspaciosNegocio = new EspaciosNegocio();
        }

        [HttpGet]
        public List<Espacios> Consultar()
        {
            if (IEspaciosNegocio == null)
                throw new Exception("No implementado");
            return IEspaciosNegocio!.Consultar();
        }

        [HttpPost]
        public Espacios Guardar([FromBody] Espacios entidad)
        {
            if (IEspaciosNegocio == null)
                throw new Exception("No implementado");
            return IEspaciosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Espacios Actualizar([FromBody] Espacios entidad)
        {
            if (IEspaciosNegocio == null)
                throw new Exception("No implementado");
            return IEspaciosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IEspaciosNegocio == null)
                throw new Exception("No implementado");
            return IEspaciosNegocio!.Eliminar(id);
        }
    }
}
