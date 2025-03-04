using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.DTOs
{
    public class RendelesDTO
    {
        public int Id { get; set; }
        public RendelesFelhasznalokDTO Felhasznalo { get; set; }
        public int Osszar { get; set; }
    }
}
