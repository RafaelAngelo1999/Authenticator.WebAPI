using Authenticador.Domain.Interfaces;
using Authenticador.Domain.Models.Usuario;
using Authenticador.Infra.Data.Entities.Usuario;
using Authenticador.Infra.Data.Interfaces.Usuario;
using AutoMapper;

namespace Authenticador.Domain.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public AutenticacaoService(IUsuarioRepository usuarioRepository, IMapper mapper, IRoleRepository roleRepository)
        {
            _usuarioRepository = usuarioRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<Usuario> ObterUsuarioPorUsernamePasswordAsync(string username, string password)
        {
            var usuarioEntity = await _usuarioRepository.ObterUsuarioPorUsernamePassword(username, password);

            usuarioEntity.Role = await _roleRepository.ObterRolePorId(usuarioEntity.RoleId);

            return _mapper.Map<UsuarioEntity, Usuario>(usuarioEntity);
        }
    }
}
