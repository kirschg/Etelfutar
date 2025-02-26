using EtelfutarWPF.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.Models
{
    public class Etelek
    {
        public Etelek(EtelekDTO etelekDTO)
        {
            Id = etelekDTO.Id;
            Nev = etelekDTO.Nev;
            Kaloria = etelekDTO.Kaloria;
            Ar = etelekDTO.Ar;
            ChainId = etelekDTO.Chain.Id;
            Indexkep = etelekDTO.Indexkep;
        }
        public Etelek()
        {

        }

        public int Id { get; set; }

        public string Nev { get; set; } = null!;

        public int Kaloria { get; set; }

        public int Ar { get; set; }

        public int ChainId { get; set; }

        public string Indexkep { get; set; } = null!;
    }
}
