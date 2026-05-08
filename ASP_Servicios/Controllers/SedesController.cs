using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SedesController : ControllerBase
    {
        private ISedesNegocio? ISedesNegocio;

        public SedesController()
        {
            ISedesNegocio = new SedesNegocio();
        }

        [HttpGet]
        public List<Sedes> Consultar()
        {
            if (ISedesNegocio == null)
                throw new Exception("No implementado");
            return ISedesNegocio!.Consultar();
        }

        [HttpPost]
        public Sedes Guardar([FromBody] Sedes entidad)
        {
            if (ISedesNegocio == null)
                throw new Exception("No implementado");
            return ISedesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Sedes Actualizar([FromBody] Sedes entidad)
        {
            if (ISedesNegocio == null)
                throw new Exception("No implementado");
            return ISedesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ISedesNegocio == null)
                throw new Exception("No implementado");
            return ISedesNegocio!.Eliminar(id);
        }
    }
}
