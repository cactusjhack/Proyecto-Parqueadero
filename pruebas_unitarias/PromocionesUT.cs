using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class PromocionesUT
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
    public void ListarPromociones()
    {
        Console.WriteLine("Listar Promociones");
        var lista_promociones = conexion.Promociones!.ToList();
        Assert.IsNotNull(lista_promociones);
    }

    // 2. INSERT - Add
    [TestMethod]
    [Priority(2)]
    public void InsertarPromocion()
    {
        Console.WriteLine("Insertar Promocion");
        Promociones nueva = new Promociones
        {
            Nombre = "Promocion Test",
            Descuento = 20m,
            FechaInicio = DateTime.Now, // si la entidad es no null si o si hay que ponerla
            FechaFin = DateTime.Now.AddDays(30),
        };

        conexion.Promociones!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);// confirmó que guardó algo
    }

    // 3. UPDATE - Modificar
    [TestMethod]
    [Priority(3)]
    public void ActualizarPromocion()
    {
        Console.WriteLine("Actualizar Promocion");

        // Busca un registro existente para modificarlo
        var Promociones = conexion.Promociones!.FirstOrDefault();
        Assert.IsNotNull(Promociones); // que exista algo en la BD

        Promociones.Nombre = "Promocion modificado";
        conexion.Promociones!.Update(Promociones);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    // 4. DELETE - Remove
    [TestMethod]
    [Priority(4)]
    public void QuitarPromocion()
    {
        Console.WriteLine("Eliminar Promocion");

        var Promociones = conexion.Promociones!
            .OrderByDescending(p => p.Id)
            .FirstOrDefault(p => p.Nombre == "Promocion Test");
        Assert.IsNotNull(Promociones);

        conexion.Promociones!.Remove(Promociones);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
