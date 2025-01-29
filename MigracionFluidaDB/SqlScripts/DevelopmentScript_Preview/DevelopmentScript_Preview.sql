-- Primera Migración Fluida
-- Autor: Julio Antonio Garcia Peza
-- Descripción: Creación de bases de datos necesarias para el proyecto.
-- Fecha: 19/12/2024

-- Creación de la base de datos DB_Universidad_UBAM
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DB_Universidad_UBAM')
BEGIN
    PRINT 'Cargando Base de datos: DB_Universidad_UBAM';
    CREATE DATABASE DB_Universidad_UBAM;
END
ELSE
BEGIN
    PRINT 'La base de datos ya existe: DB_Universidad_UBAM';
END;
