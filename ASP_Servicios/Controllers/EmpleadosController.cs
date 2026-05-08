using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmpleadosController : ControllerBase
    {
        private IEmpleadosNegocio? IEmpleadosNegocio;

        public EmpleadosController()
        {
            IEmpleadosNegocio = new EmpleadosNegocio();
        }

        [HttpGet]
        public List<Empleados> Consultar()
        {
            if (IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return IEmpleadosNegocio!.Consultar();
        }

        [HttpPost]
        public Empleados Guardar([FromBody] Empleados entidad)
        {
            if (IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return IEmpleadosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Empleados Actualizar([FromBody] Empleados entidad)
        {
            if (IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return IEmpleadosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return IEmpleadosNegocio!.Eliminar(id);
        }
    }
}
