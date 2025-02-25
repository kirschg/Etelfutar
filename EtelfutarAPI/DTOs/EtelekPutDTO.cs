namespace EtelfutarAPI.DTOs
{
    public class EtelekPutDTO
    {
        public int Id { get; set; }

        public string Nev { get; set; } = null!;

        public int Kaloria { get; set; }

        public int Ar { get; set; }

        public int ChainId { get; set; }

        public string Indexkep { get; set; } = null!;
    }
}
