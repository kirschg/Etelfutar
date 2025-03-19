using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.DTOs
{
    public class ExcludedEtelDTO
    {
        public ExcludedEtelDTO(int etelId, int etteremId)
        {
            EtelId = etelId;
            EtteremId = etteremId;
        }
        public ExcludedEtelDTO()
        {

        }

        public int EtelId { get; set; }
        public int EtteremId { get; set; }
    }
}
