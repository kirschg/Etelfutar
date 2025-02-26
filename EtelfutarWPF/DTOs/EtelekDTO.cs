using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.DTOs
{
    public class EtelekDTO
    {
        public int Id { get; set; }

        public string Nev { get; set; } = null!;

        public int Kaloria { get; set; }

        public int Ar { get; set; }

        public EtelekChainDTO Chain { get; set; }

        public string Indexkep { get; set; } = null!;
    }
}
