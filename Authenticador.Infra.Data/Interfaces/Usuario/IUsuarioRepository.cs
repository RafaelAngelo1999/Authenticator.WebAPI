using Authenticador.Infra.Data.Entities.Usuario;

namespace Authenticador.Infra.Data.Interfaces.Usuario
{
    public interface IUsuarioRepository
    {
        Task<UsuarioEntity> ObterUsuarioPorUsernamePassword(string username, string password);
    }
}
