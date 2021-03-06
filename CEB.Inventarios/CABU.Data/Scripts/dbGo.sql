USE [master]
GO
/****** Object:  Database [dbgo]    Script Date: 01/05/2016 16:00:59 ******/
CREATE DATABASE [dbgo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbgo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\dbgo.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'dbgo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\dbgo_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dbgo] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbgo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbgo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbgo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbgo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbgo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbgo] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbgo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbgo] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [dbgo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbgo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbgo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbgo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbgo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbgo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbgo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbgo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbgo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbgo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbgo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbgo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbgo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbgo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbgo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbgo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbgo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbgo] SET  MULTI_USER 
GO
ALTER DATABASE [dbgo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbgo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbgo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbgo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [dbgo]
GO
/****** Object:  Table [dbo].[tblCategorias]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCategorias](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[id_empresa] [int] NULL,
 CONSTRAINT [PK_tblCategorias_1] PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblCompras]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCompras](
	[id_compra] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](200) NULL,
	[cantidad] [int] NULL,
	[valor_compra] [varchar](70) NULL,
	[valor_total] [varchar](70) NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [PK_tblCompras] PRIMARY KEY CLUSTERED 
(
	[id_compra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEmpresa]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEmpresa](
	[id_empresa] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NOT NULL,
	[tipo_identificacion] [int] NOT NULL,
	[identificacion] [varchar](50) NOT NULL,
	[direccion] [varchar](150) NULL,
	[contacto_nombre] [varchar](250) NULL,
	[contacto_telefono] [varchar](50) NULL,
	[contacto_celular] [varchar](50) NULL,
	[contacto_email] [varchar](150) NULL,
	[activa] [bit] NULL,
 CONSTRAINT [PK_tblEmpresa] PRIMARY KEY CLUSTERED 
(
	[id_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEstadosProductos]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEstadosProductos](
	[id_estado] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [PK_tblEstadosProductos] PRIMARY KEY CLUSTERED 
(
	[id_estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblFacturas]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblFacturas](
	[id_factura] [int] IDENTITY(500,1) NOT NULL,
	[nombre] [varchar](70) NULL,
	[celular] [varchar](70) NULL,
	[direccion] [varchar](70) NULL,
	[fecha] [datetime] NULL,
	[valor_bruto] [float] NULL,
	[iva] [float] NULL,
	[valor_neto] [float] NULL,
	[usuario] [varchar](70) NULL,
	[numero_factura] [varchar](50) NULL,
	[id_sucursal] [int] NULL,
 CONSTRAINT [PK_tblFacturas] PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblLogs]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblLogs](
	[id_log] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](max) NULL,
	[usuario] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMovimientoProductos]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMovimientoProductos](
	[id_movimientoM] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NULL,
	[descripcion] [varchar](350) NULL,
	[id_producto] [int] NULL,
	[valor] [decimal](18, 0) NULL,
	[cantidad] [int] NULL,
	[total] [decimal](18, 0) NULL,
	[fecha_registro] [datetime] NULL,
 CONSTRAINT [PK_tblMovimientoProductos] PRIMARY KEY CLUSTERED 
(
	[id_movimientoM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMovimientos]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMovimientos](
	[id_movimiento] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NULL,
	[descripcion] [varchar](550) NULL,
	[tipo] [varchar](50) NULL,
	[valor] [decimal](18, 0) NULL,
	[fecha_ingreso] [datetime] NULL,
	[id_sucursal] [int] NULL,
 CONSTRAINT [PK_tblMovimientos] PRIMARY KEY CLUSTERED 
(
	[id_movimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPermisos]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPermisos](
	[id_permiso] [int] IDENTITY(1,1) NOT NULL,
	[valor] [varchar](30) NOT NULL,
	[descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_tblPermisos] PRIMARY KEY CLUSTERED 
(
	[id_permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPermisosUsuarios]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPermisosUsuarios](
	[id_permiso] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_Permisos_usuarios] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_tblPermisosUsuarios] PRIMARY KEY CLUSTERED 
(
	[id_Permisos_usuarios] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblProductos]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblProductos](
	[id_producto] [int] IDENTITY(1,100) NOT NULL,
	[nombre] [varchar](200) NOT NULL,
	[codigo] [varchar](200) NULL,
	[descripcion] [varchar](500) NULL,
	[precio] [money] NOT NULL,
	[existencias] [int] NOT NULL,
	[servicio] [bit] NULL,
	[tipo] [varchar](50) NULL,
	[marca] [varchar](50) NULL,
	[id_estado] [int] NULL,
	[fecha_ingreso] [datetime] NULL,
	[last_update] [datetime] NULL,
	[id_subcategoria] [int] NULL,
	[id_categoria] [int] NULL,
	[activo] [bit] NULL,
	[valor_compra] [decimal](18, 2) NULL,
	[imagen] [varchar](350) NULL,
	[id_sucursal] [int] NULL,
 CONSTRAINT [PK_tblProductos_1] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblRoles]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblRoles](
	[id_rol] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [PK_tblRoles] PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSubCategorias]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSubCategorias](
	[id_subcategoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[id_categoria] [int] NULL,
 CONSTRAINT [PK_tblSubCategorias_1] PRIMARY KEY CLUSTERED 
(
	[id_subcategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSucursales]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSucursales](
	[id_sucursal] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[direccion] [varchar](150) NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](150) NULL,
	[id_empresa] [int] NULL,
 CONSTRAINT [PK_tblSucursales] PRIMARY KEY CLUSTERED 
(
	[id_sucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUsuarios]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[identificacion] [varchar](50) NULL,
	[nombres] [varchar](50) NULL,
	[apellidos] [varchar](50) NULL,
	[celular] [varchar](11) NULL,
	[direccion] [varchar](100) NULL,
	[password] [varchar](255) NULL,
	[id_rol] [int] NULL,
	[fecha_ingreso] [datetime] NULL,
	[activo] [bit] NULL,
	[id_sucursal] [int] NULL,
	[username] [varchar](150) NULL,
	[email] [varchar](250) NULL,
 CONSTRAINT [PK_tblUsuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVentas]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVentas](
	[id_venta] [int] IDENTITY(1,1) NOT NULL,
	[id_producto] [int] NULL,
	[id_factura] [int] NULL,
	[cantidad] [int] NULL,
	[valor_unitario] [float] NULL,
	[valor_total] [float] NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [PK_tblVentas] PRIMARY KEY CLUSTERED 
(
	[id_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vIngresosGastos]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[vIngresosGastos]
AS
SELECT        'Factura-' + CAST(id_factura AS varchar(12)) 'FacturaOCompra', nombre 'descripcion', fecha, valor_neto 'credito', 0 'debito',id_factura, id_sucursal
FROM            dbo.tblFacturas
UNION
SELECT        'Gasto-' + CAST(id_compra AS varchar(12)), descripcion, fecha, 0, valor_total,id_compra,1
FROM            dbo.tblCompras
UNION
SELECT        'Movimiento ' + CAST(id_movimiento AS varchar(12)), [descripcion], [fecha], [valor], 0,id_movimiento, id_sucursal
FROM            [dbo].[tblMovimientos]
WHERE        tipo = 'Entrada'
UNION
SELECT        'Movimiento ' + CAST(id_movimiento AS varchar(12)), [descripcion], [fecha], 0, [valor],id_movimiento, id_sucursal
FROM            [dbo].[tblMovimientos]
WHERE        tipo = 'Salida'








GO
/****** Object:  View [dbo].[vProductos]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vProductos]
AS
SELECT     dbo.tblProductos.nombre, dbo.tblProductos.descripcion, dbo.tblProductos.precio, dbo.tblProductos.existencias, dbo.tblProductos.tipo, dbo.tblProductos.marca, 
                      dbo.tblEstadosProductos.nombre AS Estado, dbo.tblProductos.fecha_ingreso, dbo.tblCategorias.nombre AS Categoria, 
                      dbo.tblSubCategorias.nombre AS SubCategoria
FROM         dbo.tblCategorias INNER JOIN
                      dbo.tblSubCategorias ON dbo.tblCategorias.id_categoria = dbo.tblSubCategorias.id_categoria INNER JOIN
                      dbo.tblProductos ON dbo.tblSubCategorias.id_subcategoria = dbo.tblProductos.id_subcategoria AND 
                      dbo.tblCategorias.id_categoria = dbo.tblProductos.id_categoria INNER JOIN
                      dbo.tblEstadosProductos ON dbo.tblProductos.id_estado = dbo.tblEstadosProductos.id_estado




GO
/****** Object:  View [dbo].[vProductosVentas]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vProductosVentas]
AS
SELECT        dbo.tblVentas.id_factura, dbo.tblProductos.nombre, dbo.tblProductos.descripcion, dbo.tblVentas.cantidad, dbo.tblVentas.valor_unitario, 
                         dbo.tblVentas.valor_total
FROM            dbo.tblVentas INNER JOIN
                         dbo.tblProductos ON dbo.tblVentas.id_producto = dbo.tblProductos.id_producto




GO
/****** Object:  View [dbo].[vVentasProductos]    Script Date: 01/05/2016 16:00:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vVentasProductos]
AS
SELECT        dbo.tblProductos.id_producto, dbo.tblProductos.nombre, dbo.tblVentas.id_factura, dbo.tblVentas.cantidad, dbo.tblVentas.valor_unitario, 
                         dbo.tblVentas.valor_total
FROM            dbo.tblProductos INNER JOIN
                         dbo.tblVentas ON dbo.tblProductos.id_producto = dbo.tblVentas.id_producto




GO
SET IDENTITY_INSERT [dbo].[tblCategorias] ON 

INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (19, N'General 3', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (20, N'Cuidado Corporal', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (21, N'Surti Belleza GO', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (22, N'Ropa', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (23, N'Calzado', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (24, N'Tecnologia', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (25, N'Bebidas', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (29, N'Categoria 24322', 1)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (34, N'1. General', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (38, N'CateGoria Esteban', 1)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (39, N'Lacteos', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (40, N'Cat', 1)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (41, N'Categoria', 1)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (42, N'testxtxtx', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (43, N'ealmundo', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (47, N'ggg', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (48, N'nnn', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (49, N'testCat2', 2)
INSERT [dbo].[tblCategorias] ([id_categoria], [nombre], [id_empresa]) VALUES (50, N'Categoría 1', 2)
SET IDENTITY_INSERT [dbo].[tblCategorias] OFF
SET IDENTITY_INSERT [dbo].[tblCompras] ON 

INSERT [dbo].[tblCompras] ([id_compra], [descripcion], [cantidad], [valor_compra], [valor_total], [fecha]) VALUES (7, N'Pedido Marcel France', 1, N'30000', N'30000', CAST(0x0000A45600000000 AS DateTime))
INSERT [dbo].[tblCompras] ([id_compra], [descripcion], [cantidad], [valor_compra], [valor_total], [fecha]) VALUES (8, N'Tintos', 2, N'500', N'1000', CAST(0x0000A45500000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[tblCompras] OFF
SET IDENTITY_INSERT [dbo].[tblEmpresa] ON 

INSERT [dbo].[tblEmpresa] ([id_empresa], [nombre], [tipo_identificacion], [identificacion], [direccion], [contacto_nombre], [contacto_telefono], [contacto_celular], [contacto_email], [activa]) VALUES (1, N'CabuSoft', 2, N'1024526498-1', N'Cra 19 Este No 32 b 06', N'Camilo Buitrago', N'5612389', N'3002795636', N'camilobu1.9@gmail.com', 0)
INSERT [dbo].[tblEmpresa] ([id_empresa], [nombre], [tipo_identificacion], [identificacion], [direccion], [contacto_nombre], [contacto_telefono], [contacto_celular], [contacto_email], [activa]) VALUES (2, N'Empresa 2', 1, N'9090', N'Cra 19 Este No 32 b 06', N'Camilo', N'898989', N'3002131239', N'camilobu1.9@gmail.com', 0)
INSERT [dbo].[tblEmpresa] ([id_empresa], [nombre], [tipo_identificacion], [identificacion], [direccion], [contacto_nombre], [contacto_telefono], [contacto_celular], [contacto_email], [activa]) VALUES (3, N'sadf', 1, N'123|23', N'1|32|123', N'12342134', N'1|231', N'1234234', N'camilobu1.9@gmail.com', 0)
INSERT [dbo].[tblEmpresa] ([id_empresa], [nombre], [tipo_identificacion], [identificacion], [direccion], [contacto_nombre], [contacto_telefono], [contacto_celular], [contacto_email], [activa]) VALUES (4, N'Empresa Camilo', 1, N'909090', N'909090', N'909090', N'909090', N'909090', N'camilobu1.9@gmail.com', 0)
INSERT [dbo].[tblEmpresa] ([id_empresa], [nombre], [tipo_identificacion], [identificacion], [direccion], [contacto_nombre], [contacto_telefono], [contacto_celular], [contacto_email], [activa]) VALUES (5, N'Nueva empresa', 1, N'090909', N'0909', N'0909', N'0909', N'0909', N'camilobu1.9@gmail.com', 0)
INSERT [dbo].[tblEmpresa] ([id_empresa], [nombre], [tipo_identificacion], [identificacion], [direccion], [contacto_nombre], [contacto_telefono], [contacto_celular], [contacto_email], [activa]) VALUES (6, N'Empresa CABUSOFT ltda', 2, N'129312801-2', N'Calle falsa 123', N'Camilo e', N'123897687', N'3003462748', N'camilobu1.9@gmail.com', 0)
INSERT [dbo].[tblEmpresa] ([id_empresa], [nombre], [tipo_identificacion], [identificacion], [direccion], [contacto_nombre], [contacto_telefono], [contacto_celular], [contacto_email], [activa]) VALUES (7, N'Ealmundo', 2, N'106934239-2', N'Cra 19 Este No 32 b 06', N'Julieth Diaz', N'123123', N'3132267052', N'julieth.diaz0709@gmail.com', 0)
SET IDENTITY_INSERT [dbo].[tblEmpresa] OFF
SET IDENTITY_INSERT [dbo].[tblEstadosProductos] ON 

INSERT [dbo].[tblEstadosProductos] ([id_estado], [nombre]) VALUES (1, N'En Bodega')
INSERT [dbo].[tblEstadosProductos] ([id_estado], [nombre]) VALUES (2, N'Sin Existencias')
INSERT [dbo].[tblEstadosProductos] ([id_estado], [nombre]) VALUES (3, N'No Aplica')
SET IDENTITY_INSERT [dbo].[tblEstadosProductos] OFF
SET IDENTITY_INSERT [dbo].[tblFacturas] ON 

INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (507, N'Osvaldo Mora', N'3208903124', N'Calle 34 no 3 b 4', CAST(0x0000A4560168117F AS DateTime), 37800, 7200, 45000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (508, N'Mercedes Restrepo', N'', N'', CAST(0x0000A456016A2C78 AS DateTime), 26880, 5120, 32000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (509, N'Camilo Buitrago', N'310323012', N'Cra 19 este no 32 b 06', CAST(0x0000A456016CE6C0 AS DateTime), 84000, 16000, 100000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (510, N'Camilo Buitrago', N'310323012', N'Cra 19 este no 32 b 06', CAST(0x0000A456016DC645 AS DateTime), 5040, 960, 6000, N'Osvaldo  Arias', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (511, N'Camilo Buitrago', N'310323012', N'Cra 19 este no 32 b 06', CAST(0x0000A456016E355B AS DateTime), 11760, 2240, 14000, N'Osvaldo  Arias', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (512, N'Jersson', N'', N'', CAST(0x0000A45C00AE3522 AS DateTime), 10920, 2080, 13000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (513, N'Camilo Buitrago', N'3002795636', N'Cra 19 Este No 32 b 06', CAST(0x0000A47700F08FC4 AS DateTime), 10248, 1952, 12200, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (514, N'Esteban Buitrago', N'2301230', N'Calle 32 no 45 b 3', CAST(0x0000A47701083588 AS DateTime), 25200, 4800, 30000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (515, N'Prueba ', N'no tiene', N'no tiene', CAST(0x0000A477010A2EB2 AS DateTime), 21000, 4000, 25000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (516, N'Camilo', N'asdas', N'casda', CAST(0x0000A477010E4601 AS DateTime), 7896, 1504, 9400, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (517, N'Osvaldo Arias', N'30021323089', N'Calle 39 asd 132', CAST(0x0000A477012E80A1 AS DateTime), 50400, 9600, 60000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (518, N'Juan Guillermo Mora', N'', N'', CAST(0x0000A477012F4031 AS DateTime), 16800, 3200, 20000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (519, N'', N'', N'asdasd', CAST(0x0000A477017165C3 AS DateTime), 5880, 1120, 7000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (520, N'', N'', N'', CAST(0x0000A47701739133 AS DateTime), 16800, 3200, 20000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (521, N'', N'', N'', CAST(0x0000A47701749860 AS DateTime), 1008, 192, 1200, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (522, N'', N'', N'', CAST(0x0000A47701754799 AS DateTime), 5880, 1120, 7000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (523, N'', N'', N'', CAST(0x0000A4770175A60C AS DateTime), 5880, 1120, 7000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (524, N'', N'', N'', CAST(0x0000A4770176138E AS DateTime), 5880, 1120, 7000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (525, N'Usuario No Definido', N'', N'', CAST(0x0000A47701769709 AS DateTime), 5880, 1120, 7000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (526, N'Usuario No Definido', N'', N'', CAST(0x0000A4770176E61A AS DateTime), 5880, 1120, 7000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (527, N'Usuario No Definido', N'', N'', CAST(0x0000A47701778083 AS DateTime), 5880, 1120, 7000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (528, N'Esteban', N'', N'', CAST(0x0000A477017E5460 AS DateTime), 5880, 1120, 7000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (529, N'Mercedes Restrepo', N'21323', N'', CAST(0x0000A47800FE5BC2 AS DateTime), 100800, 19200, 120000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (530, N'Adriana Cortez', N'', N'', CAST(0x0000A4780101A07F AS DateTime), 13944, 2656, 16600, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (531, N'Usuario No Definido', N'', N'', CAST(0x0000A4780107C42B AS DateTime), 50400, 9600, 60000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (532, N'Paul Blend', N'', N'', CAST(0x0000A4AC0157E863 AS DateTime), 546000, 104000, 650000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (534, N'Camilooo', N'3002795636', N'Cra 123 no 23 ', NULL, 0, NULL, 0, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (535, N'Usuario No Definido', N'', N'', CAST(0x0000A5BE00F148B5 AS DateTime), 0, 0, 0, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (536, N'Usuario No Definido', N'', N'', CAST(0x0000A5BE00F17B55 AS DateTime), 126000, 24000, 150000, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (537, N'Usuario No Definido', N'', N'', CAST(0x0000A5C2012387C4 AS DateTime), 37968, 7232, 45200, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (538, N'Usuario No Definido', N'', N'', CAST(0x0000A5C20123A34D AS DateTime), 37968, 7232, 45200, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (539, N'Camilo', N'Bu', N'sdfsdfs', CAST(0x0000A5C500F2F043 AS DateTime), 1766100, 336400, 2102500, N'Camilo  Buitrago', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (540, N'Usuario No Definido', N'', N'', CAST(0x0000A5C6011E097F AS DateTime), 19908, 3792, 23700, N'Camilo Esteban', NULL, NULL)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (541, N'Camilo Bu', N'', N'Calle 123', CAST(0x0000A5C70003BA59 AS DateTime), 336000, 64000, 400000, N'Julieth Diaz Gonzalez', NULL, 7)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (542, N'Usuario No Definido', N'', N'', CAST(0x0000A5C70004B0A9 AS DateTime), 174300, 33200, 207500, N'Julieth Diaz Gonzalez', NULL, 7)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (543, N'Usuario No Definido', N'', N'', CAST(0x0000A5CC00F8F7DC AS DateTime), 15036, 2864, 17900, N'Camilo Esteban', NULL, 2)
INSERT [dbo].[tblFacturas] ([id_factura], [nombre], [celular], [direccion], [fecha], [valor_bruto], [iva], [valor_neto], [usuario], [numero_factura], [id_sucursal]) VALUES (544, N'Camilo Buitrago', N'3002795636', N'Calle 1 no 2 3', CAST(0x0000A5EA01824902 AS DateTime), 1848, 352, 2200, N'Camilo Esteban', NULL, 2)
SET IDENTITY_INSERT [dbo].[tblFacturas] OFF
SET IDENTITY_INSERT [dbo].[tblMovimientoProductos] ON 

INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (1, CAST(0xBF3A0B00 AS Date), N'Se vencio el producto', 3601, CAST(1200 AS Decimal(18, 0)), 1, CAST(1200 AS Decimal(18, 0)), CAST(0x0000A47801034364 AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (2, CAST(0xAE390B00 AS Date), N'Defecto de fabrica', 4301, CAST(120000 AS Decimal(18, 0)), 1, CAST(120000 AS Decimal(18, 0)), CAST(0x0000A4AC015A1BBC AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (3, CAST(0x1F3B0B00 AS Date), N'Concepto', 4101, CAST(30000 AS Decimal(18, 0)), 2, CAST(60000 AS Decimal(18, 0)), CAST(0x0000A5C4011BCCAF AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (4, CAST(0x1F3B0B00 AS Date), N'Concepto se fue el producto', 4101, CAST(30000 AS Decimal(18, 0)), 1, CAST(30000 AS Decimal(18, 0)), CAST(0x0000A5C4011CE35A AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (5, CAST(0x1F3B0B00 AS Date), N'Concepto se fue el producto', 4001, CAST(10000 AS Decimal(18, 0)), 1, CAST(10000 AS Decimal(18, 0)), CAST(0x0000A5C4011D123E AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (6, CAST(0x1F3B0B00 AS Date), N'Concepto se fue el producto', 4701, CAST(2000 AS Decimal(18, 0)), 1, CAST(2000 AS Decimal(18, 0)), CAST(0x0000A5C4011D1244 AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (7, CAST(0x1F3B0B00 AS Date), N'Eliminacion Directa', 4201, CAST(30000 AS Decimal(18, 0)), 1, CAST(30000 AS Decimal(18, 0)), CAST(0x0000A5C4011EA931 AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (8, CAST(0x1F3B0B00 AS Date), N'Eliminacion Directa', 4101, CAST(30000 AS Decimal(18, 0)), 1, CAST(30000 AS Decimal(18, 0)), CAST(0x0000A5C4011EC4F2 AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (9, CAST(0x223B0B00 AS Date), N'Se daño la sprite', 5301, CAST(1500 AS Decimal(18, 0)), 1, CAST(1500 AS Decimal(18, 0)), CAST(0x0000A5C700053980 AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (10, CAST(0x453B0B00 AS Date), N'Productos Dañados', 3501, CAST(20000 AS Decimal(18, 0)), 1, CAST(20000 AS Decimal(18, 0)), CAST(0x0000A5EA0186411D AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (11, CAST(0x453B0B00 AS Date), N'Productos Dañados', 3301, CAST(14000 AS Decimal(18, 0)), 1, CAST(14000 AS Decimal(18, 0)), CAST(0x0000A5EA0186412B AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (12, CAST(0x453B0B00 AS Date), N'Productos Dañaros', 5601, CAST(1000 AS Decimal(18, 0)), 1, CAST(1000 AS Decimal(18, 0)), CAST(0x0000A5EA018701A4 AS DateTime))
INSERT [dbo].[tblMovimientoProductos] ([id_movimientoM], [fecha], [descripcion], [id_producto], [valor], [cantidad], [total], [fecha_registro]) VALUES (13, CAST(0x453B0B00 AS Date), N'Productos Dañaros', 3001, CAST(20000 AS Decimal(18, 0)), 1, CAST(20000 AS Decimal(18, 0)), CAST(0x0000A5EA018701AB AS DateTime))
SET IDENTITY_INSERT [dbo].[tblMovimientoProductos] OFF
SET IDENTITY_INSERT [dbo].[tblMovimientos] ON 

INSERT [dbo].[tblMovimientos] ([id_movimiento], [fecha], [descripcion], [tipo], [valor], [fecha_ingreso], [id_sucursal]) VALUES (17, CAST(0x223B0B00 AS Date), N'Caja', N'Entrada', CAST(200000 AS Decimal(18, 0)), CAST(0x0000A5C70004E39E AS DateTime), NULL)
INSERT [dbo].[tblMovimientos] ([id_movimiento], [fecha], [descripcion], [tipo], [valor], [fecha_ingreso], [id_sucursal]) VALUES (18, CAST(0x223B0B00 AS Date), N'Tintos', N'Salida', CAST(4000 AS Decimal(18, 0)), CAST(0x0000A5C70004F8AC AS DateTime), NULL)
INSERT [dbo].[tblMovimientos] ([id_movimiento], [fecha], [descripcion], [tipo], [valor], [fecha_ingreso], [id_sucursal]) VALUES (19, CAST(0x453B0B00 AS Date), N'Compra Insumos', N'Salida', CAST(90000 AS Decimal(18, 0)), CAST(0x0000A5EA01858CFA AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[tblMovimientos] OFF
SET IDENTITY_INSERT [dbo].[tblPermisos] ON 

INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (1, N'ProductosR', N'Administracion de Productos (Lectura)')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (2, N'ProductosRW', N'Administracion de Productos (Lectura y escritura)')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (3, N'CategoriasR', N'Administracion de Categorias(Lectura)')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (4, N'CategoriasRW', N'Administracion de Categorias(Lectura y Escritura)')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (5, N'Ventas', N'Ventas')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (6, N'FacturasC', N'Consulta Facturas')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (7, N'MovimientosES', N'Movimientos Entrada/Salida')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (8, N'MovimientosP', N'MovimientosProductos')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (9, N'ReporteI', N'Reporte Inventarios')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (10, N'ReporteIG', N'Reporte de Ingresos y Gastos')
INSERT [dbo].[tblPermisos] ([id_permiso], [valor], [descripcion]) VALUES (11, N'Sucursales', N'Administracion de Sucursales')
SET IDENTITY_INSERT [dbo].[tblPermisos] OFF
SET IDENTITY_INSERT [dbo].[tblPermisosUsuarios] ON 

INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (1, 13, 1)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (3, 13, 2)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (5, 13, 3)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (6, 13, 4)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (2, 15, 20)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (6, 15, 21)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (7, 15, 22)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (8, 15, 23)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (2, 14, 26)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (9, 14, 27)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (10, 14, 28)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (1, 16, 33)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (5, 16, 34)
INSERT [dbo].[tblPermisosUsuarios] ([id_permiso], [id_usuario], [id_Permisos_usuarios]) VALUES (6, 16, 35)
SET IDENTITY_INSERT [dbo].[tblPermisosUsuarios] OFF
SET IDENTITY_INSERT [dbo].[tblProductos] ON 

INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (2601, N'Tintura 04 thinks', N'', N'Tintura 04 thinks', 9000.0000, 1, 0, N'', N'', 1, CAST(0x0000A45601667280 AS DateTime), CAST(0x0000A47801098D47 AS DateTime), 36, 20, 1, CAST(7000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (2701, N'Corte Caballero', N'', N'Corte Caballero', 7000.0000, 0, 1, N'', N'', 3, CAST(0x0000A45601667280 AS DateTime), CAST(0x0000A456016E24E8 AS DateTime), 37, 21, 1, CAST(5000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (2801, N'Crema Capilar 500gm', N'789789', N'', 12000.0000, 0, 0, N'', N'no se', 1, CAST(0x0000A45601667280 AS DateTime), CAST(0x0000A456016743E9 AS DateTime), 35, 20, 1, CAST(11000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (2901, N'Crema Capilar 600gm', N'123123', N'', 15000.0000, 0, 0, N'', N'no se', 1, CAST(0x0000A45601667280 AS DateTime), CAST(0x0000A456016758A9 AS DateTime), 35, 20, 1, CAST(12000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (3001, N'Crema de Manos', N'456456', N'Crema de Manos 600 mg', 20000.0000, -1, 0, N'', N'Juanita', 1, CAST(0x0000A45601692CF0 AS DateTime), CAST(0x0000A4560169B154 AS DateTime), 34, 19, 1, CAST(19000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (3201, N'Esmalte 1', N'565656', N'alguna cosa', 4000.0000, 0, 0, N'', N'', 1, CAST(0x0000A45C009D79AC AS DateTime), CAST(0x0000A45C009E7080 AS DateTime), 39, 19, 1, CAST(3200.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (3301, N'Aplicacion de tinte', N'', N'Cobro por aplicar tintexx', 14000.0000, 1, 1, N'', N'', 1, CAST(0x0000A45C00A200A8 AS DateTime), CAST(0x0000A5C500F05FD0 AS DateTime), 40, 21, 1, CAST(100.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (3401, N'Jugo Alpin Mi Alpin', N'7702001044400', N'Jugo alpin de vainilla xx', 1201230.0000, 4, 0, N'', N'Alpina', 2, CAST(0x0000A54600000000 AS DateTime), CAST(0x0000A477010DD69B AS DateTime), 51, 25, 0, CAST(1009.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (3501, N'Blusa Roja', N'blusa001', N'Blusa Roja Turqueza Talla L', 20000.0000, -1, 0, N'', N'', 1, CAST(0x0000A477012E0148 AS DateTime), CAST(0x0000A477012E2CF7 AS DateTime), 34, 19, 1, CAST(12000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (3601, N'Leche Alpin 2', N'7702001044467', N'leche alpina alpin ', 1200.0000, 15, 0, N'', N'Alpina', 1, CAST(0x0000A47800BFA5B8 AS DateTime), CAST(0x0000A47800BFF49A AS DateTime), 34, 19, 1, CAST(1000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (3801, N'Aceite Coco x 500mg', N'886112670122', N'Aceite de coconoa', 1300.0000, 1, 0, N'', N'', 1, CAST(0x0000A57000000000 AS DateTime), CAST(0x0000A5EA01829CB2 AS DateTime), 39, 19, 1, CAST(1000.00 AS Decimal(18, 2)), N'Images/Productos/3801_20160416232736shutterstock_182900615.jpg', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (3901, N'Manicure', N'', N'Manicure', 7000.0000, 0, 1, N'', N'GO', 2, CAST(0x0000A47800A200A8 AS DateTime), CAST(0x0000A478010088BE AS DateTime), 34, 19, 1, CAST(0.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (4001, N'Esqueleto', N'', N'Esqueletos de todos los colores', 10000.0000, 12, 0, N'', N'', 1, CAST(0x0000A56400000000 AS DateTime), CAST(0x0000A478010618B4 AS DateTime), 42, 22, 1, CAST(0.00 AS Decimal(18, 2)), N'Images/Productos/4001_201603011527352.PNG', 1)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (4101, N'Botines Marron talla 32', N'', N'Botas talla 32xx', 30000.0000, 3, 0, N'', N'sdgdngfdhgds', 1, CAST(0x0000A47801064D9C AS DateTime), CAST(0x0000A5C500ED538E AS DateTime), 45, 23, 1, CAST(0.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 1)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (4201, N'Botines talla 30', N'', N'Botines talla 30', 30000.0000, 1, 0, N'', N'OXI', 1, CAST(0x0000A47801070214 AS DateTime), CAST(0x0000A47801072EDC AS DateTime), 45, 23, 1, CAST(0.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 1)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (4301, N'Pc Lenovo', N'9888', N'Core I7, 4gb, 1tb', 120000.0000, 2, 0, N'', N'Lenovo', 1, CAST(0x0000A4AC0154D3F4 AS DateTime), CAST(0x0000A4AC015558F5 AS DateTime), 46, 24, 1, CAST(600000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 1)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (4401, N'Formateo PC', N'', N'Formateo PC', 30000.0000, 0, 1, N'', N'', 3, CAST(0x0000A4AC015591CC AS DateTime), CAST(0x0000A4AC0155F120 AS DateTime), 50, 24, 1, CAST(0.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 1)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (4501, N'Impresora Epson txt30', N'900', N'Impresora Full Color de punto', 350000.0000, 0, 0, N'', N'Epson', 1, CAST(0x0000A4AC00A200A8 AS DateTime), CAST(0x0000A4AC01567EBF AS DateTime), 48, 24, 1, CAST(200000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 1)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (4601, N'Coca Cola', N'7776786688', N'Bebida Gaseosa', 1200.0000, 98, 0, N'', N'', 1, CAST(0x0000A4EB015D0B78 AS DateTime), CAST(0x0000A596013067D7 AS DateTime), 51, 25, 1, CAST(1000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 3)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (4701, N'Lenovo', N'123123', N'Lenovo', 2000.0000, 4, 0, NULL, N'Lenovo', 1, CAST(0x0000A5BA00000000 AS DateTime), CAST(0x0000A596013067D7 AS DateTime), 34, 19, 1, CAST(1000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 3)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (4801, N'Motorola Moto X', N'909090', N'2gb Ram 16gb interna', 15000.0000, 4, 0, NULL, N'Motorola', 1, CAST(0x0000A5BB00000000 AS DateTime), CAST(0x0000A596013067D7 AS DateTime), NULL, 24, 1, CAST(9000.00 AS Decimal(18, 2)), N'Images\Productos\default_product.png', 3)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (5101, N'Camiloasdasd', N'Camiloasd', N'Camilo', 2000.0000, 0, 0, N'', N'', 3, CAST(0x0000A5C500CCB76C AS DateTime), CAST(0x0000A5C500EE197F AS DateTime), 66, 34, 1, CAST(0.00 AS Decimal(18, 2)), N'camilo.jpg', 1)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (5201, N'Producto Milo', N'090009', N'Camilo Esteban', 90000.0000, 7, 0, NULL, N'CABUSoft', 1, CAST(0x0000A5C600000000 AS DateTime), NULL, 77, 42, 1, CAST(0.00 AS Decimal(18, 2)), N'Images/Productos/_20160311230931dibujo 001.jpg', 2)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (5301, N'Sprite', N'09090909', N'Bebida Gaseosa', 1500.0000, 24, 0, NULL, N'Coca Cola', 1, CAST(0x0000A5C700000000 AS DateTime), NULL, 78, 43, 1, CAST(0.00 AS Decimal(18, 2)), N'Images/Productos/_20160312000942sprite.jpg', 7)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (5401, N'Tablet Lenovo a710', N'120120120', N'Tablet 1gb Ram 8 almacenamiento', 200000.0000, 1, 0, NULL, N'Lenovo', 1, CAST(0x0000A5C700000000 AS DateTime), NULL, 79, 43, 1, CAST(80000.00 AS Decimal(18, 2)), N'Images/Productos/5301_201603120011244jqQAMHPTqd0HqrSJsPZQGEctcQUIxdm8a3JeFaQwCA.jpg', 7)
INSERT [dbo].[tblProductos] ([id_producto], [nombre], [codigo], [descripcion], [precio], [existencias], [servicio], [tipo], [marca], [id_estado], [fecha_ingreso], [last_update], [id_subcategoria], [id_categoria], [activo], [valor_compra], [imagen], [id_sucursal]) VALUES (5601, N'Gaseosa Limon 350', N'901348320', N'Gaseosa de limon', 1000.0000, 28, 0, NULL, N'Posada y Tobon', 1, CAST(0x0000A5EA00000000 AS DateTime), NULL, 81, 50, 1, CAST(200.00 AS Decimal(18, 2)), N'Images/Productos/3801_20160416232053puedo-beber-gaseosa-o-refresco.jpg', 2)
SET IDENTITY_INSERT [dbo].[tblProductos] OFF
INSERT [dbo].[tblRoles] ([id_rol], [nombre]) VALUES (1, N'Administrador')
INSERT [dbo].[tblRoles] ([id_rol], [nombre]) VALUES (2, N'Administrador Usuarios')
INSERT [dbo].[tblRoles] ([id_rol], [nombre]) VALUES (3, N'Usuario Estandar')
INSERT [dbo].[tblRoles] ([id_rol], [nombre]) VALUES (4, N'Admin Empresa')
INSERT [dbo].[tblRoles] ([id_rol], [nombre]) VALUES (5, N'Usuario Empresa')
INSERT [dbo].[tblRoles] ([id_rol], [nombre]) VALUES (6, N'Custom User')
SET IDENTITY_INSERT [dbo].[tblSubCategorias] ON 

INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (34, N'General 3', 19)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (35, N'Crema Capilar', 20)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (36, N'Tintes', 20)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (37, N'Corte', 21)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (38, N'Cepillados', 21)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (39, N'General 2', 19)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (40, N'tinturas', 21)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (41, N'servicio', 21)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (42, N'Blusas', 22)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (43, N'Pantalones', 22)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (44, N'Sacos', 22)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (45, N'Calzado', 23)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (46, N'Computadores', 24)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (47, N'Partes', 24)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (48, N'Impresoras', 24)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (49, N'Perifericos', 24)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (50, N'Servicios', 24)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (51, N'Bebidas', 25)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (66, N'General', 34)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (67, N'General 1', 34)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (70, N'Chaquetas', 22)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (71, N'Leche', 39)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (72, N'Queso', 39)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (73, N'b1', 22)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (74, N'b2', 22)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (75, N'subcat', 40)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (76, N'Subcategoria xx', 41)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (77, N'textxtxtx', 42)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (78, N'Gaseosas', 43)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (79, N'Computadores', 43)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (80, N'TestSubc2', 49)
INSERT [dbo].[tblSubCategorias] ([id_subcategoria], [nombre], [id_categoria]) VALUES (81, N'Sub Categoria', 50)
SET IDENTITY_INSERT [dbo].[tblSubCategorias] OFF
SET IDENTITY_INSERT [dbo].[tblSucursales] ON 

INSERT [dbo].[tblSucursales] ([id_sucursal], [nombre], [direccion], [telefono], [email], [id_empresa]) VALUES (1, N'CabuSoft', N'Cra 19 Este No 32 b 06', N'5612389', N'camilobu1.9@gmail.com', 1)
INSERT [dbo].[tblSucursales] ([id_sucursal], [nombre], [direccion], [telefono], [email], [id_empresa]) VALUES (2, N'Empresa 2', N'Cra 19 Este No 32 b 06', N'898989', N'camilobu1.9@gmail.com', 2)
INSERT [dbo].[tblSucursales] ([id_sucursal], [nombre], [direccion], [telefono], [email], [id_empresa]) VALUES (3, N'Sucursal3', N'1 Ca 32 no 123', N'1|231', N'camilobu1.9@gmail.com', 3)
INSERT [dbo].[tblSucursales] ([id_sucursal], [nombre], [direccion], [telefono], [email], [id_empresa]) VALUES (4, N'Empresa Camilo', N'909090', N'909090', N'camilobu1.9@gmail.com', 4)
INSERT [dbo].[tblSucursales] ([id_sucursal], [nombre], [direccion], [telefono], [email], [id_empresa]) VALUES (5, N'Nueva empresa', N'0909', N'0909', N'camilobu1.9@gmail.com', 5)
INSERT [dbo].[tblSucursales] ([id_sucursal], [nombre], [direccion], [telefono], [email], [id_empresa]) VALUES (6, N'Empresa CABUSOFT ltda', N'Calle falsa 123', N'123897687', N'camilobu1.9@gmail.com', 6)
INSERT [dbo].[tblSucursales] ([id_sucursal], [nombre], [direccion], [telefono], [email], [id_empresa]) VALUES (7, N'Ealmundo.com', N'Cra 19 Este No 32 b 06', N'123123', N'julieth.diaz0709@gmail.com', 7)
INSERT [dbo].[tblSucursales] ([id_sucursal], [nombre], [direccion], [telefono], [email], [id_empresa]) VALUES (14, N'Empresa 2 CentroI', N'Centro internacional local 2', N'898798 ext 3', N'cam@bui.com', 2)
INSERT [dbo].[tblSucursales] ([id_sucursal], [nombre], [direccion], [telefono], [email], [id_empresa]) VALUES (15, N'Camilo Suc', N'asdasd', N'80809', N'camilo@sqsm.com', 2)
SET IDENTITY_INSERT [dbo].[tblSucursales] OFF
SET IDENTITY_INSERT [dbo].[tblUsuarios] ON 

INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (1, N'1024526498', N'Camilo ', N'Buitrago', N'3002795636', N'Cra 19 Este No 32 b 06', N'MTIzNDU2', 2, CAST(0x0000A45100BE9A6E AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (2, N'Administrador', N'Admin', N'', N'', N'', N'MTIzNDU2', 1, CAST(0x0000A451010ADADA AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (3, N'123456', N'Ada', N'Diaz', N'31231231', N'Calle 1 no 2 4', N'NjU0MzIx', 3, CAST(0x0000A4510184A27A AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (4, N'1234567', N'Osvaldo ', N'Arias', N'320894657', N'Calle 3 no 4 b 5', N'MTIzNDU2', 3, CAST(0x0000A456016D81C6 AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (5, N'123', N'123', N'123', N'123', N'123', N'MTIz', 3, CAST(0x0000A595014B7AA1 AS DateTime), 1, NULL, NULL, NULL)
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (6, N'1024526498', N'Camilo ', N'Buitrago', N'3002795636', NULL, N'MS5BZG1pbi4x', 4, CAST(0x0000A5C600F342A5 AS DateTime), 1, 1, N'camilobu1.9', N'camilobu1.9@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (7, N'1024526498', N'Camilo', N'Esteban', N'123213', NULL, N'MS5BZG1pbi4x', 4, CAST(0x0000A5C600FFE028 AS DateTime), 1, 2, N'camilobu', N'camilobu1.9@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (8, N'1212', N'1212', N'1212', N'123', NULL, N'MS5BZG1pbi4x', 4, CAST(0x0000A5C60100856D AS DateTime), 0, 3, N'123', N'camilobu1.9@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (9, N'909090', N'Camilo', N'Bu', N'909090', NULL, N'MS5BZG1pbi4x', 4, CAST(0x0000A5C60102DB1E AS DateTime), 0, 4, N'camilob19', N'camilobu1.9@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (10, N'09090', N'0909', N'09909', N'sda', NULL, N'MS5BZG1pbi4x', 4, CAST(0x0000A5C6010801F1 AS DateTime), 0, 5, N'camiloe1', N'camilobu1.9@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (11, N'1024513425', N'Camilo Esteban', N'Buitrago Restrepo', N'', NULL, N'MS5BZG1pbi4x', 4, CAST(0x0000A5C6016CA831 AS DateTime), 0, 6, N'cabuca', N'camilobu1.9@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (12, N'1069749241', N'Julieth', N'Diaz Gonzalez', N'3132267052', NULL, N'MTIzNDU2', 4, CAST(0x0000A5C700018425 AS DateTime), 0, 7, N'julieth.diaz', N'julieth.diaz0709@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (13, N'909090', N'User ', N'Apellido', N'909980', NULL, N'MS5BZG1pbi4x', 5, CAST(0x0000A5D5013625F0 AS DateTime), 1, 2, N'milo', N'camilobu1.9@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (14, N'122222', N'Esteman', N'Rest', N'80812738', NULL, N'MS5BZG1pbi4x', 5, CAST(0x0000A5D501410F5B AS DateTime), 0, 2, N'po', N'camilobu1.9@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (15, N'9090', N'Pedro', N'Picapiedra', N'9090', NULL, N'MS5BZG1pbi4x', 5, CAST(0x0000A5F2012BCB6C AS DateTime), 1, 14, N'pedropicapiedra', N'camilobu1.9@gmail.com')
INSERT [dbo].[tblUsuarios] ([id_usuario], [identificacion], [nombres], [apellidos], [celular], [direccion], [password], [id_rol], [fecha_ingreso], [activo], [id_sucursal], [username], [email]) VALUES (16, N'787878', N'Ada', N'Diaz', N'8903482390', NULL, N'MS5BZG1pbi4x', 5, CAST(0x0000A5F30186244D AS DateTime), 1, 2, N'adadiaz', N'camilobu1.9@gmail.com')
SET IDENTITY_INSERT [dbo].[tblUsuarios] OFF
SET IDENTITY_INSERT [dbo].[tblVentas] ON 

INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (2, 3901, 534, 20, 7000, 140000, CAST(0x0000A5BE00EFA8DF AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (3, 4701, 534, 1, 2000, 2000, CAST(0x0000A5BE00EFA8E5 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (4, 2701, 535, 1, 7000, 7000, CAST(0x0000A5BE00F148C7 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (5, 3601, 535, 1, 1200, 1200, CAST(0x0000A5BE00F148CC AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (6, 4401, 536, 1, 30000, 30000, CAST(0x0000A5BE00F17B65 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (7, 4301, 536, 1, 120000, 120000, CAST(0x0000A5BE00F17B6A AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (8, 3301, 537, 1, 14000, 14000, CAST(0x0000A5C20123882D AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (9, 4601, 537, 1, 1200, 1200, CAST(0x0000A5C201238833 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (10, 4201, 537, 1, 30000, 30000, CAST(0x0000A5C201238834 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (11, 3301, 538, 1, 14000, 14000, CAST(0x0000A5C20123A34E AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (12, 4601, 538, 1, 1200, 1200, CAST(0x0000A5C20123A352 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (13, 4201, 538, 1, 30000, 30000, CAST(0x0000A5C20123A353 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (14, 4501, 539, 6, 350000, 2100000, CAST(0x0000A5C500F2F048 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (15, 3601, 539, 1, 1200, 1200, CAST(0x0000A5C500F2F049 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (16, 3801, 539, 1, 1300, 1300, CAST(0x0000A5C500F2F049 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (17, 5101, 540, 8, 2000, 16000, CAST(0x0000A5C6011E0988 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (18, 3601, 540, 1, 1200, 1200, CAST(0x0000A5C6011E098E AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (19, 3801, 540, 5, 1300, 6500, CAST(0x0000A5C6011E0991 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (20, 5401, 541, 2, 200000, 400000, CAST(0x0000A5C70003BA69 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (21, 5401, 542, 1, 200000, 200000, CAST(0x0000A5C70004B0B6 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (22, 5301, 542, 5, 1500, 7500, CAST(0x0000A5C70004B0BC AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (23, 3801, 543, 3, 1300, 3900, CAST(0x0000A5CC00F8F7EC AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (24, 3301, 543, 1, 14000, 14000, CAST(0x0000A5CC00F8F7F1 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (25, 5601, 544, 1, 1000, 1000, CAST(0x0000A5EA01824918 AS DateTime))
INSERT [dbo].[tblVentas] ([id_venta], [id_producto], [id_factura], [cantidad], [valor_unitario], [valor_total], [fecha]) VALUES (26, 3601, 544, 1, 1200, 1200, CAST(0x0000A5EA01824923 AS DateTime))
SET IDENTITY_INSERT [dbo].[tblVentas] OFF
ALTER TABLE [dbo].[tblProductos] ADD  CONSTRAINT [DF__tblProduc__exist__117F9D94]  DEFAULT ((0)) FOR [existencias]
GO
ALTER TABLE [dbo].[tblProductos] ADD  CONSTRAINT [DF_tblProductos_imagen]  DEFAULT ('Images\Productos\default_product.png') FOR [imagen]
GO
ALTER TABLE [dbo].[tblCategorias]  WITH CHECK ADD  CONSTRAINT [FK_tblCategorias_tblEmpresa] FOREIGN KEY([id_empresa])
REFERENCES [dbo].[tblEmpresa] ([id_empresa])
GO
ALTER TABLE [dbo].[tblCategorias] CHECK CONSTRAINT [FK_tblCategorias_tblEmpresa]
GO
ALTER TABLE [dbo].[tblFacturas]  WITH CHECK ADD  CONSTRAINT [FK_tblFacturas_tblSucursales] FOREIGN KEY([id_sucursal])
REFERENCES [dbo].[tblSucursales] ([id_sucursal])
GO
ALTER TABLE [dbo].[tblFacturas] CHECK CONSTRAINT [FK_tblFacturas_tblSucursales]
GO
ALTER TABLE [dbo].[tblMovimientoProductos]  WITH CHECK ADD  CONSTRAINT [FK_tblMovimientoProductos_tblProductos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[tblProductos] ([id_producto])
GO
ALTER TABLE [dbo].[tblMovimientoProductos] CHECK CONSTRAINT [FK_tblMovimientoProductos_tblProductos]
GO
ALTER TABLE [dbo].[tblMovimientos]  WITH CHECK ADD  CONSTRAINT [FK_tblMovimientos_tblSucursales] FOREIGN KEY([id_sucursal])
REFERENCES [dbo].[tblSucursales] ([id_sucursal])
GO
ALTER TABLE [dbo].[tblMovimientos] CHECK CONSTRAINT [FK_tblMovimientos_tblSucursales]
GO
ALTER TABLE [dbo].[tblPermisosUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_tblPermisosUsuarios_tblPermisos] FOREIGN KEY([id_permiso])
REFERENCES [dbo].[tblPermisos] ([id_permiso])
GO
ALTER TABLE [dbo].[tblPermisosUsuarios] CHECK CONSTRAINT [FK_tblPermisosUsuarios_tblPermisos]
GO
ALTER TABLE [dbo].[tblPermisosUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_tblPermisosUsuarios_tblUsuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[tblUsuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[tblPermisosUsuarios] CHECK CONSTRAINT [FK_tblPermisosUsuarios_tblUsuarios]
GO
ALTER TABLE [dbo].[tblProductos]  WITH CHECK ADD  CONSTRAINT [FK_tblProductos_tblCategorias] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[tblCategorias] ([id_categoria])
GO
ALTER TABLE [dbo].[tblProductos] CHECK CONSTRAINT [FK_tblProductos_tblCategorias]
GO
ALTER TABLE [dbo].[tblProductos]  WITH CHECK ADD  CONSTRAINT [FK_tblProductos_tblEstadosProductos] FOREIGN KEY([id_estado])
REFERENCES [dbo].[tblEstadosProductos] ([id_estado])
GO
ALTER TABLE [dbo].[tblProductos] CHECK CONSTRAINT [FK_tblProductos_tblEstadosProductos]
GO
ALTER TABLE [dbo].[tblProductos]  WITH CHECK ADD  CONSTRAINT [FK_tblProductos_tblSubCategorias] FOREIGN KEY([id_subcategoria])
REFERENCES [dbo].[tblSubCategorias] ([id_subcategoria])
GO
ALTER TABLE [dbo].[tblProductos] CHECK CONSTRAINT [FK_tblProductos_tblSubCategorias]
GO
ALTER TABLE [dbo].[tblProductos]  WITH CHECK ADD  CONSTRAINT [FK_tblProductos_tblSucursales] FOREIGN KEY([id_sucursal])
REFERENCES [dbo].[tblSucursales] ([id_sucursal])
GO
ALTER TABLE [dbo].[tblProductos] CHECK CONSTRAINT [FK_tblProductos_tblSucursales]
GO
ALTER TABLE [dbo].[tblSubCategorias]  WITH CHECK ADD  CONSTRAINT [FK_tblSubCategorias_tblCategorias] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[tblCategorias] ([id_categoria])
GO
ALTER TABLE [dbo].[tblSubCategorias] CHECK CONSTRAINT [FK_tblSubCategorias_tblCategorias]
GO
ALTER TABLE [dbo].[tblSucursales]  WITH CHECK ADD  CONSTRAINT [FK_tblSucursales_tblEmpresa] FOREIGN KEY([id_empresa])
REFERENCES [dbo].[tblEmpresa] ([id_empresa])
GO
ALTER TABLE [dbo].[tblSucursales] CHECK CONSTRAINT [FK_tblSucursales_tblEmpresa]
GO
ALTER TABLE [dbo].[tblUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_tblUsuarios_tblRoles] FOREIGN KEY([id_rol])
REFERENCES [dbo].[tblRoles] ([id_rol])
GO
ALTER TABLE [dbo].[tblUsuarios] CHECK CONSTRAINT [FK_tblUsuarios_tblRoles]
GO
ALTER TABLE [dbo].[tblUsuarios]  WITH NOCHECK ADD  CONSTRAINT [FK_tblUsuarios_tblSucursales] FOREIGN KEY([id_sucursal])
REFERENCES [dbo].[tblSucursales] ([id_sucursal])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[tblUsuarios] CHECK CONSTRAINT [FK_tblUsuarios_tblSucursales]
GO
ALTER TABLE [dbo].[tblVentas]  WITH CHECK ADD  CONSTRAINT [FK_tblVentas_tblFacturas] FOREIGN KEY([id_factura])
REFERENCES [dbo].[tblFacturas] ([id_factura])
GO
ALTER TABLE [dbo].[tblVentas] CHECK CONSTRAINT [FK_tblVentas_tblFacturas]
GO
ALTER TABLE [dbo].[tblVentas]  WITH CHECK ADD  CONSTRAINT [FK_tblVentas_tblProductos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[tblProductos] ([id_producto])
GO
ALTER TABLE [dbo].[tblVentas] CHECK CONSTRAINT [FK_tblVentas_tblProductos]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[18] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vIngresosGastos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vIngresosGastos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblCategorias"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 95
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblEstadosProductos"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 95
               Right = 396
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblProductos"
            Begin Extent = 
               Top = 16
               Left = 477
               Bottom = 214
               Right = 642
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblSubCategorias"
            Begin Extent = 
               Top = 149
               Left = 178
               Bottom = 253
               Right = 343
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vProductos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'50
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vProductos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vProductos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblVentas"
            Begin Extent = 
               Top = 11
               Left = 37
               Bottom = 140
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "tblProductos"
            Begin Extent = 
               Top = 6
               Left = 245
               Bottom = 135
               Right = 417
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vProductosVentas'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vProductosVentas'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblProductos"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 210
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblVentas"
            Begin Extent = 
               Top = 6
               Left = 248
               Bottom = 135
               Right = 418
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vVentasProductos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vVentasProductos'
GO
USE [master]
GO
ALTER DATABASE [dbgo] SET  READ_WRITE 
GO
