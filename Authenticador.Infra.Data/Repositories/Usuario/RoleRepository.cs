using Authenticador.Infra.Data.Entities.Usuario;
using Authenticador.Infra.Data.Interfaces.Usuario;
using Authenticador.Infra.Data.Repositories.Base;

namespace Authenticador.Infra.Data.Repositories.Usuario
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RoleEntity> ObterRolePorId(Guid roleId)
        {
            return _context.Role.FirstOrDefault(role => role.Id == roleId);
        }
    }
}
