create database CrudNet8ADO
GO

USE CrudNet8ADO
GO

CREATE TABLE CONTACTO(
    Id int primary key identity not null,
    Nombre varchar(max) not null,
    Celular varchar(max) not null,
    Email varchar(max) not null,
    Estado bit default 1,
	UsuarioCreacion varchar(max) not null,
    FechaCreacion datetime default GETDATE(),
	UsuarioModificacion varchar(max) not null,
    FechaModificacion datetime default GETDATE()
)

--drop table CONTACTO

  insert into dbo.Contacto (Nombre, Celular, Email, UsuarioCreacion, UsuarioModificacion)
  values('leonardo','3213160130','leonardo.amaya94@hotmail.com','admin', 'admin');

  Select * from CONTACTO where Estado = 1


  CREATE TABLE Errores (
	ErrorId INT IDENTITY(1,1) PRIMARY KEY,
	ErrorMessage NVARCHAR(4000),
	ErrorSeverity INT,
	ErrorState INT,
	ErrorTime DATETIME
);
