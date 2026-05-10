using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosNegocio? IUsuariosNegocio;

        public UsuariosController()
        {
            IUsuariosNegocio = new UsuariosNegocio();
        }
        public class LoginDto
        {
            public string NombreUsuario { get; set; } = null!;
            public string Contrasena { get; set; } = null!;
        }

        [HttpGet]
        public List<Usuarios> Consultar()
        {
            if (IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return IUsuariosNegocio!.Consultar();
        }

        [HttpPost]
        public Usuarios Guardar([FromBody] Usuarios entidad)
        {
            if (IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return IUsuariosNegocio!.Guardar(entidad);
        }

        [HttpPost("Loging")]
        public IActionResult Loging([FromBody] LoginDto dto)
        {
            if (IUsuariosNegocio == null)
                throw new Exception("No implementado");

            var usuario = IUsuariosNegocio!.Login(dto.NombreUsuario, dto.Contrasena);
            if (usuario == null)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

            return Ok(usuario);
        }

        [HttpPut]
        public Usuarios Actualizar([FromBody] Usuarios entidad)
        {
            if (IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return IUsuariosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IUsuariosNegocio == null)
                throw new Exception("No implementado");
            return IUsuariosNegocio!.Eliminar(id);
        }
    }
}
