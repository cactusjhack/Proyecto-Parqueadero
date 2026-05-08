using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class AuditoriasUT
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
    public void ListarAuditorias()
    {
        Console.WriteLine("Listar auditorias");
        var lista_auditorias = conexion.Auditorias!.ToList();
        Assert.IsNotNull(lista_auditorias);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarAuditoria()
    {
        Console.WriteLine("Insertar auditoria");
        Auditorias nueva = new Auditorias
        {
            Tabla = "Cargo Test",
            Accion = "Test",
            Fecha = DateTime.Now
        };

        conexion.Auditorias!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarAuditoria()
    {
        Console.WriteLine("Actualizar Auditoria");

        // Busca un registro existente para modificarlo
        var Auditorias = conexion.Auditorias!.FirstOrDefault();
        Assert.IsNotNull(Auditorias); // que exista algo en la BD

        Auditorias.Tabla = "Tabla modificada";
        Auditorias.Accion = "Accion modificada";
        conexion.Auditorias!.Update(Auditorias);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarAuditoria()
    {
        Console.WriteLine("Eliminar Auditoria");

        var Auditorias = conexion.Auditorias!
            .OrderByDescending(a => a.Id)
            .FirstOrDefault(a => a.Tabla == "Tabla modificada");
        Assert.IsNotNull(Auditorias);

        conexion.Auditorias!.Remove(Auditorias);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
