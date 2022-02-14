using System.Diagnostics.CodeAnalysis;

namespace HP.Authenticador.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public abstract class DomainError
    {
        protected DomainError(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; }
        public string Message { get; }
    }
}
