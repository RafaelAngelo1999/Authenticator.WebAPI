namespace Authenticador.Domain.Models
{
    public class UsuarioAutenticado
    {
        public Usuario DadosUsuario { get; set; }
        public TokenUsuario Token { get; set; }
    }
}
