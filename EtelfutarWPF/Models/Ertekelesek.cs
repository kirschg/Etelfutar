using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.Models
{
    public class Ertekelesek
    {
        public int Id { get; set; }

        public int FelhasznaloId { get; set; }

        public int EtteremId { get; set; }

        public string Szoveg { get; set; } = null!;

        public int Ertekeles { get; set; }
    }
}
