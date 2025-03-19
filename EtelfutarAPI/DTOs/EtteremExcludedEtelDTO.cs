using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EtteremExcludedEtelDTO
    {
        public EtteremExcludedEtelDTO(Ettermek etterem)
        { 
            Id = etterem.Id;
            Cim = etterem.Cim;
            ChainId = etterem.ChainId;
            VarosId = etterem.VarosId;
        }

        public int Id { get; set; }
        public string Cim { get; set; }
        public int ChainId { get; set; }
        public int VarosId { get; set; }
    }
}
