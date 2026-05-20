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
    public class PromocionesController : ControllerBase
    {
        private IPromocionesNegocio? IPromocionesNegocio;

        public PromocionesController()
        {
            IPromocionesNegocio = new PromocionesNegocio();
        }

        [HttpGet]
        public List<Promociones> Consultar()
        {
            if (IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return IPromocionesNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var promociones = IPromocionesNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Promociones");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Nombre";
            ws.Cell(1, 3).Value = "Descuento";
            ws.Cell(1, 4).Value = "FechaInicio";
            ws.Cell(1, 5).Value = "FechaFin";
            ws.Cell(1, 6).Value = "Activa";
            ws.Cell(1, 7).Value = "TipoVehiculo";
            ws.Cell(1, 8).Value = "SoloFinDeSemana";

            // Datos
            for (int i = 0; i < promociones.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = promociones[i].Id;
                ws.Cell(i + 2, 2).Value = promociones[i].Nombre;
                ws.Cell(i + 2, 3).Value = promociones[i].Descuento;
                ws.Cell(i + 2, 4).Value = promociones[i].FechaInicio;
                ws.Cell(i + 2, 5).Value = promociones[i].FechaFin;
                ws.Cell(i + 2, 6).Value = promociones[i].Activa;
                ws.Cell(i + 2, 7).Value = promociones[i].TipoVehiculo.ToString();
                ws.Cell(i + 2, 8).Value = promociones[i].SoloFinDeSemana;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Pomociones.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var promociones = IPromocionesNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Promociones")
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
                            c.RelativeColumn();
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("Id").Bold();
                            header.Cell().Text("Nombre").Bold();
                            header.Cell().Text("Descuento").Bold();
                            header.Cell().Text("FechaInicio").Bold();
                            header.Cell().Text("FechaFin").Bold();
                            header.Cell().Text("Activa").Bold();
                            header.Cell().Text("TipoVehiculo").Bold();
                            header.Cell().Text("SoloFinDeSemana").Bold();
                        });

                        // Datos
                        foreach (var promocion in promociones)
                        {
                            table.Cell().Text(promocion.Id.ToString());
                            table.Cell().Text(promocion.Nombre ?? "");
                            table.Cell().Text(promocion.Descuento.ToString());
                            table.Cell().Text(promocion.FechaInicio?.ToString("dd/MM/yyyy"));
                            table.Cell().Text(promocion.FechaFin?.ToString("dd/MM/yyyy"));
                            table.Cell().Text(promocion.Activa ? "Sí" : "No");
                            table.Cell().Text(promocion.TipoVehiculo.ToString());
                            table.Cell().Text(promocion.SoloFinDeSemana ? "Sí" : "No");
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Promociones.pdf");
        }

        [HttpPost]
        public Promociones Guardar([FromBody] Promociones entidad)
        {
            if (IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return IPromocionesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Promociones Actualizar([FromBody] Promociones entidad)
        {
            if (IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return IPromocionesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IPromocionesNegocio == null)
                throw new Exception("No implementado");
            return IPromocionesNegocio!.Eliminar(id);
        }
    }
}
