using Authenticador.Domain.Models.Usuario;
using Authenticador.Infra.Data.Entities.Usuario;
using AutoMapper;

namespace Authenticador.Domain.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Models.Usuario.Usuario, UsuarioEntity>()
                .ReverseMap();
        }
    }
}
