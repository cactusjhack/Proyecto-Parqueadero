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
    public class ConveniosController : ControllerBase
    {
        private IConveniosNegocio? IConveniosNegocio;

        public ConveniosController()
        {
            IConveniosNegocio = new ConveniosNegocio();
        }

        [HttpGet]
        public List<Convenios> Consultar()
        {
            if (IConveniosNegocio == null)
                throw new Exception("No implementado");
            return IConveniosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var convenios = IConveniosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Convenios");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Empresa";
            ws.Cell(1, 3).Value = "Descuento";
            ws.Cell(1, 4).Value = "FechaInicio";
            ws.Cell(1, 5).Value = "FechaFin";
            ws.Cell(1, 6).Value = "Activo";
            ws.Cell(1, 7).Value = "Sede";

            // Datos
            for (int i = 0; i < convenios.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = convenios[i].Id;
                ws.Cell(i + 2, 2).Value = convenios[i].Empresa;
                ws.Cell(i + 2, 3).Value = convenios[i].Descuento;
                ws.Cell(i + 2, 4).Value = convenios[i].FechaInicio;
                ws.Cell(i + 2, 5).Value = convenios[i].FechaFin;
                ws.Cell(i + 2, 6).Value = convenios[i].Activo;
                ws.Cell(i + 2, 7).Value = convenios[i].Sede;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Convenios.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var convenios = IConveniosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Convenios")
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
                            header.Cell().Text("Empresa").Bold();
                            header.Cell().Text("Descuento").Bold();
                            header.Cell().Text("FechaInicio").Bold();
                            header.Cell().Text("FechaFin").Bold();
                            header.Cell().Text("Activo").Bold();
                            header.Cell().Text("Sede").Bold();
                        });

                        // Datos
                        foreach (var convenio in convenios)
                        {
                            table.Cell().Text(convenio.Id.ToString());
                            table.Cell().Text(convenio.Empresa ?? "");
                            table.Cell().Text(convenio.Descuento.ToString());
                            table.Cell().Text(convenio.FechaInicio.ToString("dd/MM/yyyy"));
                            table.Cell().Text(convenio.FechaFin.ToString("dd/MM/yyyy"));
                            table.Cell().Text(convenio.Activo ? "Sí" : "No");
                            table.Cell().Text(convenio.Sede.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Convenios.pdf");
        }

        [HttpPost]
        public Convenios Guardar([FromBody] Convenios entidad)
        {
            if (IConveniosNegocio == null)
                throw new Exception("No implementado");
            return IConveniosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Convenios Actualizar([FromBody] Convenios entidad)
        {
            if (IConveniosNegocio == null)
                throw new Exception("No implementado");
            return IConveniosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IConveniosNegocio == null)
                throw new Exception("No implementado");
            return IConveniosNegocio!.Eliminar(id);
        }
    }
}
