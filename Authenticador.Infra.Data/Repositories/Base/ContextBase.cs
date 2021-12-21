using Authenticador.Infra.Data.Entities.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Authenticador.Infra.Data.Repositories.Base
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<RoleEntity> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>()
               .HasData(new RoleEntity { Id = Guid.Parse("4ce35776-1d62-438b-aae9-59bfb252d74d"), Descricao = "Manager", Create_At = DateTime.Now, Update_At = DateTime.Now });

            //modelBuilder.Entity<UsuarioEntity>()
            //    .HasData(new UsuarioEntity { Id = Guid.NewGuid(), Username = "Rafael", Password = "123", Role = new RoleEntity { Id = Guid.Parse("4ce35776-1d62-438b-aae9-59bfb252d74d"), Descricao = "Manager", Create_At = DateTime.Now, Update_At = DateTime.Now }, Email = "rafaelangelowow@gmail.com", Create_At = DateTime.Now, Update_At = DateTime.Now });

        }
    }
}
