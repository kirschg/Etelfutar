using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EtelRendelesDTO
    {
        public EtelRendelesDTO(int etelId, int rendelesId)
        { 
            EtelId = etelId;
            RendelesId = rendelesId;
        }

        public int EtelId { get; set; }
        public int RendelesId { get; set; }
    }
}
