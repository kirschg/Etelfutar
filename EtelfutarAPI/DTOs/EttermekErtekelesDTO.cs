using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EttermekErtekelesDTO
    {
        public EttermekErtekelesDTO(Ertekelesek ertekeles)
        { 
            Id = ertekeles.Id;
            FelhasznaloId = ertekeles.FelhasznaloId;
            EtteremId = ertekeles.EtteremId;
        }

        public int Id { get; set; }
        public int FelhasznaloId { get; set; }
        public int EtteremId { get; set; }
    }
}
