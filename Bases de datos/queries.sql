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
    FechaCreacion datetime default GETDATE(),
    FechaModificacion datetime default GETDATE()
)


  insert into dbo.Contacto (Nombre, Celular, Email)
  values('leonardo','3213160130','leonardo.amaya94@hotmail.com')

  Select * from CONTACTO


  CREATE TABLE Errores (
	ErrorId INT IDENTITY(1,1) PRIMARY KEY,
	ErrorMessage NVARCHAR(4000),
	ErrorSeverity INT,
	ErrorState INT,
	ErrorTime DATETIME
);
