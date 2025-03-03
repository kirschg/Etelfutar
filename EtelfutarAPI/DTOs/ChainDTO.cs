using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class ChainDTO
    {
        public ChainDTO(Chain chain)
        {
            Nev = chain.Nev;
            Id = chain.Id;
        }
        public int Id { get; set; }

        public string Nev { get; set; } = null!;
    }
}
