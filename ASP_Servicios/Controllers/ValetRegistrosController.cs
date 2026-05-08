using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ValetRegistrosController : ControllerBase
    {
        private IValetRegistrosNegocio? IValetRegistrosNegocio;

        public ValetRegistrosController()
        {
            IValetRegistrosNegocio = new ValetRegistrosNegocio();
        }

        [HttpGet]
        public List<ValetRegistros> Consultar()
        {
            if (IValetRegistrosNegocio == null)
                throw new Exception("No implementado");
            return IValetRegistrosNegocio!.Consultar();
        }

        [HttpPost]
        public ValetRegistros Guardar([FromBody] ValetRegistros entidad)
        {
            if (IValetRegistrosNegocio == null)
                throw new Exception("No implementado");
            return IValetRegistrosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public ValetRegistros Actualizar([FromBody] ValetRegistros entidad)
        {
            if (IValetRegistrosNegocio == null)
                throw new Exception("No implementado");
            return IValetRegistrosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IValetRegistrosNegocio == null)
                throw new Exception("No implementado");
            return IValetRegistrosNegocio!.Eliminar(id);
        }
    }
}
