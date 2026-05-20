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
    public class PisosController : ControllerBase
    {
        private IPisosNegocio? IPisosNegocio;

        public PisosController()
        {
            IPisosNegocio = new PisosNegocio();
        }

        [HttpGet]
        public List<Pisos> Consultar()
        {
            if (IPisosNegocio == null)
                throw new Exception("No implementado");
            return IPisosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var pisos = IPisosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Pisos");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Nombre";
            ws.Cell(1, 3).Value = "Capacidad";
            ws.Cell(1, 4).Value = "Descrripcion";
            ws.Cell(1, 5).Value = "Activo";
            ws.Cell(1, 6).Value = "Sede";

            // Datos
            for (int i = 0; i < pisos.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = pisos[i].Id;
                ws.Cell(i + 2, 2).Value = pisos[i].Nombre;
                ws.Cell(i + 2, 3).Value = pisos[i].Capacidad;
                ws.Cell(i + 2, 4).Value = pisos[i].Descripcion;
                ws.Cell(i + 2, 5).Value = pisos[i].Activo;
                ws.Cell(i + 2, 6).Value = pisos[i].Sede;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Pisos.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var pisos = IPisosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Pisos")
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
                            header.Cell().Text("Capacidad").Bold();
                            header.Cell().Text("Descripcion").Bold();
                            header.Cell().Text("Activo").Bold();
                            header.Cell().Text("Sede").Bold();
                        });

                        // Datos
                        foreach (var piso in pisos)
                        {
                            table.Cell().Text(piso.Id.ToString());
                            table.Cell().Text(piso.Nombre ?? "");
                            table.Cell().Text(piso.Capacidad.ToString());
                            table.Cell().Text(piso.Descripcion ?? "");
                            table.Cell().Text(piso.Activo ? "Sí" : "No");
                            table.Cell().Text(piso.Sede.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Pisos.pdf");
        }

        [HttpPost]
        public Pisos Guardar([FromBody] Pisos entidad)
        {
            if (IPisosNegocio == null)
                throw new Exception("No implementado");
            return IPisosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Pisos Actualizar([FromBody] Pisos entidad)
        {
            if (IPisosNegocio == null)
                throw new Exception("No implementado");
            return IPisosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IPisosNegocio == null)
                throw new Exception("No implementado");
            return IPisosNegocio!.Eliminar(id);
        }
    }
}
