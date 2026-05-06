using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class ValetRegistrosUT
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
    public void ListarValetRegistros()
    {
        Console.WriteLine("Listar valet registros");
        var lista_valetRegistros = conexion.ValetRegistros!.ToList();
        Assert.IsNotNull(lista_valetRegistros);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarValetRegistro()
    {
        Console.WriteLine("Insertar valet registro");
        ValetRegistros nuevo = new ValetRegistros
        {
            HoraEntrada = DateTime.Now,
            HoraSalida = DateTime.Now.AddHours(1),
            Empleado = 1,
            Vehiculo = 1,
            Ficho = 1
        };

        conexion.ValetRegistros!.Add(nuevo);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarValetRegistro()
    {
        Console.WriteLine("Actualizar Valet Registro");

        // Busca un registro existente para modificarlo
        var valetRegistro = conexion.ValetRegistros!.FirstOrDefault();
        Assert.IsNotNull(valetRegistro); // que exista algo en la BD

        valetRegistro.HoraSalida = DateTime.Now.AddHours(2);
        conexion.ValetRegistros!.Update(valetRegistro);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarValetRegistro()
    {
        Console.WriteLine("Eliminar Valet Registro");

        var valetRegistro = conexion.ValetRegistros!
            .OrderByDescending(v => v.Id)
            .FirstOrDefault();
        Assert.IsNotNull(valetRegistro);

        conexion.ValetRegistros!.Remove(valetRegistro);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
