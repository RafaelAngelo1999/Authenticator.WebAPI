using Authenticador.AppService.Interfaces;
using Authenticador.Domain.Interfaces;
using Authenticador.Domain.Models.Usuario;

namespace Authenticador.AppService.AppServices
{
    public class AutenticarUsuarioAppService : IAutenticarUsuarioAppService
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticarUsuarioAppService(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }
        public async Task<Usuario> AutenticarUsuarioPorUsernamePasswordAsync(string username, string password)
        {
            return await _autenticacaoService.ObterUsuarioPorUsernamePasswordAsync(username, password).ConfigureAwait(false);
        }
    }
}
