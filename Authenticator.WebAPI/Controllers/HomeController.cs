using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("Anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonimo";

        [HttpGet]
        [Route("Authenticated")]
        [Authorize]
        public string Authenticated() => "Autenticado";

        [HttpGet]
        [Route("Employee")]
        [Authorize(Roles = "Employee,Manger")]
        public string Employee() => "Funcionario";

        [HttpGet]
        [Route("Manager")]
        [Authorize(Roles = "Manager")]
        public string Manager() => "Gerente";
    }
}
