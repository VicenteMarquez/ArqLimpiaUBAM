using Dominio.Excepciones;
using System.Text.Json.Serialization;

namespace Dominio.Entidades
{
    public class Contacto
    {
        
        public int ContactoId { get; set; }
        public string Contacto_TelefonoPersonal { get; set; } = null!;
        public string Contacto_Correo { get; set; } = null!;
        public string Contacto_TelefonoCasa { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Persona> Personas { get; set; } = new List<Persona>();

        public Contacto()
        {
            List<Persona> list = new List<Persona>();
        }
    }
}
