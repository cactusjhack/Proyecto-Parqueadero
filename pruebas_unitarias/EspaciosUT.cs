using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace pruebas_unitarias;

[TestClass]
public class EspaciosUT
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
    public void ListarEspacios()
    {
        Console.WriteLine("Listar Espacios");
        var lista_espacios = conexion.Espacios!.ToList();
        Assert.IsNotNull(lista_espacios);
    }

    // 2. INSERT - Add
    [TestMethod]
    [Priority(2)]
    public void InsertarEspacio()
    {
        Console.WriteLine("Insertar espacio");
        Espacios nueva = new Espacios
        {
            Numero = "Test esp",
            TipoVehiculo = Tipos.Bicicleta,
            Piso = 1
        };

        conexion.Espacios!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);// confirmó que guardó algo
    }

    // 3. UPDATE - Modificar
    [TestMethod]
    [Priority(3)]
    public void ActualizarEspacio()
    {
        Console.WriteLine("Actualizar Espacio");

        // Busca un registro existente para modificarlo
        var Espacios = conexion.Espacios!.FirstOrDefault();
        Assert.IsNotNull(Espacios); // que exista algo en la BD

            Espacios.Numero = "modificado";
        conexion.Espacios!.Update(Espacios);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    // 4. DELETE - Remove
    [TestMethod]
    [Priority(4)]
    public void QuitarEspacio()
    {
        Console.WriteLine("Eliminar Espacio");

        var Espacios = conexion.Espacios!
            .OrderByDescending(s => s.Id)
            .FirstOrDefault(s => s.Numero == "Test esp");
        Assert.IsNotNull(Espacios);

        conexion.Espacios!.Remove(Espacios);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
