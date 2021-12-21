namespace Authenticator.WebAPI.Models.Token
{
    public class TokenAutenticadoRetorno
    {
        public Guid Id { get; set; }
        public DateTime Vencimento { get; set; }
    }
}
