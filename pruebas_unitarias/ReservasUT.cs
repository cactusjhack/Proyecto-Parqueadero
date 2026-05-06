using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class ReservasUT
{
    IConexion conexion = new Conexion();

    [TestInitialize]
    public void Init()
    {
        conexion.StringConexion =
            "server=localhost;integrated Security=True;TrustServerCertificate=true;database=db_parqueadero;";
    }

    [TestMethod]
    [Priority(1)]
    public void ListarReservas()
    {
        Console.WriteLine("Listar reservas");
        var lista_reservas = conexion.Reservas!.ToList();
        Assert.IsNotNull(lista_reservas);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarReserva()
    {
        Console.WriteLine("Insertar reserva");
        Reservas nueva = new Reservas
        {
            Cliente = 1,
            Vehiculo = 1,
            Espacio = 1,
            FechaReserva = DateTime.Now,
            FechaIngreso = DateTime.Now.AddHours(3),
            Activa = false
        };

        conexion.Reservas!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);// confirmó que guardó algo
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarReserva()
    {
        Console.WriteLine("Actualizar Reserva");

        // Busca un registro existente para modificarlo
        var reservas = conexion.Reservas!.FirstOrDefault();
        Assert.IsNotNull(reservas); // que exista algo en la BD

        reservas.Activa = true; // Cambia el estado de la reserva a activa
        conexion.Reservas!.Update(reservas);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarReserva()
    {
        Console.WriteLine("Eliminar Reserva");
        var reservas = conexion.Reservas!
            .OrderByDescending(r => r.Id)
            .FirstOrDefault(r => r.Activa == false);
        Assert.IsNotNull(reservas);
        conexion.Reservas!.Remove(reservas);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
