using Authenticador.AppService.AppServices;
using Authenticador.Domain.Exceptions.Usuario;
using Authenticador.Domain.Interfaces;
using Authenticador.Domain.Models;
using Authenticador.Domain.Services;
using Authenticador.Infra.Data.Entities.Usuario;
using Authenticador.Infra.Data.Interfaces.Usuario;
using AutoMapper;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Authneticador.Testes.Service
{
    public class AutenticacaoServiceTeste
    {
        private readonly AutenticacaoService _autenticacaoService;
        private readonly Mock<IUsuarioRepository> _usuarioRepository;
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly Mock<IMapper> _mapper;

        private readonly UsuarioEntity usuarioSucessMock = new UsuarioEntity
        {
            Id = Guid.NewGuid(),
            Email = "rafael@gmail.com",
            Username = "rafael",
            Password = "123",
            Role = new RoleEntity(),
            Create_At = DateTime.Now,
            Update_At = DateTime.Now
        };

        private readonly RoleEntity roleSucessMock = new RoleEntity
        {
            Id = Guid.NewGuid(),
            Descricao = "Descricao",
            Create_At = DateTime.Now,
            Update_At = DateTime.Now
        };

        private readonly TokenUsuario tokenUsuarioMock = new TokenUsuario
        {
            Key = "123",
            DataExpiracao = DateTime.Now
        };

        public AutenticacaoServiceTeste()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _roleRepository = new Mock<IRoleRepository>();
            _mapper = new Mock<IMapper>();
            _autenticacaoService = new AutenticacaoService(_usuarioRepository.Object, _mapper.Object, _roleRepository.Object);
        }

        [Fact]
        [Trait(nameof(AutenticacaoService.ObterUsuarioPorUsernamePasswordAsync), "Sucesso")]
        public async Task ObterUsuarioPorUsernamePasswordAsync_Sucesso()
        {
            _usuarioRepository.Setup(x => x.ObterUsuarioPorUsernamePassword(It.IsAny<String>(), It.IsAny<String>())).ReturnsAsync(usuarioSucessMock);
            _roleRepository.Setup(x => x.ObterRolePorId(usuarioSucessMock.Role.Id)).ReturnsAsync(roleSucessMock);

            var actionResult = await _autenticacaoService.ObterUsuarioPorUsernamePasswordAsync(It.IsAny<String>(), It.IsAny<String>());

            Assert.NotNull(actionResult);

            Assert.Equal(actionResult.Id, usuarioSucessMock.Id);
            Assert.Equal(actionResult.Username, usuarioSucessMock.Username);
            Assert.Equal(actionResult.Password, usuarioSucessMock.Password);
            Assert.Equal(actionResult.Update_At, usuarioSucessMock.Update_At);
            Assert.Equal(actionResult.Create_At, usuarioSucessMock.Create_At);
            Assert.Equal(actionResult.Email, usuarioSucessMock.Email);

            Assert.Equal(actionResult.Role.Id, usuarioSucessMock.Role.Id);
            Assert.Equal(actionResult.Role.Descricao, usuarioSucessMock.Role.Descricao);
            Assert.Equal(actionResult.Role.Descricao, usuarioSucessMock.Role.Descricao);


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
