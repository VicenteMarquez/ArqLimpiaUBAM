using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositories
{
    public class ContactoRepository :IContactoRepository
    {
        private readonly DataContext _context;

        public ContactoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Contacto> GetContactoAsync(int ContactoId)
        {
            return await _context.Contactos.FindAsync(ContactoId);
        }

        public async Task<int> CrearContactoAsync(Contacto contacto)
        {
            await _context.Contactos.AddAsync(contacto);
            await _context.SaveChangesAsync();
            return contacto.ContactoId;
        }


        public async Task ActualizarContactoAsync(Contacto contacto)
        {
            _context.Contactos.Update(contacto);
            await _context.SaveChangesAsync();
        }
    }
}
