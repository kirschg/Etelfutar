using EtelfutarAPI.Models;
using System.Security.Policy;

namespace EtelfutarAPI.DTOs
{
    public class FelhasznaloByTokenDTO
    {
        public FelhasznaloByTokenDTO(Felhasznalok felhasznalo)
        {
            Id = felhasznalo.Id;
            FelhasznaloNev = felhasznalo.FelhasznaloNev;
            TeljesNev = felhasznalo.TeljesNev;
            Email = felhasznalo.Email;
            Varos = new FelhasznalokVarosDTO(felhasznalo.Varos);
            Lakcim = felhasznalo.Lakcim;
        }

        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
        public string TeljesNev { get; set; }
        public string Email { get; set; }
        public FelhasznalokVarosDTO Varos { get; set; }
        public string Lakcim { get; set; }
    }
}
