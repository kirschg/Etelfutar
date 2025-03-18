using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EttermekChainDTO
    {
        public EttermekChainDTO(Chain chain)
        {
            Nev = chain.Nev;
            Id = chain.Id;
        }
        public int Id { get; set; }
        public string Nev { get; set; } = null!;
    }
}
