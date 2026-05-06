using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

namespace pruebas_unitarias;

[TestClass]
public class PagosUT
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
    public void ListarPagos()
    {
        Console.WriteLine("Listar pagos");
        var lista_pagos = conexion.Pagos!.ToList();
        Assert.IsNotNull(lista_pagos);
    }

    [TestMethod]
    [Priority(2)]
    public void InsertarPago()
    {
        Console.WriteLine("Insertar pago");
        Pagos nuevo = new Pagos
        {
            Cobro = 1,
            MetodoPago = (MetodosPago)1,
            Valor = 10000,
            Fecha = DateTime.Now,
            Aprobado = true
        };

        conexion.Pagos!.Add(nuevo);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }

    [TestMethod]
    [Priority(3)]
    public void ActualizarPago()
    {
        Console.WriteLine("Actualizar Pago");

        // Busca un registro existente para modificarlo
        var pagos = conexion.Pagos!.FirstOrDefault();
        Assert.IsNotNull(pagos); // que exista algo en la BD

        pagos.MetodoPago = (MetodosPago)2;
        conexion.Pagos!.Update(pagos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);

    }

    [TestMethod]
    [Priority(4)]
    public void QuitarPago()
    {
        Console.WriteLine("Eliminar Pago");

        var pagos = conexion.Pagos!
            .OrderByDescending(c => c.Id)
            .FirstOrDefault();
        Assert.IsNotNull(pagos);

        conexion.Pagos!.Remove(pagos);
        int resultado = conexion.SaveChanges();

        Assert.IsTrue(resultado > 0);
    }
}
