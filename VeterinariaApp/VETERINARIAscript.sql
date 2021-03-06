USE [master]
GO
/****** Object:  Database [db_Veterinaria]    Script Date: 10/11/2021 09:29:53 ******/
CREATE DATABASE [db_Veterinaria]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_Veterinaria', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db_Veterinaria.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db_Veterinaria_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db_Veterinaria_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [db_Veterinaria] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_Veterinaria].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_Veterinaria] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_Veterinaria] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_Veterinaria] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_Veterinaria] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_Veterinaria] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_Veterinaria] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_Veterinaria] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_Veterinaria] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_Veterinaria] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_Veterinaria] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_Veterinaria] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_Veterinaria] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_Veterinaria] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_Veterinaria] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_Veterinaria] SET  ENABLE_BROKER 
GO
ALTER DATABASE [db_Veterinaria] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_Veterinaria] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_Veterinaria] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_Veterinaria] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_Veterinaria] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_Veterinaria] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_Veterinaria] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_Veterinaria] SET RECOVERY FULL 
GO
ALTER DATABASE [db_Veterinaria] SET  MULTI_USER 
GO
ALTER DATABASE [db_Veterinaria] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_Veterinaria] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_Veterinaria] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_Veterinaria] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_Veterinaria] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db_Veterinaria] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'db_Veterinaria', N'ON'
GO
ALTER DATABASE [db_Veterinaria] SET QUERY_STORE = OFF
GO
USE [db_Veterinaria]
GO
/****** Object:  Table [dbo].[Atencion]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Atencion](
	[cod_atencion] [int] NOT NULL,
	[cod_mascota] [int] NOT NULL,
	[fecha] [datetime] NULL,
	[descripcion] [varchar](100) NULL,
	[importe] [money] NULL,
 CONSTRAINT [pk_atencion] PRIMARY KEY CLUSTERED 
(
	[cod_atencion] ASC,
	[cod_mascota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[cod_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[sexo] [varchar](1) NULL,
	[telefono] [int] NULL,
	[documento] [int] NULL,
	[direccion] [varchar](50) NULL,
	[edad] [int] NULL,
 CONSTRAINT [pk_cliente] PRIMARY KEY CLUSTERED 
(
	[cod_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[login]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[login](
	[cod_usuario] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](20) NOT NULL,
	[contraseña] [varchar](20) NOT NULL,
 CONSTRAINT [cod] PRIMARY KEY CLUSTERED 
(
	[cod_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mascota]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mascota](
	[cod_mascota] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[edad] [smallint] NULL,
	[tipo] [smallint] NULL,
	[cod_cliente] [int] NULL,
 CONSTRAINT [pk_mascota] PRIMARY KEY CLUSTERED 
(
	[cod_mascota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo](
	[tipo] [smallint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](40) NULL,
 CONSTRAINT [pk_tipo] PRIMARY KEY CLUSTERED 
(
	[tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 3, CAST(N'2021-11-04T16:12:14.000' AS DateTime), N'Vacunacion', 780.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 4, CAST(N'2020-01-14T00:00:00.000' AS DateTime), N'Radiografia Torax', 1200.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 7, CAST(N'2021-11-03T00:00:00.000' AS DateTime), N'Corte Pelo', 1400.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 8, CAST(N'2021-11-09T00:00:00.000' AS DateTime), N'Control de oido', 550.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 15, CAST(N'2021-11-05T00:00:00.000' AS DateTime), N'Compra collar', 750.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 20, CAST(N'2021-11-08T15:45:28.000' AS DateTime), N'Pipeta Pulgas', 650.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 21, CAST(N'2021-09-14T00:00:00.000' AS DateTime), N'Peluqueria', 1500.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 35, CAST(N'2019-02-01T00:00:00.000' AS DateTime), N'Desparasitacion', 1200.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 41, CAST(N'2021-03-14T00:00:00.000' AS DateTime), N'Baño hidratante', 750.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 1031, CAST(N'2021-11-10T00:00:00.000' AS DateTime), N'Vacuna Rabia', 1200.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 1032, CAST(N'2021-06-16T00:00:00.000' AS DateTime), N'Control Oidos', 750.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 1033, CAST(N'2021-09-07T00:00:00.000' AS DateTime), N'Pipeta Pulgas', 550.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (1, 1034, CAST(N'2021-11-08T00:00:00.000' AS DateTime), N'Radiografia', 200.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (2, 1, CAST(N'2021-11-04T19:17:16.000' AS DateTime), N'Pipeta Pulgas', 1250.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (2, 3, CAST(N'2021-11-04T13:52:32.783' AS DateTime), N'Limpieza oido', 200.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (2, 5, CAST(N'2021-11-08T00:00:00.000' AS DateTime), N'Corte pelo', 560.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (2, 7, CAST(N'2021-11-09T00:00:00.000' AS DateTime), N'Compra balanceado', 600.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (2, 8, CAST(N'2021-11-09T00:00:00.000' AS DateTime), N'Ecografia de mama', 1200.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (2, 20, CAST(N'2021-11-09T00:00:00.000' AS DateTime), N'Curacion herida superficial (pata derecha superior)', 700.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (2, 35, CAST(N'2021-10-06T00:00:00.000' AS DateTime), N'Control Rutina', 700.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (2, 1032, CAST(N'2021-08-11T00:00:00.000' AS DateTime), N'Compra Medicamentos', 550.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (2, 1033, CAST(N'2021-11-07T00:00:00.000' AS DateTime), N'Compra collar y balanceado ', 1250.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (3, 1, CAST(N'2021-11-07T00:00:00.000' AS DateTime), N'Desparasitación', 750.0000)
INSERT [dbo].[Atencion] ([cod_atencion], [cod_mascota], [fecha], [descripcion], [importe]) VALUES (3, 6, CAST(N'2019-06-24T00:00:00.000' AS DateTime), N'Control Herida Lomo', 500.0000)
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (1, N'Juan C', N'M', 32516484, 40521914, N'Raul Casariego', 23)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (2, N'Ignacio', N'M', 3513843, 132468, N'Ricardo Rojas', 23)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (3, N'Fernanda', N'F', 1511805, 20879651, N'Velez Sarsfield', 25)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (4, N'Sofia', N'F', 15378940, 5798403, N'Colon', 75)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (5, N'Elian', N'M', 498403, 4687401, N'Obispo Trejo', 24)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (6, N'Rocio', N'F', 1867403, 207816, N'Independencia', 55)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (8, N'Lucrecia', N'F', 38121200, 4018613, N'Los Granaderos', 21)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (9, N'Florencia', N'F', 354981, 4018513, N'Baradero', 21)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (10, N'Martin', N'M', 357422, 40894, N'Buenos Aires ', 0)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (11, N'Micaela', N'F', 3842169, 408911, N'Parana', 0)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (13, N'Josefina', N'F', 346481, 450984, N'Colodrero', 0)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (17, N'Ricardo', N'M', 1540415, 2015612, N'Av Alem', 22)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (19, N'Lucia', N'F', 3540614, 20984013, N'Rafael Nuñez', 20)
INSERT [dbo].[Cliente] ([cod_cliente], [nombre], [sexo], [telefono], [documento], [direccion], [edad]) VALUES (1009, N'Jeremias', N'M', 31530, 40187912, N'Monseñor P Cabrera', 37)
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[login] ON 

INSERT [dbo].[login] ([cod_usuario], [usuario], [contraseña]) VALUES (1, N'nacho', N'1234')
INSERT [dbo].[login] ([cod_usuario], [usuario], [contraseña]) VALUES (2, N'Elian', N'1234')
INSERT [dbo].[login] ([cod_usuario], [usuario], [contraseña]) VALUES (3, N'fede', N'1234')
INSERT [dbo].[login] ([cod_usuario], [usuario], [contraseña]) VALUES (4, N'chinito', N'1234')
INSERT [dbo].[login] ([cod_usuario], [usuario], [contraseña]) VALUES (5, N'Fer', N'1234')
SET IDENTITY_INSERT [dbo].[login] OFF
GO
SET IDENTITY_INSERT [dbo].[Mascota] ON 

INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (1, N'Charo', 7, 2, 1)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (3, N'Simon ', 7, 2, 1)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (4, N'Scooby', 5, 4, 2)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (5, N'Kinder', 1, 3, 2)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (6, N'Dylan', 13, 1, 3)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (7, N'Rene', 5, 4, 3)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (8, N'Bunny', 2, 2, 4)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (15, N'Peluca', 23, 3, 5)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (20, N'Mara', 4, 1, 6)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (21, N'Maslaton Jr', 8, 3, 4)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (35, N'copito', 1, 2, 9)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (41, N'Pancho', 5, 4, 13)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (1031, N'Julian Alvarez', 2, 4, 1009)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (1032, N'Fontaner', 4, 2, 10)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (1033, N'Montaner', 2, 2, 8)
INSERT [dbo].[Mascota] ([cod_mascota], [nombre], [edad], [tipo], [cod_cliente]) VALUES (1034, N'Felipe', 12, 3, 19)
SET IDENTITY_INSERT [dbo].[Mascota] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipo] ON 

INSERT [dbo].[Tipo] ([tipo], [nombre]) VALUES (1, N'perro')
INSERT [dbo].[Tipo] ([tipo], [nombre]) VALUES (2, N'gato')
INSERT [dbo].[Tipo] ([tipo], [nombre]) VALUES (3, N'araña')
INSERT [dbo].[Tipo] ([tipo], [nombre]) VALUES (4, N'iguana')
SET IDENTITY_INSERT [dbo].[Tipo] OFF
GO
ALTER TABLE [dbo].[Atencion]  WITH CHECK ADD  CONSTRAINT [fk_atencion_mascota] FOREIGN KEY([cod_mascota])
REFERENCES [dbo].[Mascota] ([cod_mascota])
GO
ALTER TABLE [dbo].[Atencion] CHECK CONSTRAINT [fk_atencion_mascota]
GO
ALTER TABLE [dbo].[Mascota]  WITH CHECK ADD  CONSTRAINT [fk_cliente] FOREIGN KEY([cod_cliente])
REFERENCES [dbo].[Cliente] ([cod_cliente])
GO
ALTER TABLE [dbo].[Mascota] CHECK CONSTRAINT [fk_cliente]
GO
ALTER TABLE [dbo].[Mascota]  WITH CHECK ADD  CONSTRAINT [fk_tipo] FOREIGN KEY([tipo])
REFERENCES [dbo].[Tipo] ([tipo])
GO
ALTER TABLE [dbo].[Mascota] CHECK CONSTRAINT [fk_tipo]
GO
/****** Object:  StoredProcedure [dbo].[SP_ALTA_CLIENTE]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_ALTA_CLIENTE]
@nombre varchar(30),
@sexo varchar(1),
@telefono int,
@documento int,
@direccion varchar(50),
@edad int
as
begin
	insert into Cliente (nombre,sexo,telefono,documento,direccion,edad) values (@nombre,@sexo,@telefono,@documento,@direccion,@edad)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ALTA_MASCOTAS]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_ALTA_MASCOTAS]
@nommascota varchar (50),
@edad smallint,
@tipo smallint,
@cliente int,
@cod_mascota int OUTPUT
AS
BEGIN
	INSERT INTO Mascota(nombre, edad , tipo,cod_cliente)
    VALUES (@nommascota, @edad, @tipo,@cliente );

	SET @cod_mascota = SCOPE_IDENTITY();

END
GO
/****** Object:  StoredProcedure [dbo].[SP_ALTA_SOLO_MASCOTA]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_ALTA_SOLO_MASCOTA]
@nommascota varchar (50),
@edad smallint,
@tipo smallint,
@cliente int
AS
BEGIN
	INSERT INTO Mascota(nombre, edad , tipo,cod_cliente)
    VALUES (@nommascota, @edad, @tipo,@cliente );
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ATENCION_CONSULTA]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_ATENCION_CONSULTA]
@cod int
as
begin
	select * from Atencion where cod_mascota = @cod
end
GO
/****** Object:  StoredProcedure [dbo].[SP_COD_ATENCION]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_COD_ATENCION]
@mascota int
as
begin
	select cod_atencion from Atencion where cod_mascota = @mascota
end
GO
/****** Object:  StoredProcedure [dbo].[SP_COD_MASCOTA]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_COD_MASCOTA]
@cod int output,
@cliente int,
@nombre varchar(30)
as
begin
	SET @cod = (select cod_mascota from Mascota where cod_cliente = @cliente
				and nombre = @nombre)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_CLIENTES]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_CONSULTAR_CLIENTES]
AS
BEGIN
	SELECT * from Cliente c order by c.cod_cliente 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTAR_MASCOTA_CLIENTE]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_CONSULTAR_MASCOTA_CLIENTE]
@cod int
AS
BEGIN
	SELECT * from Mascota m 
	where m.cod_cliente = @cod
	order by m.cod_cliente 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_CLIENTE]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_DELETE_CLIENTE]
@codigo int
as
begin
	delete Cliente where cod_cliente = @codigo
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_MASCOTA]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DELETE_MASCOTA]
@idmascota int
as
BEGIN

      delete from Mascota where cod_mascota = @idmascota
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINAR_ATENCION]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_ELIMINAR_ATENCION]
@codmascota int
as
begin
	delete Atencion where cod_mascota = @codmascota
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINAR_DETALLE_ATENCION]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_ELIMINAR_DETALLE_ATENCION]
@atencion int,
@mascota int
as
begin
	delete Atencion where cod_atencion = @atencion and cod_mascota = @mascota
end
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_ATENCION]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_INSERTAR_ATENCION]
@cod_atencion int,
@cod_mascota int,
@fecha datetime,
@descripcion varchar(100),
@importe money
AS 
BEGIN
	insert into Atencion (cod_atencion,cod_mascota,fecha,descripcion,importe) 
	values (@cod_atencion,@cod_mascota,@fecha,@descripcion,@importe)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_DETALLE_ATENCION]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_INSERTAR_DETALLE_ATENCION]
@cod_atencion int,
@cod_mascota int,
@fecha datetime,
@descripcion varchar(100),
@importe money
as
begin
	insert into Atencion (cod_atencion,cod_mascota,fecha,descripcion,importe) values (@cod_atencion,@cod_mascota,@fecha,@descripcion,@importe)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_LOGIN_CHECK]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_LOGIN_CHECK]
@usuario varchar(30),
@pass varchar(30)
as
begin
	select usuario,contraseña from login where usuario = @usuario and contraseña = @pass
end
GO
/****** Object:  StoredProcedure [dbo].[SP_MASCOTA_NOMBRE]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_MASCOTA_NOMBRE]
@nom varchar(30)
as
begin
	select * from Mascota m
	where m.nombre = @nom
end
GO
/****** Object:  StoredProcedure [dbo].[SP_PROXIMO_DETALLE]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_PROXIMO_DETALLE]
@nro int output,
@cod int
as
begin
	set @nro = ( select MAX(cod_atencion) +1 from Atencion where cod_mascota = @cod )
end
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_ATENCION]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[SP_UPDATE_ATENCION]
@atencion int,
@mascota int,
@fecha datetime,
@descripcion varchar(100),
@importe money
as
begin
	update Atencion set fecha = @fecha, descripcion = @descripcion, importe = @importe
	where cod_atencion = @atencion and cod_mascota = @mascota
end
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_CLIENTE]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UPDATE_CLIENTE]
@nombre varchar(50),
@sexo varchar(1),
@telefono int,
@documento int,
@direccion varchar(50),
@edad int,
@codigo int
as
begin

	update Cliente set nombre = @nombre, sexo = @sexo, telefono = @telefono, documento= @documento, direccion=@direccion, edad = @edad
	where cod_cliente = @codigo
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_MASCOTA2]    Script Date: 10/11/2021 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_UPDATE_MASCOTA2]
@cod int,
@nom varchar (30),
@edad int,
@tipo int
as
begin
	update Mascota set nombre = @nom, edad = @edad, tipo=@tipo
	where cod_mascota = @cod
end
GO
USE [master]
GO
ALTER DATABASE [db_Veterinaria] SET  READ_WRITE 
GO
