using Application.DTOs;
using Ubam.Evolution.Domain.Entities;

namespace Application.Mappers;

public class LoginHistoryMapper
{
    public HistorialInicioSesion ToEntity(HistoryLoginDto loginHitoryDto)
    {
        return new HistorialInicioSesion
        {
            Id_HistorialInicioSesion = Guid.NewGuid(),
            Id_Usuario = loginHitoryDto.UserId,
            Fecha_InicioSesion = DateTime.Now,
            Descripcion_InicioSesion = loginHitoryDto.Description
        };
    }

    public HistoryLoginDto ToDto(Guid userId, string description)
    {
        return new HistoryLoginDto
        {
            UserId = userId,
            Description = description
        };
    }
}