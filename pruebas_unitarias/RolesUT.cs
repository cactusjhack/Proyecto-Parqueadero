using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class RolesUT
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
    public void ListarRoles()
    {
        Console.WriteLine("Listar roles");
        var lista_roles = conexion.Roles!.ToList();
        Assert.IsNotNull(lista_roles);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarRol()
    {
        Console.WriteLine("Insertar rol");
        Roles nueva = new Roles
        {
            Nombre = "Rol Test",
            Descripcion = "Test",
        };

        conexion.Roles!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarRol()
    {
        Console.WriteLine("Actualizar Rol");

        // Busca un registro existente para modificarlo
        var Roles = conexion.Roles!.FirstOrDefault();
        Assert.IsNotNull(Roles); // que exista algo en la BD

        Roles.Nombre = "Rol modificado";
        conexion.Roles!.Update(Roles);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarRol()
    {
        Console.WriteLine("Eliminar Rol");

        var Roles = conexion.Roles!
            .OrderByDescending(r => r.Id)
            .FirstOrDefault(r => r.Nombre == "Rol Test");
        Assert.IsNotNull(Roles);

        conexion.Roles!.Remove(Roles);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
