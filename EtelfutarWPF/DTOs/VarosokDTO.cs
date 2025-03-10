using EtelfutarWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.DTOs
{
    public class VarosokDTO
    {
        public VarosokDTO(Varosok varosok)
        {
            Id = varosok.Id;
            Nev = varosok.Nev;
            IndexKep = varosok.IndexKep;
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        public string IndexKep { get; set; }
    }
}
