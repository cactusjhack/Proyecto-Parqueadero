CREATE DATABASE db_parqueadero;
GO

USE db_parqueadero;
GO

CREATE TABLE Auditorias(
Id INT PRIMARY KEY IDENTITY(1,1),
Tabla NVARCHAR(70),
Accion NVARCHAR(50),
Fecha DATETIME,
);

CREATE TABLE Roles (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    Nombre      NVARCHAR(50),
    Descripcion NVARCHAR(150)
);


CREATE TABLE Sedes (
    Id        INT PRIMARY KEY IDENTITY(1,1),
    Nombre    NVARCHAR(100),
    Direccion NVARCHAR(150),
    Ciudad    NVARCHAR(50),
    Telefono  NVARCHAR(20),
    Activa    BIT DEFAULT 0,
	Latitud   FLOAT,
	Longitud  FLOAT
);

CREATE TABLE Cargos (
    Id               INT PRIMARY KEY IDENTITY(1,1),
    Nombre           NVARCHAR(50),
    Descripcion      NVARCHAR(150),
    AccesoVehiculos  BIT DEFAULT 0,
    AccesoCaja       BIT DEFAULT 0,
    AccesoSistema    BIT DEFAULT 0
);

CREATE TABLE Tarifas (
    Id             INT PRIMARY KEY IDENTITY(1,1),
    TipoVehiculo   INT NOT NULL,
    PrecioHora     DECIMAL(10,2),
    FraccionHora   DECIMAL(5,2),
    AplicaCargador BIT DEFAULT 0,
    ValorCarga     DECIMAL(10,2)
);

CREATE TABLE Convenios (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    Empresa     NVARCHAR(100),
    Descuento   DECIMAL(5,2),
    FechaInicio DATETIME,
    FechaFin    DATETIME,
    Activo      BIT DEFAULT 1,
    Sede        INT NOT NULL REFERENCES Sedes(Id)
);

CREATE TABLE Promociones (
    Id              INT PRIMARY KEY IDENTITY(1,1),
    Nombre          NVARCHAR(100),
    Descuento       DECIMAL(5,2),
    FechaInicio     DATETIME,
    FechaFin        DATETIME,
    Activa          BIT DEFAULT 0,
    TipoVehiculo    INT,
    SoloFinDeSemana BIT DEFAULT 0
);

CREATE TABLE Personas (
    Id       INT PRIMARY KEY IDENTITY(1,1),
    Nombre   NVARCHAR(50),
    Apellido NVARCHAR(50),
    Cedula   NVARCHAR(20),
    Telefono NVARCHAR(20),
    Correo   NVARCHAR(100)
);

CREATE TABLE Clientes (
    Id       INT PRIMARY KEY IDENTITY(1,1),
    Nombre   NVARCHAR(50),
    Apellido NVARCHAR(50),
    Cedula   NVARCHAR(20),
    Telefono NVARCHAR(20),
    Correo   NVARCHAR(100),
	NumeroViajero NVARCHAR(20),
	Nacionalidad NVARCHAR(20),
	NumeroPasaporte NVARCHAR(20)
);

CREATE TABLE Empleados (
    Id       INT PRIMARY KEY IDENTITY(1,1),
    Nombre   NVARCHAR(50),
    Apellido NVARCHAR(50),
    Cedula   NVARCHAR(20),
    Telefono NVARCHAR(20),
    Correo   NVARCHAR(100),
    Cargo    INT NOT NULL REFERENCES Cargos(Id),
    Turno    INT NOT NULL
);

CREATE TABLE Usuarios (
    Id       INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50),
    Contrasena   NVARCHAR(100),
    Activo       BIT DEFAULT 1,
    Rol      INT NOT NULL REFERENCES Roles(Id),
    Empleado INT NOT NULL REFERENCES Empleados(Id)
);

CREATE TABLE Pisos (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    Nombre      NVARCHAR(50),
    Capacidad   INT,
    Descripcion NVARCHAR(100),
    Activo      BIT DEFAULT 0,
    Sede        INT NOT NULL REFERENCES Sedes(Id)
);

CREATE TABLE Espacios (
    Id           INT PRIMARY KEY IDENTITY(1,1),
    Numero       NVARCHAR(10),
    TipoVehiculo INT NOT NULL,
    PuestoCarga  BIT DEFAULT 0,
    Disponible   BIT DEFAULT 1,
    Piso         INT NOT NULL REFERENCES Pisos(Id)
);

