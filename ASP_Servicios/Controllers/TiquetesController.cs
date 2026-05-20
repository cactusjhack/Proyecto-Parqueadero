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
    public class TiquetesController : ControllerBase
    {
        private ITiquetesNegocio? ITiquetesNegocio;

        public TiquetesController()
        {
            ITiquetesNegocio = new TiquetesNegocio();
        }

        [HttpGet]
        public List<Tiquetes> Consultar()
        {
            if (ITiquetesNegocio == null)
                throw new Exception("No implementado");
            return ITiquetesNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var tiquetes = ITiquetesNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Tiquetes");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Codigo";
            ws.Cell(1, 3).Value = "FechaGeneracion";
            ws.Cell(1, 4).Value = "Pagado";
            ws.Cell(1, 5).Value = "FechaVencimiento";
            ws.Cell(1, 6).Value = "Ingreso";

            // Datos
            for (int i = 0; i < tiquetes.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = tiquetes[i].Id;
                ws.Cell(i + 2, 2).Value = tiquetes[i].Codigo;
                ws.Cell(i + 2, 3).Value = tiquetes[i].FechaGeneracion;
                ws.Cell(i + 2, 4).Value = tiquetes[i].Pagado;
                ws.Cell(i + 2, 5).Value = tiquetes[i].FechaVencimiento;
                ws.Cell(i + 2, 6).Value = tiquetes[i].Ingreso;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Tiquetes.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var tiquetes = ITiquetesNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Tiquetes")
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
                            header.Cell().Text("Codigo").Bold();
                            header.Cell().Text("FechaGeneracion").Bold();
                            header.Cell().Text("Pagado").Bold();
                            header.Cell().Text("FechaVencimiento").Bold();
                            header.Cell().Text("Ingreso").Bold();
                        });

                        // Datos
                        foreach (var tiquete in tiquetes)
                        {
                            table.Cell().Text(tiquete.Id.ToString());
                            table.Cell().Text(tiquete.Codigo ?? "");
                            table.Cell().Text(tiquete.FechaGeneracion.ToString());
                            table.Cell().Text(tiquete.Pagado ? "Sí" : "No");
                            table.Cell().Text(tiquete.FechaVencimiento.ToString());
                            table.Cell().Text(tiquete.Ingreso.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Tiquetes.pdf");
        }

        [HttpPost]
        public Tiquetes Guardar([FromBody] Tiquetes entidad)
        {
            if (ITiquetesNegocio == null)
                throw new Exception("No implementado");
            return ITiquetesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Tiquetes Actualizar([FromBody] Tiquetes entidad)
        {
            if (ITiquetesNegocio == null)
                throw new Exception("No implementado");
            return ITiquetesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ITiquetesNegocio == null)
                throw new Exception("No implementado");
            return ITiquetesNegocio!.Eliminar(id);
        }
    }
}
