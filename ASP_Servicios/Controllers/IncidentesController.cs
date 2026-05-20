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
    public class IncidentesController : ControllerBase
    {
        private IIncidentesNegocio? IIncidentesNegocio;

        public IncidentesController()
        {
            IIncidentesNegocio = new IncidentesNegocio();
        }

        [HttpGet]
        public List<Incidentes> Consultar()
        {
            if (IIncidentesNegocio == null)
                throw new Exception("No implementado");
            return IIncidentesNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var incidentes = IIncidentesNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Incidentes");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Descripcion";
            ws.Cell(1, 3).Value = "Fecha";
            ws.Cell(1, 4).Value = "Tipo";
            ws.Cell(1, 5).Value = "Resuelto";
            ws.Cell(1, 6).Value = "Espacio";
            ws.Cell(1, 7).Value = "Empleado";

            // Datos
            for (int i = 0; i < incidentes.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = incidentes[i].Id;
                ws.Cell(i + 2, 2).Value = incidentes[i].Descripcion;
                ws.Cell(i + 2, 3).Value = incidentes[i].Fecha;
                ws.Cell(i + 2, 4).Value = incidentes[i].Tipo;
                ws.Cell(i + 2, 5).Value = incidentes[i].Resuelto;
                ws.Cell(i + 2, 6).Value = incidentes[i].Espacio;
                ws.Cell(i + 2, 7).Value = incidentes[i].Empleado;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Incidentes.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var incidentes = IIncidentesNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Incidentes")
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
                            c.RelativeColumn();
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("Id").Bold();
                            header.Cell().Text("Descripcion").Bold();
                            header.Cell().Text("Fecha").Bold();
                            header.Cell().Text("Tipo").Bold();
                            header.Cell().Text("Resuelto").Bold();
                            header.Cell().Text("Espacio").Bold();
                            header.Cell().Text("Empleado").Bold();
                        });

                        // Datos
                        foreach (var incidente in incidentes)
                        {
                            table.Cell().Text(incidente.Id.ToString());
                            table.Cell().Text(incidente.Descripcion ?? "");
                            table.Cell().Text(incidente.Fecha.ToString("dd/MM/yyyy HH:mm"));
                            table.Cell().Text(incidente.Tipo ?? "");
                            table.Cell().Text(incidente.Resuelto ? "Sí" : "No");
                            table.Cell().Text(incidente.Espacio.ToString());
                            table.Cell().Text(incidente.Empleado.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Incidentes.pdf");
        }

        [HttpPost]
        public Incidentes Guardar([FromBody] Incidentes entidad)
        {
            if (IIncidentesNegocio == null)
                throw new Exception("No implementado");
            return IIncidentesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Incidentes Actualizar([FromBody] Incidentes entidad)
        {
            if (IIncidentesNegocio == null)
                throw new Exception("No implementado");
            return IIncidentesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IIncidentesNegocio == null)
                throw new Exception("No implementado");
            return IIncidentesNegocio!.Eliminar(id);
        }
    }
}
