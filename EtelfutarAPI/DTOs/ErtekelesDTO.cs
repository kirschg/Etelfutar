using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class ErtekelesDTO
    {
        public ErtekelesDTO(Ertekelesek ertekeles)
        { 
            Id = ertekeles.Id;
            Felhasznalo = new FelhasznaloErtekelesDTO(ertekeles.Felhasznalo);
            Etterem = new EttermekErtekelesDTO(ertekeles);
            Szoveg = ertekeles.Szoveg;
            Ertekeles = ertekeles.Ertekeles;
        }

        public int Id { get; set; }
        public FelhasznaloErtekelesDTO Felhasznalo { get; set; }
        public EttermekErtekelesDTO Etterem { get; set; }
        public string Szoveg { get; set; }
        public int Ertekeles { get; set; }
    }
}
