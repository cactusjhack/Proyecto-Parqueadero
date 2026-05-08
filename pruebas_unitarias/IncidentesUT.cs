using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class IncidentesUT
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
    public void ListarIncidentes()
    {
        Console.WriteLine("Listar incidentes");
        var lista_incidentes = conexion.Incidentes!.ToList();
        Assert.IsNotNull(lista_incidentes);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarIncidente()
    {
        Console.WriteLine("Insertar incidente");
        Incidentes nueva = new Incidentes
        {
            Descripcion = "Incidente Test",
            Fecha = DateTime.Now,
            Tipo = "Tipo Test",
            Resuelto = false,
            Espacio = 1,
            Empleado = 1
        };

        conexion.Incidentes!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarIncidente()
    {
        Console.WriteLine("Actualizar Incidente");

        var Incidentes = conexion.Incidentes!.FirstOrDefault();
        Assert.IsNotNull(Incidentes);

        Incidentes.Descripcion = "Incidente modificado";
        conexion.Incidentes!.Update(Incidentes);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarIncidente()
    {
        Console.WriteLine("Eliminar Incidente");

        var Incidentes = conexion.Incidentes!
            .OrderByDescending(c => c.Id)
            .FirstOrDefault(c => c.Descripcion == "Incidente Test");
        Assert.IsNotNull(Incidentes);

        conexion.Incidentes!.Remove(Incidentes);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
