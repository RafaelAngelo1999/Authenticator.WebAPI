using Authenticator.WebAPI.Models.Token;

namespace Authenticator.WebAPI.Models.Usuario
{
    public class UsuarioAutenticadoRetorno
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public TokenAutenticadoRetorno Token { get; set; }
    }
}
