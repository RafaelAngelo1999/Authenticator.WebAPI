using Authenticador.Infra.Data.Entities.Usuario;

namespace Authenticador.Infra.Data.Interfaces.Usuario
{
    public interface IRoleRepository
    {
        Task<RoleEntity> ObterRolePorId(Guid roleId);
    }
}
