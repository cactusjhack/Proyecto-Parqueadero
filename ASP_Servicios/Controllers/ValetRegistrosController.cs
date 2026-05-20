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
    public class ValetRegistrosController : ControllerBase
    {
        private IValetRegistrosNegocio? IValetRegistrosNegocio;

        public ValetRegistrosController()
        {
            IValetRegistrosNegocio = new ValetRegistrosNegocio();
        }

        [HttpGet]
        public List<ValetRegistros> Consultar()
        {
            if (IValetRegistrosNegocio == null)
                throw new Exception("No implementado");
            return IValetRegistrosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var valetregsitros = IValetRegistrosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("ValetRegistros");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "HoraEntrada";
            ws.Cell(1, 3).Value = "HoraSalida";
            ws.Cell(1, 4).Value = "Empleado";
            ws.Cell(1, 5).Value = "Vehiculo";
            ws.Cell(1, 6).Value = "Ficho";

            // Datos
            for (int i = 0; i < valetregsitros.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = valetregsitros[i].Id;
                ws.Cell(i + 2, 2).Value = valetregsitros[i].HoraEntrada;
                ws.Cell(i + 2, 3).Value = valetregsitros[i].HoraSalida;
                ws.Cell(i + 2, 4).Value = valetregsitros[i].Empleado;
                ws.Cell(i + 2, 5).Value = valetregsitros[i].Vehiculo;
                ws.Cell(i + 2, 6).Value = valetregsitros[i].Ficho;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ValetRegistros.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var valetregistros = IValetRegistrosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Valet Registros")
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
                            header.Cell().Text("HoraEntrada").Bold();
                            header.Cell().Text("HoraSalida").Bold();
                            header.Cell().Text("Empleado").Bold();
                            header.Cell().Text("Vehiculo").Bold();
                            header.Cell().Text("Ficho").Bold();
                        });

                        // Datos
                        foreach (var registro in valetregistros)
                        {
                            table.Cell().Text(registro.Id.ToString());
                            table.Cell().Text(registro.HoraEntrada.ToString());
                            table.Cell().Text(registro.HoraSalida.ToString());
                            table.Cell().Text(registro.Empleado.ToString());
                            table.Cell().Text(registro.Vehiculo.ToString());
                            table.Cell().Text(registro.Ficho.ToString());
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "ValetRegistros.pdf");
        }

        [HttpPost]
        public ValetRegistros Guardar([FromBody] ValetRegistros entidad)
        {
            if (IValetRegistrosNegocio == null)
                throw new Exception("No implementado");
            return IValetRegistrosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public ValetRegistros Actualizar([FromBody] ValetRegistros entidad)
        {
            if (IValetRegistrosNegocio == null)
                throw new Exception("No implementado");
            return IValetRegistrosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IValetRegistrosNegocio == null)
                throw new Exception("No implementado");
            return IValetRegistrosNegocio!.Eliminar(id);
        }
    }
}
