using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.Models
{
    public class Ettermek
    {
        public int Id { get; set; }

        public string Cim { get; set; } = null!;

        public int ChainId { get; set; }

        public int VarosId { get; set; }

        public string Indexkep { get; set; } = null!;
    }
}
