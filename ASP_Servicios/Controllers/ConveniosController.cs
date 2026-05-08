using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ConveniosController : ControllerBase
    {
        private IConveniosNegocio? IConveniosNegocio;

        public ConveniosController()
        {
            IConveniosNegocio = new ConveniosNegocio();
        }

        [HttpGet]
        public List<Convenios> Consultar()
        {
            if (IConveniosNegocio == null)
                throw new Exception("No implementado");
            return IConveniosNegocio!.Consultar();
        }

        [HttpPost]
        public Convenios Guardar([FromBody] Convenios entidad)
        {
            if (IConveniosNegocio == null)
                throw new Exception("No implementado");
            return IConveniosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Convenios Actualizar([FromBody] Convenios entidad)
        {
            if (IConveniosNegocio == null)
                throw new Exception("No implementado");
            return IConveniosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IConveniosNegocio == null)
                throw new Exception("No implementado");
            return IConveniosNegocio!.Eliminar(id);
        }
    }
}
