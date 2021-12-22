using Authenticador.Domain.Models;

namespace Authenticador.Domain.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<Usuario> ObterUsuarioPorUsernamePasswordAsync(string username, string password);
        TokenUsuario GerarTokenUsuario(Usuario usuario);

    }
}
