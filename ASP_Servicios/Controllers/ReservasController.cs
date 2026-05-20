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
    public class ReservasController : ControllerBase
    {
        private IReservasNegocio? IReservasNegocio;

        public ReservasController()
        {
            IReservasNegocio = new ReservasNegocio();
        }

        [HttpGet]
        public List<Reservas> Consultar()
        {
            if (IReservasNegocio == null)
                throw new Exception("No implementado");
            return IReservasNegocio!.Consultar();
        }

        [HttpGet]
        public IActionResult ExportarExcel()
        {
            var reservas = IReservasNegocio!.Consultar();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Reservas");

            // Encabezados
            ws.Cell(1, 1).Value = "Id";
            ws.Cell(1, 2).Value = "Cliente";
            ws.Cell(1, 3).Value = "Vehiculo";
            ws.Cell(1, 4).Value = "Espacio";
            ws.Cell(1, 5).Value = "FechaReserva";
            ws.Cell(1, 6).Value = "FechaIngreso";
            ws.Cell(1, 7).Value = "Activa";

            // Datos
            for (int i = 0; i < reservas.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = reservas[i].Id;
                ws.Cell(i + 2, 2).Value = reservas[i].Cliente;
                ws.Cell(i + 2, 3).Value = reservas[i].Vehiculo;
                ws.Cell(i + 2, 4).Value = reservas[i].Espacio;
                ws.Cell(i + 2, 5).Value = reservas[i].FechaReserva;
                ws.Cell(i + 2, 6).Value = reservas[i].FechaIngreso;
                ws.Cell(i + 2, 7).Value = reservas[i].Activa;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Reservas.xlsx");
        }

        [HttpGet]
        public IActionResult ExportarPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var reservas = IReservasNegocio!.Consultar();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    page.Header().Text("Reporte de Reservas")
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
                            header.Cell().Text("Cliente").Bold();
                            header.Cell().Text("Vehiculo").Bold();
                            header.Cell().Text("Espacio").Bold();
                            header.Cell().Text("FechaReserva").Bold();
                            header.Cell().Text("FechaIngreso").Bold();
                            header.Cell().Text("Activa").Bold();
                        });

                        // Datos
                        foreach (var reserva in reservas)
                        {
                            table.Cell().Text(reserva.Id.ToString());
                            table.Cell().Text(reserva.Cliente.ToString());
                            table.Cell().Text(reserva.Vehiculo.ToString());
                            table.Cell().Text(reserva.Espacio.ToString());
                            table.Cell().Text(reserva.FechaReserva.ToString("dd/MM/yyyy HH:mm"));
                            table.Cell().Text(reserva.FechaIngreso.ToString("dd/MM/yyyy HH:mm"));
                            table.Cell().Text(reserva.Activa ? "Sí" : "No");
                        }
                    });

                    page.Footer().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontSize(10).AlignRight();
                });
            });

            var stream = new MemoryStream();
            pdf.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "Reservas.pdf");
        }

        [HttpPost]
        public Reservas Guardar([FromBody] Reservas entidad)
        {
            if (IReservasNegocio == null)
                throw new Exception("No implementado");
            return IReservasNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Reservas Actualizar([FromBody] Reservas entidad)
        {
            if (IReservasNegocio == null)
                throw new Exception("No implementado");
            return IReservasNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IReservasNegocio == null)
                throw new Exception("No implementado");
            return IReservasNegocio!.Eliminar(id);
        }
    }
}
