using ClosedXML.Excel;
using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var usuarios = IUsuariosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Usuarios");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "NombreUsuario";
            ws.Cell(1, 3).Value = "Contrasena";
            ws.Cell(1, 4).Value = "Activo";
            ws.Cell(1, 5).Value = "Rol";
            ws.Cell(1, 6).Value = "Empleado";

            // Datos
            for (int i = 0; i < usuarios.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = usuarios[i].Id;
                ws.Cell(i + 2, 2).Value = usuarios[i].NombreUsuario;
                ws.Cell(i + 2, 3).Value = usuarios[i].Contrasena;
                ws.Cell(i + 2, 4).Value = usuarios[i].Activo;
                ws.Cell(i + 2, 5).Value = usuarios[i].Rol;
                ws.Cell(i + 2, 6).Value = usuarios[i].Empleado;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Usuarios.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var usuarios = IUsuariosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Usuarios")
                        .FontSize(20).Bold().AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("Id").Bold();
                            header.Cell().Text("NombreUsuario").Bold();
                            header.Cell().Text("Contrasena").Bold();
                            header.Cell().Text("Activo").Bold();
                            header.Cell().Text("Rol").Bold();
                            header.Cell().Text("Empleado").Bold();
                        });
                        // Datos
                        foreach (var usuario in usuarios)
                        {
                            table.Cell().Text(usuario.Id.ToString());
                            table.Cell().Text(usuario.NombreUsuario ?? "");
                            table.Cell().Text(usuario.Contrasena ?? "");
                            table.Cell().Text(usuario.Activo ? "Sí" : "No");
                            table.Cell().Text(usuario.Rol.ToString());
                            table.Cell().Text(usuario.Empleado.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Usuarios.pdf");
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
