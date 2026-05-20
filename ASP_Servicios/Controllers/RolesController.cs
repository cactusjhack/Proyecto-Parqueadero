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

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var roles = IRolesNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Roles");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Nombre";
            ws.Cell(1, 3).Value = "Descripcion";

            // Datos
            for (int i = 0; i < roles.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = roles[i].Id;
                ws.Cell(i + 2, 2).Value = roles[i].Nombre;
                ws.Cell(i + 2, 3).Value = roles[i].Descripcion;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Roles.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var roles = IRolesNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Roles")
                        .FontSize(20).Bold().AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("Id").Bold();
                            header.Cell().Text("Nombre").Bold();
                            header.Cell().Text("Descripcion").Bold();
                        });

                        // Datos
                        foreach (var rol in roles)
                        {
                            table.Cell().Text(rol.Id.ToString());
                            table.Cell().Text(rol.Nombre ?? "");
                            table.Cell().Text(rol.Descripcion ?? "");
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Roles.pdf");
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
