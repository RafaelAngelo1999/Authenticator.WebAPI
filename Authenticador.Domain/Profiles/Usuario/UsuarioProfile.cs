using Authenticador.Infra.Data.Entities.Usuario;
using AutoMapper;

namespace Authenticador.Domain.Profiles.Usuario
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Models.Usuario, UsuarioEntity>()
                .ReverseMap();
        }
    }
}
