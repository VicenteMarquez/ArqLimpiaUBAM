-- Segundo exportación de registros de la base de datos
-- Características de la base de datos: tablas: tbl_Usuario, tbl_HistorialUsuario, tbl_Persona

USE DB_Universidad_UBAM;
GO

-- Insertar registros en tbl_Contacto
BEGIN TRY
    INSERT INTO tbl_Contacto (Contacto_TelefonoPersonal, Contacto_Correo, Contacto_TelefonoCasa)
    VALUES
    ('55464484162', 'tg8189041@gmail', '55478496'),
    ('55464485162', 'JuanPerez12@gmail.com', '9876543211'),
    ('1234567892', 'JuanPerez12@gmail.com', '9876543212'),
    ('55464484162', 'Juanswer@gmail.com', '55478496561');
END TRY
BEGIN CATCH
    PRINT 'Error al insertar los datos en tbl_Contacto.';
END CATCH;

GO

-- Insertar registros en tbl_Persona
BEGIN TRY
    INSERT INTO tbl_Persona (Persona_Nombre, Persona_ApellidoPaterno, Persona_ApellidoMaterno, Persona_FechaNacimiento, Persona_ContactoId)
    VALUES
    ('Julio Antonio', 'Garcia', 'Peza', '2004-02-16', 1),
    ('Maria', 'Lopez', 'Martinez', '1985-05-15', 2),
    ('Carlos', 'Diaz', 'Hernandez', '1992-08-20', 3),
    ('Ana', 'Sanchez', 'Rodriguez', '1995-12-25', 4)
END TRY
BEGIN CATCH
    PRINT 'Error al insertar los datos en tbl_Persona.';
END CATCH;

GO

-- Insertar registros en tbl_Usuario
BEGIN TRY
    INSERT INTO tbl_Usuario (Usuario_Nombre, Usuario_ContraHash, Usuario_Rol, Usuario_PersonaId)
    VALUES
    ('Julio Garza','BvJ79sBUCE6QLw6rRL9vZKISrs1dIhSQlsLA0BSHBeghyZsvJSQbga0e+DXCX+bnXFIVWOCLPEIPT7gjLsGZDQ==', 'admin', 1),
    ('marialopez', 'BvJ79sBUCE6QLw6rRL9vZKISrs1dIhSQlsLA0BSHBeghyZsvJSQbga0e+DXCX+bnXFIVWOCLPEIPT7gjLsGZDQ==', 'empleado', 2),
    ('carlosdiaz', 'BvJ79sBUCE6QLw6rRL9vZKISrs1dIhSQlsLA0BSHBeghyZsvJSQbga0e+DXCX+bnXFIVWOCLPEIPT7gjLsGZDQ==', 'admin', 3),
    ('anasanchez', 'BvJ79sBUCE6QLw6rRL9vZKISrs1dIhSQlsLA0BSHBeghyZsvJSQbga0e+DXCX+bnXFIVWOCLPEIPT7gjLsGZDQ==', 'cliente', 4)
END TRY
BEGIN CATCH
    PRINT 'Error al insertar los datos en tbl_Usuario.';
END CATCH;

GO



