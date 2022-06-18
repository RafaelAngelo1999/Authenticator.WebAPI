using Authenticador.AppService.AppServices;
using Authenticador.Domain.Exceptions.Usuario;
using Authenticador.Domain.Interfaces;
using Authenticador.Domain.Models;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Authneticador.Testes.AppService
{
    public class AutenticarUsuarioTeste
    {
        private readonly AutenticarUsuarioAppService _autenticarUsuarioAppService;
        private readonly Mock<IAutenticacaoService> _autenticacaoService;

        private readonly Usuario usuarioSucessMock = new Usuario
        {
            Id = Guid.NewGuid(),
            Email = "rafael@gmail.com",
            Username = "rafael",
            Password = "123",
            Role = new Role { Id = Guid.NewGuid(), Descricao = "Descricao", Create_At = DateTime.Now, Update_At = DateTime.Now },
            Create_At = DateTime.Now,
            Update_At = DateTime.Now
        };

        private readonly TokenUsuario tokenUsuarioMock = new TokenUsuario
        {
            Key = "123",
            DataExpiracao = DateTime.Now            
        };

        public AutenticarUsuarioTeste()
        {
            _autenticacaoService = new Mock<IAutenticacaoService>();
            _autenticarUsuarioAppService = new AutenticarUsuarioAppService(_autenticacaoService.Object);
        }

        [Fact]
        [Trait(nameof(AutenticarUsuarioAppService.AutenticarUsuarioPorUsernamePasswordAsync), "Sucesso")]
        public async Task AutenticarUsuarioPorUsernamePasswordAsync_Sucesso()
        {
            _autenticacaoService.Setup(x => x.ObterUsuarioPorUsernamePasswordAsync(It.IsAny<String>(), It.IsAny<String>())).ReturnsAsync(usuarioSucessMock);
            _autenticacaoService.Setup(x => x.GerarTokenUsuario(usuarioSucessMock)).Returns(tokenUsuarioMock);

            var actionResult = await _autenticarUsuarioAppService.AutenticarUsuarioPorUsernamePasswordAsync(It.IsAny<String>(), It.IsAny<String>());

            Assert.NotNull(actionResult);

            Assert.Equal(actionResult.DadosUsuario, usuarioSucessMock);
            Assert.Equal(actionResult.Token, tokenUsuarioMock);
        }

        [Fact]
        [Trait(nameof(AutenticarUsuarioAppService.AutenticarUsuarioPorUsernamePasswordAsync), "Nao_Encontrado_Invalido")]
        public async Task AutenticarUsuarioPorUsernamePasswordAsync_Nao_Encontrado_Invalido()
        {
            _autenticacaoService.Setup(x => x.ObterUsuarioPorUsernamePasswordAsync(It.IsAny<String>(), It.IsAny<String>()));

            var ex = await Assert.ThrowsAsync<AutenticarUsuarioDomainException>(() => _autenticarUsuarioAppService.AutenticarUsuarioPorUsernamePasswordAsync(USERNAME_VALID, PASSWORD_VALID)); 

            Assert.Single(ex.Errors);
            Assert.Equal("UsernameOrPasswordInvalid", ex.Errors.First().Key);
        }
    }
}
