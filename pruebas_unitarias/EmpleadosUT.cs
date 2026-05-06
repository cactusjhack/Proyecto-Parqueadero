using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class EmpleadosUT
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
    public void ListarEmpleados()
    {
        Console.WriteLine("Listar Empleados");
        var lista_empleados = conexion.Empleados!.ToList();
        Assert.IsNotNull(lista_empleados);
    }

    // 2. INSERT - Add
    [TestMethod]
    [Priority(2)]
    public void InsertarEmpleado()
    {
        Console.WriteLine("Insertar Empleado");
        Empleados nueva = new Empleados
        {
            Nombre = "Empleado Test",
            Cargo = 1,
            Turno = (Turnos)1
        };

        conexion.Empleados!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);// confirmó que guardó algo
    }

    // 3. UPDATE - Modificar
    [TestMethod]
    [Priority(3)]
    public void ActualizarEmpleado()
    {
        Console.WriteLine("Empleado Promocion");

        // Busca un registro existente para modificarlo
        var Empleados = conexion.Empleados!.FirstOrDefault();
        Assert.IsNotNull(Empleados); // que exista algo en la BD

        Empleados.Nombre = "Empleado modificado";
        conexion.Empleados!.Update(Empleados);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    // 4. DELETE - Remove
    [TestMethod]
    [Priority(4)]
    public void QuitarEmpleado()
    {
        Console.WriteLine("Eliminar Empleado");

        var Empleados = conexion.Empleados!
            .OrderByDescending(p => p.Id)
            .FirstOrDefault(p => p.Nombre == "Empleado Test");
        Assert.IsNotNull(Empleados);

        conexion.Empleados!.Remove(Empleados);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
