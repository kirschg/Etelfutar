using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.DTOs
{
    public class RendeltEtelDTO
    {
        public RendeltEtelDTO(int etelId, int rendelesId)
        {
            EtelId = etelId;
            RendelesId = rendelesId;
        }

        public RendeltEtelDTO()
        {
            
        }

        public int EtelId { get; set; }
        public int RendelesId { get; set; }
    }
}
