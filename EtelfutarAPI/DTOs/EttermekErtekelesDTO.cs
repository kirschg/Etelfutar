using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EttermekErtekelesDTO
    {
        public EttermekErtekelesDTO(Ertekelesek ertekeles)
        { 
            FelhasznaloId = ertekeles.FelhasznaloId;
            EtteremId = ertekeles.EtteremId;
            Szoveg = ertekeles.Szoveg;
            Ertekeles = ertekeles.Ertekeles;
        }

        public int Id { get; set; }
        public int FelhasznaloId { get; set; }
        public int EtteremId { get; set; }
        public string Szoveg { get; set; }
        public int Ertekeles { get; set; }
    }
}
