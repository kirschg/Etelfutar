using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EttermekDTO
    {
        public EttermekDTO(Ettermek ettermek)
        { 
            Id = ettermek.Id;
            Cim = ettermek.Cim;
            Chain = new EttermekChainDTO(ettermek.Chain);
            Varos = new EttermekVarosDTO(ettermek.Varos);
            Ertekeles = ettermek.Ertekeles;
            IndexKep = ettermek.Indexkep;
        }

        public int Id { get; set; }
        public string Cim { get; set; }
        public EttermekChainDTO Chain { get; set; }
        public EttermekVarosDTO Varos { get; set; }
        public EttermekErtekelesDTO Ertekeles { get; set; }
        public string IndexKep { get; set; }
    }
}
