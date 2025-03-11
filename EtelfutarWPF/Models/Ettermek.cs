using EtelfutarWPF.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarWPF.Models
{
    public class Ettermek
    {
        public Ettermek(EttermekDTO ettermekDTO)
        {
            Id = ettermekDTO.Id;
            Cim = ettermekDTO.Cim;
            ChainId = ettermekDTO.Id;
            VarosId = ettermekDTO.Varos.Id;
            Indexkep = ettermekDTO.IndexKep;
        }
        public Ettermek()
        {

        }

        public int Id { get; set; }

        public string Cim { get; set; } = null!;

        public int ChainId { get; set; }

        public int VarosId { get; set; }

        public string Indexkep { get; set; } = null!;
        public virtual Chain? Chain { get; set; } = null!;

        public virtual Varosok? Varos { get; set; } = null!;
        public virtual ICollection<Etelek> Etels { get; set; } = new List<Etelek>();
    }
}
