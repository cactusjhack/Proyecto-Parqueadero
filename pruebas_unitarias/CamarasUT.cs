using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class CamarasUT
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
    public void ListarCamaras()
    {
        Console.WriteLine("Listar camaras");
        var lista_camaras = conexion.Camaras!.ToList();
        Assert.IsNotNull(lista_camaras);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarCamara()
    {
        Console.WriteLine("Insertar camara");
        Camaras nueva = new Camaras
        {
            Codigo = "Camara Test",
            Ubicacion = "Test",
            Activa = true,
            Piso = 1
        };

        conexion.Camaras!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarCamara()
    {
        Console.WriteLine("Actualizar Camara");

        // Busca un registro existente para modificarlo
        var camaras = conexion.Camaras!.FirstOrDefault();
        Assert.IsNotNull(camaras); // que exista algo en la BD

        camaras.Codigo = "Camara modificada";
        conexion.Camaras!.Update(camaras);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarCamara()
    {
        Console.WriteLine("Eliminar Camara");

        var camaras = conexion.Camaras!
            .OrderByDescending(c => c.Id)
            .FirstOrDefault(c => c.Codigo == "Camara Test");
        Assert.IsNotNull(camaras);

        conexion.Camaras!.Remove(camaras);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
