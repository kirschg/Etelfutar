using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EtelRendelesDTO
    {
        public EtelRendelesDTO(int etelId, int felhasznaloId)
        { 
            EtelId = etelId;
            FelhasznaloId = felhasznaloId;
        }

        public int EtelId { get; set; }
        public int FelhasznaloId { get; set; }
    }
}
