using Authenticador.Domain.Models.Usuario;

namespace Authenticador.AppService.Interfaces
{
    public interface IAutenticarUsuarioAppService
    {
        Task<Usuario> AutenticarUsuarioPorUsernamePasswordAsync(string username, string password);
    }
}
