namespace Authenticador.Infra.Data.Entities.Usuario
{
    public class UsuarioEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public RoleEntity Role { get; set; }
        public Guid RoleId { get; set; }
        public DateTime Create_At { get; set; }
        public DateTime Update_At { get; set; }
    }
}
