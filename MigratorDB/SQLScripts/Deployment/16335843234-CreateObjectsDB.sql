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

-- Usar la base de datos
DECLARE @SQL NVARCHAR(MAX);
SET @SQL = N'USE ' + QUOTENAME(@DatabaseName) + N';';

-- Tabla de Usuarios
SET @SQL = @SQL + N'
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = ''Users'')
BEGIN
    PRINT ''Creando la tabla Users.'';
    CREATE TABLE Users (
        Id INT PRIMARY KEY IDENTITY(1,1),
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Email NVARCHAR(100) NOT NULL UNIQUE,
        CreatedAt DATETIME DEFAULT GETDATE()
    );
    PRINT ''Tabla Users creada correctamente.'';
END
ELSE
BEGIN
    PRINT ''La tabla Users ya existe.'';
END

-- Validación de campos en la tabla Users
IF COL_LENGTH(''Users'', ''FirstName'') IS NULL
BEGIN
    PRINT ''La columna FirstName no existe en la tabla Users.'';
END
ELSE
BEGIN
    PRINT ''La columna FirstName existe en la tabla Users.'';
END

IF COL_LENGTH(''Users'', ''LastName'') IS NULL
BEGIN
    PRINT ''La columna LastName no existe en la tabla Users.'';
END
ELSE
BEGIN
    PRINT ''La columna LastName existe en la tabla Users.'';
END

IF COL_LENGTH(''Users'', ''Email'') IS NULL
BEGIN
    PRINT ''La columna Email no existe en la tabla Users.'';
END
ELSE
BEGIN
    PRINT ''La columna Email existe en la tabla Users.'';
END

-- Insertar datos en la tabla Users
IF NOT EXISTS (SELECT * FROM Users)
BEGIN
    PRINT ''Insertando datos en la tabla Users.'';
    INSERT INTO Users (FirstName, LastName, Email)
    VALUES 
    (''John'', ''Doe'', ''john.doe@example.com''),
    (''Jane'', ''Smith'', ''jane.smith@example.com''),
    (''Carlos'', ''Hernandez'', ''carlos.hernandez@example.com'');
    PRINT ''Datos insertados en la tabla Users.'';
END
ELSE
BEGIN
    PRINT ''La tabla Users ya tiene datos.'';
END
';

-- Tabla de Productos
SET @SQL = @SQL + N'
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = ''Products'')
BEGIN
    PRINT ''Creando la tabla Products.'';
    CREATE TABLE Products (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ProductName NVARCHAR(100) NOT NULL,
        Price DECIMAL(18, 2) NOT NULL,
        CreatedAt DATETIME DEFAULT GETDATE()
    );
    PRINT ''Tabla Products creada correctamente.'';
END
ELSE
BEGIN
    PRINT ''La tabla Products ya existe.'';
END

-- Validación de campos en la tabla Products
IF COL_LENGTH(''Products'', ''ProductName'') IS NULL
BEGIN
    PRINT ''La columna ProductName no existe en la tabla Products.'';
END
ELSE
BEGIN
    PRINT ''La columna ProductName existe en la tabla Products.'';
END

IF COL_LENGTH(''Products'', ''Price'') IS NULL
BEGIN
    PRINT ''La columna Price no existe en la tabla Products.'';
END
ELSE
BEGIN
    PRINT ''La columna Price existe en la tabla Products.'';
END

-- Insertar datos en la tabla Products
IF NOT EXISTS (SELECT * FROM Products)
BEGIN
    PRINT ''Insertando datos en la tabla Products.'';
    INSERT INTO Products (ProductName, Price)
    VALUES 
    (''Product A'', 19.99),
    (''Product B'', 29.99),
    (''Product C'', 39.99);
    PRINT ''Datos insertados en la tabla Products.'';
END
ELSE
BEGIN
    PRINT ''La tabla Products ya tiene datos.'';
END
';

-- Ejecutar el SQL dinámico
EXEC sp_executesql @SQL;