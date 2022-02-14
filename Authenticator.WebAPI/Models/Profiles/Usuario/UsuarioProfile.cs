using Authenticador.Infra.Data.Entities.Usuario;
using Authenticator.WebAPI.Models.Usuario;
using AutoMapper;

namespace Authenticator.WebAPI.Models.Profiles.Usuario
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioInput, UsuarioEntity>()
                .ReverseMap();
        }
    }
}
