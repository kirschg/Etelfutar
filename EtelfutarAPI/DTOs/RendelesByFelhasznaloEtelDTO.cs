using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class RendelesByFelhasznaloEtelDTO
    {
        public RendelesByFelhasznaloEtelDTO(Etelek etel)
        { 
            Id = etel.Id;
            Nev = etel.Nev;
            Kaloria = etel.Kaloria;
            Ar = etel.Ar;
        }
        public int Id { get; set; }

        public int Ar { get; set; }

        public string Nev { get; set; } = null!;

        public int Kaloria { get; set; }
    }
}
