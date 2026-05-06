
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Entidades
{
        public enum Tipos
        {
            Moto = 1,
            Carro = 2,
            Camioneta = 3,
            Bicicleta = 4,
            Patineta = 5
        }

        public enum Combustiones
        {
            Gasolina = 1,
            Diesel = 2,
            Electrico = 3,
            Hibrido = 4,
            Gas = 5
        }

        public enum Turnos
        {
            Mañana = 1,
            Tarde = 2,
            Noche = 3
        }

        public enum MetodosPago
        {
            Efectivo = 1,
            Tarjeta = 2,
            Transferencia = 3,
            App = 4
        }

        public interface IFacturas
        {
            decimal CalcularTotal();
        }

        public class Sedes
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Direccion { get; set; }
            public string? Ciudad { get; set; }
            public string? Telefono { get; set; }
            public bool Activa { get; set; }

            public List<Pisos>? Pisos { get; set; }
        }

        public class Cargos
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Descripcion { get; set; }
            public bool AccesoVehiculos { get; set; }
            public bool AccesoCaja { get; set; }
            public bool AccesoSistema { get; set; }

            public List<Empleados>? Empleados { get; set; }
        }

        public class Tarifas
        {
            public int Id { get; set; }
            public Tipos TipoVehiculo { get; set; }
            public decimal PrecioHora { get; set; }
            public decimal FraccionHora { get; set; }
            public bool AplicaCargador { get; set; }
            public decimal ValorCarga { get; set; }

            public List<Cobros>? Cobros { get; set; }

            public decimal CalcularCosto(decimal horas, bool esElectrico)
            {
                decimal horasRedondeadas = Math.Ceiling(horas / FraccionHora) * FraccionHora;
                decimal costo = horasRedondeadas * PrecioHora;

                if (esElectrico && AplicaCargador)
                    costo += ValorCarga;

                return costo;
            }
        }

        public class Promociones
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public decimal Descuento { get; set; }
            public DateTime? FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
            public bool Activa { get; set; }
            public Tipos? TipoVehiculo { get; set; }
            public bool SoloFinDeSemana { get; set; }

            public List<Cobros>? Cobros { get; set; }

            public bool AplicaHoy(Tipos tipo)
            {
                bool enFecha = DateTime.Today >= FechaInicio && DateTime.Today <= FechaFin;
                bool aplicaTipo = TipoVehiculo == null || TipoVehiculo == tipo;
                bool aplicaDia = !SoloFinDeSemana ||
                                 DateTime.Today.DayOfWeek == DayOfWeek.Saturday ||
                                 DateTime.Today.DayOfWeek == DayOfWeek.Sunday;
                return Activa && enFecha && aplicaTipo && aplicaDia;
            }
        }

        public class Personas
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Apellido { get; set; }
            public string? Cedula { get; set; }
            public string? Telefono { get; set; }
            public string? Correo { get; set; }
        }

        public class Pisos
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public int Capacidad { get; set; }
            public string? Descripcion { get; set; }
            public bool Activo { get; set; }
            public int Sede { get; set; }

            public Sedes? _Sede { get; set; }
            public List<Espacios>? Espacios { get; set; }
        }

        public class Clientes : Personas
        {
            public string? NumeroViajero { get; set; }
            public string? Nacionalidad { get; set; }
            public string? NumeroPasaporte { get; set; }

            public List<Vehiculos>? Vehiculos { get; set; }
            public List<Reservas>? Reservas { get; set; }
            public List<Fichos>? Fichos { get; set; }
            public List<Cobros>? Cobros { get; set; }
            public List<Notificaciones>? Notificaciones { get; set; }
        }

        public class Empleados : Personas
        {
            public int Cargo { get; set; }
            public Turnos Turno { get; set; }

            public Cargos? _Cargo { get; set; }
            public List<Ingresos>? Ingresos { get; set; }
            public List<ValetRegistros>? ValetRegistros { get; set; }
            public List<Detalles>? Detalles { get; set; }
            public List<Mantenimientos>? Mantenimientos { get; set; }
        }

        public class Vehiculos
        {
            public int Id { get; set; }
            public Tipos Tipo { get; set; }
            public string? Placa { get; set; }
            public string? Marca { get; set; }
            public string? Color { get; set; }
            public Combustiones Combustion { get; set; }
            public int Cliente { get; set; }

            public Clientes? _Cliente { get; set; }
            public List<Ingresos>? Ingresos { get; set; }
            public List<Reservas>? Reservas { get; set; }
            public List<Detalles>? Detalles { get; set; }
            public List<ValetRegistros>? ValetRegistros { get; set; }
        }

        public class Espacios
        {
            public int Id { get; set; }
            public string? Numero { get; set; }
            public Tipos TipoVehiculo { get; set; }
            public bool PuestoCarga { get; set; }
            public bool Disponible { get; set; } = true;
            public int Piso { get; set; }

            public Pisos? _Piso { get; set; }
            public List<Ingresos>? Ingresos { get; set; }
            public List<Reservas>? Reservas { get; set; }
            public List<Mantenimientos>? Mantenimientos { get; set; }
        }

        public class Reservas
        {
            public int Id { get; set; }
            public int Cliente { get; set; }
            public int Vehiculo { get; set; }
            public int Espacio { get; set; }
            public DateTime FechaReserva { get; set; }
            public DateTime FechaIngreso { get; set; }
            public bool Activa { get; set; }

            public Clientes? _Cliente { get; set; }
            public Vehiculos? _Vehiculo { get; set; }
            public Espacios? _Espacio { get; set; }
        }

        public class Notificaciones
        {
            public int Id { get; set; }
            public string? Mensaje { get; set; }
            public bool Leida { get; set; }
            public string? Canal { get; set; }
            public DateTime Fecha { get; set; }
            public int Cliente { get; set; }

            public Clientes? _Cliente { get; set; }
        }

        public class Tiquetes
        {
            public int Id { get; set; }
            public string? Codigo { get; set; }
            public DateTime FechaGeneracion { get; set; }
            public bool Pagado { get; set; }
            public DateTime FechaVencimiento { get; set; }
            public int Ingreso { get; set; }

            public Ingresos? _Ingreso { get; set; }
        }

        public class Mantenimientos
        {
            public int Id { get; set; }
            public string? Descripcion { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFin { get; set; }
            public bool Activo { get; set; }
            public int Espacio { get; set; }
            public int Empleado { get; set; }

            public Espacios? _Espacio { get; set; }
            public Empleados? _Empleado { get; set; }
        }

        public class Ingresos
        {
            public int Id { get; set; }
            public DateTime HoraEntrada { get; set; }
            public DateTime HoraSalida { get; set; }
            public decimal TotalHoras { get; set; }
            public int Vehiculo { get; set; }
            public int Empleado { get; set; }
            public int Espacio { get; set; }

            public Vehiculos? _Vehiculo { get; set; }
            public Empleados? _Empleado { get; set; }
            public Espacios? _Espacio { get; set; }
            public List<Cobros>? Cobros { get; set; }
            public List<Tiquetes>? Tiquetes { get; set; }

            public decimal CalcularHoras()
            {
                TimeSpan total = HoraSalida - HoraEntrada;
                return (decimal)total.TotalHours;
            }
        }

        public class Fichos
        {
            public int Id { get; set; }
            public DateTime Fecha { get; set; }
            public string? Codigo { get; set; }
            public bool Entregado { get; set; }
            public int Cliente { get; set; }

            public Clientes? _Cliente { get; set; }
            public List<ValetRegistros>? ValetRegistros { get; set; }
        }

        public class ValetRegistros
        {
            public int Id { get; set; }
            public DateTime HoraEntrada { get; set; }
            public DateTime HoraSalida { get; set; }
            public int Empleado { get; set; }
            public int Vehiculo { get; set; }
            public int Ficho { get; set; }

            public Empleados? _Empleado { get; set; }
            public Vehiculos? _Vehiculo { get; set; }
            public Fichos? _Ficho { get; set; }
        }

        public class Detalles
        {
            public int Id { get; set; }
            public bool Ingreso { get; set; }
            public string? Descripcion { get; set; }
            public DateTime Fecha { get; set; }
            public int Vehiculo { get; set; }
            public int Empleado { get; set; }

            public Vehiculos? _Vehiculo { get; set; }
            public Empleados? _Empleado { get; set; }
        }

        public class Cobros : IFacturas
        {
            public int Id { get; set; }
            public decimal Subtotal { get; set; }
            public decimal Descuento { get; set; }
            public decimal Total { get; set; }
            public int Ingreso { get; set; }
            public bool UsoValet { get; set; }
            public decimal TarifaValet { get; set; }
            public int Cliente { get; set; }
            public int Tarifa { get; set; }
            public int? Promocion { get; set; }

            public Ingresos? _Ingreso { get; set; }
            public Clientes? _Cliente { get; set; }
            public Tarifas? _Tarifa { get; set; }
            public Promociones? _Promocion { get; set; }
            public List<Pagos>? Pagos { get; set; }

            public decimal CalcularTotal()
            {
                decimal total = Subtotal;
                total = total - (total * Descuento / 100);
                if (UsoValet)
                    total += TarifaValet;
                return total;
            }
        }

        public class Pagos
        {
            public int Id { get; set; }
            public int Cobro { get; set; }
            public MetodosPago MetodoPago { get; set; }
            public decimal Valor { get; set; }
            public DateTime Fecha { get; set; }
            public bool Aprobado { get; set; }

            public Cobros? _Cobro { get; set; }
        }
}
