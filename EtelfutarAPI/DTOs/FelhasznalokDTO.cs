using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class FelhasznalokDTO
    {
        public FelhasznalokDTO(Felhasznalok felhasznalok)
        { 
            Id = felhasznalok.Id;
            FelhasznaloNev = felhasznalok.FelhasznaloNev;
            TeljesNev = felhasznalok.TeljesNev;
            Email = felhasznalok.Email;
            Varos = new FelhasznalokVarosDTO(felhasznalok.Varos);
            Lakcim = felhasznalok.Lakcim;
            Hash = felhasznalok.Hash;
            Salt = felhasznalok.Salt;
            Aktiv = felhasznalok.Aktiv;
            Jogosultsag = felhasznalok.Jogosultsag;
        }

        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
        public string TeljesNev { get; set; }
        public string Email { get; set; }
        public FelhasznalokVarosDTO Varos { get; set; }
        public string Lakcim { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int Aktiv { get; set; }
        public int Jogosultsag { get; set; }
    }
}
