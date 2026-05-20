using ClosedXML.Excel;
using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent; 
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditoriasController : ControllerBase
    {
        private IAuditoriasNegocio? IAuditoriasNegocio;

        public AuditoriasController()
        {
            IAuditoriasNegocio = new AuditoriasNegocio();
        }

        [HttpGet]
        public List<Auditorias> Consultar()
        {
            if (IAuditoriasNegocio == null)
                throw new Exception("No implementado");
            return IAuditoriasNegocio!.Consultar();
        }
        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var auditorias = IAuditoriasNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Auditorias");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Tabla";
            ws.Cell(1, 3).Value = "Accion";
            ws.Cell(1, 4).Value = "Fecha";

            // Datos
            for (int i = 0; i < auditorias.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = auditorias[i].Id;
                ws.Cell(i + 2, 2).Value = auditorias[i].Tabla;
                ws.Cell(i + 2, 3).Value = auditorias[i].Accion;
                ws.Cell(i + 2, 4).Value = auditorias[i].Fecha;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Auditorias.xlsx");
        }
        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var auditorias = IAuditoriasNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Auditorias")
                        .FontSize(20).Bold().AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                            c.RelativeColumn();
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("Id").Bold();
                            header.Cell().Text("Tabla").Bold();
                            header.Cell().Text("Accion").Bold();
                            header.Cell().Text("Fecha").Bold();
                        });

                        // Datos
                        foreach (var auditoria in auditorias)
                        {
                            table.Cell().Text(auditoria.Id.ToString());
                            table.Cell().Text(auditoria.Tabla ?? "");
                            table.Cell().Text(auditoria.Accion ?? "");
                            table.Cell().Text(auditoria.Fecha.ToString("dd/MM/yyyy HH:mm"));
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Auditorias.pdf");
        }
    }
}
