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
    public class CamarasController : ControllerBase
    {
        private ICamarasNegocio? ICamarasNegocio;

        public CamarasController()
        {
            ICamarasNegocio = new CamarasNegocio();
        }

        [HttpGet]
        public List<Camaras> Consultar()
        {
            if (ICamarasNegocio == null)
                throw new Exception("No implementado");
            return ICamarasNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var camaras = ICamarasNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Camaras");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = " Codigo";
            ws.Cell(1, 3).Value = "Ubicacion";
            ws.Cell(1, 4).Value = "Activa";
            ws.Cell(1, 5).Value = "Piso";

            // Datos
            for (int i = 0; i < camaras.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = camaras[i].Id;
                ws.Cell(i + 2, 2).Value = camaras[i].Codigo;
                ws.Cell(i + 2, 3).Value = camaras[i].Ubicacion;
                ws.Cell(i + 2, 4).Value = camaras[i].Activa;
                ws.Cell(i + 2, 5).Value = camaras[i].Piso;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Camaras.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var camaras = ICamarasNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Cámaras")
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
                            header.Cell().Text("Codigo").Bold();
                            header.Cell().Text("Ubicacion").Bold();
                            header.Cell().Text("Activa").Bold();
                            header.Cell().Text("Piso").Bold();
                        });

                        // Datos
                        foreach (var camara in camaras)
                        {
                            table.Cell().Text(camara.Id.ToString());
                            table.Cell().Text(camara.Codigo ?? "");
                            table.Cell().Text(camara.Ubicacion ?? "");
                            table.Cell().Text(camara.Activa ? "Sí" : "No");
                            table.Cell().Text(camara.Piso.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Camaras.pdf");
        }

        [HttpPost]
        public Camaras Guardar([FromBody] Camaras entidad)
        {
            if (ICamarasNegocio == null)
                throw new Exception("No implementado");
            return ICamarasNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Camaras Actualizar([FromBody] Camaras entidad)
        {
            if (ICamarasNegocio == null)
                throw new Exception("No implementado");
            return ICamarasNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ICamarasNegocio == null)
                throw new Exception("No implementado");
            return ICamarasNegocio!.Eliminar(id);
        }
    }
}
