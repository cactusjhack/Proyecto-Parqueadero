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
    public class MantenimientosController : ControllerBase
    {
        private IMantenimientosNegocio? IMantenimientosNegocio;

        public MantenimientosController()
        {
            IMantenimientosNegocio = new MantenimientosNegocio();
        }

        [HttpGet]
        public List<Mantenimientos> Consultar()
        {
            if (IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return IMantenimientosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var mantenimientos = IMantenimientosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Mantenimientos");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Descripcion";
            ws.Cell(1, 3).Value = "FechaInicio";
            ws.Cell(1, 4).Value = "FechaFin";
            ws.Cell(1, 5).Value = "Activo";
            ws.Cell(1, 6).Value = "Espacios";
            ws.Cell(1, 7).Value = "Empleado";

            // Datos
            for (int i = 0; i < mantenimientos.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = mantenimientos[i].Id;
                ws.Cell(i + 2, 2).Value = mantenimientos[i].Descripcion;
                ws.Cell(i + 2, 3).Value = mantenimientos[i].FechaInicio;
                ws.Cell(i + 2, 4).Value = mantenimientos[i].FechaFin;
                ws.Cell(i + 2, 5).Value = mantenimientos[i].Activo;
                ws.Cell(i + 2, 6).Value = mantenimientos[i].Espacio;
                ws.Cell(i + 2, 7).Value = mantenimientos[i].Empleado;
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Mantenimientos.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var mantenimientos = IMantenimientosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Mantenimientos")
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
                            header.Cell().Text("Descripcion").Bold();
                            header.Cell().Text("FechaInicio").Bold();
                            header.Cell().Text("FechaFin").Bold();
                            header.Cell().Text("Activo").Bold();
                            header.Cell().Text("Espacios").Bold();
                            header.Cell().Text("Empleado").Bold();
                        });
                        // Datos
                        foreach (var mantenimiento in mantenimientos)
                        {
                            table.Cell().Text(mantenimiento.Id.ToString());
                            table.Cell().Text(mantenimiento.Descripcion ?? "");
                            table.Cell().Text(mantenimiento.FechaInicio.ToString("dd/MM/yyyy"));
                            table.Cell().Text(mantenimiento.FechaFin.ToString("dd/MM/yyyy"));
                            table.Cell().Text(mantenimiento.Activo ? "Sí" : "No");
                            table.Cell().Text(mantenimiento.Espacio.ToString());
                            table.Cell().Text(mantenimiento.Empleado.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Mantenimientos.pdf");
        }

        [HttpPost]
        public Mantenimientos Guardar([FromBody] Mantenimientos entidad)
        {
            if (IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return IMantenimientosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Mantenimientos Actualizar([FromBody] Mantenimientos entidad)
        {
            if (IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return IMantenimientosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IMantenimientosNegocio == null)
                throw new Exception("No implementado");
            return IMantenimientosNegocio!.Eliminar(id);
        }
    }
}
