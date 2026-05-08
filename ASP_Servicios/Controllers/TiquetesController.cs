using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TiquetesController : ControllerBase
    {
        private ITiquetesNegocio? ITiquetesNegocio;

        public TiquetesController()
        {
            ITiquetesNegocio = new TiquetesNegocio();
        }

        [HttpGet]
        public List<Tiquetes> Consultar()
        {
            if (ITiquetesNegocio == null)
                throw new Exception("No implementado");
            return ITiquetesNegocio!.Consultar();
        }

        [HttpPost]
        public Tiquetes Guardar([FromBody] Tiquetes entidad)
        {
            if (ITiquetesNegocio == null)
                throw new Exception("No implementado");
            return ITiquetesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Tiquetes Actualizar([FromBody] Tiquetes entidad)
        {
            if (ITiquetesNegocio == null)
                throw new Exception("No implementado");
            return ITiquetesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ITiquetesNegocio == null)
                throw new Exception("No implementado");
            return ITiquetesNegocio!.Eliminar(id);
        }
    }
}
