using Authenticador.Domain.Models;
using Authenticador.Infra.Data.Entities.Usuario;
using AutoMapper;

namespace Authenticador.Domain.Profiles.Usuario
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleEntity>()
                .ReverseMap();
        }
    }
}
