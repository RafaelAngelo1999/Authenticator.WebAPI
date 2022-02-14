using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace HP.Authenticador.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public abstract class DomainException : Exception
    {
        protected DomainException(string message)
            : base(message)
        {

        }
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public abstract string Key { get; }

        protected ICollection<DomainError> errors = new List<DomainError>();

        public IEnumerable<DomainError> Errors { get { return errors; } }
    }

    [ExcludeFromCodeCoverage]
    public abstract class DomainException<T> : DomainException
        where T : DomainError
    {
        protected DomainException()
            : base("Ocorreu um erro de negócio, verifique a propriedade 'errors' para obter detalhes.")
        {

        }

        protected DomainException(string message)
            : base(message)
        {

        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DomainException AddError(params T[] errors)
        {
            foreach (var error in errors)
            {
                this.errors.Add(error);
            }

            return this;
        }
    }
}
