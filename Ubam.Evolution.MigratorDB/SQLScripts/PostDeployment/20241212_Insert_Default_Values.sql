-- Insertar registros en Persona
IF NOT EXISTS (SELECT 1
               FROM Persona)
    BEGIN
        INSERT INTO Persona (Id_Persona, Nombre_Persona, ApellidoPaterno_Persona, ApellidoMaterno_Persona,
                             FechaDeNacimiento_Persona, Genero_Persona, Curp_Persona)
        VALUES ('b6318d7c-6684-4772-93ff-ff7f0da81164', 'ramiro zein', 'contreras', 'rangel', '2003-09-25', 'masculino',
                'CORR900310HDFAZBD2'),
               ('69eded4b-c246-4300-8d38-a78b06e505ce', 'maría', 'jain', 'sánchez', '1985-07-22', 'femenino',
                'HERH850722MDFBJDG2'),
               ('2032ffd4-fc61-40f1-96e3-e1724baefabf', 'yuritzi', 'flores', 'robles', '1992-12-01', 'femenino',
                'MARR921201HDFRAZL8');
    END;

-- Insertar registros en Rol
IF NOT EXISTS (SELECT 1
               FROM Rol)
    BEGIN
        INSERT INTO Rol (Id_Rol, Nombre_Rol)
        VALUES ('23e35e60-d2e3-4591-b849-ee4c711f1a82', 'Administrador'),
               ('b4ea1a59-3f92-4993-96cb-c85ac306c621', 'Docente'),
               ('25fbde92-32c6-4180-9d35-1eff9288df39', 'Alumno');
    END;

-- Insertar registros en Usuario con GUIDs estáticos
IF NOT EXISTS (SELECT 1
               FROM Usuario)
    BEGIN
        INSERT INTO Usuario (Id_Usuario, Nombre_Usuario, Clave_Usuario, Estatus_Usuario, Id_Persona)
        VALUES ('9ecb2e7e-0fbd-475e-9dba-27e33d1641af', 'ramiro.zein',
                'gaBc5EzVRrgTiatH1BmMKw==:p8Y6NfWmeay75sEpMbbjjZuUyC8+rwqvzMolqwpVu4k=', 1,
                'b6318d7c-6684-4772-93ff-ff7f0da81164'),
               ('df0f4d2c-d4ad-49b1-8330-0098ee193c6d', 'maria.jain',
                'gaBc5EzVRrgTiatH1BmMKw==:p8Y6NfWmeay75sEpMbbjjZuUyC8+rwqvzMolqwpVu4k=', 1,
                '69eded4b-c246-4300-8d38-a78b06e505ce'),
               ('1f51ca6d-da54-494f-9817-19f29b3550d0', 'yuri.flores',
                'gaBc5EzVRrgTiatH1BmMKw==:p8Y6NfWmeay75sEpMbbjjZuUyC8+rwqvzMolqwpVu4k=', 0,
                '2032ffd4-fc61-40f1-96e3-e1724baefabf');
    END;

-- Insertar registros en UsuarioRol con GUIDs estáticos
IF NOT EXISTS (SELECT 1
               FROM UsuarioRol)
    BEGIN
        INSERT INTO UsuarioRol (Id_UsuarioRol, Id_Usuario, Id_Rol)
        VALUES ('7c96f5c6-9188-44e7-bdb5-56407491196a', '9ecb2e7e-0fbd-475e-9dba-27e33d1641af',
                '23e35e60-d2e3-4591-b849-ee4c711f1a82'),
               ('ee25c51f-f5ed-464e-b2ab-6a666ccfa2ab', 'df0f4d2c-d4ad-49b1-8330-0098ee193c6d',
                'b4ea1a59-3f92-4993-96cb-c85ac306c621'),
               ('154ad3e4-492e-4f3f-ad13-eb8142a628f6', '1f51ca6d-da54-494f-9817-19f29b3550d0',
                '25fbde92-32c6-4180-9d35-1eff9288df39');
    END;
