using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientesController : ControllerBase
    {
        private IClientesNegocio? IClientesNegocio;

        public ClientesController()
        {
            IClientesNegocio = new ClientesNegocio();
        }

        [HttpGet]
        public List<Clientes> Consultar()
        {
            if (IClientesNegocio == null)
                throw new Exception("No implementado");
            return IClientesNegocio!.Consultar();
        }

        [HttpPost]
        public Clientes Guardar([FromBody] Clientes entidad)
        {
            if (IClientesNegocio == null)
                throw new Exception("No implementado");
            return IClientesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Clientes Actualizar([FromBody] Clientes entidad)
        {
            if (IClientesNegocio == null)
                throw new Exception("No implementado");
            return IClientesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IClientesNegocio == null)
                throw new Exception("No implementado");
            return IClientesNegocio!.Eliminar(id);
        }
    }
}
