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
    public class NotificacionesController : ControllerBase
    {
        private INotificacionesNegocio? INotificacionesNegocio;

        public NotificacionesController()
        {
            INotificacionesNegocio = new NotificacionesNegocio();
        }

        [HttpGet]
        public List<Notificaciones> Consultar()
        {
            if (INotificacionesNegocio == null)
                throw new Exception("No implementado");
            return INotificacionesNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var notificaciones = INotificacionesNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Notificaciones");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Mensaje";
            ws.Cell(1, 3).Value = "Leida";
            ws.Cell(1, 4).Value = "Canal";
            ws.Cell(1, 5).Value = "Fecha";
            ws.Cell(1, 6).Value = "Cliente";

            // Datos
            for (int i = 0; i < notificaciones.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = notificaciones[i].Id;
                ws.Cell(i + 2, 2).Value = notificaciones[i].Mensaje;
                ws.Cell(i + 2, 3).Value = notificaciones[i].Leida;
                ws.Cell(i + 2, 4).Value = notificaciones[i].Canal;
                ws.Cell(i + 2, 5).Value = notificaciones[i].Fecha;
                ws.Cell(i + 2, 6).Value = notificaciones[i].Cliente;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Notificaciones.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var notificaciones = INotificacionesNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Notificaciones")
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
                            header.Cell().Text("Mensaje").Bold();
                            header.Cell().Text("Leida").Bold();
                            header.Cell().Text("Canal").Bold();
                            header.Cell().Text("Fecha").Bold();
                            header.Cell().Text("Cliente").Bold();
                        });

                        // Datos
                        foreach (var notificacion in notificaciones)
                        {
                            table.Cell().Text(notificacion.Id.ToString());
                            table.Cell().Text(notificacion.Mensaje ?? "");
                            table.Cell().Text(notificacion.Leida ? "Sí" : "No");
                            table.Cell().Text(notificacion.Canal ?? "");
                            table.Cell().Text(notificacion.Fecha.ToString("dd/MM/yyyy HH:mm"));
                            table.Cell().Text(notificacion.Cliente.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Notificaciones.pdf");
        }

        [HttpPost]
        public Notificaciones Guardar([FromBody] Notificaciones entidad)
        {
            if (INotificacionesNegocio == null)
                throw new Exception("No implementado");
            return INotificacionesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Notificaciones Actualizar([FromBody] Notificaciones entidad)
        {
            if (INotificacionesNegocio == null)
                throw new Exception("No implementado");
            return INotificacionesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (INotificacionesNegocio == null)
                throw new Exception("No implementado");
            return INotificacionesNegocio!.Eliminar(id);
        }
    }
}
