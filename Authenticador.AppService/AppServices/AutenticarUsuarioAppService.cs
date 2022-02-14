using Authenticador.AppService.Interfaces;
using Authenticador.Domain.Exceptions.Usuario;
using Authenticador.Domain.Interfaces;
using Authenticador.Domain.Models;

namespace Authenticador.AppService.AppServices
{
    public class AutenticarUsuarioAppService : IAutenticarUsuarioAppService
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticarUsuarioAppService(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }
        public async Task<UsuarioAutenticado> AutenticarUsuarioPorUsernamePasswordAsync(string username, string password)
        {
            var usuario =  await _autenticacaoService.ObterUsuarioPorUsernamePasswordAsync(username, password).ConfigureAwait(false);
            if (usuario == null) throw new AutenticarUsuarioDomainException(AutenticarUsuarioError.UserNameOrPasswordInvalid);

            var token =  _autenticacaoService.GerarTokenUsuario(usuario);

            return new UsuarioAutenticado { Token = token, DadosUsuario = usuario };
        }
    }
}
