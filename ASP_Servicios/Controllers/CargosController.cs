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
    public class CargosController : ControllerBase
    {
        private ICargosNegocio? ICargosNegocio;

        public CargosController()
        {
            ICargosNegocio = new CargosNegocio();
        }

        [HttpGet]
        public List<Cargos> Consultar()
        {
            if (ICargosNegocio == null)
                throw new Exception("No implementado");
            return ICargosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var cargos = ICargosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Cargos");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Nombre";
            ws.Cell(1, 3).Value = "Descripcion";
            ws.Cell(1, 4).Value = "AccesoVehiculos";
            ws.Cell(1, 5).Value = "AccesoCaja";
            ws.Cell(1, 5).Value = "AccesoSistema";

            // Datos
            for (int i = 0; i < cargos.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = cargos[i].Id;
                ws.Cell(i + 2, 2).Value = cargos[i].Nombre;
                ws.Cell(i + 2, 3).Value = cargos[i].Descripcion;
                ws.Cell(i + 2, 4).Value = cargos[i].AccesoVehiculos;
                ws.Cell(i + 2, 5).Value = cargos[i].AccesoCaja;
                ws.Cell(i + 2, 6).Value = cargos[i].AccesoSistema;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Cargos.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var cargos = ICargosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Cargos")
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
                            header.Cell().Text("Nombre").Bold();
                            header.Cell().Text("Descripcion").Bold();
                            header.Cell().Text("AccesoVehiculos").Bold();
                            header.Cell().Text("AccesoCaja").Bold();
                            header.Cell().Text("AccesoSistema").Bold();
                        });

                        // Datos
                        foreach (var cargo in cargos)
                        {
                            table.Cell().Text(cargo.Id.ToString());
                            table.Cell().Text(cargo.Nombre ?? "");
                            table.Cell().Text(cargo.Descripcion ?? "");
                            table.Cell().Text(cargo.AccesoVehiculos ? "Sí" : "No");
                            table.Cell().Text(cargo.AccesoCaja ? "Sí" : "No");
                            table.Cell().Text(cargo.AccesoSistema ? "Sí" : "No");
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Cargos.pdf");
        }

        [HttpPost]
        public Cargos Guardar([FromBody] Cargos entidad)
        {
            if (ICargosNegocio == null)
                throw new Exception("No implementado");
            return ICargosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Cargos Actualizar([FromBody] Cargos entidad)
        {
            if (ICargosNegocio == null)
                throw new Exception("No implementado");
            return ICargosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ICargosNegocio == null)
                throw new Exception("No implementado");
            return ICargosNegocio!.Eliminar(id);
        }
    }
}