CREATE TABLE Vehiculos (
    Id         INT PRIMARY KEY IDENTITY(1,1),
    Tipo       INT NOT NULL,
    Placa      NVARCHAR(20),
    Marca      NVARCHAR(50),
    Color      NVARCHAR(30),
    Combustion INT NOT NULL,
    Cliente    INT NOT NULL REFERENCES Clientes(Id)
);

CREATE TABLE Ingresos (
    Id         INT PRIMARY KEY IDENTITY(1,1),
    HoraEntrada DATETIME,
    HoraSalida  DATETIME,
    TotalHoras  DECIMAL(5,2),
    Vehiculo   INT NOT NULL REFERENCES Vehiculos(Id),
    Empleado   INT NOT NULL REFERENCES Empleados(Id),
    Espacio    INT NOT NULL REFERENCES Espacios(Id)
);

CREATE TABLE Reservas (
    Id           INT PRIMARY KEY IDENTITY(1,1),
    Cliente      INT NOT NULL REFERENCES Clientes(Id),
    Vehiculo     INT NOT NULL REFERENCES Vehiculos(Id),
    Espacio      INT NOT NULL REFERENCES Espacios(Id),
    FechaReserva DATETIME,
    FechaIngreso DATETIME,
    Activa       BIT DEFAULT 0
);

CREATE TABLE Tiquetes (
    Id               INT PRIMARY KEY IDENTITY(1,1),
    Codigo           NVARCHAR(30),
    FechaGeneracion  DATETIME,
    Pagado           BIT DEFAULT 0,
    FechaVencimiento DATETIME,
    Ingreso          INT NOT NULL REFERENCES Ingresos(Id)
);

CREATE TABLE Fichos (
    Id        INT PRIMARY KEY IDENTITY(1,1),
    Fecha     DATETIME,
    Codigo    NVARCHAR(20),
    Entregado BIT DEFAULT 0,
    Cliente   INT NOT NULL REFERENCES Clientes(Id)
);

CREATE TABLE ValetRegistros (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    HoraEntrada DATETIME,
    HoraSalida  DATETIME,
    Empleado    INT NOT NULL REFERENCES Empleados(Id),
    Vehiculo    INT NOT NULL REFERENCES Vehiculos(Id),
    Ficho       INT NOT NULL REFERENCES Fichos(Id)
);

CREATE TABLE Detalles (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    Ingreso     BIT DEFAULT 0,
    Descripcion NVARCHAR(200),
    Fecha       DATETIME,
    Vehiculo    INT NOT NULL REFERENCES Vehiculos(Id),
    Empleado    INT NOT NULL REFERENCES Empleados(Id)
);

CREATE TABLE Cobros (
    Id         INT PRIMARY KEY IDENTITY(1,1),
    Subtotal   DECIMAL(10,2),
    Descuento  DECIMAL(5,2),
    Total      DECIMAL(10,2),
    Ingreso    INT NOT NULL REFERENCES Ingresos(Id),
    UsoValet   BIT DEFAULT 0,
    TarifaValet DECIMAL(10,2),
    Cliente    INT NOT NULL REFERENCES Clientes(Id),
    Tarifa     INT NOT NULL REFERENCES Tarifas(Id),
    Promocion  INT NULL REFERENCES Promociones(Id)
);

CREATE TABLE Pagos (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    Cobro       INT NOT NULL REFERENCES Cobros(Id),
    MetodoPago  INT NOT NULL,
    Valor       DECIMAL(10,2),
    Fecha       DATETIME,
    Aprobado    BIT DEFAULT 0
);

CREATE TABLE Notificaciones (
    Id       INT PRIMARY KEY IDENTITY(1,1),
    Mensaje  NVARCHAR(300),
    Leida    BIT DEFAULT 0,
    Canal    NVARCHAR(20),
    Fecha    DATETIME,
    Cliente  INT NOT NULL REFERENCES Clientes(Id)
);

CREATE TABLE Camaras (
    Id        INT PRIMARY KEY IDENTITY(1,1),
    Codigo    NVARCHAR(30),
    Ubicacion NVARCHAR(100),
    Activa    BIT DEFAULT 1,
    Piso      INT NOT NULL REFERENCES Pisos(Id)
);

