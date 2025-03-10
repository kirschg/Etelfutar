using EtelfutarWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.DTOs
{
    public class EtelekDTO
    {
        public EtelekDTO(Etelek etelek)
        {
            Id = etelek.Id;
            Nev = etelek.Nev;
            Kaloria = etelek.Kaloria;
            Ar = etelek.Ar;
            Indexkep = etelek.Indexkep;
            Chain = new EtelekChainDTO(etelek.Chain);
        }

        public int Id { get; set; }

        public string Nev { get; set; } = null!;

        public int Kaloria { get; set; }

        public int Ar { get; set; }

        public EtelekChainDTO Chain { get; set; }

        public string Indexkep { get; set; } = null!;
    }
}
