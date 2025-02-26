using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.DTOs
{
    public class EttermekDTO
    {
        public int Id { get; set; }
        public string Cim { get; set; }
        public EttermekChainDTO Chain { get; set; }
        public EttermekVarosDTO Varos { get; set; }
        public string IndexKep { get; set; }
    }
}
