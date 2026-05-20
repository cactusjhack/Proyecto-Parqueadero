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
    public class FichosController : ControllerBase
    {
        private IFichosNegocio? IFichosNegocio;

        public FichosController()
        {
            IFichosNegocio = new FichosNegocio();
        }

        [HttpGet]
        public List<Fichos> Consultar()
        {
            if (IFichosNegocio == null)
                throw new Exception("No implementado");
            return IFichosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var fichos = IFichosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Fichos");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Fecha";
            ws.Cell(1, 3).Value = "Codigo";
            ws.Cell(1, 4).Value = "Entregado";
            ws.Cell(1, 5).Value = "Cliente";

            // Datos
            for (int i = 0; i < fichos.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = fichos[i].Id;
                ws.Cell(i + 2, 2).Value = fichos[i].Fecha;
                ws.Cell(i + 2, 3).Value = fichos[i].Codigo;
                ws.Cell(i + 2, 4).Value = fichos[i].Entregado;
                ws.Cell(i + 2, 5).Value = fichos[i].Cliente;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Fichos.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var fichos = IFichosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Fichos")
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
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("Id").Bold();
                            header.Cell().Text("Fecha").Bold();
                            header.Cell().Text("Codigo").Bold();
                            header.Cell().Text("Entregado").Bold();
                            header.Cell().Text("Cliente").Bold();
                        });

                        // Datos
                        foreach (var ficho in fichos)
                        {
                            table.Cell().Text(ficho.Id.ToString());
                            table.Cell().Text(ficho.Fecha.ToString("dd/MM/yyyy HH:mm"));
                            table.Cell().Text(ficho.Codigo ?? "");
                            table.Cell().Text(ficho.Entregado ? "Sí" : "No");
                            table.Cell().Text(ficho.Cliente.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Fichos.pdf");
        }

        [HttpPost]
        public Fichos Guardar([FromBody] Fichos entidad)
        {
            if (IFichosNegocio == null)
                throw new Exception("No implementado");
            return IFichosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Fichos Actualizar([FromBody] Fichos entidad)
        {
            if (IFichosNegocio == null)
                throw new Exception("No implementado");
            return IFichosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IFichosNegocio == null)
                throw new Exception("No implementado");
            return IFichosNegocio!.Eliminar(id);
        }
    }
}
