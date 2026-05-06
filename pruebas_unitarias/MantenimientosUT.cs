using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class MantenimientosUT
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
    public void ListarMantenimientos()
    {
        Console.WriteLine("Listar mantenimientos");
        var lista_mantenimientos = conexion.Mantenimientos!.ToList();
        Assert.IsNotNull(lista_mantenimientos);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarMantenimiento()
    {
        Console.WriteLine("Insertar mantenimiento");
        Mantenimientos nuevo = new Mantenimientos
        {
            Descripcion = "Mantenimiento Test",
            FechaInicio = DateTime.Now,
            FechaFin = DateTime.Now.AddDays(7),
            Activo = true,
            Espacio = 1,
            Empleado = 1
        };

        conexion.Mantenimientos!.Add(nuevo);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarMantenimiento()
    {
        Console.WriteLine("Actualizar Mantenimiento");

        // Busca un registro existente para modificarlo
        var Mantenimientos = conexion.Mantenimientos!.FirstOrDefault();
        Assert.IsNotNull(Mantenimientos); // que exista algo en la BD

        Mantenimientos.Descripcion = "Mantenimiento modificado";
        conexion.Mantenimientos!.Update(Mantenimientos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarMantenimiento()
    {
        Console.WriteLine("Eliminar Mantenimiento");

        var Mantenimientos = conexion.Mantenimientos!
            .OrderByDescending(m => m.Id)
            .FirstOrDefault(m => m.Descripcion == "Mantenimiento Test");
        Assert.IsNotNull(Mantenimientos);

        conexion.Mantenimientos!.Remove(Mantenimientos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
