using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class RendelesByFelhasznaloDTO
    {
        public RendelesByFelhasznaloDTO(Rendeles rendeles)
        {
            Id = rendeles.Id;
            Osszar = rendeles.OsszAr;
            Rendeles = 
        }
        public List<RendelesByFelhasznaloEtelDTO> Rendeles { get; set; }

        public int Id { get; set; }

        public int Osszar { get; set; }
    }
}
