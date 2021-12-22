namespace Authenticador.Domain.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Create_At { get; set; }
        public DateTime Update_At { get; set; }
    }
}
