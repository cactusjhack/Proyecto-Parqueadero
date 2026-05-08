using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TarifasController : ControllerBase
    {
        private ITarifasNegocio? ITarifaNegocio;

        public TarifasController()
        {
            ITarifaNegocio = new TarifasNegocio();
        }

        [HttpGet]
        public List<Tarifas> Consultar()
        {
            if (ITarifaNegocio == null)
                throw new Exception("No implementado");
            return ITarifaNegocio!.Consultar();
        }

        [HttpPost]
        public Tarifas Guardar([FromBody] Tarifas entidad)
        {
            if (ITarifaNegocio == null)
                throw new Exception("No implementado");
            return ITarifaNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Tarifas Actualizar([FromBody] Tarifas entidad)
        {
            if (ITarifaNegocio == null)
                throw new Exception("No implementado");
            return ITarifaNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ITarifaNegocio == null)
                throw new Exception("No implementado");
            return ITarifaNegocio!.Eliminar(id);
        }
    }
}
