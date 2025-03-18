using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class FelhasznaloErtekelesDTO
    {
        public FelhasznaloErtekelesDTO(Felhasznalok felhasznalo)
        {
            Id = felhasznalo.Id;
            FelhasznaloNev = felhasznalo.FelhasznaloNev;
        }

        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
    }
}
