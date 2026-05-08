using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class ConveniosUT
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
    public void ListarConvenios()
    {
        Console.WriteLine("Listar convenios");
        var lista_convenios = conexion.Convenios!.ToList();
        Assert.IsNotNull(lista_convenios);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarConvenio()
    {
        Console.WriteLine("Insertar convenio");
        Convenios nueva = new Convenios
        {
            Empresa = "Convenio Test",
            Descuento = 0.15m,
            FechaInicio = DateTime.Now,
            FechaFin = DateTime.Now.AddMonths(6),
            Activo = true,
            Sede = 1
        };

        conexion.Convenios!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarConvenio()
    {
        Console.WriteLine("Actualizar Convenio");

        // Busca un registro existente para modificarlo
        var convenios = conexion.Convenios!.FirstOrDefault();
        Assert.IsNotNull(convenios); // que exista algo en la BD

        convenios.Empresa = "Convenio modificado";
        conexion.Convenios!.Update(convenios);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarConvenio()
    {
        Console.WriteLine("Eliminar Convenio");

        var convenios = conexion.Convenios!
            .OrderByDescending(c => c.Id)
            .FirstOrDefault(c => c.Empresa == "Convenio Test");
        Assert.IsNotNull(convenios);

        conexion.Convenios!.Remove(convenios);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
