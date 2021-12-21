using Authenticador.Domain.Models.Usuario;

namespace Authenticador.Domain.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<Usuario> ObterUsuarioPorUsernamePasswordAsync(string username, string password);
    }
}
