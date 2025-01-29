-- Primara Migración Fluida
-- Autor Julio Antonio Garcia Peza
-- Poder comprender el uso de la arquitectura limpia y la implementación en un login seguro
-- Fecha 19/12/2024

Declare @DatabaseName nvarchar(50) ='DB_Universidad_UBAM';

if not exists (Select*from sys.databases Where name= @DatabaseName)
	Begin
		print 'Cargando Base de datos: '+@DatabaseName;
		Exec(' Create Database'+@DatabaseName);
	End
Else
	Begin 
		print 'La base de datos ya existe:' +@DatabaseName;
	end