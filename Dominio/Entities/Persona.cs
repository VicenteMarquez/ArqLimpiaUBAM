using System.Text.Json.Serialization;
using Dominio.Excepciones;
namespace Dominio.Entidades
{
    public class Persona
    {
        
        public int  Persona_Id { get;  set; }
        public string Persona_Nombre { get; set; } = null!;
        public string Persona_ApellidoPaterno { get; set; } = null!;
        public string Persona_ApellidoMaterno { get; set; } = null!;
        public DateTime Persona_FechaNacimiento { get; set; }
        public int Persona_ContactoId { get; set; }
        public int Persona_Activo { get; set;}
        public Contacto Contactos { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Usuario> usuarios { get; set; }  = new List<Usuario>();
       public Persona()
        {
            List<Usuario> usuarios = new List<Usuario>();
        }
    }
}
