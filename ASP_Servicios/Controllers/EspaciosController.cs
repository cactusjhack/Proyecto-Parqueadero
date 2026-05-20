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
    public class EspaciosController : ControllerBase
    {
        private IEspaciosNegocio? IEspaciosNegocio;

        public EspaciosController()
        {
            IEspaciosNegocio = new EspaciosNegocio();
        }

        [HttpGet]
        public List<Espacios> Consultar()
        {
            if (IEspaciosNegocio == null)
                throw new Exception("No implementado");
            return IEspaciosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var espacios = IEspaciosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Espacios");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Numero";
            ws.Cell(1, 3).Value = "TipoVehiculo";
            ws.Cell(1, 4).Value = "PuestoCarga";
            ws.Cell(1, 5).Value = "Disponible";
            ws.Cell(1, 6).Value = "Piso";

            // Datos
            for (int i = 0; i < espacios.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = espacios[i].Id;
                ws.Cell(i + 2, 2).Value = espacios[i].Numero;
                ws.Cell(i + 2, 3).Value = espacios[i].TipoVehiculo.ToString();
                ws.Cell(i + 2, 4).Value = espacios[i].PuestoCarga;
                ws.Cell(i + 2, 5).Value = espacios[i].Disponible;
                ws.Cell(i + 2, 6).Value = espacios[i].Piso;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Espacios.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var espacios = IEspaciosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Espacios")
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
                            header.Cell().Text("Numero").Bold();
                            header.Cell().Text("TipoVehiculo").Bold();
                            header.Cell().Text("PuestoCarga").Bold();
                            header.Cell().Text("Disponible").Bold();
                            header.Cell().Text("Piso").Bold();
                        });

                        // Datos
                        foreach (var espacio in espacios)
                        {
                            table.Cell().Text(espacio.Id.ToString());
                            table.Cell().Text(espacio.Numero ?? "");
                            table.Cell().Text(espacio.TipoVehiculo.ToString());
                            table.Cell().Text(espacio.PuestoCarga ? "Si" : "No");
                            table.Cell().Text(espacio.Disponible ? "Sí" : "No");
                            table.Cell().Text(espacio.Piso.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Espacios.pdf");
        }

        [HttpPost]
        public Espacios Guardar([FromBody] Espacios entidad)
        {
            if (IEspaciosNegocio == null)
                throw new Exception("No implementado");
            return IEspaciosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Espacios Actualizar([FromBody] Espacios entidad)
        {
            if (IEspaciosNegocio == null)
                throw new Exception("No implementado");
            return IEspaciosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IEspaciosNegocio == null)
                throw new Exception("No implementado");
            return IEspaciosNegocio!.Eliminar(id);
        }
    }
}
