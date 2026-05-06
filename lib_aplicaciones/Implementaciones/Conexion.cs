using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace lib_aplicaciones.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Le dice a EF Core que Clientes tiene su propia tabla "Clientes" en SQL
            // Sin esto, EF Core se confunde por la herencia de Personas y busca Discriminator
            //modelBuilder.Entity<Personas>().ToTable("Personas");

            // Lo mismo para Clientes - mapea directo a su tabla sin pasar por Personas
            modelBuilder.Entity<Clientes>().ToTable("Clientes");

            // Lo mismo para Empleados - mapea directo a su tabla sin pasar por Personas
            modelBuilder.Entity<Empleados>().ToTable("Empleados");

            // Configura manualmente la relacion entre Empleados y Cargos porque
            // los nombres son distintos: "_Cargo" (navegacion) y "Cargo" (FK int)
            // Sin esto EF Core inventa una columna "_CargoId" que no existe en SQL
            modelBuilder.Entity<Empleados>()
                .HasOne(e => e._Cargo)        // Empleados tiene UN Cargo (objeto completo)
                .WithMany(c => c.Empleados)   // un Cargo puede tener MUCHOS Empleados
                .HasForeignKey(e => e.Cargo); // la FK real en SQL es la columna "Cargo" (int)

            modelBuilder.Entity<Pisos>()
                .HasOne(p => p._Sede)
                .WithMany(c => c.Pisos)
                .HasForeignKey(p => p.Sede);

            modelBuilder.Entity<Espacios>()
                .HasOne(p => p._Piso)
                .WithMany(c => c.Espacios)
                .HasForeignKey(p => p.Piso);

            modelBuilder.Entity<Vehiculos>()
                .HasOne(p => p._Cliente)
                .WithMany(c => c.Vehiculos)
                .HasForeignKey(p => p.Cliente);

            modelBuilder.Entity<Ingresos>(entity =>
            {
                entity.HasOne(p => p._Vehiculo)
                .WithMany(c => c.Ingresos)
                .HasForeignKey(p => p.Vehiculo);

                entity.HasOne(p => p._Empleado)
                .WithMany(c => c.Ingresos)
                .HasForeignKey(p => p.Empleado);

                entity.HasOne(p => p._Espacio)
                .WithMany(c => c.Ingresos)
                .HasForeignKey(p => p.Espacio);
            });

            modelBuilder.Entity<Reservas>(entity =>
            {
                entity.HasOne(p => p._Vehiculo)
                .WithMany(c => c.Reservas)
                .HasForeignKey(p => p.Vehiculo);

                entity.HasOne(p => p._Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(p => p.Cliente);

                entity.HasOne(p => p._Espacio)
                .WithMany(c => c.Reservas)
                .HasForeignKey(p => p.Espacio);
            });

            modelBuilder.Entity<Tiquetes>(entity =>
            {
                entity.HasOne(p => p._Ingreso)
                .WithMany(c => c.Tiquetes)
                .HasForeignKey(p => p.Ingreso);
            });

            modelBuilder.Entity<Fichos>(entity =>
            {
                entity.HasOne(p => p._Cliente)
                .WithMany(c => c.Fichos)
                .HasForeignKey(p => p.Cliente);
            });

            modelBuilder.Entity<ValetRegistros>(entity =>
            {
                entity.HasOne(P => P._Empleado)
                .WithMany(C => C.ValetRegistros)
                .HasForeignKey(p => p.Empleado);

                entity.HasOne(P => P._Vehiculo)
                .WithMany(C => C.ValetRegistros)
                .HasForeignKey(p => p.Vehiculo);

                entity.HasOne(P => P._Ficho)
                .WithMany(C => C.ValetRegistros)
                .HasForeignKey(p => p.Ficho);

            });

            modelBuilder.Entity<Detalles>(entity =>
            {
                entity.HasOne(P => P._Empleado)
               .WithMany(C => C.Detalles)
               .HasForeignKey(p => p.Empleado);

                entity.HasOne(P => P._Vehiculo)
                .WithMany(C => C.Detalles)
                .HasForeignKey(p => p.Vehiculo);
            });

            modelBuilder.Entity<Cobros>(entity =>
            {
                entity.HasOne(P => P._Ingreso)
               .WithMany(C => C.Cobros)
               .HasForeignKey(p => p.Ingreso);

                entity.HasOne(P => P._Cliente)
                .WithMany(C => C.Cobros)
                .HasForeignKey(p => p.Cliente);

                entity.HasOne(P => P._Tarifa)
                .WithMany(C => C.Cobros)
                .HasForeignKey(p => p.Tarifa);

                entity.HasOne(P => P._Promocion)
                .WithMany(C => C.Cobros)
                .HasForeignKey(p => p.Promocion);
            });

            modelBuilder.Entity<Pagos>(entity =>
            {
                entity.HasOne(p => p._Cobro)
                .WithMany(c => c.Pagos)
                .HasForeignKey(p => p.Cobro);
            });

            modelBuilder.Entity<Notificaciones>(entity =>
            {
                entity.HasOne(p => p._Cliente)
                .WithMany(c => c.Notificaciones)
                .HasForeignKey(p => p.Cliente);
            });

            modelBuilder.Entity<Mantenimientos>(entity =>
            {
                entity.HasOne(P => P._Espacio)
               .WithMany(C => C.Mantenimientos)
               .HasForeignKey(p => p.Espacio);

                entity.HasOne(P => P._Empleado)
                .WithMany(C => C.Mantenimientos)
                .HasForeignKey(p => p.Empleado);
            });


        }

        public DbSet<Sedes>? Sedes { get; set; }
        public DbSet<Cargos>? Cargos { get; set; }
        public DbSet<Tarifas>? Tarifas { get; set; }
        public DbSet<Promociones>? Promociones { get; set; }
       //public DbSet<Personas>? Personas { get; set; }
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
    }
}
