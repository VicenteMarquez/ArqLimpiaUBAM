--1633584323-CreateSchema.sql
--Autor:Vicente Marquez
--Objetivo:Creación de un esquema de Base de datos para objetos de Base dde datos 
--Fecha: 2024-09-30
--Comentarios_Aquí de pueden crear en esta fase, los esquemas de base de datos

-- Nombre de la base de datos
DECLARE @DatabaseName NVARCHAR(50) = 'PatosaCommercial';

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = @DatabaseName)
BEGIN
    PRINT 'Creando la base de datos: ' + @DatabaseName;
    EXEC('CREATE DATABASE ' + @DatabaseName);
END
ELSE
BEGIN
    PRINT 'La base de datos ya existe: ' + @DatabaseName;
END

--1633584323-CreateSchema.sql