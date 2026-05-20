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
    public class CobrosController : ControllerBase
    {
        private ICobrosNegocio? ICobrosNegocio;

        public CobrosController()
        {
            ICobrosNegocio = new CobrosNegocio();
        }

        [HttpGet]
        public List<Cobros> Consultar()
        {
            if (ICobrosNegocio == null)
                throw new Exception("No implementado");
            return ICobrosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var cobros = ICobrosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Cobros");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Subtotal";
            ws.Cell(1, 3).Value = "Descuento";
            ws.Cell(1, 4).Value = "Total";
            ws.Cell(1, 5).Value = "Ingreso";
            ws.Cell(1, 6).Value = "UsoValet";
            ws.Cell(1, 7).Value = "TarifaValet";
            ws.Cell(1, 8).Value = "Cliente";
            ws.Cell(1, 9).Value = "Tarifa";
            ws.Cell(1, 10).Value = "Promocion";

            // Datos
            for (int i = 0; i < cobros.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = cobros[i].Id;
                ws.Cell(i + 2, 2).Value = cobros[i].Subtotal;
                ws.Cell(i + 2, 3).Value = cobros[i].Descuento;
                ws.Cell(i + 2, 4).Value = cobros[i].Total;
                ws.Cell(i + 2, 5).Value = cobros[i].Ingreso;
                ws.Cell(i + 2, 6).Value = cobros[i].UsoValet;
                ws.Cell(i + 2, 7).Value = cobros[i].TarifaValet;
                ws.Cell(i + 2, 8).Value = cobros[i].Cliente;
                ws.Cell(i + 2, 9).Value = cobros[i].Tarifa;
                ws.Cell(i + 2, 10).Value = cobros[i].Promocion;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Cobros.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var cobros = ICobrosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Cobros")
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
                            c.RelativeColumn();
                            c.RelativeColumn();
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("Id").Bold();
                            header.Cell().Text("Subtotal").Bold();
                            header.Cell().Text("Descuento").Bold();
                            header.Cell().Text("Total").Bold();
                            header.Cell().Text("Ingreso").Bold();
                            header.Cell().Text("UsoValet").Bold();
                            header.Cell().Text("TarifaValet").Bold();
                            header.Cell().Text("Cliente").Bold();
                            header.Cell().Text("Tarifa").Bold();
                            header.Cell().Text("Promocion").Bold();
                        });

                        // Datos
                        foreach (var cobro in cobros)
                        {
                            table.Cell().Text(cobro.Id.ToString());
                            table.Cell().Text(cobro.Subtotal.ToString());
                            table.Cell().Text(cobro.Descuento.ToString());
                            table.Cell().Text(cobro.Total.ToString());
                            table.Cell().Text(cobro.Ingreso.ToString());
                            table.Cell().Text(cobro.UsoValet ? "Sí" : "No");
                            table.Cell().Text(cobro.TarifaValet.ToString());
                            table.Cell().Text(cobro.Cliente.ToString());
                            table.Cell().Text(cobro.Tarifa.ToString());
                            table.Cell().Text(cobro.Promocion.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Cobros.pdf");
        }

        [HttpPost]
        public Cobros Guardar([FromBody] Cobros entidad)
        {
            if (ICobrosNegocio == null)
                throw new Exception("No implementado");
            return ICobrosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Cobros Actualizar([FromBody] Cobros entidad)
        {
            if (ICobrosNegocio == null)
                throw new Exception("No implementado");
            return ICobrosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ICobrosNegocio == null)
                throw new Exception("No implementado");
            return ICobrosNegocio!.Eliminar(id);
        }
    }
}
