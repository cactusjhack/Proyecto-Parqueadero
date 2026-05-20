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
    public class TarifasController : ControllerBase
    {
        private ITarifasNegocio? ITarifasNegocio;

        public TarifasController()
        {
            ITarifasNegocio = new TarifasNegocio();
        }

        [HttpGet]
        public List<Tarifas> Consultar()
        {
            if (ITarifasNegocio == null)
                throw new Exception("No implementado");
            return ITarifasNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var tarifas = ITarifasNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Tarifas");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "TipoVehiculo";
            ws.Cell(1, 3).Value = "PrecioHora";
            ws.Cell(1, 4).Value = "FraccionHora";
            ws.Cell(1, 5).Value = "AplicaCargador";
            ws.Cell(1, 6).Value = "ValorCarga";

            // Datos
            for (int i = 0; i < tarifas.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = tarifas[i].Id;
                ws.Cell(i + 2, 2).Value =   tarifas[i].TipoVehiculo.ToString();
                ws.Cell(i + 2, 3).Value = tarifas[i].PrecioHora;
                ws.Cell(i + 2, 4).Value = tarifas[i].FraccionHora;
                ws.Cell(i + 2, 5).Value = tarifas[i].AplicaCargador;
                ws.Cell(i + 2, 6).Value = tarifas[i].ValorCarga;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Tarifas.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var tarifas = ITarifasNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Tarifas")
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
                            header.Cell().Text("TipoVehiculo").Bold();
                            header.Cell().Text("PrecioHora").Bold();
                            header.Cell().Text("FraccionHora").Bold();
                            header.Cell().Text("AplicaCargador").Bold();
                            header.Cell().Text("ValorCarga").Bold();
                        });

                        // Datos
                        foreach (var tarifa in tarifas)
                        {
                            table.Cell().Text(tarifa.Id.ToString());
                            table.Cell().Text(tarifa.TipoVehiculo.ToString());
                            table.Cell().Text(tarifa.PrecioHora.ToString());
                            table.Cell().Text(tarifa.FraccionHora.ToString());
                            table.Cell().Text(tarifa.AplicaCargador ? "Sí" : "No");
                            table.Cell().Text(tarifa.ValorCarga.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Tarifas.pdf");
        }

        [HttpPost]
        public Tarifas Guardar([FromBody] Tarifas entidad)
        {
            if (ITarifasNegocio == null)
                throw new Exception("No implementado");
            return ITarifasNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Tarifas Actualizar([FromBody] Tarifas entidad)
        {
            if (ITarifasNegocio == null)
                throw new Exception("No implementado");
            return ITarifasNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ITarifasNegocio == null)
                throw new Exception("No implementado");
            return ITarifasNegocio!.Eliminar(id);
        }
    }
}
