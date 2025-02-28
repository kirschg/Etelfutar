using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class RendelesByFelhasznaloDTO
    {
        public RendelesByFelhasznaloDTO(Rendeles rendeles)
        {
            Id = rendeles.Id;
            //Osszar = rendeles.OsszAr;
            foreach (var e in rendeles.Etels)
            {
                Rendeles.Add(new RendelesByFelhasznaloEtelDTO(e));
            }
        }
        public List<RendelesByFelhasznaloEtelDTO> Rendeles { get; set; } = new List<RendelesByFelhasznaloEtelDTO> ();

        public int Id { get; set; }

        public int Osszar { get {
                int sum = 0;
                Rendeles.Sum(x => sum += x.Ar);
                return sum; 
            } }
    }
}