CREATE TABLE Mantenimientos (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(200),
    FechaInicio DATETIME,
    FechaFin    DATETIME,
    Activo      BIT DEFAULT 0,
    Espacio     INT NOT NULL REFERENCES Espacios(Id),
    Empleado    INT NOT NULL REFERENCES Empleados(Id)
);

CREATE TABLE Incidentes (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(200),
    Fecha       DATETIME,
    Tipo        NVARCHAR(50),      -- Robo, Daño, Accidente, Otro
    Resuelto    BIT DEFAULT 0,
    Espacio     INT NOT NULL REFERENCES Espacios(Id),
    Empleado    INT NOT NULL REFERENCES Empleados(Id)
);

-- =============================================
-- ROLES
-- =============================================
INSERT INTO Roles (Nombre, Descripcion) VALUES
('Administrador', 'Acceso total al sistema'),
('Operario',      'Acceso operativo basico'),
('Supervisor',    'Supervision de operaciones');

-- =============================================
-- SEDES
-- =============================================
INSERT INTO Sedes (Nombre, Direccion, Ciudad, Telefono, Activa, Latitud, Longitud) VALUES
('El Tesoro',    'Cra. 25a #1a Sur 45',   'Medellin', '4441111', 1, 6.197575713282872, -75.55821857458426),
('Santafe',      'Carrera 43A, Cl. 7 Sur #170',      'Medellin', '4442222', 1, 6.197011985958832,-75.57439391876616),
('Oviedo',   'Cra. 43A #6s-15',     'Medellin', '4443333', 1, 6.199203681862756,-75.57390544278232),
('Viva Envigado','Cra. 48 #32B Sur - 139', 'Envigado', '4444444', 1, 6.176514737445258,-75.59090699484959),
('Premium Plaza','Cra. 43A # 30-25, Av. El Poblado', 'Medellin', '4445555', 1, 6.229112074268121,-75.57084194650517);
 
-- =============================================
-- CARGOS
-- =============================================
INSERT INTO Cargos (Nombre, Descripcion, AccesoVehiculos, AccesoCaja, AccesoSistema) VALUES
('Recepcionista', 'Atiende clientes en entrada y salida',       0, 1, 1),
('Supervisor',    'Supervisa operaciones del parqueadero',      0, 1, 1),
('Valet',         'Recibe y estaciona vehiculos de clientes',   1, 0, 0),
('Vigilante',     'Controla acceso y seguridad del parqueadero',0, 0, 1),
('Mantenimiento', 'Limpieza y mantenimiento de zonas',          0, 0, 0);
 
-- =============================================
-- TARIFAS
-- TipoVehiculo: Moto=1, Carro=2, Camioneta=3, Bicicleta=4, Patineta=5
-- =============================================
INSERT INTO Tarifas (TipoVehiculo, PrecioHora, FraccionHora, AplicaCargador, ValorCarga) VALUES
(1, 2000, 0.25, 1, 3000),   -- Moto
(2, 4000, 0.25, 1, 7000),   -- Carro
(3, 6000, 0.25, 1, 10000),  -- Camioneta
(4, 1000, 0.25, 1, 2000),   -- Bicicleta
(5,  500, 0.25, 1, 1500);   -- Patineta
 
 -- =============================================
-- CONVENIOS
-- =============================================
INSERT INTO Convenios (Empresa, Descuento, FechaInicio, FechaFin, Activo, Sede) VALUES
('Empresa A', 10, '2026-01-01', '2026-12-31', 1, 1),
('Empresa B', 15, '2026-01-01', '2026-12-31', 1, 2),
('Empresa C', 20, '2026-01-01', '2026-12-31', 1, 3),
('Empresa D', 25, '2026-01-01', '2026-12-31', 1, 4),
('Empresa E', 30, '2026-01-01', '2026-12-31', 1, 5);
-- =============================================
-- PROMOCIONES
-- TipoVehiculo: NULL = aplica a todos
-- =============================================
INSERT INTO Promociones (Nombre, Descuento, FechaInicio, FechaFin, Activa, TipoVehiculo, SoloFinDeSemana) VALUES
('Fin de Semana', 10, '2026-01-01', '2026-12-31', 1, NULL, 1),
('Moto Feliz',    15, '2026-01-01', '2026-12-31', 1, 1,    0),
('Eco Friendly',  20, '2026-01-01', '2026-12-31', 1, 4,    0),
('Black Friday',  25, '2026-11-27', '2026-11-29', 1, NULL, 0),
('Navidad',       30, '2026-12-24', '2026-12-31', 1, NULL, 0);
 
