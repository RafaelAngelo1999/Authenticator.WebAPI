namespace Authenticador.Domain.Models.Usuario
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime Create_At { get; set; }
        public DateTime Update_At { get; set; }
    }
}
