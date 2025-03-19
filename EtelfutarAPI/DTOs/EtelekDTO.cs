using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class EtelekDTO
    {
        public EtelekDTO(Etelek etelek) 
        {
            Id = etelek.Id;
            Nev = etelek.Nev;
            Kaloria = etelek.Kaloria;
            Ar = etelek.Ar;
            //Learazas = new EtelLearazasDTO(etelek.Learazas);
            Indexkep = etelek.Indexkep;
            Chain = new EtelekChainDTO(etelek.Chain);
        }

        public int Id { get; set; }

        public string Nev { get; set; } = null!;

        public int Kaloria { get; set; }

        public int Ar { get; set; }

        public EtelekChainDTO Chain { get; set; }

        public EtelLearazasDTO Learazas { get; set; }

        public string Indexkep { get; set; } = null!;
    }
}
