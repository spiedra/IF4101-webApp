CREATE DATABASE IF4101_EXAMEN_B97452;
GO

USE IF4101_EXAMEN_B97452;
GO

CREATE SCHEMA ADMINISTRACION
GO

CREATE SCHEMA CLIENTES
GO

/*  TABLAS DEL ESQUEMA ADMINISTRACIÓN */

CREATE TABLE ADMINISTRACION.tb_ESTADIA
(
	ID           INT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE       VARCHAR(32) NOT NULL,
	PROVINCIA    VARCHAR(32) NOT NULL,
	DIRECCION    VARCHAR(46) NOT NULL,
	PRECIO_NOCHE DECIMAL(9,2) NOT NULL,
	CAPACIDAD    INT NOT NULL, 
	RUTA_IMAGEN  VARCHAR(46) NOT NULL,
	ID_CATEGORIA INT NOT NULL, 
	DESCRIPCION  VARCHAR(62) NULL,
	IS_DELETED BIT DEFAULT 0 NULL,
	CONSTRAINT FK_ESTADIA_CATEGORIA FOREIGN KEY (ID_CATEGORIA) REFERENCES ADMINISTRACION.tb_CATEGORIA(ID)
)

CREATE TABLE ADMINISTRACION.tb_CATEGORIA
(
	ID INT IDENTITY(1,1) PRIMARY KEY, 
	TIPO VARCHAR(36) NOT NULL,
	IS_DELETED BIT DEFAULT 0 NULL
)

CREATE TABLE ADMINISTRACION.tb_RESERVA
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	ID_ESTADIA INT NOT NULL,
	CEDULA_CLIENTE VARCHAR(42) NOT NULL,
	CANTIDAD_PERSONAS INT NOT NULL,
	FECHA_ENTRADA DATE NULL, 
	FECHA_SALIDA  DATE NULL,
	IS_DELETED BIT DEFAULT 0 NULL,
	CONSTRAINT FK_RESERVA_ESTADIA FOREIGN KEY (ID_ESTADIA) REFERENCES ADMINISTRACION.tb_ESTADIA(ID),
	CONSTRAINT FK_RESERVA_CLIENTE FOREIGN KEY (CEDULA_CLIENTE) REFERENCES CLIENTES.tb_CLIENTE(CEDULA)
)

/*  TABLAS DEL ESQUEMA CLIENTES */

CREATE TABLE CLIENTES.tb_CLIENTE
(
	CEDULA VARCHAR(42) PRIMARY KEY,
	NOMBRE_COMPLETO VARCHAR(58) NOT NULL,
	TELEFONO_CONTACTO VARCHAR(43) NOT NULL,
	IS_DELETED BIT DEFAULT 0 NULL
)