using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class NotificacionesUT
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
    public void ListarNotificaciones()
    {
        Console.WriteLine("Listar notificaciones");
        var lista_notificaciones = conexion.Notificaciones!.ToList();
        Assert.IsNotNull(lista_notificaciones);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarNotificacion()
    {
        Console.WriteLine("Insertar notificacion");
        Notificaciones nueva = new Notificaciones
        {
            Mensaje = "Notificacion test",
            Leida = false,
            Canal = "email",
            Fecha = DateTime.Now,
            Cliente = 1
        };

        conexion.Notificaciones!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarNotificacion()
    {
        Console.WriteLine("Actualizar Notificacion");

        var notificaciones = conexion.Notificaciones!.FirstOrDefault();
        Assert.IsNotNull(notificaciones); 

        notificaciones.Mensaje = "Notificacion modificada";
        conexion.Notificaciones!.Update(notificaciones);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarNotificacion()
    {
        Console.WriteLine("Eliminar Notificacion");

        var notificaciones = conexion.Notificaciones!
            .OrderByDescending(n => n.Id)
            .FirstOrDefault(n => n.Mensaje == "Notificacion test");
        Assert.IsNotNull(notificaciones);

        conexion.Notificaciones!.Remove(notificaciones);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
