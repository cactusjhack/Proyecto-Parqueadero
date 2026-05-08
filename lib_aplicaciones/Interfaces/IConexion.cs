using lib_aplicaciones.Entidades;
using Microsoft.EntityFrameworkCore;


namespace lib_aplicaciones.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }
        public DbSet<Sedes>? Sedes { get; set; }
        public DbSet<Cargos>? Cargos { get; set; }
        public DbSet<Tarifas>? Tarifas { get; set; }
        public DbSet<Promociones>? Promociones { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Pisos>? Pisos { get; set; }
        public DbSet<Espacios>? Espacios { get; set; }
        public DbSet<Vehiculos>? Vehiculos { get; set; }
        public DbSet<Ingresos>? Ingresos { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Tiquetes>? Tiquetes { get; set; }
        public DbSet<Fichos>? Fichos { get; set; }
        public DbSet<ValetRegistros>? ValetRegistros { get; set; }
        public DbSet<Detalles>? Detalles { get; set; }
        public DbSet<Cobros>? Cobros { get; set; }
        public DbSet<Pagos>? Pagos { get; set; }
        public DbSet<Notificaciones>? Notificaciones { get; set; }
        public DbSet<Mantenimientos>? Mantenimientos { get; set; }
        public DbSet<Auditorias>? Auditorias { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<Convenios>? Convenios { get; set; }
        public DbSet<Camaras>? Camaras { get; set; }
        public DbSet<Incidentes>? Incidentes { get; set; }
        int SaveChanges();
    }
}
