namespace UsuariosAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? CorreoElectronico { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
