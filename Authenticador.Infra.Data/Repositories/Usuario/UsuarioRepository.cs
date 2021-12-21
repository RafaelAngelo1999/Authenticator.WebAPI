using Authenticador.Infra.Data.Entities.Usuario;
using Authenticador.Infra.Data.Interfaces.Usuario;
using Authenticador.Infra.Data.Repositories.Base;

namespace Authenticador.Infra.Data.Repositories.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioEntity> ObterUsuarioPorUsernamePassword(string username, string password)
        {
            return _context.Usuario.FirstOrDefault(user => user.Username.ToLower() == username.ToLower() && user.Password.ToLower() == password);
        }
    }
}
