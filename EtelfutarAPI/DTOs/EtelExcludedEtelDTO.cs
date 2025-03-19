using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EtelExcludedEtelDTO
    {
        public EtelExcludedEtelDTO(Etelek etel)
        {
            Id = etel.Id;
            Nev = etel.Nev;
        }
    

        public int Id { get; set; }
        public string Nev { get; set; }
    }

}
