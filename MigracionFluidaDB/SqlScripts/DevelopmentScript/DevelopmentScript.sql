-- Tercera Exportación de Registros de la Base de Datos
-- Características de la base de datos: tablas tbl_Usuario, tbl_Registro, tbl_Persona, tbl_Contacto
-- Autor: Julio Antonio Garcia Peza
-- Fecha: 27/12/2024
-- Fecha Modificacion: 14/01/2025

USE DB_Universidad_UBAM;
Go
-- Crear tabla tbl_Contacto si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tbl_Contacto')
BEGIN
    PRINT 'Cargando tabla tbl_Contacto...';
    CREATE TABLE tbl_Contacto (
        ContactoId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Contacto_TelefonoPersonal NVARCHAR(15) NOT NULL,
        Contacto_Correo NVARCHAR(50) NOT NULL,
        Contacto_TelefonoCasa NVARCHAR(15) NOT NULL
    );
    PRINT 'Tabla Creada: tbl_Contacto';
END
ELSE
BEGIN
    PRINT 'La tabla tbl_Contacto ya existe.';
END;
GO

-- Crear tabla tbl_Persona si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tbl_Persona')
BEGIN
    PRINT 'Cargando tabla tbl_Persona...';
    CREATE TABLE tbl_Persona (
        Persona_Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Persona_Nombre NVARCHAR(50) NOT NULL,
        Persona_ApellidoPaterno NVARCHAR(50) NOT NULL,
        Persona_ApellidoMaterno NVARCHAR(50) NOT NULL,
        Persona_FechaNacimiento DATE NOT NULL,
        Persona_ContactoId INT NOT NULL,
        Persona_Activo int DEFAULT 1 NOT NULL,
        CONSTRAINT FK_Union_Contacto FOREIGN KEY (Persona_ContactoId) REFERENCES tbl_Contacto(ContactoId),
        CONSTRAINT UQ_NombreCompleto UNIQUE (Persona_Nombre, Persona_ApellidoPaterno, Persona_ApellidoMaterno, Persona_FechaNacimiento, Persona_ContactoId)
    );
    PRINT 'Tabla Creada: tbl_Persona';
END
ELSE
BEGIN
    PRINT 'La tabla tbl_Persona ya existe.';
END;
GO

-- Crear tabla tbl_Usuario si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tbl_Usuario')
BEGIN
    PRINT 'Cargando tabla tbl_Usuario...';
    CREATE TABLE tbl_Usuario (
        Usuario_Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Usuario_Nombre NVARCHAR(50) NOT NULL,
        Usuario_ContraHash NVARCHAR(100) NOT NULL,
        Usuario_Rol NVARCHAR(20) NOT NULL,
        Usuario_PersonaId INT NULL,
        Usuario_Activo int DEFAULT 1 NOT NULL,
        CONSTRAINT FK_Union_Persona FOREIGN KEY (Usuario_PersonaId) REFERENCES tbl_Persona(Persona_Id),
        CONSTRAINT UQ_Data_User UNIQUE (Usuario_Nombre, Usuario_Rol)
    );
    PRINT 'Tabla Creada: tbl_Usuario';
END
ELSE
BEGIN
    PRINT 'La tabla tbl_Usuario ya existe.';
END;
GO
 -- 

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tbl_Registro')
BEGIN
    PRINT 'Cargando tabla tbl_Registro...';
    CREATE TABLE tbl_Registro (
        Registro_Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Registro_Usuario NVARCHAR(50) NOT NULL,
        Registro_Detalle NVARCHAR(100) NOT NULL,
        Registro_Fecha DATETIME NOT NULL,
        Registro_Hash NVARCHAR(64) NOT NULL
    );
    PRINT 'Tabla Creada: tbl_Registro';
END
ELSE
BEGIN
    PRINT 'La tabla tbl_Registro ya existe.';
END;
GO

IF NOT EXISTS (SELECT * FROM sys.Procedures WHERE name = 'sp_RegistroUsuarioCompleto')
BEGIN
    PRINT 'Cargando Stored Procedure: sp_RegistroUsuarioCompleto';
    
    EXEC('
    CREATE PROCEDURE sp_RegistroUsuarioCompleto
        @Persona_Nombre NVARCHAR(50),
        @Persona_ApellidoPaterno NVARCHAR(50),
        @Persona_ApellidoMaterno NVARCHAR(50),
        @Persona_FechaNacimiento DATE,
        @Contacto_TelefonoPersonal NVARCHAR(50),
        @Contacto_Correo NVARCHAR(50),
        @Contacto_TelefonoCasa NVARCHAR(50),
        @Usuario_Nombre NVARCHAR(50),
        @Usuario_Contrasena NVARCHAR(100),
        @Usuario_Rol NVARCHAR(20)
    AS
    BEGIN
        BEGIN TRANSACTION;
 
        IF @Persona_FechaNacimiento > GETDATE()
        BEGIN
            PRINT ''Error: La fecha de nacimiento no puede ser mayor que la fecha actual.'';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        BEGIN TRY
            DECLARE @Contacto_ID INT, @Persona_ID INT;

            -- Insert Contacto
            INSERT INTO tbl_Contacto (Contacto_Correo, Contacto_TelefonoCasa, Contacto_TelefonoPersonal)
            VALUES (@Contacto_Correo, @Contacto_TelefonoCasa, @Contacto_TelefonoPersonal);

            SET @Contacto_ID = SCOPE_IDENTITY();

            -- Insert Persona
            INSERT INTO tbl_Persona (Persona_Nombre, Persona_ApellidoPaterno, Persona_ApellidoMaterno, Persona_FechaNacimiento, Persona_ContactoId)
            VALUES (@Persona_Nombre, @Persona_ApellidoPaterno, @Persona_ApellidoMaterno, @Persona_FechaNacimiento, @Contacto_ID);
        
            SET @Persona_ID = SCOPE_IDENTITY();

            -- Insert Usuario (with password hashed)
            INSERT INTO tbl_Usuario (Usuario_Nombre, Usuario_ContraHash, Usuario_Rol, Usuario_PersonaId)
            VALUES (@Usuario_Nombre,@Usuario_Contrasena, @Usuario_Rol, @Persona_ID);

            -- Insert Registro (Log the operation)
            INSERT INTO tbl_Registro (Registro_Usuario, Registro_Detalle, Registro_Fecha, Registro_Hash) 
            VALUES (@Usuario_Nombre, ''Ingreso de Usuario nuevo: '' + @Usuario_Nombre, GETDATE(), HASHBYTES(''SHA2_256'', ''UsuarioNuevo''));

            PRINT ''Usuario registrado exitosamente.'';
            COMMIT TRANSACTION;
        END TRY
        BEGIN CATCH
            PRINT ''Error en la operación de inserción: '' + ERROR_MESSAGE();
            ROLLBACK TRANSACTION;
        END CATCH;
    END
    ');
    
    PRINT 'El procedimiento almacenado fue creado correctamente.';
END
ELSE
BEGIN
    PRINT 'El procedimiento almacenado ya existe.';
END;
