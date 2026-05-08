using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CamarasController : ControllerBase
    {
        private ICamarasNegocio? ICamarasNegocio;

        public CamarasController()
        {
            ICamarasNegocio = new CamarasNegocio();
        }

        [HttpGet]
        public List<Camaras> Consultar()
        {
            if (ICamarasNegocio == null)
                throw new Exception("No implementado");
            return ICamarasNegocio!.Consultar();
        }

        [HttpPost]
        public Camaras Guardar([FromBody] Camaras entidad)
        {
            if (ICamarasNegocio == null)
                throw new Exception("No implementado");
            return ICamarasNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Camaras Actualizar([FromBody] Camaras entidad)
        {
            if (ICamarasNegocio == null)
                throw new Exception("No implementado");
            return ICamarasNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ICamarasNegocio == null)
                throw new Exception("No implementado");
            return ICamarasNegocio!.Eliminar(id);
        }
    }
}
