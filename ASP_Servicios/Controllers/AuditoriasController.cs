using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditoriasController : ControllerBase
    {
        private IAuditoriasNegocio? IAuditoriasNegocio;

        public AuditoriasController()
        {
            IAuditoriasNegocio = new AuditoriasNegocio();
        }

        [HttpGet]
        public List<Auditorias> Consultar()
        {
            if (IAuditoriasNegocio == null)
                throw new Exception("No implementado");
            return IAuditoriasNegocio!.Consultar();
        }
    }
}
