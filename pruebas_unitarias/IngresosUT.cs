using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace pruebas_unitarias;

[TestClass]
public class IngresosUT
{
    IConexion conexion = new Conexion();

    // Método que se ejecuta ANTES de cada test (configura la conexión)
    [TestInitialize]
    public void Init()
    {
        conexion.StringConexion =
            "server=localhost;integrated Security=True;TrustServerCertificate=true;database=db_parqueadero;";
    }

    // 1. SELECT - Listar
    [TestMethod]
    public void Listar()
    {
        Console.WriteLine("Listar Ingresos");
        var lista_ingresos = conexion.Ingresos!.ToList();
        Assert.IsNotNull(lista_ingresos);
    }

    // 2. INSERT - Add
    [TestMethod]
    public void Insertar()
    {
        Console.WriteLine("Insertar Ingresos");
        Ingresos nueva = new Ingresos
        {
            HoraEntrada = DateTime.Now,
            HoraSalida = DateTime.Now,
            Vehiculo = 1,
            Empleado = 1,
            Espacio = 1,
        };

        conexion.Ingresos!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);// confirmó que guardó algo
    }

    // 3. UPDATE - Modificar
    [TestMethod]
    public void Actualizar()
    {
        Console.WriteLine("Actualizar Ingresos");

        // Busca un registro existente para modificarlo
        var Ingresos = conexion.Ingresos!.FirstOrDefault();
        Assert.IsNotNull(Ingresos); // que exista algo en la BD

        Ingresos.HoraSalida = DateTime.Now.AddHours(2);
        conexion.Ingresos!.Update(Ingresos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    // 4. DELETE - Remove
    [TestMethod]
    public void Quitar()
    {
        Console.WriteLine("Eliminar Ingreso");

        var Ingresos = conexion.Ingresos!
            .OrderByDescending(s => s.Id)
            .FirstOrDefault();
        Assert.IsNotNull(Ingresos);

        conexion.Ingresos!.Remove(Ingresos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
