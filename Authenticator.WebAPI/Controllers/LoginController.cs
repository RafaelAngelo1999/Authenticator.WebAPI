using Authenticador.AppService.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAutenticarUsuarioAppService _autenticarUsuarioAppService;

        public LoginController(IMapper mapper, IAutenticarUsuarioAppService autenticarUsuarioAppService)
        {
            _mapper = mapper;
            _autenticarUsuarioAppService = autenticarUsuarioAppService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AutenticarAsync([FromQuery] string username, string password)
        {
            return await _autenticarUsuarioAppService.AutenticarUsuarioPorUsernamePasswordAsync(username, password);
        }
    }
}
