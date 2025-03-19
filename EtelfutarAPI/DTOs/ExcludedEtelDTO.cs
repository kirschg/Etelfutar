using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class ExcludedEtelDTO
    {
        public ExcludedEtelDTO(int etel, int etterem)
        {
            EtteremId = etterem;
            EtelId = etel;
        }

        public int EtteremId { get; set; }
        public int EtelId { get; set; }
    }
}
