using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class DetallesUT
{
    IConexion conexion = new Conexion();

    // Método que se ejecuta ANTES de cada test (configura la conexión)
    [TestInitialize]
    public void Init()
    {
        conexion.StringConexion =
            "server=localhost;integrated Security=True;TrustServerCertificate=true;database=db_parqueadero;";
    }

    [TestMethod]
    [Priority(1)]
    public void ListarDetalles()
    {
        Console.WriteLine("Listar detalles");
        var lista_detalles = conexion.Detalles!.ToList();
        Assert.IsNotNull(lista_detalles);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarDetalle()
    {
        Console.WriteLine("Insertar detalle");
        Detalles nuevo = new Detalles
        {
            Ingreso = true,
            Descripcion = "Detalle Test",
            Fecha = DateTime.Now,
            Vehiculo = 1,
            Empleado = 1,
        };

        conexion.Detalles!.Add(nuevo);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarDetalle()
    {
        Console.WriteLine("Actualizar Detalle");

        // Busca un registro existente para modificarlo
        var detalles = conexion.Detalles!.FirstOrDefault();
        Assert.IsNotNull(detalles); // que exista algo en la BD

        detalles.Descripcion = "Detalle modificado";
        conexion.Detalles!.Update(detalles);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarDetalle()
    {
        Console.WriteLine("Eliminar Detalle");

        var detalles = conexion.Detalles!
            .OrderByDescending(c => c.Id)
            .FirstOrDefault(c => c.Descripcion == "Detalle Test");
        Assert.IsNotNull(detalles);

        conexion.Detalles!.Remove(detalles);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
