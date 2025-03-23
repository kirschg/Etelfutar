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
            Ertekeles = ettermek.Ertekeleseks.Select(x => new EttermekErtekelesDTO(x)).ToList();
            IndexKep = ettermek.Indexkep;
        }

        public int Id { get; set; }
        public string Cim { get; set; }
        public EttermekChainDTO Chain { get; set; }
        public EttermekVarosDTO Varos { get; set; }
        public List<EttermekErtekelesDTO> Ertekeles { get; set; }
        public double AVGErtekeles { get {
                if (Ertekeles.Count!=0)
                {
                    return Ertekeles.Average(x => x.Ertekeles);
                }
                else
                {
                    return 0;
                }
            } }
        public string IndexKep { get; set; }
    }
}