-- =============================================
-- CLIENTES
-- =============================================
INSERT INTO Clientes (Nombre, Apellido, Cedula, Telefono, Correo, NumeroViajero, Nacionalidad, NumeroPasaporte) VALUES
('Julian', 'Restrepo', '54102', '3101234567', 'julian@gmail.com', 'VIP-001', 'Colombiana', 'CC54102'),
('Jimmy',  'Castro',   '10259', '3102345678', 'jimmy@gmail.com',  'FRQ-002', 'Colombiana', 'CC10259'),
('Maria',  'Lopez',    '89546', '3103456789', 'maria@gmail.com',  'VIP-003', 'Colombiana', 'CC89546'),
('Sansa',  'Stark',    '87546', '3104567890', 'sansa@gmail.com',  'VIP-004', 'Colombiana', 'CC87546'),
('David',  'Parra',    '78102', '3105678901', 'david@gmail.com',  'FRQ-005', 'Colombiana', 'CC78102'),
('Jose',   'Moreno',   '84102', '3106789012', 'jose@gmail.com',   'VIS-006', 'Colombiana', 'CC84102');
 
-- =============================================
-- EMPLEADOS
-- Turno: Mañana=1, Tarde=2, Noche=3
-- =============================================
INSERT INTO Empleados (Nombre, Apellido, Cedula, Telefono, Correo, Cargo, Turno) VALUES
('Andrea', 'Gomez',  '75369', '3001234567', 'andrea@mallplaza.com', 1, 1),
('Henry',  'Torres', '95147', '3002345678', 'henry@mallplaza.com',  2, 1),
('Miguel', 'Rios',   '85236', '3003456789', 'miguel@mallplaza.com', 5, 2),
('Michel', 'Vargas', '96321', '3004567890', 'michel@mallplaza.com', 3, 1),
('Jacobo', 'Mejia',  '25874', '3005678901', 'jacobo@mallplaza.com', 3, 2);


-- =============================================
-- USUARIOS
-- =============================================
INSERT INTO Usuarios (NombreUsuario, Contrasena, Activo, Rol, Empleado) VALUES
('andrea.gomez',  '1234', 1, 1, 1),
('henry.torres',  '1234', 1, 2, 2),
('miguel.rios',   '1234', 1, 3, 3);
 
-- =============================================
-- PISOS
-- =============================================
INSERT INTO Pisos (Nombre, Capacidad, Descripcion, Activo, Sede) VALUES
('Zona A', 80, 'Carros primer nivel',      1, 1),
('Zona B', 80, 'Carros segundo nivel',     1, 1),
('Zona C', 60, 'Motos y bicicletas',       1, 1),
('Zona D', 20, 'VIP y discapacitados',     1, 1),
('Zona E', 40, 'Visitantes corta estadia', 1, 1);
 
-- =============================================
-- ESPACIOS
-- TipoVehiculo: Moto=1, Carro=2, Camioneta=3, Bicicleta=4
-- =============================================
INSERT INTO Espacios (Numero, TipoVehiculo, PuestoCarga, Disponible, Piso) VALUES
('A-01', 2, 0, 0, 1),  -- Carro
('A-02', 2, 0, 0, 1),  -- Carro
('C-01', 1, 0, 0, 3),  -- Moto
('D-01', 3, 0, 0, 4),  -- Camioneta
('B-01', 2, 1, 0, 2),  -- Carro con cargador
('C-02', 4, 0, 0, 3);  -- Bicicleta
 
