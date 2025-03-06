using EtelfutarAPI.Models;

namespace EtelfutarAPI.DTOs
{
    public class LearazasDTO
    {
        public LearazasDTO(Learaza learazas)
        {
            //Etterem = new EttermekLearazasDTO(learazas.Etterem);
            //Etel = new EtelLearazasDTO(learazas.Etel);
            Learazas = learazas.Learazas;
        }

        public EttermekLearazasDTO Etterem { get; set; }
        public EtelLearazasDTO Etel { get; set; }
        public int Learazas { get; set; }
    }
}
