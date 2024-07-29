USE[CrudNet8ADO]
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Leonardo Amaya
-- Create date: 25/07/2024
-- Description:	Crud Ado net 8
-- =============================================
CREATE OR ALTER PROCEDURE sp_crudAdoNet
	-- Add the parameters for the stored procedure here
	@opcion int,
	@id int,
	@nombre varchar(100) = '',
	@celular varchar(100) = '',
	@email varchar(100) = '',
	@estado bit = 1,
	@usuarioCreacion varchar(max) = '',
	@fechaCreacion datetime = '',
	@usuarioModificacion varchar(max) = '',
	@fechaModificacion datetime = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Captura el error
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT;

		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();

    BEGIN TRY
		-- Inicia la transacción
		BEGIN TRANSACTION;

		IF @opcion = 1
		BEGIN
			SELECT 
			Id,
			Nombre,
			Celular,
			Email,
			Estado,
			UsuarioCreacion,
			FechaCreacion,
			UsuarioModificacion,
			FechaModificacion
			FROM CONTACTO
			WHERE Estado = 1
		END

		IF @opcion = 2
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM CONTACTO WHERE Id = @id)
				BEGIN
				 RAISERROR ('El contacto no existe en la base de datos', 16,1);
				 INSERT INTO Errores (ErrorMessage, ErrorSeverity, ErrorState, ErrorTime)
				 VALUES ('El contacto no existe en la base de datos', @ErrorSeverity, @ErrorState, GETDATE());
				 RETURN;
				END
			ELSE
				BEGIN
					SELECT 
					Id,
					Nombre,
					Celular,
					Email,
					Estado,
					UsuarioCreacion,
					FechaCreacion,
					UsuarioModificacion,
					FechaModificacion
					FROM CONTACTO
					WHERE Id = @id
					AND Estado = 1
				END
		END

		IF @opcion = 3
		BEGIN
			IF EXISTS (SELECT * FROM CONTACTO WHERE Nombre = @nombre AND Celular = @celular AND Email = @email)
			BEGIN
				RAISERROR ('El cliente ya existe en nuestra base de datos', 16, 1);
				INSERT INTO Errores (ErrorMessage, ErrorSeverity, ErrorState, ErrorTime)
				 VALUES ('El cliente ya existe en nuestra base de datos', @ErrorSeverity, @ErrorState, GETDATE());
				RETURN;
			END

			INSERT INTO CONTACTO(Nombre, Celular, Email, UsuarioCreacion, UsuarioModificacion) 
			VALUES (@nombre, @celular, @email, @usuarioCreacion, @usuarioModificacion);

			--SELECT @id = SCOPE_IDENTITY();
		END

		IF @opcion = 4
		BEGIN
			IF NOT EXISTS (SELECT * FROM CONTACTO WHERE Id = @id AND Nombre = @nombre AND Celular = @celular AND Email = @email)
			BEGIN
				RAISERROR ('El cliente no existe en nuestra base de datos', 16, 1);
				INSERT INTO Errores (ErrorMessage, ErrorSeverity, ErrorState, ErrorTime)
				 VALUES ('El cliente no existe en nuestra base de datos', @ErrorSeverity, @ErrorState, GETDATE());
				RETURN;
			END

			UPDATE CONTACTO
			SET Nombre = @nombre,
				Celular = @celular,
				Email = @email,
				Estado = @estado,
				UsuarioModificacion = @usuarioModificacion,
				FechaModificacion = GETDATE()
			WHERE Id = @id;

			--SELECT @id = SCOPE_IDENTITY();
		END

		IF @opcion = 5
		BEGIN
			IF NOT EXISTS (SELECT * FROM CONTACTO WHERE Id = @id)
			BEGIN
				RAISERROR ('El cliente no existe en nuestra base de datos', 16, 1);
				INSERT INTO Errores (ErrorMessage, ErrorSeverity, ErrorState, ErrorTime)
				 VALUES ('El cliente no existe en nuestra base de datos', @ErrorSeverity, @ErrorState, GETDATE());
				RETURN;
			END

			--DELETE FROM Clientes WHERE Id = @id;

			UPDATE CONTACTO
			SET Estado = 0,
				FechaModificacion = GETDATE()
			WHERE Id = @id;

			--SELECT @id = SCOPE_IDENTITY();
		END

		-- Confirma la transacción si todo es exitoso
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH

		-- Revertir la transacción
		ROLLBACK TRANSACTION;

		-- Inserta el error en la tabla de errores
		INSERT INTO Errores (ErrorMessage, ErrorSeverity, ErrorState, ErrorTime)
		VALUES (@ErrorMessage, @ErrorSeverity, @ErrorState, GETDATE());

		-- Re-lanza el error para su manejo en el cliente
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
	END CATCH
END
GO
