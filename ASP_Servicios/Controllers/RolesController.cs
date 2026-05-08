using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RolesController : ControllerBase
    {
        private IRolesNegocio? IRolesNegocio;
        public RolesController()
        {
            IRolesNegocio = new RolesNegocio();
        }

        [HttpGet]
        public List<Roles> Consultar()
        {
            if (IRolesNegocio == null)
                throw new Exception("No implementado");
            return IRolesNegocio!.Consultar();
        }

        [HttpPost]
        public Roles Guardar([FromBody] Roles entidad)
        {
            if (IRolesNegocio == null)
                throw new Exception("No implementado");
            return IRolesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Roles Actualizar([FromBody] Roles entidad)
        {
            if (IRolesNegocio == null)
                throw new Exception("No implementado");
            return IRolesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IRolesNegocio == null)
                throw new Exception("No implementado");
            return IRolesNegocio!.Eliminar(id);
        }
    }
}
