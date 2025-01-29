namespace Dominio.Entidades
{
    public class Registro
    {
        public int RegistroId { get; set; }
        public string Registro_Usuario { get; set; } = null!;
        public string Registro_Detalle { get; set; } = null!;
        public DateTime Registro_Fecha { get; set; }
        public string Registro_Hash { get; set; } = null!;
    }
}
