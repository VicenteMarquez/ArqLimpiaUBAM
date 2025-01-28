using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Ubam.Evolution.Domain.Entities;
using Ubam.Evolution.Domain.Enum;
using Ubam.Evolution.Domain.Exceptions;
using Ubam.Evolution.Domain.Interfaces;

namespace Infrastructure.Repositories;

public class PersonRepository(ApplicationDbContext context) : IPersonRepository
{
    public async Task<Persona?> GetByIdAsync(Guid id)
    {
        var person = await context.People.FirstOrDefaultAsync(p => p.Id_Persona == id);
        if (person == null) throw new ValidationException(ExceptionEnum.DataAccessException);

        return person;
    }

    public async Task<Persona?> GetByCurpAsync(string curp)
    {
        var person = await context.People.FirstOrDefaultAsync(p => p.Curp_Persona == curp);
        return person;
    }

    public async Task<List<Persona>> GetAllAsync()
    {
        var people = await context.People.ToListAsync();
        if (people == null) throw new ValidationException(ExceptionEnum.DataAccessException);

        return people;
    }

    public async Task<List<Persona>> GetDocentesAsync()
    {
        var docentes = await context.UserRoles
            .Where(ur => ur.Id_Rol == Guid.Parse("b4ea1a59-3f92-4993-96cb-c85ac306c621"))
            .Join(context.Users, ur => ur.Id_Usuario, u => u.Id_Usuario, (ur, u) => u.Id_Persona)
            .Join(context.People, personId => personId, p => p.Id_Persona, (personId, person) => person)
            .ToListAsync();

        if (docentes == null) throw new ValidationException(ExceptionEnum.DataAccessException);

        return docentes;
    }

    public async Task<List<Persona>> GetAlumnosAsync()
    {
        var alumnos = await context.UserRoles
            .Where(ur => ur.Id_Rol == Guid.Parse("25fbde92-32c6-4180-9d35-1eff9288df39"))
            .Join(context.Users, ur => ur.Id_Usuario, u => u.Id_Usuario, (ur, u) => u.Id_Persona)
            .Join(context.People, personId => personId, p => p.Id_Persona, (personId, person) => person)
            .ToListAsync();

        if (alumnos == null) throw new ValidationException(ExceptionEnum.DataAccessException);

        return alumnos;
    }

    public async Task<Persona> AddAsync(Persona persona)
    {
        ArgumentNullException.ThrowIfNull(persona);

        var existingPerson = await GetByCurpAsync(persona.Curp_Persona);
        if (existingPerson != null) throw new ValidationException(ExceptionEnum.UserAlreadyExists);

        await context.People.AddAsync(persona);
        await context.SaveChangesAsync();

        return persona;
    }

    public async Task UpdateAsync(Persona persona)
    {
        var personEntity = await context.People.FirstOrDefaultAsync(p => p.Id_Persona == persona.Id_Persona);

        if (personEntity == null) throw new ValidationException(ExceptionEnum.DataAccessException);

        personEntity.Nombre_Persona = persona.Nombre_Persona;
        personEntity.ApellidoPaterno_Persona = persona.ApellidoPaterno_Persona;
        personEntity.ApellidoMaterno_Persona = persona.ApellidoMaterno_Persona;
        personEntity.FechaDeNacimiento_Persona = persona.FechaDeNacimiento_Persona;
        personEntity.Genero_Persona = persona.Genero_Persona;
        personEntity.Curp_Persona = persona.Curp_Persona;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var person = await GetByIdAsync(id);

        if (person == null) throw new ValidationException(ExceptionEnum.DataAccessException);

        context.People.Remove(person);
        await context.SaveChangesAsync();
    }
}