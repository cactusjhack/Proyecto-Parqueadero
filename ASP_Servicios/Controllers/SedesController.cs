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

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var sedes = ISedesNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Sedes");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Nombre";
            ws.Cell(1, 3).Value = "Direccion";
            ws.Cell(1, 4).Value = "Ciudad";
            ws.Cell(1, 5).Value = "Telefono";
            ws.Cell(1, 6).Value = "Activa";

            // Datos
            for (int i = 0; i < sedes.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = sedes[i].Id;
                ws.Cell(i + 2, 2).Value = sedes[i].Nombre;
                ws.Cell(i + 2, 3).Value = sedes[i].Direccion;
                ws.Cell(i + 2, 4).Value = sedes[i].Ciudad;
                ws.Cell(i + 2, 5).Value = sedes[i].Telefono;
                ws.Cell(i + 2, 6).Value = sedes[i].Activa;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Sedes.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var sedes = ISedesNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Sedes")
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
                            header.Cell().Text("Nombre").Bold();
                            header.Cell().Text("Direccion").Bold();
                            header.Cell().Text("Ciudad").Bold();
                            header.Cell().Text("Teléfono").Bold();
                            header.Cell().Text("Activa").Bold();
                        });

                        // Datos
                        foreach (var sede in sedes)
                        {
                            table.Cell().Text(sede.Id.ToString());
                            table.Cell().Text(sede.Nombre ?? "");
                            table.Cell().Text(sede.Direccion ?? "");
                            table.Cell().Text(sede.Ciudad ?? "");
                            table.Cell().Text(sede.Telefono ?? "");
                            table.Cell().Text(sede.Activa ? "Sí" : "No");
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Sedes.pdf");
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
