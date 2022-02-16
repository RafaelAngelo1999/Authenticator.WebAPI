using Authenticador.Infra.Data.Entities.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Authenticador.Infra.Data.Repositories.Base
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<RoleEntity> Role { get; set; }

    }
}
