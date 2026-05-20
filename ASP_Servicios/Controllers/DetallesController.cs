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
    public class DetallesController : ControllerBase
    {
        private IDetallesNegocio? IDetallesNegocio;

        public DetallesController()
        {
            IDetallesNegocio = new DetallesNegocio();
        }

        [HttpGet]
        public List<Detalles> Consultar()
        {
            if (IDetallesNegocio == null)
                throw new Exception("No implementado");
            return IDetallesNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var detalles = IDetallesNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Detalles");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Ingreso";
            ws.Cell(1, 3).Value = "Descripcion";
            ws.Cell(1, 4).Value = "Fecha";
            ws.Cell(1, 5).Value = "Vehiculo";
            ws.Cell(1, 6).Value = "Empleado";

            // Datos
            for (int i = 0; i < detalles.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = detalles[i].Id;
                ws.Cell(i + 2, 2).Value = detalles[i].Ingreso;
                ws.Cell(i + 2, 3).Value = detalles[i].Descripcion;
                ws.Cell(i + 2, 4).Value = detalles[i].Fecha;
                ws.Cell(i + 2, 5).Value = detalles[i].Vehiculo;
                ws.Cell(i + 2, 6).Value = detalles[i].Empleado;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Detalles.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var detalles = IDetallesNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Detalles")
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
                            header.Cell().Text("Ingreso").Bold();
                            header.Cell().Text("Descripcion").Bold();
                            header.Cell().Text("Fecha").Bold();
                            header.Cell().Text("Vehiculo").Bold();
                            header.Cell().Text("Empleado").Bold();
                        });

                        // Datos
                        foreach (var detalle in detalles)
                        {
                            table.Cell().Text(detalle.Id.ToString());
                            table.Cell().Text(detalle.Ingreso.ToString());
                            table.Cell().Text(detalle.Descripcion ?? "");
                            table.Cell().Text(detalle.Fecha.ToString("dd/MM/yyyy HH:mm"));
                            table.Cell().Text(detalle.Vehiculo.ToString());
                            table.Cell().Text(detalle.Empleado.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Detalles.pdf");
        }

        [HttpPost]
        public Detalles Guardar([FromBody] Detalles entidad)
        {
            if (IDetallesNegocio == null)
                throw new Exception("No implementado");
            return IDetallesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Detalles Actualizar([FromBody] Detalles entidad)
        {
            if (IDetallesNegocio == null)
                throw new Exception("No implementado");
            return IDetallesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IDetallesNegocio == null)
                throw new Exception("No implementado");
            return IDetallesNegocio!.Eliminar(id);
        }
    }
}
