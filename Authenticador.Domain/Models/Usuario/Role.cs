using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticador.Domain.Models.Usuario
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Create_At { get; set; }
        public DateTime Update_At { get; set; }
    }
}
