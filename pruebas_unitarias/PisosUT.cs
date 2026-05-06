using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Runtime.CompilerServices;

namespace pruebas_unitarias;

[TestClass]
public class PisosUT
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
    public void ListarPisos()
    {
        Console.WriteLine("Listar Pisos");
        var lista_pisos = conexion.Pisos!.ToList();
        Assert.IsNotNull(lista_pisos);
    }

    // 2. INSERT - Add
    [TestMethod]
    [Priority(2)]
    public void InsertarPiso()
    {
        Console.WriteLine("Insertar Piso");
        Pisos nueva = new Pisos
        {
            Nombre = "Piso Test",
            Sede = 1,
        };

        conexion.Pisos!.Add(nueva);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);// confirmó que guardó algo
    }

    // 3. UPDATE - Modificar
    [TestMethod]
    [Priority(3)]
    public void ActualizarPiso()
    {
        Console.WriteLine("Actualizar Piso");

        // Busca un registro existente para modificarlo
        var Pisos = conexion.Pisos!.FirstOrDefault();
        Assert.IsNotNull(Pisos); // que exista algo en la BD

        Pisos.Nombre = "sede modificada";
        conexion.Pisos!.Update(Pisos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    // 4. DELETE - Remove
    [TestMethod]
    [Priority(4)]
    public void QuitarPiso()
    {
        Console.WriteLine("Eliminar Piso");

        var Pisos = conexion.Pisos!
            .OrderByDescending(s => s.Id)
            .FirstOrDefault(s => s.Nombre == "Piso Test");
        Assert.IsNotNull(Pisos);

        conexion.Pisos!.Remove(Pisos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
