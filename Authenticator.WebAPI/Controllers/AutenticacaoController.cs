using Authenticador.AppService.Interfaces;
using Authenticator.WebAPI.Models.Token;
using Authenticator.WebAPI.Models.Usuario;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAutenticarUsuarioAppService _autenticarUsuarioAppService;

        public AutenticacaoController(IMapper mapper, IAutenticarUsuarioAppService autenticarUsuarioAppService)
        {
            _mapper = mapper;
            _autenticarUsuarioAppService = autenticarUsuarioAppService;
        }

        [HttpPost]
        [Route("Autenticar")]
        public async Task<ActionResult<dynamic>> AutenticarAsync([FromQuery] string username, string password)
        {
            var usuario = await _autenticarUsuarioAppService.AutenticarUsuarioPorUsernamePasswordAsync(username, password);

            return new UsuarioAutenticadoRetorno
            {
                Id = usuario.DadosUsuario.Id,
                Email = usuario.DadosUsuario.Email,
                Role = usuario.DadosUsuario.Role.Descricao,
                Username = usuario.DadosUsuario.Username,
                Token = new TokenAutenticadoRetorno { Key = usuario.Token.Key, Vencimento = usuario.Token.DataExpiracao }
            };
        }
    }
}
