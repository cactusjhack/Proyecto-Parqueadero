using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FichosController : ControllerBase
    {
        private IFichosNegocio? IFichosNegocio;

        public FichosController()
        {
            IFichosNegocio = new FichosNegocio();
        }

        [HttpGet]
        public List<Fichos> Consultar()
        {
            if (IFichosNegocio == null)
                throw new Exception("No implementado");
            return IFichosNegocio!.Consultar();
        }

        [HttpPost]
        public Fichos Guardar([FromBody] Fichos entidad)
        {
            if (IFichosNegocio == null)
                throw new Exception("No implementado");
            return IFichosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Fichos Actualizar([FromBody] Fichos entidad)
        {
            if (IFichosNegocio == null)
                throw new Exception("No implementado");
            return IFichosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IFichosNegocio == null)
                throw new Exception("No implementado");
            return IFichosNegocio!.Eliminar(id);
        }
    }
}
