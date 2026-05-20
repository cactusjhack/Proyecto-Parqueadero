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
    public class ClientesController : ControllerBase
    {
        private IClientesNegocio? IClientesNegocio;

        public ClientesController()
        {
            IClientesNegocio = new ClientesNegocio();
        }

        [HttpGet]
        public List<Clientes> Consultar()
        {
            if (IClientesNegocio == null)
                throw new Exception("No implementado");
            return IClientesNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var clientes = IClientesNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Clientes");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Nombre";
            ws.Cell(1, 3).Value = "Apellido";
            ws.Cell(1, 4).Value = "Cedula";
            ws.Cell(1, 5).Value = "Telefono";
            ws.Cell(1, 6).Value = "Correo";
            ws.Cell(1, 7).Value = "NumeroViajero";
            ws.Cell(1, 8).Value = "Nacionalidad";
            ws.Cell(1, 9).Value = "NumeroPasaporte";

            // Datos
            for (int i = 0; i < clientes.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = clientes[i].Id;
                ws.Cell(i + 2, 2).Value = clientes[i].Nombre;
                ws.Cell(i + 2, 3).Value = clientes[i].Apellido;
                ws.Cell(i + 2, 4).Value = clientes[i].Cedula;
                ws.Cell(i + 2, 5).Value = clientes[i].Telefono;
                ws.Cell(i + 2, 6).Value = clientes[i].Correo;
                ws.Cell(i + 2, 7).Value = clientes[i].NumeroViajero;
                ws.Cell(i + 2, 8).Value = clientes[i].Nacionalidad;
                ws.Cell(i + 2, 9).Value = clientes[i].NumeroPasaporte;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Clientes.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var clientes = IClientesNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Clientes")
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
                            header.Cell().Text("NumeroViajero").Bold();
                            header.Cell().Text("Nacionalidad").Bold();
                            header.Cell().Text("NumeroPasaporte").Bold();
                        });
                        // Datos
                        foreach (var cliente in clientes)
                        {
                            table.Cell().Text(cliente.Id.ToString());
                            table.Cell().Text(cliente.Nombre ?? "");
                            table.Cell().Text(cliente.Apellido ?? "");
                            table.Cell().Text(cliente.Cedula ?? "");
                            table.Cell().Text(cliente.Telefono ?? "");
                            table.Cell().Text(cliente.Correo ?? "");
                            table.Cell().Text(cliente.NumeroViajero ?? "");
                            table.Cell().Text(cliente.Nacionalidad ?? "");
                            table.Cell().Text(cliente.NumeroPasaporte ?? "");
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Clientes.pdf");
        }

        [HttpPost]
        public Clientes Guardar([FromBody] Clientes entidad)
        {
            if (IClientesNegocio == null)
                throw new Exception("No implementado");
            return IClientesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Clientes Actualizar([FromBody] Clientes entidad)
        {
            if (IClientesNegocio == null)
                throw new Exception("No implementado");
            return IClientesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IClientesNegocio == null)
                throw new Exception("No implementado");
            return IClientesNegocio!.Eliminar(id);
        }
    }
}
