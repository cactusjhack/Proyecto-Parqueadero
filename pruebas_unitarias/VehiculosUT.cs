using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace pruebas_unitarias;

[TestClass]
public class VehiculosUT
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
        Console.WriteLine("Listar Vehiculos");
        var lista_vehiculos = conexion.Vehiculos!.ToList();
        Assert.IsNotNull(lista_vehiculos);
    }

    // 2. INSERT - Add
    [TestMethod]
    public void Insertar()
    {
        Console.WriteLine("Insertar Vehiculo");
        Vehiculos nueva = new Vehiculos
        {
            Tipo = Tipos.Patineta,
            Combustion = Combustiones.Gas,
            Cliente = 1
        };

        conexion.Vehiculos!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);// confirmó que guardó algo
    }

    // 3. UPDATE - Modificar
    [TestMethod]
    public void Actualizar()
    {
        Console.WriteLine("Actualizar Vehiculo");

        // Busca un registro existente para modificarlo
        var Vehiculos = conexion.Vehiculos!.FirstOrDefault();
        Assert.IsNotNull(Vehiculos); // que exista algo en la BD

        Vehiculos.Combustion = Combustiones.Gasolina;
        conexion.Vehiculos!.Update(Vehiculos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    // 4. DELETE - Remove
    [TestMethod]
    public void Quitar()
    {
        Console.WriteLine("Eliminar Vehiculo");

        var Vehiculos = conexion.Vehiculos!
            .OrderByDescending(s => s.Id)
            .FirstOrDefault(s => s.Combustion == Combustiones.Gas);
        Assert.IsNotNull(Vehiculos);

        conexion.Vehiculos!.Remove(Vehiculos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
