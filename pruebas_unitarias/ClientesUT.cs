using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class ClientesUT
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
    [Priority(1)]
    public void ListarClientes()
    {
        Console.WriteLine("Listar clientes");
        var lista_clientes = conexion.Clientes!.ToList();
        Assert.IsNotNull(lista_clientes);
    }

    // 2. INSERT - Add
    [TestMethod]
    [Priority(2)]
    public void InsertarCliente()
    {
        Console.WriteLine("Insertar Cliente");
        Clientes nueva = new Clientes
        {
            Nombre = "Cliente Test",
            Nacionalidad = "Test",
        };

        conexion.Clientes!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);// confirmó que guardó algo
    }

    // 3. UPDATE - Modificar
    [TestMethod]
    [Priority(3)]
    public void ActualizarCliente()
    {
        Console.WriteLine("Actualizar Cliente");

        // Busca un registro existente para modificarlo
        var Clientes = conexion.Clientes!.FirstOrDefault();
        Assert.IsNotNull(Clientes); // que exista algo en la BD

        Clientes.Nombre = "Cliente modificado";
        conexion.Clientes!.Update(Clientes);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    // 4. DELETE - Remove
    [TestMethod]
    [Priority(4)]
    public void QuitarCliente()
    {
        Console.WriteLine("Eliminar Cliente");

        var Clientes = conexion.Clientes!
            .OrderByDescending(c => c.Id)
            .FirstOrDefault(c => c.Nombre == "Cliente Test");
        Assert.IsNotNull(Clientes);

        conexion.Clientes!.Remove(Clientes);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
