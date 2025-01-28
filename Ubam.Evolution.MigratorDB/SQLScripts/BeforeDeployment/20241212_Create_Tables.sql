-- Tablas principales
IF NOT EXISTS (SELECT *
               FROM sys.objects
               WHERE object_id = OBJECT_ID(N'[dbo].[Persona]')
                 AND type = 'U')
    BEGIN
        CREATE TABLE [dbo].[Persona]
        (
            Id_Persona                UNIQUEIDENTIFIER PRIMARY KEY,
            Nombre_Persona            NVARCHAR(100) NOT NULL,
            ApellidoPaterno_Persona   NVARCHAR(100) NOT NULL,
            ApellidoMaterno_Persona   NVARCHAR(100) NOT NULL,
            FechaDeNacimiento_Persona DATE          NOT NULL,
            Genero_Persona            NVARCHAR(10)  NOT NULL,
            Curp_Persona              NVARCHAR(18)  NOT NULL
        );

        CREATE UNIQUE INDEX IX_Persona_Curp ON [dbo].[Persona] (Curp_Persona);
        CREATE INDEX IX_Persona_Nombres ON [dbo].[Persona] (Nombre_Persona, ApellidoPaterno_Persona, ApellidoMaterno_Persona);
    END;

IF NOT EXISTS (SELECT *
               FROM sys.objects
               WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]')
                 AND type = 'U')
    BEGIN
        CREATE TABLE [dbo].[Usuario]
        (
            Id_Usuario      UNIQUEIDENTIFIER PRIMARY KEY,
            Nombre_Usuario  NVARCHAR(100)    NOT NULL,
            Clave_Usuario   NVARCHAR(256)    NOT NULL,
            Estatus_Usuario BIT              NOT NULL,
            Id_Persona      UNIQUEIDENTIFIER NOT NULL,
            FOREIGN KEY (Id_Persona) REFERENCES Persona (Id_Persona)
        );

        CREATE UNIQUE INDEX IX_Usuario_NombreUsuario ON [dbo].[Usuario] (Nombre_Usuario);
        CREATE INDEX IX_Usuario_IdPersona ON [dbo].[Usuario] (Id_Persona);
        CREATE INDEX IX_Usuario_Estatus ON [dbo].[Usuario] (Estatus_Usuario);
    END;

IF NOT EXISTS (SELECT *
               FROM sys.objects
               WHERE object_id = OBJECT_ID(N'[dbo].[Rol]')
                 AND type = 'U')
    BEGIN
        CREATE TABLE [dbo].[Rol]
        (
            Id_Rol     UNIQUEIDENTIFIER PRIMARY KEY,
            Nombre_Rol NVARCHAR(100) NOT NULL
        );

        CREATE UNIQUE INDEX IX_Rol_NombreRol ON [dbo].[Rol] (Nombre_Rol);
    END;

IF NOT EXISTS (SELECT *
               FROM sys.objects
               WHERE object_id = OBJECT_ID(N'[dbo].[UsuarioRol]')
                 AND type = 'U')
    BEGIN
        CREATE TABLE [dbo].[UsuarioRol]
        (
            Id_UsuarioRol UNIQUEIDENTIFIER PRIMARY KEY,
            Id_Usuario    UNIQUEIDENTIFIER NOT NULL,
            Id_Rol        UNIQUEIDENTIFIER NOT NULL,
            FOREIGN KEY (Id_Usuario) REFERENCES Usuario (Id_Usuario),
            FOREIGN KEY (Id_Rol) REFERENCES Rol (Id_Rol)
        );

        CREATE INDEX IX_UsuarioRol_IdUsuario ON [dbo].[UsuarioRol] (Id_Usuario);
        CREATE INDEX IX_UsuarioRol_IdRol ON [dbo].[UsuarioRol] (Id_Rol);
        CREATE UNIQUE INDEX IX_UsuarioRol_Combinado ON [dbo].[UsuarioRol] (Id_Usuario, Id_Rol);
    END;

IF NOT EXISTS (SELECT *
               FROM sys.objects
               WHERE object_id = OBJECT_ID(N'[dbo].[HistorialInicioSesion]')
                 AND type = 'U')
    BEGIN
        CREATE TABLE [dbo].[HistorialInicioSesion]
        (
            Id_HistorialInicioSesion UNIQUEIDENTIFIER PRIMARY KEY,
            Id_Usuario               UNIQUEIDENTIFIER NOT NULL,
            Fecha_InicioSesion       DATETIME         NOT NULL,
            Descripcion_InicioSesion NVARCHAR(100)    NOT NULL,

            FOREIGN KEY (Id_Usuario) REFERENCES Usuario (Id_Usuario)
        );

        CREATE INDEX IX_HistorialInicioSesion_IdUsuario ON [dbo].[HistorialInicioSesion] (Id_Usuario);
        CREATE INDEX IX_HistorialInicioSesion_Fecha ON [dbo].[HistorialInicioSesion] (Fecha_InicioSesion);
    END;
