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
    public class VehiculosController : ControllerBase
    {
        private IVehiculosNegocio? IVehiculosNegocio;

        public VehiculosController()
        {
            IVehiculosNegocio = new VehiculosNegocio();
        }

        [HttpGet]
        public List<Vehiculos> Consultar()
        {
            if (IVehiculosNegocio == null)
                throw new Exception("No implementado");
            return IVehiculosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var vehiculos = IVehiculosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Vehiculos");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Tipo";
            ws.Cell(1, 3).Value = "Placa";
            ws.Cell(1, 4).Value = "Marca";
            ws.Cell(1, 5).Value = "Color";
            ws.Cell(1, 6).Value = "Combustion";
            ws.Cell(1, 7).Value = "Cliente";

            // Datos
            for (int i = 0; i < vehiculos.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = vehiculos[i].Id;
                ws.Cell(i + 2, 2).Value = vehiculos[i].Tipo.ToString();
                ws.Cell(i + 2, 3).Value = vehiculos[i].Placa;
                ws.Cell(i + 2, 4).Value = vehiculos[i].Marca;
                ws.Cell(i + 2, 5).Value = vehiculos[i].Color;
                ws.Cell(i + 2, 6).Value = vehiculos[i].Combustion.ToString();
                ws.Cell(i + 2, 7).Value = vehiculos[i].Cliente;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Vehiculos.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var vehiculos = IVehiculosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Vehiculos")
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
                            header.Cell().Text("Tipo").Bold();
                            header.Cell().Text("Placa").Bold();
                            header.Cell().Text("Marca").Bold();
                            header.Cell().Text("Color").Bold();
                            header.Cell().Text("Combustion").Bold();
                            header.Cell().Text("Cliente").Bold();
                        });

                        // Datos
                        foreach (var vehiculo in vehiculos)
                        {
                            table.Cell().Text(vehiculo.Id.ToString());
                            table.Cell().Text(vehiculo.Tipo.ToString());
                            table.Cell().Text(vehiculo.Placa ?? "");
                            table.Cell().Text(vehiculo.Marca ?? "");
                            table.Cell().Text(vehiculo.Color ?? "");
                            table.Cell().Text(vehiculo.Combustion.ToString());
                            table.Cell().Text(vehiculo.Cliente.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Vehiculos.pdf");
        }

        [HttpPost]
        public Vehiculos Guardar([FromBody] Vehiculos entidad)
        {
            if (IVehiculosNegocio == null)
                throw new Exception("No implementado");
            return IVehiculosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Vehiculos Actualizar([FromBody] Vehiculos entidad)
        {
            if (IVehiculosNegocio == null)
                throw new Exception("No implementado");
            return IVehiculosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IVehiculosNegocio == null)
                throw new Exception("No implementado");
            return IVehiculosNegocio!.Eliminar(id);
        }
    }
}
