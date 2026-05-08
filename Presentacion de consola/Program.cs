using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

Console.WriteLine("Conexion a base de datos");
IConexion conexion = new Conexion();
conexion.StringConexion = "server=localhost;integrated Security=True;TrustServerCertificate=true;database=db_parqueadero;";
var lista_auditorias = conexion.Auditorias!.ToList();
var lista_roles = conexion.Roles!.ToList();
var lista_sedes = conexion.Sedes!.ToList();
var lista_cargos = conexion.Cargos!.ToList();
var lista_tarifas = conexion.Tarifas!.ToList();
var lista_promociones = conexion.Promociones!.ToList();
var lista_clientes = conexion.Clientes!.ToList();
var lista_empleados = conexion.Empleados!.ToList();
var lista_pisos = conexion.Pisos!.ToList();
var lista_espacios = conexion.Espacios!.ToList();
var lista_vehiculos = conexion.Vehiculos!.ToList();
var lista_ingresos = conexion.Ingresos!.ToList();
var lista_reservas = conexion.Reservas!.ToList();
var lista_tiquetes = conexion.Tiquetes!.ToList();
var lista_fichos = conexion.Fichos!.ToList();
var lista_valetRegistros = conexion.ValetRegistros!.ToList();
var lista_detalles = conexion.Detalles!.ToList();
var lista_cobros = conexion.Cobros!.ToList();
var lista_pagos = conexion.Pagos!.ToList();
var lista_notificaciones = conexion.Notificaciones!.ToList();
var lista_mantenimientos = conexion.Mantenimientos!.ToList();
var lista_usuarios = conexion.Usuarios!.ToList();
var lista_incidentes = conexion.Incidentes!.ToList();
var lista_convenios = conexion.Convenios!.ToList();
var lista_camaras = conexion.Camaras!.ToList();


Console.WriteLine("SEDES");
foreach (var s in lista_sedes)
{
    Console.WriteLine($"{s.Id} - {s.Nombre}  ");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("CARGOS");
foreach (var c in lista_cargos)
{
    Console.WriteLine($"{c.Id} - {c.Nombre}  ");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("TARIFA");
foreach (var t in lista_tarifas)
{
    Console.WriteLine($"{t.Id} - {t.TipoVehiculo} {t.PrecioHora}  ");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("PROMOCION");
foreach (var p in lista_promociones)
{
    Console.WriteLine($"{p.Id} - {p.Nombre} {p.Descuento} ");
}

//Console.WriteLine($"Clientes encontrados: {lista_clientes.Count}");
//foreach (var cli in lista_clientes)

Console.WriteLine("----------------------------------");

Console.WriteLine("CLIENTES");
foreach (var cli in lista_clientes)
{
    Console.WriteLine($"{cli.Id} - {cli.Nombre} {cli.Apellido} {cli.Nacionalidad}  ");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("EMPLEADOS");
foreach (var e in lista_empleados)
{
    Console.WriteLine($"{e.Id} - {e.Nombre} {e.Apellido} {e.Cargo}  ");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("PISOS");
foreach (var pi in lista_pisos)
{
    Console.WriteLine($"{pi.Nombre} {pi._Sede}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("ESPACIOS");
foreach (var es in lista_espacios)
{
    Console.WriteLine($"{es.Numero} {es._Piso} {es.TipoVehiculo}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("VEHICULOS");
foreach (var v in lista_vehiculos)
{
    Console.WriteLine($"{v.Tipo} {v.Combustion} {v.Cliente}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("INGRESOS");
foreach (var i in lista_ingresos)
{
    Console.WriteLine($"{i.HoraEntrada} {i.HoraSalida} {i.TotalHoras}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("RESERVAS");
foreach (var r in lista_reservas)
{
    Console.WriteLine($"{r.Id} {r.Cliente} {r.Vehiculo} {r.FechaReserva} {r.FechaIngreso} {r.Activa}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("TIQUETES");
foreach (var t in lista_tiquetes)
{
    Console.WriteLine($"{t.Id} {t.Codigo} {t.FechaGeneracion} {t.Pagado} {t.FechaVencimiento} {t.Ingreso}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("FICHOS");
foreach (var f in lista_fichos)
{
    Console.WriteLine($"{f.Id} {f.Fecha} {f.Codigo} {f.Entregado} {f.Cliente}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("VALET REGISTROS");
foreach (var v in lista_valetRegistros)
{
    Console.WriteLine($"{v.Id} {v.HoraEntrada} {v.HoraSalida} {v.Empleado} {v.Vehiculo} {v.Ficho}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("DETALLES");
foreach (var d in lista_detalles)
{
    Console.WriteLine($"{d.Id} {d.Ingreso} {d.Descripcion} {d.Fecha} {d.Vehiculo} {d.Empleado} ");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("COBROS");
foreach (var c in lista_cobros)
{
    Console.WriteLine($"{c.Id} {c.Subtotal} {c.Descuento} {c.Total}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("PAGOS");
foreach (var p in lista_pagos)
{
    Console.WriteLine($"{p.Id} {p.Cobro} {p.MetodoPago} {p.Valor} {p.Fecha} {p.Aprobado}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("NOTIFICACIONES");
foreach (var n in lista_notificaciones)
{
    Console.WriteLine($"{n.Id} {n.Mensaje} {n.Leida} {n.Fecha} {n.Cliente}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("MANTENIMIENTOS");
foreach (var m in lista_mantenimientos)
{
    Console.WriteLine($"{m.Id} {m.Descripcion} {m.FechaInicio} {m.FechaFin} {m.Activo} {m.Espacio} {m.Empleado}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("USUARIOS");
foreach (var u in lista_usuarios)
{
    Console.WriteLine($"{u.Id} {u.NombreUsuario} {u.Rol}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("ROLES");
foreach (var r in lista_roles)
{
    Console.WriteLine($"{r.Id} {r.Nombre} {r.Descripcion}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("CONVENIOS");
foreach (var c in lista_convenios)
{
    Console.WriteLine($"{c.Id} {c.Empresa} {c.Descuento} {c.FechaInicio} {c.FechaFin} {c.Activo} {c.Sede}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("CAMARAS");
foreach (var c in lista_camaras)
{
    Console.WriteLine($"{c.Id} {c.Codigo} {c.Ubicacion} {c.Activa} {c.Piso}");
}

Console.WriteLine("----------------------------------");

Console.WriteLine("INCIDENTES");
foreach (var i in lista_incidentes)
{
    Console.WriteLine($"{i.Id} {i.Descripcion} {i.Fecha} {i.Espacio} {i.Empleado}");
}

Console.WriteLine("Final");


