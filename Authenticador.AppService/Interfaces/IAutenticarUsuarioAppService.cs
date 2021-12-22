using Authenticador.Domain.Models;

namespace Authenticador.AppService.Interfaces
{
    public interface IAutenticarUsuarioAppService
    {
        Task<UsuarioAutenticado> AutenticarUsuarioPorUsernamePasswordAsync(string username, string password);
    }
}
