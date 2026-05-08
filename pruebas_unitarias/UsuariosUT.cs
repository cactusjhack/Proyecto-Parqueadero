using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class UsuariosUT
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
    public void ListarUsuarios()
    {
        Console.WriteLine("Listar usuarios");
        var lista_usuarios = conexion.Usuarios!.ToList();
        Assert.IsNotNull(lista_usuarios);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarUsuario()
    {
        Console.WriteLine("Insertar usuario");
        Usuarios nueva = new Usuarios
        {
            NombreUsuario = "NombreUsuario Test",
            Activo = true,
            Empleado = 1,
            Rol = 1,
        };

        conexion.Usuarios!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarUsuario()
    {
        Console.WriteLine("Actualizar Usuario");

        var Usuarios = conexion.Usuarios!.FirstOrDefault();
        Assert.IsNotNull(Usuarios);

        Usuarios.NombreUsuario = "Usuario modificado";
        conexion.Usuarios!.Update(Usuarios);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarUsuario()
    {
        Console.WriteLine("Eliminar Usuario");

        var Usuarios = conexion.Usuarios!
            .OrderByDescending(u => u.Id)
            .FirstOrDefault(u => u.NombreUsuario == "Usuario modificado");
        Assert.IsNotNull(Usuarios);

        conexion.Usuarios!.Remove(Usuarios);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
