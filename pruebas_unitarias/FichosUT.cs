using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class FichosUT
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
    public void ListarFichos()
    {
        Console.WriteLine("Listar fichos");
        var lista_fichos = conexion.Fichos!.ToList();
        Assert.IsNotNull(lista_fichos);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarFicho()
    {
        Console.WriteLine("Insertar ficho");
        Fichos nueva = new Fichos
        {
            Fecha = DateTime.Now,
            Codigo = "codigo Test",
            Entregado = false,
            Cliente = 1
        };

        conexion.Fichos!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarFicho()
    {
        Console.WriteLine("Actualizar Ficho");

        var Fichos = conexion.Fichos!.FirstOrDefault();
        Assert.IsNotNull(Fichos);

        Fichos.Codigo = "codigo modificado";
        conexion.Fichos!.Update(Fichos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarFicho()
    {
        Console.WriteLine("Eliminar Ficho");

        var Fichos = conexion.Fichos!
            .OrderByDescending(c => c.Id)
            .FirstOrDefault(c => c.Codigo == "codigo Test");
        Assert.IsNotNull(Fichos);

        conexion.Fichos!.Remove(Fichos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
