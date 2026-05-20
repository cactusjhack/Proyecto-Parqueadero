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
    public class EmpleadosController : ControllerBase
    {
        private IEmpleadosNegocio? IEmpleadosNegocio;

        public EmpleadosController()
        {
            IEmpleadosNegocio = new EmpleadosNegocio();
        }

        [HttpGet]
        public List<Empleados> Consultar()
        {
            if (IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return IEmpleadosNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var empleados = IEmpleadosNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Empleados");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Nombre";
            ws.Cell(1, 3).Value = "Apellido";
            ws.Cell(1, 4).Value = "Cedula";
            ws.Cell(1, 5).Value = "Telefono";
            ws.Cell(1, 6).Value = "Correo";
            ws.Cell(1, 7).Value = "Cargo";
            ws.Cell(1, 8).Value = "Turno";
            // Datos
            for (int i = 0; i < empleados.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = empleados[i].Id;
                ws.Cell(i + 2, 2).Value = empleados[i].Nombre;
                ws.Cell(i + 2, 3).Value = empleados[i].Apellido;
                ws.Cell(i + 2, 4).Value = empleados[i].Cedula;
                ws.Cell(i + 2, 5).Value = empleados[i].Telefono;
                ws.Cell(i + 2, 6).Value = empleados[i].Correo;
                ws.Cell(i + 2, 7).Value = empleados[i].Cargo;
                ws.Cell(i + 2, 8).Value = empleados[i].Turno.ToString();
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Empleados.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var empleados = IEmpleadosNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Empleados")
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
                            c.RelativeColumn();
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Text("Id").Bold();
                            header.Cell().Text("Nombre").Bold();
                            header.Cell().Text("Apellido").Bold();
                            header.Cell().Text("Cedula").Bold();
                            header.Cell().Text("Telefono").Bold();
                            header.Cell().Text("Correo").Bold();
                            header.Cell().Text("Cargo").Bold();
                            header.Cell().Text("Turno").Bold();
                        });
                        // Datos
                        foreach (var empleado in empleados)
                        {
                            table.Cell().Text(empleado.Id.ToString());
                            table.Cell().Text(empleado.Nombre ?? "");
                            table.Cell().Text(empleado.Apellido ?? "");
                            table.Cell().Text(empleado.Cedula ?? "");
                            table.Cell().Text(empleado.Telefono ?? "");
                            table.Cell().Text(empleado.Correo ?? "");
                            table.Cell().Text(empleado.Cargo.ToString());
                            table.Cell().Text(empleado.Turno.ToString() ?? "");
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Empleados.pdf");
        }

        [HttpPost]
        public Empleados Guardar([FromBody] Empleados entidad)
        {
            if (IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return IEmpleadosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Empleados Actualizar([FromBody] Empleados entidad)
        {
            if (IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return IEmpleadosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IEmpleadosNegocio == null)
                throw new Exception("No implementado");
            return IEmpleadosNegocio!.Eliminar(id);
        }
    }
}