-- =============================================
-- VEHICULOS
-- Tipo: Moto=1, Carro=2, Camioneta=3, Bicicleta=4
-- Combustion: Gasolina=1, Diesel=2, Electrico=3, Hibrido=4, Gas=5
-- =============================================
INSERT INTO Vehiculos (Tipo, Placa, Marca, Color, Combustion, Cliente) VALUES
(1, 'ERT 25G', 'Suzuki', 'Negro', 1, 1),  -- Moto Gasolina
(2, 'MZR 587', 'Toyota', 'Gris',  4, 2),  -- Carro Hibrido
(2, 'JKL 894', 'Jeep',   'Blanco',5, 3),  -- Carro Gas
(3, 'PRT 27H', 'Ford',   'Negro', 1, 4),  -- Camioneta Gasolina
(2, 'KIL 478', 'BMW',    'Azul',  3, 5),  -- Carro Electrico
(4, 'N/A',     'Trek',   'Rojo',  3, 6);  -- Bicicleta Electrica
 
-- =============================================
-- INGRESOS
-- =============================================
INSERT INTO Ingresos (HoraEntrada, HoraSalida, TotalHoras, Vehiculo, Empleado, Espacio) VALUES
('2026-03-03 09:00', '2026-03-03 11:00', 2.0, 1, 1, 3),
('2026-03-03 09:00', '2026-03-03 14:00', 5.0, 2, 1, 1),
('2026-03-03 11:00', '2026-03-03 13:00', 2.0, 3, 1, 2),
('2026-03-03 10:30', '2026-03-03 13:30', 3.0, 4, 1, 4),
('2026-03-03 08:00', '2026-03-03 10:00', 2.0, 5, 1, 5),
('2026-03-03 07:00', '2026-03-03 09:00', 2.0, 6, 1, 6);
 
-- =============================================
-- RESERVAS
-- =============================================
INSERT INTO Reservas (Cliente, Vehiculo, Espacio, FechaReserva, FechaIngreso, Activa) VALUES
(1, 1, 3, '2026-03-01 09:00', '2026-03-03 10:00', 1),
(2, 2, 1, '2026-03-01 10:00', '2026-03-03 09:00', 1),
(3, 3, 2, '2026-03-02 08:00', '2026-03-03 11:00', 1),
(4, 4, 4, '2026-03-02 09:00', '2026-03-03 10:30', 0),
(5, 5, 5, '2026-03-02 11:00', '2026-03-03 08:00', 1);
 
-- =============================================
-- FICHOS
-- =============================================
INSERT INTO Fichos (Fecha, Codigo, Entregado, Cliente) VALUES
('2026-03-03 09:00', 'V-001', 1, 1),
('2026-03-03 11:00', 'V-002', 1, 3),
('2026-03-03 10:30', 'V-003', 1, 4),
('2026-03-03 08:00', 'V-004', 0, 5),
('2026-03-03 07:00', 'V-005', 1, 6);
 
-- =============================================
-- VALET REGISTROS
-- =============================================
INSERT INTO ValetRegistros (HoraEntrada, HoraSalida, Empleado, Vehiculo, Ficho) VALUES
('2026-03-03 09:00', '2026-03-03 09:05', 4, 1, 1),
('2026-03-03 11:00', '2026-03-03 11:04', 5, 3, 2),
('2026-03-03 10:30', '2026-03-03 10:34', 4, 4, 3),
('2026-03-03 08:00', '2026-03-03 08:06', 5, 5, 4),
('2026-03-03 07:00', '2026-03-03 07:04', 4, 6, 5);
 
-- =============================================
-- TIQUETES
-- =============================================
INSERT INTO Tiquetes (Codigo, FechaGeneracion, Pagado, FechaVencimiento, Ingreso) VALUES
('TK-20260303-001', '2026-03-03 09:00', 1, '2026-03-03 09:15', 1),
('TK-20260303-002', '2026-03-03 09:05', 1, '2026-03-03 09:20', 2),
('TK-20260303-003', '2026-03-03 10:00', 0, '2026-03-03 10:15', 3),
('TK-20260303-004', '2026-03-03 10:30', 1, '2026-03-03 10:45', 4),
('TK-20260303-005', '2026-03-03 08:00', 1, '2026-03-03 08:15', 5);
 
-- =============================================
-- COBROS
-- =============================================
INSERT INTO Cobros (Subtotal, Descuento, Total, Ingreso, UsoValet, TarifaValet, Cliente, Tarifa, Promocion) VALUES
( 4000, 15, 8400,  1, 1, 5000, 1, 1, 2),
(20000,  0, 20000, 2, 0,    0, 2, 2, NULL),
( 8000,  0, 13000, 3, 1, 5000, 3, 2, NULL),
(18000,  0, 23000, 4, 1, 5000, 4, 3, NULL),
(15000,  0, 15000, 5, 0,    0, 5, 2, NULL),
( 2000, 20,  1600, 6, 0,    0, 6, 4, 3);
 
