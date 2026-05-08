using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PromocionesController : ControllerBase
    {
        private IPromocionesNegocio? IPromocionesNegocio;

        public PromocionesController()
        {
            IPromocionesNegocio = new PromocionesNegocio();
        }

        [HttpGet]
        public List<Promociones> Consultar()
        {
            if (IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return IPromocionesNegocio!.Consultar();
        }

        [HttpPost]
        public Promociones Guardar([FromBody] Promociones entidad)
        {
            if (IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return IPromocionesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Promociones Actualizar([FromBody] Promociones entidad)
        {
            if (IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return IPromocionesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return IPromocionesNegocio!.Eliminar(id);
        }
    }
}
