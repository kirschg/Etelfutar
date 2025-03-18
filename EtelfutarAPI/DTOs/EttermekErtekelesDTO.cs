using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EttermekErtekelesDTO
    {
        public EttermekErtekelesDTO(Ettermek etterem)
        { 
            Id = etterem.Id;
            Cim = etterem.Cim;
            ChainId = etterem.ChainId;
        }

        public int Id { get; set; }
        public string Cim { get; set; }
        public int ChainId { get; set; }
    }
}
