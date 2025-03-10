using EtelfutarWPF.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.Models
{
    public partial class Felhasznalok
    {
        /*public Felhasznalok(FelhasznalokDTO felhasznalokDTO)
        {
            Id = felhasznalokDTO.Id;
            FelhasznaloNev = felhasznalokDTO.FelhasznaloNev;
            TeljesNev = felhasznalokDTO.TeljesNev;
            Email = felhasznalokDTO.Email;
            VarosId = felhasznalokDTO.Varos.Id;
            Lakcim = felhasznalokDTO.Lakcim;
            Hash = felhasznalokDTO.Hash;
            Salt = felhasznalokDTO.Salt;
            Jogosultsag = felhasznalokDTO.Jogosultsag;
        }*/

        public Felhasznalok()
        {

        }
        public int Id { get; set; }

        public string FelhasznaloNev { get; set; } = null!;

        public string TeljesNev { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int VarosId { get; set; }

        public string Lakcim { get; set; } = null!;

        public string Hash { get; set; } = null!;

        public string Salt { get; set; } = null!;

        public int Jogosultsag { get; set; }

        public int Aktiv { get; set; }
        public virtual ICollection<Ertekelesek> Ertekeleseks { get; set; } = new List<Ertekelesek>();

        public virtual Rendeles? Rendeles { get; set; }

        public Varosok? Varos { get; set; } = null!;
    }
}
