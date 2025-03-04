using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.DTOs
{
    public class RendelesFelhasznalokDTO
    {
        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
        public string Email { get; set; }
        public FelhasznalokVarosDTO Varos { get; set; }
        public string Lakcim { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int Jogosultsag { get; set; }
    }
}
