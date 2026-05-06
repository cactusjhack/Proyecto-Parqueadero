using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace pruebas_unitarias;

[TestClass]
public class TiquetesUT
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
    public void ListarTiquetes()
    {
        Console.WriteLine("Listar Tiquetes");
        var lista_tiquetes = conexion.Tiquetes!.ToList();
        Assert.IsNotNull(lista_tiquetes);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarTiquete()
    {
        Console.WriteLine("Insertar Tiquete");
        Tiquetes nueva = new Tiquetes
        {
            Codigo = "Codigo Test",
            FechaGeneracion = DateTime.Now,
            Pagado = false,
            FechaVencimiento = DateTime.Now.AddMinutes(10),
            Ingreso = 1
        };

        conexion.Tiquetes!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);// confirmó que guardó algo
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarTiquete()
    {
        Console.WriteLine("Actualizar Tiquete");

        var Tiquetes = conexion.Tiquetes!.FirstOrDefault();
        Assert.IsNotNull(Tiquetes); 

        Tiquetes.Codigo = "Codigo Actualizado";
        conexion.Tiquetes!.Update(Tiquetes);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarTiquete()
    {
        Console.WriteLine("Eliminar Tiquete");

        var Tiquetes = conexion.Tiquetes!
            .OrderByDescending(t => t.Id)
            .FirstOrDefault(t => t.Codigo == "Codigo Actualizado");
        Assert.IsNotNull(Tiquetes);

        conexion.Tiquetes!.Remove(Tiquetes);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
