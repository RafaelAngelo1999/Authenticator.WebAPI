using HP.Authenticador.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Authenticador.Domain.Exceptions.Usuario
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class AutenticarUsuarioDomainException : DomainException<AutenticarUsuarioError>
    {
        public override string Key => "ObterClientePorCpfDomainException";

        public AutenticarUsuarioDomainException(AutenticarUsuarioError autenticarUsuarioError)
        => AddError(autenticarUsuarioError);

        protected AutenticarUsuarioDomainException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }

    }

    [ExcludeFromCodeCoverage]
    public class AutenticarUsuarioError : DomainError
    {
        public static AutenticarUsuarioError UserNameOrPasswordInvalid
            => new("UsernameOrPasswordInvalid",
                "O Username ou password é invalido!");


        protected AutenticarUsuarioError(string key, string message)
            : base(key, message)
        {
        }
    }
}

