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
    public class PagosController : ControllerBase
    {
        private IPagosNegocio? IPagosNegocio;

        public PagosController()
        {
            IPagosNegocio = new PagosNegocio();
        }

        [HttpGet]
        public List<Pagos> Consultar()
        {
            if (IPagosNegocio == null)
                throw new Exception("No implementado");
            return IPagosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var pagos = IPagosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Pagos");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Cobro";
            ws.Cell(1, 3).Value = "MetodoPago";
            ws.Cell(1, 4).Value = "Valor";
            ws.Cell(1, 5).Value = "Fecha";
            ws.Cell(1, 6).Value = "Aprobado";

            // Datos
            for (int i = 0; i < pagos.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = pagos[i].Id;
                ws.Cell(i + 2, 2).Value = pagos[i].Cobro;
                ws.Cell(i + 2, 3).Value = pagos[i].MetodoPago.ToString();
                ws.Cell(i + 2, 4).Value = pagos[i].Valor;
                ws.Cell(i + 2, 5).Value = pagos[i].Fecha;
                ws.Cell(i + 2, 6).Value = pagos[i].Aprobado;
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Pagos.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var pagos = IPagosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Pagos")
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
                            header.Cell().Text("Cobro").Bold();
                            header.Cell().Text("MetodoPago").Bold();
                            header.Cell().Text("Valor").Bold();
                            header.Cell().Text("Fecha").Bold();
                            header.Cell().Text("Aprobado").Bold();
                        });

                        // Datos
                        foreach (var pago in pagos)
                        {
                            table.Cell().Text(pago.Id.ToString());
                            table.Cell().Text(pago.Cobro.ToString());
                            table.Cell().Text(pago.MetodoPago.ToString());
                            table.Cell().Text(pago.Valor.ToString());
                            table.Cell().Text(pago.Fecha.ToString("dd/MM/yyyy HH:mm"));
                            table.Cell().Text(pago.Aprobado ? "Sí" : "No");
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Pagos.pdf");
        }

        [HttpPost]
        public Pagos Guardar([FromBody] Pagos entidad)
        {
            if (IPagosNegocio == null)
                throw new Exception("No implementado");
            return IPagosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Pagos Actualizar([FromBody] Pagos entidad)
        {
            if (IPagosNegocio == null)
                throw new Exception("No implementado");
            return IPagosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IPagosNegocio == null)
                throw new Exception("No implementado");
            return IPagosNegocio!.Eliminar(id);
        }
    }
}
