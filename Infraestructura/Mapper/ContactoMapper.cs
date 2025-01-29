using Aplicacion.Dtos;
using Dominio.Entidades;

namespace Infraestructura.Mapper
{
    public class ContactoMapper
    {
      public static ContactoDto ToDto(Contacto contacto)
        {
            return new ContactoDto
            {
                ContactoId =contacto.ContactoId,
                Contacto_Correo=contacto.Contacto_Correo,
                Contacto_Telefono=contacto.Contacto_TelefonoPersonal,
                Contacto_TelefonoCasa=contacto.Contacto_TelefonoCasa,
            };
        }
        public static Contacto toEntity(ContactoDto ContactoDto)
        {
            return new Contacto
            {
                ContactoId=ContactoDto.ContactoId,
                Contacto_Correo=ContactoDto.Contacto_Correo,
                Contacto_TelefonoCasa=ContactoDto.Contacto_TelefonoCasa,
                Contacto_TelefonoPersonal=ContactoDto.Contacto_Telefono,
            };
        }
    }
}
