using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class RendelesDTO
    {
        public RendelesDTO(Rendeles rendeles)
        { 
            Id = rendeles.Id;
            Felhasznalo = new RendelesFelhasznalokDTO(rendeles.Felhasznalo);
        }

        public int Id { get; set; }
        public RendelesFelhasznalokDTO Felhasznalo { get; set; }
        public int Osszar { get; set; }
    }
}
