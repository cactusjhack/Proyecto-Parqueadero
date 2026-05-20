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
    public class IngresosController : ControllerBase
    {
        private IIngresosNegocio? IIngresosNegocio;

        public IngresosController()
        {
            IIngresosNegocio = new IngresosNegocio();
        }

        [HttpGet]
        public List<Ingresos> Consultar()
        {
            if (IIngresosNegocio == null)
                throw new Exception("No implementado");
            return IIngresosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var ingresos = IIngresosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Ingresos");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "HoraEntrada";
            ws.Cell(1, 3).Value = "HoraSalida";
            ws.Cell(1, 4).Value = "TotalHoras";
            ws.Cell(1, 5).Value = "Vehiculo";
            ws.Cell(1, 6).Value = "Empleado";
            ws.Cell(1, 7).Value = "Espacios";

            // Datos
            for (int i = 0; i < ingresos.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = ingresos[i].Id;
                ws.Cell(i + 2, 2).Value = ingresos[i].HoraEntrada;
                ws.Cell(i + 2, 3).Value = ingresos[i].HoraSalida;
                ws.Cell(i + 2, 4).Value = ingresos[i].TotalHoras;
                ws.Cell(i + 2, 5).Value = ingresos[i].Vehiculo;
                ws.Cell(i + 2, 6).Value = ingresos[i].Empleado;
                ws.Cell(i + 2, 7).Value = ingresos[i].Espacio;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Ingresos.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var ingresos = IIngresosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Ingresos")
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
                            header.Cell().Text("HoraEntrada").Bold();
                            header.Cell().Text("HoraSalida").Bold();
                            header.Cell().Text("TotalHoras").Bold();
                            header.Cell().Text("Vehiculo").Bold();
                            header.Cell().Text("Empleado").Bold();
                            header.Cell().Text("Espacios").Bold();
                        });

                        // Datos
                        foreach (var ingreso in ingresos)
                        {
                            table.Cell().Text(ingreso.Id.ToString());
                            table.Cell().Text(ingreso.HoraEntrada.ToString("HH:mm"));
                            table.Cell().Text(ingreso.HoraSalida.ToString("HH:mm"));
                            table.Cell().Text(ingreso.TotalHoras.ToString("HH:mm"));
                            table.Cell().Text(ingreso.Vehiculo.ToString());
                            table.Cell().Text(ingreso.Empleado.ToString());
                            table.Cell().Text(ingreso.Espacio.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Ingresos.pdf");
        }

        [HttpPost]
        public Ingresos Guardar([FromBody] Ingresos entidad)
        {
            if (IIngresosNegocio == null)
                throw new Exception("No implementado");
            return IIngresosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Ingresos Actualizar([FromBody] Ingresos entidad)
        {
            if (IIngresosNegocio == null)
                throw new Exception("No implementado");
            return IIngresosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IIngresosNegocio == null)
                throw new Exception("No implementado");
            return IIngresosNegocio!.Eliminar(id);
        }
    }
}
