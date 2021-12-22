using Authenticador.Domain.Interfaces;
using Authenticador.Domain.Models;
using Authenticador.Infra.Data.Entities.Usuario;
using Authenticador.Infra.Data.Interfaces.Usuario;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public TokenUsuario GerarTokenUsuario(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("d41d8cd98f00b204e9800998ecf8427e");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role.Descricao.ToString()),

                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenUsuario { Key = tokenHandler.WriteToken(token), DataExpiracao = (DateTime)tokenDescriptor.Expires };
        }
    }
}