-- =============================================
-- PAGOS
-- MetodoPago: Efectivo=1, Tarjeta=2, Transferencia=3, App=4
-- =============================================
INSERT INTO Pagos (Cobro, MetodoPago, Valor, Fecha, Aprobado) VALUES
(1, 4,  8400,  '2026-03-03 11:05', 1),
(2, 2,  20000, '2026-03-03 14:05', 1),
(3, 1,  13000, '2026-03-03 13:05', 1),
(4, 2,  23000, '2026-03-03 13:35', 1),
(5, 4,  15000, '2026-03-03 10:05', 1),
(6, 1,  1600,  '2026-03-03 09:05', 1);
 
-- =============================================
-- NOTIFICACIONES
-- =============================================
INSERT INTO Notificaciones (Mensaje, Leida, Canal, Fecha, Cliente) VALUES
('Tu reserva para el 03/03 a las 10:00 fue confirmada', 1, 'App',    '2026-03-01 09:05', 1),
('Tu reserva para el 03/03 a las 09:00 fue confirmada', 1, 'Correo', '2026-03-01 10:05', 2),
('Tu tiquete TK-003 vence en 15 minutos',               0, 'App',    '2026-03-03 10:00', 3),
('Promo Moto Feliz: 15% de descuento todos los dias',   1, 'App',    '2026-01-01 08:00', 1),
('Tu vehiculo fue recibido por el valet. Ficho: V-004', 1, 'App',    '2026-03-03 08:01', 5);
 
-- =============================================
-- DETALLES
-- =============================================
INSERT INTO Detalles (Ingreso, Descripcion, Fecha, Vehiculo, Empleado) VALUES
(1, 'Rayon puerta izquierda',  '2026-03-03', 2, 4),
(1, 'Abolladura parachoques',  '2026-03-03', 3, 5),
(0, 'Espejo retrovisor roto',  '2026-03-03', 4, 4),
(1, 'Llanta desinflada',       '2026-03-03', 1, 5),
(0, 'Rayon puerta derecha',    '2026-03-03', 5, 4);
 
 -- =============================================
-- CAMARAS
-- =============================================
INSERT INTO Camaras (Codigo, Ubicacion, Activa, Piso) VALUES
('C-001', 'Entrada principal', 1, 1),
('C-002', 'Zona A',           1, 1),
('C-003', 'Zona B',           1, 2),
('C-004', 'Zona C',           1, 3),
('C-005', 'Zona D',           1, 4),
('C-006', 'Zona E',           1, 5);
-- =============================================
-- MANTENIMIENTOS
-- =============================================
INSERT INTO Mantenimientos (Descripcion, FechaInicio, FechaFin, Activo, Espacio, Empleado) VALUES
('Pintura de demarcacion vial',  '2026-03-01 06:00', '2026-03-01 10:00', 0, 1, 3),
('Limpieza de aceite en piso',   '2026-03-02 07:00', '2026-03-02 09:00', 0, 2, 3),
('Reparacion sensor de barrera', '2026-03-03 08:00', '2026-03-03 12:00', 1, 3, 3),
('Cambio de iluminacion LED',    '2026-03-03 06:00', '2026-03-03 08:00', 0, 4, 3),
('Limpieza general zona C',      '2026-03-04 07:00', '2026-03-04 11:00', 0, 6, 3);

-- =============================================
-- INCIDENTES
-- =============================================
INSERT INTO Incidentes (Descripcion, Fecha, Tipo, Resuelto, Espacio, Empleado) VALUES
('Robo de objetos del interior del vehiculo', '2026-03-03 12:00', 'Robo', 0, 1, 4),
('Daño en carroceria por colision', '2026-03-03 13:00', 'Daño', 0, 2, 5),
('Accidente entre dos vehiculos al estacionar', '2026-03-03 14:00', 'Accidente', 0, 3, 4),
('Falla en sistema de pago automatico', '2026-03-03 15:00', 'Otro', 1, 4, 5),
('Pérdida de tiquete por parte del cliente', '2026-03-03 16:00', 'Otro', 1, 5, 4);

SELECT * FROM Clientes;