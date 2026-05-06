using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class CobrosUT
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
    public void ListarCobros()
    {
        Console.WriteLine("Listar cobros");
        var lista_cobros = conexion.Cobros!.ToList();
        Assert.IsNotNull(lista_cobros);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarCobro()
    {
        Console.WriteLine("Insertar cobro");
        Cobros nuevo = new Cobros
        {
            Subtotal = 5000m,
            Descuento = 0.15m,
            Total = 4250m,
            Ingreso =1,
            UsoValet = false,
            TarifaValet = 0m,
            Cliente = 1,
            Tarifa = 1,
            Promocion = null
        };

        conexion.Cobros!.Add(nuevo);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarCobro()
    {
        Console.WriteLine("Actualizar Cobro");
        var Cobros = conexion.Cobros!.FirstOrDefault();
        Assert.IsNotNull(Cobros);

        Cobros.Subtotal = 6000m;
        Cobros.Descuento = 0.20m;
        Cobros.Total = 4800m;
        conexion.Cobros!.Update(Cobros);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarCobro()
    {
        Console.WriteLine("Eliminar Cobro");

        var cobro = conexion.Cobros!
            .OrderByDescending(c => c.Id)
            .FirstOrDefault();
        Assert.IsNotNull(cobro);

        conexion.Cobros!.Remove(cobro);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
