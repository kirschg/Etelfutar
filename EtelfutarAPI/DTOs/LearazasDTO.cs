using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class LearazasDTO
    {
        public LearazasDTO(Learaza learazas)
        {
            Etel = new EtelLearazasDTO(learazas);
            Learazas = learazas.Learazas;
        }

        public EtelLearazasDTO Etel { get; set; }
        public int Learazas { get; set; }
    }
}
