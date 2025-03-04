using EtelfutarWPF.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.Models
{
    public class Rendeles
    {
        public Rendeles(RendelesDTO rendelesDTO)
        {
            Id = rendelesDTO.Id;
            FelhasznaloId = rendelesDTO.Felhasznalo.Id;
            OsszAr = rendelesDTO.Osszar;
        }
        public Rendeles()
        {

        }

        public int Id { get; set; }

        public int FelhasznaloId { get; set; }

        public int OsszAr { get; set; }
    }
}
